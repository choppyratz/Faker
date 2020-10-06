using System;
using System.Collections.Generic;
using System.Text;
using TracerPluginInterface;

namespace FakerStringPlugin
{
    class StringGenerator : IGenerator
    {
        public string typeName { get; } = "String";
        public object generateValue()
        {
            string randomString = "";
            Random rnd = new Random();

            for (int i = 0; i < rnd.Next(100); i++)
            {
                randomString += GenerateChar();
            }

            return (object)randomString;
        }

        private string GenerateChar()
        {
            Random random = new Random();

            return Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65))).ToString();
        }

    }
}
