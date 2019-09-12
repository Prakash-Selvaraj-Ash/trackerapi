using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        public virtual TDomain Create(TDomain domain)
        {
            var createdDomain = _repository.Create(domain);
            _entityService.Save();
            return createdDomain;
        }

        public virtual async Task<TDomain> CreateAsync(TDomain domain, CancellationToken token)
        {
            var createdEntity = await _repository.CreateAsync(domain, token);
            await _entityService.SaveAsync(token);
            return createdEntity.Entity;
        }

        public virtual IQueryable<TDomain> ReadAll()
        {
            return _repository.GetAll();
        }

        public virtual async Task<TDomain[]> ReadAllAsync(CancellationToken token)
        {
            return await _repository.GetAllAsync(token);
        }

        public virtual TDomain ReadById(Guid id)
        {
            return _repository.ReadById(id);
        }

        public Task<TDomain> ReadByIdAsync(Guid id, CancellationToken token)
        {
            return _repository.ReadByIdAsync(id, token);
        }

        public virtual IQueryable<TDomain> ReadByIds(Guid[] ids)
        {
            return _repository.ReadByIds(ids);
        }

        public virtual async Task<TDomain[]> ReadByIdsAsync(Guid[] ids, CancellationToken token)
        {
            return await _repository.ReadByIdsAsync(ids, token);
        }

        public virtual TDomain Update(TDomain domain)
        {
            var updatedDomain = _repository.Update(domain);
            _entityService.Save();
            return updatedDomain;
        }

        public virtual void DeleteAll()
        {
            _repository.DeleteAll();
            _entityService.Save();
        }
    }
}
