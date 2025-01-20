using Microsoft.SemanticKernel;
using System.Text;
using Microsoft.SemanticKernel.ChatCompletion;
using OpenAI;
using System.ClientModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using lab02b.Pages;
using lab02b.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();

var modelId = config["AI:Model"]!;
var uri = config["AI:Endpoint"]!;
var githubPAT = config["AI:PAT"]!;

var client = new OpenAIClient(new ApiKeyCredential(githubPAT), new OpenAIClientOptions { Endpoint = new Uri(uri) });


var kernelBuilder = Kernel.CreateBuilder();
kernelBuilder.AddOpenAIChatCompletion(modelId, client);
var kernel = kernelBuilder.Build();


builder.Services.AddSingleton(kernel);
builder.Services.AddSingleton<IChatCompletionService>(kernel.GetRequiredService<IChatCompletionService>());

builder.Services.AddSingleton<lab02b.Services.AIService>();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();