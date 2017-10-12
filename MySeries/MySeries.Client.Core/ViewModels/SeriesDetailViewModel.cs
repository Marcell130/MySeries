using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MySeries.Client.Core.Model;
using MySeries.Client.Core.Services;

namespace MySeries.Client.Core.ViewModels
{
    public class SeriesDetailViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;
        private readonly IMySeriesDataService dataService;


        //TODO: token save
        private Token token;

        //public MvxObservableCollection<Season> Seasons { get; set; } = new MvxObservableCollection<Season>();


        private readonly int seriesId;
        private TvShow _series;
        public TvShow Series
        {
            get => this._series;
            set => SetProperty( ref this._series, value );
        }

        public SeriesDetailViewModel( int seriesId, IMvxNavigationService navigationService, IMySeriesDataService dataService )
        {
            this.seriesId = seriesId;
            this.navigationService = navigationService;
            this.dataService = dataService;
        }

        public override async Task Initialize()
        {
            //Seasons.Clear();

            this.token = await this.dataService.TryGetTokenAsync( "gyory.marcell@gmail.com", "MyPassword!" );
            Series = await this.dataService.GetSeriesDetailsAsync( this.token, this.seriesId );
            //RaiseAllPropertiesChanged();
        }

        //public IMvxCommand SeasonClickCommand => new MvxCommand<Season>( async season => { } /*await this.navigationService.Navigate<SeasonDetailViewModel, Season>( season )*/ );
        //public IMvxCommand AddSeriesCommand => new MvxCommand( async () => { }/*await this.dataService.AddTvShowForUserAsync( this.token, Series.Id ) */);

        //public override void Prepare( TvShow series )
        //{
        //    //this.Series = series;
        //}


        public IMvxAsyncCommand<Season> SeasonClickCommand => new MvxAsyncCommand<Season>( async season => await NavigateToSeason( season ) );

        private async Task NavigateToSeason( Season season )
        {
            await this.navigationService.Navigate( new SeasonDetailViewModel( season.Id, this.navigationService, this.dataService ) );
        }
    }
}
