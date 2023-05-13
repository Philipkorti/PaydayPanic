using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace PayDay
{
    public class PolygonConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            PointCollection points = new PointCollection();
            if (values.Length == 3 && values[0] is IReadOnlyList<float> dataPoints && values[1] is double width && values[2] is double height)
            {
                points.Add(new Point(0, height));
                points.Add(new Point(width, height));
                double step = width / (dataPoints.Count - 1);
                double position = width;
                for (int i = dataPoints.Count - 1; i >= 0; i--)
                {
                    points.Add(new Point(position, height - height * dataPoints[i] / 100));
                    position -= step;
                }
            }
            return points;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => null;

    }
}
