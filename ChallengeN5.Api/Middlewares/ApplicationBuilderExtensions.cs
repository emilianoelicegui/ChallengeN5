namespace ChallengeN5.Api.Middlewares
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseGlobalException(this IApplicationBuilder builder)
           => builder.UseMiddleware<GlobalExceptionMiddleware>();
    }

}
