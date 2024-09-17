using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace TodoApp.Helpers
{
    public static class DataSeeder
    {
        public static async Task SeedDataAsync(IServiceProvider services)
        {
            try
            {
                using var scope = services.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<MongoDbContext>();

                var todosCollection = context.Todos;

                if (await todosCollection.CountDocumentsAsync(Builders<ToDoItem>.Filter.Empty) > 0)
                {
                    return; // Data already exists
                }

                var initialTodos = new List<ToDoItem>
                {
                    new ToDoItem
                    {
                        Title = "Buy groceries",
                        Text = "Milk, Eggs, Bread",
                        Completed = false,
                        Status = "Pending",
                        Deadline = DateTime.UtcNow.AddDays(3)
                    },
                    new ToDoItem
                    {
                        Title = "Walk the dog",
                        Text = "Take Max to the park",
                        Completed = true,
                        Status = "Completed",
                        Deadline = DateTime.UtcNow.AddDays(-1)
                    },
                    new ToDoItem
                    {
                        Title = "Finish project",
                        Text = "Complete the final report",
                        Completed = false,
                        Status = "In Progress",
                        Deadline = DateTime.UtcNow.AddDays(7)
                    }
                };

                // Field names are in camelCase
                var bsonInitialTodos = initialTodos.Select(todo => new BsonDocument
                {
                    { "title", todo.Title },
                    { "text", todo.Text },
                    { "completed", todo.Completed },
                    { "status", todo.Status },
                    { "deadline", todo.Deadline }
                });

                await todosCollection.InsertManyAsync(initialTodos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during data seeding: {ex.Message}");
                throw; // Rethrow the exception to ensure it's not silently ignored
            }
        }
    }
}
