
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Customers.API.Infrastructure.AutofacModules;
using MediatR.Extensions.Autofac.DependencyInjection;

var configuration = GetConfiguration();

var builder = WebApplication.CreateBuilder(args);

// Switch to using Autofac dependency injection
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Add services to the container.
builder.Services.AddEndpointsApiExplorer()
                .AddCustomMvc()
                .AddSwaggerGen()
                .AddApplicationInsights(configuration);

builder.Host.ConfigureContainer<ContainerBuilder>(b => b.RegisterMediatR(typeof(Program).Assembly));
builder.Host.ConfigureContainer<ContainerBuilder>(b => b.RegisterModule(new MediatorModule()));
builder.Host.ConfigureContainer<ContainerBuilder>(b => b.RegisterModule(new ApplicationModule(configuration["ConnectionString"])));

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();

static IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

    return builder.Build();
}

static class CustomExtensionMethods
{
    public static IServiceCollection AddApplicationInsights(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationInsightsTelemetry(configuration);
        services.AddApplicationInsightsKubernetesEnricher();

        return services;
    }

    public static IServiceCollection AddCustomMvc(this IServiceCollection services)
    {
        // Add framework services.
        services.AddControllers(options =>
        {
            // options.Filters.Add(typeof(HttpGlobalExceptionFilter));
        }).AddApplicationPart(typeof(CustomerController).Assembly)
          .AddJsonOptions(options =>
           {
               options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
               options.JsonSerializerOptions.WriteIndented = true;
           });


        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder
                .SetIsOriginAllowed((host) => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
        });

        return services;
    }
}

