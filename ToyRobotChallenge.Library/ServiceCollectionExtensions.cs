using Microsoft.Extensions.DependencyInjection;

namespace ToyRobotChallenge.Library
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddToyRobotServices(this IServiceCollection services)
        {
            services.AddSingleton<IBoard, Board>();
            services.AddScoped<IToyRobot, ToyRobot>();
            services.AddScoped<ICommandParser, CommandParser>();
            return services;
        }
    }
}
