using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SchoolManagement.Infrastructure;
using SchoolManagement.Models;
using SchoolManagement.Repositories;
using SchoolManagement.Services;

var builder = WebApplication.CreateBuilder(args);

SqlInitializer.Initialize(builder.Services, builder.Configuration);

// Configurar o DbContext
builder.Services.AddDbContext<SchoolManagementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicionar Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<SchoolManagementDbContext>()
    .AddDefaultTokenProviders();

// Configurar JWT
builder.Services.AddScoped<ITokenService, TokenService>();

// Adicionar serviços ao contêiner
builder.Services.AddControllers();

// Configurar Swagger/OpenAPI
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Escola",
        Version = "v1",
        Description = "AspNetSchoolManagement",
        Contact = new OpenApiContact
        {
            Name = "Patrick Mendes",
            Email = "Mendespatrick720@gmail.com"
        }
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configurar o pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();