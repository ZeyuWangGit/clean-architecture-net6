using GloboTicket.TicketManagement.Api;
using Serilog;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("GloboTicket Api Starting");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfiguration) => loggerConfiguration
    .WriteTo.Console()
    .ReadFrom.Configuration(context.Configuration));

var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

app.UseSerilogRequestLogging();

app.Run();
