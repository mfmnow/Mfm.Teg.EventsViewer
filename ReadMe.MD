# Mfm.Teg.EventsViewer project -- Backend

## 1. Tech Stack
- .NET Core 8
- Moq (Unit testing)
- Fluent FluentAssertions (Unit testing)
- Swagger (API Docos)
- Visual Studio 2022

## 2. List of APIs
- api/v1.0/event-viewer/all-venues -> Gets List of all Venues.
- api/v1.0/event-viewer/events/{venueId} -> Gets List of Events by VenueId

## 3. Addressing protecting the API and S3 API
### 3.1 Rate Limiting
- Using built-in .NET Core 8 RateLimiter Middleware
- WindowInSeconds and PermitLimit are configurable per env
### 3.2 Caching
- The risky call to retrieve data from S3 is cached against keys.
- This should improve stability.
- I implemented a super simple ConcurrentDictionary caching logic.. maybe in Prod, Redis would be a better idea with load balancers
### 3.3 Future improvements
- Maybe queue requests and control how much calls we execute against third party APIs

## 4. Docos
### 4.1 Swaggar
- Used Swagger against Dev only for security purposes.
- Swagger integrates with assemblies and XML docs to give max level of documentation.
### 4.2 Code comments
- XML code comments is enforced in some projects

## 5. DI, HTTPClient and Configs
### 5.1 DI
- Used MS DI which is built-in .NET Core
- Used Singletons and Transits as needed
### 5.2 HTTPClient
- Used .NET Core built-in HTTPClient registration and assigning to services
### 5.3 Configs
- All Configs are Singletons and loaded on App start once.

## 6. Error handling
- Create an Error handling Middleware (ErrorHandlerMiddleware)

## 7. Logging
- Used Microsoft extension that could be hooked to any logging service like AppInsights or Serilog (Not configured).

## 8 Furure work
- Using validation services
- Using mappers
- Applying External APIs queuing logic
- Write integration tests
- Finish unit tests

## 9. Run / Test
- Open the solution using VS.NET 2022
- Base URL should be: https://localhost:7151
- Review Swagger docos
- Import Postman collection (Mfm.Teg.EventsViewer.postman_collection.json) included in solution.
- Run endpoints using Postman
- Dev Rate limit is 3 requests per 10 seconds.

# Mfm.Teg.EventsViewer project -- Frontend -- Work in progress
## 1. Tech Stack
- React, Node (Latest)
- Bootstrap for React
- Date functions liberay

