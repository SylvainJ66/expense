using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lucca.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "first_name", "name", "currency" },
                values: new object[] { "f7f04793-c99e-42b0-85bf-3fb7ba6bf6d7", "Anthony", "Stark", "USD" });
            
            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "first_name", "name", "currency" },
                values: new object[] { "df5f2e19-7023-480e-ad50-9cfb4f22231f", "Natasha", "Romanova", "RUB" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValues: new object[] { "f7f04793-c99e-42b0-85bf-3fb7ba6bf6d7" });
            
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValues: new object[] { "df5f2e19-7023-480e-ad50-9cfb4f22231f" });
        }
    }
}
