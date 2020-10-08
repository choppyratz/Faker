using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TracerPluginInterface;
using System.Linq;

namespace FakerLibrary
{
    public class Generator : IFaker
    {
        private readonly string pluginPath = System.IO.Path.Combine(
                                                Directory.GetCurrentDirectory(),
                                                "plugins");



        public List<IGenerator> generators { get; set; } = new List<IGenerator> {
            new LongGenerator(),
            new DoubleGenerator(),
            new FloatGenerator(),
            new DateTimeGenerator(),
            new ListGenerator()
        };

        public List<Type> fillContext { get; set; } = new List<Type> { };

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
                    generators.Add(plugin);
                }
            }
        }
        public object FillDTO(Type type)
        {
            IGenerator generator = null;
            foreach (IGenerator g in generators)
            {
                if (g.CanGenerate(type))
                {
                    generator = g;
                    break;
                }
            }

            if (generator == null && type.IsPrimitive)
            {
                return null;
            }

            if (generator == null)
            {
                generator = new ObjectGenerator();
            }



            GeneratorContext gC = new GeneratorContext(new Random(), type, this);
            object result = generator.Generate(gC);
            return result;
        }
    }
}
