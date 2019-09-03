using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTrackerApi.Services.Student
{
    public interface IStudentService : IBaseCrudService<Domains.Student>
    {
        Domains.Student ReadByName(string name);
    }
}
