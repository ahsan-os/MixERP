using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Serilog;

namespace Frapid.Framework.Extensions
{
    public static class TypeExtensions
    {
        public static IEnumerable<T> GetTypeMembers<T>(this Type iType)
        {
            var members = new List<T>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                try
                {
                    var types = assembly.GetTypes();

                    var membersInAssembly = types.Where(x => iType.IsAssignableFrom(x) && !x.IsInterface).Select(Activator.CreateInstance).Cast<T>();

                    members.AddRange(membersInAssembly);
                }
                catch (Exception ex)
                {
                    var loadException = ex as ReflectionTypeLoadException;

                    if (loadException != null)
                    {
                        var typeLoadException = loadException;
                        var loaderExceptions = typeLoadException.LoaderExceptions;

                        foreach (var exception in loaderExceptions)
                        {
                            Log.Error(exception.Message);
                        }
                    }
                }
            }

            return members;
        }

        public static IEnumerable<T> GetTypeMembersNotAbstract<T>(this Type iType)
        {
            var members = new List<T>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                try
                {
                    var types = assembly.GetTypes();

                    var membersInAssembly = types.Where(x => iType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).Select(Activator.CreateInstance).Cast<T>();

                    members.AddRange(membersInAssembly);
                }
                catch (Exception ex)
                {
                    var loadException = ex as ReflectionTypeLoadException;

                    if (loadException != null)
                    {
                        var typeLoadException = loadException;
                        var loaderExceptions = typeLoadException.LoaderExceptions;

                        foreach (var exception in loaderExceptions)
                        {
                            Log.Error(exception.Message);
                        }
                    }
                }
            }

            return members;
        }
    }
}