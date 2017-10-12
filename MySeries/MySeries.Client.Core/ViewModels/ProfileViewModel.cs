using MvvmCross.Core.ViewModels;
using MySeries.Client.Core.Services;

namespace MySeries.Client.Core.ViewModels
{
    public class ProfileViewModel : MvxViewModel
    {

        private IAudioRecorderService recorderService;
        private ICognitiveService cognitiveService;


        private string text;

        public string Text
        {
            get => this.text;
            set => SetProperty(ref this.text, value);
        }


        private bool _isRecording;
        public bool IsRecording
        {
            get => this._isRecording;
            set => SetProperty(ref this._isRecording, value);
        }

        public ProfileViewModel(IAudioRecorderService recorderService, ICognitiveService cognitiveService)
        {
            this.recorderService = recorderService;
            this.cognitiveService = cognitiveService;
        }

        public IMvxCommand RecordCommand => new MvxCommand(async () =>
        {
            if (IsRecording)
            {
                var filename = this.recorderService.StopRecording();
                var result = await this.cognitiveService.RecognizeSpeechAsync(filename);
                Text = result.DisplayText;

                IsRecording = false;
            }
            else
            {
                Text = "";
                this.recorderService.StartRecording();

                IsRecording = true;
            }
        });

    }
}
