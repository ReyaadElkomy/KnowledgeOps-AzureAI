using Microsoft.SemanticKernel.ChatCompletion;

namespace KnowledgeOps.AI.Services;

public interface IKnoledgeOpsChatClient
{
    Task<string> ReplyAsync(ChatHistory history, CancellationToken cancellationToken = default);
    Task<string> GetCurrentDateAsync(CancellationToken cancellationToken = default);
    Task<string> AskWithPromptAsync(string prompt, CancellationToken cancellationToken = default);
    Task<string> SummrizeRequestAsync(string requestTitle, string requestDetails, string audience, CancellationToken cancellationToken = default);

    Task<string> CreateOperationsBriefAsync(string requestDetails, CancellationToken cancellationToken = default);

}

