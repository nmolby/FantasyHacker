using System;
using System.Linq;
using System.Threading.Tasks;

namespace FantasyHacker.Helper
{
    public class DataExporter
    {
        private MLBAPIHelper apiHelper = new MLBAPIHelper();

        public async Task<string> ExportData(bool excludePitchers = false, int sampleSize = 50)
        {

            var randomDates = await apiHelper.GetRandomDates(sampleSize);


            var csvFileName = DateTime.Now.ToString("HHmmss_d") + ".csv";
            var baseFilePath = "/Users/nmolby/Desktop/Output/";
            var fullFilePath = new Uri(new Uri(baseFilePath), csvFileName).AbsolutePath;
            var csvHelper = new MyCsvHelper(fullFilePath);

            csvHelper.WritePlayerCsvHeader();

            foreach (var date in randomDates)
            {
                foreach (var game in date.Games)
                {
                    var players = (await apiHelper.RetrievePlayerInfo(game.GamePk, game.GameDate)).Where(x => x.IsActivePlayer() && x.SeasonStatsBeforeGame.Batting.AtBats >= 50);
                    if (excludePitchers)
                    {
                        players = players.Where(x => !x.IsPitcher());
                    }
                    foreach (var player in players)
                    {
                        csvHelper.ExportToCsv(player);
                    }
                }
            }

            csvHelper.Flush();

            return csvFileName;
        }
    }
}

