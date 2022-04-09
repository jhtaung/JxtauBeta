namespace Server.Interfaces
{
    public interface ISearchHelper<T>
    {
        IQueryable<T> ApplySearch(IQueryable<T> entities, string searchQueryString);
    }
}