using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Laboratory_2.Migrations
{
    /// <inheritdoc />
    public partial class Inspection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultations_Comments_CommentId",
                table: "Consultations");

            migrationBuilder.DropTable(
                name: "InspectionConsultationModel");

            migrationBuilder.DropTable(
                name: "InspectionCommentModel");

            migrationBuilder.DropIndex(
                name: "IX_Consultations_CommentId",
                table: "Consultations");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "Consultations");

            migrationBuilder.RenameColumn(
                name: "inspectionId",
                table: "Consultations",
                newName: "InspectionId");

            migrationBuilder.AlterColumn<Guid>(
                name: "PreviousInspectionId",
                table: "Inspections",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NextVisitDate",
                table: "Inspections",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeathDate",
                table: "Inspections",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<Guid>(
                name: "BaseInspectionId",
                table: "Inspections",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentId",
                table: "Comments",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Comments",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddColumn<Guid>(
                name: "ConsultationId",
                table: "Comments",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_InspectionId",
                table: "Consultations",
                column: "InspectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ConsultationId",
                table: "Comments",
                column: "ConsultationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Consultations_ConsultationId",
                table: "Comments",
                column: "ConsultationId",
                principalTable: "Consultations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultations_Inspections_InspectionId",
                table: "Consultations",
                column: "InspectionId",
                principalTable: "Inspections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Consultations_ConsultationId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Consultations_Inspections_InspectionId",
                table: "Consultations");

            migrationBuilder.DropIndex(
                name: "IX_Consultations_InspectionId",
                table: "Consultations");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ConsultationId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ConsultationId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "InspectionId",
                table: "Consultations",
                newName: "inspectionId");

            migrationBuilder.AlterColumn<Guid>(
                name: "PreviousInspectionId",
                table: "Inspections",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NextVisitDate",
                table: "Inspections",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeathDate",
                table: "Inspections",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "BaseInspectionId",
                table: "Inspections",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "CommentId",
                table: "Consultations",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentId",
                table: "Comments",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Comments",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "InspectionCommentModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AuthorId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Content = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ParentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectionCommentModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InspectionCommentModel_Doctors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "InspectionConsultationModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RootCommentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SpecialityId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CommentsNumber = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IncpectionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    InspectionId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectionConsultationModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InspectionConsultationModel_InspectionCommentModel_RootComme~",
                        column: x => x.RootCommentId,
                        principalTable: "InspectionCommentModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InspectionConsultationModel_Inspections_InspectionId",
                        column: x => x.InspectionId,
                        principalTable: "Inspections",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InspectionConsultationModel_Specialties_SpecialityId",
                        column: x => x.SpecialityId,
                        principalTable: "Specialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_CommentId",
                table: "Consultations",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_InspectionCommentModel_AuthorId",
                table: "InspectionCommentModel",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_InspectionConsultationModel_InspectionId",
                table: "InspectionConsultationModel",
                column: "InspectionId");

            migrationBuilder.CreateIndex(
                name: "IX_InspectionConsultationModel_RootCommentId",
                table: "InspectionConsultationModel",
                column: "RootCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_InspectionConsultationModel_SpecialityId",
                table: "InspectionConsultationModel",
                column: "SpecialityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultations_Comments_CommentId",
                table: "Consultations",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
