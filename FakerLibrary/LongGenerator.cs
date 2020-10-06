using System;
using TracerPluginInterface;

namespace FakerLibrary
{
    class LongGenerator : IGenerator
    {
        public string typeName { get; } = "Long";

        public object generateValue()
        {
            Random rnd = new Random();
            byte[] buf = new byte[8];
            rnd.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);
            return (object)(Math.Abs(longRand % (100000000000000000 - 100000000000000050)) + 100000000000000000);
        }
    }
}
