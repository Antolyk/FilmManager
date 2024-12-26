# Film Management System

## Overview

This project is a CRUD application for managing films. It allows users to create, update, delete, and view films with filtering and sorting functionality. The application is built with .NET 8 and Entity Framework Core.

## Features

CRUD operations: Create, Read, Update, and Delete films.

Filtering and Sorting: Filter films by genre and sort by attributes such as release year or rating.

Documentation: Swagger/OpenAPI documentation for API endpoints.

## Requirements
1. Language & Framework: .NET 8, Entity Framework Core
2. Database: SQL Server Express
3. Version Control: Git

## Project Setup

1. Clone the Repository
```bash
git clone <Repository_URL>
cd <Project_Name>
```
3. Install Dependencies
```bash
dotnet restore
```
5. Configure Settings:

Update the appsettings.json file in the root directory of the project with your connection string:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Your_Connection_String"
  }
}
```
Update the FilmDbContext.cs in the FilmManager.Data with your connection string:
```bash
optionsBuilder.UseSqlServer("Your_Connection_String");
```
4. Apply Database Migrations
```bash
dotnet ef database update
```
6. Run the Project
```bash
dotnet run
```
## Architecture

The project follows a 3-tier architecture:
- Controllers: Handles HTTP requests and responses.
- Service Layer: Implements business logic.
- Data Layer: Manages data access via Entity Framework Core.

## API Endpoints

### Film Management

#### Get All Films
- **GET** `/api/films`
- **Query Parameters:** `genre (optional):` Filter by genre.
`sortBy (optional):` Sort by attribute (e.g., Release Year, Rating).
`ascending (optional):` true for ascending order, false for descending (default: true).
- **Response:** `List of films`.

#### Get Film by ID

- **GET** `/api/films/{id}`
- **Path Parameter:** `id:` Film ID.
- **Response:** `Film details`.

#### Add a New Film

- **POST** `/api/films`
- **Request Body:** `FilmDto` (Title, Genre, Director, ReleaseYear, Rating, Description).
- **Response:** `Created film`.

#### Update Film by ID
- **PUT** `/api/films/{id}`
- **Path Parameter:** `id:` Film ID.
- **Request Body:** `FilmDto` (Title, Genre, Director, ReleaseYear, Rating, Description).
- **Response:** `Updated film`.

#### Delete Film by ID
- **DELETE** `/api/films/{id}`
- **Path Parameter:** `id:` Film ID.
- **Response:** `Success or error message`.

## Best Practices
- Dependency Injection: Services and repositories are injected where needed.
- Database Connection: Ensure the connection string is properly configured.
- Swagger Documentation: Automatically generated API documentation for ease of use.

## Additional Information
Unit Testing: Includes tests for key components (optional).
Uncomment code in FilmDbContext.cs when you need to check unit tests.
