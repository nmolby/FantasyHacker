using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;

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
            Uri uri = new Uri(BASE_URL, $"schedule/games?sportId=1&date={dateString}");

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Add("User-Agent", "College Project");

            var scheduleResponse = await _client.GetStreamAsync(uri);

            return await JsonSerializer.DeserializeAsync<ScheduleResponse.Schedule>(scheduleResponse);
        }

        public async Task<ScheduleResponse.Schedule> GetSchedule(int season)
        {
            Uri uri = new Uri(BASE_URL, $"schedule/games?sportId=1&season={season}");

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Add("User-Agent", "College Project");

            var scheduleResponse = await _client.GetStreamAsync(uri);

            return await JsonSerializer.DeserializeAsync<ScheduleResponse.Schedule>(scheduleResponse);
        }

        public async Task<PersonResponse.PersonResponseRoot> GetPeopleStats(IEnumerable<int> personIds, string season)
        {
            // https://statsapi.mlb.com/api/v1/people?personIds=475253,475254&season=2018&hydrate=stats(type=gameLog,season=2018)
            Uri uri = new Uri(BASE_URL, $"people?personIds={String.Join(',', personIds)}&season={season}&hydrate=stats(type=gameLog,season={season})");

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
    }
}
