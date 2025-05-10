var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// 1. Dodaj kontrolery
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// 2. Mapowanie końcówek kontrolerów
app.MapControllers();

app.Run();

/*
 Folder models z klasami. 
 Oddzielnie plik Database ze statyczną listą.
*/