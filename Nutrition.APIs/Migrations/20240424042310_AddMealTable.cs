using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nutrition.APIs.Migrations
{
    /// <inheritdoc />
    public partial class AddMealTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealDetails_Foods_FoodId",
                table: "MealDetails");

            migrationBuilder.DropColumn(
                name: "FoodId",
                table: "FoodNutritionValues");

            migrationBuilder.RenameColumn(
                name: "FoodId",
                table: "MealDetails",
                newName: "FoodVariationId");

            migrationBuilder.RenameIndex(
                name: "IX_MealDetails_FoodId",
                table: "MealDetails",
                newName: "IX_MealDetails_FoodVariationId");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "FoodNutritionValues",
                newName: "Amount");

            migrationBuilder.AddForeignKey(
                name: "FK_MealDetails_FoodVariations_FoodVariationId",
                table: "MealDetails",
                column: "FoodVariationId",
                principalTable: "FoodVariations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealDetails_FoodVariations_FoodVariationId",
                table: "MealDetails");

            migrationBuilder.RenameColumn(
                name: "FoodVariationId",
                table: "MealDetails",
                newName: "FoodId");

            migrationBuilder.RenameIndex(
                name: "IX_MealDetails_FoodVariationId",
                table: "MealDetails",
                newName: "IX_MealDetails_FoodId");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "FoodNutritionValues",
                newName: "Value");

            migrationBuilder.AddColumn<Guid>(
                name: "FoodId",
                table: "FoodNutritionValues",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MealDetails_Foods_FoodId",
                table: "MealDetails",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
