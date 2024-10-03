using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurrencyTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddUsersCurrencies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCurrency_Currencies_CurrencyId",
                table: "UserCurrency");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCurrency_Users_UserId",
                table: "UserCurrency");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCurrency",
                table: "UserCurrency");

            migrationBuilder.RenameTable(
                name: "UserCurrency",
                newName: "UsersCurrencies");

            migrationBuilder.RenameIndex(
                name: "IX_UserCurrency_CurrencyId",
                table: "UsersCurrencies",
                newName: "IX_UsersCurrencies_CurrencyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersCurrencies",
                table: "UsersCurrencies",
                columns: new[] { "UserId", "CurrencyId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UsersCurrencies_Currencies_CurrencyId",
                table: "UsersCurrencies",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersCurrencies_Users_UserId",
                table: "UsersCurrencies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersCurrencies_Currencies_CurrencyId",
                table: "UsersCurrencies");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersCurrencies_Users_UserId",
                table: "UsersCurrencies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersCurrencies",
                table: "UsersCurrencies");

            migrationBuilder.RenameTable(
                name: "UsersCurrencies",
                newName: "UserCurrency");

            migrationBuilder.RenameIndex(
                name: "IX_UsersCurrencies_CurrencyId",
                table: "UserCurrency",
                newName: "IX_UserCurrency_CurrencyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCurrency",
                table: "UserCurrency",
                columns: new[] { "UserId", "CurrencyId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserCurrency_Currencies_CurrencyId",
                table: "UserCurrency",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCurrency_Users_UserId",
                table: "UserCurrency",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
