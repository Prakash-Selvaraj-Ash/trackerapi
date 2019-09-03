using BusTrackerApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTrackerApi.Services
{
    public interface IBaseCrudService<TDomain>
        where TDomain: class, IDomain, IWithId
    {
        IQueryable<TDomain> ReadAll();
        TDomain Create(TDomain domain);
        TDomain Update(TDomain domain);
        TDomain ReadById(Guid id);
        IQueryable<TDomain> ReadByIds(Guid[] ids);
    }
}
