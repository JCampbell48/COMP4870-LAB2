using System.Text;
using lab02b.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.SemanticKernel.ChatCompletion;

namespace lab02b.Pages

{
    public class IndexModel : PageModel
    {
        private readonly AIService _aiService;

        public IndexModel(AIService aiService)
        {
            _aiService = aiService;
        }

        [BindProperty]
        public string UserMessage { get; set; }

        public string AIResponse { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!string.IsNullOrEmpty(UserMessage))
            {
                AIResponse = await _aiService.GetAIResponseAsync(UserMessage);
            }

            return Page();
        }
    }
}
