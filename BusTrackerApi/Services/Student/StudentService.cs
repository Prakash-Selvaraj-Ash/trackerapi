using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusTrackerApi.Domains;
using BusTrackerApi.Repositories;
using BusTrackerApi.Services.Entity;

namespace BusTrackerApi.Services.Student
{
    public class StudentService : BaseCrudService<Domains.Student> , IStudentService
    {
        private readonly IRepository<Domains.Student> _studentRepository;
        public StudentService(IRepository<Domains.Student> repository,
            IEntityService entityService) : base(repository, entityService)
        {
            _studentRepository = repository;
        }

        public Domains.Student ReadByName(string name)
        {
            return _studentRepository.Set.Single(s => s.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
