using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Frapid.Authorization.DAL;

namespace Frapid.Authorization.ViewModels
{
    public class Entity
    {
        public string Name { get; set; }
        public string ObjectId { get; set; }

        public static async Task<IEnumerable<Entity>> GetEntitiesAsync(string tenant)
        {
            var data = await Entities.GetAsync(tenant).ConfigureAwait(false);

            var entities = data.Select
                (
                    item => new Entity
                    {
                        ObjectId = item.ObjectName,
                        Name = ToTitleCase(item.ObjectName.Replace(".", ": ").Replace("_", " "))
                    }).OrderBy(x => x.ObjectId).ToList();

            entities.Insert
                (
                    0,
                    new Entity
                    {
                        ObjectId = "",
                        Name = "All Objects"
                    });

            return entities;
        }

        private static string ToTitleCase(string s)
        {
            return string.Join(" ",
                s.Split(' ').Select(i => i.Substring(0, 1).ToUpper() + i.Substring(1).ToLower()).ToArray());
        }
    }
}