# Parking App - ASP.NET Core Application

Simple Parking Management application built with ASP.NET Core 8.0 (.NET 8).  

## Quick Start
1. Clone or download the repository from GitHub - ```git clone https://github.com/Mateusz19021996/ParkingManagement```
2. Open the solution in Visual Studio or VS Code.
3. Ensure you have .NET 8 SDK installed.
4. Run application by IIS express if You are using Visual Studio or use the command line:

   ```bash
	dotnet restore
	dotnet build
	dotnet run --urls https://localhost:44349
   ```

## Technical Assumptions
1. The application uses an InMemoryDatabase
   This makes it easier to run and test without additional dependencies
   Note: This is for simplification only — InMemoryDatabase has limitations and should be replaced in a real project
2. There is no Authorization or Authentication implemented, for demo purposes
3. The project uses .NET 8
   This is a relatively new version, chosen for improved security and to avoid potential issues with brand-new features in the latest releases
4. This app is a simple demo
   It follows a clean architecture with basic DTOs. Many parts of the application could be improved in a real project
5. Tests included are only unit tests
   In a real project, integration tests, end-to-end tests, and more comprehensive coverage would be necessary


## Assumptions
1. The parking has unlimited spaces
2. Parking place name is not used as an ID, but it can be unique
3. The system allows you to:
   - Allocate a car
   - Check parking availability
   - Deallocate a car
4. Exit time: It is assumed that the exit time is recorded as the moment the request reaches the API
   This ensures that the user is not charged extra if the operation takes longer than one minute for any reasonn

## What Can Be Improved
1. Add enums to the database: Avoid code changes when adding new types
2. Store prices in the database: No need for code updates/releases when changing rates
3. Improve exception handling: Distinguish multiple exceptions and add custom ones
4. Enhance logging: More comprehensive and structured logging
5. Add more tests: Expand unit and integration test coverage
6. Implement Unit of Work: Better repository pattern with transaction management

Many other improvements possible - I will be happy to discuss! :)

## Open Questions
1. **Duplicate parking**: How should we handle someone trying to park a car already on the lot?
2. **Short stays**: What if someone enters and exits in under 1 minute?
3. **Environments**: What environments do we use (dev/test/staging/prod)? Need different `appsettings.*.json` configs.
4. How many places parkign should have?