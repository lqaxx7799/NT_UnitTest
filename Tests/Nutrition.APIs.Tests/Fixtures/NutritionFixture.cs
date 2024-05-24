namespace Nutrition.APIs.Tests;

public class NutritionFixture
{
    public static List<Nutrition> GetTestNutritions() => 
    [
        new Nutrition
        {
            Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"),
            Name = "Protein", 
        },
        new Nutrition
        {
            Id = new Guid("B72523E3-AD5A-4A55-87D5-E52647141093"),
            Name = "Fat", 
        },
        new Nutrition
        {
            Id = new Guid("D3969157-0489-442B-9245-5885F444354B"),
            Name = "Carbohydrate", 
        }
    ];
}
