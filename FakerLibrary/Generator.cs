using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TracerPluginInterface;
using System.Linq;

namespace FakerLibrary
{
    public class Generator
    {
        private readonly string pluginPath = System.IO.Path.Combine(
                                                Directory.GetCurrentDirectory(),
                                                "plugins");

        private Dictionary<string, IGenerator> generators = new Dictionary<string, IGenerator> 
        {

            {"Single", new FloatGenerator()},
            {"Double", new DoubleGenerator()},
            {"Int64", new LongGenerator()},
            {"DateTime", new DateTimeGenerator()}
        };

        private List<string> fillContext = new List<string> { };

        public Generator()
        {
            loadPlugins();
        }

        private void loadPlugins()
        {
            DirectoryInfo pluginDirectory = new DirectoryInfo(pluginPath);
            if (!pluginDirectory.Exists)
                pluginDirectory.Create();
    
            var pluginFiles = Directory.GetFiles(pluginPath, "*.dll");
            foreach (var file in pluginFiles)
            {
                Assembly asm = Assembly.LoadFrom(file);
                var types = asm.GetTypes().
                                Where(t => t.GetInterfaces().
                                Where(i => i.FullName == typeof(IGenerator).FullName).Any());

                foreach (var type in types)
                {
                    IGenerator plugin = asm.CreateInstance(type.FullName) as IGenerator;
                    generators.Add(plugin.typeName, plugin);
                }
            }
        }
        public object FillDTO(Type type)
        {
            object result = null;
            if (generators.ContainsKey(type.Name))
            {
                result = generators[type.Name].generateValue();
            }
           
            if (result == null)
            {
                switch (type.Name.ToString())
                {
                    case "List`1":
                        var obj = (IList)Activator.CreateInstance(type);
                        Type[] pms = obj.GetType().GetGenericArguments();

                        for (int i = 0; i < 3; i++)
                        {
                            obj.Add(FillDTO(pms[0]));
                        }
                        return obj;
                }
                return FillObject(type);
            }else
            {
                return result;
            }
        }

        public object FillObject(Type type)
        {
            if (fillContext.Contains(type.Name))
            {
                return null;
            }

            FieldInfo[] fields = type.GetFields();
            object fillingObject = null;
            fillContext.Add(type.Name);
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
                    object value = FillDTO(f.FieldType);
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
                    object value = FillDTO(prop.PropertyType);
                    prop.SetValue(fillingObject, value);
                }
            }
            fillContext.RemoveAt(fillContext.Count - 1);
            return fillingObject;
        }

        public object FillList(Type type)
        {
            return null;
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
                    ConstructorParametrs.Add(FillDTO(parametrinfo.ParameterType));
                }

                return maxConstructorParametrsObject.Invoke(ConstructorParametrs.ToArray());
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public object GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }
    }
}
