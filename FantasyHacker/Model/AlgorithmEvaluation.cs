using System;
using System.Linq;
using System.Collections.Generic;
using FantasyHacker.Interface;
using Extreme.Statistics;

namespace FantasyHacker.Model
{
    public class AlgorithmEvaluation<IFantasyPlayer>
    {
        public IAlgorithm<IFantasyPlayer> algorithm;
        public Dictionary<ScheduleResponse.Game, decimal> correlations;
        public double averageCorrelation => (double) correlations.Values.Average();
        public double stdCorrelation => Stats.StandardDeviation(correlations.Values.Select(x => decimal.ToDouble(x)));
        public (double, double) confidenceInterval => (-1.96 * stdCorrelation + averageCorrelation, 1.96 * stdCorrelation + averageCorrelation);

        public AlgorithmEvaluation (IAlgorithm<IFantasyPlayer> algorithm, Dictionary<ScheduleResponse.Game, decimal> correlations = null)
        {
            this.algorithm = algorithm;
            this.correlations = correlations ?? new Dictionary<ScheduleResponse.Game, decimal>();
        }

        public void AddCorrelation (ScheduleResponse.Game game, decimal correlation)
        {
            correlations.Add(game, correlation);
        }

        public void AddCorrelations (Dictionary<ScheduleResponse.Game, decimal> correlations)
        {
            foreach (var correlation in correlations)
            {
                this.correlations.Add(correlation.Key, correlation.Value);
            }
        }
    }
}

