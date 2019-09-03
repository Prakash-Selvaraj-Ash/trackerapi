using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusTrackerApi.DbConnector;
using BusTrackerApi.Domains;
using BusTrackerApi.Hubs;
using BusTrackerApi.Mappings;
using BusTrackerApi.Repositories;
using BusTrackerApi.Services.BroadCast;
using BusTrackerApi.Services.Bus;
using BusTrackerApi.Services.BusTrack;
using BusTrackerApi.Services.Entity;
using BusTrackerApi.Services.Place;
using BusTrackerApi.Services.PushService;
using BusTrackerApi.Services.Route;
using BusTrackerApi.Services.Student;
using BusTrackerApi.Services.Track;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BusTrackerApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BusTrackContext>(options => 
                options
                .UseLazyLoadingProxies()
                .UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddTransient(typeof(BusTrackContext));
            services.AddTransient<IQueryableConnector, DbConnector.DbConnector>();

            RegisterServices(services);
            RegisterRepositories(services);
            RegisterMappings(services);

            services.AddCors();
            services.AddSignalR();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        private void RegisterRepositories(IServiceCollection services)
        {
            services.AddTransient<IRepository<Bus>, BusRepository>();
            services.AddTransient<IRepository<Student>, StudentRepository>();
            services.AddTransient<IRepository<BusTracker>, BusTrackerRepository>();
            services.AddTransient<IRepository<LiveTracker>, LiveTrackerRepository>();
            services.AddTransient<IRepository<Place>, PlaceRepository>();
            services.AddTransient<IRepository<Route>, RouteRepository>();
            services.AddTransient<IRepository<RouteAssociation>, RouteAssociationRepository>();
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<ITrackService, TrackService>();
            services.AddTransient<IBroadCastService, BroadCastService>();
            services.AddTransient<IPlaceService, PlaceService>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IBusService, BusService>();
            services.AddTransient<IRouteService, RouteService>();
            services.AddTransient<IBusTrackService, BusTrackService>();
            services.AddTransient<IEntityService, EntityService>();
            services.AddTransient<IPushNotifyService, PushNotificationService>();
        }

        private void RegisterMappings(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new PlaceProfile());
                mc.AddProfile(new RouteProfile());
                mc.AddProfile(new StudentProfile());
                mc.AddProfile(new BusProfile());
                mc.AddProfile(new BusTrackerProfile());
                mc.AddProfile(new TrackProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(builder =>
            {
                builder.WithOrigins("http://localhost:4200")
                .AllowAnyHeader().AllowAnyMethod().AllowCredentials();
            });

            app.UseSignalR(routes =>
            {
                routes.MapHub<BroadCastHub>("/broadCast");
            });
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
