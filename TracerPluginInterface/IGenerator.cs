using System;
using System.Collections.Generic;
using System.Text;

namespace TracerPluginInterface
{
    public interface IGenerator
    {
        public string typeName { get; }
        object generateValue();
    }
}
