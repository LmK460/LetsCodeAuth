using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore;
using Microsoft.OpenApi.Models;
using MinimalLetsApiAuth.DTO;
using MinimalLetsApiAuth.Services;
using MinimalLetsApiAuth.Domain.Interfaces;
using MinimalLetsApiAuth.Repository;
using MinimalLetsApiAuth.Domain.Configure;

var builder = WebApplication.CreateBuilder(args);
//Carregando key
var key = Encoding.ASCII.GetBytes(builder.Configuration["TokenConfigurations:secureKey"]);
//Carregando connectionstring
var connectionString = builder.Configuration.GetConnectionString("PostGreesConnection");

#region Configure Services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "letsCodeAuthAPI", Version = "v1" });
});


#region JWT scope
builder.Services.AddAuthentication(z =>
{
    z.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    z.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(z =>
{
    z.SaveToken = true;
    z.RequireHttpsMetadata = false;
    z.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});
#endregion

#endregion


var app = builder.Build();
//Ativando Swagger para testes
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
}
app.UseAuthentication();
// app.UseAuthorization();


#region endpoints

app.MapGet("/", () => "Hello World!");

app.MapPost("/auth", async (UserLoginDTO userLoginDTO) =>
{
    if((userLoginDTO == null || userLoginDTO.UserName ==null || userLoginDTO.Password == null))
    {
        return Results.BadRequest("Login Inválido");
    }
    else
    {
        var dataBaseConnectionFactory = new DataBaseConnectionFactory(connectionString);

        IAuthRepository auth = new AuthRepository(dataBaseConnectionFactory);
        var authService = new AuthService(auth);
        var result = await authService.Login(userLoginDTO);
        if (result.Autenticated)
            return Results.Ok(result);
        else
            return Results.BadRequest(result);
    }
});
#endregion



app.Run();
