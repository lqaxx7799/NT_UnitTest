using FluentAssertions;
using Nutrition.Business;

namespace Nutrition.APIs.Tests;

public class TestConversionUtilities
{
    [Theory]
    [InlineData(1000, 1)]
    [InlineData(40, 0.04)]
    [InlineData(0, 0)]
    public void Convert_GToKg_Correct(double input, double expected)
    {
        // Arrange

        // Act
        var sut = ConversionUtilities.Convert(input, "g", "kg");

        // Assert
        sut.Should().Be(expected);
    }

    [Theory]
    [InlineData(1000)]
    [InlineData(40)]
    [InlineData(0)]
    public void Convert_NotExistingFromUnit_ThrowException(double input)
    {
        // Arrange

        // Act
        var sut = () => ConversionUtilities.Convert(input, "ton", "kg");

        // Assert
        sut.Should().ThrowExactly<ArgumentException>().And.Message.Should().Be("Unit not supported");
    }

    [Theory]
    [InlineData(1000)]
    [InlineData(40)]
    [InlineData(0)]
    public void Convert_NotExistingToUnit_ThrowException(double input)
    {
        // Arrange

        // Act
        var sut = () => ConversionUtilities.Convert(input, "oz", "pound");

        // Assert
        sut.Should().ThrowExactly<ArgumentException>().And.Message.Should().Be("Unit not supported");
    }

    [Theory]
    [InlineData("g", SystemUnit.Gram)]
    [InlineData("gram", SystemUnit.Gram)]
    [InlineData("kg", SystemUnit.Kilogram)]
    [InlineData("l", SystemUnit.Liter)]
    [InlineData("cow", null)]
    [InlineData("", null)]
    public void GetSystemUnit_WhenFind_ReturnCorrectEnum(string input, SystemUnit? expected)
    {
        // Arrange

        // Act
        var sut = ConversionUtilities.GetSystemUnit(input);

        // Assert
        sut.Should().Be(expected);
    }

    [Theory]
    [InlineData("Weight", SystemUnit.Gram)]
    [InlineData("weight", SystemUnit.Gram)]
    [InlineData("Volume", SystemUnit.Milliliter)]
    [InlineData("Length", null)]
    [InlineData("", null)]
    public void GetDefaultUnit_WhenFind_ReturnCorrectEnum(string input, SystemUnit? expected)
    {
        // Arrange

        // Act
        var sut = ConversionUtilities.GetDefaultUnit(input);

        // Assert
        sut.Should().Be(expected);
    }
}
