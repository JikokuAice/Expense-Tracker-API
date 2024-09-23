using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Expense_Tracker_API.Model
{
	public class Expense
	{
		[Key]
		public int Id { get; set; }


		public String ExpenseName {  get; set; }


		public double ExpenseAmount { get; set; }


		

		[BindNever]
		public int UsersId { get; set; }

		[JsonIgnore]
		public Users? Users { get; set; }


		public String CategoryType { get; set; }



		[JsonIgnore]
		public ICollection<Category> category { get; } = [];
		

		[JsonIgnore]
		public ICollection<ExpenseCategory> ExpenseCategories { get; set; }



	}

	public class ExpenseDto
	{

		public int Id { get; set; }


		public String ExpenseName { get; set; }


		public double ExpenseAmount { get; set; }


		public String ExpenseType { get; set; }




	}


}
