using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreBanking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fix1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PoliceClearanceStatus",
                table: "Authentications",
                newName: "PoliceClearancePassed");

            migrationBuilder.RenameColumn(
                name: "CivilRegistryStatus",
                table: "Authentications",
                newName: "CivilRegistryVerified");

            migrationBuilder.RenameColumn(
                name: "CentralBankCreditCheckStatus",
                table: "Authentications",
                newName: "CentralBankCreditCheckPassed");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PoliceClearancePassed",
                table: "Authentications",
                newName: "PoliceClearanceStatus");

            migrationBuilder.RenameColumn(
                name: "CivilRegistryVerified",
                table: "Authentications",
                newName: "CivilRegistryStatus");

            migrationBuilder.RenameColumn(
                name: "CentralBankCreditCheckPassed",
                table: "Authentications",
                newName: "CentralBankCreditCheckStatus");
        }
    }
}
