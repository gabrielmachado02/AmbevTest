using Ambev.Sales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Ambev.Sales.ORM;

public class DefaultContext : DbContext
{
    public DbSet<Sale> Sales { get; set; }

    public DbSet<SaleItem> SaleItems { get; set; }

    public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
public class YourDbContextFactory : IDesignTimeDbContextFactory<DefaultContext>
{
    public DefaultContext CreateDbContext(string[] args)
    {
        Console.WriteLine("➡️ DefaultContextFactory está sendo executado...");

        // 🔍 Detecta se estamos em ambiente de desenvolvimento
        string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
        Console.WriteLine($"🔹 Ambiente detectado: {environment}");

        // 🔍 Obtém o caminho do projeto principal (Ambev.Sales.WebApi)
        string basePath = Path.Combine(Directory.GetCurrentDirectory(), "../Ambev.Sales.WebApi");
        Console.WriteLine($"🔹 Base Path: {basePath}");

        // Define o nome do arquivo de configuração baseado no ambiente
        string configFileName = environment == "Development" ? "appsettings.Development.json" : "appsettings.json";
        Console.WriteLine($"🔹 Usando arquivo de configuração: {configFileName}");

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(basePath) // 📌 Busca o appsettings correto
            .AddJsonFile(configFileName, optional: false, reloadOnChange: true)
            .Build();

        var builder = new DbContextOptionsBuilder<DefaultContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        Console.WriteLine($"🔹 Connection String: {connectionString}");

        builder.UseNpgsql(
            connectionString,
            b => b.MigrationsAssembly("Ambev.Sales.ORM") // 📌 Aponta as migrations para Ambev.Sales.ORM
        );

        Console.WriteLine("✅ DbContext configurado com sucesso!");

        return new DefaultContext(builder.Options);
    }
}