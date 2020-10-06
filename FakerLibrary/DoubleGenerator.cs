using System;
using TracerPluginInterface;

namespace FakerLibrary
{
    public class DoubleGenerator : IGenerator
    {
        public string typeName { get; } = "Double";
        public object generateValue()
        {
            Random random = new Random();
            return (object)random.NextDouble();
        }
    }
}
