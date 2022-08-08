var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Session
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "mysession.fronted";
    options.IdleTimeout = TimeSpan.FromMinutes(5);
    options.Cookie.IsEssential = true;
});


//menambahkan agar pengguna yang belum login diminta login dan dapat mengakses controller
builder.Services.ConfigureApplicationCookie(opt => opt.LoginPath = "/Account/Login");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();