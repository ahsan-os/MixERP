using System.Collections.Generic;
using System.Reflection;
using System.Web.Http.Dispatcher;
using Frapid.WebApi;
using Serilog;

namespace Frapid.Web.Application
{
    public class DefaultAssemblyResolver : DefaultAssembliesResolver
    {
        public override ICollection<Assembly> GetAssemblies()
        {
            var baseAssemblies = base.GetAssemblies();
            var assemblies = new List<Assembly>(baseAssemblies);

            try
            {
                var items = FrapidApiController.GetMembers();

                foreach (var item in items)
                {
                    baseAssemblies.Add(item);
                }
            }
            catch (ReflectionTypeLoadException ex)
            {
                foreach (var exception in ex.LoaderExceptions)
                {
                    Log.Error("Could not load assemblies containing Frapid Web API. Exception: {Exception}", exception);
                }
            }

            return assemblies;
        }
    }
}