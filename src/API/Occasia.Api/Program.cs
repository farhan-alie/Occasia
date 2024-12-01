using Occasia.Common.Application;
using Occasia.Common.Infastructure;
using Occasia.Modules.Events.Application;
using Occasia.Modules.Events.Presentation;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(static (context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddApplication([AssemblyReference.Assembly]);

builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("Database")!);

builder.Services.AddEventsModule(builder.Configuration);

WebApplication app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapEventModuleEndpoints();

await app.RunAsync();
