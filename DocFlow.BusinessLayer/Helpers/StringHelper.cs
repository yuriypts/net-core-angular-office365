using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DocFlow.BusinessLayer.Helpers
{
    public static class StringHelper
    {
        public static string ToSentenceCase(this string str)
        {
            return Regex.Replace(str, "[a-z][A-Z]", m => $"{m.Value[0]} {char.ToLower(m.Value[1])}");
        }
    }
}
