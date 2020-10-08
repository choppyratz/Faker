using System;
using System.Collections.Generic;
using System.Text;

namespace TracerPluginInterface
{
    public interface IFaker
    {
        public List<Type> fillContext { get; set; }
        public List<IGenerator> generators { get; set; }
        public object FillDTO(Type type);
    }
}
