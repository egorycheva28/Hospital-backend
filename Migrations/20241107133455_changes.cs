using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Laboratory_2.Migrations
{
    /// <inheritdoc />
    public partial class changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Consultations_ConsultationId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "Conclusion",
                table: "Inspections",
                newName: "Conclusions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "Doctors",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<Guid>(
                name: "ConsultationId",
                table: "Comments",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Consultations_ConsultationId",
                table: "Comments",
                column: "ConsultationId",
                principalTable: "Consultations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Consultations_ConsultationId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "Conclusions",
                table: "Inspections",
                newName: "Conclusion");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "Doctors",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ConsultationId",
                table: "Comments",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Consultations_ConsultationId",
                table: "Comments",
                column: "ConsultationId",
                principalTable: "Consultations",
                principalColumn: "Id");
        }
    }
}
