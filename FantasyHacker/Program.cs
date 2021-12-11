using System;
using System.Text.Json;
using System.Linq;
using FantasyHacker.Interface;
using FantasyHacker.Helper;
using System.Threading.Tasks;
using FantasyHacker.Model;
using System.Collections.Generic;

namespace FantasyHacker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var fileName = await ExportData();
            Console.WriteLine(fileName);

            await EvaluateAlgorithm(new SlgRunsAlgorithm());

        }

        static async Task<string> ExportData(bool excludePitchers = true, int sampleSize = 50)
        {
            var dataExporter = new DataExporter();

            return await dataExporter.ExportData(excludePitchers, sampleSize);
        }

        static async Task EvaluateAlgorithm(IAlgorithm<MLBPlayer> algorithm, int sampleSize = 10)
        {
            var algorithmEvaluator = new AlgorithmEvaluator();
            var randAlgEval = await algorithmEvaluator.EvaluateAlgorithm(new RandomAlgorithm(), sampleSize);
            var inputAlgEval = await algorithmEvaluator.EvaluateAlgorithm(algorithm, sampleSize);
            Console.WriteLine($"Random Algorithm Average: {randAlgEval.averageCorrelation}. Confidence Interval: {randAlgEval.confidenceInterval}. Std. Deviation: {randAlgEval.stdCorrelation}");
            Console.WriteLine($"Input Algorithm Average: {inputAlgEval.averageCorrelation}. Confidence Interval: {inputAlgEval.confidenceInterval}. Std. Deviation: {inputAlgEval.stdCorrelation}");
        }


    }
}
