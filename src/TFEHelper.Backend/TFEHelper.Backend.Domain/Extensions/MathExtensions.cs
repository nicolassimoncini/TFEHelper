using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Domain.Extensions
{
    public static class MathExtensions
    {
        public static bool IsInRange(this int x, int a, int b)
        {
            return x >= a && x <= b;
        }
    }
}
