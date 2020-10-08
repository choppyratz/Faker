using System;
using System.Collections.Generic;
using System.Text;
using TracerPluginInterface;
using System.Reflection;
using System.Collections;

namespace FakerLibrary
{
    class ListGenerator : IGenerator
    {
        private IFaker gen = null;
        public bool CanGenerate(Type type)
        {
            return type.Name.ToString() == "List`1";
        }

        public object Generate(GeneratorContext context)
        {
            var obj = (IList)Activator.CreateInstance(context.TargetType);
            Type[] pms = obj.GetType().GetGenericArguments();

            for (int i = 0; i < 3; i++)
            {
                obj.Add(context.Faker.FillDTO(pms[0]));
            }
            return obj;
        }
    }
}
