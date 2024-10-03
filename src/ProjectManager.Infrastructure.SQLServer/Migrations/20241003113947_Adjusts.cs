using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManager.Infrastructure.SQLServer.Migrations
{
    /// <inheritdoc />
    public partial class Adjusts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTaskHistory_ProjectTasks_ProjectTaskId",
                table: "ProjectTaskHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTaskHistory_Users_UserId",
                table: "ProjectTaskHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectTaskHistory",
                table: "ProjectTaskHistory");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "ProjectTasks");

            migrationBuilder.RenameTable(
                name: "ProjectTaskHistory",
                newName: "ProjectTaskHistories");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectTaskHistory_UserId",
                table: "ProjectTaskHistories",
                newName: "IX_ProjectTaskHistories_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectTaskHistory_ProjectTaskId",
                table: "ProjectTaskHistories",
                newName: "IX_ProjectTaskHistories_ProjectTaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectTaskHistories",
                table: "ProjectTaskHistories",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProjectTaskComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProjectTaskId = table.Column<int>(type: "int", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTaskComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectTaskComments_ProjectTasks_ProjectTaskId",
                        column: x => x.ProjectTaskId,
                        principalTable: "ProjectTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectTaskComments_Users_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTaskComments_CreatedUserId",
                table: "ProjectTaskComments",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTaskComments_ProjectTaskId",
                table: "ProjectTaskComments",
                column: "ProjectTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTaskHistories_ProjectTasks_ProjectTaskId",
                table: "ProjectTaskHistories",
                column: "ProjectTaskId",
                principalTable: "ProjectTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTaskHistories_Users_UserId",
                table: "ProjectTaskHistories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTaskHistories_ProjectTasks_ProjectTaskId",
                table: "ProjectTaskHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTaskHistories_Users_UserId",
                table: "ProjectTaskHistories");

            migrationBuilder.DropTable(
                name: "ProjectTaskComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectTaskHistories",
                table: "ProjectTaskHistories");

            migrationBuilder.RenameTable(
                name: "ProjectTaskHistories",
                newName: "ProjectTaskHistory");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectTaskHistories_UserId",
                table: "ProjectTaskHistory",
                newName: "IX_ProjectTaskHistory_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectTaskHistories_ProjectTaskId",
                table: "ProjectTaskHistory",
                newName: "IX_ProjectTaskHistory_ProjectTaskId");

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "ProjectTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectTaskHistory",
                table: "ProjectTaskHistory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTaskHistory_ProjectTasks_ProjectTaskId",
                table: "ProjectTaskHistory",
                column: "ProjectTaskId",
                principalTable: "ProjectTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTaskHistory_Users_UserId",
                table: "ProjectTaskHistory",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
