using Microsoft.SemanticKernel.ChatCompletion;

namespace KnowledgeOps.AI.Services;

public interface IKnoledgeOpsChatClient
{
    Task<string> ReplyAsync(ChatHistory history, CancellationToken cancellationToken = default);
    Task<string> GetCurrentDateAsync(CancellationToken cancellationToken = default);
}

