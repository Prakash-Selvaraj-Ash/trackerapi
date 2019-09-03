using System;
using System.Linq;
using BusTrackerApi.DbConnector;
using BusTrackerApi.Domains;
using Microsoft.EntityFrameworkCore;

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
    }
}
