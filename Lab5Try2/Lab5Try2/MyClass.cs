using System;
using System.Collections.Generic;

namespace WpfLab5
{
    internal class MyClass : Dictionary<string, double>
    {
        public static implicit operator List<object>(MyClass v)
        {
            throw new NotImplementedException();
        }
    }
}