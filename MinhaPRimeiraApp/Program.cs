using Microsoft.AspNetCore.Components;
using MinhaPrimeiraApp.Components.Cascading;
using MinhaPrimeiraApp.Components.DI;
using MinhaPRimeiraApp.Components;
using System.ComponentModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddCascadingValue(sp =>
  {
      //var StyleContext = new StyleContext { BackgroundColor = "lightblue" };
      //var source = new CascadingValueSource<StyleContext>(StyleContext, isFixed: false);
      //return source;
      return new StyleContext { BackgroundColor = "lightblue" };
  });

builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode();

app.Run();

//public static class CascadingValueSource
//{
//    public static CascadingValueSource<T> CreateNotifying<T>(T value, bool isFixed = false)
//           where T : INotifyPropertyChanged
//    {
//        var source = new CascadingValueSource<T>(value, isFixed);
        
//        value.PropertyChanged += (s, e) =>
//        {
//            source.NotifyValueChangedAsync();
//        };

//        return source;
//    }
//}
