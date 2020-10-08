using System;
using System.Collections.Generic;
using System.Text;

namespace TracerPluginInterface
{
    public interface IGenerator
    {
        public object Generate(GeneratorContext context);
        public bool CanGenerate(Type type);
    }
}
