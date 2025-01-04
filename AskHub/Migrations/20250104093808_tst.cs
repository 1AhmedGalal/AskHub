using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AskHub.Migrations
{
    public partial class tst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "CreatedDate",
            //    table: "Answers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<DateTime>(
            //    name: "CreatedDate",
            //    table: "Answers",
            //    type: "datetime2",
            //    nullable: false,
            //    defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
