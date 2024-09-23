using Expense_Tracker_API.Data;
using Expense_Tracker_API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Web.Http.Description;

namespace Expense_Tracker_API.Controllers
{
	[Route("[controller]")]
	[ApiController]
	[Authorize]
	public class ExpenseController : ControllerBase
	{

		private readonly AppDbContext _db;
		public ExpenseController(AppDbContext db)
		{
			_db = db;

		}



		[HttpGet]
		[ResponseType(typeof(ExpenseDto))]
		public IActionResult Get()
		{
			var getUserId = getCurrentUser().Id;

			System.Diagnostics.Debug.WriteLine(getUserId);

			var allExpenses = _db.Expense.Where(e => e.UsersId == getUserId).ToList();
	


			var data = covertToDto();

			 List<ExpenseDto> covertToDto()
			{
				 List<ExpenseDto> data=new List<ExpenseDto>();
				for(int i = 0; i < allExpenses.Count; i++)
				{
					var convertedDto = DtoConverter(expense: allExpenses[i]);
					data.Add(convertedDto);
				}

				return data;
			}

			if (allExpenses != null)
			{
				return Ok(data);
			}
			return BadRequest();

		}


		[HttpPost]
		public IActionResult Post(ExpenseDto expense)
		{

			var checkIfCategoryIsCorrect = _db.categories.FirstOrDefault(e=>e.Name.Equals(expense.ExpenseType));

			if (checkIfCategoryIsCorrect == null)
			{
				return BadRequest($"Expense type : {expense.ExpenseType} !! doesnot exist");
			}


			var getUserId = getCurrentUser().Id;

			var selectedExpense = _db.Expense.FirstOrDefault(exp =>
			exp.CategoryType.Equals(expense.ExpenseType)
			&& exp.ExpenseName.Equals(expense.ExpenseName) && exp.UsersId.Equals(getUserId));

			if (selectedExpense != null)
			{
				   return BadRequest("Expense you are trying to create already exists.");
			}


			var checkUserIdExist = _db.UsersDataSet.Find(getUserId);

			if (checkUserIdExist == null)
			{
				return Unauthorized("user doesn't exist ! u cant perform follwing request");
			}


			try
			{
				//converting from expense DTO obbj to Expense class  OBJ
				var expenseConvert = DtoToExpenseConverter(expense, getUserId);


				_db.Expense.Add(expenseConvert);
				_db.SaveChanges();




				var expenseCategory = new ExpenseCategory
				{
					ExpenseId = expenseConvert.Id,
					Categoryid = checkIfCategoryIsCorrect!.Id,

				};


				_db.ExpenseCategory.Add(expenseCategory);
				_db.SaveChanges();



				var dtoConvert = DtoConverter(expenseConvert);

				return Created("ok",dtoConvert);

			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex);
				return StatusCode(500, "An error occurred while creating the expense.");
			}

			



		}


		


		[HttpDelete("{expenseId}")]
		public IActionResult Delete([FromRoute]int expenseId)
		{

			var currentExpense = _db.Expense.Find(expenseId);
		 

			if (currentExpense != null)
			{

				var getCategoryID = _db.categories.FirstOrDefault(e=>e.Name==currentExpense.CategoryType);
				var findExpenseCategoryObj = _db.ExpenseCategory.FirstOrDefault(e => e.ExpenseId == expenseId && e.Categoryid == getCategoryID!.Id);

            _db.Remove(currentExpense);
			_db.SaveChanges();

		


				return Ok("Sucessfully deleted Expense");
			}else if (currentExpense == null)
			{
				return NotFound("Expense does't exists");
			}

			return BadRequest("please! url carefully before requesting");				
			

		}

		[HttpPut]
		public IActionResult Put(ExpenseDto expense)
		{

			var checkIfExpenseIdExist = _db.Expense.Find(expense.Id);
			var currentUserId = getCurrentUser().Id;




			var convertToExpense = DtoToExpenseConverter(expense, currentUserId);
			if(checkIfExpenseIdExist != null)
			{
				_db.Expense.Update(convertToExpense);
				_db.SaveChanges();
				return Ok("sucessfully updated expense");
			}else
			{
				return NotFound("expense  you want update !!doesn't exists!!");
			}

		}



		private ExpenseDto DtoConverter(Expense expense) {

			return new ExpenseDto()
			{
				Id = expense.Id,
				ExpenseName = expense.ExpenseName,
				ExpenseAmount = expense.ExpenseAmount,
				ExpenseType = expense.CategoryType

			};

		}


		private Expense DtoToExpenseConverter(ExpenseDto dto,int userId)
		{
			return new Expense
			{
				Id=0,
				ExpenseName= dto.ExpenseName,
				ExpenseAmount= dto.ExpenseAmount,
				CategoryType=dto.ExpenseType,
				 UsersId = userId
			};

		}


		private Users getCurrentUser()
		{
			var user = HttpContext.User.Identity as ClaimsIdentity;

			if (user != null)
			{
				var userClaim = user.Claims;

				return new Users
				{
					Id = int.Parse(userClaim.FirstOrDefault(o => o.Type == ClaimTypes.PrimarySid)?.Value!)

				};
			}
			return null;

		}



	}
}
