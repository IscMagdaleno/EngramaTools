


using Engrama.API.EngramaLevels.Dominio.Core;
using Engrama.API.EngramaLevels.Dominio.Interfaces;
using Engrama.API.EngramaLevels.Infrastructure.Interfaces;
using Engrama.API.EngramaLevels.Infrastructure.Repository;

using EngramaCoreStandar.Extensions;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddScoped<IUsersDominio, UsersDominio>();
builder.Services.AddScoped<IDataBaseDominio, DataBaseDominio>();
builder.Services.AddScoped<ICommonScriptsDominio, CommonScriptsDominio>();
builder.Services.AddScoped<IAnalyzeCodeDominio, AnalyzeCodeDominio>();
builder.Services.AddScoped<IAI_ToolsDominio, AI_ToolsDominio>();

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IDataBaseRepository, DataBaseRepository>();
builder.Services.AddScoped<ICommonScriptsRepository, CommonScriptsRepository>();


builder.Services.AddEngramaDependenciesAPI();
// MapperHelper como singleton porque maneja su propia configuración



/*Swagger configuration*/
builder.Services.AddSwaggerGen(options =>
{
	// using System.Reflection;
	var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	var path = Path.Combine(AppContext.BaseDirectory, xmlFilename);
	options.IncludeXmlComments(path);


	/*Token*/
	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header
	});
});



/*JWT configuration*/
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = false,
		ValidateAudience = false,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(
			Encoding.UTF8.GetBytes(builder.Configuration["keyJWT"])),
		ClockSkew = TimeSpan.Zero
	});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors(x => x
					.AllowAnyMethod()
					.AllowAnyHeader()
					.SetIsOriginAllowed(origin => true) // allow any origin
					.AllowCredentials());


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
