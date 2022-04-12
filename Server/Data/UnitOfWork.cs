using AutoMapper;
using Server.Interfaces;

namespace Server.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppealContext _context;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        public UnitOfWork(AppealContext context, IMapper mapper, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _mapper = mapper;
            _context = context;
        }
        public IAppealRepo AppealRepo => new AppealRepo(_context, _mapper);
        public IEformRepo EformRepo => new EformRepo(_httpClient);
        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}