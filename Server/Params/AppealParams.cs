using LinqKit;
using Microsoft.EntityFrameworkCore;
using Server.DTOs;
using Server.Helpers;

namespace Server.Params
{
    public class AppealParams : PageParams
    {
        public ParamPropInt? Id { get; set; }
        public bool? Rap { get; set; }
        public string? Dept { get; set; }
        public ParamPropString? Mpid { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? Meeting { get; set; }
        public string? Status { get; set; }
        public string? Notes { get; set; }
        public string? StatusUpdateUser { get; set; }
        public DateTime? StatusUpdateDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public string? Search { get; set; }
    }

    public class ParamPropString
    {
        public string? Match { get; set; }
        public bool Or { get; set; } = false;
        public ParamPropString[]? MatchMultiple { get; set; }
    }

    public class ParamPropInt
    {
        public int? Match { get; set; }
        public bool Or { get; set; } = false;
    }

    public class AppealQuery
    {
        private readonly AppealParams _appealParams;
        private IQueryable<AppealListDto> _query;
        private ExpressionStarter<AppealListDto> _predicate;
        private bool _hasPredicate = false;
        public AppealQuery(AppealParams appealParams, IQueryable<AppealListDto> query)
        {
            _appealParams = appealParams;
            _query = query;
            _predicate = PredicateBuilder.New<AppealListDto>();  

            this.BuildQueryId();
            this.BuildQueryRap();
            this.BuildQueryDept();
            this.BuildQueryMpid();
            this.BuildQueryFirstName();
            this.BuildQueryLastName();
            this.BuildQueryStatus();
            this.BuildQueryUpdatedUser();
            this.BuildQueryAll();
        }

        public IQueryable<AppealListDto> GetQuery()
        {
            if (_hasPredicate) {
                _query = _query.Where(_predicate);
            } 
            return _query;
        }

        private void BuildQueryId()
        {
            if (_appealParams.Id == null) {
                return;
            }
            
            if (_appealParams.Id.Or) {
                _hasPredicate = true;
                if (_appealParams.Id.Match != null)
                {
                    _predicate = _predicate.Or(x => x.Id == _appealParams.Id.Match);
                }
                return;
            }

            if (_appealParams.Id.Match != null) {
                _query = _query.Where(x => x.Id == _appealParams.Id.Match);
            }
        }

        private void BuildQueryRap()
        {
            if (_appealParams.Rap == null) {
                return;
            }

            _query = _query.Where(x => x.Rap == _appealParams.Rap);
        }

        private void BuildQueryDept()
        {
            if (_appealParams.Dept == null) {
                return;
            }

            _query = _query.Where(x => x.Dept == _appealParams.Dept);
        }

        private void BuildQueryMpid()
        {
            if (_appealParams.Mpid == null)
            {
                return;
            }

            if (_appealParams.Mpid.Or && _appealParams.Mpid.Match != null) 
            {
                _hasPredicate = true;
                _predicate = _predicate.Or(x => EF.Functions.Like(x.Mpid, _appealParams.Mpid.Match));
                return;
            }

            if (_appealParams.Mpid.Match != null)
            {
                _query = _query.Where(x => EF.Functions.Like(x.Mpid, _appealParams.Mpid.Match));
                return;
            }
            
            if (_appealParams.Mpid.MatchMultiple != null) 
            {
                foreach (ParamPropString prop in _appealParams.Mpid.MatchMultiple) 
                {
                    if (prop.Match != null) 
                    {
                        _hasPredicate = true;
                        _predicate = _predicate.Or(x => EF.Functions.Like(x.Mpid, prop.Match));
                    }
                }
            }
        }

        private void BuildQueryFirstName()
        {
            if (_appealParams.FirstName == null) {
                return;
            }

            _query = _query.Where(x => x.FirstName!.Contains(_appealParams.FirstName));
        }

        private void BuildQueryLastName()
        {
            if (_appealParams.LastName == null) {
                return;
            }

            _query = _query.Where(x => x.LastName!.Contains(_appealParams.LastName));
        }

        private void BuildQueryStatus()
        {
            if (_appealParams.Status == null) {
                return;
            }

            _query = _query.Where(x => x.Status!.Contains(_appealParams.Status));
        }

        private void BuildQueryUpdatedUser()
        {
            if (_appealParams.StatusUpdateUser == null) {
                return;
            }

            _query = _query.Where(x => x.StatusUpdateUser!.Contains(_appealParams.StatusUpdateUser));
        }

        private void BuildQueryAll()
        {
            if (_appealParams.Search == null) {
                return;
            }

            var searchHelper = new SearchHelper<AppealListDto>();
            _query = searchHelper.ApplySearch(_query, _appealParams.Search);

            // string search = appealParams.Search;
            // var listAnd = search.Split("AND").ToList();
            // var listOr = search.Split("OR").ToList();

            // var predicate = PredicateBuilder.New<AppealListDto>();
            // predicate.And(x => x.Rap == appealParams.Rap);
        }
    }
}