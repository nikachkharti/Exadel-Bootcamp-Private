using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using ReportHub.API.Extensions;

namespace ReportHub.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

            builder.AddControllers();
            //builder.AddOpenApi();
            builder.AddSwagger();
            builder.AddInfrastructureLayer();
            builder.AddApplicationLayer();


            var app = builder.Build();

            //app.MapOpenApi();
            app.UseDataSeeder();
            app.UseSwagger();
            app.UseSwaggerUI();
            if (builder.Environment.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }
            app.UseAuthorization();
            app.MapControllers();
            app.UseExceptionHandler(options => { });
            app.Run();
        }
    }
}
