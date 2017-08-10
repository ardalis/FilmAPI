﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FilmAPI.Interfaces;
using FilmAPI.Services;
using FilmAPI.Core.Interfaces;
using FilmAPI.Infrastructure.Repositories;
using FilmAPI.Infrastructure.Data;

namespace FilmAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
       
            services.AddScoped<IFilmRepository, FilmRepository>();
            services.AddScoped<IPersonRepository, IPersonRepository>();
            services.AddScoped<IMediumRepository, IMediumRepository>();
            services.AddScoped<IFilmPersonRepository, IFilmPersonRepository>();
            services.AddScoped<IFilmService, FilmService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IFilmPersonService, FilmPersonService>();
            services.AddScoped<IMediumService, MediumService>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}