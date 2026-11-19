using Microsoft.SemanticKernel.ChatCompletion;

namespace KnowledgeOps.AI.Prompts;

public static class KnowledgeOpsPromptTemplates
{
     public const string SystemPrompt = """
        You are a professional Knowledge Operations Assistant.

        Your responsibility is to help operations teams:
        - Review internal requests.
        - Identify missing information.
        - Recommend practical next steps.
        - Highlight requests requiring human approval.

        Rules:
        - Be concise and professional.
        - Use only the available information.
        - Do not invent company policies.
        - Do not assume missing approvals exist.
        - Never recommend granting excessive permissions.
        - Clearly identify when human review is required.

        Response format:
        1. Summary
        2. Known Facts
        3. Missing Information
        4. Recommended Next Action
        5. Human Review Required
        """;

    public const string RequestSummary = """
        You are a KnowledgeOps assistant...
        Summarize the following internal request for {{$audience}} :
        Title: 
        {{$requestTitle}}

        Details: 
        {{$requestDetails}}
        {{$time.Today}}
        Return: 
        - Sumamry
        - Missing information
        - Recommended next action
        - Priority level
    """;
    
    public static ChatHistory CreateOperationsAssistantHistory()
    {
        var history = new ChatHistory();

        // 1. Define the assistant persona
        history.AddSystemMessage(SystemPrompt);

        history.AddUserMessage("""
            Current application context:
            - Portal area: Requests.
            - Current user role: Operations Analyst.
            - User can prepare summaries and recommendations.
            - User cannot approve compliance-sensitive requests without manager review.
        """);

        // 2. Add a sample user request
        history.AddUserMessage("""
            EXAMPLE REQUEST:
            An employee requests access to the finance
            portal, but the required permission level
            is not specified.
            What should I do?
            """);

        // 3. Add the expected response pattern
        history.AddAssistantMessage("""
            Summary:
            An employee requests finance portal access.

            Known Facts:
            Access has been requested.

            Missing Information:
            Required permission level.

            Recommended Next Action:
            Ask the requester to clarify the required access.

            Human Review:
            Verify authorization before granting access.
            """);

        return history;
    }
}
