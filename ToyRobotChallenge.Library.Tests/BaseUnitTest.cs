using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ToyRobotChallenge.Library.Tests
{
    public abstract class BaseUnitTest : IDisposable
    {
        private readonly ServiceProvider _serviceProvider;
        protected ICommandParser CommandParser;

        protected BaseUnitTest()
        {
            var services = new ServiceCollection()
                .AddToyRobotServices()
                .AddLogging(config => config.AddConsole());

            _serviceProvider = services.BuildServiceProvider();
            CommandParser = _serviceProvider.GetRequiredService<ICommandParser>();
        }

        public void Dispose() => ((IDisposable)_serviceProvider)?.Dispose();
    }
}
