using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Expense_Tracker_API.Model
{
	public class Category
	{

		[Key]
		public int Id { get; set; }

		public String Name { get; set; }



		[JsonIgnore]
		public ICollection<Expense> expenses { get; } = [];

		[JsonIgnore]
		public ICollection<ExpenseCategory> ExpenseCategories { get; set; }

	}



	public class CategoryDto
	{

		public int Id { get; set; }

		public String Name { get; set; }

	};


}
