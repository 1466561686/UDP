using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace UDP.Tools
{
    public class EmptyHelper
    {
        public static bool isEmpty(string value)
        {
            if (value == null || value == "")
            {
                return true;
            }
            return false;
        }

        public static bool isEmpty(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return true;
            }
            return false;
        }

        public static bool isEmpty<T>(List<T> value)
        {
            if (value == null || value.Count == 0)
            {
                return true;
            }
            return false;
        }
    }
}
