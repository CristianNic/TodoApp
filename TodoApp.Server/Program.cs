using TodoApp.Helpers;
using TodoApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Configure MongoDB settings
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDB"));

// Register MongoDB context
builder.Services.AddSingleton<MongoDbContext>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS policy -- Allow All
builder.Services.AddCors(options =>
{
   options.AddPolicy("AllowAll", policy =>
   {
       policy.AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader();
   });
});

var app = builder.Build();



app.UseCors("AllowAll");

// Seed data
await DataSeeder.SeedDataAsync(app.Services);

// app.UseDefaultFiles();
// app.UseStaticFiles();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// Enable Swagger in all environments, including production, for demo
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoApp API V1");
});

// Commented out for Docker
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// app.MapFallbackToFile("/index.html");

app.Run();
