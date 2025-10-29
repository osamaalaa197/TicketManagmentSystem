using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketManagementSystem.persistence.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderTotal = table.Column<int>(type: "int", nullable: false),
                    OrderPlaced = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderPaid = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArtistName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalPrice = table.Column<int>(type: "int", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "DateDeleted", "DateUpdated", "IsDeleted", "Name", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("6313179f-7837-473a-a4d5-a5571b43e6a6"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, false, "Musicals", null },
                    { new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, false, "Concerts", null },
                    { new Guid("bf3f3002-7e53-441e-8b76-f6280be284aa"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, false, "Plays", null },
                    { new Guid("fe98f549-e790-4e9f-aa16-18c2292a2ee9"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, false, "Conferences", null }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "DateDeleted", "DateUpdated", "IsDeleted", "OrderPaid", "OrderPlaced", "OrderTotal", "UpdatedBy", "UserId" },
                values: new object[,]
                {
                    { new Guid("3dcb3ea0-80b1-4781-b5c0-4d85c41e55a6"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, false, true, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 245, null, new Guid("4ad901be-f447-46dd-bcf7-dbe401afa203") },
                    { new Guid("771cca4b-066c-4ac7-b3df-4d12837fe7e0"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, false, true, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 85, null, new Guid("d97a15fc-0d32-41c6-9ddf-62f0735c4c1c") },
                    { new Guid("7e94bc5b-71a5-4c8c-bc3b-71bb7976237e"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, false, true, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 400, null, new Guid("a441eb40-9636-4ee6-be49-a66c5ec1330b") },
                    { new Guid("86d3a045-b42d-4854-8150-d6a374948b6e"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, false, true, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 135, null, new Guid("ac3cfaf5-34fd-4e4d-bc04-ad1083ddc340") },
                    { new Guid("ba0eb0ef-b69b-46fd-b8e2-41b4178ae7cb"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, false, true, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 116, null, new Guid("7aeb2c01-fe8e-4b84-a5ba-330bdf950f5c") },
                    { new Guid("e6a2679c-79a3-4ef1-a478-6f4c91b405b6"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, false, true, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 142, null, new Guid("7aeb2c01-fe8e-4b84-a5ba-330bdf950f5c") },
                    { new Guid("f5a6a3a0-4227-4973-abb5-a63fbe725923"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, false, true, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 40, null, new Guid("f5a6a3a0-4227-4973-abb5-a63fbe725923") }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "ArtistName", "CategoryId", "CreatedBy", "DateCreated", "DateDeleted", "DateUpdated", "Description", "EventDate", "ImageUrl", "IsDeleted", "Name", "TotalPrice", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("1babd057-e980-4cb3-9cd2-7fdd9e525668"), "Many", new Guid("fe98f549-e790-4e9f-aa16-18c2292a2ee9"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "The best tech conference in the world", new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), "https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/conf.jpg", false, "Techorama 2021", 400, null },
                    { new Guid("3448d5a4-0f72-4dd7-bf15-c14a46b26c00"), "Michael Johnson", new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Michael Johnson doesn't need an introduction. His 25 concert across the globe last year were seen by thousands. Can we add you to the list?", new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), "https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/michael.jpg", false, "The State of Affairs: Michael Live!", 85, null },
                    { new Guid("62787623-4c52-43fe-b0c9-b7044fb5929b"), "Manuel Santinonisi", new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Get on the hype of Spanish Guitar concerts with Manuel.", new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/guitar.jpg", false, "Spanish guitar hits with Manuel", 25, null },
                    { new Guid("adc42c09-08c1-4d2c-9f96-2d15bb1af299"), "Nick Sailor", new Guid("6313179f-7837-473a-a4d5-a5571b43e6a6"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "The critics are over the moon and so will you after you've watched this sing and dance extravaganza written by Nick Sailor, the man from 'My dad and sister'.", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/musical.jpg", false, "To the Moon and Back", 135, null },
                    { new Guid("b419a7ca-3321-4f38-be8e-4d7b6a529319"), "DJ 'The Mike'", new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "DJs from all over the world will compete in this epic battle for eternal fame.", new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/dj.jpg", false, "Clash of the DJs", 85, null },
                    { new Guid("ee272f8b-6096-4cb6-8625-bb4bb2d89e8b"), "John Egbert", new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Join John for his farwell tour across 15 continents. John really needs no introduction since he has already mesmerized the world with his banjo.", new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), "https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/banjo.jpg", false, "John Egbert Live", 65, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_CategoryId",
                table: "Events",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
