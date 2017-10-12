using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MySeries.Client.Core.Model.Cognitive;
using Newtonsoft.Json;

namespace MySeries.Client.Core.Services
{
    public class CognitiveService : ICognitiveService
    {
        //TODO
        public const string AuthenticationTokenEndpoint = "https://api.cognitive.microsoft.com/sts/v1.0";
        public const string SpeechRecognitionEndpoint = "https://speech.platform.bing.com/speech/recognition/interactive/cognitiveservices/v1?language=en-US";
        public const string BingSpeechApiKey = "83438d089b05423784ba40b09e64cbd3";
        public const string AudioContentType = @"audio/wav; codec=""audio/pcm""; samplerate=16000";

        //public const string TextTranslatorApiKey = "<INSERT_API_KEY_HERE>";
        //public const string TextTranslatorEndpoint = "https://api.microsofttranslator.com/v2/http.svc/Translate";


        async Task<string> GetTokenAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", BingSpeechApiKey);
                    UriBuilder uriBuilder = new UriBuilder(AuthenticationTokenEndpoint);
                    uriBuilder.Path += "/issueToken";

                    var result = await client.PostAsync(uriBuilder.Uri.AbsoluteUri, null);
                    return await result.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                //TODO
                throw ex;
            }
        }

        public async Task<SpeechResult> RecognizeSpeechAsync(string filename)
        {
            // Read audio file to a stream
            var file = await PCLStorage.FileSystem.Current.LocalStorage.GetFileAsync(filename);
            var fileStream = await file.OpenAsync(PCLStorage.FileAccess.Read);

            // Send audio stream to Bing and deserialize the response
            //string requestUri = GenerateRequestUri(SpeechRecognitionEndpoint);

            //TODO
            string accessToken = await GetTokenAsync();
            var response = await SendRequestAsync(fileStream, SpeechRecognitionEndpoint, accessToken, AudioContentType);
            var speechResult = JsonConvert.DeserializeObject<SpeechResult>(response);

            fileStream.Dispose();
            return speechResult;
        }

        //string GenerateRequestUri(string speechEndpoint)
        //{
        //    string requestUri = speechEndpoint;
        //    requestUri += @"?scenarios=ulm";                                    // websearch is the other option
        //    requestUri += @"&appid=D4D52672-91D7-4C74-8AD8-42B1D98141A5";       // You must use this ID.
        //    requestUri += @"&locale=en-US";                                     // Other languages supported.
        //    requestUri += @"&version=3.0";                                      // Required value
        //    requestUri += @"&format=json";                                      // Required value
        //    requestUri += @"&instanceid=fe34a4de-7927-4e24-be60-f0629ce1d808";  // GUID for device making the request
        //    requestUri += @"&requestid=" + Guid.NewGuid();                      // GUID for the request
        //    return requestUri;
        //}

        async Task<string> SendRequestAsync(Stream fileStream, string url, string bearerToken, string contentType)
        {
            var content = new StreamContent(fileStream);
            content.Headers.TryAddWithoutValidation("Content-Type", contentType);

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                var response = await httpClient.PostAsync(url, content);

                var responseString = await response.Content.ReadAsStringAsync();
                return responseString;
            }
        }


    }

}
