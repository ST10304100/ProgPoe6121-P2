using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROG_PART_2.Migrations
{
    /// <inheritdoc />
    public partial class DocumentUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Claims_ClaimId",
                table: "Documents");

            migrationBuilder.AlterColumn<int>(
                name: "ClaimId",
                table: "Documents",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Claims_ClaimId",
                table: "Documents",
                column: "ClaimId",
                principalTable: "Claims",
                principalColumn: "ClaimId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Claims_ClaimId",
                table: "Documents");

            migrationBuilder.AlterColumn<int>(
                name: "ClaimId",
                table: "Documents",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Claims_ClaimId",
                table: "Documents",
                column: "ClaimId",
                principalTable: "Claims",
                principalColumn: "ClaimId");
        }
    }
}
