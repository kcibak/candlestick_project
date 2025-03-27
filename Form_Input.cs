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
        // Add to existing fields
        private FibonacciAnalyzer fibAnalyzer = new FibonacciAnalyzer();
        private FibonacciWave selectedWave;
        private Dictionary<string, decimal> patternWeights = new Dictionary<string, decimal>
        {
            { "Hammer", 2m },
            { "Doji", 1.5m },
            { "Reversal", 3m }
        };

        public Form_Input()
        {
            InitializeComponent();
            candlesticks = new List<candlestick>();
            this.WindowState = FormWindowState.Maximized;
            chart_candlesNvolume.DataSource = candlesticks;
            //InitializeFibonacciControls();
        }

        private void AnalyzeSelectedWave()
        {
            if (selectedWave?.Start == null || selectedWave?.End == null)
            {
                MessageBox.Show("Invalid wave selection");
                return;
            }


            var fibLevels = fibAnalyzer.CalculateFibonacciLevels(selectedWave);
            DrawFibonacciLevels(fibLevels);

            var beautyScores = fibAnalyzer.ProjectBeautyScores(
                selectedWave,
                selectedWave.Start.Low * 0.9m,
                selectedWave.End.High * 1.3m,
                selectedWave.WaveHeight * 0.01m,
                patternWeights);

            UpdateBeautyChart(beautyScores);
        }

        private void DrawFibonacciLevels(Dictionary<decimal, decimal> fibLevels)
        {
            foreach (var level in fibLevels)
            {
                var line = new StripLine
                {
                    IntervalOffset = (double)level.Value,
                    StripWidth = 0.1,
                    BackColor = GetLevelColor(level.Key),
                    Text = $"{level.Key * 100}%"
                };
                chart_candlesNvolume.ChartAreas[0].AxisY.StripLines.Add(line);
            }
        }

        private Color GetLevelColor(decimal level)
        {
            switch (level)
            {
                case 0.236m:
                    return Color.LightGoldenrodYellow;
                case 0.382m:
                    return Color.LightSalmon;
                case 0.5m:
                    return Color.LightSkyBlue;
                case 0.618m:
                    return Color.LightGreen;
                default:
                    return Color.LightGray;
            }
        }

        private void UpdateBeautyChart(Dictionary<decimal, decimal> beautyScores)
        {
            if (chartBeautyScores.Series.Count == 0 || beautyScores == null)
            {
                MessageBox.Show("No beauty scores to display");
                return;
            }

            try
            {
                chartBeautyScores.Series["BeautyScoresSeries"].Points.DataBindXY(
                    beautyScores.Keys.Select(k => (double)k).ToList(),
                    beautyScores.Values.Select(v => (double)v).ToList()
                );

                // Configure axis settings
                var area = chartBeautyScores.ChartAreas[0];
                area.AxisX.Title = "Price Level";
                area.AxisY.Title = "Beauty Score";
                area.AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
                area.RecalculateAxesScale();

                chartBeautyScores.Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating beauty chart: {ex.Message}");
            }
        }
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

        private List<SmartCandlestick> filteredSmartCandlesticks;

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

            // Precompute smart candlesticks
            filteredSmartCandlesticks = ConvertToSmart(filteredCandlesticks);

            // Update chart display
            normalizeChart(filteredCandlesticks);
            chart_candlesNvolume.DataSource = filteredCandlesticks;
            propertyGrid_smartprops.Visible = false;
        }

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

        private string[] SplitCsvLine(string line)
        {
            List<string> result = new List<string>();
            StringBuilder current = new StringBuilder();
            bool inQuotes = false;

            foreach (char c in line)
            {
                if (c == '"')
                {
                    inQuotes = !inQuotes;
                }
                else if (c == ',' && !inQuotes)
                {
                    result.Add(current.ToString().Trim());
                    current.Clear();
                }
                else
                {
                    current.Append(c);
                }
            }

            result.Add(current.ToString().Trim());
            return result.ToArray();
        }

        private List<candlestick> loadStockinList(string selectedFilePath)
        {
            candlesticks.Clear();
            try
            {
                using (var reader = new StreamReader(selectedFilePath))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = SplitCsvLine(line);

                        string dateString = values[0];
                        if (!DateTime.TryParse(dateString, out DateTime date))
                        {
                            //MessageBox.Show($"Invalid date format: {dateString}");
                            continue;
                        }

                        try
                        {
                            decimal open = decimal.Parse(values[1]);
                            decimal high = decimal.Parse(values[2]);
                            decimal low = decimal.Parse(values[3]);
                            decimal close = decimal.Parse(values[4]);
                            long volume = long.Parse(values[5]);

                            candlesticks.Add(new candlestick(date, open, high, low, close, volume));
                        }
                        catch (FormatException ex)
                        {
                            MessageBox.Show($"Error parsing line: {line}\n{ex.Message}");
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Error reading file: {ex.Message}");
            }
            return candlesticks;
        }

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

        public List<SmartCandlestick> ConvertToSmart(List<candlestick> candlesticks, int lookbackPeriod = 5)
        {
            List<SmartCandlestick> smartCandlesticks = new List<SmartCandlestick>();

            foreach (var candle in candlesticks)
            {
                int i = candlesticks.IndexOf(candle);
                SmartCandlestick smartCandle = new SmartCandlestick(
                    candle.Date, candle.Open, candle.High, candle.Low, candle.Close, candle.Volume
                );

                // Determine valid window range for peak/valley detection
                int startWindow = Math.Max(0, i - lookbackPeriod);
                int endWindow = Math.Min(candlesticks.Count - 1, i + lookbackPeriod);

                // Check for peak
                bool isPeak = true;
                double currentHigh = (double)candle.High;
                for (int j = startWindow; j <= endWindow; j++)
                {
                    if (j == i) continue;
                    if ((double)candlesticks[j].High >= currentHigh)
                    {
                        isPeak = false;
                        break;
                    }
                }

                // Apply peak threshold (1% higher than max surrounding high)
                if (isPeak)
                {
                    double maxSurroundingHigh = candlesticks
                        .Where((c, idx) => idx >= startWindow && idx <= endWindow && idx != i)
                        .Max(c => (double)c.High);
                    double minHeightDifference = 0.01;

                    if (currentHigh - maxSurroundingHigh < maxSurroundingHigh * minHeightDifference)
                        isPeak = false;
                }
                smartCandle.IsPeak = isPeak;

                // Check for valley
                bool isValley = true;
                double currentLow = (double)candle.Low;
                for (int j = startWindow; j <= endWindow; j++)
                {
                    if (j == i) continue;
                    if ((double)candlesticks[j].Low <= currentLow)
                    {
                        isValley = false;
                        break;
                    }
                }

                // Apply valley threshold (1% lower than min surrounding low)
                if (isValley)
                {
                    double minSurroundingLow = candlesticks
                        .Where((c, idx) => idx >= startWindow && idx <= endWindow && idx != i)
                        .Min(c => (double)c.Low);
                    double minHeightDifference = 0.01;

                    if (minSurroundingLow - currentLow < currentLow * minHeightDifference)
                        isValley = false;
                }
                smartCandle.IsValley = isValley;

                smartCandlesticks.Add(smartCandle);
            }

            return smartCandlesticks;
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            if (candlesticks == null || candlesticks.Count == 0)
            {
                return;
            }
            FilterByDateNDisplay();
        }

        private void chart_candlesNvolume_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            HitTestResult result = chart_candlesNvolume.HitTest(me.X, me.Y);
            SmartCandlestick clickedCandle = null; // Declare outside inner scope

            if (result.ChartElementType == ChartElementType.DataPoint &&
                filteredCandlesticks != null &&
                result.PointIndex < filteredSmartCandlesticks.Count)
            {
                clickedCandle = filteredSmartCandlesticks[result.PointIndex];
                propertyGrid_smartprops.SelectedObject = clickedCandle;
                propertyGrid_smartprops.Show();
            }

            if (selectedWave != null && clickedCandle != null)
            {
                var fibLevels = fibAnalyzer.CalculateFibonacciLevels(selectedWave);
                decimal score = fibAnalyzer.CalculateBeautyScore(
                    clickedCandle, fibLevels, patternWeights);
                ShowTooltip(clickedCandle, score);
            }
        }

        private void ShowTooltip(SmartCandlestick candle, decimal beautyScore)
        {
            var point = chart_candlesNvolume.Series[0].Points
                .FirstOrDefault(p => p.XValue == candle.Date.ToOADate());

            if (point != null)
            {
                chart_candlesNvolume.Series[0].ToolTip = $"Beauty Score: {beautyScore}";
            }
        }

        private void chartBeautyScores_Click(object sender, MouseEventArgs e)
        {
            //MouseEventArgs me = (MouseEventArgs)e;
            var hitTest = chartBeautyScores.HitTest(e.X, e.Y);
            if (hitTest.PointIndex >= 0 && hitTest.Series != null)
            {
                var point = hitTest.Series.Points[hitTest.PointIndex];
                decimal price = (decimal)point.XValue;
                decimal score = (decimal)point.YValues[0];

                MessageBox.Show($"Potential Support/Resistance at {price:C}\nBeauty Score: {score}",
                              "Price Level Analysis",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
            }
        }

        private void chartBeautyScores_MouseMove(object sender, MouseEventArgs e)
        {
            var hitTest = chartBeautyScores.HitTest(e.X, e.Y);
            if (hitTest.PointIndex >= 0 && hitTest.Series != null)
            {
                var point = hitTest.Series.Points[hitTest.PointIndex];
                chartBeautyScores.Series[0].ToolTip = $"Price: {point.XValue:C}\nScore: {point.YValues[0]}";
            }
            else
            {
                chartBeautyScores.Series[0].ToolTip = string.Empty;
            }
        }

        private void chartBeautyScores_AxisViewChanged(object sender, ViewEventArgs e)
        {
            // Handle zoom/scroll events
            if (e.Axis.AxisName == AxisName.X)
            {
                // Update calculations for visible range
                var minX = e.Axis.ScaleView.ViewMinimum;
                var maxX = e.Axis.ScaleView.ViewMaximum;

                if (selectedWave != null)
                {
                    var beautyScores = fibAnalyzer.ProjectBeautyScores(
                        selectedWave,
                        (decimal)minX,
                        (decimal)maxX,
                        selectedWave.WaveHeight * 0.005m,  // Higher resolution
                        patternWeights);

                    UpdateBeautyChart(beautyScores);
                }
            }
        }

        private Color GetScoreColor(decimal score)
        {
            if (score > 8m) return Color.DarkGreen;
            else if (score > 6m) return Color.Green;
            else if (score > 4m) return Color.LightGreen;
            else if (score > 2m) return Color.Yellow;
            else return Color.OrangeRed;
        }

        private void button_selectWave_Click(object sender, EventArgs e)
        {
            // Clear previous selection
            selectedWave = null;

            // Clear any existing Fibonacci levels
            chart_candlesNvolume.ChartAreas[0].AxisY.StripLines.Clear();

            // Clear beauty scores chart
            if (chartBeautyScores.Series.Count > 0)
            {
                chartBeautyScores.Series["BeautyScoresSeries"].Points.Clear();
            }

            // Enable wave selection mode
            chart_candlesNvolume.MouseClick += chartBeautyScores_Click;
            MessageBox.Show("Please click the START point (peak/valley) of the wave on the main chart");
        }
    }
}
