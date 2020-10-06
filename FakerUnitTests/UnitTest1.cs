using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakerLibrary;
using System;
using System.Collections.Generic;

namespace FakerUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void PrimitiveTypeFillingTest()
        {
            int? i = null;
            long? j = null;
            double? k = null;
            DateTime? z = null;
            string e = null;
            var faker = new FakerLibrary.Faker();
            i = faker.Create<int>();
            j = faker.Create<long>();
            k = faker.Create<double>();
            z = faker.Create<DateTime>();
            e = faker.Create<string>();
            Assert.IsNotNull(i);
            Assert.IsNotNull(j);
            Assert.IsNotNull(k);
            Assert.IsNotNull(z);
            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void ObjectFillingTest()
        {
            var faker = new FakerLibrary.Faker();
            object test = null;
            test = faker.Create<Test1>();
            Assert.IsNotNull(test);
        }

        [TestMethod]
        public void RecursionFillingTest()
        {
            var faker = new FakerLibrary.Faker();
            A test = null;
            test = faker.Create<A>();
            Assert.IsNull(test.b.c.a);
        }

        [TestMethod]
        public void StructFillingTest()
        {
            var faker = new FakerLibrary.Faker();
            Test2 test = faker.Create<Test2>();
            Assert.AreNotEqual(test.a, 0);
            Assert.AreNotEqual(test.b, 0);
            Assert.AreNotEqual(test.c, 0.0);
            Assert.AreNotEqual(test.d, "");
            Assert.AreNotEqual(test.ab, 0);
        }

        [TestMethod]
        public void ListFillingTest()
        {
            var list = new List<Test1> { };
            var faker = new FakerLibrary.Faker();
            list = faker.Create<List<Test1>>();
            Assert.AreNotEqual(list.Count, 0);
        }
    }

    public class Test1
    {
        public Test1(int a, int b, int c)
        {
            ab = a;
        }

        public Test1()
        {

        }
        public int ab { get; set; }

        public int a;
        public long b;
        public double c;
        public string d;
        public DateTime e;
        public char bbb;
    }

    public class A
    {
        public B b { get; set; }
    }

    public class B
    {
        public C c { get; set; }
    }

    public class C
    {
        public A a { get; set; }
    }

    public struct Test2
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
