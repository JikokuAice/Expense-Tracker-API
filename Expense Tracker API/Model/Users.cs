using System.ComponentModel.DataAnnotations;

namespace Expense_Tracker_API.Model
{
	public class Users
	{
		[Key]
		public int Id { get; set; }


		[Required]
		public String Name { get; set; }

		[Required]
		[EmailAddress]
		public String Email { get; set; }

		[Required]
		public String Password { get; set; }


		[Required]
		[Compare(otherProperty:"Password",ErrorMessage ="Confirm password Input doesnot match given password")]
		public String ConfirmPassword { get; set; }


		public ICollection<Expense> Expenses { get;}=new List<Expense>();	//colltection of expense related to sepeciif user


	}
}
