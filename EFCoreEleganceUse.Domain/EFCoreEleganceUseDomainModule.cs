using Autofac;
using EFCoreEleganceUse.Domain.Entities;

namespace EFCoreEleganceUse.Domain
{
    public class EFCoreEleganceUseDomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EFEntityInfo>().SingleInstance();
        }
    }
}
