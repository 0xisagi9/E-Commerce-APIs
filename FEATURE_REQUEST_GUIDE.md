# JSON Feature Request/Response Guide

## Problem Fixed ?

The issue was that the `Feature` property was incorrectly defined as `JsonDocument?` instead of `string?` in:
- `CreateProductCommand`
- `ProductDTO`

**The application now correctly receives and sends JSON features as STRINGS.**

---

## How to Send Feature JSON

### ? CORRECT - Feature as JSON String

**HTTP Request:**
```http
POST /api/product HTTP/1.1
Content-Type: application/json

{
  "name": "Gaming Laptop",
  "description": "High-performance gaming laptop",
  "brandId": 1,
  "smallImage": "https://example.com/laptop.jpg",
  "feature": "{\"processor\":\"Intel i9-13900K\",\"ram\":\"32GB DDR5\",\"storage\":\"1TB NVMe\",\"display\":\"4K 144Hz\",\"gpu\":\"RTX 4090\"}",
  "categoryIds": [1, 2, 5]
}
```

### Breaking Down the Feature String

```
"feature": "{\"processor\":\"Intel i9-13900K\",\"ram\":\"32GB DDR5\"...}"
           ?                ?                    ?
           Start of string  Escaped quotes      More escaped quotes
```

### Important: Proper Escaping

In JSON requests, when you have a JSON string as a value, you must escape the quotes:

**Original JSON structure:**
```json
{"processor":"Intel i9-13900K","ram":"32GB DDR5"}
```

**Escaped for HTTP request:**
```json
"{\"processor\":\"Intel i9-13900K\",\"ram\":\"32GB DDR5\"}"
```

---

## Using Different Tools

### cURL Command
```bash
curl -X POST "https://localhost:7000/api/product" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Gaming Laptop",
    "feature": "{\"processor\":\"Intel i9\",\"ram\":\"32GB\"}",
    "brandId": 1,
    "categoryIds": [1, 2]
  }'
```

### Postman
1. **Method:** POST
2. **URL:** `https://localhost:7000/api/product`
3. **Body Type:** JSON (application/json)
4. **Body:**
```json
{
  "name": "Gaming Laptop",
  "feature": "{\"processor\":\"Intel i9\",\"ram\":\"32GB\"}",
  "brandId": 1,
  "categoryIds": [1, 2]
}
```

### C# HttpClient
```csharp
var json = new
{
    name = "Gaming Laptop",
    feature = "{\"processor\":\"Intel i9\",\"ram\":\"32GB\"}",
    brandId = 1,
    categoryIds = new[] { 1, 2 }
};

var content = new StringContent(
    JsonSerializer.Serialize(json),
    Encoding.UTF8,
    "application/json"
);

var response = await httpClient.PostAsync("https://localhost:7000/api/product", content);
```

### JavaScript/TypeScript
```javascript
const featureData = {
  processor: "Intel i9",
  ram: "32GB",
  gpu: "RTX 4090"
};

const payload = {
  name: "Gaming Laptop",
  feature: JSON.stringify(featureData),  // Convert to string
  brandId: 1,
  categoryIds: [1, 2]
};

const response = await fetch('https://localhost:7000/api/product', {
  method: 'POST',
  headers: { 'Content-Type': 'application/json' },
  body: JSON.stringify(payload)
});
```

---

## Expected Response

### Success Response (201 Created)
```json
{
  "isSuccess": true,
  "data": {
    "id": 1,
    "name": "Gaming Laptop",
    "description": "High-performance gaming laptop",
    "brand": "ASUS",
    "smallImage": "https://example.com/laptop.jpg",
    "features": "{\"processor\":\"Intel i9-13900K\",\"ram\":\"32GB DDR5\",\"storage\":\"1TB NVMe\",\"display\":\"4K 144Hz\",\"gpu\":\"RTX 4090\"}",
    "categories": ["Electronics", "Computers", "Gaming"]
  },
  "message": "Product created successfully",
  "statusCode": 201
}
```

