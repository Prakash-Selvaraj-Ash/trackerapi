using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BusTrackerApi.DbConnector;
using BusTrackerApi.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BusTrackerApi.Repositories
{
    public class RepositoryBase<TDomain> : IRepository<TDomain>
        where TDomain : class, IDomain, IWithId, new()
    {
        public IQueryableConnector Connector { get; }

        public DbSet<TDomain> Set { get { return Connector.GetDbSet<TDomain>(); } }

        public RepositoryBase(IQueryableConnector queryableConnector)
        {
            Connector = queryableConnector;
        }

        public IQueryable<TDomain> GetAll()
        {
            return Connector.ReadAll<TDomain>();
        }

        public TDomain ReadById(Guid id)
        {
            return Connector.ReadById<TDomain>(id);
        }

        public TDomain Update(TDomain domain)
        {
            return Connector.Update(domain);
        }

        public TDomain Create(TDomain domain)
        {
            return Connector.Create(domain);
        }

        public IQueryable<TDomain> ReadByIds(Guid[] ids)
        {
            return Connector.ReadByIds<TDomain>(ids);
        }

        public async Task<TDomain[]> GetAllAsync(CancellationToken token)
        {
            return await Connector.ReadAllAsync<TDomain>(token);
        }

        public async Task<EntityEntry<TDomain>> CreateAsync(TDomain domain, CancellationToken token)
        {
            return await Connector.CreateAsync(domain, token);
        }

        public async Task<TDomain> ReadByIdAsync(Guid id, CancellationToken token)
        {
            return await Connector.ReadByIdAsync<TDomain>(id, token);
        }

        public async Task<TDomain[]> ReadByIdsAsync(Guid[] ids, CancellationToken token)
        {
            return await Connector.ReadByIdsAsync<TDomain>(ids, token);
        }
    }
}
