using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Nutrition.APIs.Tests;

public class HttpContextHelper
{
    public static DefaultHttpContext CreateMockHttpContext()
    {
        var mockHttpContext = new DefaultHttpContext
        {
            // Set up a mock HttpContext with necessary properties
            RequestServices = new ServiceCollection().AddLogging().BuildServiceProvider(),
            Response =
            {
                // The default response body is Stream.Null which throws away anything that is written to it.
                Body = new MemoryStream(),
            },
        };
        return mockHttpContext;
    }
}
