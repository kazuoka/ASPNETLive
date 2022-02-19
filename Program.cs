using ASPNETLive.Services.ThaiDate;
using Microsoft.EntityFrameworkCore;
using ASPNETLive.Data;
using Microsoft.AspNetCore.Identity;
using ASPNETLive.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<APIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("APIContext")));

//Identity
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<IdentityContext>();
builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityContext")));
builder.Services.Configure<IdentityOptions>(options => 
{
    options.Password.RequiredLength = 3;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Custom Service
builder.Services.AddScoped<IThaiDate, ThaiDate>();

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

app.Run();
