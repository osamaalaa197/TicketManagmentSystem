using Serilog;
using TicketManagement.Api;


Log.Logger=new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();
var builder = WebApplication.CreateBuilder(args);

var app= builder.ConfigureServices().ConfiqurePipline();
//await app.ResetDatabaseAsync();
// Configure the HTTP request pipeline.
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));
app.UseSerilogRequestLogging();
app.Run();
