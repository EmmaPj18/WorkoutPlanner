using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutPlanner.Migrations.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Exercise",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ExplainVideoUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercise", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lookup",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lookup", x => new { x.Id, x.Category });
                });

            migrationBuilder.CreateTable(
                name: "Routine",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ExplainVideoUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routine", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Set",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ExplainVideoUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoundsNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Set", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDay = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoutineSet",
                schema: "dbo",
                columns: table => new
                {
                    RoutineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SetId = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutineSet", x => new { x.SetId, x.RoutineId });
                    table.ForeignKey(
                        name: "FK_RoutineSet_Routine_RoutineId",
                        column: x => x.RoutineId,
                        principalSchema: "dbo",
                        principalTable: "Routine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoutineSet_Set_SetId",
                        column: x => x.SetId,
                        principalSchema: "dbo",
                        principalTable: "Set",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SetExercise",
                schema: "dbo",
                columns: table => new
                {
                    SetId = table.Column<int>(type: "int", nullable: false),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<double>(type: "float(18)", precision: 18, scale: 2, nullable: false),
                    QuantityType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SetExercise", x => new { x.SetId, x.ExerciseId });
                    table.ForeignKey(
                        name: "FK_SetExercise_Exercise_ExerciseId",
                        column: x => x.ExerciseId,
                        principalSchema: "dbo",
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SetExercise_Set_SetId",
                        column: x => x.SetId,
                        principalSchema: "dbo",
                        principalTable: "Set",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoutine",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoutineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkOutDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoutine", x => new { x.UserId, x.RoutineId });
                    table.ForeignKey(
                        name: "FK_UserRoutine_Routine_RoutineId",
                        column: x => x.RoutineId,
                        principalSchema: "dbo",
                        principalTable: "Routine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoutine_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Lookup",
                columns: new[] { "Category", "Id", "Description", "IsActive", "Name", "SortOrder" },
                values: new object[,]
                {
                    { "QuantityType", 1, "Seconds per excersice rep", true, "Seconds", 0 },
                    { "SetType", 1, "AMRAP - As Many Rounds As Possible", true, "AMRAP", 0 },
                    { "QuantityType", 2, "Minutes per excersice rep", true, "Minutes", 1 },
                    { "SetType", 2, "EMON - Every Minute On a Minute", true, "EMON", 1 },
                    { "QuantityType", 3, "Repetitions over excercise", true, "Reps", 2 },
                    { "SetType", 3, "RTF - Rounds For Time", true, "RFT", 2 },
                    { "SetType", 4, "RM - Round Max or Rep Max", true, "RM", 3 },
                    { "SetType", 5, "Ladder - Increasing or Decreasing Workload over time", true, "Ladder", 4 },
                    { "SetType", 6, "Tabata - 8 rounds of High-Intensity intervals. 20 seconds effort + 10 seconds rest", true, "Tabata", 5 },
                    { "SetType", 7, "Chipper - List of excercises to do in the order that is listed", true, "Chipper", 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_Name",
                schema: "dbo",
                table: "Exercise",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lookup_Category_Name",
                schema: "dbo",
                table: "Lookup",
                columns: new[] { "Category", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_RoutineSet_RoutineId",
                schema: "dbo",
                table: "RoutineSet",
                column: "RoutineId");

            migrationBuilder.CreateIndex(
                name: "IX_Set_Name",
                schema: "dbo",
                table: "Set",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Set_Type",
                schema: "dbo",
                table: "Set",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_SetExercise_ExerciseId",
                schema: "dbo",
                table: "SetExercise",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                schema: "dbo",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_UserName",
                schema: "dbo",
                table: "User",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoutine_RoutineId",
                schema: "dbo",
                table: "UserRoutine",
                column: "RoutineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lookup",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "RoutineSet",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SetExercise",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserRoutine",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Exercise",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Set",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Routine",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "User",
                schema: "dbo");
        }
    }
}
