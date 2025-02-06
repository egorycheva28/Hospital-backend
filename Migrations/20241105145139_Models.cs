using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Laboratory_2.Migrations
{
    /// <inheritdoc />
    public partial class Models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultations_Inspections_inspectionId",
                table: "Consultations");

            migrationBuilder.DropIndex(
                name: "IX_Consultations_inspectionId",
                table: "Consultations");

            migrationBuilder.CreateTable(
                name: "InspectionCommentModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ParentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Content = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AuthorId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ModifyTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
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
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IncpectionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SpecialityId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RootCommentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CommentsNumber = table.Column<int>(type: "int", nullable: false),
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InspectionConsultationModel");

            migrationBuilder.DropTable(
                name: "InspectionCommentModel");

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_inspectionId",
                table: "Consultations",
                column: "inspectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultations_Inspections_inspectionId",
                table: "Consultations",
                column: "inspectionId",
                principalTable: "Inspections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
