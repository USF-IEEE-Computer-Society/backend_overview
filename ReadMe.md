
---
# Workshop Basics: Backend Overview

## Project Overview
This project demonstrates fundamental backend concepts using ASP.NET Web API, including:

- Basic endpoint routing with `ApiController`
- Simulating a database using a `DummyDB` and corresponding dummy controllers
- Registering dependencies and using Inversion of Control (IoC)
- Understanding different service lifecycles
- Integrating a real database with Entity Framework (EF) and an ORM
- Implementing JWT-based authentication
- Securing endpoints with role-based token policies

> **Note**: Performance and scalability were not primary considerations for this demo. The focus is on showing core concepts. Feel free to improve features like error handling, adding more endpoints, etc.

---

## Project Structure

### 1. DummyDatabase
**Files:**
- An entity representing a user in the dummy database
- A simple implementation of the dummy database (list manipulation)

### 2. Database
**Folder:** `\Database\DBContainer`
- `populate.sql`: Script to populate data in the database
- `Dockerfile`: Configuration for a PostgreSQL image

**Folder:** `\Database\Entities`
- `Admin` -> Represents `Admins` table
- `Post` -> Represents `Posts` table
- `Users` -> Represents `Users` table

**AppDbContext**
- Maps classes to tables and configures relationships

### 3. Controllers
1. **DummyControllers**  
   Demonstrates basic routing, introduces API controllers, and shows dependency injection.
2. **ImprovedDummyControllers**  
   Uses an actual database to store and retrieve data.
3. **Controller**  
   Combines database usage with authorization features.

### 4. Models
Contains shared classes (e.g., `Credentials`) used to pass data within the project.
> Example: Instead of exposing the entire `User` object for login, only pass `nickname` and `password`.

### 5. Service
Hosts common classes used by controllers. These services are registered for dependency injection.

---
## Running the ASP.NET Web API Project on .NET 8

## 1. Install the .NET 8 SDK

1. **Download and Install**
   - Visit [Microsoft’s .NET downloads page](https://dotnet.microsoft.com/en-us/download).
   - Find and download the **.NET 8** installer for your operating system (Windows, macOS, or Linux).
   - Follow the on-screen instructions to complete the installation.

2. **Verify Installation**
   - Open your terminal or command prompt.
   - Run:
     ```bash
     dotnet --version
     ```
   - You should see a version number starting with `8` (e.g., `8.0.100`).


## 2. Run the ASP.NET Web API Project

1. **Open Your Project Folder**
   - Navigate to the directory containing your `.csproj` file (./Workshop_Basics).

2. **Build and Restore Dependencies**
   - When you build the project, dependencies are **automatically installed** (restored) if they are missing.
   - Run:
     ```bash
     dotnet build
     ```

3. **Run the Application**
   - Execute:
     ```bash
     dotnet run
     ```
   - The application will start and listen on the ports specified in `launchSettings.json` (e.g., `https://localhost:7101`).

You are now set to develop and run your ASP.NET Web API project on **.NET 8**.


---
## Setting Up the Database with Docker

1. **Install Docker**  
   Make sure Docker is correctly installed and running.

2. **Build the Docker Image**  
   Navigate to `DBContainer` and build from the provided `Dockerfile`:
   ``` 
   cd /Database/DBContainer
   ```

   ```bash
   docker build -t postgres-db:workshop .
   ```
3. **Create a Docker Volume**
   ```bash
   docker volume create database_data
   ```
4. **Create a Docker Volume**
   ```bash 
   docker run -d \
     --name postgres-container \
     -p 5432:5432 \
     -v database_data:/var/lib/mysql \
     postgres-db:workshop
   ```

Starting and stopping container
**Starting the container**
   ```bash
    docker start postgres-container
   ```
**Stopping the container**
```bash
docker stop postgres-container
```
---
## Setting Up Initial migration

**Run first migration**
```bash
dotnet ef migrations add InitialCreate
```
***Make sure that database is on***
```bash
dotnet ef database update
```
---
## Populate Data

```bash 
psql -h localhost -U akmalchik -d workshopdb -f populate.sql
```
You will be asked to enter password "ieeecs@usf|is|amazing"


---
## Suggestions & Next Steps
0. **Practice Coding Challenges**
   - Use platforms like LeetCode or HackerRank to hone problem-solving skills.
   - Combine algorithmic thinking with practical development experience.


1. **Master Programming Fundamentals**
   - Focus on core concepts such as variables, data types, control flow, and object-oriented programming.
   - Build small console applications to strengthen your foundation.


2. **Use Git/GitHub Early**
   - Familiarize yourself with version control for managing and collaborating on code.
   - Create your own GitHub repository to store and showcase your projects.


3. **Understand HTTP & Basic Web Development**
   - Learn how web requests and responses function (methods, headers, status codes).
   - Explore how RESTful APIs are structured and consumed.


4. **Explore Databases & ORMs**
   - Study both SQL and NoSQL databases, focusing on CRUD operations.
   - Practice using an ORM (or direct database queries) to interact with data.


5. **Implement Authentication & Authorization**
   - Understand foundational security concepts.
   - Experiment with different methods (JWT, OAuth, etc.) to protect your applications.


6. **Try Real-Time Features (WebSockets)**
   - Investigate real-time communication for chat apps, notifications, or live dashboards.
   - Use libraries or frameworks that simplify WebSocket integration.


7. **Add Caching**
   - Improve performance by using in-memory or distributed caching (e.g., Redis).
   - Learn caching strategies to handle high-traffic scenarios.


8. **Write Tests**
   - Incorporate unit tests and integration tests into your workflow.
   - Adopt test-driven development (TDD) practices to maintain code quality.


9. **Host & Set Up CI/CD**
   - Deploy your applications to a hosting service or cloud platform.
   - Automate builds, tests, and deployments to streamline development.


10. **Experiment with Architectures**
- Compare monolithic vs. microservices designs.
- Understand when and why you’d choose one pattern over another.


11. **Learn Advanced Topics**
- Delve into areas like cloud computing, message brokers, containerization (Docker), and orchestration (Kubernetes).
- Expand your knowledge of distributed systems, scalability, and resilience.

> **Remember**: Mastery takes time—there’s no need to rush. Consistent practice is key: the more you apply what you learn, the more proficient you become. Enjoy the journey of exploring, experimenting, and improving your projects!


