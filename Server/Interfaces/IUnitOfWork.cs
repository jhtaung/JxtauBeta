namespace Server.Interfaces
{
    public interface IUnitOfWork
    {
        IAppealRepo AppealRepo { get; }
        IEformRepo EformRepo { get; }
        Task<bool> Complete();
        bool HasChanges();
    }
}