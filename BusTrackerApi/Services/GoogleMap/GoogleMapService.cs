using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BusTrackerApi.DTOS;

namespace BusTrackerApi.Services.GoogleMap
{
    public class GoogleMapService : IGoogleMapService
    {
        private const string DirectionService = "Directions";
        private const string DurationService = "Durations";

        public async Task<string> GetDirections(
            GeoCoordinateDto origin, 
            IEnumerable<GeoCoordinateDto> wayPoints,
            GeoCoordinateDto destination)
        {
            var baseUrlFormat = GetBaseUrlFormat(DirectionService);
            string wayPointString = string
                .Join("|", wayPoints
                    .Select(wp => $"via:{wp.ToString()}"));

            var directionUrl = string.Format(baseUrlFormat, origin.ToString(), destination.ToString(), wayPointString);
            return await GetResponse(directionUrl);
        }

        public async Task<string> GetDuration(GeoCoordinateDto currentPoint, IEnumerable<GeoCoordinateDto> wayPoints)
        {
            var baseUrlFormat = GetBaseUrlFormat(DurationService);
            string wayPointString = string
                .Join("|", wayPoints.Select(wp => wp.ToString()));

            var durationUrl = string.Format(baseUrlFormat, currentPoint.ToString(), wayPointString);
            return await GetResponse(durationUrl);
        }

        private async Task<string> GetResponse(string url)
        {
            using (var client = new HttpClient())
            {
                var responseString = await client.GetStringAsync(url);
                return responseString;
            }
        }

        private string GetBaseUrlFormat(string service)
        {
            switch (service)
            {
                case DirectionService:
                    return "https://maps.googleapis.com/maps/api/directions/json?origin={0}&destination={1}&waypoints={2}&key=AIzaSyDJXHoRw6f7SuuH-YHHV5M03aa3wrL6SRA";
                case DurationService:
                    return "https://maps.googleapis.com/maps/api/distancematrix/json?origins={0}&destinations={1}&key=AIzaSyDJXHoRw6f7SuuH-YHHV5M03aa3wrL6SRA";
                default:
                    return null;
            }
        }
    }
}
