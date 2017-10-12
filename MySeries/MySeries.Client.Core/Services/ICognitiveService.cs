using System.Threading.Tasks;
using MySeries.Client.Core.Model.Cognitive;

namespace MySeries.Client.Core.Services
{
    public interface ICognitiveService
    {
        Task<SpeechResult> RecognizeSpeechAsync(string filename);
    }
}