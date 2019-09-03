using BusTrackerApi.DbConnector;
using BusTrackerApi.Domains;

namespace BusTrackerApi.Repositories
{
    public class StudentRepository : RepositoryBase<Student>
    {
        public StudentRepository(IQueryableConnector queryableConnector) : base(queryableConnector)
        {
        }
    }
}
