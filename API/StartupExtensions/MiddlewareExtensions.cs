namespace API.Extensions
{
    public static class MiddlewareExtensions
    {
        public static WebApplication UseCustomMiddlewares(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            return app;
        }
    }
}