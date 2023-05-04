using GloboTicket.TicketManagement.Api.Middleware;
using GloboTicket.TicketManagement.Application;
using GloboTicket.TicketManagement.Infrastructure;
using GloboTicket.TicketManagement.Persistence;
using Microsoft.OpenApi.Models;

namespace GloboTicket.TicketManagement.Api;

public static class StartupExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        AddSwagger(builder.Services);
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddPersistenceServices(builder.Configuration);

        builder.Services.AddControllers();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        });

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "GloboTicket TicketManagement Api");
            });
        }


        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();

        app.UseCustomExceptionHandler(); // for exception handling

        app.UseCors("Open"); // for cors
        
        app.UseAuthorization();
        
        app.MapControllers();
        
        return app;
    }

    private static void AddSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "GloboTicket Ticket Management Api"
            });

        });
    }
}