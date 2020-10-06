using System;
using TracerPluginInterface;


namespace FakerLibrary
{
    class DateTimeGenerator : IGenerator
    {
        public string typeName { get; } = "DateTime";
        public object generateValue()
        {
            return (object)DateTime.Now;
        }
    }
}
