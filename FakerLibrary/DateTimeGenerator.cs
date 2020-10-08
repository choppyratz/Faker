using System;
using TracerPluginInterface;


namespace FakerLibrary
{
    class DateTimeGenerator : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return type == typeof(DateTime);
        }

        public object Generate(GeneratorContext context)
        {
            return (object)DateTime.Now;
        }
    }
}
