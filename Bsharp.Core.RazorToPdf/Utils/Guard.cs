
using System;

namespace BSharp.Core.RazorToPdf.Utils
{
    internal static class Guard
    {
        internal static void CheckIsNull(object value, string paramName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}
