using System;
using FakerLibrary;
using System.Collections.Generic;

namespace Faker
{
    class Program
    {
        static void Main(string[] args)
        {
            var faker = new FakerLibrary.Faker();
            var test = faker.Create<string>();
            var test1 = faker.Create<int>();
            var test5 = faker.Create<Foo>();
            var test6 = faker.Create<List<int>>();
            var test7 = faker.Create<List<Foo>>();
            var test8 = faker.Create<A>();
            Console.ReadKey();
        }
    }

    public class Foo
    {
        public int ab { get; }
        public int ac { get; }
        public Foo(int a, int b, int c)
        {
            ab = a;
            ac = b;
        }
        public Foo(int a)
        {
            ab = a;
        }

        public Foo(int a, int b)
        {
            ab = a;
            ac = b;
        }

        public Foo()
        {

        }

        public int a;
        public long b;
        public double c;
        public string d;
        public DateTime e;
        public char bbb;
    }

    public struct Foo2
    {
        public int ab { get; set; }

        public int a;
        public long b;
        public double c;
        public string d;
        public DateTime e;
        public char bbb;
    }


}
