using Autofac;
using Customers.API.Application.Commands;
using Customers.Infrastructure.Repositories;
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

     
        builder.RegisterType<CustomerRepository>()
            .As<ICustomerRepository>()
            .InstancePerLifetimeScope();

        //builder.RegisterType<RequestManager>()
        //    .As<IRequestManager>()
        //    .InstancePerLifetimeScope();

        //builder.RegisterAssemblyTypes(typeof(CreateCustomerCommand).GetTypeInfo().Assembly)
        //    .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));

    }
}
