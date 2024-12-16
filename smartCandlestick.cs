using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace projectpractice
{
    public class SmartCandlestick : candlestick
    {
        // Additional properties
        public decimal Range { get; }
        public decimal BodyRange { get; }
        public decimal TopPrice { get; }
        public decimal BottomPrice { get; }
        public decimal UpperTail { get; }
        public decimal LowerTail { get; }
        public bool IsBullish { get; }
        public bool IsBearish { get; }
        public bool IsNeutral { get; }
        public bool IsMarubozu { get; }
        public bool IsHammer { get; }
        public bool IsDoji { get; }
        public bool IsDragonfly { get; }
        public bool IsGravestone { get; }

        public bool isPeak { get; set; }
        public bool isValley { get; set; }
        public bool IsBullishPeak { get; set; }
        public bool IsBullishValley { get; set; }
        public bool IsBearishPeak { get; set; }
        public bool IsBearishValley { get; set; }

        // Constructor
        public SmartCandlestick(DateTime date, decimal open, decimal high, decimal low, decimal close, long volume)
            : base(date, open, high, low, close, volume)
        {
            // Calculate values based on candlestick properties
            Range = high - low;
            BodyRange = Math.Abs(close - open);
            TopPrice = Math.Max(open, close);
            BottomPrice = Math.Min(open, close);
            UpperTail = high - TopPrice;
            LowerTail = BottomPrice - low;

            // Set other properties based on conditions
            IsBullish = close > open;
            IsBearish = open > close;
            IsNeutral = open == close;
            IsMarubozu = BodyRange == Range; // No tails, just a full body
            IsHammer = (Range > 2 * BodyRange) && (UpperTail <= BodyRange && LowerTail >= 2 * BodyRange);
            IsDoji = BodyRange <= (high - low) * 0.1m; // A doji candle is where the body is very small compared to the entire range

            // Additional conditions for Dragonfly Doji and Gravestone Doji
            IsDragonfly = IsDoji && UpperTail > 2 * BodyRange;
            IsGravestone = IsDoji && LowerTail > 2 * BodyRange;

            IsBullishPeak = false;
            IsBullishValley = false;
            IsBearishPeak = false;
            IsBearishValley = false;
        }
    }
}