using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;

namespace aes128tools.Converters
{
    class HexStringConverter:IValueConverter
    {
        public static byte TypeFlag = 0;

        public static  byte[] String2Hex(string s)
        {
            if (String.IsNullOrEmpty(s))
                return new byte[0];

            s = s.Replace(" ", string.Empty);
            //(?!.*[0-9a-fA-F]).*$
            Regex r = new Regex(@"[^0-9a-fA-F]");
            s = r.Replace(s,"");
            int slenght = s.Length;
            byte[] hex = null;

            if (slenght % 2 == 0 && slenght != 1)
            {
                hex = new byte[slenght / 2];

                for (int i = 0; i < slenght; i += 2)
                {
                    hex[i / 2] = System.Convert.ToByte(s.Substring(i, 2), 16);
                }
                TypeFlag = 0;
            }
            else
            {
                TypeFlag = 1;
                hex = Encoding.Default.GetBytes(s);
            }

            return hex;
        }

       public static string Hex2String(byte[] hex)
        {   
            

            if (hex == null)
                return "";

            if (TypeFlag == 1)
            {
                return Encoding.Default.GetString(hex);

            }
            
            return BitConverter.ToString(hex).Replace("-", "");
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Hex2String((byte[])value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return String2Hex((string) value);
        }
    }
}
