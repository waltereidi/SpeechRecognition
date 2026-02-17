using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpeechRecognition.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitialFileStorageSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileStorageAggregate",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileStorageAggregate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AudioTranslation",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    Translation = table.Column<string>(type: "text", nullable: false),
                    FileStorageConversionId = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    IsSuccess = table.Column<bool>(type: "boolean", nullable: false),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: true),
                    TranslationTemplate = table.Column<int>(type: "integer", nullable: false),
                    WhisperModel = table.Column<int>(type: "integer", nullable: false),
                    FileStorageAggregateId = table.Column<string>(type: "character varying(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudioTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AudioTranslation_FileStorageAggregate_FileStorageAggregateId",
                        column: x => x.FileStorageAggregateId,
                        principalTable: "FileStorageAggregate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileStorage",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    FileFullName = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    OriginalFileName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    FileStorageAggregateId = table.Column<string>(type: "character varying(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileStorage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileStorage_FileStorageAggregate_FileStorageAggregateId",
                        column: x => x.FileStorageAggregateId,
                        principalTable: "FileStorageAggregate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileStorageConversion",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    FileStorageId = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    FileStorageAggregateId = table.Column<string>(type: "character varying(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileStorageConversion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileStorageConversion_FileStorageAggregate_FileStorageAggre~",
                        column: x => x.FileStorageAggregateId,
                        principalTable: "FileStorageAggregate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RabbitMqLog",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Severity = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    FileStorageAggregateId = table.Column<string>(type: "character varying(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RabbitMqLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RabbitMqLog_FileStorageAggregate_FileStorageAggregateId",
                        column: x => x.FileStorageAggregateId,
                        principalTable: "FileStorageAggregate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AudioTranslation_FileStorageAggregateId",
                table: "AudioTranslation",
                column: "FileStorageAggregateId");

            migrationBuilder.CreateIndex(
                name: "IX_FileStorage_FileStorageAggregateId",
                table: "FileStorage",
                column: "FileStorageAggregateId");

            migrationBuilder.CreateIndex(
                name: "IX_FileStorageConversion_FileStorageAggregateId",
                table: "FileStorageConversion",
                column: "FileStorageAggregateId");

            migrationBuilder.CreateIndex(
                name: "IX_RabbitMqLog_FileStorageAggregateId",
                table: "RabbitMqLog",
                column: "FileStorageAggregateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AudioTranslation");

            migrationBuilder.DropTable(
                name: "FileStorage");

            migrationBuilder.DropTable(
                name: "FileStorageConversion");

            migrationBuilder.DropTable(
                name: "RabbitMqLog");

            migrationBuilder.DropTable(
                name: "FileStorageAggregate");
        }
    }
}
