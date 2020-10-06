using System;
using System.Collections.Generic;
using System.Reflection;

namespace FakerLibrary
{
    public class Faker
    {


        public T Create<T>()
        {
            Generator gen = new Generator();
            return (T)gen.FillDTO(typeof(T));

        }


    }
}
