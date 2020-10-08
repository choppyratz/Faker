using System;
using System.Collections.Generic;
using System.Text;
using TracerPluginInterface;
using System.Reflection;

namespace FakerLibrary
{
    class ObjectGenerator : IGenerator
    {
        private IFaker gen = null;

        public bool CanGenerate(Type type)
        {
            return type.IsClass;
        }

        public object Generate(GeneratorContext context)
        {
            if (context.Faker.fillContext.Contains(context.TargetType)) {
                return null;
            }

            if (!context.TargetType.IsPrimitive)
            {
                context.Faker.fillContext.Add(context.TargetType);
            }

            gen = context.Faker;
            object result = FillObject(context.TargetType);
            if (!context.TargetType.IsPrimitive)
            {
                context.Faker.fillContext.RemoveAt(context.Faker.fillContext.Count - 1);
            }
            return result;
        }

        public object FillObject(Type type)
        {
            try
            {
                FieldInfo[] fields = type.GetFields();
                object fillingObject = null;
                fillingObject = fillConstructor(type);

                if (fillingObject == null)
                {
                    fillingObject = Activator.CreateInstance(type);
                }

                if (fillingObject == null && (type.IsPrimitive || type.IsClass))
                {
                    return null;
                }

                foreach (FieldInfo f in fields)
                {
                    try
                    {
                        object value = gen.FillDTO(f.FieldType);
                        f.SetValue(fillingObject, value);
                    }
                    catch (Exception e)
                    {

                    }
                }

                PropertyInfo[] props = type.GetProperties();
                foreach (PropertyInfo prop in type.GetProperties())
                {
                    if (prop.CanWrite)
                    {
                        object value = gen.FillDTO(prop.PropertyType);
                        prop.SetValue(fillingObject, value);
                    }
                }
                return fillingObject;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public object fillConstructor(Type type)
        {
            try
            {
                ConstructorInfo maxConstructorParametrsObject = null;
                int maxParametrsCount = 0;
                int i = 0;
                foreach (ConstructorInfo ctor in type.GetConstructors())
                {
                    if (maxConstructorParametrsObject == null)
                    {
                        maxConstructorParametrsObject = ctor;
                        maxParametrsCount = ctor.GetParameters().Length;
                    }

                    ParameterInfo[] parameters = ctor.GetParameters();
                    if (parameters.Length > maxParametrsCount)
                    {
                        maxConstructorParametrsObject = ctor;
                        maxParametrsCount = parameters.Length;
                    }
                }

                if (maxConstructorParametrsObject == null)
                {
                    return null;
                }

                List<object> ConstructorParametrs = new List<object> { };
                foreach (ParameterInfo parametrinfo in maxConstructorParametrsObject.GetParameters())
                {
                    ConstructorParametrs.Add(gen.FillDTO(parametrinfo.ParameterType));
                }

                return maxConstructorParametrsObject.Invoke(ConstructorParametrs.ToArray());
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
