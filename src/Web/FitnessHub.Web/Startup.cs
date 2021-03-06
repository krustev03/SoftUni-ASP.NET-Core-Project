﻿namespace FitnessHub.Web
{
    using System;
    using System.Reflection;

    using CloudinaryDotNet;
    using FitnessHub.Data;
    using FitnessHub.Data.Common;
    using FitnessHub.Data.Common.Repositories;
    using FitnessHub.Data.Models;
    using FitnessHub.Data.Repositories;
    using FitnessHub.Data.Seeding;
    using FitnessHub.Services.Data;
    using FitnessHub.Services.Mapping;
    using FitnessHub.Services.Messaging;
    using FitnessHub.Web.Hubs;
    using FitnessHub.Web.Infrastructure.Payment;
    using FitnessHub.Web.ViewModels;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Stripe;

    using static FitnessHub.Common.GlobalConstants;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.Configure<StripeSettings>(this.configuration.GetSection("Stripe"));

            services.Configure<MailSettings>(this.configuration.GetSection("MailSettings"));

            services.AddSignalR();

            var account = new CloudinaryDotNet.Account(
                this.configuration["Cloudinary:CloudName"],
                this.configuration["Cloudinary:ApiKey"],
                this.configuration["Cloudinary:ApiSecret"]);

            var cloudUtility = new Cloudinary(account);
            services.AddSingleton(cloudUtility);

            services.AddControllersWithViews(
                options =>
                    {
                        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    }).AddRazorRuntimeCompilation();
            services.AddRazorPages();

            services.AddSingleton(this.configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender>(x => new SendGridEmailSender(SendGridKey));
            services.AddTransient<ITrainerPostService, TrainerPostService>();
            services.AddTransient<ISuplementService, SuplementService>();
            services.AddTransient<IEquipmentService, EquipmentService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<INewsService, NewsService>();
            services.AddTransient<IMailService, MailService>();
            services.AddTransient<IOrderService, Services.Data.OrderService>();
            services.AddTransient<ITrainingProgramService, TrainingProgramService>();
            services.AddTransient<ITrainingService, TrainingService>();
            services.AddTransient<IExerciseService, ExerciseService>();
            services.AddTransient<IMuscleGroupService, MuscleGroupService>();
            services.AddTransient<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            StripeConfiguration.ApiKey = this.configuration.GetSection("Stripe")["SecretKey"];

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapHub<ChatHub>("/chatter");
                        endpoints.MapControllerRoute("Trainer", "{area:exists}/{controller=TrainerPosts}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
