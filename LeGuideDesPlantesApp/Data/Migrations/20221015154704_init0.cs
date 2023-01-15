using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeGuideDesPlantesApp.Data.Migrations
{
    public partial class init0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            _ = migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            _ = migrationBuilder.AddColumn<string>(
                name: "Adresse",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            _ = migrationBuilder.AddColumn<string>(
                name: "Nom",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            _ = migrationBuilder.AddColumn<string>(
                name: "Pays",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            _ = migrationBuilder.AddColumn<string>(
                name: "Ville",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            _ = migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            _ = migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            _ = migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "AspNetRoles",
                type: "int",
                nullable: true);

            _ = migrationBuilder.CreateTable(
                name: "ApplicationRoleApplicationUser",
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_ApplicationRoleApplicationUser", x => new { x.RoleId, x.UserId });
                    _ = table.ForeignKey(
                        name: "FK_ApplicationRoleApplicationUser_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    _ = table.ForeignKey(
                        name: "FK_ApplicationRoleApplicationUser_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            _ = migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HuilesEssentielId = table.Column<int>(type: "int", nullable: true),
                    ArbresId = table.Column<int>(type: "int", nullable: true),
                    PlantesAromatiquesId = table.Column<int>(type: "int", nullable: true),
                    PlantesSauvagesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_Categories", x => x.Id);
                });

            _ = migrationBuilder.CreateTable(
                name: "Pays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomPays = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HuilesEssentielId = table.Column<int>(type: "int", nullable: true),
                    PlantesAromatiquesId = table.Column<int>(type: "int", nullable: true),
                    PlantesSauvagesId = table.Column<int>(type: "int", nullable: true),
                    ArbresId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_Pays", x => x.Id);
                });

            _ = migrationBuilder.CreateTable(
                name: "PlantesAromatiques",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Habitat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BienFait = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Culture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Taille = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rusticite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maladies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeriodeDeFleuraison = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Arosage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Voisinage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrefenrenceTerrain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Entretiens = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecetteSimple = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SearchString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategorieId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_PlantesAromatiques", x => x.Id);
                    _ = table.ForeignKey(
                        name: "FK_PlantesAromatiques_Categories_CategorieId",
                        column: x => x.CategorieId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            _ = migrationBuilder.CreateTable(
                name: "PlantesSauvages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Habitat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Danger = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BienFait = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Culture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Taille = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rusticite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maladies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeriodeDeFleuraison = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Arosage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Voisinage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrefenrenceTerrain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Entretiens = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecetteSimple = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SearchString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategorieId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_PlantesSauvages", x => x.Id);
                    _ = table.ForeignKey(
                        name: "FK_PlantesSauvages_Categories_CategorieId",
                        column: x => x.CategorieId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            _ = migrationBuilder.CreateTable(
                name: "PaysPlantesAromatiques",
                columns: table => new
                {
                    PaysId = table.Column<int>(type: "int", nullable: false),
                    PlantesAromatiquesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_PaysPlantesAromatiques", x => new { x.PaysId, x.PlantesAromatiquesId });
                    _ = table.ForeignKey(
                        name: "FK_PaysPlantesAromatiques_Pays_PaysId",
                        column: x => x.PaysId,
                        principalTable: "Pays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    _ = table.ForeignKey(
                        name: "FK_PaysPlantesAromatiques_PlantesAromatiques_PlantesAromatiquesId",
                        column: x => x.PlantesAromatiquesId,
                        principalTable: "PlantesAromatiques",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            _ = migrationBuilder.CreateTable(
                name: "HuilesEssentiel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomLatin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Famille = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MethodeExtraction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComposantPrincipal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Conservation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProprietePrincipal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionSurLeCorps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Circulation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Digestion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MusclesEtArticulations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeauEtCheveux = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProblemesFeminins = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrossesEtEnfants = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SensualitePourCouples = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Precaution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlantesSauvagesId = table.Column<int>(type: "int", nullable: true),
                    PlantesAromatiquesId = table.Column<int>(type: "int", nullable: true),
                    SearchString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategorieId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_HuilesEssentiel", x => x.Id);
                    _ = table.ForeignKey(
                        name: "FK_HuilesEssentiel_Categories_CategorieId",
                        column: x => x.CategorieId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    _ = table.ForeignKey(
                        name: "FK_HuilesEssentiel_PlantesAromatiques_PlantesAromatiquesId",
                        column: x => x.PlantesAromatiquesId,
                        principalTable: "PlantesAromatiques",
                        principalColumn: "Id");
                    _ = table.ForeignKey(
                        name: "FK_HuilesEssentiel_PlantesSauvages_PlantesSauvagesId",
                        column: x => x.PlantesSauvagesId,
                        principalTable: "PlantesSauvages",
                        principalColumn: "Id");
                });

            _ = migrationBuilder.CreateTable(
                name: "PaysPlantesSauvages",
                columns: table => new
                {
                    PaysId = table.Column<int>(type: "int", nullable: false),
                    PlantesSauvagesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_PaysPlantesSauvages", x => new { x.PaysId, x.PlantesSauvagesId });
                    _ = table.ForeignKey(
                        name: "FK_PaysPlantesSauvages_Pays_PaysId",
                        column: x => x.PaysId,
                        principalTable: "Pays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    _ = table.ForeignKey(
                        name: "FK_PaysPlantesSauvages_PlantesSauvages_PlantesSauvagesId",
                        column: x => x.PlantesSauvagesId,
                        principalTable: "PlantesSauvages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            _ = migrationBuilder.CreateTable(
                name: "Arbres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Habitat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Danger = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BienFait = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Culture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Taille = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rusticite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maladies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeriodeDeFleuraison = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Arosage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Voisinage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrefenrenceTerrain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Entretiens = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HuilesEssentielId = table.Column<int>(type: "int", nullable: true),
                    SearchString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategorieId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_Arbres", x => x.Id);
                    _ = table.ForeignKey(
                        name: "FK_Arbres_Categories_CategorieId",
                        column: x => x.CategorieId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    _ = table.ForeignKey(
                        name: "FK_Arbres_HuilesEssentiel_HuilesEssentielId",
                        column: x => x.HuilesEssentielId,
                        principalTable: "HuilesEssentiel",
                        principalColumn: "Id");
                });

            _ = migrationBuilder.CreateTable(
                name: "HuilesEssentielPays",
                columns: table => new
                {
                    HuilesEssentielsId = table.Column<int>(type: "int", nullable: false),
                    PaysId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_HuilesEssentielPays", x => new { x.HuilesEssentielsId, x.PaysId });
                    _ = table.ForeignKey(
                        name: "FK_HuilesEssentielPays_HuilesEssentiel_HuilesEssentielsId",
                        column: x => x.HuilesEssentielsId,
                        principalTable: "HuilesEssentiel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    _ = table.ForeignKey(
                        name: "FK_HuilesEssentielPays_Pays_PaysId",
                        column: x => x.PaysId,
                        principalTable: "Pays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            _ = migrationBuilder.CreateTable(
                name: "ArbresPays",
                columns: table => new
                {
                    ArbresId = table.Column<int>(type: "int", nullable: false),
                    PaysId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_ArbresPays", x => new { x.ArbresId, x.PaysId });
                    _ = table.ForeignKey(
                        name: "FK_ArbresPays_Arbres_ArbresId",
                        column: x => x.ArbresId,
                        principalTable: "Arbres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    _ = table.ForeignKey(
                        name: "FK_ArbresPays_Pays_PaysId",
                        column: x => x.PaysId,
                        principalTable: "Pays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            _ = migrationBuilder.CreateIndex(
                name: "IX_ApplicationRoleApplicationUser_UserId",
                table: "ApplicationRoleApplicationUser",
                column: "UserId");

            _ = migrationBuilder.CreateIndex(
                name: "IX_Arbres_CategorieId",
                table: "Arbres",
                column: "CategorieId");

            _ = migrationBuilder.CreateIndex(
                name: "IX_Arbres_HuilesEssentielId",
                table: "Arbres",
                column: "HuilesEssentielId");

            _ = migrationBuilder.CreateIndex(
                name: "IX_ArbresPays_PaysId",
                table: "ArbresPays",
                column: "PaysId");

            _ = migrationBuilder.CreateIndex(
                name: "IX_HuilesEssentiel_CategorieId",
                table: "HuilesEssentiel",
                column: "CategorieId");

            _ = migrationBuilder.CreateIndex(
                name: "IX_HuilesEssentiel_PlantesAromatiquesId",
                table: "HuilesEssentiel",
                column: "PlantesAromatiquesId",
                unique: true,
                filter: "[PlantesAromatiquesId] IS NOT NULL");

            _ = migrationBuilder.CreateIndex(
                name: "IX_HuilesEssentiel_PlantesSauvagesId",
                table: "HuilesEssentiel",
                column: "PlantesSauvagesId",
                unique: true,
                filter: "[PlantesSauvagesId] IS NOT NULL");

            _ = migrationBuilder.CreateIndex(
                name: "IX_HuilesEssentielPays_PaysId",
                table: "HuilesEssentielPays",
                column: "PaysId");

            _ = migrationBuilder.CreateIndex(
                name: "IX_PaysPlantesAromatiques_PlantesAromatiquesId",
                table: "PaysPlantesAromatiques",
                column: "PlantesAromatiquesId");

            _ = migrationBuilder.CreateIndex(
                name: "IX_PaysPlantesSauvages_PlantesSauvagesId",
                table: "PaysPlantesSauvages",
                column: "PlantesSauvagesId");

            _ = migrationBuilder.CreateIndex(
                name: "IX_PlantesAromatiques_CategorieId",
                table: "PlantesAromatiques",
                column: "CategorieId");

            _ = migrationBuilder.CreateIndex(
                name: "IX_PlantesSauvages_CategorieId",
                table: "PlantesSauvages",
                column: "CategorieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.DropTable(
                name: "ApplicationRoleApplicationUser");

            _ = migrationBuilder.DropTable(
                name: "ArbresPays");

            _ = migrationBuilder.DropTable(
                name: "HuilesEssentielPays");

            _ = migrationBuilder.DropTable(
                name: "PaysPlantesAromatiques");

            _ = migrationBuilder.DropTable(
                name: "PaysPlantesSauvages");

            _ = migrationBuilder.DropTable(
                name: "Arbres");

            _ = migrationBuilder.DropTable(
                name: "Pays");

            _ = migrationBuilder.DropTable(
                name: "HuilesEssentiel");

            _ = migrationBuilder.DropTable(
                name: "PlantesAromatiques");

            _ = migrationBuilder.DropTable(
                name: "PlantesSauvages");

            _ = migrationBuilder.DropTable(
                name: "Categories");

            _ = migrationBuilder.DropColumn(
                name: "Adresse",
                table: "AspNetUsers");

            _ = migrationBuilder.DropColumn(
                name: "Nom",
                table: "AspNetUsers");

            _ = migrationBuilder.DropColumn(
                name: "Pays",
                table: "AspNetUsers");

            _ = migrationBuilder.DropColumn(
                name: "Ville",
                table: "AspNetUsers");

            _ = migrationBuilder.DropColumn(
                name: "UserId",
                table: "AspNetRoles");

            _ = migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            _ = migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            _ = migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            _ = migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
