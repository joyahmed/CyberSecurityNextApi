using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CyberSecurityNextApi.Migrations
{
    /// <inheritdoc />
    public partial class PostAndTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    PostDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostTitle = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleonSlide = table.Column<int>(type: "int", nullable: false),
                    Subtitle = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThumbImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SlideImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentFileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShowInHomePage = table.Column<int>(type: "int", nullable: false),
                    PublishStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tag_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Post_CategoryId",
                table: "Post",
                column: "CategoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Post_PostTitle",
                table: "Post",
                column: "PostTitle",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Post_Subtitle",
                table: "Post",
                column: "Subtitle",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tag_PostId",
                table: "Tag",
                column: "PostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Post");
        }
    }
}
