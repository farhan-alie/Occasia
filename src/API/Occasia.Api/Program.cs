using Occasia.Common.Infastructure;
using Occasia.Modules.Events.Presentation;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("Database")!);

builder.Services.AddEventsModule(builder.Configuration);

WebApplication app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapEventModuleEndpoints();

await app.RunAsync();
