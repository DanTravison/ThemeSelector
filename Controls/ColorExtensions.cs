using Microsoft.Maui.Graphics;
using System.Reflection;

namespace ThemeSelector.Controls
{
    internal static class ColorExtensions
    {
        static readonly Dictionary<string, string> _names = new ();
        static ColorExtensions()
        {
            foreach (FieldInfo info in typeof(Colors).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                if (info.FieldType == typeof(Color))
                {
                    object infoValue = info.GetValue(null);
                    if (infoValue == null)
                    {
                        continue;
                    }
                    Color color = (Color)infoValue;
                    string id = color.ToHex();
                    if (!_names.ContainsKey(id))
                    {
                        _names.Add(id, info.Name);
                    }
                }
            }
        }

        /// <summary>
        /// Gets a string name for a <see cref="Color"/>.
        /// </summary>
        /// <param name="c">The <see cref="Color"/> to query.</param>
        /// <returns>The string name of for the color; otherwise, the RGB hex string.</returns>
        public static string Name(this Color c)
        {
            string name;
            if (c != null)
            {
                string id = c.ToHex();
                if (!_names.TryGetValue(id, out name))
                {
                    name = id;
                }
            }
            else
            {
                name = "[NULL]";
            }
            return name;
        }
    }
}
