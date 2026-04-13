using Microsoft.EntityFrameworkCore;
using MyVoice.Components;
using MyVoice.Data;
using MyVoice.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add controllers for API endpoints
builder.Services.AddControllers();

// Add SQLite database context
builder.Services.AddDbContext<VoiceDbContext>(options =>
    options.UseSqlite("Data Source=voices.db"));

// Add voice recording service
builder.Services.AddScoped<IVoiceRecordingService, VoiceRecordingService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// Map API controller routes
app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Initialize database
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<VoiceDbContext>();
    db.Database.EnsureCreated();
}

app.Run();
