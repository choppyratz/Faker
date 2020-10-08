using System;
using TracerPluginInterface;

namespace FakerLibrary
{
    public class DoubleGenerator : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return type == typeof(double);
        }

        public object Generate(GeneratorContext context)
        {
            return context.Random.NextDouble();
        }
    }
}
