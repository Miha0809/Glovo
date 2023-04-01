using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Glovo.Services.Migrations
{
    /// <inheritdoc />
    public partial class V4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Companies_Email",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Clients_Email",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Clients");

            migrationBuilder.AddColumn<int>(
                name: "EmailId",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmailId",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_EmailId",
                table: "Companies",
                column: "EmailId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_EmailId",
                table: "Clients",
                column: "EmailId");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_Name",
                table: "Emails",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Emails_EmailId",
                table: "Clients",
                column: "EmailId",
                principalTable: "Emails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Emails_EmailId",
                table: "Companies",
                column: "EmailId",
                principalTable: "Emails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Emails_EmailId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Emails_EmailId",
                table: "Companies");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropIndex(
                name: "IX_Companies_EmailId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Clients_EmailId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "EmailId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "EmailId",
                table: "Clients");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Companies",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Clients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Email",
                table: "Companies",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Email",
                table: "Clients",
                column: "Email",
                unique: true);
        }
    }
}
