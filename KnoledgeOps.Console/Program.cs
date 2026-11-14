using KnowledgeOps.AI;
using KnowledgeOps.AI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

var builder = Host.CreateApplicationBuilder(args);
builder.Configuration.AddUserSecrets<Program>(optional: false);
builder.Services.AddKnowledgeOpsAI(builder.Configuration);
using var host = builder.Build();

// working on chat history
var chat = host.Services.GetRequiredService<IKnoledgeOpsChatClient>();
var history = new ChatHistory("You are a concise, helpful assistance for bussiness app demos!");
Console.WriteLine("Type message. Type exit to quit.");

while (true)
{
    Console.Write("User :: ");
    var input = Console.ReadLine();

    if(string.IsNullOrWhiteSpace(input) || input.Equals("exit", StringComparison.OrdinalIgnoreCase))
        break;

    if(input.Equals("/today", StringComparison.OrdinalIgnoreCase))
    {
        var today = await chat.GetCurrentDateAsync();
        Console.WriteLine($"System :: {today}");
        continue;
    }

    history.AddUserMessage(input);
    var response = await chat.ReplyAsync(history);

    history.AddAssistantMessage(response);
    Console.WriteLine($"Assistant :: {response}");

}

// var config = new ConfigurationBuilder()
//     .AddUserSecrets<Program>()
//     .Build();

// var deploymentName = config["AzureOpenAI:DeploymentName"]
//     ?? throw new InvalidOperationException("Missing AzureOpenAI:DeploymentName");

// var endpoint = config["AzureOpenAI:Endpoint"]
//     ?? throw new InvalidOperationException("Missing AzureOpenAI:Endpoint");

// var apiKey = config["AzureOpenAI:ApiKey"]
//     ?? throw new InvalidOperationException("Missing AzureOpenAI:ApiKey");


// var builder = Kernel.CreateBuilder();
// builder.AddAzureOpenAIChatCompletion(deploymentName, endpoint, apiKey);

// builder.Services.AddLogging(logging => 
//     logging.AddConsole()
//     .SetMinimumLevel(LogLevel.Information));

// Kernel kernel = builder.Build();

// Console.Write("User > ");
// var userInput = Console.ReadLine();

// if(string.IsNullOrWhiteSpace(userInput))
//     return;

// var result = await kernel.InvokePromptAsync(userInput);
// Console.WriteLine($"Assistant > {result}");

