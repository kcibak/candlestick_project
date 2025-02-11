/*
Program Name: Stock Analysis Platform
Author: Kira Cibak (U06950566)
Description: This Windows Forms application provides a comprehensive platform for analyzing stock market data.
The program allows users to:
- Load and visualize stock data from CSV files
- Display interactive candlestick charts with volume information
- Compare multiple stocks simultaneously in separate windows
- Filter data by date ranges
- Identify peaks and valleys in stock price movements
- View detailed properties of individual candlesticks
- Analyze smart candlestick patterns with customizable lookback periods

File Format: Expects CSV files with columns for Date, Open, High, Low, Close, and Volume
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace projectpractice
{
    public partial class Form_Input : Form
    {
        // Main collection to store candlestick data
        public List<candlestick> candlesticks;

        // Collection to store filtered candlesticks based on date range
        private List<candlestick> filteredCandlesticks;

        /// <summary>
        /// Constructor: Initializes the form and sets up initial configurations
        /// </summary>
        public Form_Input()
        {
            InitializeComponent();
            candlesticks = new List<candlestick>();
            this.WindowState = FormWindowState.Maximized;
            chart_candlesNvolume.DataSource = candlesticks;
        }

        /// <summary>
        /// Adjusts the Y-axis scale of the chart based on the loaded stock data
        /// </summary>
        /// <param name="filteredCandles">List of candlesticks to use for scaling</param>
        private void normalizeChart(List<candlestick> filteredCandles)
        {
            // Add padding to min/max values to prevent data points from touching chart edges
            double min = Math.Floor(0.98 * (double)filteredCandles.Min(candle => candle.Low));
            double max = Math.Ceiling(1.02 * (double)filteredCandles.Max(candle => candle.High));

            // Configure chart axes
            chart_candlesNvolume.ChartAreas["ChartArea_candles"].AxisY.Minimum = min;
            chart_candlesNvolume.ChartAreas["ChartArea_candles"].AxisY.Maximum = max;
            chart_candlesNvolume.ChartAreas["ChartArea_candles"].AxisY.Interval = Math.Ceiling((max - min) / 5);

            var candleArea = chart_candlesNvolume.ChartAreas["ChartArea_candles"];
            candleArea.AxisX.Title = "Date";
            candleArea.AxisY.Title = "Price";
        }

        /// <summary>
        /// Filters candlesticks based on selected date range and updates the display
        /// </summary>
        private void FilterByDateNDisplay()
        {
            DateTime startDate = dateTimePicker_start.Value;
            DateTime endDate = dateTimePicker_end.Value;

            // Validate date range
            if (startDate > endDate)
            {
                MessageBox.Show("Start date cannot be greater than end date", "Date Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            filteredCandlesticks = new List<candlestick>();

            // Filter candlesticks within the selected date range
            foreach (candlestick candle in candlesticks)
            {
                if (candle.Date >= startDate && candle.Date <= endDate)
                {
                    filteredCandlesticks.Add(candle);
                }

                if (candle.Date > endDate)
                {
                    break;
                }
            }

            // Update chart display
            normalizeChart(filteredCandlesticks);
            chart_candlesNvolume.DataSource = filteredCandlesticks;
            List<SmartCandlestick> smartCandles = ConvertToSmart(filteredCandlesticks);
            propertyGrid_smartprops.Visible = false;
        }

        /// <summary>
        /// Handles loading of multiple stock files into separate windows
        /// </summary>
        /// <param name="additionalFilePaths">Array of file paths to load</param>
        private void LoadAdditionalFiles(string[] additionalFilePaths)
        {
            foreach (string filePath in additionalFilePaths)
            {
                Form_separateDisplay separateDisplay = new Form_separateDisplay();
                List<candlestick> additionalCandlesticks = loadStockinList(filePath);

                // Sort candlesticks chronologically
                additionalCandlesticks = new List<candlestick>(
                    additionalCandlesticks.OrderBy(candle => candle.Date).ToList()
                );

                string fileName = Path.GetFileName(filePath);
                separateDisplay.LoadData(additionalCandlesticks, fileName);
                separateDisplay.Show();
            }
        }

        /// <summary>
        /// Event handler for the Load Stock button click
        /// Manages loading single or multiple stock files
        /// </summary>
        private void button_loadStock_Click(object sender, EventArgs e)
        {
            if (openFileDialog_load.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string[] selectedFilePaths = openFileDialog_load.FileNames;

            // Handle single file selection
            if (selectedFilePaths.Length == 1)
            {
                string mainFilePath = selectedFilePaths[0];
                string fileName = Path.GetFileName(mainFilePath);
                this.Text = $"Chart for {fileName}";

                loadStockinList(mainFilePath);
                candlesticks = new List<candlestick>(candlesticks.OrderBy(candle => candle.Date).ToList());
                chart_candlesNvolume.Show();
            }
            // Handle multiple file selection
            else if (selectedFilePaths.Length > 1)
            {
                string mainFilePath = selectedFilePaths[0];
                string fileName = Path.GetFileName(mainFilePath);
                this.Text = $"Chart for {fileName} (main form)";

                loadStockinList(mainFilePath);
                candlesticks = new List<candlestick>(candlesticks.OrderBy(candle => candle.Date).ToList());
                chart_candlesNvolume.Show();
                multipleFilesPresent(selectedFilePaths);
            }
        }

        /// <summary>
        /// Loads stock data from CSV file into the main candlesticks list
        /// </summary>
        /// <param name="selectedFilePath">Path to the CSV file</param>
        /// <returns>List of parsed candlesticks</returns>
        private List<candlestick> loadStockinList(string selectedFilePath)
        {
            candlesticks.Clear();
            using (var reader = new StreamReader(selectedFilePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    string dateString = values[0].Trim('"', '\'');

                    if (DateTime.TryParse(dateString, out DateTime date))
                    {
                        candlestick candle = new candlestick(
                            date,
                            decimal.Parse(values[1]),
                            decimal.Parse(values[2]),
                            decimal.Parse(values[3]),
                            decimal.Parse(values[4]),
                            long.Parse(values[5])
                        );
                        candlesticks.Add(candle);
                    }
                }
                return candlesticks;
            }
        }

        /// <summary>
        /// Loads stock data into a separate list for additional displays
        /// </summary>
        /// <param name="selectedFilePath">Path to the CSV file</param>
        /// <returns>List of parsed candlesticks</returns>
        private List<candlestick> loadStockinSeparateList(string selectedFilePath)
        {
            List<candlestick> candlesticksSep = new List<candlestick>();

            using (var reader = new StreamReader(selectedFilePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    string dateString = values[0].Trim('"', '\'');

                    if (DateTime.TryParse(dateString, out DateTime date))
                    {
                        candlestick candle = new candlestick(
                            date,
                            decimal.Parse(values[1]),
                            decimal.Parse(values[2]),
                            decimal.Parse(values[3]),
                            decimal.Parse(values[4]),
                            long.Parse(values[5])
                        );
                        candlesticksSep.Add(candle);
                    }
                }
            }
            return candlesticksSep;
        }

        /// <summary>
        /// Handles the display of multiple stock files in separate windows
        /// </summary>
        /// <param name="selectedFilePaths">Array of selected file paths</param>
        private void multipleFilesPresent(string[] selectedFilePaths)
        {
            string[] additionalFilePaths = selectedFilePaths.Skip(1).ToArray();

            foreach (string filePath in additionalFilePaths)
            {
                Form_separateDisplay separateDisplay = new Form_separateDisplay();
                List<candlestick> additionalCandlesticks = loadStockinSeparateList(filePath);
                additionalCandlesticks = new List<candlestick>(
                    additionalCandlesticks.OrderBy(candle => candle.Date).ToList()
                );

                string fileName = Path.GetFileName(filePath);
                separateDisplay.LoadData(additionalCandlesticks, fileName);
                separateDisplay.Show();
            }
        }

        /// <summary>
        /// Converts regular candlesticks to smart candlesticks with peak/valley detection
        /// </summary>
        /// <param name="candlesticks">List of regular candlesticks</param>
        /// <param name="lookbackPeriod">Number of periods to look back for pattern detection</param>
        /// <returns>List of smart candlesticks with identified patterns</returns>
        public List<SmartCandlestick> ConvertToSmart(List<candlestick> candlesticks, int lookbackPeriod = 5)
        {
            List<SmartCandlestick> smartCandlesticks = new List<SmartCandlestick>();

            // Ensure enough data points for analysis
            if (candlesticks.Count < (lookbackPeriod * 2 + 1))
                return smartCandlesticks;

            for (int i = lookbackPeriod; i < candlesticks.Count - lookbackPeriod; i++)
            {
                var candle = candlesticks[i];
                SmartCandlestick smartCandle = new SmartCandlestick(
                    candle.Date, candle.Open, candle.High, candle.Low, candle.Close, candle.Volume
                );

                // Check for price peaks
                bool isPeak = true;
                for (int j = i - lookbackPeriod; j <= i + lookbackPeriod; j++)
                {
                    if (j != i && candlesticks[j].High >= candle.High)
                    {
                        isPeak = false;
                        break;
                    }
                }
                smartCandle.IsPeak = isPeak;

                // Check for price valleys
                bool isValley = true;
                for (int j = i - lookbackPeriod; j <= i + lookbackPeriod; j++)
                {
                    if (j != i && candlesticks[j].Low <= candle.Low)
                    {
                        isValley = false;
                        break;
                    }
                }
                smartCandle.IsValley = isValley;

                // Apply minimum price movement threshold for peaks
                if (isPeak)
                {
                    double minHeightDifference = 0.01; // 1% threshold
                    double lowestLow = double.MaxValue;
                    for (int j = i - lookbackPeriod; j <= i + lookbackPeriod; j++)
                    {
                        lowestLow = Math.Min(lowestLow, (double)candlesticks[j].Low);
                    }
                    if (((double)candle.High - lowestLow) / lowestLow < minHeightDifference)
                    {
                        smartCandle.IsPeak = false;
                    }
                }

                // Apply minimum price movement threshold for valleys
                if (isValley)
                {
                    double minHeightDifference = 0.01; // 1% threshold
                    double highestHigh = double.MinValue;
                    for (int j = i - lookbackPeriod; j <= i + lookbackPeriod; j++)
                    {
                        highestHigh = Math.Max(highestHigh, (double)candlesticks[j].High);
                    }
                    if ((highestHigh - (double)candle.Low) / (double)candle.Low < minHeightDifference)
                    {
                        smartCandle.IsValley = false;
                    }
                }

                smartCandlesticks.Add(smartCandle);
            }

            return smartCandlesticks;
        }

        /// <summary>
        /// Event handler for the Update button click
        /// Refreshes the display with current filter settings
        /// </summary>
        private void button_update_Click(object sender, EventArgs e)
        {
            if (candlesticks == null || candlesticks.Count == 0)
            {
                return;
            }
            FilterByDateNDisplay();
        }

        /// <summary>
        /// Event handler for chart clicks
        /// Displays detailed properties of clicked candlestick
        /// </summary>
        private void chart_candlesNvolume_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            HitTestResult result = chart_candlesNvolume.HitTest(me.X, me.Y);

            if (result.ChartElementType == ChartElementType.DataPoint)
            {
                int pointIndex = result.PointIndex;

                if (filteredCandlesticks != null && filteredCandlesticks.Count > pointIndex)
                {
                    var smartCandlesticks = ConvertToSmart(filteredCandlesticks);
                    if (smartCandlesticks.Count > pointIndex)
                    {
                        SmartCandlestick clickedCandle = smartCandlesticks[pointIndex];
                        propertyGrid_smartprops.SelectedObject = null;
                        propertyGrid_smartprops.SelectedObject = clickedCandle;
                        propertyGrid_smartprops.Refresh();
                        propertyGrid_smartprops.Show();
                    }
                }
            }
        }
    }
}
