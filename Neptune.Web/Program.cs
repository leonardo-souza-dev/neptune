using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Neptune.Application;
using Neptune.Infra;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddSingleton<ITransacaoService, TransacaoService>();
builder.Services.AddSingleton<IContaService, ContaService>();
builder.Services.AddSingleton<ITransacaoRepository, TransacaoRepository>();
builder.Services.AddSingleton<IContaRepository, ContaRepository>();
builder.Services.AddSingleton<ICategoriaService, CategoriaService>();

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
