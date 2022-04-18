using Microsoft.EntityFrameworkCore.Migrations;

namespace BuyMe.DL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    MRPAmount = table.Column<decimal>(nullable: false),
                    DiscountPercentage = table.Column<int>(nullable: false),
                    InStock = table.Column<bool>(nullable: false),
                    MaxOrderAmount = table.Column<int>(nullable: false),
                    InventoryId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DiscountPercentage", "Image", "InStock", "InventoryId", "MRPAmount", "MaxOrderAmount", "Name" },
                values: new object[,]
                {
                    { 1, 10, "oppo1.png", true, "Mob-oppo-1", 15999m, 3, "Oppo Reno 6" },
                    { 2, 10, "vivo1.png", true, "Mob-vivo-1", 15999m, 3, "Vivo X" },
                    { 3, 10, "samsung1.png", true, "Mob-Sam-1", 15999m, 3, "Samsung M31" },
                    { 4, 10, "IPhone1.png", true, "Mob-iphone-1", 15999m, 3, "Iphone 13 Max pro" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
