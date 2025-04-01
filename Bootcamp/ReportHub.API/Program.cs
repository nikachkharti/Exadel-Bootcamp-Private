using ReportHub.API.Extensions;

namespace ReportHub.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.AddControllers();
            //builder.AddOpenApi();
            builder.AddSwagger();
            builder.AddInfrastructureLayer();
            builder.AddApplicationLayer();


            var app = builder.Build();

            // Bind to Heroku port
            var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
            app.Urls.Add($"http://*:{port}");

            //app.MapOpenApi();
            app.UseDataSeeder();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.UseExceptionHandler(options => { });
            app.Run();
        }
    }
}
