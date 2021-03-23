using Infrastructure.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.AppConfig
{
    public static class JwtAuthConfig
    {
        //public static void Setup(IServiceCollection services, IConfiguration configuration)
        //{
        //    var jwtConfig = configuration.GetSection("JwtConfig").Get<JwtTokenConfig>();
        //    var secret = Encoding.ASCII.GetBytes(jwtConfig.Secret); // Keys use to validate signature

        //    services.AddSingleton(jwtConfig); // Dependancy Injection!!
        //    var tokenValidationParameters = new TokenValidationParameters
        //    {
        //        ValidateIssuerSigningKey = true,
        //        IssuerSigningKey = new SymmetricSecurityKey(secret),
        //        ValidateIssuer = false,
        //        ValidateAudience = false,
        //        RequireExpirationTime = false,
        //        ValidateLifetime = true
        //    };
        //    services.AddSingleton(tokenValidationParameters);
        //    // Enable Authentication
        //    services.AddAuthentication(it =>
        //    {
        //        // Use JWT Bearer schema
        //        it.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //        it.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        //        it.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //    }).AddJwtBearer(it =>
        //    {
        //        // Setup JWT validation options


        //        //it.RequireHttpsMetadata = true; // Not mandatory => may be skip
        //        //it.SaveToken = true; // Not mandatory => may be skip
        //        it.RequireHttpsMetadata = false;
        //        it.SaveToken = true;
        //        it.TokenValidationParameters = tokenValidationParameters;

        //        //-----------------------------
        //        // Simple version
        //        //-----------------------------
        //        //var secret = Encoding.ASCII.GetBytes("ewogICAgInAiOiAiNDhpUXNGaGlHQ0lLT21yYWJyMUlYRUxoRjJsMXRqeW9NOVRSQ0FpdWNDZ0VKbUgwOWZvYlQ1REF5eEVEakhrUFllVkxnemEwazJMTG9OdWZRaG92c3J1eXFDQ1phY1RfVXNNZ1JEZmsyMkJCclBRaDhtSC15eVU0TG5QS1VKeVhSdkJzUjVncXNJd2tQa1B3eTZDdmEycFhzandJTldoeE1DVmRKOXJqTWlVIiwKICAgICJrdHkiOiAiUlNBIiwKICAgICJxIjogImt0X0dVZm5LeGxCcmdzVXdGSWdEUE5VcC16dHdNdUpEN2Z6Z3plRV93b2lFcU15ejJNMXRocFIyM21KeTQwdEdKU3psak9Jb2ZqV2N6YzNyMS0wR3B5SzhsaTh2WUdGVFBJY0toYVRHRFk4TTRsVkxIQ0xPdDlBeVVrWGNJQUtrY0R2R08xaWFQWnR6c0JTOGZ3eTVaYUE4eFpGZUxGdjBCS0lRVDAxRk5nayIsCiAgICAiZCI6ICJRQWdhN0JmN29XVTlhb2hCR3JWSDFRRmdqSVc2V2ZZUjVXTFNFb1NQODRDbjRNM3k1cGdFOEY2cHBRSzJIZVB6VnhyUms0a001aFU5Z3JEQXhFZmdfVjkxWnBsUDJXOGVjQmROZUgyT1Z0aVh6X3RTU2x4d2Flbk9EemtWcFBVWFd3RG5KMDdRcWJJOGVBa1RncGl5MmRLQWFiRVhMT0QxLTY3eV81UjZCVkJFZVA4a3JSQTl0S1dpUGMwcjIxQmNpZDBfdTJ1TGU1Z3ktWFNsSWdseGFpS2pfOGtfOTNmMTlUSm1zUVZXTDdneG1LVks2UEVSb29HVVozV2s2MFhnRzR4azhuc0w1b0tGdHlELV94SEM5Wk1tNFhRVU1sd2pkVUN4NXZKLUhSTTRLZkRRa0NIRHpIVFhORDMwYzVuandmVWRwQU1BZWlLTUREaTkyMVIwNFEiLAogICAgImUiOiAiQVFBQiIsCiAgICAidXNlIjogInNpZyIsCiAgICAicWkiOiAibkNVamt6THBBajJWUUhKeTJlcjhXZ3ZPNm00RFRrd19tQVNiakFaMUkwZnVDMTRHVDkxTndncHQxUkZGNjdPRW02Mk1YWlROOEhpanFtYXpma2F6WHF2bXlJdjdZWnZQcXQwVXlNSnJqNWEtM004eHdCQWlyR2xISUtSLUtIeV9QdUJEWS01NUNGZXpIQkFmQ3VKdlJBcGpXYWFWeUFrbnVjUHJfWFU3dVA0IiwKICAgICJkcCI6ICJ4dkZLWmFxYjZrWDR0dkxTV252Wk1qTjdVcTVhRDlORWVSaDdoTV9JUXM3QWFTR1BDRExzSFg3bzA3TEZiYV9pSG5kY2ozcDlGbWpvUGxMeThwSGFiUG1BWFZVemlHeDFMenlhOXRuMmVSX0YzSl9ROGxoaVo5elhCOE03eFZJdTBkZDBTc2Q5S1hzTXdKRW9RMERSVlRpbzVWT0hPOVlfWFhTVXV4Vnk3ejAiLAogICAgImFsZyI6ICJSUzI1NiIsCiAgICAiZHEiOiAiZzBEMVV6YmVqbUU1NHRCalNrWE9WdVBNTTVjSGlvR0g0VHRXanNZWUc3bXlpOEw3aWVqelV3eUVPZkpTNTJyU2tkQlFoNWxvUkhtVzJwaUdoaDF0RDVuTVhJNW9VVUpJaDFQTEJHbWZFR2poUTdhamU4NWpYQUx6cFhDRzZoaFNhWFlSamVGOVhVYjVtTWFfT083ZkU4R2g3WkVlVHBnU05yMjV3NVN6SURrIiwKICAgICJuIjogImdxOS1zS3lsbk5Da2pDS2hCR1ZWTS1iUjNQelhpYkdaUzhBX3Nacmg2NzAzX0VpVDhHM3JRbEdhNUdEdjBCNWVWbE5wZ0Rsbmc1V3RkQ3JLamJ6Tm9mcE1SNlVxcW4wRmRHc0lnVDFxNTRZOFVzY213RVJVUlRfaDEwRW1JYzhCSkF1SGZlQUgyVTgwNGZzQmROSUk5dzdPZUNlOXQ3bEt5ekROM2JzTlRkLTVDNV9aaTBuYXcxTjlLa3ZaOVRiX0RkWkN3emdZeGNuOXQ3T29jZWxRaHRib1VJTVFqcGh2RG1VOUtuXzlObllVeTA4QzdiMnZxdW5Zdmh6R2dGR09SRHE5MGdNMnl4VVdOZjZycWN0TlFPV1JTdTYxUzdjcDR3SlVWUkw5NjZCLTc5N1NZbThBLUo0WFNQa0FySzJfa3B4djV4dWZBd2E2TTgxUFE0bVJUUSIKfQ=="); // Keys use to validate signature
        //        //it.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        //        //{
        //        //    ValidateIssuer = true, // Require validate issuer, if true then mis specify ValidIssuer
        //        //    ValidIssuer = "giant.store.dev", // Issuer of token

        //        //    ValidateIssuerSigningKey = true, // Verify signature, if false then no need Secret Key to verify token
        //        //    IssuerSigningKey = new SymmetricSecurityKey(secret),

        //        //    ValidateAudience = true, // Validate audience, if true, must specify ValidAudience
        //        //    ValidAudience = "giant.store.dev",

        //        //    ValidateLifetime = true, // Validate JWT lifetime, if false will not check nbf and exp

        //        //    ClockSkew = TimeSpan.FromMinutes(1) // Tolerance to validate JWT lifetime
        //        //};

        //        // Token validation parameters
        //        //it.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        //        //{
        //        //    ValidateIssuer = true, // Require validate issuer, if true then mis specify ValidIssuer
        //        //    ValidIssuer = jwtConfig.Issuer, // Issuer of token

        //        //    ValidateIssuerSigningKey = true, // Verify signature, if false then no need Secret Key to verify token
        //        //    IssuerSigningKey = new SymmetricSecurityKey(secret),

        //        //    ValidateAudience = true, // Validate audience, if true, must specify ValidAudience
        //        //    ValidAudience = jwtConfig.Audience,

        //        //    ValidateLifetime = true, // Validate JWT lifetime, if false will not check nbf and exp

        //        //    ClockSkew = TimeSpan.FromMinutes(1) // Tolerance to validate JWT lifetime
        //        //};
        //    });
        //}
        public static void Setup(IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfig = Configdependecyinjection.jwtConfig;
            // Enable Authentication
            services.AddAuthentication(it =>
            {
                // Use JWT Bearer schema
                it.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                it.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(it =>
            {
                // Setup JWT validation options

                var secret = Encoding.ASCII.GetBytes(jwtConfig.Secret); // Keys use to validate signature

                it.RequireHttpsMetadata = true; // Not mandatory => may be skip
                it.SaveToken = true; // Not mandatory => may be skip

                // Token validation parameters
                it.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true, // Require validate issuer, if true then mis specify ValidIssuer
                    ValidIssuer = jwtConfig.Issuer, // Issuer of token

                    ValidateIssuerSigningKey = true, // Verify signature, if false then no need Secret Key to verify token
                    IssuerSigningKey = new SymmetricSecurityKey(secret),

                    ValidateAudience = true, // Validate audience, if true, must specify ValidAudience
                    ValidAudience = jwtConfig.Audience,

                    ValidateLifetime = true, // Validate JWT lifetime, if false will not check nbf and exp

                    ClockSkew = TimeSpan.FromMinutes(1) // Tolerance to validate JWT lifetime
                };
            });
        }
    }
}

