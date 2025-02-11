/*
Class Name: candlestick
Purpose: Represents a single candlestick in financial market data visualization
Description: 
    This class models a single trading period's data in the financial markets,
    containing all essential price points (open, high, low, close) and trading volume.
    It serves as the fundamental data structure for stock market analysis and visualization,
    particularly in candlestick charts.

Usage:
    - Created for each trading period (typically daily) when parsing stock market data
    - Used as the basic building block for stock analysis and chart rendering
    - Can be aggregated into lists/collections for time-series analysis
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectpractice
{
    /// <summary>
    /// Represents a single candlestick in financial market data,
    /// containing price and volume information for a specific time period
    /// </summary>
    public class candlestick
    {
        /// <summary>
        /// The date and time of this trading period
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The opening price of the trading period
        /// </summary>
        public decimal Open { get; set; }

        /// <summary>
        /// The highest price reached during the trading period
        /// </summary>
        public decimal High { get; set; }

        /// <summary>
        /// The lowest price reached during the trading period
        /// </summary>
        public decimal Low { get; set; }

        /// <summary>
        /// The closing price of the trading period
        /// </summary>
        public decimal Close { get; set; }

        /// <summary>
        /// The total number of shares/units traded during this period
        /// </summary>
        public long Volume { get; set; }

        /// <summary>
        /// Initializes a new instance of the candlestick class with specified values
        /// </summary>
        /// <param name="date">The date of the trading period</param>
        /// <param name="open">The opening price</param>
        /// <param name="high">The highest price during the period</param>
        /// <param name="low">The lowest price during the period</param>
        /// <param name="close">The closing price</param>
        /// <param name="volume">The trading volume</param>
        public candlestick(DateTime date, decimal open, decimal high, decimal low, decimal close, long volume)
        {
            // Initialize all properties with the provided values
            Date = date;      // Set the trading period date
            Open = open;      // Set the opening price
            High = high;      // Set the highest price
            Low = low;        // Set the lowest price
            Close = close;    // Set the closing price
            Volume = volume;  // Set the trading volume
        }
    }
}
