using dotnet_anime_list;
using dotnet_anime_list.Data;
using dotnet_anime_list.API.Models;
using dotnet_anime_list.API.Services;
using dotnet_anime_list.API.Repositories.TokenRepository;
using dotnet_anime_list.API.Repositories.UserRepository;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using dotnet_anime_list.API.Repositories.AnimeRepository;
using dotnet_anime_list.API.Repositories.GenreRepository;
using dotnet_anime_list.API.Repositories.SeasonRepository;
using Microsoft.Extensions.FileProviders;
using dotnet_anime_list.API.Repositories.CommentRepository;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
        {
            policy.WithHeaders("localhost:4200");
            policy.AllowCredentials();
            policy.AllowAnyMethod();
            policy.AllowAnyHeader();
            policy.SetIsOriginAllowed(_ => true);
        }));

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
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
                In = ParameterLocation.Header
            },
            new List<string>()
        }
            });
        });
        
        builder.Services.AddScoped<AppDbContext>();
        builder.Services.AddScoped<UserService>();
        builder.Services.AddScoped<AuthService>();
        builder.Services.AddScoped<TokenService>();
        builder.Services.AddScoped<AnimeService>();
        builder.Services.AddScoped<UtilsService>();
        builder.Services.AddScoped<GenreService>();
        builder.Services.AddScoped<SeasonService>();
        builder.Services.AddScoped<CommentService>();
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ITokenRepository, TokenRepository>();
        builder.Services.AddScoped<IAnimeRepository, AnimeRepository>();
        builder.Services.AddScoped<IGenreRepository, GenreRepository>();
        builder.Services.AddScoped<ISeasonRepository, SeasonRepository>();
        builder.Services.AddScoped<ICommentRepository, CommentRepository>();

        var key = Encoding.ASCII.GetBytes(Key.secret);
        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
        builder.Services.AddAuthorization();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
            RequestPath = "/Uploads"
        });	

        app.UseHttpsRedirection();
        app.UseCors();
        app.UseRouting(); 
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}