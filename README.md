# Wallet System for Sports Betting Platform 🏦

This service was developed as part of a code challenge to manage user funds in a sports betting platform. Each user has one or more *wallets* to securely hold their funds, allowing them to deposit, withdraw, and check their balance as they participate in the platform.

### Key Features and Requirements

- **Create a Wallet**: Each user can have a wallet created to hold their funds.
- **Deposit and Withdraw Funds**: Users can add or remove funds from their wallet based on events like betting, winning, or receiving bonuses.
- **Query Wallet State**: Users and administrators can retrieve the current balance and wallet state.
- **Atomic Operations**: Transactions are handled carefully to prevent double-spending and ensure balance consistency.
- **Negative Balances Prohibited**: Wallets cannot have a negative balance to prevent overspending.

### API Design Overview

This service provides RESTful APIs to support wallet management operations:

1. **Creating Wallets**: Initialize a new wallet for a user.
2. **Managing Funds**: APIs to deposit into or withdraw from the wallet, with checks to prevent negative balances.
3. **Batch Processing**: For operations involving multiple users, batch transactions are supported, allowing deposits or withdrawals for several users in a single request.

 [View OpenAPI Sample for Wallet Creation and Funds Management](./openapi/openapi.wallets.yaml)

---

- [Getting Started 🏃](#getting-started-)
  - [Run the service using Docker](#run-the-service-using-docker-or-the-net-cli)
  - [Create a wallet](#create-a-wallet)
  - [Get wallet state](#get-wallet-state)
  - [Deposit funds](#deposit-funds)
  - [Withdraw funds](#withdraw-funds)
  - [Batch Movements](#batch-movements)

# Getting Started 🏃

## Run the service using Docker

```shell
docker compose up
```
The service should be available in http://localhost:5001/swagger/index.html

## Create a wallet 

```yaml
POST {{host}}/api/v1/tenants/{tenantId}/wallets
Content-Type: application/json
```

```http
{
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```
## Get wallet state
```yaml
GET {{host}}/api/v1/tenants/{tenantId}/wallets/{walletId}
Content-Type: application/json
```

### Sample Response
```http
{
  "id": "ca8e1358-2b20-44de-ac1d-f3d1b5d223d0",
  "tenantId": 1,
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "token": "AAAAAAAARlQ=",
  "balance": 0
}
```

## Deposit funds

```yaml
POST {{host}}/api/v1/tenants/{tenantId}/wallets/{walletId}
Content-Type: application/json
```
```http
{
  "amount": 0
}
```


## Withdraw funds

```yaml
POST {{host}}/api/v1/tenants/{tenantId}/wallets/{walletId}/withdraws
Content-Type: application/json
```
```http
{
  "token": "string",
  "amount": 0
}
```
## Batch Movements

This would be called by a bets system to deposit or withdraw money for several users.

### Creating a batch
```yaml
POST {{host}}/api/v1/tenants/{tenantId}/batch/wallets
Content-Type: application/json
```
```http
{
  "movements": [
    {
      "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "movementType": "Deposit",
      "amount": 0
    }
  ]
}
```
### Getting batch state
```yaml
GET {{host}}/api/v1/tenants/{tenantId}/batch/wallets/{batchId}
Content-Type: application/json
```
### Response sample
```http
{
  "id": "28592804-ca1f-480c-aef3-5bd53b99ba89",
  "tenantId": 1,
  "status": "NotStarted",
  "movementsRequests": [
    {
      "batchId": "28592804-ca1f-480c-aef3-5bd53b99ba89",
      "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "operationType": "Deposit",
      "amount": 1000,
      "status": "NotStarted",
      "errorDescription": "",
      "id": "bdf4d6e6-44f0-4229-beee-4c39d79586f1"
    }
  ]
}
```
