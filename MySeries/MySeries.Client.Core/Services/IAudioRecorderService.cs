namespace MySeries.Client.Core.Services
{
    public interface IAudioRecorderService
    {
        void StartRecording();
        string StopRecording();
    }
}