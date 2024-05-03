using Portafolio.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IRepositorioProyectos, RepositorioProyectos>();

builder.Services.AddTransient<ServicioTransitorio>();
builder.Services.AddScoped<ServicioDelimitado>();
builder.Services.AddSingleton<ServicioUnico>();

//Transient: Servicio transitorio
//Scoped: Servicio delimitado, se instancia cada vez que se usa
//Singleton: Servicio único, vive para siempre, sólo se instancia cuando la aplicación es reiniciada

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
