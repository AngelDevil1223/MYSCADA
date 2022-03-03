using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySCADA
{
    public static class FormPropertyHelper
    {
        public static string ToStorage(this Size s) => $"{s.Width};{s.Height}";
        public static string ToStorage(this Point s) => $"{s.X};{s.Y}";
        public static string ToStorage(this Font s) => $"{s.FontFamily.Name};{s.Size};{s.Style}";
        public static string ToStorage(this Color s) => $"{s.Name}";

        public static Size ToSize(this string val)
        {
            var elements = val.Split(';');
            return new Size(elements[0].ToInt(), elements[1].ToInt());
        }
        public static Point ToPoint(this string val)
        {
            var elements = val.Split(';');
            return new Point(elements[0].ToInt(), elements[1].ToInt());
        }
        public static Font ToFont(this string val)
        {
            var elements = val.Split(';');
            return new System.Drawing.Font(elements[0], elements[1].ToFloat(), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }
    }
}
