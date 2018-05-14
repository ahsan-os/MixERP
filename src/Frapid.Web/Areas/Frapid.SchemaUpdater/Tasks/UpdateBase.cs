using System.Threading.Tasks;
using Frapid.Configuration.Models;

namespace Frapid.SchemaUpdater.Tasks
{
    public abstract class UpdateBase
    {
        protected UpdateBase(string tenant, Installable app)
        {
            this.Tenant = tenant;
            this.App = app;
            this.Candidate = new UpdateCandidate(app, tenant);
        }

        public Installable App { get; }
        public string Tenant { get; }
        public UpdateCandidate Candidate { get; }

        public abstract Task<string> UpdateAsync();
    }
}