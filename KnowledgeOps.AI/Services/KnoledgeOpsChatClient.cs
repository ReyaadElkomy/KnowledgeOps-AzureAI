using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace KnowledgeOps.AI.Services;

public sealed class KnoledgeOpsChatClient(IChatCompletionService chatCompletionService, 
    Kernel kernel) : IKnoledgeOpsChatClient
{
    public async Task<string> GetCurrentDateAsync(CancellationToken cancellationToken = default)
    {
        var result = await kernel.InvokeAsync("Time", "Today", cancellationToken: cancellationToken);
        return result.ToString();
    }

    public async Task<string> ReplyAsync(ChatHistory history, CancellationToken cancellationToken = default)
    {
        var settings = new OpenAIPromptExecutionSettings
        {
            FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
        };

        var result = await chatCompletionService.GetChatMessageContentAsync(
                    history, 
                    executionSettings: settings,
                    kernel: kernel, 
                    cancellationToken: cancellationToken);
                    
        return result.Content ?? string.Empty;
    }

    
}
