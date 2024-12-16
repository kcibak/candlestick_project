using System;

namespace projectpractice
{
    partial class Form_Input
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Input));
            this.button_loadStock = new System.Windows.Forms.Button();
            this.openFileDialog_load = new System.Windows.Forms.OpenFileDialog();
            this.dateTimePicker_start = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_end = new System.Windows.Forms.DateTimePicker();
            this.chart_candlesNvolume = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button_update = new System.Windows.Forms.Button();
            this.textBox_end = new System.Windows.Forms.TextBox();
            this.textBox_start = new System.Windows.Forms.TextBox();
            this.label_price = new System.Windows.Forms.Label();
            this.label_volume = new System.Windows.Forms.Label();
            this.comboBox_AnnotationsVisibility = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart_candlesNvolume)).BeginInit();
            this.SuspendLayout();
            // 
            // button_loadStock
            // 
            this.button_loadStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_loadStock.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button_loadStock.Location = new System.Drawing.Point(33, 15);
            this.button_loadStock.Margin = new System.Windows.Forms.Padding(24, 15, 24, 15);
            this.button_loadStock.Name = "button_loadStock";
            this.button_loadStock.Size = new System.Drawing.Size(246, 148);
            this.button_loadStock.TabIndex = 0;
            this.button_loadStock.Text = "Load stock(s)";
            this.button_loadStock.UseVisualStyleBackColor = true;
            this.button_loadStock.Click += new System.EventHandler(this.button_loadStock_Click);
            // 
            // openFileDialog_load
            // 
            this.openFileDialog_load.DefaultExt = "csv";
            this.openFileDialog_load.FileName = "DIS-Month.csv";
            this.openFileDialog_load.Filter = "All|*.csv|Monthly|*-month.csv|Weekly|*-week.csv|Daily|*-day.csv";
            this.openFileDialog_load.InitialDirectory = "\\\\Mac\\Home\\Desktop\\sToNkS";
            this.openFileDialog_load.Multiselect = true;
            // 
            // dateTimePicker_start
            // 
            this.dateTimePicker_start.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker_start.Location = new System.Drawing.Point(932, 78);
            this.dateTimePicker_start.Margin = new System.Windows.Forms.Padding(24, 15, 24, 15);
            this.dateTimePicker_start.MaxDate = new System.DateTime(2024, 10, 24, 0, 0, 0, 0);
            this.dateTimePicker_start.MinDate = new System.DateTime(2001, 1, 31, 0, 0, 0, 0);
            this.dateTimePicker_start.Name = "dateTimePicker_start";
            this.dateTimePicker_start.Size = new System.Drawing.Size(238, 22);
            this.dateTimePicker_start.TabIndex = 2;
            this.dateTimePicker_start.Value = new System.DateTime(2022, 1, 1, 0, 0, 0, 0);
            // 
            // dateTimePicker_end
            // 
            this.dateTimePicker_end.Checked = false;
            this.dateTimePicker_end.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker_end.Location = new System.Drawing.Point(932, 136);
            this.dateTimePicker_end.Margin = new System.Windows.Forms.Padding(24, 15, 24, 15);
            this.dateTimePicker_end.MaxDate = new System.DateTime(2024, 9, 12, 0, 0, 0, 0);
            this.dateTimePicker_end.MinDate = new System.DateTime(2001, 1, 31, 0, 0, 0, 0);
            this.dateTimePicker_end.Name = "dateTimePicker_end";
            this.dateTimePicker_end.Size = new System.Drawing.Size(238, 22);
            this.dateTimePicker_end.TabIndex = 3;
            this.dateTimePicker_end.Value = new System.DateTime(2024, 9, 12, 0, 0, 0, 0);
            // 
            // chart_candlesNvolume
            // 
            chartArea1.AlignWithChartArea = "ChartArea_volume";
            chartArea1.BackSecondaryColor = System.Drawing.Color.White;
            chartArea1.Name = "ChartArea_candles";
            chartArea2.AlignWithChartArea = "ChartArea_candles";
            chartArea2.Name = "ChartArea_volume";
            this.chart_candlesNvolume.ChartAreas.Add(chartArea1);
            this.chart_candlesNvolume.ChartAreas.Add(chartArea2);
            this.chart_candlesNvolume.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.chart_candlesNvolume.Location = new System.Drawing.Point(0, 341);
            this.chart_candlesNvolume.Margin = new System.Windows.Forms.Padding(24, 15, 24, 15);
            this.chart_candlesNvolume.Name = "chart_candlesNvolume";
            this.chart_candlesNvolume.Padding = new System.Windows.Forms.Padding(5);
            this.chart_candlesNvolume.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SemiTransparent;
            series1.BackSecondaryColor = System.Drawing.SystemColors.ActiveCaption;
            series1.ChartArea = "ChartArea_candles";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series1.CustomProperties = "PriceDownColor=Red, PriceUpColor=LimeGreen";
            series1.IsVisibleInLegend = false;
            series1.IsXValueIndexed = true;
            series1.Name = "Series_candles";
            series1.XValueMember = "Date";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series1.YValueMembers = "High,Low,Open,Close";
            series1.YValuesPerPoint = 5;
            series2.ChartArea = "ChartArea_volume";
            series2.IsVisibleInLegend = false;
            series2.IsXValueIndexed = true;
            series2.Name = "Series_volume";
            series2.XValueMember = "Date";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series2.YValueMembers = "Volume";
            series2.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.UInt64;
            this.chart_candlesNvolume.Series.Add(series1);
            this.chart_candlesNvolume.Series.Add(series2);
            this.chart_candlesNvolume.Size = new System.Drawing.Size(1245, 423);
            this.chart_candlesNvolume.TabIndex = 5;
            this.chart_candlesNvolume.Text = "Candlesticks";
            title1.Alignment = System.Drawing.ContentAlignment.TopCenter;
            title1.DockedToChartArea = "ChartArea_candles";
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.IsDockedInsideChartArea = false;
            title1.Name = "candles";
            title1.Text = "Candlestick Chart";
            title2.Alignment = System.Drawing.ContentAlignment.TopCenter;
            title2.DockedToChartArea = "ChartArea_volume";
            title2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title2.IsDockedInsideChartArea = false;
            title2.Name = "volume";
            title2.Text = "Stock Volume Chart";
            this.chart_candlesNvolume.Titles.Add(title1);
            this.chart_candlesNvolume.Titles.Add(title2);
            this.chart_candlesNvolume.Visible = false;
            // 
            // button_update
            // 
            this.button_update.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_update.ForeColor = System.Drawing.Color.Black;
            this.button_update.Location = new System.Drawing.Point(1218, 73);
            this.button_update.Margin = new System.Windows.Forms.Padding(24, 23, 24, 23);
            this.button_update.Name = "button_update";
            this.button_update.Size = new System.Drawing.Size(190, 63);
            this.button_update.TabIndex = 6;
            this.button_update.Text = "Update";
            this.button_update.UseVisualStyleBackColor = true;
            this.button_update.Click += new System.EventHandler(this.button_update_Click);
            // 
            // textBox_end
            // 
            this.textBox_end.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.textBox_end.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_end.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox_end.Enabled = false;
            this.textBox_end.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_end.Location = new System.Drawing.Point(846, 136);
            this.textBox_end.Margin = new System.Windows.Forms.Padding(6);
            this.textBox_end.Name = "textBox_end";
            this.textBox_end.ReadOnly = true;
            this.textBox_end.Size = new System.Drawing.Size(64, 17);
            this.textBox_end.TabIndex = 9;
            this.textBox_end.Text = "END:";
            // 
            // textBox_start
            // 
            this.textBox_start.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.textBox_start.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_start.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox_start.Enabled = false;
            this.textBox_start.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_start.Location = new System.Drawing.Point(846, 83);
            this.textBox_start.Margin = new System.Windows.Forms.Padding(6);
            this.textBox_start.Name = "textBox_start";
            this.textBox_start.ReadOnly = true;
            this.textBox_start.Size = new System.Drawing.Size(64, 17);
            this.textBox_start.TabIndex = 8;
            this.textBox_start.Text = "START:";
            // 
            // label_price
            // 
            this.label_price.AutoSize = true;
            this.label_price.BackColor = System.Drawing.Color.White;
            this.label_price.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_price.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.label_price.Location = new System.Drawing.Point(7, 510);
            this.label_price.Name = "label_price";
            this.label_price.Size = new System.Drawing.Size(45, 17);
            this.label_price.TabIndex = 13;
            this.label_price.Text = "Price";
            this.label_price.Visible = false;
            // 
            // label_volume
            // 
            this.label_volume.AutoSize = true;
            this.label_volume.BackColor = System.Drawing.Color.White;
            this.label_volume.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_volume.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.label_volume.Location = new System.Drawing.Point(1, 714);
            this.label_volume.Name = "label_volume";
            this.label_volume.Size = new System.Drawing.Size(59, 17);
            this.label_volume.TabIndex = 15;
            this.label_volume.Text = "Shares";
            this.label_volume.Visible = false;
            // 
            // comboBox_AnnotationsVisibility
            // 
            this.comboBox_AnnotationsVisibility.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_AnnotationsVisibility.FormattingEnabled = true;
            this.comboBox_AnnotationsVisibility.Items.AddRange(new object[] {
            "Hide Annotations",
            "Show All Annotations",
            "Show Peaks Only",
            "Show Valleys Only"});
            this.comboBox_AnnotationsVisibility.Location = new System.Drawing.Point(33, 267);
            this.comboBox_AnnotationsVisibility.MaxDropDownItems = 2;
            this.comboBox_AnnotationsVisibility.Name = "comboBox_AnnotationsVisibility";
            this.comboBox_AnnotationsVisibility.Size = new System.Drawing.Size(121, 21);
            this.comboBox_AnnotationsVisibility.TabIndex = 17;
            this.comboBox_AnnotationsVisibility.Visible = false;
            this.comboBox_AnnotationsVisibility.SelectionChangeCommitted += new System.EventHandler(this.comboBox_AnnotationsVisibility_SelectionChangeCommitted);
            // 
            // Form_Input
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1245, 764);
            this.Controls.Add(this.comboBox_AnnotationsVisibility);
            this.Controls.Add(this.label_volume);
            this.Controls.Add(this.label_price);
            this.Controls.Add(this.textBox_end);
            this.Controls.Add(this.textBox_start);
            this.Controls.Add(this.button_update);
            this.Controls.Add(this.dateTimePicker_end);
            this.Controls.Add(this.dateTimePicker_start);
            this.Controls.Add(this.button_loadStock);
            this.Controls.Add(this.chart_candlesNvolume);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.InfoText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(24, 15, 24, 15);
            this.Name = "Form_Input";
            this.Text = "Analyze your stocks";
            ((System.ComponentModel.ISupportInitialize)(this.chart_candlesNvolume)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_loadStock;
        private System.Windows.Forms.OpenFileDialog openFileDialog_load;
        private System.Windows.Forms.DateTimePicker dateTimePicker_start;
        private System.Windows.Forms.DateTimePicker dateTimePicker_end;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_candlesNvolume;
        private System.Windows.Forms.Button button_update;
        private System.Windows.Forms.TextBox textBox_end;
        private System.Windows.Forms.TextBox textBox_start;
        private System.Windows.Forms.Label label_price;
        private System.Windows.Forms.Label label_volume;
        private System.Windows.Forms.ComboBox comboBox_AnnotationsVisibility;
    }
}

