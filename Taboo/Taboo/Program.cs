
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using SwaggerThemes;
using Taboo.DAL;
using Taboo.Enums;
using Taboo.Exceptions;
using Taboo.External_Services.Abstracts;
using Taboo.External_Services.Implements;

namespace Taboo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
        
            builder.Services.AddControllers();
            builder.Services.AddFluentValidationAutoValidation();   
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();
            
            builder.Services.AddCacheService(builder.Configuration,CacheTypes.Local);
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddService();
            builder.Services.AddDbContext<TabuDbContext>(opt=>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("MSSql"));
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(Theme.UniversalDark);
            }
            if(!app.Environment.IsDevelopment())
            {
                app.UseExpectionHandler();
            }
           
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
