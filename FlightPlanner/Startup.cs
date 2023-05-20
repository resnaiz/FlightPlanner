using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using FlightPlanner.Handlers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data.Validation;
using FlightPlanner.Data.ValidationForSearch;
using FlightPlanner.Mapper;
using FlightPlanner.Data.Database;
using FlightPlanner.Services.Services;

namespace FlightPlanner
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FlightPlanner", Version = "v1" });
            });
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationAttribute>("BasicAuthentication", null);
            services.AddDbContext<FlightPlannerDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Flight-planner")));

            services.AddScoped<IFlightPlannerDbContext, FlightPlannerDbContext>();
            services.AddScoped<IDbService, DbService>();
            services.AddScoped<IEntityService<Flight>, EntityService<Flight>>();
            services.AddScoped<IEntityService<Airport>, EntityService<Airport>>();
            services.AddScoped<IFlightService, FlightService>();
            services.AddSingleton<IMapper>(AutoMapperConfig.CreateMapper());
            services.AddScoped<IValidation, AirportNameValidator>();
            services.AddScoped<IValidation, AirportNameEqualValidator>();
            services.AddScoped<IValidation, ArrivalTimeValidator>();
            services.AddScoped<IValidation, CarrierValidator>();
            services.AddScoped<IValidation, CityValidator>();
            services.AddScoped<IValidation, CountryValidator>();
            services.AddScoped<IValidation, DepartureTimeValidator>();
            services.AddScoped<IValidation, TimeValidator>();
            services.AddScoped<IValidationForSearch, SearchAirportNameEqualValidator>();
            services.AddScoped<IValidationForSearch, SearchAirportNameValidator>();
            services.AddScoped<IValidationForSearch, SearchDepartureDateValidator>();
            services.AddScoped<IAirportService, AirportService>();
            services.AddScoped<IClear, ClearFunction>();
            services.AddScoped<IAirportService, AirportService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FlightPlanner v1"));
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
