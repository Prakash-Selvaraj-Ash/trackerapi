using BusTrackerApi.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTrackerApi.Repositories
{
    public interface IRepository<TDomain>
        where TDomain: class, IDomain
    {
        IQueryable<TDomain> GetAll();
        TDomain ReadById(Guid id);
        IQueryable<TDomain> ReadByIds(Guid[] ids);
        TDomain Update(TDomain domain);
        TDomain Create(TDomain domain);
        //void Delete(Guid id);
        DbSet<TDomain> Set { get; }
    }
}
