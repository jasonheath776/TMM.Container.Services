using Autofac;
using Customers.API.Application.Commands;
using Customers.API.Application.Validations;
using Customers.Infrastructure;
using Customers.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Customers.API.Infrastructure.AutofacModules;

public class ApplicationModule
    : Autofac.Module
{

    public string QueriesConnectionString { get; }

    public ApplicationModule(string qconstr)
    {
        QueriesConnectionString = qconstr;

    }

    protected override void Load(ContainerBuilder builder)
    {

        //builder.Register(c => new OrderQueries(QueriesConnectionString))
        //    .As<IOrderQueries>()
        //    .InstancePerLifetimeScope();
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        var dbPath = Path.Join(path, "customer.db");

        var optionsBuilder = new DbContextOptionsBuilder<CustomerContext>()
            .UseSqlite(dbPath);
        builder
     .RegisterType<CustomerContext>()
     .WithParameter("options", optionsBuilder.Options)
     .InstancePerLifetimeScope();

        //builder.AddDbContext<OrderingContext>(options =>
        //{
        //    options.UseSqlServer(configuration["ConnectionString"],
        //        sqlServerOptionsAction: sqlOptions =>
        //        {
        //            sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
        //            sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
        //        });
        //},
        //           ServiceLifetime.Scoped  //Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
        //       );

        builder.RegisterType<CustomerRepository>()
            .As<ICustomerRepository>()
            .InstancePerLifetimeScope();

        builder
          .RegisterAssemblyTypes(typeof(CreateCustomerCommandValidator).GetTypeInfo().Assembly)
          .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
          .AsImplementedInterfaces();

        builder.RegisterAssemblyTypes(typeof(CreateCustomerCommandHandler).GetTypeInfo().Assembly)
            .AsClosedTypesOf(typeof(IRequest<>));

    }
}
