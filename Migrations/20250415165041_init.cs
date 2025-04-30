using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EDStationDatabase.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Allegiances",
                columns: table => new
                {
                    AllegianceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AllegianceName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allegiances", x => x.AllegianceId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Economies",
                columns: table => new
                {
                    EconomyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EconomyName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Economies", x => x.EconomyId);
                });

            migrationBuilder.CreateTable(
                name: "StationType",
                columns: table => new
                {
                    StationTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StationTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StationType", x => x.StationTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Superpowers",
                columns: table => new
                {
                    SuperpowerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuperpowerName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Superpowers", x => x.SuperpowerId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StationBookmarks",
                columns: table => new
                {
                    StationBookmarkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EconomyId = table.Column<int>(type: "int", nullable: true),
                    AllegianceId = table.Column<int>(type: "int", nullable: true),
                    StationTypeId = table.Column<int>(type: "int", nullable: true),
                    SuperpowerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StationBookmarks", x => x.StationBookmarkId);
                    table.ForeignKey(
                        name: "FK_StationBookmarks_Allegiances_AllegianceId",
                        column: x => x.AllegianceId,
                        principalTable: "Allegiances",
                        principalColumn: "AllegianceId");
                    table.ForeignKey(
                        name: "FK_StationBookmarks_Economies_EconomyId",
                        column: x => x.EconomyId,
                        principalTable: "Economies",
                        principalColumn: "EconomyId");
                    table.ForeignKey(
                        name: "FK_StationBookmarks_StationType_StationTypeId",
                        column: x => x.StationTypeId,
                        principalTable: "StationType",
                        principalColumn: "StationTypeId");
                    table.ForeignKey(
                        name: "FK_StationBookmarks_Superpowers_SuperpowerId",
                        column: x => x.SuperpowerId,
                        principalTable: "Superpowers",
                        principalColumn: "SuperpowerId");
                });

            migrationBuilder.InsertData(
                table: "Allegiances",
                columns: new[] { "AllegianceId", "AllegianceName" },
                values: new object[,]
                {
                    { 1, "Aisling Duval" },
                    { 2, "Arissa Lavingy-Duval" },
                    { 3, "Denton Patreus" },
                    { 4, "Zemina Torval" },
                    { 5, "Jerome Archer" },
                    { 6, "Felicia Winters" },
                    { 7, "Edmund Mahon" },
                    { 8, "Nakato Kaine" },
                    { 9, "Archon Delaine" },
                    { 10, "Li Yong-Rui" },
                    { 11, "Parnav Antal" },
                    { 12, "Yuri Grom" },
                    { 13, "Independent" },
                    { 14, "Pilot's Federation" }
                });

            migrationBuilder.InsertData(
                table: "Economies",
                columns: new[] { "EconomyId", "EconomyName" },
                values: new object[,]
                {
                    { 1, "Agriculture" },
                    { 2, "Colony" },
                    { 3, "Extraction" },
                    { 4, "High Tech" },
                    { 5, "Industrial" },
                    { 6, "Military" },
                    { 7, "Refinery" },
                    { 8, "Service" },
                    { 9, "Terraforming" },
                    { 10, "Tourism" },
                    { 11, "Prison Colony" }
                });

            migrationBuilder.InsertData(
                table: "StationType",
                columns: new[] { "StationTypeId", "StationTypeName" },
                values: new object[,]
                {
                    { 1, "Coriolis" },
                    { 2, "Orbis" },
                    { 3, "Ocellus" },
                    { 4, "Outpost" },
                    { 5, "Planetary Outpost" },
                    { 6, "Planetary Port" },
                    { 7, "Planetary Surface Port" },
                    { 8, "Planetary Settlement" },
                    { 9, "Asteroid Base" }
                });

            migrationBuilder.InsertData(
                table: "Superpowers",
                columns: new[] { "SuperpowerId", "SuperpowerName" },
                values: new object[,]
                {
                    { 1, "Empire" },
                    { 2, "Federation" },
                    { 3, "Alliance" },
                    { 4, "Independent" }
                });

            migrationBuilder.InsertData(
                table: "StationBookmarks",
                columns: new[] { "StationBookmarkId", "AllegianceId", "EconomyId", "StationName", "StationTypeId", "SuperpowerId", "SystemName" },
                values: new object[,]
                {
                    { 1, 13, 7, "Low City", 3, null, "HR 1980" },
                    { 2, 10, 4, "Ray Gateway", 1, null, "Diaguandri" },
                    { 3, 13, 6, "Copernicus Observatory", 1, null, "Asterope" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StationBookmarks_AllegianceId",
                table: "StationBookmarks",
                column: "AllegianceId");

            migrationBuilder.CreateIndex(
                name: "IX_StationBookmarks_EconomyId",
                table: "StationBookmarks",
                column: "EconomyId");

            migrationBuilder.CreateIndex(
                name: "IX_StationBookmarks_StationTypeId",
                table: "StationBookmarks",
                column: "StationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StationBookmarks_SuperpowerId",
                table: "StationBookmarks",
                column: "SuperpowerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "StationBookmarks");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Allegiances");

            migrationBuilder.DropTable(
                name: "Economies");

            migrationBuilder.DropTable(
                name: "StationType");

            migrationBuilder.DropTable(
                name: "Superpowers");
        }
    }
}
