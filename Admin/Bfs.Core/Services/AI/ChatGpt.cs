using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Bfs.Core.Services.AI
{
    public class ChatGpt
    {
        private static readonly string ChatGptBaseUrl = "https://api.openai.com";
        private static readonly string ApiKey = "OpenApi_Secret";

        public static async Task<string> SendCompletionRequest(string subject)
        {
            var reply = string.Empty;
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ChatGptBaseUrl);

                // Add Authorization header
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);

                // Request body
                var requestBody = new
                {
                    model = "gpt-5-nano",//the cheapest model
                    n =1, // number of completions to generate
                    messages = new[]
                    {
                      new { role = "system", content = "Only return the exact json requested data. Do not add introductions, explanations, or extra text." },
                      new { role = "user", content = subject }
                    }
                };

                string jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                // Send POST request
                HttpResponseMessage response = await httpClient.PostAsync("/v1/chat/completions", content);

                string responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    JObject json = JObject.Parse(responseString);
                    reply = json["choices"]?[0]?["message"]?["content"]?.ToString();
                }

                return response.IsSuccessStatusCode? reply: $@"statusCode [{response.StatusCode}] { responseString}";
            }
        }
    }

}

//Steps to Call ChatGPT Completion API in C#
    //Get API Key
    //Sign in to OpenAI
    //Copy your API key (keep it secret).

//Set Base URL
    //The endpoint is:
    //https://api.openai.com/v1/chat/completions

//Prepare Request Body
    //Example:

    //model: e.g. "gpt-4o-mini" or "gpt-4.1".
    //messages: an array with role-based messages (system, user, assistant).
    //Example JSON body:

    //{
    //    "model": "gpt-4o-mini",
    //  "messages": [
    //    { "role": "system", "content": "You are a helpful assistant." },
    //    { "role": "user", "content": "Hello, how are you?" }
    //  ]
    //}


//Send POST Request with Authorization Header

        //Use HttpClient in C#.
        //Add header: Authorization: Bearer { YourApiKey}.
        //Set Content-Type: application / json.

//Parse Response

        //The response JSON contains choices → message → content.