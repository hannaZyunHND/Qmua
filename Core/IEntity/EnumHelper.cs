using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MI.Entity
{
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field, AllowMultiple = true)]
    public sealed class EnumDescriptionAttribute : Attribute
    {
        private string description;

        /// <span class="code-SummaryComment"><summary></span>
        /// Gets the description stored in this attribute.
        /// <span class="code-SummaryComment"></summary></span>
        /// <span class="code-SummaryComment"><value>The description stored in the attribute.</value></span>
        public string Description
        {
            get
            {
                return this.description;
            }
        }

        /// <span class="code-SummaryComment"><summary></span>
        /// Initializes a new instance of the
        /// <span class="code-SummaryComment"><see cref="EnumDescriptionAttribute"/> class.</span>
        /// <span class="code-SummaryComment"></summary></span>
        /// <span class="code-SummaryComment"><param name="description">The description to store in this attribute.</span>
        /// <span class="code-SummaryComment"></param></span>
        public EnumDescriptionAttribute(string description)
            : base()
        {
            this.description = description;
        }
    }
    public static class EnumHelper
    {
        /// <span class="code-SummaryComment"><summary></span>
        /// Gets the <span class="code-SummaryComment"><see cref="DescriptionAttribute" /> of an <see cref="Enum" /> </span>
        /// type value.
        /// <span class="code-SummaryComment"></summary></span>
        /// <span class="code-SummaryComment"><param name="value">The <see cref="Enum" /> type value.</param></span>
        /// <span class="code-SummaryComment"><returns>A string containing the text of the</span>
        /// <span class="code-SummaryComment"><see cref="DescriptionAttribute"/>.</returns></span>
        public static string GetDescription(this Enum value)
        {
            try
            {
                if (value == null)
                {
                    return String.Empty;
                    //throw new ArgumentNullException("value");
                }

                string description = value.ToString();
                FieldInfo fieldInfo = value.GetType().GetField(description);
                EnumDescriptionAttribute[] attributes = (EnumDescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0)
                {
                    description = attributes[0].Description;
                }
                return description;
            }
            catch
            {
            }
            return "Not found enum";
        }

        /// <span class="code-SummaryComment"><summary></span>
        /// Converts the <span class="code-SummaryComment"><see cref="Enum" /> type to an <see cref="IList" /> </span>
        /// compatible object.
        /// <span class="code-SummaryComment"></summary></span>
        /// <span class="code-SummaryComment"><param name="type">The <see cref="Enum"/> type.</param></span>
        /// <span class="code-SummaryComment"><returns>An <see cref="IList"/> containing the enumerated</span>
        /// type value and description.<span class="code-SummaryComment"></returns></span>
        public static IList ToList(this Type type, bool withAll = false)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            ArrayList list = new ArrayList();
            Array enumValues = Enum.GetValues(type);
            foreach (object value in enumValues)
            {
                list.Add(new KeyValuePair<byte, string>((byte)value, GetDescription((Enum)value)));
            }
            if (!withAll)
                list.RemoveAt(0);
            return list;
        }
        public static IList ToList2(this Type type, bool withAll = false)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            ArrayList list = new ArrayList();
            Array enumValues = Enum.GetValues(type);
            foreach (Enum value in enumValues)
            {
                list.Add(new KeyValuePair<string, string>(value.ToString(), GetDescription(value)));
            }
            if (!withAll)
                list.RemoveAt(0);
            return list;
        }

        public static Dictionary<int, string> ToDic(this Type type, bool withAll = false)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            Dictionary<int, string> dic = new Dictionary<int, string>();
            Array enumValues = Enum.GetValues(type);
            foreach (var value in enumValues)
            {
                dic.Add(Convert.ToInt32(value), GetDescription((Enum)value));
            }
            if (!withAll)
                dic.Remove((int)0);
            return dic;
        }
        public static T ToEnum<T>(this string enumString) //where T : struct, IConvertible
        {
            //T obj = default(T);
            //Enum.TryParse<T>(enumString, out obj);
            //return obj;
            return (T)Enum.Parse(typeof(T), enumString, true);
        }
        public static Enum ToEnumFromDescription<T>(string des) //where T : struct, IConvertible
        {
            var list = ToList(typeof(T), true);
            foreach (KeyValuePair<Enum, string> item in list)
            {
                if (item.Value.Equals(des, StringComparison.OrdinalIgnoreCase))
                    return item.Key;
            }
            return null;
        }
    }
}
