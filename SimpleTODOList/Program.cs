using Microsoft.EntityFrameworkCore;
using SimpleTODOList.Data;
using SimpleTODOList.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<ITodoRepository, TodoRepository>();

builder.Services.AddDbContext<TodoContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("tododb")
        ?? throw new ArgumentException("the database connection string must not be empty"));
    options.EnableSensitiveDataLogging();
    options.EnableDetailedErrors();
});

//var dbContext = builder.Services?.BuildServiceProvider().GetService<TodoContext>();
//dbContext.Database.EnsureCreated();
//dbContext.Database.Migrate();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var dbContext = app.Services.GetRequiredService<TodoContext>();
dbContext.Database.EnsureCreated();
dbContext.Database.Migrate();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