### Error Response - Invalid JSON (400 Bad Request)
```json
{
  "isSuccess": false,
  "data": null,
  "message": "Validation failed",
  "statusCode": 400,
  "errors": {
    "Feature": [
      "Feature must be valid JSON format. Error: '\"' is an invalid start of a value at line 1, position 2."
    ]
  }
}
```

---

## Common Mistakes ?

### Mistake 1: Sending Feature as Object
```json
{
  "feature": {
    "processor": "Intel i9",
    "ram": "32GB"
  }
}
```
**Error:** Application expects string, not object
**Fix:** Convert to string: `"feature": "{\"processor\":\"Intel i9\",\"ram\":\"32GB\"}"`

### Mistake 2: Missing Escaped Quotes
```json
{
  "feature": "{"processor":"Intel i9"}"
}
```
**Error:** Invalid JSON - unescaped quotes
**Fix:** Escape quotes: `"feature": "{\"processor\":\"Intel i9\"}"`

### Mistake 3: Empty Feature Value
```json
{
  "feature": ""
}
```
**Result:** Accepted (feature is optional) - Empty features allowed

### Mistake 4: Invalid JSON in Feature
```json
{
  "feature": "{processor: 'Intel i9'}"
}
```
**Error:** Invalid JSON format (missing quotes on key and value)
**Fix:** `"{\"processor\":\"Intel i9\"}"`

---

## Valid Feature Examples

### Laptop Features
```json
{
  "feature": "{\"processor\":\"Intel i9-13900K\",\"ram\":\"32GB DDR5\",\"storage\":\"1TB NVMe\",\"display\":\"4K 144Hz\",\"gpu\":\"RTX 4090\",\"battery\":\"100Wh\",\"warranty\":\"24 months\"}"
}
```

### Clothing Features
```json
{
  "feature": "{\"material\":\"100% Cotton\",\"sizes\":[\"S\",\"M\",\"L\",\"XL\"],\"colors\":[\"Black\",\"White\",\"Navy\"],\"fit\":\"Regular\"}"
}
```

### Appliance Features
```json
{
  "feature": "{\"capacity\":\"450L\",\"warranty\":\"3 years\",\"energyRating\":\"5-star\",\"features\":[\"IceMaker\",\"Dispenser\"]}"
}
```

---

## Flow Diagram

```
CLIENT REQUEST
    ?
"feature": "{\"processor\":\"Intel i9\"}"  (JSON String)
    ?
DESERIALIZATION
    ?
CreateProductCommand.Feature = string value
    ?
VALIDATION (JsonDocument.Parse)
    ?
? Valid JSON?
    ?
CREATE PRODUCT
    ?
Product.Feature = JSON string (stored in JSONB column)
    ?
DATABASE STORAGE (PostgreSQL)
    ?
RETRIEVAL
    ?
ProductDTO.Features = JSON string
    ?
API RESPONSE
    ?
"features": "{\"processor\":\"Intel i9\"}"  (JSON String)
    ?
CLIENT RECEIVES
    ?
Client can parse with JSON.parse() if needed
```

---

## Key Points

? **Feature is sent as STRING** - Not as JSON object  
? **Feature must be valid JSON** - JsonDocument.Parse validates it  
? **Quotes must be escaped** - Use `\"` for quotes inside JSON string  
? **Feature is optional** - Can send empty string or null  
? **Flexible schema** - Any valid JSON structure allowed  
? **Response includes feature as string** - Same format as request  

---

## What Changed in the Code

| Component | Change |
|-----------|--------|
| **CreateProductCommand** | `Feature: string?` (was `JsonDocument?`) |
| **ProductDTO** | `Features: string?` (was `JsonDocument?`) |
| **Validator** | Validates `Feature` as JSON string |
| **Handler** | Stores `request.Feature` directly as string |
| **Mapping** | Maps `Feature` ? `Features` correctly |

All fixes are now complete and the build is successful! ?
