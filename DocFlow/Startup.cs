using AutoMapper;
using DocFlow.BusinessLayer.Helpers;
using DocFlow.BusinessLayer.ImplementInterfaces;
using DocFlow.BusinessLayer.ImplementInterfaces.GraphServices;
using DocFlow.BusinessLayer.Interfaces;
using DocFlow.BusinessLayer.Interfaces.GraphServices;
using DocFlow.BusinessLayer.Services;
using DocFlow.BusinessLayer.Services.ApplicationServices;
using DocFlow.BusinessLayer.Services.AuthorizationServices;
using DocFlow.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DocFlow
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
            services.AddDbContext<DocFlowCotext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Auto Mapper Configurations
            MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton(s => GetAzureADService());

            JwtService jwtService = GetJwtService();

            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IGraphServiceFiles, GraphServiceFiles>();
            services.AddScoped<IReportService, ReportService>();

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            DocFlowCotext contextService = serviceProvider.GetService<DocFlowCotext>();

            IReportService reportService = (IReportService)serviceProvider.GetService(typeof(IReportService));
            IDocumentService documentService = (IDocumentService)serviceProvider.GetService(typeof(IDocumentService));

            services.AddTransient<IAuthentication>(s => new Authentication(GetAzureADService(), jwtService, contextService));
            services.AddTransient<IDigitalSignature>(d => new DigitalSignature(GetDigitalSignatureCertificate(), documentService, reportService));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtService.ValidIssuer,
                    ValidAudience = jwtService.ValidAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtService.IssuerSigningKey))
                };
            });
        }

        public AzureADService GetAzureADService()
        {
            return Configuration.GetSection(nameof(AzureADService)).Get<AzureADService>();
        }

        public JwtService GetJwtService()
        {
            return Configuration.GetSection(nameof(JwtService)).Get<JwtService>();
        }

        public DigitalSignatureCertificate GetDigitalSignatureCertificate()
        {
            return Configuration.GetSection(nameof(DigitalSignatureCertificate)).Get<DigitalSignatureCertificate>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DocFlowCotext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}");
            });

            DbInitializer.Initialize(context);

        }
    }
}
