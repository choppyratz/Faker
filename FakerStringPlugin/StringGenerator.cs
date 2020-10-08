using System;
using System.Collections.Generic;
using System.Text;
using TracerPluginInterface;

namespace FakerStringPlugin
{
    class StringGenerator : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return type == typeof(string);
        }

        public object Generate(GeneratorContext context)
        {
            return generateValue(context.Random);
        }
        public object generateValue(Random rnd)
        {
            string randomString = "";

            for (int i = 0; i < rnd.Next(100); i++)
            {
                randomString += GenerateChar(rnd);
            }

            return (object)randomString;
        }

        private string GenerateChar(Random rnd)
        {
            Random random = new Random();

            return Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65))).ToString();
        }

    }
}
