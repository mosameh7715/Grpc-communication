using GrpcClient.IClientSerivces;
using GrpcServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace GrpcClient.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentController:ControllerBase
	{
		private readonly IStudent _student;

		public StudentController(IStudent student)
		{
			_student = student;
		}

		[HttpPost("PostNewStudent")]
		public IActionResult PostNewStudent(string name)
		{
			int Id = _student.PostNewStudent(name);
			if (Id == 0)
			{
				return StatusCode(500,"Internal Server Error");
			}
			return Ok($"{name} is added successfully with Id: {Id}");
		}

		[HttpGet("GetStudentById/{studentId}")]
		public IActionResult GetStudentById (int studentId)
		{
			Student student = _student.GetStudentById(studentId);

			if (student != null)
			{
				return Ok(student);
			}
			return NotFound();
		}
	}
}
