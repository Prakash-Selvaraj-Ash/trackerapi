using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusTrackerApi.Domains;
using BusTrackerApi.DTOS;
using BusTrackerApi.Services.Place;
using BusTrackerApi.Services.Student;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IPlaceService _placeService;
        public StudentsController(
            IStudentService studentService,
            IPlaceService placeService,
            IMapper mapper)
        {
            _studentService = studentService;
            _placeService = placeService;
            _mapper = mapper;
        }

        [HttpGet()]
        public StudentResponse[] ReadAll()
        {
            var students = _studentService.ReadAll().ToArray();
            return students.Select(s => _mapper.Map<StudentResponse>(s)).ToArray();
        }

        [HttpGet("byName")]
        public StudentResponse ReadByName([FromQuery]string name)
        {
            var student = _studentService.ReadByName(name);
            return _mapper.Map<StudentResponse>(student);
        }

        [HttpGet("{studentId}")]
        public StudentResponse ReadById(Guid studentId)
        {
            var student = _studentService.ReadById(studentId);
            return _mapper.Map<StudentResponse>(student);
        }

        [HttpPost]
        public StudentResponse Create(CreateStudentRequest createStudentRequest)
        {
            var student = _mapper.Map<Student>(createStudentRequest);

            var createdStudent = _studentService.Create(student);

            var studentResponse = _mapper.Map<StudentResponse>(createdStudent);

            return studentResponse;
        }
    }
}