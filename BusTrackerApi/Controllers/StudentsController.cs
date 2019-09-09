using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusTrackerApi.Domains;
using BusTrackerApi.DTOS;
using BusTrackerApi.Extensions;
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
        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet()]
        public StudentResponse[] ReadAll()
        {
            var students = _studentService.ReadAll().ToList<StudentResponse>();
            return students.ToArray();
        }

        [HttpGet("byName")]
        public StudentResponse ReadByName([FromQuery]string name)
        {
            var student = _studentService.ReadByName(name);
            return student.To<StudentResponse>();
        }

        [HttpGet("{studentId}")]
        public StudentResponse ReadById(Guid studentId)
        {
            var student = _studentService.ReadById(studentId);
            return student.To<StudentResponse>();
        }

        [HttpPost]
        public StudentResponse Create(CreateStudentRequest createStudentRequest)
        {
            var student = createStudentRequest.To<Student>();

            var createdStudent = _studentService.Create(student);

            var studentResponse = createStudentRequest.To<StudentResponse>();

            return studentResponse;
        }
    }
}