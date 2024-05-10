using Nutrition.Library;

namespace Nutrition.APIs;

public class FoodCategory : BaseEntity
{
    public Guid? FoodId { get; set; }
    public Guid? CategoryId { get; set; }

    public Food? Food { get; set; }
    public Category? Category { get; set; }
}
