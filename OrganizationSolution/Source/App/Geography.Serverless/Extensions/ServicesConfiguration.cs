﻿namespace Geography.Serverless
{
    using Framework.Configuration.Models;
    using Framework.Constant;
    using Geography.DataAccess;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Geography.Business.Country.Models;

    /// <summary>
    /// Defines the <see cref="ServicesConfiguration" />.
    /// </summary>
    public static class ServicesConfiguration
    {

        public const string AuthenticationScheme = "Bearer";

        /// <summary>
        /// The ConfigureClientServices.
        /// </summary>
        /// <param name="services">The services<see cref="IServiceCollection"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ConfigureClientServices(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var applicationOptions = serviceProvider.GetRequiredService<ApplicationOptions>();

            return services
                .ConfigureAutoMapper()                
                .ConfigureDbServices(applicationOptions.ConnectionString);

        }

        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddAutoMapper(typeof(CountryMappingProfile).Assembly);
            return services;
        }

        /// <summary>
        /// The ConfigureSwagger.
        /// </summary>
        /// <param name="services">The services<see cref="IServiceCollection"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            ////var swaggerAssemblies = new[] { typeof(Program).Assembly, typeof(CountryCreateModel).Assembly, typeof(Model).Assembly };
            //services.AddSwaggerWithComments(ApiConstants.ApiName, ApiConstants.ApiVersion, swaggerAssemblies);
            //services.AddSwaggerWithComments(ApiConstants.JobsApiName, ApiConstants.JobsApiVersion, swaggerAssemblies);
            return services;
        }

        public static IServiceCollection ConfigureAwsCongnitoSecurity(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var applicationOptions = serviceProvider.GetRequiredService<ApplicationOptions>();

            services.AddCognitoIdentity();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = applicationOptions.CognitoAuthorityURL;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = applicationOptions.CognitoAuthorityURL,
                    ValidateLifetime = true,
                    LifetimeValidator = (before, expires, token, param) => expires > DateTime.UtcNow,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,

                };
            });
            return services;
        }
    }
}