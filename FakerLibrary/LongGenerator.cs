using System;
using TracerPluginInterface;

namespace FakerLibrary
{
    class LongGenerator : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return type == typeof(long);
        }

        public object Generate(GeneratorContext context)
        {
            byte[] buf = new byte[8];
            context.Random.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);
            return (object)(Math.Abs(longRand % (100000000000000000 - 100000000000000050)) + 100000000000000000);
        }
    }
}
