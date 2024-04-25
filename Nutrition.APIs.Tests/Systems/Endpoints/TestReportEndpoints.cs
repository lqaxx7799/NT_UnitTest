using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Moq;

namespace Nutrition.APIs.Tests;

public class TestReportEndpoints
{
    // [Fact]
    // public void UseReportEndpoints_WhenCalled_InvokesMap()
    // {
    //     // Arrange
    //     // var webApplication = WebApplication.CreateBuilder().Build();
    //     var mockEndpointRouteBuilder = new Mock<IEndpointRouteBuilder>();
    //     mockEndpointRouteBuilder.Setup(p => p.MapGroup(It.IsAny<string>())).Returns(It.IsAny<RouteGroupBuilder>());

    //     // Act
    //     ReportEndpoints.UseReportEndpoints(mockEndpointRouteBuilder.Object);

    //     // Assert
    //     mockEndpointRouteBuilder.Verify(x => x.MapGroup("report"), Times.Once);
    // }

    [Fact]
    public async Task GetNutritionProfileReport_OnSuccess_ReturnsStatusCode200()
    {
        // Arrange
        var mockReportService = new Mock<IReportService>();
        mockReportService
            .Setup(x => x.GetNutritionProfile(It.IsAny<ReportNutritionProfileRequest>()))
            .ReturnsAsync(new ReportNutritionProfileResponse());
        var mockHttpContext = HttpContextHelper.CreateMockHttpContext();

        // Act
        var sut = await ReportEndpoints.GetNutritionProfileReport(new ReportNutritionProfileRequest(), mockReportService.Object);
        await sut.ExecuteAsync(mockHttpContext);

        // Assert
        mockHttpContext.Response.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task GetNutritionProfileReport_OnSuccess_InvokesReportServiceExactlyOnce()
    {
        // Arrange
        var mockReportService = new Mock<IReportService>();
        mockReportService
            .Setup(x => x.GetNutritionProfile(It.IsAny<ReportNutritionProfileRequest>()))
            .ReturnsAsync(new ReportNutritionProfileResponse());
        var mockHttpContext = HttpContextHelper.CreateMockHttpContext();

        // Act
        var sut = await ReportEndpoints.GetNutritionProfileReport(new ReportNutritionProfileRequest(), mockReportService.Object);
        await sut.ExecuteAsync(mockHttpContext);

        // Assert
        mockReportService.Verify(x => x.GetNutritionProfile(It.IsAny<ReportNutritionProfileRequest>()), Times.Once);
    }
}
