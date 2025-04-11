# Sales Records API

An API for complete management (CRUD) of sales records. The API is designed to store and provide sales data, including the following:

## Features

### Sales Data
- Sale number
- Sale date
- Customer information
- Total sale amount
- Branch where the sale occurred
- Products
- Quantities
- Unit prices
- Discounts
- Total amount per item
- Cancellation status (Cancelled or Not Cancelled)

### Event Publishing (Differential)
- `SaleCreated` - Event triggered when a sale is created.
- `SaleModified` - Event triggered when a sale is modified.
- `SaleCancelled` - Event triggered when a sale is cancelled.
- `ItemCancelled` - Event triggered when an item is cancelled.

---

## Business Rules

1. **Quantity-based Discounts:**
   - 4+ items: 10% discount
   - 10–20 items: 20% discount

2. **Restrictions:**
   - Maximum of 20 items per product
   - No discounts for purchases with less than 4 items

---

## Technologies Used
- **.NET Core 8.0**: Framework for building the API.
- **C#**: Primary programming language.
- **Database**: PostgreSQL.
- **Swagger**: For interactive API documentation.

---

## How to Run the Project

1. Clone this repository:
   ```bash
   git clone https://github.com/victor4hs/AmbevChallenge
   cd AmbevChallenge
   ```

2. Run with Docker Composee
	```
	docker-compose -p AmbevChallenge up -d
	```

3. For Kubernetes deployment
	```
	kubectl apply -f ./docker/kubernetes/
	```

## Local Development

Navigate to the project folder:
   ```bash
   cd src/Ambev.DeveloperEvaluation.WebApi
   ```
   
Restore dependencies:
   ```bash
   dotnet restore
   ```

Run the application:
   ```bash
   dotnet run
   ```

Access the API documentation at:
   ```
   http://localhost:<port>/swagger
   ```
	
---

## Contributing

1. Fork the repository
2. Create a branch for your feature (`git checkout -b feature/new-feature`)
3. Commit your changes (`git commit -m 'Add new feature'`)
4. Push to the branch (`git push origin feature/new-feature`)
5. Open a Pull Request

---

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.