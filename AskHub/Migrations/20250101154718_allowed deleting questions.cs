using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AskHub.Migrations
{
    public partial class alloweddeletingquestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Questions_AspNetUsers_DestinationAppUserId",
            //    table: "Questions");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Questions_AspNetUsers_SourceAppUserId",
            //    table: "Questions");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Questions_AspNetUsers_DestinationAppUserId",
            //    table: "Questions",
            //    column: "DestinationAppUserId",
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Questions_AspNetUsers_SourceAppUserId",
            //    table: "Questions",
            //    column: "SourceAppUserId",
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Questions_AspNetUsers_DestinationAppUserId",
            //    table: "Questions");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Questions_AspNetUsers_SourceAppUserId",
            //    table: "Questions");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Questions_AspNetUsers_DestinationAppUserId",
            //    table: "Questions",
            //    column: "DestinationAppUserId",
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Questions_AspNetUsers_SourceAppUserId",
            //    table: "Questions",
            //    column: "SourceAppUserId",
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }
    }
}
