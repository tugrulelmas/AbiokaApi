using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using NHibernate.Type;

namespace AbiokaApi.Repository
{
    public class UtcConvention : IPropertyConvention
    {
        public void Apply(IPropertyInstance instance) {
            if (instance.Type.Name == "Date") {
                instance.CustomType<UtcDateTimeType>();
            }
        }
    }
}
