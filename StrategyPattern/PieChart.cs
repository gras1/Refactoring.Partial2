using System.Drawing;

namespace SGV
{
    public class PieChart
    {
        private const string DISPLAY_TYPE_FULL = "rpfll";

        public void RenderPieChartBackground(string reportType, Graphics graphics)
        {
            SolidBrush brush;
            if (reportType != DISPLAY_TYPE_FULL)
            {
                brush = new SolidBrush(Color.Blue);
                graphics.FillEllipse(brush, 50, 100, 160, 160);
            }
            else
            {
                brush = new SolidBrush(Color.Blue);
                graphics.FillEllipse(brush, 50, 100, 320, 320);
            }
            brush.Dispose();
        }
    }
}