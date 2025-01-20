using System;
using Microsoft.SemanticKernel.ChatCompletion;
using System.Text;
using System.Threading.Tasks;

namespace lab02b.Services;

public class AIService
{
    private readonly IChatCompletionService _chatCompletionService;

    public AIService(IChatCompletionService chatCompletionService)
    {
        _chatCompletionService = chatCompletionService;
    }

    public async Task<string> GetAIResponseAsync(string userMessage)
    {
        ChatHistory chat = new(@"
            You are an AI assistant that helps people find information. 
            The response must be brief and should not exceed one paragraph.
            If you do not know the answer then simply say 'I do not know the answer'."
        );

        chat.AddUserMessage(userMessage);

        StringBuilder strBuilder = new();
        await foreach (var message in _chatCompletionService.GetStreamingChatMessageContentsAsync(chat))
        {
            strBuilder.Append(message.Content);
        }

        return strBuilder.ToString();
    }
}