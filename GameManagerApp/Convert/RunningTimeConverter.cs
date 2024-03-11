using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GameManagerApp.Convert
{
    public class RunningTimeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 && values[0] is string startTime && values[1] is string endTime)
            {
                // 尝试解析开始和结束时间
                if (DateTime.TryParse(startTime, out DateTime startDateTime) && DateTime.TryParse(endTime, out DateTime endDateTime))
                {
                    // 提取小时部分并转换为"XX时"格式
                    string startHour = startDateTime.ToString("HH时");
                    string endHour = endDateTime.ToString("HH时");

                    // 如果开始和结束时间的小时部分相同，则只返回一个时间
                    if (startHour == endHour)
                    {
                        return startHour;
                    }
                    else
                    {
                        return $"{startHour} --- {endHour}";
                    }
                }
            }
            return "Unknown";
        }


        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
