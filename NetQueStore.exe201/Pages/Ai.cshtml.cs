using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace NetQueStore.exe201.Pages
{
    [IgnoreAntiforgeryToken]
    public class AiModel : PageModel
    {
        public class RequestData
        {
            public string message { get; set; }
        }

        public class NovitaResponse
        {
            public Choice[] choices { get; set; }
        }

        public class Choice
        {
            public Message message { get; set; }
        }

        public class Message
        {
            public string role { get; set; }
            public string content { get; set; }
        }

        public async Task<IActionResult> OnPostAsync([FromBody] RequestData data)
        {
            if (string.IsNullOrWhiteSpace(data?.message))
                return BadRequest(new { reply = "Tin nhắn trống." });

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", "sk_I_zE9SM9VkxUO-6xEOr7BzisMauVqEYr-bD40b9GwCs");

            var payload = new
            {
                model = "meta-llama/llama-3.1-8b-instruct",
                messages = new[]
                {
                    new { role = "system", content = "Act like you are a helpful assistant." },
                    new { role = "user", content = data.message }
                },
                max_tokens = 512
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://api.novita.ai/v3/openai/chat/completions", content);

            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return new JsonResult(new { reply = $"Lỗi API: {response.StatusCode} - {responseBody}" });
            }

            var result = JsonSerializer.Deserialize<NovitaResponse>(responseBody);
            var reply = result?.choices?[0]?.message?.content ?? "Không nhận được phản hồi hợp lệ.";

            return new JsonResult(new { reply });
        }
    }
}
