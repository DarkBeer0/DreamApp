using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DreamApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// ���������� ������ ����������� �� ������������
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// ����������� ��������� ���� ������
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// ����������� �������� Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// ���������� ������������ � ��������������� (���� ����������� MVC)
builder.Services.AddControllersWithViews();

// ���������� Razor Pages (���������� ��� ������� Identity)
builder.Services.AddRazorPages();

var app = builder.Build();

// ��������� ����� ��������� ����������
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // ��������� ��� ��������� ����������� ������

app.UseRouting();

app.UseAuthentication(); // ��������� ��� ��������� ��������������
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// �������� ��� Razor Pages (���������� ��� ������� Identity)
app.MapRazorPages();

app.Run();