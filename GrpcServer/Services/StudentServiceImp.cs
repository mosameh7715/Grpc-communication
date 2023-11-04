using Grpc.Core;
using GrpcServer.Data;
using GrpcServer.Protos;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentModel = GrpcServer.Models.Student;
namespace GrpcServer.Services
{
	public class StudentServiceImp : StudentService.StudentServiceBase
	{
		private readonly AppDbContext _context;
		public StudentServiceImp(AppDbContext context)
		{
			_context = context;
		}
		public override async Task<ResponseStudentId> AddNewStudent(RequestAddStudent request,ServerCallContext context)
		{
			try
			{
				if (request.Student.Name.IsNullOrEmpty())
				{
					throw new RpcException(new Status(StatusCode.InvalidArgument,"Provice a Valid name"));
				}

				StudentModel newStudent = new StudentModel();
				newStudent.Name = request.Student.Name;
				_context.Students.Add(newStudent);
				await _context.SaveChangesAsync();

				ResponseStudentId responseStudentId = new ResponseStudentId();
				responseStudentId.StdId = newStudent.Id;

				return await Task.FromResult(responseStudentId);
			}
			catch(Exception ex)
			{
				throw new RpcException(new Status(StatusCode.Unavailable,"Student data failed to be saved"));
			}
		}

		public override async Task<ResponseStudentData> GetStudentById(RequestStudentId request,ServerCallContext context)
		{
			try
			{
				var student = await _context.Students.Where(std => std.Id == request.StdId).FirstOrDefaultAsync(); 

				if (student != null)
				{
					ResponseStudentData studentData = new ResponseStudentData();
					studentData.Student = new Student();
					studentData.Student.StdId = student.Id;
					studentData.Student.Name = student.Name;

					return await Task.FromResult(studentData);
				}

				throw new RpcException(new Status(StatusCode.NotFound,$"student with Id: {request.StdId} is not found"));
			}
			catch(Exception)
			{

				throw new RpcException(new Status(StatusCode.Unavailable,"Student data failed to be retrieved"));
			}
		}
	}
}
