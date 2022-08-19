using System;

namespace ClassLibrary2
{

    public class Vehicle
    {
        //在任何项目中，任何地方都可访问
        public string Owner { get; set; }
        //仅在本项目中，任何地方都可访问
        internal int MyProperty1 { get; set; }
        //仅限其继承链上所有子类可以访问
        protected int MyProperty2 { get; set; }
        //仅限这个类在类体里自己访问
        //私有字段不用属性，因为不用向外暴露数据
        //命名在前面加_
        private int _myProperty3;
    }

    public class Car:Vehicle
    {
        public Car()
        {
            MyProperty2 = 1;
        }
    }

    class RaceCar:Car
    {

    }
    internal class Unvisable
    {
        //仅在本项目中，任何地方都能访问
        public int MyProperty { get; set; }
        //此时与上条可访问性一致，
        internal int MyProperty1 { get; set; }
    }
}
