/*Kira Cibak
  U06950566
  This program creates a platform for users to analyze stock data using built-in Winforms components.*/

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
        public List<candlestick> candlesticks; // Initialize as List

        public Form_Input()
        {
            InitializeComponent();
            candlesticks = new List<candlestick>(); // Initialize candlesticks as List
            this.WindowState = FormWindowState.Maximized; // Have window maximized on startup
            chart_candlesNvolume.DataSource = candlesticks; // Bind to Chart once
        }

        private void normalizeChart(List<candlestick> filteredCandles) // Allows the candlestick chart Y-axis to be indexed properly and show the right price range
                                                                       // depending on the stock that is loaded
        {
            double min = Math.Floor(0.98 * (double)filteredCandles.Min(candle => candle.Low)); // calculate the minimum value for the Y-axis
            double max = Math.Ceiling(1.02 * (double)filteredCandles.Max(candle => candle.High)); // calculate the maximum value for the Y-axis

            chart_candlesNvolume.ChartAreas["ChartArea_candles"].AxisY.Minimum = min; // Set the minimum value for the Y-axis
            chart_candlesNvolume.ChartAreas["ChartArea_candles"].AxisY.Maximum = max; // Set the maximum value for the Y-axis
            chart_candlesNvolume.ChartAreas["ChartArea_candles"].AxisY.Interval = Math.Ceiling((max - min) / 5); // Interval for the axis
        }

        private void FilterByDateNDisplay() // Filter the data grid view and chart by date and display the filtered data in the chart and data grid view
        {
            DateTime startDate = dateTimePicker_start.Value; // Get the user-specified start date
            DateTime endDate = dateTimePicker_end.Value; // Get the user-specified end date

            if (startDate > endDate) // Check if the date range makes sense
            {
                MessageBox.Show("Start date cannot be greater than end date", "Date Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<candlestick> filteredCandlesticks = new List<candlestick>(); // Create a new list to store the filtered candlesticks

            foreach (candlestick candle in candlesticks) // Iterate through each candlestick in the candlesticks list
            {
                if (candle.Date >= startDate && candle.Date <= endDate) // Filter by user-specified date and update the list
                {
                    filteredCandlesticks.Add(candle); // Add the candlestick to the filtered candlesticks list
                }

                if (candle.Date > endDate) // Stop when we reach the end date
                {
                    break; // Break out of the loop
                }
            }

            normalizeChart(filteredCandlesticks); // Normalize the chart
            chart_candlesNvolume.DataSource = filteredCandlesticks; // Update the chart
            displayPeakNValley(ConvertToSmart(filteredCandlesticks)); // Display the peak and valley annotations
        }

        private void LoadAdditionalFiles(string[] additionalFilePaths) // Load additional files into separate forms
        {
            foreach (string filePath in additionalFilePaths) // Iterate through each file path in the additional file paths array
            { 
                Form_separateDisplay separateDisplay = new Form_separateDisplay(); // Create a new separate display form
                List<candlestick> additionalCandlesticks = loadStockinList(filePath); // Load the stock data for this file

                additionalCandlesticks = new List<candlestick>( // Sort candlesticks by date
                    additionalCandlesticks.OrderBy(candle => candle.Date).ToList() // Order by date, using LINQ to sort the list
                );

                string fileName = Path.GetFileName(filePath); // Get the file name and load the data using your existing method
                separateDisplay.LoadData(additionalCandlesticks, fileName); // Load the data into the separate display form
                separateDisplay.Show(); // Show the form
            }
        }

        private void button_loadStock_Click(object sender, EventArgs e) // Load stock data from a file event handler
        {
            if (openFileDialog_load.ShowDialog() != DialogResult.OK) // Show the open file dialog and check if the result is OK
            {
                return; // Return if the dialog is cancelled or no file is selected
            }

            string[] selectedFilePaths = openFileDialog_load.FileNames;  // Get all selected files

            if (selectedFilePaths.Length == 1)  // Single file selected - load in main form
            {

                string mainFilePath = selectedFilePaths[0]; // Get the main file path
                string fileName = Path.GetFileName(mainFilePath); // Get the file name
                this.Text = $"Chart for {fileName}"; // Set the form title

                loadStockinList(mainFilePath); // Load the stock data from the selected file into the candlesticks list
                candlesticks = new List<candlestick>(candlesticks.OrderBy(candle => candle.Date).ToList()); // Sort the candlesticks by date using LINQ
                FilterByDateNDisplay(); // Filter the data by date and display the filtered data in the chart and data grid view

                chart_candlesNvolume.Show(); // Show the chart
                label_price.Show(); // Show the price label
                label_volume.Show(); // Show the volume label
                comboBox_AnnotationsVisibility.Show(); // Show the annotations visibility combo box
            }
            else if (selectedFilePaths.Length > 1)  // Multiple files selected
            {
                string mainFilePath = selectedFilePaths[0]; // Get the main file path
                string fileName = Path.GetFileName(mainFilePath); // Get the file name
                this.Text = $"Chart for {fileName} (main form)"; // Set the form title

                loadStockinList(mainFilePath); // Load the stock data from the selected file into the candlesticks list
                candlesticks = new List<candlestick>(candlesticks.OrderBy(candle => candle.Date).ToList()); // Sort the candlesticks by date using LINQ
                FilterByDateNDisplay(); // Filter the data by date and display the filtered data in the chart and data grid view
                chart_candlesNvolume.Show(); // Show the chart
                label_price.Show(); // Show the price label
                label_volume.Show(); // Show the volume label
                comboBox_AnnotationsVisibility.Show(); // Show the annotations visibility combo box
                multipleFilesPresent(selectedFilePaths); // Load additional files into separate forms
            }
        }

        private List<candlestick> loadStockinList(string selectedFilePath) // Load stock data from the selected file into the candlesticks binding list
        {
            candlesticks.Clear(); // Clear existing data
            using (var reader = new StreamReader(selectedFilePath)) // Read the file using a stream reader
            {
                while (!reader.EndOfStream) // Read each line and add to candlesticks until the end of the .csv file
                {
                    var line = reader.ReadLine(); // This reads a single line and stores it in line
                    var values = line.Split(','); // The values are separated, delimited by comma and place into values array;
                                                  // var is used to allow the compiler to infer the datatype of each variable

                    string dateString = values[0].Trim('"', '\''); // First value is the date, but needs the quotations and 

                    if (DateTime.TryParse(dateString, out DateTime date)) // If the date is able to be parsed in the proper format, continue parsing all 
                                                                          // candlestick members into the binding list
                    {
                        candlestick candle = new candlestick( // Create a new candlestick object
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
                return candlesticks; // Return list of candlesticks
            }
        }

        private List<candlestick> loadStockinSeparateList(string selectedFilePath) // Load stock data from the selected file into a separate list if multiple files are present
        {
            List<candlestick> candlesticksSep = new List<candlestick>(); // Create a new list for each separate display

            using (var reader = new StreamReader(selectedFilePath)) // Read the file using a stream reader
            {
                while (!reader.EndOfStream) // Read each line and add to candlesticks until the end of the .csv file
                {
                    var line = reader.ReadLine(); // This reads a single line and stores it in line
                    var values = line.Split(','); // The values are separated, delimited by comma and placed into values array

                    string dateString = values[0].Trim('"', '\''); // First value is the date, but needs the quotations removed

                    if (DateTime.TryParse(dateString, out DateTime date)) // If the date is able to be parsed in the proper format, continue parsing all candlestick members into the list
                    {
                        candlestick candle = new candlestick( // Create a new candlestick object
                            date,
                            decimal.Parse(values[1]),
                            decimal.Parse(values[2]),
                            decimal.Parse(values[3]),
                            decimal.Parse(values[4]),
                            long.Parse(values[5])
                        );
                        candlesticksSep.Add(candle); // Add the candlestick to the list
                    }
                }
            }

            return candlesticksSep; // Return separate list of candlesticks
        }

        private void multipleFilesPresent(string[] selectedFilePaths) // Load additional files into separate forms
        {
            string[] additionalFilePaths = selectedFilePaths.Skip(1).ToArray(); // Skip the first file and get the rest using LINQ

            foreach (string filePath in additionalFilePaths) // Iterate through each file path in the additional file paths array
            {
                Form_separateDisplay separateDisplay = new Form_separateDisplay(); // Create a new separate display form
                List<candlestick> additionalCandlesticks = loadStockinSeparateList(filePath); // Load the stock data for this file
                additionalCandlesticks = new List<candlestick>( // Sort candlesticks by date using LINQ
                    additionalCandlesticks.OrderBy(candle => candle.Date).ToList()
                );

                string fileName = Path.GetFileName(filePath); // Get the file name
                separateDisplay.LoadData(additionalCandlesticks, fileName); // Load the data into the separate display form
                separateDisplay.Show(); // Show the form
            }
        }

        public List<SmartCandlestick> ConvertToSmart(List<candlestick> candlesticks)
        {
            List<SmartCandlestick> smartCandlesticks = new List<SmartCandlestick>(); // Create a new list of SmartCandlesticks
            foreach (var candle in candlesticks) // Iterate through each candlestick in the candlesticks list
            {
                SmartCandlestick smartCandle = new SmartCandlestick( // Create a new SmartCandlestick object
                    candle.Date,
                    candle.Open,
                    candle.High,
                    candle.Low,
                    candle.Close,
                    candle.Volume
                );

                smartCandlesticks.Add(smartCandle); // Add the SmartCandlestick to the list
            }

            return smartCandlesticks; // Return the list of SmartCandlesticks
        }

        public void displayPeakNValley(List<SmartCandlestick> smartCandlesticks) // Display the peak and valley annotations on the chart
        {
            chart_candlesNvolume.Annotations.Clear(); // Clear existing annotations

            List<int> peakIndices = new List<int>(); // Create a new list of peak indices
            List<int> valleyIndices = new List<int>(); // Create a new list of valley indices

            for (int i = 1; i < smartCandlesticks.Count - 1; i++) // Iterate through each SmartCandlestick in the list
            {
                if (smartCandlesticks[i].High > smartCandlesticks[i - 1].High && // Check if the current candlestick is a peak
                    smartCandlesticks[i].High > smartCandlesticks[i + 1].High)
                {
                    peakIndices.Add(i); // Add the peak index to the list of peaks
                }

                if (smartCandlesticks[i].Low < smartCandlesticks[i - 1].Low && // Check if the current candlestick is a valley
                    smartCandlesticks[i].Low < smartCandlesticks[i + 1].Low)
                {
                    valleyIndices.Add(i); // Add the valley index to the list of valleys
                }
            }
            double rightmostX = smartCandlesticks.Count - 1; // Get the rightmost X value

            for (int i = 0; i < peakIndices.Count; i++) // Iterate through each identified peak
            {
                int index = peakIndices[i]; // Get the peak index
                var peakPrice = smartCandlesticks[index].High; // Get the peak price

                var peakAnnotation = new HorizontalLineAnnotation // Create a new horizontal line annotation for the peak
                {
                    AxisX = chart_candlesNvolume.ChartAreas["ChartArea_candles"].AxisX, // Set the X axis
                    AxisY = chart_candlesNvolume.ChartAreas["ChartArea_candles"].AxisY, // Set the Y axis
                    ClipToChartArea = "ChartArea_candles", // Clip to the chart area
                    AnchorY = 0, // Anchor at Y = 0
                    AnchorAlignment = ContentAlignment.TopLeft, // Anchor alignment
                    AnchorOffsetY = -10, // Offset the anchor
                    Y = (double)peakPrice, // Set the Y value to the peak price
                    LineColor = Color.Green, // Set the line color to green
                    LineWidth = 2, // Set the line width
                    IsInfinitive = true, // Set the line to be infinite
                    Visible = false // Set the line to be invisible
                };

                chart_candlesNvolume.Annotations.Add(peakAnnotation); // Add the peak annotation to the chart

                var connector = new ArrowAnnotation // Create a new arrow annotation for the peaks
                {
                    AxisX = chart_candlesNvolume.ChartAreas["ChartArea_candles"].AxisX, // Set the X axis
                    AxisY = chart_candlesNvolume.ChartAreas["ChartArea_candles"].AxisY, // Set the Y axis
                    ClipToChartArea = "ChartArea_candles", // Clip to the chart area
                    Width = 0.8,  // Smaller width
                    Height = 0,  // No vertical displacement
                    LineColor = Color.Green, // Set the line color to green
                    LineWidth = 1, // Set the line width
                    X = index + 1,  // Start at the peak point
                    Y = (double)peakPrice, // Set the Y value to the peak price
                    Visible = false // Set the line to be invisible
                };

                chart_candlesNvolume.Annotations.Add(connector); // Add the connector to the chart
            }

            for (int i = 0; i < valleyIndices.Count; i++) // Iterate through each identified valley
            {
                int index = valleyIndices[i]; // Get the valley index
                var valleyPrice = smartCandlesticks[index].Low; // Get the valley price

                var valleyAnnotation = new HorizontalLineAnnotation // Create a new horizontal line annotation for the valley
                {
                    AxisX = chart_candlesNvolume.ChartAreas["ChartArea_candles"].AxisX, // Set the X axis
                    AxisY = chart_candlesNvolume.ChartAreas["ChartArea_candles"].AxisY, // Set the Y axis
                    ClipToChartArea = "ChartArea_candles", // Clip to the chart area
                    AnchorY = 0, // Anchor at Y = 0
                    AnchorAlignment = ContentAlignment.TopLeft, // Anchor alignment
                    AnchorOffsetY = -10, // Offset the anchor
                    Y = (double)valleyPrice, // Set the Y value to the valley price
                    LineColor = Color.Red, // Set the line color to red
                    LineWidth = 2, // Set the line width
                    IsInfinitive = true, // Set the line to be infinite
                    Visible = false // Set the line to be invisible
                };

                chart_candlesNvolume.Annotations.Add(valleyAnnotation); // Add the valley annotation to the chart

                var connector = new ArrowAnnotation // Create a new arrow annotation for the valleys
                {
                    AxisX = chart_candlesNvolume.ChartAreas["ChartArea_candles"].AxisX, // Set the X axis
                    AxisY = chart_candlesNvolume.ChartAreas["ChartArea_candles"].AxisY, // Set the Y axis
                    ClipToChartArea = "ChartArea_candles", // Clip to the chart area
                    Width = 0.8,  // Smaller width
                    Height = 0,  // No vertical displacement
                    LineColor = Color.Red, // Set the line color to red
                    LineWidth = 1, // Set the line width
                    X = index + 1,  // Start at the valley point
                    Y = (double)valleyPrice, // Set the Y value to the valley price
                    Visible = false // Set the line to be invisible
                };

                chart_candlesNvolume.Annotations.Add(connector); // Add the connector to the chart
            }
            comboBox_AnnotationsVisibility.SelectedIndex = 0; // Set the annotations visibility combo box to the first option
            chart_candlesNvolume.Invalidate(); // Invalidate the chart
        }

        private void button_update_Click(object sender, EventArgs e) // Update the data grid view and chart
        {
            if (candlesticks == null || candlesticks.Count == 0) // Check if the candlesticks list is empty
            {
                return; // Return
            }
            FilterByDateNDisplay(); // Refilter the data on each update
        }

        private void comboBox_AnnotationsVisibility_SelectionChangeCommitted(object sender, EventArgs e) // Change the visibility of the annotations
        {
            string selectedOption = comboBox_AnnotationsVisibility.SelectedItem.ToString(); // Get the selected option using the combo box

            foreach (var annotation in chart_candlesNvolume.Annotations) // Iterate through each annotation in the chart
            {
                bool isPeak = false; // Initialize isPeak as false
                bool isValley = false; // Initialize isValley as false

                if (annotation is HorizontalLineAnnotation lineAnnotation) // Check if the annotation is a horizontal line annotation
                {
                    isPeak = lineAnnotation.LineColor == Color.Green; // Check if the line color is green and set isPeak accordingly
                    isValley = lineAnnotation.LineColor == Color.Red; // Check if the line color is red and set isValley accordingly
                }

                else if (annotation is ArrowAnnotation arrowAnnotation) // Check if the annotation is an arrow annotation
                {
                    isPeak = arrowAnnotation.LineColor == Color.Green; // Check if the line color is green and set isPeak accordingly
                    isValley = arrowAnnotation.LineColor == Color.Red; // Check if the line color is red and set isValley accordingly
                }

                bool shouldBeVisible = false; // Initialize shouldBeVisible as false
                switch (selectedOption) // Switch based on the selected option
                {
                    case "Hide Annotations": // Hide all annotations
                        shouldBeVisible = false;
                        break;
                    case "Show All Annotations": // Show all annotations
                        shouldBeVisible = true;
                        break;
                    case "Show Peaks Only": // Show only peaks
                        shouldBeVisible = isPeak;
                        break;
                    case "Show Valleys Only": // Show only valleys
                        shouldBeVisible = isValley;
                        break;
                    default: // Default case which is hidden
                        shouldBeVisible = false;
                        break;
                }
                if (annotation is HorizontalLineAnnotation line) line.Visible = shouldBeVisible; // Set the visibility of the line annotation
                else if (annotation is ArrowAnnotation arrow) arrow.Visible = shouldBeVisible; // Set the visibility of the arrow annotation
            }
            chart_candlesNvolume.Invalidate(); // redraw the chart
        }
    }
}