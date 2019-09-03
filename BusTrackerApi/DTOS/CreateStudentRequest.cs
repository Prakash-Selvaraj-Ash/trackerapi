using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTrackerApi.DTOS
{
    public class CreateStudentRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid PlaceId { get; set; }
        public Guid RouteId { get; set; }
        public string FcmId { get; set; }
    }
}
