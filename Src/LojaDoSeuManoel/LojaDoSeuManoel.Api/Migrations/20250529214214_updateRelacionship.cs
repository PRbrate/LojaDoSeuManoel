using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaDoSeuManoel.Api.Migrations
{
    /// <inheritdoc />
    public partial class updateRelacionship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Requested_RequestedId",
                schema: "dbo",
                table: "Products");

            migrationBuilder.AlterColumn<Guid>(
                name: "RequestedId",
                schema: "dbo",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Requested_RequestedId",
                schema: "dbo",
                table: "Products",
                column: "RequestedId",
                principalSchema: "dbo",
                principalTable: "Requested",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Requested_RequestedId",
                schema: "dbo",
                table: "Products");

            migrationBuilder.AlterColumn<Guid>(
                name: "RequestedId",
                schema: "dbo",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Requested_RequestedId",
                schema: "dbo",
                table: "Products",
                column: "RequestedId",
                principalSchema: "dbo",
                principalTable: "Requested",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
