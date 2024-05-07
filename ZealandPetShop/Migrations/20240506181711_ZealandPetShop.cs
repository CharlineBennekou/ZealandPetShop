using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZealandPetShop.Migrations
{
    /// <inheritdoc />
    public partial class ZealandPetShop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Art",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Art",
                table: "Items");
        }
    }
}
