using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karta_Pracy_SMT
{
    public class Charting
    {
        public struct EfficiencyAtTime
        {
            public float efficiency;
            public DateTime time;
        }

        private static void savePoints(List<EfficiencyAtTime> pointsList)
        {
            List<string> fileToSave = new List<string>();
            foreach (var point in pointsList)
            {
                string newLine = Math.Round(point.efficiency * (float)10, 0).ToString() + ";" + point.time;
                fileToSave.Add(newLine);
            }
            File.WriteAllLines("efficiencyPoints.pts", fileToSave.ToArray());
        }

        private static List<EfficiencyAtTime> makeListOfPoints(float newPoint)
        {
            List<EfficiencyAtTime> result = new List<EfficiencyAtTime>();
            if (File.Exists("efficiencyPoints.pts"))
            {
                string[] fileLines = File.ReadAllLines("efficiencyPoints.pts");
                foreach (var line in fileLines)
                {
                    string effString = line.Split(';')[0];
                    if (effString.Trim() == "") continue;
                    float eff = 0;
                    if (!float.TryParse(effString.Replace(",","."), out eff))
                    {
                        Debug.WriteLine("skipped " + effString);
                        continue;
                    }
                    
                    DateTime time = DateTime.Parse(line.Split(';')[1]);
                    EfficiencyAtTime newItem = new EfficiencyAtTime();
                    newItem.efficiency = eff / 10;
                    newItem.time = time;
                    result.Add(newItem);
                }
            }

            if (newPoint > 0)
            {


                EfficiencyAtTime latestPoint = new EfficiencyAtTime();
                latestPoint.time = DateTime.Now;
                latestPoint.efficiency = newPoint;

#if DEBUG

                Random rnd = new Random();
                rnd.Next(80, 120);
                latestPoint.efficiency = newPoint * (float)rnd.Next(80, 120) / 100;
#endif

                result.Add(latestPoint);
            }

            if (result.Count > 16)
            {
                do
                {
                    result.RemoveAt(0);
                } while (result.Count > 16);
            }
            //if (result.Count>16)
            //{
            //    result.RemoveAt(0);
            //}
            savePoints(result);
            return result;
        }

        public static void DrawEfficiencyChart(PictureBox pbChart, float newPoint)
        {
            if (pbChart.ClientRectangle.Width == 0) return;

            List<EfficiencyAtTime> allPoints = makeListOfPoints(newPoint);


            if (allPoints.Count > 0)
            {
                float maxVal = Math.Max(allPoints.Select(ef => ef.efficiency).ToList().Max(), 100);
                float minVal = allPoints.Select(ef => ef.efficiency).ToList().Min();

                float scale = pbChart.ClientRectangle.Height / (float)((maxVal * 1.1));
                float xInterval = (float)(pbChart.ClientRectangle.Width - 10) / allPoints.Count;


                Bitmap DrawArea = new Bitmap(pbChart.ClientRectangle.Width, pbChart.ClientRectangle.Height);
                pbChart.Image = DrawArea;

                Graphics g;
                g = Graphics.FromImage(DrawArea);
                g.SmoothingMode = SmoothingMode.AntiAlias;

                Pen penEfficiencyThisShift = new Pen(Brushes.Lime, 2);
                Pen penEfficiencyPreviousShift = new Pen(Brushes.LightYellow, 2);
                Pen penNorm = new Pen(Brushes.Gray, 1);
                LinearGradientBrush linearGradientBrushThisShift = new LinearGradientBrush(new PointF(10, pbChart.ClientRectangle.Height- maxVal*scale), new PointF(10, pbChart.ClientRectangle.Height), Color.Lime, Color.Transparent);
                LinearGradientBrush linearGradientBrushPreviousShift = new LinearGradientBrush(new PointF(10, pbChart.ClientRectangle.Height - maxVal * scale), new PointF(10, pbChart.ClientRectangle.Height), Color.LightYellow, Color.Transparent);

                float startX = 10;
                float startY = pbChart.ClientRectangle.Height - allPoints[0].efficiency * scale;
                g.Clear(Color.Black);
                //GraphicsPath path = new GraphicsPath();
                //path.AddLine(10, pbChart.ClientRectangle.Height, 10, startY);

                g.DrawLine(penNorm, 10, pbChart.ClientRectangle.Height - 100 * scale, pbChart.ClientRectangle.Width, pbChart.ClientRectangle.Height - 100 * scale);
                g.DrawString("100%", MainForm.DefaultFont, Brushes.Gray, pbChart.ClientRectangle.Width - 40, pbChart.ClientRectangle.Height - 98 * scale);

                g.DrawString(allPoints[0].time.ToString("HH:mm"), MainForm.DefaultFont, Brushes.Gray, 0, pbChart.ClientRectangle.Height - 18);
                Debug.WriteLine("--- " + allPoints.Count + "points");

                List<PointF> thisShiftPoints = new List<PointF>();
                List<PointF> previousShiftPoints = new List<PointF>();

                for (int i = 1; i < allPoints.Count; i++)
                {
                    float nextX = i * xInterval;
                    float nextY = pbChart.ClientRectangle.Height - allPoints[i].efficiency * scale;

                    if (allPoints[i].time  > (DateTools.whatDayShiftIsit(DateTime.Now).fixedDate))
                    {
                        g.DrawLine(penEfficiencyThisShift, startX, startY, nextX, nextY);

                        if (thisShiftPoints.Count == 0)
                        {
                            thisShiftPoints.Add(new PointF(startX, startY));
                        }
                        thisShiftPoints.Add(new PointF(nextX, nextY));
                    }
                    else
                    {
                        g.DrawLine(penEfficiencyPreviousShift, startX, startY, nextX, nextY);

                        if (previousShiftPoints.Count == 0)
                        {
                            previousShiftPoints.Add(new PointF(startX, startY));
                        }
                        previousShiftPoints.Add(new PointF(nextX, nextY));
                    }
                    g.DrawString(allPoints[i].time.ToString("HH:mm"), MainForm.DefaultFont, Brushes.Gray, (float)i * xInterval - 20, pbChart.ClientRectangle.Height - 18);
                    g.DrawLine(penNorm, i * xInterval, pbChart.ClientRectangle.Height - 20, i * xInterval, pbChart.ClientRectangle.Height - allPoints[i].efficiency * scale);

                    //Debug.WriteLine($"line: ({startX}x{startY}) -> ({nextX}x{nextY})");
                    //g.DrawLine(penEfficiencyThisShift, startX, startY, nextX, nextY);
                    //g.DrawLine(penNorm, i * xInterval, pbChart.ClientRectangle.Height - 20, i * xInterval, pbChart.ClientRectangle.Height - allPoints[i].efficiency * scale);
                    //g.DrawString(allPoints[i].time.ToString("HH:mm"), MainForm.DefaultFont, Brushes.Gray, (float)i * xInterval - 20, pbChart.ClientRectangle.Height - 18);

                    //path.AddLine(startX, (float)Math.Round(startY,0), nextX, (float)Math.Round(nextY,0));

                    startX = nextX;
                    startY = nextY;
                }
                if (thisShiftPoints.Count > 0)
                {
                    thisShiftPoints.Insert(0, new PointF(thisShiftPoints.First().X, pbChart.ClientRectangle.Height));
                    thisShiftPoints.Add(new PointF(thisShiftPoints.Last().X, pbChart.ClientRectangle.Height));
                    thisShiftPoints.Add(new PointF(thisShiftPoints.First().X, pbChart.ClientRectangle.Height));

                    GraphicsPath thisShiftPath = new GraphicsPath();
                    thisShiftPath.AddLines(thisShiftPoints.ToArray());
                    g.FillPath(linearGradientBrushThisShift, thisShiftPath);

                }
                if (previousShiftPoints.Count > 0)
                {
                    previousShiftPoints.Insert(0, new PointF(previousShiftPoints.First().X, pbChart.ClientRectangle.Height));
                    previousShiftPoints.Add(new PointF(previousShiftPoints.Last().X, pbChart.ClientRectangle.Height));
                    previousShiftPoints.Add(new PointF(previousShiftPoints.First().X, pbChart.ClientRectangle.Height));

                    GraphicsPath prevShiftPath = new GraphicsPath();
                    prevShiftPath.AddLines(previousShiftPoints.ToArray());
                    g.FillPath(linearGradientBrushPreviousShift, prevShiftPath);
                }

                //path.AddLine(startX, startY, startX, pbChart.ClientRectangle.Height);
                //path.AddLine(startX, pbChart.ClientRectangle.Height, 10, pbChart.ClientRectangle.Height);









                //g.DrawLine(0,)

                //g.DrawLine(mypen, 0, 0, 200, 200);
                //g.Clear(Color.White);
                g.Dispose();
            }
        }

        public static void DrawDayByDayEfficiency(DataGridView grid, PictureBox pbChart)
        {
            if (grid.Columns.Count > 0)
            {
                Dictionary<string, float[]> efficiencyPerDayShift = new Dictionary<string, float[]>();
                foreach (DataGridViewColumn col in grid.Columns)
                {
                    string shift1 = "";
                    string shift2 = "";
                    string shift3 = "";

                    if (grid.Rows[0].Cells[col.Name].Value != null)
                        shift1 = grid.Rows[0].Cells[col.Name].Value.ToString().Split('/')[1].Trim();
                    if (grid.Rows[1].Cells[col.Name].Value != null)
                        shift2 = grid.Rows[1].Cells[col.Name].Value.ToString().Split('/')[1].Trim();
                    if (grid.Rows[2].Cells[col.Name].Value != null)
                        shift3 = grid.Rows[2].Cells[col.Name].Value.ToString().Split('/')[1].Trim();

                    float val1 = 0;
                    float val2 = 0;
                    float val3 = 0;
                    float valSum = 0;

                    float.TryParse(shift1, out val1);
                    float.TryParse(shift2, out val2);
                    float.TryParse(shift3, out val3);
                    valSum = val1 + val2 + val3;

                    float[] val = new float[] { val1, val2, val3, valSum };
                    efficiencyPerDayShift.Add(col.HeaderText, val);
                }

                Bitmap DrawArea = new Bitmap(pbChart.Size.Width, pbChart.Size.Height);
                pbChart.Image = DrawArea;

                Graphics g;
                g = Graphics.FromImage(DrawArea);
                //g.SmoothingMode = SmoothingMode.AntiAlias;

                Pen penEfficiencyThisShift = new Pen(Brushes.Lime, 2);
                int bottomMargin = 18;
                int halfWidth = 20;
                float interval = (pbChart.Width - 10) / grid.Columns.Count;
                float maxValue = efficiencyPerDayShift.Select(v => v.Value[3]).ToArray().Max();
                float scale = (float)(pbChart.Height * 0.8) / (maxValue + bottomMargin);

                int itemCounter = 0;

                Brush firstShiftColor = new SolidBrush(Tools.GetShiftColor(new DateTime(2018, 05, 29, 08, 00, 00)));
                Brush secondShiftColor = new SolidBrush(Tools.GetShiftColor(new DateTime(2018, 05, 29, 15, 00, 00)));
                Brush thirdShiftColor = new SolidBrush(Tools.GetShiftColor(new DateTime(2018, 05, 29, 23, 00, 00)));

                foreach (var dayEntry in efficiencyPerDayShift) 
                {
                    float x = 50 + itemCounter * interval;
                    float y1 = dayEntry.Value[0] * scale;
                    float y2 = dayEntry.Value[1] * scale;
                    float y3 = dayEntry.Value[2] * scale;

                    g.FillRectangle(firstShiftColor, x - halfWidth, pbChart.Height - y3 - bottomMargin, halfWidth * 2, y3);
                    g.DrawRectangle(new Pen(Brushes.White), x - halfWidth, pbChart.Height - y3 - bottomMargin, halfWidth * 2, y3);

                    g.FillRectangle(secondShiftColor, x - halfWidth, pbChart.Height - bottomMargin - y3 - y2, halfWidth * 2, y2);
                    g.DrawRectangle(new Pen(Brushes.White), x - halfWidth, pbChart.Height - bottomMargin - y3 - y2, halfWidth * 2, y2);

                    g.FillRectangle(thirdShiftColor, x - halfWidth, pbChart.Height - bottomMargin - y1 - y2 - y3, halfWidth * 2, y1);
                    g.DrawRectangle(new Pen(Brushes.White), x - halfWidth, pbChart.Height - bottomMargin - y1 - y2 - y3, halfWidth * 2, y1);

                    g.DrawString(dayEntry.Key, MainForm.DefaultFont, Brushes.White, x - 17, pbChart.Height - 13);

                    itemCounter++;
                }
            }
        }
    }
}
