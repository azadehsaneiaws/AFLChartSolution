# **DualFanDepthChartSolution**

## **Overview**

The **DualFanDepthChartSolution** allows you to add, remove, and manage players' positions within a team's depth chart.
The system is built with scalability, maintainability, and testability in mind, leveraging several modern software architecture principles and design patterns.
I Implemented this App Using TDD And for Design Pattern As you can see I Levredged Design pattern, There were A few Things I would do Differenly But Would add them In areas of Improvement  at the end of the  file. 
## ** Technologies Used **
Nothing Fancy AS this is Just a POC Implemented in 46 minutes on  a sunny sunday morning :))

.NET : 8.0 (LTS).
xUnit: The testing framework used for unit tests.
C#: The primary programming language used for development.
Git: Version control system for managing the project's source code.


## Domain-Driven Design (DDD)
I used Domain-Driven Design principles, Tried to focus on creating a rich domain model with these Key aspects:

Entities: The Player entity => it represents the #core object# within the system, encapsulating data and behavior.
Aggregates: The DepthChart => it is  an "aggregate root" that ensures consistency and encapsulates operations related to managing players' positions.
Repositories: Repositories provide access to aggregates while abstracting the data layer, allowing for easy substitution of different data sources (e.g., in-memory, SQL, etc.).

## Test-Driven Development (TDD)
The development of this project followed the principles of Test-Driven Development (TDD). Again nothing Fancy, Just Couple of Methos as representation for the concept 
that all functionalities are covered by tests and that the code is reliable and maintainable from the start.

## Design Patterns
1. Repository Pattern
The Repository Pattern is used to abstract the data layer, providing a clean API for the domain layer to interact with data sources. This abstraction enables the application to switch between different data storage mechanisms (e.g., in-memory, SQL, NoSQL) without changing the business logic.

PlayerRepository: Manages access to player data.
DepthChartRepository: Manages the depth chart data and ensures consistency of operations.

## Service Layer Pattern
I tried to encapsulate business logic,  DepthChartService is the central class in this pattern, handling operations like adding and removing players, retrieving backups, and providing the full depth chart.
Why? cause I dont want the application layer  bogged down by business rules. 

## Usage
The DualFanDepthChartSolution is primarily a backend service. It can be extended with a frontend UI or integrated with other systems via APIs. Currently, the service can manage NFL team depth charts by:

1- Adding players to the depth chart
2- Removing players from the depth chart
3- Retrieving backups for a specific player
4- Getting the full depth chart for a team


## How to run this 
clone it: 
git clone https://github.com/azadehsaneiaws/DualFanDepthChartSolution.git
Build it: 
cd DualFanDepthChartSolution
dotnet build
Test it: 
dotnet test
Contact me if you dont have access to repo azadehsanei.aws@gmail.com

# Areas of Improvement: 
what I would add to this project? 
This was just a POC so I decided to take it easy and use InMemory Not to deal with databases However,  The API Should be smart enough to handle different db types, The only thing you need to do is, Just add an apporopriate Implemention for each DB type 
public class MySqlPlayerRepository : IPlayerRepository, public class DynamoDBPlayerRepository : IPlayerRepository, public class SqlPlayerRepository : IPlayerRepository, etc, 
# Example 
using DepthChart.Domain.Entities;
using DepthChart.Domain.Repositories;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DepthChart.Infrastructure.Repositories.SqlServer
{
    public class SqlServerPlayerRepository : IPlayerRepository
    {
        private readonly string _connectionString;

        public SqlServerPlayerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Player GetPlayerById(int number)
        {...
        }

        public void AddPlayer(Player player)
        {...
        }

        public void RemovePlayer(Player player)
        {...
        }

        public IEnumerable<Player> GetAllPlayers()
        {...
        }
    }
}
And then Using a dependency injection (DI) container, you can dynamically choose the appropriate repository implementation based on configuration.
something like this 
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        var dbType = Configuration["Database:Type"];
        var connectionString = Configuration.GetConnectionString("DefaultConnection");

        if (dbType == "SqlServer")
        {
            services.AddScoped<IPlayerRepository, SqlServerPlayerRepository>(_ => new SqlServerPlayerRepository(connectionString));
            services.AddScoped<IDepthChartRepository, SqlServerDepthChartRepository>(_ => new SqlServerDepthChartRepository(connectionString));
        }
        else if (dbType == "MySql")
        {
            services.AddScoped<IPlayerRepository, MySqlPlayerRepository>(_ => new MySqlPlayerRepository(connectionString));
            services.AddScoped<IDepthChartRepository, MySqlDepthChartRepository>(_ => new MySqlDepthChartRepository(connectionString));
        }

## Note: I left this Repo open to public, Appreciate If you have a plan to Change the code Create a PR 
Regards
Roxana Sanei
