using System.Text.Json.Serialization;

namespace GymManagementSystem.API.Extensions;

public static class SerializationExtensions
{
    public static IServiceCollection AddJsonSerializationConfig(this IServiceCollection services)
    {
        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());

            options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

        return services;
    }
}
