using Neptune.Ui.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<IPagesService, PagesService>();
builder.Services.AddScoped<IApiHelper, ApiHelper>();

//builder.Services.AddHttpClient<PagesService>(client =>
//{
//    client.BaseAddress = new Uri("https://localhost:21061");
//    client.DefaultRequestHeaders.Add("Accept", "application/+json");
//});
builder.Services.AddScoped(hc => new HttpClient { BaseAddress = new Uri("https://localhost:21061") });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
