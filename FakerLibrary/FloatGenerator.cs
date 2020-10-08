using System;
using TracerPluginInterface;

namespace FakerLibrary
{
    class FloatGenerator : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return type == typeof(float);
        }

        public object Generate(GeneratorContext context)
        {
            double mantissa = (context.Random.NextDouble() * 2.0) - 1.0;
            double exponent = Math.Pow(2.0, context.Random.Next(-126, 128));
            return (object)(mantissa * exponent);
        }
    }
}
