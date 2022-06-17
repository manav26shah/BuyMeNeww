using Microsoft.EntityFrameworkCore.Migrations;

namespace BuyMe.DL.Migrations
{
    public partial class addpincodes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pincodes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    city = table.Column<string>(nullable: true),
                    deliverydays = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pincodes", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DiscountPercentage",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DiscountPercentage", "Image", "MRPAmount", "Name" },
                values: new object[] { 0, "https://rukminim1.flixcart.com/image/832/832/l3929ow0/mobile/v/n/s/-original-imageewzeguggzvc.jpeg?q=70", 79999.0, "Vivo X80 Pro" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DiscountPercentage", "Image", "Name" },
                values: new object[] { 0, "https://m.media-amazon.com/images/I/71F4jU7MRUS._SL1500_.jpg", "Samsung M32" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DiscountPercentage", "Image" },
                values: new object[] { 0, "https://www.apple.com/newsroom/images/product/iphone/standard/Apple_iPhone-13-Pro_iPhone-13-Pro-Max_09142021_inline.jpg.large.jpg" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pincodes");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DiscountPercentage",
                value: 10);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DiscountPercentage", "Image", "MRPAmount", "Name" },
                values: new object[] { 10, "vivo1.png", 15999.0, "Vivo X" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DiscountPercentage", "Image", "Name" },
                values: new object[] { 10, "samsung1.png", "Samsung M31" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DiscountPercentage", "Image" },
                values: new object[] { 10, "IPhone1.png" });
        }
    }
}
