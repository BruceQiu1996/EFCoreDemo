using Autofac;
using EFCoreEleganceUse.Domain;
using EFCoreEleganceUse.Domain.Repository;
using EFCoreEleganceUse.EF.Mysql.Repository;

namespace EFCoreEleganceUse.EF.Mysql
{
    public class EFCoreEleganceUseEFCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterModule<EFCoreEleganceUseDomainModule>(); //注入domain模块
            builder.RegisterGeneric(typeof(GenericRepository<,>))//将dbcontext注入到仓储的构造中
                   .UsingConstructor(typeof(LibraryDbContext))
                   .AsImplementedInterfaces()
                   .InstancePerDependency();

            builder.RegisterType<WorkUnit>().As<IWorkUnit>().InstancePerDependency();
        }
    }
}
