﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using AutoMapper;
using GerenciadorDeJogos.Api.Filtro;
using GerenciadorDeJogos.Application.AutoMapper;
using GerenciadorDeJogos.Application.Interfaces;
using GerenciadorDeJogos.Application.Repositorios;
using GerenciadorDeJogos.Application.Seguranca.Configuracao;
using GerenciadorDeJogos.Application.Servicos;
using GerenciadorDeJogos.Infrastructure.Contexto;
using GerenciadorDeJogos.Infrastructure.Repositorios;
using GerenciadorDeJogos.Infrastructure.Repositorios.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;
using Swashbuckle.AspNetCore.Swagger;

namespace GerenciadorDeJogos.Api
{
    public class Startup
    {

        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
           
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = _configuration["MySqlConnection:MySqlConnectionString"];
            services.AddDbContext<JogosContexto>(options => options.UseMySql(connectionString));

            var signingConfigurations = new SigningConfiguracao();
            services.AddSingleton(signingConfigurations);

            var tokenConfiguracao = new TokenConfiguracao();

            new ConfigureFromConfigurationOptions<TokenConfiguracao>(
                _configuration.GetSection("TokenConfiguracao")
            )
            .Configure(tokenConfiguracao);

            services.AddSingleton(tokenConfiguracao);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfiguracao.Audience;
                paramsValidation.ValidIssuer = tokenConfiguracao.Issuer;

                // Validates the signing of a received token
                paramsValidation.ValidateIssuerSigningKey = true;

                // Checks if a received token is still valid
                paramsValidation.ValidateLifetime = true;

                // Tolerance time for the expiration of a token (used in case
                // of time synchronization problems between different
                // computers involved in the communication process)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            // Enables the use of the token as a means of
            // authorizing access to this project's resources
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ExceptionHandlerFilterAttribute));
                options.RespectBrowserAcceptHeader = true;
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("text/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));

            })
           .AddXmlSerializerFormatters();

            //Add Swagger Service
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "Gerenciar Jogos API", Version = "V1" });
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                         Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,

                },
                new List<string>()
            }
        });

            });

            services.AddHealthChecks();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperConfiguration).GetTypeInfo().Assembly });

            #region Dependency Injection

            services.AddScoped<IJogoRepositorio, JogoRepositorio>();
            services.AddScoped<IJogoServico, JogoServico>();
            services.AddScoped<IAmigoRepositorio, AmigoRepositorio>();
            services.AddScoped<IAmigoServico, AmigoServico>();
            services.AddScoped<IEmprestimoRepositorio, EmprestimoRepositorio>();
            services.AddScoped<IEmprestimoServico, EmprestimoServico>();
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddScoped<IUsuarioServico, UsuarioServico>();
            services.AddScoped<ILoginServico, LoginServico>();
            services.AddScoped(typeof(IRepositorioBase<>), typeof(RepositorioBase<>));

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {

            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
            app.UseHttpsRedirection();


            var supportedCultures = new[]
           {
                new CultureInfo("pt-BR")
            };

            app.UseRequestLocalization(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("pt-BR");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new AcceptLanguageHeaderRequestCultureProvider()
                };
            });

            app.UseCors(options => options.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
            );

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gerenciar Jogos API V1");
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
            });

            //Starting our API in Swagger page
            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });


        }
    }
}
