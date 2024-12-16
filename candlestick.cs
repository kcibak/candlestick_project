using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectpractice
{
    public class candlestick // Class definition for candlestick
    {
        // Properties
        public DateTime Date { get; set; } // Date of the candlestick
        public decimal Open { get; set; }   // Open price
        public decimal High { get; set; }   // High price
        public decimal Low { get; set; }    // Low price
        public decimal Close { get; set; }  // Close price
        public long Volume { get; set; }     // Volume of stocks traded

        public candlestick(DateTime date, decimal open, decimal high, decimal low, decimal close, long volume) // Constructor
        {
            Date = date;
            
            Open = open;

            High = high;

            Low = low;

            Close = close;
            
            Volume = volume;
        }
    }
}
