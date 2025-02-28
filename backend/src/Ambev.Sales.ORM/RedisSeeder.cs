using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using Newtonsoft.Json;
using System.Xml;

namespace Ambev.Sales.ORM
{
    public  static class RedisSeeder
    {
        private const string SaleKey = "sale:123456";

        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger(typeof(RedisSeeder));

            try
            {
                var redis = services.GetRequiredService<IConnectionMultiplexer>();
                var db = redis.GetDatabase();

                // 🔹 Aguarda o Redis estar pronto antes de continuar
                await WaitForRedisAsync(db, logger);

                // 🔹 Criando o JSON da venda
                var saleJson = new
                {
                    saleNumber = "123456",
                    saleDate = "2024-02-26T12:00:00Z",
                    customerId = "550e8400-e29b-41d4-a716-446655440000",
                    divisionId = "123e4567-e89b-12d3-a456-426614174000",
                    items = new[]
                    {
                        new { productId = "c3fcd3d7-99b2-4f18-8e83-cccd50a39320", quantity = 2, unitPrice = 10.50, discount = 1.00 },
                        new { productId = "a7f3c2d8-882a-43df-9aa5-53c2d42b96c5", quantity = 1, unitPrice = 20.00, discount = 0.00 }
                    }
                };

                string saleJsonString = System.Text.Json.JsonSerializer.Serialize(saleJson);

                // 🔹 Salvando no Redis com expiração de 24 horas
                await db.StringSetAsync(SaleKey, saleJsonString, TimeSpan.FromHours(24));
                logger.LogInformation($"✅ Venda salva no Redis com chave: {SaleKey}");

                // 🔹 Verificando se a venda foi salva corretamente
                var savedSale = await db.StringGetAsync(SaleKey);
                if (!savedSale.IsNullOrEmpty)
                {
                    logger.LogInformation("✅ Venda armazenada com sucesso no Redis!");
                }
                else
                {
                    logger.LogError("🚨 Erro ao salvar a venda no Redis.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "🚨 Erro ao executar o DatabaseSeeder.");
            }
        }

        private static async Task WaitForRedisAsync(IDatabase db, ILogger logger)
        {
            int retryCount = 5;
            while (retryCount > 0)
            {
                try
                {
                    await db.PingAsync();
                    logger.LogInformation("✅ Redis está pronto!");
                    return;
                }
                catch
                {
                    logger.LogWarning("⏳ Aguardando Redis...");
                    await Task.Delay(2000); // Aguarda 2 segundos antes de tentar novamente
                    retryCount--;
                }
            }

            throw new Exception("🚨 Não foi possível conectar ao Redis após várias tentativas.");
        }
    }
}