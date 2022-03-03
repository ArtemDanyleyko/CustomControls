using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomControls.Extensions
{
    public static class StringExtentions
    {
        public static bool IsNullOrEmpty(this string value)
        {
            if (value is null || value.Equals(string.Empty))
            {
                return true;
            }

            return false;
        }
    }
}
