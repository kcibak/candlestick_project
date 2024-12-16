namespace projectpractice
{
    partial class Form_separateDisplay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_separateDisplay));
            this.comboBox_AnnotationsV = new System.Windows.Forms.ComboBox();
            this.label_volume = new System.Windows.Forms.Label();
            this.label_price = new System.Windows.Forms.Label();
            this.textBox_end = new System.Windows.Forms.TextBox();
            this.textBox_start = new System.Windows.Forms.TextBox();
            this.dateTimePicker_endD = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_startD = new System.Windows.Forms.DateTimePicker();
            this.chart_candlesNvolumer = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button_newUpdate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart_candlesNvolumer)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox_AnnotationsV
            // 
            this.comboBox_AnnotationsV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_AnnotationsV.FormattingEnabled = true;
            this.comboBox_AnnotationsV.Items.AddRange(new object[] {
            "Hide Annotations",
            "Show All Annotations",
            "Show Peaks Only",
            "Show Valleys Only"});
            this.comboBox_AnnotationsV.Location = new System.Drawing.Point(33, 267);
            this.comboBox_AnnotationsV.MaxDropDownItems = 2;
            this.comboBox_AnnotationsV.Name = "comboBox_AnnotationsV";
            this.comboBox_AnnotationsV.Size = new System.Drawing.Size(121, 21);
            this.comboBox_AnnotationsV.TabIndex = 17;
            this.comboBox_AnnotationsV.Visible = false;
            this.comboBox_AnnotationsV.SelectionChangeCommitted += new System.EventHandler(this.comboBox_AnnotationsV_SelectionChangeCommitted);
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
            // dateTimePicker_endD
            // 
            this.dateTimePicker_endD.Checked = false;
            this.dateTimePicker_endD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker_endD.Location = new System.Drawing.Point(932, 136);
            this.dateTimePicker_endD.Margin = new System.Windows.Forms.Padding(24, 15, 24, 15);
            this.dateTimePicker_endD.MaxDate = new System.DateTime(2024, 9, 12, 0, 0, 0, 0);
            this.dateTimePicker_endD.MinDate = new System.DateTime(1999, 1, 31, 0, 0, 0, 0);
            this.dateTimePicker_endD.Name = "dateTimePicker_endD";
            this.dateTimePicker_endD.Size = new System.Drawing.Size(238, 22);
            this.dateTimePicker_endD.TabIndex = 19;
            this.dateTimePicker_endD.Value = new System.DateTime(2024, 9, 12, 0, 0, 0, 0);
            // 
            // dateTimePicker_startD
            // 
            this.dateTimePicker_startD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker_startD.Location = new System.Drawing.Point(932, 78);
            this.dateTimePicker_startD.Margin = new System.Windows.Forms.Padding(24, 15, 24, 15);
            this.dateTimePicker_startD.MaxDate = new System.DateTime(2024, 10, 24, 0, 0, 0, 0);
            this.dateTimePicker_startD.MinDate = new System.DateTime(1999, 1, 31, 0, 0, 0, 0);
            this.dateTimePicker_startD.Name = "dateTimePicker_startD";
            this.dateTimePicker_startD.Size = new System.Drawing.Size(238, 22);
            this.dateTimePicker_startD.TabIndex = 18;
            this.dateTimePicker_startD.Value = new System.DateTime(2022, 1, 1, 0, 0, 0, 0);
            // 
            // chart_candlesNvolumer
            // 
            chartArea1.AlignWithChartArea = "ChartArea_volume";
            chartArea1.BackSecondaryColor = System.Drawing.Color.White;
            chartArea1.Name = "ChartArea_candles";
            chartArea2.AlignWithChartArea = "ChartArea_candles";
            chartArea2.Name = "ChartArea_volume";
            this.chart_candlesNvolumer.ChartAreas.Add(chartArea1);
            this.chart_candlesNvolumer.ChartAreas.Add(chartArea2);
            this.chart_candlesNvolumer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.chart_candlesNvolumer.Location = new System.Drawing.Point(0, 230);
            this.chart_candlesNvolumer.Margin = new System.Windows.Forms.Padding(24, 15, 24, 15);
            this.chart_candlesNvolumer.Name = "chart_candlesNvolumer";
            this.chart_candlesNvolumer.Padding = new System.Windows.Forms.Padding(5);
            this.chart_candlesNvolumer.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SemiTransparent;
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
            this.chart_candlesNvolumer.Series.Add(series1);
            this.chart_candlesNvolumer.Series.Add(series2);
            this.chart_candlesNvolumer.Size = new System.Drawing.Size(1205, 423);
            this.chart_candlesNvolumer.TabIndex = 5;
            this.chart_candlesNvolumer.Text = "Candlesticks";
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
            this.chart_candlesNvolumer.Titles.Add(title1);
            this.chart_candlesNvolumer.Titles.Add(title2);
            // 
            // button_newUpdate
            // 
            this.button_newUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_newUpdate.Location = new System.Drawing.Point(1218, 73);
            this.button_newUpdate.Margin = new System.Windows.Forms.Padding(24, 23, 24, 23);
            this.button_newUpdate.Name = "button_newUpdate";
            this.button_newUpdate.Size = new System.Drawing.Size(190, 63);
            this.button_newUpdate.TabIndex = 28;
            this.button_newUpdate.Text = "Update";
            this.button_newUpdate.UseVisualStyleBackColor = true;
            this.button_newUpdate.Click += new System.EventHandler(this.button_newUpdate_Click);
            // 
            // Form_separateDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1205, 653);
            this.Controls.Add(this.button_newUpdate);
            this.Controls.Add(this.comboBox_AnnotationsV);
            this.Controls.Add(this.label_volume);
            this.Controls.Add(this.label_price);
            this.Controls.Add(this.textBox_end);
            this.Controls.Add(this.textBox_start);
            this.Controls.Add(this.dateTimePicker_endD);
            this.Controls.Add(this.dateTimePicker_startD);
            this.Controls.Add(this.chart_candlesNvolumer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_separateDisplay";
            ((System.ComponentModel.ISupportInitialize)(this.chart_candlesNvolumer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_AnnotationsV;
        private System.Windows.Forms.Label label_volume;
        private System.Windows.Forms.Label label_price;
        private System.Windows.Forms.TextBox textBox_end;
        private System.Windows.Forms.TextBox textBox_start;
        private System.Windows.Forms.DateTimePicker dateTimePicker_endD;
        private System.Windows.Forms.DateTimePicker dateTimePicker_startD;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_candlesNvolumer;
        private System.Windows.Forms.Button button_newUpdate;
    }
}