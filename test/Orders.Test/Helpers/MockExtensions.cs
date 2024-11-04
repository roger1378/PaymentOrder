using Microsoft.Extensions.Logging;
using Moq;

namespace Orders.Test.Helpers;

public static class MockExtensions
{
    public static void AssertLoggerPrinted<T>(this Mock<ILogger<T>> mockLogger, string value, LogLevel logLevel, Times times)
    {
        mockLogger.Verify(
            logger => logger.Log(
                It.Is<LogLevel>(level => level == logLevel),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((@object, @type) =>
                    @object.ToString().Contains(value)),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            times);
    }
}
