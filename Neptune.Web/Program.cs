using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Neptune.Application;
using Neptune.Infra;
using Neptune.Web.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddSingleton<PagesService>();
builder.Services.AddSingleton<ITransacaoService, TransacaoService>();
builder.Services.AddSingleton<IContaService, ContaService>();
builder.Services.AddSingleton<ITransacaoRepository, TransacaoRepository>();
builder.Services.AddSingleton<IContaRepository, ContaRepository>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
