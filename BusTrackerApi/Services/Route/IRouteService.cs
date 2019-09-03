using BusTrackerApi.Domains;
using BusTrackerApi.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTrackerApi.Services.Route
{
    public interface IRouteService : IBaseCrudService<Domains.Route>
    {
        void AddPlace(RouteAssociation routeAssociation);
        Domains.Place[] ReadPlaces(Guid routeId);
    }
}
