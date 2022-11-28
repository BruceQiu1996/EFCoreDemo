using System.Reflection;

namespace EFCoreEleganceUse.Domain.Entities
{
    public class EFEntityInfo
    {
        public (Assembly Assembly, IEnumerable<Type> Types) EFEntitiesInfo => (GetType().Assembly, GetEntityTypes(GetType().Assembly));
        private IEnumerable<Type> GetEntityTypes(Assembly assembly)
        {
            var efEntities = assembly.GetTypes().Where(m => m.FullName != null
                                                            && Array.Exists(m.GetInterfaces(), t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEFEntity<>))
                                                            && !m.IsAbstract && !m.IsInterface).ToArray();

            return efEntities;
        }
    }
}
