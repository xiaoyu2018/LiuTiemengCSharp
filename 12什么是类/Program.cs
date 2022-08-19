using System;

/*学习内容
*1 类的介绍
*2 类的声明和访问级别
*3 类的继承和类成员访问
*4 重写和多态
*/

//1 类的介绍
//类是一种有自然界各种事物抽象出来的数据结构。
//类是一种引用类型的数据类型
//类代表现实世界中的“种类”

//2 类的声明和访问级别
//c#中可以出现类声明的位置：名称空间中（99%以上）、显式名称空间之外、类里（成员类）
//在c#中声明即定义
//类修饰符：new、public、protected、internal、private、abstract、sealed、static
//类的访问控制：public：对依赖的项目可见、internal（默认）：对依赖的项目不可见，在自己的项目内自由访问

//3 类的继承和类成员访问
//类的继承可看作派生类在基类基础上在功能上的横向（对类成员个数的扩充）和纵向（对类成员的重写）扩展
//子类对父类成员必须全盘继承，无法删除某个指定成员
//一个类只有一个基类，但可以实现多个接口
//sealed类不可被继承
//子类访问级别不可超过父类
//在创建类的实例时，递归的调用基类构造器，再调用自身构造器
//实例构造器不被继承！
//类成员的访问级别是以类访问级别为上限的(不是指报错，而是其他项目中的可见性)

//4 重写和多态
//重写与隐藏的发生条件：函数成员，函数名签名一致，可见
//只有构成了重写，才能发挥多态特性
//隐藏并不能发挥多态特性，隐藏基本上用不到，因为重写就包括了隐藏的功能
//属性也可以被重写

namespace _12什么是类
{
    //名称空间里声明的类
    class Program
    {
        //成员类
        class InternalClass
        {

        }

        static void Main(string[] args)
        {
            //Four();
            One();
        }
        //类的声明和访问级别
        static void One()
        {
            Student s1 = new Student() { ID = 1, Name = "赵华琼" };
            s1.Reprot();

            //简单反射
            Type t = typeof(Student);
            Object o = Activator.CreateInstance(t);
            Student s2 = o as Student;
            s2.Name = "马阳";
            Console.WriteLine($"{s2.ID} {s2.Name}");
        }

        //类的继承和类成员访问
        static void Two()
        {
            Type t = typeof(Car);
            Console.WriteLine(t.BaseType);

            //是一个 is a，一个子类的实例从语义上讲也是一个父类的实例
            Car car = new Car("tt");
            Vehicle vehicle = new Vehicle("tt");
            Console.WriteLine(car is Vehicle);
            Console.WriteLine(car is Car);
            Console.WriteLine(car is object);
            Console.WriteLine(vehicle is Car);

            //父类型的引用变量可以引用一个子类型的实例
            Vehicle vehicle1 = new Car("tt");
            Console.WriteLine(vehicle1.Owner);
            //成员继承
            RaceCar raceCar = new RaceCar("tt");
        }

        //类成员访问级别
        //引用项目ClassLibrary2
        static void Three()
        {
            ClassLibrary2.Vehicle vehicle = new ClassLibrary2.Vehicle();

            vehicle.Owner = "Timothy";

        }

        //重写和多态
        static void Four()
        {
            Vehicle vehicle = new Car("timothy");
            Car car = vehicle as Car;
            //隐藏，调用的还是vehicle的SomeOp方法
            vehicle.SomeOp();
            //隐藏，转换成Car类型才是调用Car的SomeOp方法
            car.SomeOp();
            //重写产生的多态，调用的是Car类的Run方法
            vehicle.Run();

            Vehicle v2 = new RaceCar("tt");
            v2.Run();
        }
    }

    //类的继承和成员访问
    //sealed类不可被继承
    /*sealed*/ class Vehicle
    {
        private int _speed;
        public virtual int Speed 
        {
            get { return _speed; }

            set { _speed = value; } 
        }

        public void SomeOp()
        {
            Console.WriteLine("111");
        }
        //必须标记vritual，构成重写
        public virtual void Run()
        {
            Console.WriteLine("Vehicle is running!");
            _speed = 100;
        }
        
        public Vehicle(string owner)
        {
            this.Owner = owner;
        }
        public string Owner { get; set; }
    }

    //子类访问级别不可超过父类
    /*public*/ class Car:Vehicle
    {
        private int _rpm;
        public override int Speed { get { return _rpm / 100; } set { _rpm = value * 100; } }

        public Car(string owner):base(owner)
        {
           //这里Owner已经再基类赋过值了，Car实例中继承的Owner会跟父类一样
           //所以不用再赋值
        }

        public void ShowBaseOwner()
        {
            //base指的是基类的一个实例
            //base只能向上访问一层，不能base.base....
            
            Console.WriteLine(base.Owner);
        }

        //此为隐藏
        public new void SomeOp()
        {
            Console.WriteLine("我把基类给隐藏了");
        }
        //必须标记override，构成重写
        public override void Run()
        {
            Console.WriteLine("Car is running");
            _rpm = 10000;
        }
    }

    class RaceCar:Car
    {
        public RaceCar(string owner):base(owner)
        {

        }

        public override void Run()
        {
            Console.WriteLine("Race car is runnnig!");
        }
    }

    //类介绍相关
    class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public void Reprot()
        {
            Console.WriteLine($"I.m No.{ID} student, my name is {Name}");
        }

        static Student()
        {
            Console.WriteLine("I'm static construtor!");
        }

        public Student()
        {
            Console.WriteLine("I'm construtor!");
        }

        ~Student()
        {
            Console.WriteLine("I'm 析构器！");
        }
    }
}

//名称空间外声明的类（实际上属于c#的全局名称空间）
//大项目绝对不推荐这么做
class Computer
{

}