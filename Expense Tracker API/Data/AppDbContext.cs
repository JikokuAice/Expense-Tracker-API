using Expense_Tracker_API.Model;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker_API.Data
{
	public class AppDbContext:DbContext
	{


		//make a field readonly meaning we can only add value to _optionat time of decleartion or in contructor only.
		private readonly DbContextOptions<AppDbContext> _options;	

		public  AppDbContext(DbContextOptions<AppDbContext> options):base(options:options) { 
			//we can insert value while declearing constructor.
		_options = options;
		}


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Expense>().HasOne(e => e.Users).WithMany(e => e.Expenses).HasForeignKey(e => e.UsersId).IsRequired();
			modelBuilder.Entity<Expense>().HasMany(e => e.category).WithMany(e => e.expenses).UsingEntity<ExpenseCategory>();
		}

		public DbSet<Users> UsersDataSet { get; set; }	

		public DbSet<Expense> Expense {  get; set; }

		public DbSet<Category> categories {  get; set; }

		public DbSet<ExpenseCategory> ExpenseCategory { get; set; }	

	}
}
