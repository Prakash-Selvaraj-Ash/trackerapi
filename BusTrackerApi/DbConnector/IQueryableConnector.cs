using BusTrackerApi.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BusTrackerApi.DbConnector
{
    public interface IQueryableConnector
    {
        DbSet<TDomain> GetDbSet<TDomain>()
            where TDomain : class, IDomain;
        IQueryable<TDomain> ReadAll<TDomain>()
            where TDomain : class, IDomain;
        IQueryable<TDomain> ReadByIds<TDomain>(Guid[] ids)
            where TDomain : class, IDomain, IWithId;
        TDomain ReadById<TDomain>(Guid id)
            where TDomain : class, IDomain;
        TDomain Update<TDomain>(TDomain domain)
            where TDomain : class, IDomain;
        TDomain Create<TDomain>(TDomain domain)
            where TDomain : class, IDomain;
        Task<TDomain> ReadByIdAsync<TDomain>(Guid id, CancellationToken cancellationToken)
            where TDomain : class, IDomain;
        Task<EntityEntry<TDomain>> CreateAsync<TDomain>(TDomain domain, CancellationToken cancellationToken)
            where TDomain : class, IDomain;
    }
}
