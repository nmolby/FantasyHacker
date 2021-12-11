using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using FantasyHacker.Model;

namespace FantasyHacker.Helper
{
    public class MyCsvHelper
    {
        CsvWriter csvWriter;
        List<int> GamesBackToRecord = new List<int> { -1, 3, 5, 10, 20, 30 };

        public MyCsvHelper(string filePath)
        {
            var streamWriter = new StreamWriter(filePath);
            csvWriter = new CsvWriter(streamWriter, CultureInfo.CurrentCulture);
        }

        public void Flush()
        {
            csvWriter.Flush();
        }


        public void WritePlayerCsvHeader()
        {
            var statsPlaceholder = new MlbStats();

            csvWriter.WriteField("Player Id");
            csvWriter.WriteField("Game Id");

            foreach (var gamesBack in GamesBackToRecord)
            {
                string valueToAdd;

                if (gamesBack == -1)
                {
                    valueToAdd = "Season ";
                }
                else
                {
                    valueToAdd = $"{gamesBack} Game ";
                }

                foreach (var keyValue in statsPlaceholder.labelCounts)
                {
                    csvWriter.WriteField(valueToAdd + keyValue.Key);
                }
                foreach (var keyValue in statsPlaceholder.labelCalcullations)
                {
                    csvWriter.WriteField(valueToAdd + keyValue.Key);
                }
            }

            csvWriter.WriteField("Score");
        }

        public void ExportToCsv(MLBPlayer player)
        {
            csvWriter.NextRecord();
            csvWriter.WriteField(player.PlayerId);
            csvWriter.WriteField(player.GameId);

            foreach (var gamesBack in GamesBackToRecord)
            {
                try
                {
                    var stats = player.GetStats(gamesBack);
                    foreach (var keyValue in stats.labelCounts)
                    {
                        csvWriter.WriteField(keyValue.Value);
                    }
                    foreach (var keyValue in stats.labelCalcullations)
                    {
                        csvWriter.WriteField(keyValue.Value);
                    }
                } catch (Exception e)
                {
                    Console.WriteLine($"EVERYTHING BROKE FOR {player.PlayerId} on {player.DateOfGame}");
                }


            }

            csvWriter.WriteField(player.Score());
        }
    }
}

