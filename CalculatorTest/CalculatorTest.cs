using Calculator;

namespace CalculatorTest;

public class CalculatorTest
{
    [Fact]
    public void MyCalculator_NullTest()
    {
        var calculator = new MyCalculator();
        Assert.NotNull(calculator);
    }

    [Fact]
    public void AddTest()
    {
        // arrange
        var calculator = new MyCalculator();

        // act
        var actual = calculator.Add(1, 2);

        // assert
        Assert.Equal(3, actual);
    }

    [Fact]
    public void SubtractTest()
    {
        // arrange
        var calculator = new MyCalculator();

        // act
        var actual = calculator.Subtract(10, 4);

        // assert
        Assert.Equal(6, actual);
    }

    [Fact]
    public void Subtract_ReturnsNegative()
    {
        // arrange
        var calculator = new MyCalculator();

        // act
        var actual = calculator.Subtract(5, 8);

        // assert
        Assert.Equal(-3, actual);
    }

    [Fact]
    public void MultiplyTest()
    {
        // arrange
        var calculator = new MyCalculator();
    
        // act
        var actual = calculator.Multiply(4, 5);

        // assert
        Assert.Equal(20, actual);
    }

    [Theory]
    [InlineData(1, 2, 2)]
    [InlineData(7, 5, 35)]
    [InlineData(4, 4, 16)]
    [InlineData(0, 0, 0)]
    public void Multiply_MultipleInputs(int x, int y, int expected)
    {
        // arrange
        var calculator = new MyCalculator();

        // act
        var actual = calculator.Multiply(x, y);

        // assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DivideTest()
    {
        // arrange
        var calculator = new MyCalculator();

        // act
        var actual = calculator.Divide(42, 6);

        // assert
        Assert.Equal(7, actual);
    }

    [Fact]
    public void Divide_DivideByZero_Exception()
    {
        // arrange
        var calculator = new MyCalculator();

        // act
        Action actual = () => calculator.Divide(10, 0);
    
        // assert
        Assert.Throws<DivideByZeroException>(actual);
    }
}
