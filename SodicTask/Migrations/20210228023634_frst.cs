using Microsoft.EntityFrameworkCore.Migrations;

namespace SodicTask.Migrations
{
    public partial class frst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CatagNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentNo = table.Column<int>(type: "int", nullable: false),
                    LevelNo = table.Column<short>(type: "smallint", nullable: false),
                    CatagName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Colors = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Width = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAvailable = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CatagNo);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProdId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ProdDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CatagNo = table.Column<int>(type: "int", nullable: false),
                    SubCatag = table.Column<int>(type: "int", nullable: false),
                    ProdMainImg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    categoryCatagNo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProdId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_categoryCatagNo",
                        column: x => x.categoryCatagNo,
                        principalTable: "Categories",
                        principalColumn: "CatagNo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductImags",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdId = table.Column<int>(type: "int", nullable: false),
                    ProdImages = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductProdId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImags", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_ProductImags_Products_ProductProdId",
                        column: x => x.ProductProdId,
                        principalTable: "Products",
                        principalColumn: "ProdId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductImags_ProductProdId",
                table: "ProductImags",
                column: "ProductProdId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_categoryCatagNo",
                table: "Products",
                column: "categoryCatagNo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImags");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
