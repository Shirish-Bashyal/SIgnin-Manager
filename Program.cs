using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SIgnin_Manager.Data;
using SIgnin_Manager.Helper;
using SIgnin_Manager.Hubs;
using SIgnin_Manager.Interface;
using SIgnin_Manager.Models;
using SIgnin_Manager.Repositories;
using System.Text;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//adding connection string 
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SIgnin_Manager.Data.ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));



// getting values from configuration file.

//var data = builder.Configuration["My_Key"];

    






//adding identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>   // using applicationuser instead of identity user coz it inherits from identity user
{
    //options.Password.RequiredLength = 6;
    //options.Password.RequireUppercase = true;
   
}).AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();


//adding token gererator
builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,    
        ValidAudience= "http://ahmadmozaffar.net",
        ValidIssuer= "http://ahmadmozaffar.net",
        RequireExpirationTime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This is the key that we will use in the encryption")),
        ValidateIssuerSigningKey = true
    };
});
 

builder.Services.AddScoped<IUserInterface, UserService>();
builder.Services.AddScoped<IFriendRequestInterface, FriendRequest>();
builder.Services.AddScoped<IMessageInterface, Messages>();
builder.Services.AddSignalR();


builder.Services.AddSingleton<JwtValidateAndDeserialize>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Signin Manager", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHub<SignalrHub>("/SignalrHub");

app.Run();
