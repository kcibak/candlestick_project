/*
Class Name: SmartCandlestick
Purpose: Extends the basic candlestick class with advanced technical analysis capabilities
Description: 
    This class enhances the basic candlestick by adding pattern recognition and 
    technical analysis features. It automatically calculates and identifies various
    candlestick patterns such as Doji, Hammer, Marubozu, etc., and provides additional
    metrics like price ranges and tail measurements. It also supports the identification
    of price peaks and valleys for trend analysis.

Patterns Detected:
    - Bullish/Bearish patterns
    - Doji variations (standard, dragonfly, gravestone)
    - Hammer patterns
    - Marubozu (full-bodied candlestick)
    - Price peaks and valleys
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace projectpractice
{
    /// <summary>
    /// Advanced candlestick class that provides automatic pattern recognition and technical analysis features
    /// Inherits from the base candlestick class
    /// </summary>
    public class SmartCandlestick : candlestick
    {
        #region Price Range Properties
        /// <summary>
        /// Total price range of the candlestick (High - Low)
        /// </summary>
        public decimal Range { get; private set; }

        /// <summary>
        /// Range of the candlestick's body (absolute difference between Open and Close)
        /// </summary>
        public decimal BodyRange { get; private set; }

        /// <summary>
        /// Higher price between Open and Close
        /// </summary>
        public decimal TopPrice { get; private set; }

        /// <summary>
        /// Lower price between Open and Close
        /// </summary>
        public decimal BottomPrice { get; private set; }

        /// <summary>
        /// Length of the upper shadow/wick (High - TopPrice)
        /// </summary>
        public decimal UpperTail { get; private set; }

        /// <summary>
        /// Length of the lower shadow/wick (BottomPrice - Low)
        /// </summary>
        public decimal LowerTail { get; private set; }
        #endregion

        #region Pattern Properties
        /// <summary>
        /// Indicates if the candlestick is bullish (Close > Open)
        /// </summary>
        public bool IsBullish { get; private set; }

        /// <summary>
        /// Indicates if the candlestick is bearish (Open > Close)
        /// </summary>
        public bool IsBearish { get; private set; }

        /// <summary>
        /// Indicates if the candlestick is neutral (Open = Close)
        /// </summary>
        public bool IsNeutral { get; private set; }

        /// <summary>
        /// Indicates if the candlestick is a Marubozu (no shadows/wicks)
        /// </summary>
        public bool IsMarubozu { get; private set; }

        /// <summary>
        /// Indicates if the candlestick forms a hammer pattern (long lower shadow)
        /// </summary>
        public bool IsHammer { get; private set; }

        /// <summary>
        /// Indicates if the candlestick is a Doji (very small body)
        /// </summary>
        public bool IsDoji { get; private set; }

        /// <summary>
        /// Indicates if the candlestick is a Dragonfly Doji (Doji with long lower shadow)
        /// </summary>
        public bool IsDragonfly { get; private set; }

        /// <summary>
        /// Indicates if the candlestick is a Gravestone Doji (Doji with long upper shadow)
        /// </summary>
        public bool IsGravestone { get; private set; }

        /// <summary>
        /// Indicates if this candlestick represents a local price peak
        /// </summary>
        public bool IsPeak { get; set; }

        /// <summary>
        /// Indicates if this candlestick represents a local price valley
        /// </summary>
        public bool IsValley { get; set; }
        #endregion

        /// <summary>
        /// Initializes a new instance of the SmartCandlestick class and calculates all pattern properties
        /// </summary>
        /// <param name="date">Trading period date</param>
        /// <param name="open">Opening price</param>
        /// <param name="high">Highest price</param>
        /// <param name="low">Lowest price</param>
        /// <param name="close">Closing price</param>
        /// <param name="volume">Trading volume</param>
        public SmartCandlestick(DateTime date, decimal open, decimal high, decimal low, decimal close, long volume)
            : base(date, open, high, low, close, volume)
        {
            // Calculate basic price ranges
            Range = high - low;
            BodyRange = Math.Abs(close - open);
            TopPrice = Math.Max(open, close);
            BottomPrice = Math.Min(open, close);
            UpperTail = high - TopPrice;
            LowerTail = BottomPrice - low;

            // Determine basic candlestick types
            IsBullish = close > open;    // Closing price higher than opening
            IsBearish = open > close;    // Opening price higher than closing
            IsNeutral = open == close;   // Opening and closing prices equal

            // Identify specific patterns
            IsMarubozu = BodyRange == Range;     // No shadows/wicks

            // Hammer pattern: small body with long lower shadow
            IsHammer = (Range > 2 * BodyRange) && (UpperTail <= BodyRange && LowerTail >= 2 * BodyRange);

            // Doji patterns: very small body relative to total range
            IsDoji = BodyRange <= (high - low) * 0.1m;
            IsDragonfly = IsDoji && UpperTail > 2 * BodyRange;    // Doji with long upper shadow
            IsGravestone = IsDoji && LowerTail > 2 * BodyRange;   // Doji with long lower shadow

            // Initialize trend identification properties
            IsPeak = false;    // Will be set by external analysis
            IsValley = false;  // Will be set by external analysis
        }
    }
}
