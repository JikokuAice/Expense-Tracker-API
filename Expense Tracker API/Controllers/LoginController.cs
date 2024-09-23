using Expense_Tracker_API.Data;
using Expense_Tracker_API.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Expense_Tracker_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{

		private readonly AppDbContext _db;
		private readonly IConfiguration _configuration;

        public LoginController(AppDbContext db,IConfiguration configuration)
        {
			_db = db;

			_configuration = configuration;
            
        }

        [AllowAnonymous]
		[HttpGet]
		public IActionResult Get()
		{
			return Ok("check check in login Api");
		}

		[AllowAnonymous]
		[HttpPost]
		public IActionResult Post([FromBody] Login loginData )
		{

			var user = Authenticate(loginData);

			if(user!=null)
			{
				System.Diagnostics.Debug.WriteLine("check it now");

				var token = GenerateToken(user);

				return Ok(token);
			
			}

			return BadRequest("Login failure ! please try again.");
		


		}

		private Users? Authenticate(Login data)
		{

			var currentUser = _db.UsersDataSet.FirstOrDefault(e =>

			e.Email.Equals(data.Email) && e.Password.Equals(data.Password)

			);

			if (currentUser != null)
			{
				return currentUser;
			}

			return null;

		}


		private String GenerateToken(Users user)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

			var credential = new SigningCredentials(securityKey, algorithm:SecurityAlgorithms.HmacSha256);


			var claims = new Claim[]
			{
				new Claim (type:ClaimTypes.PrimarySid,user.Id.ToString()),
				new Claim(type:ClaimTypes.Email,user.Email),
				new Claim(type:ClaimTypes.Name,user.Name),

			};


			var token = new  JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"],claims,expires:DateTime.Now.AddMinutes(15),signingCredentials:credential);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}

	}
}






