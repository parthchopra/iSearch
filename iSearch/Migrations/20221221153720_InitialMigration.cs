using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iSearch.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CollectionEntities",
                columns: table => new
                {
                    CollectionId = table.Column<long>(type: "bigint", nullable: false),
                    ClickCount = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionEntities", x => x.CollectionId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollectionEntities");
        }
    }
}
