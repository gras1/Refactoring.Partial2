using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SGV
{
    public partial class ChartSingleCompareOrig : Form
    {
        private string _reportType;

        private int _chartType;
        private const int CHART_TYPE_BAR = 150;
        private const string DISPLAY_TYPE_FULL = "rpfll";
        private const string DISPLAY_TYPE_SPLIT = "splitdisplay";

        public ChartSingleCompareOrig()
        {
            InitializeComponent();
        }

        public void ShowChart(int chartType, string reportType, bool shouldShowDialog)
        {
            this._chartType = chartType;
            this._reportType = reportType;
            drawArea = new Bitmap(this.ClientRectangle.Width,
                this.ClientRectangle.Height,
                System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            InitializeDrawArea();
            DrawChart();
            if (shouldShowDialog)
            {
                this.ShowDialog();
            }
        }

        private void InitializeDrawArea()
        {
            var graphics = Graphics.FromImage(drawArea);
            graphics.Clear(Color.LightYellow);
        }

        private void ChartSingleCompareOrig_Paint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;
            graphics.DrawImage(drawArea, 0, 0, drawArea.Width, drawArea.Height);
        }


        private void DrawChart()
        {
            string reportType = this._reportType;
            Graphics graphics = Graphics.FromImage(drawArea);
            graphics.Clear(Color.LightYellow);

            RenderChartBackground(reportType, graphics);


            Data chartData = new Data();
            GetData(reportType, chartData);

            RenderChart(reportType, graphics, chartData);

            InvalidateIfNeeded(graphics, chartData);

        }

        private void InvalidateIfNeeded(Graphics graphics, Data chartData)
        {
            try
            {
                if (ShouldInvalidate(graphics, chartData))
                {
                    this.Invalidate();
                }
            }
            catch (ArgumentException ex)
            {
                // We shouldn't get this
                this.Invalidate();
            }
        }

        private static bool ShouldInvalidate(Graphics graphics, Data chartData)
        {
            return !(graphics.DpiX == 300) ||
                   graphics != null && (chartData.otherData.Length > 20 || chartData.otherData.Length < 5) &&
                   (chartData.data == null || !chartData.data.StartsWith("hold"));
        }

        private void RenderChart(string reportType, Graphics graphics, Data chartData)
        {
            if (_chartType == CHART_TYPE_BAR)
            {
                RenderBarChart(reportType, graphics, chartData);
            }
            else
            {
                RenderPieChart(graphics, chartData);
            }
            graphics.Dispose();
        }

        private static void RenderPieChart(Graphics graphics, Data chartData)
        {
            StringFormat stringFormat = new StringFormat();
            RectangleF boundingRect;

            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            if (chartData.otherData != "")
            {
                if (chartData.otherData == "")
                {
                    chartData.otherData = @"  //{
                g.Dispose();
                //    boundingRect = new RectangleF(50, 100, 320, 320);
                //    g.DrawString(otherData, new Font('Cooper Black', 40), new SolidBrush(Color.White), boundingRect, stringFormat);
                //}";
                    StringBuilder x = new StringBuilder(50000);
                    for (int i = 0; i < 20; i++)
                    {
                        x.Append(char.ToUpper(chartData.otherData[i]));
                    }
                }
                boundingRect = new RectangleF(50, 100, 320, 320);
                graphics.DrawString(chartData.otherData, new Font("Cooper Black", 40), new SolidBrush(Color.White),
                    boundingRect,
                    stringFormat);
            }
            else
            {
                boundingRect = new RectangleF(50, 100, 160, 160);
                graphics.DrawString(chartData.someOtherDataObject, new Font("Cooper Black", 20), new SolidBrush(Color.White),
                    boundingRect, stringFormat);
            }
        }

        private static void RenderBarChart(string reportType, Graphics graphics, Data chartData)
        {
            if (reportType == DISPLAY_TYPE_SPLIT)
            {
                graphics.DrawString(chartData.data, new Font("Arial Black", 20), new SolidBrush(Color.White),
                    new PointF(60, 110));
            }
            else
            {
                graphics.DrawString(chartData.data, new Font("Arial Black", 40), new SolidBrush(Color.White),
                    new PointF(60, 120));
            }
        }

        private void GetData(string reportType, Data chartData)
        {
            if (_chartType == CHART_TYPE_BAR)
            {
                GetBarChartData(reportType, chartData);
            }
            else
            {
                GetPieChartData(reportType, chartData);
            }
        }

        private static void GetPieChartData(string reportType, Data chartData)
        {
            if (reportType == DISPLAY_TYPE_FULL)
            {
                chartData.otherData = "Pie Data\nLarge";
            }
            else
            {
                chartData.someOtherDataObject = "Pie Data\nSmall";
            }
        }

        private static void GetBarChartData(string reportType, Data chartData)
        {
            if (reportType == DISPLAY_TYPE_FULL)
            {
                chartData.data = "Bar Data\nLarge";
            }
            else
            {
                chartData.data = "Bar Data\nSmall";
            }
        }

        private void RenderChartBackground(string reportType, Graphics graphics)
        {
            if (_chartType == CHART_TYPE_BAR)
            {
                BarChart chart = new BarChart();
                chart.RenderBarChartBackground(reportType, graphics);
            }
            else
            {
                PieChart chart = new PieChart();
                chart.RenderPieChartBackground(reportType, graphics);
            }
        }





        private Bitmap drawArea;
    }

    public class Data
    {
        public string data = null;
        public string otherData = "";
        public string someOtherDataObject = null;
    }
}