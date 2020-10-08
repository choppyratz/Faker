using System;
using System.Collections.Generic;
using System.Text;
using TracerPluginInterface;

namespace FakerIntGenerator
{
    class IntGenerator : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return type == typeof(int);
        }

        public object Generate(GeneratorContext context)
        {
            return context.Random.Next(1, System.Int32.MaxValue);
        }
    }
}
