using GM_DAL.IServices;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using GM_DAL.Services;
using GM_DAL;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<SQLAdoContext>();
builder.Services.AddScoped<IUserInfoService, UserInfoService>();
builder.Services.AddScoped<IBankService, BankService>();
builder.Services.AddScoped<IChiTieuService, ChiTieuService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();



var contact = new OpenApiContact()
{
    Name = "nghiaht",
    Email = "huynhnghia911@gmail.com",
    Url = new Uri("http://www.gamanjsc.com")
};

var license = new OpenApiLicense()
{
    Name = "GM",
    Url = new Uri("http://www.gamanjsc.com")
};

var info = new OpenApiInfo()
{
    Version = "v1",
    Title = "TKTK",
    Description = "TKTK config",
    TermsOfService = new Uri("http://www.example.com"),
    Contact = contact,
    License = license
};


builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", info);
    //o.CustomSchemaIds(i => i.FullName?.Replace("+", "."));
    o.UseInlineDefinitionsForEnums();

    o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Token please",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    o.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });


});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle



builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false, // Không validate Issuer
        ValidateAudience = false, // Không validate Audience
        ValidateLifetime = true, // Validate thời gian hết hạn
        ValidateIssuerSigningKey = true, // Validate khóa ký
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});




var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
