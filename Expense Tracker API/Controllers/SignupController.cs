using Expense_Tracker_API.Data;
using Expense_Tracker_API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Expense_Tracker_API.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class SignupController : ControllerBase
	{

		private readonly AppDbContext _db;	
		public SignupController(AppDbContext db) { 
		_db = db;
		
		}

		//user signup logic
		[HttpPost]
		public IActionResult Post([FromBody] Users user)
		{
			var userExists = _db.UsersDataSet.FirstOrDefault(db => db.Email == user.Email);

			if (user != null && userExists==null)
			{

				_db.UsersDataSet.Add(user);
				_db.SaveChanges();

				return Ok($"you are sucessfully signup Mr/Mrs.{user.Name}");


			}


			return Conflict("User Exists Confict ocuured ! try Again.");


		}




	}
}	
