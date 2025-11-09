using Serilog;
using TicketManagement.Api;


Log.Logger=new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();
Log.Information("GloboTicket API starting");

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, loggerConfiguration) => loggerConfiguration
     .WriteTo.Console()
     .ReadFrom.Configuration(context.Configuration));


var app= builder.ConfigureServices().ConfiqurePipline();
//await app.ResetDatabaseAsync();
// Configure the HTTP request pipeline.

app.UseSerilogRequestLogging();
app.Run();
