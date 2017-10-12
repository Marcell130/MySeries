using System;
using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MySeries.Client.Core.Model;
using MySeries.Client.Core.Services;

namespace MySeries.Client.Core.ViewModels
{
    public class ExploreViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;
        private readonly IMySeriesDataService dataService;

        //TODO: to resources
        private const int PageSize = 10;

        //TODO: token save
        private Token token;

        public MvxObservableCollection<TvShow> RecommendedTvShows { get; set; } = new MvxObservableCollection<TvShow>();

        public ExploreViewModel( IMvxNavigationService navigationService, IMySeriesDataService dataService )
        {
            this.navigationService = navigationService;
            this.dataService = dataService;
        }

        private bool isInitialized = false;

        public override async Task Initialize()
        {
            if (!this.isInitialized)
            {
                //TODO: token save
                this.token = await this.dataService.TryGetTokenAsync( "gyory.marcell@gmail.com", "MyPassword!" );

                //TODO: Get Recommended instead of all series
                RecommendedTvShows.Clear();
                await LoadMoreAsync();
                this.isInitialized = true;
            }
            await base.Initialize();
        }

        public IMvxAsyncCommand<TvShow> SeriesClickCommand => new MvxAsyncCommand<TvShow>( async tvShow => await NavigateToSeries( tvShow ) );

        public async Task LoadMoreAsync()
        {
            var nextPage = await this.dataService.GetAllSeriesAsync( this.token, (RecommendedTvShows.Count / PageSize) + 1, PageSize );

            RecommendedTvShows.AddRange( nextPage );
        }

        private async Task NavigateToSeries( TvShow tvShow )
        {
            await this.navigationService.Navigate( new SeriesDetailViewModel( tvShow.Id, this.navigationService, this.dataService ) );
        }
    }
}