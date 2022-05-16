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


        builder
            .RegisterType<CustomerRepository>()
            .As<ICustomerRepository>()
            .InstancePerLifetimeScope();

        builder
           .RegisterAssemblyTypes(typeof(CreateCustomerCommandHandler).GetTypeInfo().Assembly)
           .AsClosedTypesOf(typeof(IRequest<>));

    }
}
