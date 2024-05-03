using HC4xServer.Core;
using LibServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

internal class Program
{
  private static void Main(string[] args)
  {
    WebApplicationBuilder objBuilder;
    WebApplication objApp;
    BlazorServerService objServer;
    objBuilder = WebApplication.CreateBuilder(args);
    ConfigureServices(objBuilder.Services);
    objApp = objBuilder.Build();
    objServer.Init(objApp);
    if (objApp.Environment.IsDevelopment())
    {
      //# objApp.UseDeveloperExceptionPage();
    }
    else
    {
      objApp.UseExceptionHandler("/Error");
      objApp.UseHsts();
    }
    ConfigureRoute();
    objApp.UseHttpsRedirection();
    objApp.UseSession();
    objApp.UseStaticFiles();
    objApp.UseRouting();
    objApp.UseAuthorization();
    objApp.MapRazorPages();
    objApp.Run();
    void ConfigureRoute()
    {
      objApp.MapGet("/", PublicRoot);
      objApp.MapGet("/publicarea", PublicRoot);
      objApp.MapGet("/publicarea/{lang}", PublicRoot);
      objApp.MapGet("/privatearea", PrivateRoot);
      objApp.MapGet("/privatearea/{lang}", PrivateRoot);
      objApp.MapGet("/rest", RestRoot);
      objApp.MapGet("/rest/{lang}", RestRoot);
      objApp.MapGet("/rest/{lang}/{pageid}/{*QueryString}", RestGet);
      objApp.MapPost("/rest/{lang}/{pageid}/{*QueryString}", RestPost);
      //# TODO: Implement objApp.MapGet("/restprivate", RestRoot);
      //# objApp.MapGet("/restprivate/{lang}", RestRoot);
      //# objApp.MapGet("/restprivate/{lang}/{pageid}/{*QueryString}", RestGet);
      //# objApp.MapPost("/restprivate/{lang}/{pageid}/{*QueryString}", RestPost);
    }
    void ConfigureServices(IServiceCollection parServices)
    {
      objServer = new BlazorServerService();
      parServices.AddRazorPages();
      parServices.AddSingleton(objServer);
      parServices.AddSession(s =>
      {
        s.IdleTimeout = TimeSpan.FromDays(30);
        s.Cookie.Name = "HC4X_Cookie";
      });
    }
  }
  #region Method
  private static async Task<bool> RestPost(HttpContext parContext)
  {
    bool retValue = false;
    RestCore objRestLauncher;
    try
    {
      objRestLauncher = new RestCore(parContext, hc4x_RequestMethod.Post);
      if (objRestLauncher.Init())
        retValue = await objRestLauncher.RestPost();
    }
    catch (Exception) { throw; }
    return (retValue);
  }
  private static async Task<bool> RestGet(HttpContext parContext)
  {
    bool retValue = false;
    RestCore objRestLauncher;
    try
    {
      objRestLauncher = new RestCore(parContext, hc4x_RequestMethod.Get);
      if (objRestLauncher.Init())
        retValue = await objRestLauncher.RestGet();
    }
    catch (Exception) { throw; }
    return (retValue);
  }
  private static Task PublicRoot(HttpContext parContext) { return (GearCore.DefaultRedirect(parContext, hc4x_SiteArea.publicarea, "Index")); }
  private static Task PrivateRoot(HttpContext parContext) { return (GearCore.DefaultRedirect(parContext, hc4x_SiteArea.privatearea, "Index")); }
  private static Task RestRoot(HttpContext parContext) { return (GearCore.DefaultRedirect(parContext, hc4x_SiteArea.rest, "ready")); }
  #endregion
}