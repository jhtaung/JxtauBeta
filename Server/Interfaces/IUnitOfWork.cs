namespace Server.Interfaces
{
    public interface IUnitOfWork
    {
        IAppealRepo AppealRepo { get; }
        Task<bool> Complete();
        bool HasChanges();
    }
}