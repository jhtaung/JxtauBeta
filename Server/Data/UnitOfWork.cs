using AutoMapper;
using Server.Interfaces;

namespace Server.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppealContext _appealContext;
        private readonly PorticoContext _porticoContext;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        public UnitOfWork(
            AppealContext appealContext, 
            PorticoContext porticoContext, 
            IMapper mapper, 
            HttpClient httpClient
        ) {
            _appealContext = appealContext;
            _porticoContext = porticoContext;
            _mapper = mapper;
            _httpClient = httpClient;
        }
        public IAddressRepo AddressRepo => new AddressRepo(_httpClient);
        public IAppealRepo AppealRepo => new AppealRepo(_appealContext, _mapper);
        public IEformRepo EformRepo => new EformRepo(_porticoContext, _mapper, _httpClient);
        public async Task<bool> Complete()
        {
            return await _appealContext.SaveChangesAsync() > 0;
        }
        public bool HasChanges()
        {
            return _appealContext.ChangeTracker.HasChanges();
        }
    }
}