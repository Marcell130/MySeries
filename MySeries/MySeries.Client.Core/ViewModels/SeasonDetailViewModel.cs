using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MySeries.Client.Core.Model;
using MySeries.Client.Core.Services;

namespace MySeries.Client.Core.ViewModels
{
    public class SeasonDetailViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;
        private readonly IMySeriesDataService dataService;

        ////TODO: token save
        private Token token;


        private readonly int seasonId;
        private Season _season;
        public Season Season
        {
            get => this._season;
            set => SetProperty( ref this._season, value );
        }
        public SeasonDetailViewModel( int seasonId, IMvxNavigationService navigationService, IMySeriesDataService dataService )
        {
            this.seasonId = seasonId;
            this.navigationService = navigationService;
            this.dataService = dataService;
        }

        public override async Task Initialize()
        {
            this.token = await this.dataService.TryGetTokenAsync( "gyory.marcell@gmail.com", "MyPassword!" );

            Season = await this.dataService.GetSeasonDetailsAsync( this.token, this.seasonId );
        }

        public IMvxAsyncCommand<Episode> EpisodeClickCommand => new MvxAsyncCommand<Episode>( async episode => await NavigateToEpisode( episode ) );

        private async Task NavigateToEpisode( Episode episode )
        {
            await this.navigationService.Navigate( new EpisodeDetailViewModel( episode.Id, this.dataService ) );
        }
    }
}
