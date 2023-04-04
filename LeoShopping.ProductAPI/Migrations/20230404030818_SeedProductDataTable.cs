using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeoShopping.ProductAPI.Migrations
{
    public partial class SeedProductDataTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "product",
                columns: new[] { "id", "category_name", "description", "image_url", "name", "price" },
                values: new object[] { 3L, "T-shirt", "Mussum Ipsum, cacilds vidis litro abertis.", "https://github.com/leandro-SI/microservices-dotNet6/blob/main/LeoShopping.ProductAPI/Imagens/6_spacex.jpg?raw=true", "Camiseta Spacex", 69.9m });

            migrationBuilder.InsertData(
                table: "product",
                columns: new[] { "id", "category_name", "description", "image_url", "name", "price" },
                values: new object[] { 4L, "T-shirt", "Mussum Ipsum, cacilds vidis litro abertis.", "https://github.com/leandro-SI/microservices-dotNet6/blob/main/LeoShopping.ProductAPI/Imagens/12_gnu_linux.jpg?raw=true", "Camiseta GNU Linux", 70.5m });

            migrationBuilder.InsertData(
                table: "product",
                columns: new[] { "id", "category_name", "description", "image_url", "name", "price" },
                values: new object[] { 5L, "T-shirt", "Mussum Ipsum, cacilds vidis litro abertis.", "https://github.com/leandro-SI/microservices-dotNet6/blob/main/LeoShopping.ProductAPI/Imagens/7_coffee.jpg?raw=true", "Camiseta Coffee", 39.5m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "id",
                keyValue: 5L);
        }
    }
}
