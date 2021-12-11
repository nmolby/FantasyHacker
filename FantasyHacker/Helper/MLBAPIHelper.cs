using System;
using System.Net.Http;
using System.Threading.Tasks;
using FantasyHacker.Model;

using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using FantasyHacker.Interface;

namespace FantasyHacker.Helper
{
    public class MLBAPIHelper
    {
        private HttpClient _client = new HttpClient();
        private Uri BASE_URL = new Uri("https://statsapi.mlb.com/api/v1/");

        public async Task<BoxScoreResponse.BoxScore> GetBoxScore(int gameId)
        {

            Uri uri = new Uri(BASE_URL, $"game/{gameId}/boxscore");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Add("User-Agent", "College Project");

            var gameResponseTask = _client.GetStreamAsync(uri);

            var gameResponse = await gameResponseTask;

            return await JsonSerializer.DeserializeAsync<BoxScoreResponse.BoxScore>(gameResponse);
        }

        public async Task<ScheduleResponse.Schedule> GetSchedule(DateTime date)
        {
            string dateString = date.ToString("yyyy-MM-dd");
            Uri uri = new Uri(BASE_URL, $"schedule/games?sportId=1&date={dateString}&gameType=R");

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Add("User-Agent", "College Project");

            var scheduleResponse = await _client.GetStreamAsync(uri);

            return await JsonSerializer.DeserializeAsync<ScheduleResponse.Schedule>(scheduleResponse);
        }

        public async Task<ScheduleResponse.Schedule> GetSchedule(DateTime startDate, DateTime endDate)
        { 
            //https://statsapi.mlb.com/api/v1/schedule/games/?sportId=1&startDate=2021-06-07&endDate=2021-09-14

            string startDateString = startDate.ToString("yyyy-MM-dd");
            string endDateString = endDate.ToString("yyyy-MM-dd");

            Uri uri = new Uri(BASE_URL, $"schedule/games?sportId=1&startDate={startDateString}&endDate={endDateString}&gameType=R");

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Add("User-Agent", "College Project");

            var scheduleResponse = await _client.GetStreamAsync(uri);

            return await JsonSerializer.DeserializeAsync<ScheduleResponse.Schedule>(scheduleResponse);
        }

        public async Task<ScheduleResponse.Schedule> GetSchedule(int season)
        {
            Uri uri = new Uri(BASE_URL, $"schedule/games?sportId=1&season={season}&gameType=R");

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Add("User-Agent", "College Project");

            var scheduleResponse = await _client.GetStreamAsync(uri);

            return await JsonSerializer.DeserializeAsync<ScheduleResponse.Schedule>(scheduleResponse);
        }

        public async Task<PersonResponse.PersonResponseRoot> GetPeopleStats(IEnumerable<int> personIds, int season)
        {
            // https://statsapi.mlb.com/api/v1/people?personIds=475253,475254&season=2018&hydrate=stats(type=gameLog,season=2018)
            Uri uri = new Uri(BASE_URL, $"people?personIds={String.Join(',', personIds)}&season={season}&hydrate=stats(type=gameLog,season={season},gameType=R)");

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Add("User-Agent", "College Project");

            var personResponse = await _client.GetStreamAsync(uri);

            PersonResponse.PersonResponseRoot personResponseRoot = await JsonSerializer.DeserializeAsync<PersonResponse.PersonResponseRoot>(personResponse);

            return personResponseRoot;
        }

        public async Task<LiveFeed.Root> GetLiveFeed(int gameId)
        {
            // https://statsapi.mlb.com/api/v1.1/game/632580/feed/live
            var updatedBaseUrl = new Uri("https://statsapi.mlb.com/api/v1.1/");
            Uri uri = new Uri(updatedBaseUrl, $"game/{gameId}/feed/live");

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Add("User-Agent", "College Project");

            var liveFeedResponse = await _client.GetStreamAsync(uri);

            var liveFeedRoot = await JsonSerializer.DeserializeAsync<LiveFeed.Root>(liveFeedResponse);

            return liveFeedRoot;
        }

        public async Task<IEnumerable<ScheduleResponse.Date>> GetRandomDates(int quantity)
        {
            var datesOfInterest = new List<ScheduleResponse.Date>();

            List<Task<ScheduleResponse.Schedule>> schedulesOfInterest = new List<Task<ScheduleResponse.Schedule>>() {
                GetSchedule(2021),
                GetSchedule(2020),
                GetSchedule(2019)
            };

            while (schedulesOfInterest.Any())
            {
                var schedule = await Task.WhenAny(schedulesOfInterest);
                schedulesOfInterest.Remove(schedule);
                datesOfInterest.AddRange((await schedule).Dates);
            }

            var rnd = new Random();
            var randomDates = datesOfInterest.OrderBy(x => rnd.Next()).Take(quantity);
            return randomDates;
        }

        public async Task<List<MLBPlayer>> RetrievePlayerInfo(int gameId, DateTime date)
        {
            var boxScore = await GetBoxScore(gameId);
            var players = new List<MLBPlayer>();

            foreach (IFantasyPlayer player in boxScore.GetPlayers())
            {
                try
                {
                    var newPlayer = new MLBPlayer(player.Person.Id, date);
                    newPlayer.IngestBoxScore(boxScore, gameId);
                    players.Add(newPlayer);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Injesting box score failed for {player.Person.FullName}. Error {e}");
                }

            }
            var seasonStats = await GetPeopleStats(players.Select(x => x.PlayerId), date.Year);

            foreach (var player in players)
            {
                try
                {
                    player.IngestSeasonStats(seasonStats);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Injesting season score failed for {player.Person.FullName}. Error {e}");
                }
            }
            return players;
        }
    }
}
