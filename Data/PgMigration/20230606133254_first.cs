using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Advocate.Data.PgMigration
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mst_Act",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    ActCategory = table.Column<string>(type: "varchar(10)", nullable: true),
                    ActTypeId = table.Column<int>(type: "int", nullable: false),
                    ActNumber = table.Column<int>(type: "int", nullable: false),
                    SubActNumber = table.Column<string>(type: "varchar(3)", nullable: false),
                    ActYear = table.Column<int>(type: "int", nullable: false),
                    AssentBy = table.Column<string>(type: "varchar(100)", nullable: true),
                    AssentDate = table.Column<DateTime>(type: "date", nullable: true),
                    ActName = table.Column<string>(type: "varchar(250)", nullable: true),
                    PublishedIn = table.Column<int>(type: "int", nullable: false),
                    PartId = table.Column<int>(type: "int", nullable: false),
                    Nature = table.Column<string>(type: "varchar(10)", nullable: true),
                    GazetteDate = table.Column<DateTime>(type: "date", nullable: true),
                    PageNo = table.Column<int>(type: "int", nullable: true),
                    ComeInforce = table.Column<string>(type: "varchar(10)", nullable: true),
                    SubjectAct = table.Column<string>(type: "varchar(100)", nullable: true),
                    PublishedInGazeteDate = table.Column<DateTime>(type: "date", nullable: true),
                    UserID = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_Act", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mst_ActTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ActType = table.Column<string>(type: "varchar(100)", nullable: true),
                    UserID = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_ActTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mst_AmendedNotification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NotificationId = table.Column<int>(type: "integer", nullable: false),
                    AmendedNotificationID = table.Column<int>(type: "integer", nullable: false),
                    UserID = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_AmendedNotification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mst_AmendedRule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RuleId = table.Column<int>(type: "integer", nullable: false),
                    AmendedRuleID = table.Column<int>(type: "integer", nullable: false),
                    UserID = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_AmendedRule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mst_BookEntryDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    BookYear = table.Column<int>(type: "int", nullable: false),
                    BookVolume = table.Column<string>(type: "varchar(20)", nullable: true),
                    BookPageNo = table.Column<string>(type: "varchar(10)", nullable: true),
                    BookSerialNo = table.Column<int>(type: "int", nullable: false),
                    DateType = table.Column<string>(type: "varchar(20)", nullable: true),
                    GazetteDate = table.Column<DateTime>(type: "date", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    LegislativeNature = table.Column<string>(type: "varchar(20)", nullable: true),
                    TallyType = table.Column<string>(type: "varchar(20)", nullable: true),
                    UserID = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_BookEntryDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mst_Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BookName = table.Column<string>(type: "varchar(100)", nullable: true),
                    ShortName = table.Column<string>(type: "varchar(30)", nullable: true),
                    UserID = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mst_Gazette",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GazetteName = table.Column<string>(type: "varchar(150)", nullable: true),
                    UserID = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_Gazette", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mst_NavigationMenus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MenuName = table.Column<string>(type: "varchar(100)", nullable: true),
                    MenuCode = table.Column<string>(type: "varchar(50)", nullable: true),
                    Area = table.Column<string>(type: "varchar(100)", nullable: true),
                    Controller = table.Column<string>(type: "varchar(100)", nullable: true),
                    ActionName = table.Column<string>(type: "varchar(50)", nullable: true),
                    IsExternal = table.Column<bool>(type: "bool", nullable: false),
                    ExternalUrl = table.Column<string>(type: "text", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_NavigationMenus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mst_Notification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    ActTypeId = table.Column<int>(type: "integer", nullable: false),
                    ActId = table.Column<int>(type: "integer", nullable: false),
                    NotificationNo = table.Column<string>(type: "text", nullable: true),
                    GSR_SO = table.Column<string>(type: "text", nullable: true),
                    GSRSO_Number = table.Column<string>(type: "text", nullable: true),
                    Notification_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Notifiction_SectionRule = table.Column<string>(type: "text", nullable: true),
                    NotificationRuleKind = table.Column<string>(type: "text", nullable: true),
                    NotificationRuleId = table.Column<int>(type: "integer", nullable: false),
                    GazzetId = table.Column<int>(type: "integer", nullable: false),
                    PartId = table.Column<int>(type: "integer", nullable: false),
                    Nature = table.Column<string>(type: "text", nullable: true),
                    GazetteDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NotificationType = table.Column<int>(type: "integer", nullable: false),
                    PageNo = table.Column<int>(type: "integer", nullable: true),
                    ComeInforce = table.Column<string>(type: "text", nullable: true),
                    PublishedInGazeteDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Substance = table.Column<string>(type: "text", nullable: true),
                    UserID = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_Notification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mst_NotificationBook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RuleId = table.Column<int>(type: "integer", nullable: false),
                    BookId = table.Column<int>(type: "integer", nullable: false),
                    BookYear = table.Column<int>(type: "integer", nullable: false),
                    BookPageNo = table.Column<string>(type: "text", nullable: true),
                    BookSrNo = table.Column<int>(type: "integer", nullable: true),
                    Volume = table.Column<string>(type: "text", nullable: true),
                    UserID = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_NotificationBook", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mst_NotificationExtAct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NotificationId = table.Column<int>(type: "integer", nullable: false),
                    ActId = table.Column<int>(type: "integer", nullable: false),
                    UserID = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_NotificationExtAct", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mst_NotificationType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar(100)", nullable: true),
                    UserID = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_NotificationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mst_RepealedNotification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NotificationId = table.Column<int>(type: "integer", nullable: false),
                    RepealedNotificationID = table.Column<int>(type: "integer", nullable: false),
                    UserID = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_RepealedNotification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mst_RepealedRule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RuleId = table.Column<int>(type: "integer", nullable: false),
                    RepealedRuleID = table.Column<int>(type: "integer", nullable: false),
                    UserID = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_RepealedRule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mst_Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mst_Rule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    ActTypeId = table.Column<int>(type: "int", nullable: false),
                    ActId = table.Column<int>(type: "int", nullable: false),
                    RuleNo = table.Column<string>(type: "varchar(50)", nullable: true),
                    GSRSO_Prefix = table.Column<string>(type: "varchar(5)", nullable: true),
                    GSRSO_No = table.Column<string>(type: "varchar(10)", nullable: true),
                    RuleType = table.Column<string>(type: "varchar(10)", nullable: true),
                    RuleName = table.Column<string>(type: "varchar(250)", nullable: true),
                    RuleDate = table.Column<DateTime>(type: "date", nullable: true),
                    GazzetId = table.Column<int>(type: "int", nullable: true),
                    PartId = table.Column<int>(type: "int", nullable: true),
                    Nature = table.Column<string>(type: "varchar(10)", nullable: true),
                    GazzetDate = table.Column<DateTime>(type: "date", nullable: true),
                    PageNo = table.Column<int>(type: "int", nullable: false),
                    ComeInforce = table.Column<string>(type: "varchar(10)", nullable: true),
                    ComeInforceEFDate = table.Column<DateTime>(type: "date", nullable: true),
                    UserID = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_Rule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mst_RuleBook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RuleId = table.Column<int>(type: "integer", nullable: false),
                    BookId = table.Column<int>(type: "integer", nullable: false),
                    BookYear = table.Column<int>(type: "integer", nullable: false),
                    BookPageNo = table.Column<string>(type: "text", nullable: true),
                    BookSrNo = table.Column<int>(type: "integer", nullable: true),
                    Volume = table.Column<string>(type: "text", nullable: true),
                    UserID = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_RuleBook", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mst_RuleExtAct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RuleId = table.Column<int>(type: "integer", nullable: false),
                    ActId = table.Column<int>(type: "integer", nullable: false),
                    UserID = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_RuleExtAct", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mst_Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: true),
                    UserID = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_Subjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mst_Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(100)", nullable: true),
                    MiddleName = table.Column<string>(type: "varchar(100)", nullable: true),
                    LastName = table.Column<string>(type: "varchar(100)", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mst_ActBook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ActId = table.Column<int>(type: "integer", nullable: false),
                    BookId = table.Column<int>(type: "integer", nullable: false),
                    BookYear = table.Column<int>(type: "integer", nullable: false),
                    BookPageNo = table.Column<string>(type: "text", nullable: true),
                    BookSrNo = table.Column<int>(type: "integer", nullable: true),
                    Volume = table.Column<string>(type: "text", nullable: true),
                    UserID = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_ActBook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mst_ActBook_Mst_Act_ActId",
                        column: x => x.ActId,
                        principalTable: "Mst_Act",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mst_AmendedAct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ActID = table.Column<int>(type: "integer", nullable: false),
                    AmendedActID = table.Column<int>(type: "integer", nullable: false),
                    UserID = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_AmendedAct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mst_AmendedAct_Mst_Act_ActID",
                        column: x => x.ActID,
                        principalTable: "Mst_Act",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mst_RepealedAct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ActID = table.Column<int>(type: "integer", nullable: false),
                    RepealedActID = table.Column<int>(type: "integer", nullable: false),
                    UserID = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_RepealedAct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mst_RepealedAct_Mst_Act_ActID",
                        column: x => x.ActID,
                        principalTable: "Mst_Act",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mst_Part",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PartName = table.Column<string>(type: "varchar(100)", nullable: true),
                    GazettId = table.Column<int>(type: "integer", nullable: false),
                    UserID = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_Part", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mst_Part_Mst_Gazette_GazettId",
                        column: x => x.GazettId,
                        principalTable: "Mst_Gazette",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mst_RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mst_RoleClaims_Mst_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Mst_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_Mst_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Mst_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mst_UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_Mst_UserLogins_Mst_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Mst_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mst_UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_Mst_UserRoles_Mst_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Mst_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mst_UserRoles_Mst_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Mst_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mst_UserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_Mst_UserTokens_Mst_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Mst_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Mst_Act_ActTypeId_ActNumber_ActYear",
                table: "Mst_Act",
                columns: new[] { "ActTypeId", "ActNumber", "ActYear" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mst_ActBook_ActId",
                table: "Mst_ActBook",
                column: "ActId");

            migrationBuilder.CreateIndex(
                name: "IX_Mst_ActTypes_ActType",
                table: "Mst_ActTypes",
                column: "ActType",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mst_AmendedAct_ActID",
                table: "Mst_AmendedAct",
                column: "ActID");

            migrationBuilder.CreateIndex(
                name: "IX_Mst_Gazette_GazetteName",
                table: "Mst_Gazette",
                column: "GazetteName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mst_NavigationMenus_MenuCode",
                table: "Mst_NavigationMenus",
                column: "MenuCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mst_NotificationType_Name",
                table: "Mst_NotificationType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mst_Part_GazettId",
                table: "Mst_Part",
                column: "GazettId");

            migrationBuilder.CreateIndex(
                name: "IX_Mst_Part_PartName_GazettId",
                table: "Mst_Part",
                columns: new[] { "PartName", "GazettId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mst_RepealedAct_ActID",
                table: "Mst_RepealedAct",
                column: "ActID");

            migrationBuilder.CreateIndex(
                name: "IX_Mst_RoleClaims_RoleId",
                table: "Mst_RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Mst_Roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mst_Subjects_Name",
                table: "Mst_Subjects",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mst_UserLogins_UserId",
                table: "Mst_UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Mst_UserRoles_RoleId",
                table: "Mst_UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Mst_Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Mst_Users",
                column: "NormalizedUserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "Mst_ActBook");

            migrationBuilder.DropTable(
                name: "Mst_ActTypes");

            migrationBuilder.DropTable(
                name: "Mst_AmendedAct");

            migrationBuilder.DropTable(
                name: "Mst_AmendedNotification");

            migrationBuilder.DropTable(
                name: "Mst_AmendedRule");

            migrationBuilder.DropTable(
                name: "Mst_BookEntryDetail");

            migrationBuilder.DropTable(
                name: "Mst_Books");

            migrationBuilder.DropTable(
                name: "Mst_NavigationMenus");

            migrationBuilder.DropTable(
                name: "Mst_Notification");

            migrationBuilder.DropTable(
                name: "Mst_NotificationBook");

            migrationBuilder.DropTable(
                name: "Mst_NotificationExtAct");

            migrationBuilder.DropTable(
                name: "Mst_NotificationType");

            migrationBuilder.DropTable(
                name: "Mst_Part");

            migrationBuilder.DropTable(
                name: "Mst_RepealedAct");

            migrationBuilder.DropTable(
                name: "Mst_RepealedNotification");

            migrationBuilder.DropTable(
                name: "Mst_RepealedRule");

            migrationBuilder.DropTable(
                name: "Mst_RoleClaims");

            migrationBuilder.DropTable(
                name: "Mst_Rule");

            migrationBuilder.DropTable(
                name: "Mst_RuleBook");

            migrationBuilder.DropTable(
                name: "Mst_RuleExtAct");

            migrationBuilder.DropTable(
                name: "Mst_Subjects");

            migrationBuilder.DropTable(
                name: "Mst_UserLogins");

            migrationBuilder.DropTable(
                name: "Mst_UserRoles");

            migrationBuilder.DropTable(
                name: "Mst_UserTokens");

            migrationBuilder.DropTable(
                name: "Mst_Gazette");

            migrationBuilder.DropTable(
                name: "Mst_Act");

            migrationBuilder.DropTable(
                name: "Mst_Roles");

            migrationBuilder.DropTable(
                name: "Mst_Users");
        }
    }
}
