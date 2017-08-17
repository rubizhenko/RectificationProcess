namespace RectificationProcess
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.процесToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.статичніХарактеристикиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.дефлегматорToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.кипятильникToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ректифікаційнаКолонаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.динамічніХарактеристикиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.дефлегматорToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.кипятильникToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ректифікаційнаКолонаToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 38);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(752, 475);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chart1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(744, 449);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // chart1
            // 
            chartArea3.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea3);
            this.chart1.Location = new System.Drawing.Point(6, 6);
            this.chart1.Name = "chart1";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.MarkerBorderWidth = 3;
            series3.MarkerColor = System.Drawing.Color.Red;
            series3.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series3.Name = "Series1";
            this.chart1.Series.Add(series3);
            this.chart1.Size = new System.Drawing.Size(732, 437);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(744, 449);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.процесToolStripMenuItem,
            this.статичніХарактеристикиToolStripMenuItem,
            this.динамічніХарактеристикиToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1096, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // процесToolStripMenuItem
            // 
            this.процесToolStripMenuItem.Name = "процесToolStripMenuItem";
            this.процесToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.процесToolStripMenuItem.Text = "Процес";
            // 
            // статичніХарактеристикиToolStripMenuItem
            // 
            this.статичніХарактеристикиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.дефлегматорToolStripMenuItem,
            this.кипятильникToolStripMenuItem,
            this.ректифікаційнаКолонаToolStripMenuItem});
            this.статичніХарактеристикиToolStripMenuItem.Name = "статичніХарактеристикиToolStripMenuItem";
            this.статичніХарактеристикиToolStripMenuItem.Size = new System.Drawing.Size(156, 20);
            this.статичніХарактеристикиToolStripMenuItem.Text = "Статичні характеристики";
            // 
            // дефлегматорToolStripMenuItem
            // 
            this.дефлегматорToolStripMenuItem.Name = "дефлегматорToolStripMenuItem";
            this.дефлегматорToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.дефлегматорToolStripMenuItem.Text = "Дефлегматор";
            this.дефлегматорToolStripMenuItem.Click += new System.EventHandler(this.дефлегматорToolStripMenuItem_Click);
            // 
            // кипятильникToolStripMenuItem
            // 
            this.кипятильникToolStripMenuItem.Name = "кипятильникToolStripMenuItem";
            this.кипятильникToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.кипятильникToolStripMenuItem.Text = "Кип\'ятильник";
            this.кипятильникToolStripMenuItem.Click += new System.EventHandler(this.кипятильникToolStripMenuItem_Click);
            // 
            // ректифікаційнаКолонаToolStripMenuItem
            // 
            this.ректифікаційнаКолонаToolStripMenuItem.Name = "ректифікаційнаКолонаToolStripMenuItem";
            this.ректифікаційнаКолонаToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.ректифікаційнаКолонаToolStripMenuItem.Text = "Ректифікаційна колона";
            this.ректифікаційнаКолонаToolStripMenuItem.Click += new System.EventHandler(this.ректифікаційнаКолонаToolStripMenuItem_Click);
            // 
            // динамічніХарактеристикиToolStripMenuItem
            // 
            this.динамічніХарактеристикиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.дефлегматорToolStripMenuItem1,
            this.кипятильникToolStripMenuItem1,
            this.ректифікаційнаКолонаToolStripMenuItem1});
            this.динамічніХарактеристикиToolStripMenuItem.Name = "динамічніХарактеристикиToolStripMenuItem";
            this.динамічніХарактеристикиToolStripMenuItem.Size = new System.Drawing.Size(165, 20);
            this.динамічніХарактеристикиToolStripMenuItem.Text = "Динамічні характеристики";
            // 
            // дефлегматорToolStripMenuItem1
            // 
            this.дефлегматорToolStripMenuItem1.Name = "дефлегматорToolStripMenuItem1";
            this.дефлегматорToolStripMenuItem1.Size = new System.Drawing.Size(202, 22);
            this.дефлегматорToolStripMenuItem1.Text = "Дефлегматор";
            // 
            // кипятильникToolStripMenuItem1
            // 
            this.кипятильникToolStripMenuItem1.Name = "кипятильникToolStripMenuItem1";
            this.кипятильникToolStripMenuItem1.Size = new System.Drawing.Size(202, 22);
            this.кипятильникToolStripMenuItem1.Text = "Кип\'ятильник";
            // 
            // ректифікаційнаКолонаToolStripMenuItem1
            // 
            this.ректифікаційнаКолонаToolStripMenuItem1.Name = "ректифікаційнаКолонаToolStripMenuItem1";
            this.ректифікаційнаКолонаToolStripMenuItem1.Size = new System.Drawing.Size(202, 22);
            this.ректифікаційнаКолонаToolStripMenuItem1.Text = "Ректифікаційна колона";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1096, 587);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Процес ректифікації";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem процесToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem статичніХарактеристикиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem дефлегматорToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem кипятильникToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ректифікаційнаКолонаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem динамічніХарактеристикиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem дефлегматорToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem кипятильникToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ректифікаційнаКолонаToolStripMenuItem1;
    }
}

