using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTrackerApi.Extensions
{
    public static class MapperExtensions
    {
        static public IMapper Mapper { get; set; }
        public static T To<T>(this object soure)
        {
            return Mapper.Map<T>(soure);
        }

        public static List<T> ToList<T>(this IEnumerable<object> source)
        {
            return source.Select(s => s.To<T>()).ToList();
        }
    }
}
