namespace Prabu.Library
{
    using Entities;
    using NHibernate.Cfg;
    using NHibernate.Mapping.ByCode;

    [System.Diagnostics.DebuggerNonUserCode]
    public static class MappingExtensions
    {
        public static void WithMappings(this ModelMapper mapper, Configuration configuration)
        {
            mapper.AddMappings(typeof(Account).Assembly.GetTypes());
            configuration.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());
        }
    }
}