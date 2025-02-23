using BikeStore.Persistence;
using BikeStore.Persistence.Data;
using BikeStore.Persistence.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using BikeStore.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using BikeStore.Api;
using MapsterMapper;
using System.Reflection;
using BikeStore.Persistence.Data.SeedData;
using Microsoft.Extensions.FileProviders;
using Mapster;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.RegisterDI(builder.Configuration);

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(option =>
{
    option.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<BikeStoresContext>().AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
{

    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))

    };
});

builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("AdminRoles", policy =>
        policy.RequireRole("STOREADMIN", "SUPERADMIN"));
    
        options.AddPolicy("AdminStaff", policy =>
        policy.RequireRole("STOREADMIN", "SUPERADMIN", "EMPLOYEE"));

        options.AddPolicy("CanEdit", policy =>
        policy.RequireClaim("Permission", "CanEdit"));

        options.AddPolicy("CanDelete", policy =>
        policy.RequireClaim("Permission", "CanDelete"));

        options.AddPolicy("CanView", policy =>
        policy.RequireClaim("Permission", "CanView"));

    });


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Configure Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("CustomPolicy", x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
});
#endregion

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        Description = @"Jwt Authorization header using Bearer Scheme.
                        Enter 'Bearer' [space] and then your token in the input below
                        Example:'Bearer 12345waeaeae'",

    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
        new OpenApiSecurityScheme
        {
            Reference=new OpenApiReference
            {
                Type=ReferenceType.SecurityScheme,
                Id="Bearer"
            },
            Scheme="Oauth2",
            Name="Bearer",
            In=ParameterLocation.Header

        },new List<string>()

      }
    });
});


var app = builder.Build();
var serviceProvider = app.Services;
await RolesSeed.SeedRoles(serviceProvider);
//await RolesSeed.SeedUsers(serviceProvider);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CustomPolicy");

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Assets")),
    RequestPath = "/Assets"
});

app.MapControllers();

app.Run();
