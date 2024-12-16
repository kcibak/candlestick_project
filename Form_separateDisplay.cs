using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace projectpractice
{
    public partial class Form_separateDisplay : Form
    {
        public List<candlestick> candlestickers; // Initialize as List
        public Form_separateDisplay()
        {
            InitializeComponent();
            candlestickers = new List<candlestick>(); // Initialize candlesticks as List
            this.WindowState = FormWindowState.Maximized; // Have window maximized on startup
        }

        private void normalize(List<candlestick> filteredCandless)
        {
            double min = Math.Floor(0.98 * (double)filteredCandless.Min(candle => candle.Low)); // Get the minimum low value
            double max = Math.Ceiling(1.02 * (double)filteredCandless.Max(candle => candle.High)); // Get the maximum high value

            chart_candlesNvolumer.ChartAreas["ChartArea_candles"].AxisY.Minimum = min; // Set the minimum value for the y-axis
            chart_candlesNvolumer.ChartAreas["ChartArea_candles"].AxisY.Maximum = max; // Set the maximum value for the y-axis
            chart_candlesNvolumer.ChartAreas["ChartArea_candles"].AxisY.Interval = Math.Ceiling((max - min) / 5); // Set the interval for the y-axis
        }

        private void FilterNDisplay() // Function to filter and display the candlesticks
        {
            DateTime startDater = dateTimePicker_startD.Value; // Get the user-specified start date
            DateTime endDater = dateTimePicker_endD.Value; // Get the user-specified end date

            if (startDater > endDater) // Validate date range
            {
                MessageBox.Show("Start date cannot be greater than end date", "Date Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); // Show an error message if the start date is greater than the end date
                return;
            }

            List<candlestick> filteredCandlestickers = new List<candlestick>(); // Create a list to store the filtered candlesticks

            foreach (candlestick candler in candlestickers) // Iterate through each candlestick in the candlestickers list
            {
                if (candler.Date >= startDater && candler.Date <= endDater) // Filter by user-specified date
                {
                    filteredCandlestickers.Add(candler); // Add the candlestick to the filtered list
                }

                if (candler.Date > endDater) // Stop filtering when we reach the end date
                {
                    break; // Break out of the loop
                }
            }

            normalize(filteredCandlestickers); // Normalize the chart based on the filtered data
            chart_candlesNvolumer.DataSource = filteredCandlestickers; // Update the chart data source
            displayPeakNValleys(ConvertToSmarter(filteredCandlestickers)); // Update peaks and valleys in the chart
        }

        public void LoadData(List<candlestick> data, string fileName) // Function to load data into the chart
        {
            this.Text = $"Chart for {fileName}"; // Set the window title to the file name

            chart_candlesNvolumer.DataSource = data; // Set the data source for the chart
            candlestickers = data; // Set the candlestickers list to the data
            chart_candlesNvolumer.DataBind(); // Bind the data to the chart

            FilterNDisplay(); // Filter and display the candlesticks
            comboBox_AnnotationsV.SelectedIndex = 0; // Set the default selection to "Hide Annotations"
            comboBox_AnnotationsV.Show(); // Show the combo box for annotations
        }

        public List<SmartCandlestick> ConvertToSmarter(List<candlestick> candlestickers) // Function to convert candlesticks to SmartCandlesticks
        {
            List<SmartCandlestick> smartCandlestickers = new List<SmartCandlestick>(); // Create a list to store the SmartCandlesticks

            foreach (var candle in candlestickers) // Iterate through each candlestick
            {
                SmartCandlestick smartCandle = new SmartCandlestick( // Create a SmartCandlestick object
                    candle.Date,
                    candle.Open,
                    candle.High,
                    candle.Low,
                    candle.Close,
                    candle.Volume
                );
                 
                smartCandlestickers.Add(smartCandle); // Add the SmartCandlestick to the list
            }
            return smartCandlestickers; // Return the list of SmartCandlesticks
        }

        public void displayPeakNValleys(List<SmartCandlestick> smartCandlesticks) // Function to display peaks and valleys in the chart
        {
            chart_candlesNvolumer.Annotations.Clear(); // Clear existing annotations

            List<int> peakIndices = new List<int>(); // Create a list to store peak indices
            List<int> valleyIndices = new List<int>(); // Create a list to store valley indices

            for (int i = 1; i < smartCandlesticks.Count - 1; i++) // Iterate through the SmartCandlesticks
            {
                if (smartCandlesticks[i].High > smartCandlesticks[i - 1].High && // Check if the candle is a peak
                    smartCandlesticks[i].High > smartCandlesticks[i + 1].High)
                {
                    peakIndices.Add(i); // Add the peak index to the list
                }

                if (smartCandlesticks[i].Low < smartCandlesticks[i - 1].Low && // Check if the candle is a valley
                    smartCandlesticks[i].Low < smartCandlesticks[i + 1].Low)
                {
                    valleyIndices.Add(i); // Add the valley index to the list
                }
            }

            double rightmostX = smartCandlesticks.Count - 1; // Get the rightmost X value

            for (int i = 0; i < peakIndices.Count; i++) // Iterate through the list of peaks
            {
                int index = peakIndices[i]; // get current peak index
                var peakPrice = smartCandlesticks[index].High; // get current peak price

                var peakAnnotation = new HorizontalLineAnnotation // create new horizontal line annotation
                {
                    AxisX = chart_candlesNvolumer.ChartAreas["ChartArea_candles"].AxisX, // set the x-axis of the peak
                    AxisY = chart_candlesNvolumer.ChartAreas["ChartArea_candles"].AxisY, // set the y-axis of the peak
                    ClipToChartArea = "ChartArea_candles", // clip the annotation to the right chart area
                    AnchorY = 0, // anchor the annotation to the y-axis
                    AnchorAlignment = ContentAlignment.TopLeft, // align the annotation to the top left
                    AnchorOffsetY = -10, // set the offset of the anchor
                    Y = (double)peakPrice, // set the y-value of the annotation
                    LineColor = Color.Green, // set the color of the line annotation
                    LineWidth = 1, // set the line width
                    IsInfinitive = true, // set the line to span across the whole chart
                    Visible = false // is invisible until selection is changed
                };

                chart_candlesNvolumer.Annotations.Add(peakAnnotation); // add line annotation to the chart annotations collection

                var connector = new ArrowAnnotation // new arrow annotation
                {
                    AxisX = chart_candlesNvolumer.ChartAreas["ChartArea_candles"].AxisX, // set the x-axis of the peak
                    AxisY = chart_candlesNvolumer.ChartAreas["ChartArea_candles"].AxisY, // set the y-axis of the peak
                    ClipToChartArea = "ChartArea_candles", // clip the annotation to the right chart area
                    Width = 0.8,  // Smaller width
                    Height = 0,  // No vertical displacement
                    LineColor = Color.Green, // set color of arrow
                    LineWidth = 1, // set width of arrow
                    X = index + 1,  // Start at the peak point
                    Y = (double)peakPrice, // set the y-value of the arrow
                    Visible = false // is invisible at first
                };

                chart_candlesNvolumer.Annotations.Add(connector); // add the arrow annotation to the chart annotations collection
            }

            for (int i = 0; i < valleyIndices.Count; i++) // iterate through the list of valleys
            {
                int index = valleyIndices[i]; // set the index of the current valley
                var valleyPrice = smartCandlesticks[index].Low; // set the price of the current valley

                var valleyAnnotation = new HorizontalLineAnnotation // create new horizontal line annotation
                {
                    AxisX = chart_candlesNvolumer.ChartAreas["ChartArea_candles"].AxisX, // set the x-axis of the valley
                    AxisY = chart_candlesNvolumer.ChartAreas["ChartArea_candles"].AxisY, // set the y-axis of the valley
                    ClipToChartArea = "ChartArea_candles", // clip to the correct chart area
                    AnchorY = 0, // set the anchor of the anchor for the line annotation
                    AnchorAlignment = ContentAlignment.TopLeft, // align the line to top left
                    AnchorOffsetY = -10, // set the offset of the y-value of the annotation
                    Y = (double)valleyPrice, // set the y-value of the valley
                    LineColor = Color.Red, // set the color
                    LineWidth = 1, // set the width
                    IsInfinitive = true, // make the line span across the whole chart
                    Visible = false // invisible at first
                };

                chart_candlesNvolumer.Annotations.Add(valleyAnnotation); // add this annotation to the chart annotations collection

                var connector = new ArrowAnnotation // creat a new arrow annotation for the valley arrow
                {
                    AxisX = chart_candlesNvolumer.ChartAreas["ChartArea_candles"].AxisX, // set the x-axis of the valley
                    AxisY = chart_candlesNvolumer.ChartAreas["ChartArea_candles"].AxisY, // set the y-axis of the valley
                    ClipToChartArea = "ChartArea_candles", // clip to the correct chart area
                    Width = 0.8,  // Smaller width
                    Height = 0,  // No vertical displacement
                    LineColor = Color.Red, // set the color
                    LineWidth = 1, // set the line width
                    X = index + 1,  // Start at the valley point
                    Y = (double)valleyPrice, // set the y-value of the valley
                    Visible = false // invisible at first
                };

                chart_candlesNvolumer.Annotations.Add(connector); // add this annotation to the annotation collection
            }

            comboBox_AnnotationsV.SelectedIndex = 0; // make sure the drop down is set to hidden
            chart_candlesNvolumer.Invalidate(); // redraw the chart
        }

        private void button_newUpdate_Click(object sender, EventArgs e)
        {
            if (candlestickers == null || candlestickers.Count == 0) // Check if the candlesticks list is empty
            {
                return; // Return
            }
            FilterNDisplay(); // Refilter the data on each update
        }

        private void comboBox_AnnotationsV_SelectionChangeCommitted(object sender, EventArgs e) // event handler for the annotation visibility selection
        {
            string selectedOption = comboBox_AnnotationsV.SelectedItem.ToString(); // find which option is selected

            foreach (var annotation in chart_candlesNvolumer.Annotations) // iterate through each annotation
            {
                bool isPeak = false; // set the peak to false at first
                bool isValley = false; // set the vally to false at first

                if (annotation is HorizontalLineAnnotation lineAnnotation) // if the annotation is a Horizontal line
                {
                    isPeak = lineAnnotation.LineColor == Color.Green; // its a peak if the line is green
                    isValley = lineAnnotation.LineColor == Color.Red; // its a valley if the line is red
                }

                else if (annotation is ArrowAnnotation arrowAnnotation) // if its an arrow annotation
                {
                    isPeak = arrowAnnotation.LineColor == Color.Green; // its a peak if the line is green
                    isValley = arrowAnnotation.LineColor == Color.Red; // its a valley if the line is red
                }

                bool shouldBeVisible = false; // should not be visible by default
                switch (selectedOption) // switch case for the different drop-down options
                {
                    case "Hide Annotations": // hidden
                        shouldBeVisible = false; // set to invisible
                        break;
                    case "Show All Annotations": // show both peaks and valleys
                        shouldBeVisible = true; // set all annotations to visible
                        break;
                    case "Show Peaks Only":
                        shouldBeVisible = isPeak; // only set peaks visible
                        break;
                    case "Show Valleys Only":
                        shouldBeVisible = isValley; // only set valleys visible
                        break;
                    default:
                        shouldBeVisible = false; // invisible by default
                        break;
                }
                if (annotation is HorizontalLineAnnotation line) line.Visible = shouldBeVisible; // apply the visibility determined by the switch-case
                else if (annotation is ArrowAnnotation arrow) arrow.Visible = shouldBeVisible; // apply the visibility determined by the switch-case
            }

            chart_candlesNvolumer.Invalidate(); // redraw the chart with the new annotations
        }
    }
}
