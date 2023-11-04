using System.ComponentModel.DataAnnotations.Schema;

namespace GrpcServer.Models
{
	public class Student
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string? Name { get; set; }
	}
}
