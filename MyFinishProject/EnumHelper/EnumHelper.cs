using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MyFinishProject.EnumHelper
{
    public static class EnumHelper
    {
        /// <summary>
        /// Получение Description значения Enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string GetDisplayName<T>(this T source) where T : Enum
        {
            var type = source.GetType();
            if (!type.IsEnum)
                throw new ArgumentException("Тип параметра не является типом перечисления");

            var fi = type.GetField(source.ToString());
            if (fi == null)
                return string.Empty;

            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : source.ToString();
        }
    }
}