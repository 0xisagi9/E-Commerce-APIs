# Quick Reference - JSON Feature Fix

## ? CORRECT Way to Send Features

```json
POST /api/product
{
  "name": "Gaming Laptop",
  "feature": "{\"processor\":\"Intel i9\",\"ram\":\"32GB\"}",
  "brandId": 1,
  "categoryIds": [1, 2]
}
```

## ? WRONG Ways

```json
// WRONG 1: Feature as object
{
  "feature": {
    "processor": "Intel i9"
  }
}

// WRONG 2: Unescaped quotes
{
  "feature": "{"processor":"Intel i9"}"
}

// WRONG 3: Single quotes
{
  "feature": "{'processor':'Intel i9'}"
}
```

---

## Code Changes Summary

| File | Change | Before | After |
|------|--------|--------|-------|
| CreateProductCommand | Feature type | `JsonDocument?` | `string?` |
| ProductDTO | Features type | `JsonDocument?` | `string?` |
| Validator | JSON validation | ? | ? Validates string as JSON |
| Handler | Feature storage | ? | ? Stores as string |
| Mapping | Feature mapping | ? | ? Maps Feature ? Features |

---

## Expected Response

```json
{
  "isSuccess": true,
  "data": {
    "id": 1,
    "name": "Gaming Laptop",
    "features": "{\"processor\":\"Intel i9\",\"ram\":\"32GB\"}",
    "categories": ["Electronics"]
  },
  "statusCode": 201
}
```

---

## Feature Examples

### Laptop
```
"feature": "{\"processor\":\"Intel i9\",\"ram\":\"32GB\",\"storage\":\"1TB\"}"
```

### Clothing
```
"feature": "{\"material\":\"Cotton\",\"sizes\":[\"S\",\"M\",\"L\"],\"colors\":[\"Black\",\"White\"]}"
```

### Appliance
```
"feature": "{\"capacity\":\"450L\",\"warranty\":\"3 years\",\"energyRating\":\"5-star\"}"
```

---

## Quote Escaping Rule

Inside JSON:
- Use `\"` instead of `"`
- Use `\\` instead of `\`
- Use `\n` for newlines

Example:
```
Original: {"processor":"Intel i9"}
In JSON:  "{\"processor\":\"Intel i9\"}"
```

---

## Build Status

? **All 5 files fixed and building successfully**

---

## Files Modified

1. ? CreateProductCommand.cs
2. ? ProductDTO.cs
3. ? CreateProductCommandValidator.cs
4. ? CreateProductCommandHandler.cs
5. ? MappingProfile.cs

---

## Remember

- Feature is sent as **STRING** (not object)
- Feature is stored as **JSONB** in PostgreSQL
- Feature is returned as **STRING** (not object)
- Quotes must be **escaped** in JSON requests
- Validation ensures **valid JSON** structure
