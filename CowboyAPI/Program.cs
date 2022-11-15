using CowboyAPI.Data;
using CowboyAPI.DataRepos;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Serialization;

namespace CowboyAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddNewtonsoftJson(s =>
            {
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); 
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //builder.Services.AddScoped<ICowboyAPIRepo, MockCowboyAPIRepository>();
            builder.Services.AddScoped<ICowboyAPIRepo, CowboyAPISqlRepository>();

            builder.Services.AddDbContext<CowboysAPIDbContext>(options => options.UseInMemoryDatabase("CowboyAPIDb"));

            builder.Services.AddHttpClient("FakeNameAPI", client =>
            {
                //client.BaseAddress = new Uri("https://api.namefake.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            });            
           
           

            var app = builder.Build();




            // Configure the HTTP request pipeline.
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