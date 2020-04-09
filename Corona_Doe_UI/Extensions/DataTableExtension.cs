using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Corona_Doe_UI.Extensions
{
    public static class DataTableExtension
    {
        public static IEnumerable<T> ToEnumerable<T>(this DataTable table) where T : class, new()
        {
            foreach (var row in table.AsEnumerable())
            {
                T obj = new T();

                foreach (var prop in obj.GetType().GetProperties())
                {
                    if (!table.Columns.Contains(prop.Name)) continue;

                    try
                    {
                        PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                        propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                    }
                    catch
                    {
                        continue;
                    }
                }

                yield return obj;
            }
        }
    }
}
