using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MySeries.Client.Core.Helpers;
using MySeries.Client.Core.Model;
using MySeries.Client.Core.Services;

namespace MySeries.Client.Core.ViewModels
{
    public class UpcomingViewModel : MvxViewModel
    {

        private readonly IMySeriesDataService dataService;

        //TODO token save
        private Token token;
        public ObservableCollection<Group<DateTime?, Episode>> GroupedEpisodes { get; set; } = new ObservableCollection<Group<DateTime?, Episode>>();


        public UpcomingViewModel(IMySeriesDataService dataService)
        {
            this.dataService = dataService;

            Task.Run(async () =>
            {
                this.token = await this.dataService.TryGetTokenAsync("gyory.marcell@gmail.com", "MyPassword!");
            }).Wait();
        }

        private bool isInitialized = false;

        public override async Task Initialize()
        {
            //if (!this.isInitialized)
            //{
            //var groups = await GetEpisodeGroupsAsync(DateTime.Today, DateTime.Today.AddMonths(1));

            //TODO
            var start = new DateTime(2015, 1, 1);
            var end = new DateTime(2015, 3, 1);
            var groups = await GetEpisodeGroupsAsync(start, end);

            GroupedEpisodes.Clear();
            foreach (var group in groups)
            {
                GroupedEpisodes.Add(group);
            }

            this.isInitialized = true;
            //}
            await base.Initialize();
        }

        public async Task<List<Group<DateTime?, Episode>>> GetEpisodeGroupsAsync(DateTime startInterval, DateTime endInterval)
        {
            var upcomingEpisodes = await this.dataService.GetEpisodesInInterval(this.token, startInterval, endInterval);

            var groups = upcomingEpisodes.GroupBy(e => e.AirDate).Select(g => new Group<DateTime?, Episode>(g.Key, g)).OrderBy(g => g.Key).ToList();

            return groups;
        }
    }
}