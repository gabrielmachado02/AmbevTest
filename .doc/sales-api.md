```

**Response (JSON):**
```json
{
  "success": true,
  "message": "Sale cancelled successfully",
  "data": {
    "saleId": "string",
    "status": "Cancelled"
  }
}
```

---

#### 4. Cancel Sale Item
**Endpoint:** PUT /api/sales/cancel-item

**Description:** Cancels specific items from a sale.

**Request (JSON):**
```json
{
  "saleId": "string",
  "saleItemIds": ["string"]
}
```

**Response (JSON):**
```json
{
  "success": true,
  "message": "Sales item cancelled successfully",
  "data": {
    "saleId": "string",
    "status": "Partially Cancelled"
  }
}
```

