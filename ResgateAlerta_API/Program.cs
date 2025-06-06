using ResgateAlerta.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace ResgateAlerta
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API ResgateAlerta",
                    Description = "API para o sistema ResgateAlerta",
                    Contact = new OpenApiContact
                    {
                        Name = "Suporte",
                        Email = "suporte@resgatealerta.com"
                    }
                });
            });


            // Configura o DbContext para Oracle (ou outro banco que usar)

            //var connectionString = builder.Configuration.GetConnectionString("Oracle");

            //Console.WriteLine("STRING DE CONEX�O");
            //Console.WriteLine(connectionString); // verificando no console

            builder.Services.AddDbContext<ResgateAlertaContext>(options =>
            {
                options.UseOracle(builder.Configuration.GetConnectionString("Oracle"));

            });

            var app = builder.Build();

            // Middleware pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
