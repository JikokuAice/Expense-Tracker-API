using Expense_Tracker_API.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(option => {
	option.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionKey"));
	option.UseQueryTrackingBehavior(queryTrackingBehavior: QueryTrackingBehavior.NoTracking);
	}
);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{

	var audience = builder.Configuration.GetValue<String>("Jwt:Audience");
	var issuer = builder.Configuration.GetValue<String>("Jwt:Issuer");
	var key  = builder.Configuration.GetValue<String>("Jwt:Key");


	options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
	{

		ValidateLifetime = true,
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateIssuerSigningKey = true,
		ValidAudience = audience,
		ValidIssuer = issuer,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!))


	};

});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
