using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Navigation;
using MySeries.Client.Core.Services;
using MySeries.Client.Core.ViewModels;

namespace MySeries.Client.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;

        private readonly ExploreViewModel exploreViewModel;
        private readonly UpcomingViewModel upcomingViewModel;
        private readonly ProfileViewModel profileViewModel;

        public MainViewModel( IMvxNavigationService navigationService, IMySeriesDataService dataService, IAudioRecorderService recorderService, ICognitiveService cognitiveService )
        {
            this.navigationService = navigationService;
            this.exploreViewModel = new ExploreViewModel( navigationService, dataService );
            this.upcomingViewModel = new UpcomingViewModel( dataService );
            this.profileViewModel = new ProfileViewModel( recorderService, cognitiveService );
        }

        //public IMvxAsyncCommand ShowExploreFragment => new MvxAsyncCommand( async () => await navigationService.Navigate<ExploreViewModel>() );

        //public IMvxAsyncCommand ShowUpcomingFragment => new MvxAsyncCommand( async () => await navigationService.Navigate<UpcomingViewModel>() );

        //public IMvxAsyncCommand ShowProfileFragment => new MvxAsyncCommand( async () => await navigationService.Navigate<ProfileViewModel>() );


        public async Task ShowExploreFragment()
        {
            await this.navigationService.Navigate( exploreViewModel );
        }
        public async Task ShowUpcomingFragment()
        {
            await this.navigationService.Navigate( upcomingViewModel );
        }
        public async Task ShowProfileFragment()
        {
            await this.navigationService.Navigate( profileViewModel );
        }

        public override async Task Initialize()
        {
            await this.navigationService.Navigate( this.exploreViewModel );
        }
    }
}
