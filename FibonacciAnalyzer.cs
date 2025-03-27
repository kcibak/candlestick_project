using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectpractice
{
    public class FibonacciWave
    {
        public SmartCandlestick Start { get; set; }
        public SmartCandlestick End { get; set; }
        public decimal WaveHeight => Math.Abs(End.High - Start.Low);
        public bool IsUpTrend => End.High > Start.Low;
    }

    public class FibonacciAnalyzer
    {
        private const double MinWaveHeightPercentage = 0.05; // 5% minimum price movement
        private readonly List<decimal> RetracementLevels = new List<decimal>
            { 0m, 0.236m, 0.382m, 0.5m, 0.618m, 0.764m, 1m };
        private readonly List<decimal> ExtensionLevels = new List<decimal>
            { 1.272m, 1.618m };

        public List<FibonacciWave> IdentifyValidWaves(List<SmartCandlestick> candles)
        {
            var waves = new List<FibonacciWave>();
            SmartCandlestick currentStart = null;

            for (int i = 1; i < candles.Count; i++)
            {
                var current = candles[i];
                var previous = candles[i - 1];

                // Detect new wave start
                if (current.IsPeak && currentStart == null)
                {
                    currentStart = current;
                }
                else if (current.IsValley && currentStart != null)
                {
                    if (IsValidWave(currentStart, current))
                    {
                        waves.Add(new FibonacciWave { Start = currentStart, End = current });
                        currentStart = null;
                    }
                }
            }
            return waves;
        }

        public bool IsValidWave(SmartCandlestick start, SmartCandlestick end)
        {
            if (start == null || end == null) return false;
            return Math.Abs(end.High - start.Low) >= start.Low * 0.05m;
        }

        public Dictionary<decimal, decimal> CalculateFibonacciLevels(FibonacciWave wave)
        {
            var levels = new Dictionary<decimal, decimal>();
            decimal basePrice = wave.IsUpTrend ? wave.Start.Low : wave.Start.High;
            decimal waveHeight = wave.WaveHeight;

            foreach (var level in RetracementLevels)
            {
                decimal price = basePrice + (waveHeight * level);
                levels[level] = price;
            }

            foreach (var level in ExtensionLevels)
            {
                decimal price = basePrice + (waveHeight * level);
                levels[level] = price;
            }

            return levels;
        }

        public decimal CalculateBeautyScore(SmartCandlestick candle,
            Dictionary<decimal, decimal> fibLevels,
            Dictionary<string, decimal> patternWeights)
        {
            decimal score = 0;
            const decimal threshold = 0.005m; // 0.5% threshold

            foreach (var fibLevel in fibLevels.Values)
            {
                // Check all price points against Fibonacci level
                score += GetConfirmationScore(candle.Open, fibLevel, threshold);
                score += GetConfirmationScore(candle.High, fibLevel, threshold);
                score += GetConfirmationScore(candle.Low, fibLevel, threshold);
                score += GetConfirmationScore(candle.Close, fibLevel, threshold);
            }

            // Add pattern recognition weights
            if (candle.IsHammer) score += patternWeights["Hammer"];
            if (candle.IsDoji) score += patternWeights["Doji"];
            if (candle.IsPeak || candle.IsValley) score += patternWeights["Reversal"];

            return score;
        }

        private decimal GetConfirmationScore(decimal price, decimal fibLevel, decimal threshold)
        {
            decimal difference = Math.Abs(price - fibLevel);
            return difference <= (fibLevel * threshold) ? 1 : 0;
        }

        public Dictionary<decimal, decimal> ProjectBeautyScores(
            FibonacciWave wave,
            decimal priceStart,
            decimal priceEnd,
            decimal step,
            Dictionary<string, decimal> patternWeights)
        {
            var scores = new Dictionary<decimal, decimal>();
            var fibLevels = CalculateFibonacciLevels(wave);

            for (decimal price = priceStart; price <= priceEnd; price += step)
            {
                decimal score = 0;
                foreach (var fibLevel in fibLevels.Values)
                {
                    score += GetConfirmationScore(price, fibLevel, 0.005m);
                }
                scores[price] = score;
            }

            return scores;
        }
    }
}
