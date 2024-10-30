using MudBlazor.Services;
using Westwind.AspNetCore.Markdown;
using YouTubeGPT.Client;
using YouTubeGPT.Client.Components;
using YouTubeGPT.Ingestion;
using YouTubeGPT.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<MetadataDbContext>(ServiceNames.MetadataDB);

builder.AddSemanticKernel();
builder.AddSemanticKernelMemory();
builder.AddSemanticKernelPlugins();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddMudServices();

builder.Services.AddMarkdown();

builder.Services.AddScoped<BuildVectorDatabaseOperationHandler>();

// Add support for user profiles with the ability to save preferences and settings
builder.Services.AddScoped<UserProfileService>();

// Provide options for users to upload and use custom avatars in the chat
builder.Services.AddScoped<AvatarService>();

var app = builder.Build();

app.MapDefaultEndpoints();

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
    .AddInteractiveServerRenderMode();

app.Run();
