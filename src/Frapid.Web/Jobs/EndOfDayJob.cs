using System;
using System.Linq;
using Frapid.Framework.Routines;
using Quartz;
using Serilog;

namespace Frapid.Web.Jobs
{
    public class EndOfDayJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Log.Verbose("End of day job invoked.");

            try
            {
                var iType = typeof(IDayEndTask);
                var members =
                    AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(x => x.GetTypes())
                        .Where(x => iType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                        .Select(Activator.CreateInstance)
                        .ToList();

                foreach (IDayEndTask member in members)
                {
                    try
                    {
                        Log.Verbose($"Executing the job with description \"{member.Description}\".");
                        member.RegisterAsync().GetAwaiter().GetResult();
                    }
                    catch (Exception ex)
                    {
                        Log.Error("Could not register the EOD job \"{Description}\" due to errors. Exception: {Exception}", member.Description, ex);
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("{Exception}", ex);
                throw;
            }
        }
    }
}