using System.Text;
using EstudoPlanner.BLL.Configurations;
using EstudoPlanner.BLL.Services.Auth;
using EstudoPlanner.BLL.Services.StudyPlan;
using EstudoPlanner.DAL.DataContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Add service to the container
builder.Services.AddControllers();
builder.Services.AddSwaggerConfiguration();

//Database
builder.Services.AddDatabaConfiguration(builder.Configuration);

//Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IStudyPlanService, StudyPlanService>();
builder.Services.AddScoped<JwtTokenGenerateService>();

//AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


//JWT
builder.Services.AddJwtConfiguration(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Estudo Planner API v1");
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();