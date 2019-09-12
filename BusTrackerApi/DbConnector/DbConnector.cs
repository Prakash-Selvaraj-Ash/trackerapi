using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BusTrackerApi.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BusTrackerApi.DbConnector
{
    public class DbConnector : IQueryableConnector
    {
        private readonly BusTrackContext _busTrackContext;
        public DbConnector(BusTrackContext busTrackContext)
        {
            _busTrackContext = busTrackContext;
        }
        
        public DbSet<TDomain> GetDbSet<TDomain>()
            where TDomain: class, IDomain
        {
            return _busTrackContext.Set<TDomain>();
        }

        public TDomain Create<TDomain>(TDomain domain)
            where TDomain : class, IDomain
        {
            var set = _busTrackContext.Set<TDomain>();
            var result = set.Add(domain);
            return result.Entity;
        }

        public IQueryable<TDomain> ReadAll<TDomain>()
            where TDomain : class, IDomain
        {
            var set = _busTrackContext.Set<TDomain>();
            return set;
        }

        public TDomain ReadById<TDomain>(Guid id)
            where TDomain : class, IDomain
        {
            var set = _busTrackContext.Set<TDomain>();
            return set.Find(id);
        }

        public void DeleteAll<TDomain>()
            where TDomain : class, IDomain
        {
            var items = _busTrackContext.Set<TDomain>().ToArray();
            _busTrackContext.Set<TDomain>().RemoveRange(items.ToArray());
        }

        public IQueryable<TDomain> ReadByIds<TDomain>(Guid[] ids)
            where TDomain : class, IDomain, IWithId
        {
            var set = _busTrackContext.Set<TDomain>();
            return set.Where(domain => ids.Contains(domain.Id));
        }

        public TDomain Update<TDomain>(TDomain domain)
            where TDomain : class, IDomain
        {
            var set = _busTrackContext.Set<TDomain>();
            var updatedSet = set.Update(domain);
            return updatedSet.Entity;
        }

        public async Task<EntityEntry<TDomain>> CreateAsync<TDomain>(TDomain domain, CancellationToken cancellationToken)
            where TDomain : class, IDomain
        {
            var set = _busTrackContext.Set<TDomain>();
            return await set.AddAsync(domain, cancellationToken);
        }

        public async Task<TDomain> ReadByIdAsync<TDomain>(Guid id, CancellationToken cancellationToken)
            where TDomain : class, IDomain
        {
            var set = _busTrackContext.Set<TDomain>();
            return await set.FindAsync(id);
        }

        public async Task<TDomain[]> ReadAllAsync<TDomain>(CancellationToken token)
            where TDomain : class, IDomain
        {
            var set = _busTrackContext.Set<TDomain>();
            return await set.ToArrayAsync(token);
        }

        public async Task<TDomain[]> ReadByIdsAsync<TDomain>(Guid[] ids, CancellationToken token)
            where TDomain : class, IDomain, IWithId
        {
            var set = _busTrackContext.Set<TDomain>();
            return await set.Where(domain => ids.Contains(domain.Id)).ToArrayAsync(token);
        }
    }
}
