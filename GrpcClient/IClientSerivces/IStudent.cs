using GrpcServer.Models;

namespace GrpcClient.IClientSerivces
{
	public interface IStudent
	{
		public int PostNewStudent(string name);
		public Student GetStudentById(int Id);
	}
}
