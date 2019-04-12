using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.ServiceModel;

namespace Trending
{


    public class FakeChartForm1 : Form, ServiceReference1.I_TrendingCallback
    {

        static ServiceReference1.I_TrendingClient service;

        static Chart chart1 = new Chart();

        static Dictionary<string, int> tagValues = new Dictionary<string, int>();
        static Dictionary<string, Series> tagSeries = new Dictionary<string, Series>();

        static System.Windows.Forms.Timer timer;

        public FakeChartForm1()
        {
            InitializeComponent();

            //dodaj referencu na servis
            InstanceContext ic = new InstanceContext(this);
            service = new ServiceReference1.I_TrendingClient(ic);

            service.initTrending();

            //inicijalizacija timer-a
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 8000; // specify interval time as you want
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

        }

        public void timer_Tick(object sender, EventArgs e)
        {

            if(chart1.Series.Count > 0)
            {
                foreach (Series series in chart1.Series)
                {
                    if(series.Points.Count > 0)
                    {
                        series.Points.RemoveAt(0);
                    }
                   
                }
            }
            
            //chart1.Series[0].Points.RemoveAt(0);
            chart1.ResetAutoValues(); // Add this line.

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
     

        private void InitializeComponent()
        {
            ChartArea chartArea1 = new ChartArea();
            Legend legend1 = new Legend();
            ((System.ComponentModel.ISupportInitialize)(chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            
            chartArea1.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea1);
            chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            chart1.Legends.Add(legend1);
            chart1.Location = new System.Drawing.Point(0, 0);
            chart1.Name = "chart1";
            chart1.Size = new System.Drawing.Size(284, 262);
            chart1.TabIndex = 0;
            chart1.Text = "chart1";
            chart1.Click += new System.EventHandler(this.chart1_Click);
            // 
            // FakeChartForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 400);
            this.Controls.Add(chart1);
            this.Name = "FakeChartForm1";
            this.Text = "FakeChart";
            
            ((System.ComponentModel.ISupportInitialize)(chart1)).EndInit();
            this.ResumeLayout(false); 
            this.Load += new System.EventHandler(this.Form1_Load);
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }


        public void addNewValue(String tagID, int value)
        {
            if (!tagSeries.ContainsKey(tagID))
            {
                tagSeries[tagID] = new Series(tagID);
                //tagSeries[tagID].Points.AddY(0); //initial value
                chart1.Series.Add(tagSeries[tagID]);
            }

            tagValues[tagID] = value;
            updateValues(tagID);
            chart1.Update();
        }


        public void updateValues(string tagID)
        {
            //foreach (string tagID in tagValues.Keys)
            //{
                tagSeries[tagID].Points.AddY(tagValues[tagID]); //update initial value
            //}

        }

    }

}
