using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleCatalog.Infrastructure.Telemetry
{
    internal static partial class LoggerExtensions
    {
        [LoggerMessage(LogLevel.Information, "Starting the app...")]
        public static partial void StartingApp(this ILogger logger);

        [LoggerMessage(LogLevel.Information, "Food `{name}` price changed to `{price}`.")]
        public static partial void FoodPriceChanged(this ILogger logger, string name, double price);
    }
}
