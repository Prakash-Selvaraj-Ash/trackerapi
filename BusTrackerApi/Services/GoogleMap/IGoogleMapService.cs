using BusTrackerApi.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTrackerApi.Services.GoogleMap
{
    public interface IGoogleMapService
    {
        Task<string> GetDirections(
            GeoCoordinateDto origin,
            IEnumerable<GeoCoordinateDto> wayPoints,
            GeoCoordinateDto destination);

        Task<string> GetDuration(
            GeoCoordinateDto currentPoint,
            IEnumerable<GeoCoordinateDto> wayPoints);
    }
}
