using System;
using System.Linq;
using BusTrackerApi.Domains;
using BusTrackerApi.Repositories;
using BusTrackerApi.Services.Entity;

namespace BusTrackerApi.Services
{
    public class BaseCrudService<TDomain> : IBaseCrudService<TDomain>
        where TDomain : class, IDomain, IWithId
    {
        private readonly IRepository<TDomain> _repository;
        private readonly IEntityService _entityService;

        public BaseCrudService(
            IRepository<TDomain> repository,
            IEntityService entityService)
        {
            _repository = repository;
            _entityService = entityService;
        }
        public TDomain Create(TDomain domain)
        {
            var createdDomain = _repository.Create(domain);
            _entityService.Save();
            return createdDomain;
        }

        public IQueryable<TDomain> ReadAll()
        {
            return _repository.GetAll();
        }

        public TDomain ReadById(Guid id)
        {
            return _repository.ReadById(id);
        }

        public IQueryable<TDomain> ReadByIds(Guid[] ids)
        {
            return _repository.ReadByIds(ids);
        }

        public TDomain Update(TDomain domain)
        {
            var updatedDomain = _repository.Update(domain);
            _entityService.Save();
            return updatedDomain;
        }
    }
}
