using System;
using TracerPluginInterface;

namespace FakerLibrary
{
    class FloatGenerator : IGenerator
    {
        public string typeName { get; } = "Single";
        public object generateValue()
        {
            Random rnd = new Random();
            double mantissa = (rnd.NextDouble() * 2.0) - 1.0;
            double exponent = Math.Pow(2.0, rnd.Next(-126, 128));
            return (object)(mantissa * exponent);
        }
    }
}
