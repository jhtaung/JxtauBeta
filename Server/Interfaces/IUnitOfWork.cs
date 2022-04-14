namespace Server.Interfaces
{
    public interface IUnitOfWork
    {
        IAddressRepo AddressRepo { get; }
        IAppealRepo AppealRepo { get; }
        IEformRepo EformRepo { get; }
        Task<bool> Complete();
        bool HasChanges();
    }
}