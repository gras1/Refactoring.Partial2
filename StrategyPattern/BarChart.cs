using System.Drawing;

namespace SGV
{
    public class BarChart
    {
        private const string DISPLAY_TYPE_FULL = "rpfll";

        public void RenderBarChartBackground(string reportType, Graphics graphics)
        {
            SolidBrush brush;
            if (reportType == DISPLAY_TYPE_FULL)
            {
                brush = new SolidBrush(Color.Red);
                graphics.FillRectangle(brush, 50, 100, 300, 300);
            }
            else
            {
                brush = new SolidBrush(Color.Red);


                graphics.FillRectangle(brush, 50, 100, 150, 150);
            }
            brush.Dispose();
        }
    }
}