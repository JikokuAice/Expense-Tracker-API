using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expense_Tracker_API.Migrations
{
    /// <inheritdoc />
    public partial class introducedJoinTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryExpense");

            migrationBuilder.CreateTable(
                name: "ExpenseCategory",
                columns: table => new
                {
                    ExpenseId = table.Column<int>(type: "int", nullable: false),
                    Categoryid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseCategory", x => new { x.Categoryid, x.ExpenseId });
                    table.ForeignKey(
                        name: "FK_ExpenseCategory_Expense_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expense",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpenseCategory_categories_Categoryid",
                        column: x => x.Categoryid,
                        principalTable: "categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseCategory_ExpenseId",
                table: "ExpenseCategory",
                column: "ExpenseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpenseCategory");

            migrationBuilder.CreateTable(
                name: "CategoryExpense",
                columns: table => new
                {
                    categoryId = table.Column<int>(type: "int", nullable: false),
                    expensesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryExpense", x => new { x.categoryId, x.expensesId });
                    table.ForeignKey(
                        name: "FK_CategoryExpense_Expense_expensesId",
                        column: x => x.expensesId,
                        principalTable: "Expense",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryExpense_categories_categoryId",
                        column: x => x.categoryId,
                        principalTable: "categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryExpense_expensesId",
                table: "CategoryExpense",
                column: "expensesId");
        }
    }
}
