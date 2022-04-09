
using System.Reflection;
using System.Text;
using Server.Extensions;
using Server.Interfaces;

namespace Server.Helpers
{
    public class SearchHelper<T> : ISearchHelper<T>
    {
        public IQueryable<T> ApplySearch(IQueryable<T> entities, string searchQueryString)
        {
            if (!entities.Any()) return entities;

            if (string.IsNullOrWhiteSpace(searchQueryString)) return entities;

            entities = entities.FilterByProperties(searchQueryString, (prop, value) => prop.Contains(value), true);
            return entities;
        }        
    }
}