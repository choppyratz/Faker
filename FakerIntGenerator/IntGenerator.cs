using System;
using System.Collections.Generic;
using System.Text;
using TracerPluginInterface;

namespace FakerIntGenerator
{
    class IntGenerator : IGenerator
    {
        public string typeName { get; } = "Int32";
        public object generateValue()
        {
            Random rnd = new Random();
            return (object)rnd.Next(System.Int32.MinValue, System.Int32.MaxValue);
        }
    }
}
