using Expense_Tracker_API.Data;
using Expense_Tracker_API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Expense_Tracker_API.Controllers
{
	[Route("[controller]")]
	[ApiController]
	[Authorize]
	public class CategoryController : ControllerBase
	{



		private readonly AppDbContext _db;

		public CategoryController(AppDbContext db) { 
		_db = db;
		
		}


		[HttpGet]
		public IActionResult Get() 
		{

			var fetchAllCategory = _db.categories.Where(e => e.Name != null).ToList();


			var ResultConvertedToDto = covertToDto();

			List<CategoryDto> covertToDto()
			{
				List<CategoryDto> data = new List<CategoryDto>();
				for (int i = 0; i < fetchAllCategory.Count; i++)
				{
					var convertedDto = DtoConverter(fetchAllCategory[i]);
					data.Add(convertedDto);
				}

				return data;
			}



			if (ResultConvertedToDto != null)
			{
				return Ok(ResultConvertedToDto);
			}

			return BadRequest();



		}



		private CategoryDto DtoConverter(Category category)
		{
			return new CategoryDto {Id=category.Id,Name = category.Name };


		}



	}
}
