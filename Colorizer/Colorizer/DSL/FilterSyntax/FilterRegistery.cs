using Colorizer.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Colorizer.DSL.FilterSyntax
{
    public static class FilterRegistery
    {
        public static MetaImageFilter MetaFilter = new MetaImageFilter();
        public static LockBitmap Bitmap { get; private set; }
        public static PictureBox PictureBox { get; private set; }

        private static Dictionary<string, Type> typeDictionary;
        private static Dictionary<string, Action<string[]>> commandDictionary;
        static FilterRegistery()
        {
            FilterRegistery.typeDictionary = new Dictionary<string, Type>();
            FilterRegistery.commandDictionary = new Dictionary<string, Action<string[]>>();

            FilterRegistery.commandDictionary.Add("clear", (x) => FilterRegistery.MetaFilter.Clear());
            FilterRegistery.commandDictionary.Add("+filter", (x) =>
                {
                    var type = FilterRegistery.typeDictionary[x[1]];
                    var ctor = type.GetConstructor(new Type[] { });
                    var instance = ctor.Invoke(new Type[] { });

                    foreach (var item in x.Skip(2))
                    {
                        FilterRegistery.SetNecessaryPropertiesFromUnaryString(instance, type, item);
                    }
                    FilterRegistery.MetaFilter.Add(instance as IImageFilter);
                });
            FilterRegistery.commandDictionary.Add("show", (x) =>
                {
                    FilterRegistery.PictureBox.Image = FilterRegistery.MetaFilter.Filter(FilterRegistery.Bitmap);
                });
            FilterRegistery.commandDictionary.Add("show-filters", (x) =>
            {
                Console.WriteLine();
                foreach (var item in FilterRegistery.MetaFilter)
                {
                    Console.Write(item.ToString().Split('.').Last() + " ");
                }
                Console.WriteLine();
            });


        }

        public static void RegisterBitmap(LockBitmap bit)
        {
            FilterRegistery.Bitmap = bit;
        }
        public static void RegisterPictureBox(PictureBox pc)
        {
            FilterRegistery.PictureBox = pc;
        }
        public static void Register<T>(T a, string s) where T : IImageFilter, new()
        {
            FilterRegistery.typeDictionary.Add(s, typeof(T));
        }
        public static bool AppendCommand(string command)
        {
            var cleanCommad = command.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                FilterRegistery.commandDictionary[cleanCommad[0]](cleanCommad);
            }
            catch
            {
                return false;
            }
            return true;
        }
        private static void SetNecessaryPropertiesFromUnaryString(object obj, Type tm, string s)
        {
            var split = s.Split('|');
            PropertyInfo info = tm.GetProperty(split[0]);
            if (split[1] == "true" || split[1] == "false")
            {
                info.SetValue(obj, bool.Parse(split[1]));
                return;
            }
            var len = split[1].Length;
            switch (split[1].Last())
            {
                case 'I':
                    info.SetValue(obj, int.Parse(split[1].Remove(len - 1, 1)));
                    return;
                case 'D':
                    info.SetValue(obj, double.Parse(split[1].Remove(len - 1, 1)));
                    return;
            }
        }
    }
}
