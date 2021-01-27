using Microsoft.EntityFrameworkCore.Migrations;
#pragma warning disable 1591

namespace Db.Infrastructure.Migrations.Manual
{
    public partial class ManualInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carburetors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carburetors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormatTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormatTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Makes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Makes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PartNumber = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    MakersPartNumber = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    Unit = table.Column<string>(type: "TEXT", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BookId = table.Column<int>(type: "INTEGER", nullable: false),
                    SectionHeader = table.Column<string>(type: "TEXT", nullable: true),
                    StartPage = table.Column<int>(type: "INTEGER", nullable: false),
                    EndPage = table.Column<int>(type: "INTEGER", nullable: false),
                    FigureNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    FigureDescription = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Engines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Displacement = table.Column<short>(type: "INTEGER", nullable: false),
                    Carburetors = table.Column<short>(type: "INTEGER", nullable: false),
                    CarburetorId = table.Column<int>(type: "INTEGER", nullable: false),
                    Transmission = table.Column<short>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Engines_Carburetors_CarburetorId",
                        column: x => x.CarburetorId,
                        principalTable: "Carburetors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    PartId = table.Column<int>(type: "INTEGER", nullable: false),
                    PropertyName = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                    PropertyTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    FormatTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    PropertyValue = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => new { x.PartId, x.PropertyName });
                    table.ForeignKey(
                        name: "FK_Properties_FormatTypes_FormatTypeId",
                        column: x => x.FormatTypeId,
                        principalTable: "FormatTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_PropertyTypes_PropertyTypeId",
                        column: x => x.PropertyTypeId,
                        principalTable: "PropertyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SectionParts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SectionId = table.Column<int>(type: "INTEGER", nullable: false),
                    PartId = table.Column<int>(type: "INTEGER", nullable: false),
                    Reference = table.Column<int>(type: "INTEGER", nullable: true),
                    PageNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    AdditionalInfo = table.Column<string>(type: "TEXT", nullable: true),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Remarks = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SectionParts_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SectionParts_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BookId = table.Column<int>(type: "INTEGER", nullable: true),
                    MakeId = table.Column<int>(type: "INTEGER", nullable: false),
                    EngineId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Models_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Models_Engines_EngineId",
                        column: x => x.EngineId,
                        principalTable: "Engines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Models_Makes_MakeId",
                        column: x => x.MakeId,
                        principalTable: "Makes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImagePoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SectionPartsId = table.Column<int>(type: "INTEGER", nullable: false),
                    PositionX = table.Column<double>(type: "REAL", nullable: false),
                    PositionY = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagePoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImagePoints_SectionParts_SectionPartsId",
                        column: x => x.SectionPartsId,
                        principalTable: "SectionParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SectionPartParents",
                columns: table => new
                {
                    ParentId = table.Column<int>(type: "INTEGER", nullable: false),
                    ChildId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionPartParents", x => new { x.ParentId, x.ChildId });
                    table.ForeignKey(
                        name: "FK_SectionPartParents_SectionParts_ChildId",
                        column: x => x.ChildId,
                        principalTable: "SectionParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SectionPartParents_SectionParts_ParentId",
                        column: x => x.ParentId,
                        principalTable: "SectionParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SectionModels",
                columns: table => new
                {
                    SectionId = table.Column<int>(type: "INTEGER", nullable: false),
                    ModelId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionModels", x => new { x.SectionId, x.ModelId });
                    table.ForeignKey(
                        name: "FK_SectionModels_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SectionModels_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Engines_CarburetorId",
                table: "Engines",
                column: "CarburetorId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagePoints_SectionPartsId",
                table: "ImagePoints",
                column: "SectionPartsId");

            migrationBuilder.CreateIndex(
                name: "IX_Models_BookId",
                table: "Models",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Models_EngineId",
                table: "Models",
                column: "EngineId");

            migrationBuilder.CreateIndex(
                name: "IX_Models_MakeId",
                table: "Models",
                column: "MakeId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_FormatTypeId",
                table: "Properties",
                column: "FormatTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_PropertyTypeId",
                table: "Properties",
                column: "PropertyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionModels_ModelId",
                table: "SectionModels",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionPartParents_ChildId",
                table: "SectionPartParents",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionParts_PartId",
                table: "SectionParts",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionParts_SectionId",
                table: "SectionParts",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_BookId",
                table: "Sections",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImagePoints");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "SectionModels");

            migrationBuilder.DropTable(
                name: "SectionPartParents");

            migrationBuilder.DropTable(
                name: "FormatTypes");

            migrationBuilder.DropTable(
                name: "PropertyTypes");

            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.DropTable(
                name: "SectionParts");

            migrationBuilder.DropTable(
                name: "Engines");

            migrationBuilder.DropTable(
                name: "Makes");

            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "Carburetors");

            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
