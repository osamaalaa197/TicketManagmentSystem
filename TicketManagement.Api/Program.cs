using TicketManagement.Api;

var builder = WebApplication.CreateBuilder(args);

var app= builder.ConfigureServices().ConfiqurePipline();
//await app.ResetDatabaseAsync();
// Configure the HTTP request pipeline.


app.Run();
