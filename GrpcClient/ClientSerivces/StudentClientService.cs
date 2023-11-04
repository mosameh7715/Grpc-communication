using GrpcClient.IClientSerivces;
using GrpcServer.Protos;

namespace GrpcClient.ClientSerivces
{
	public class StudentClientService:IStudent
	{
		private readonly StudentService.StudentServiceClient _stdClient;

		public StudentClientService(StudentService.StudentServiceClient stdClient)
		{
			_stdClient = stdClient;
		}
		public int PostNewStudent(string name)
		{
			try
			{
				RequestAddStudent addStudent = new RequestAddStudent();
				addStudent.Student = new Student();
				addStudent.Student.Name = name;
				var stdId = _stdClient.AddNewStudent(addStudent).StdId;
				return stdId;
			}
			catch(Exception ex)
			{

                Console.WriteLine(ex.Message);
            }
			return 0;
		}
		public GrpcServer.Models.Student GetStudentById(int Id)
		{
			try
			{
				RequestStudentId requestStudentId = new RequestStudentId();
				requestStudentId.StdId = Id;
				var studentData =  _stdClient.GetStudentById(requestStudentId).Student;

				var student = new GrpcServer.Models.Student(); 
				student.Name = studentData.Name;	
				student.Id = studentData.StdId;

				return student;
			}
			catch(Exception ex)
			{
                Console.WriteLine(ex.Message);
            }
			return null;
		}
	}
}
