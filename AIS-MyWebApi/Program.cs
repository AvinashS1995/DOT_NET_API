using AIS_MyWebApi.Repositories;
using AIS_MyWebApi.Repositories.Interface;
using AIS_MyWebApi.Services;
using AIS_MyWebApi.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddTransient<IUserRepository, UserRepository>();   
builder.Services.AddTransient<IUserService, UserService>();   
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();


