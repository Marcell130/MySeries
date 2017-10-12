using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MySeries.Client.Core.Model;
using MySeries.Client.Core.Services;

namespace MySeries.Client.Core.ViewModels
{
    public class EpisodeDetailViewModel : MvxViewModel
    {
        private readonly IMySeriesDataService dataService;

        ////TODO: token save
        private Token token;

        private readonly int episodeId;
        private Episode _episode;
        public Episode Episode
        {
            get => this._episode;
            set => SetProperty( ref this._episode, value );
        }
        public EpisodeDetailViewModel( int episodeId, IMySeriesDataService dataService )
        {
            this.episodeId = episodeId;
            this.dataService = dataService;
        }

        public override async Task Initialize()
        {
            this.token = await this.dataService.TryGetTokenAsync( "gyory.marcell@gmail.com", "MyPassword!" );

            Episode = await this.dataService.GetEpisodeDetailsAsync( this.token, this.episodeId );
        }
    }
}
