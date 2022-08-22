using System;
using System.Collections;

/*学习内容
*1 什么是接口和抽象类
*2 接口与依赖反转
*/

//1 什么是接口和抽象类
//接口和抽象类都是软件工程产物
//具体类->抽象类->接口：越来越抽象。内部实现的东西越来越少
//抽象类是未完全实现逻辑的类（可以有字段和非public成员，他们代表了具体逻辑，当然可以拥有public成员）
//抽象类为复用而生：专门作为基类来使用，具有解耦功能
//封装确定的(除非修bug和增加新功能，不然不要改类的代码)，开放不确定的，不确定的推迟到子类中去实现（抽象方法）。（开闭原则）
//接口是完全未实现逻辑的类（纯虚类，只能有函数成员，成员全部public）
//接口为解耦而生：高内聚，低耦合
//接口是一个协约，有分工必有协作，有协作必有协约
//接口与抽象类都不能被实例化。只能用来声明引用变量，来引用具体类的实例

//2 接口与依赖反转
//接口所有成员都必须是public，反映了接口作为一个“契约”对所有人公开
//接口使自由合作成为可能。
//接口带来的松耦合使功能的提供方变得可替换，减小了功能提供方不可替代的高风险（见手机和用户、引擎和摩托的例子）。
//一般而言，功能的使用者依赖于功能的提供者，功能提供得越直接依赖就越紧。功能提供者出问题，功能使用者也会出问题。
//使用接口实现了依赖反转，反转指的是UML图中的依赖箭头，并非功能提供者和使用者双方的依赖直接反转
namespace _13接口_抽象类_隔离_反射
{
    class Program
    {
        static void Main(string[] args)
        {
            Three();
        }

        //
        static void One()
        {
            Vehicle v1 = new Car();
            Vehicle v2 = new Truck();

            v1.Run();
            v1.Stop();
            v2.Run();
            v2.Stop();
        }
        //一个引入接口的实例
        static void Two()
        {
            int[] nums1 = new int[] { 1, 2, 3, 4, 5 };
            ArrayList nums2 = new ArrayList() { 1, 2, 3, 4, 5 };

            Console.WriteLine(Sum(nums1));
            Console.WriteLine(Sum(nums2));
        }
        
        //配合接口实例
        //也是多态
        static int Sum(IEnumerable e)
        {
            int s = 0;
            foreach (int i in e)
            {
                s += i;
            }

            return s;
        }

        //耦合
        static void Three()
        {
            //未使用接口
            Motor motor = new Motor(new Engine());
            motor.Run(50);
            Console.WriteLine(motor.Speed);

            //使用接口
            PhoneUser user1 = new PhoneUser(new NokiaPhone());
            user1.UsePthone();
            PhoneUser user2 = new PhoneUser(new MiPhone());
            user2.UsePthone();
        }
    }

    //一旦类中有抽象方法（Study），该类一定要被abstract所修饰
    //有abstract修饰的方法是抽象方法，抽象方法不能有方法体
    //类中只要有一个抽象方法，他就是抽象类
    //抽象方法不能是private，因为他要等着子类去实现
    //抽象方法没有方法体，所以又被称为纯虚方法，虚方法（virtual修饰）有方法体，
    abstract class Vehicle:IVehicle
    {
        //可以有具体的字段
        private int _speed;

        //如果实现接口，则不用使用override，因为是实现而非重写
        public void Stop()
        {
            Console.WriteLine("Stop!");
        }

        //在VehicleBase中声明
        //若是实现接口则必须在这里声明虚方法，以推迟实现
        abstract public void Run();
        //要是虚方法的方法体中什么都无，则直接用抽象方法
        //public virtual void Run() { }

    }
    class Car:Vehicle
    {
        public override void Run()
        {
            Console.WriteLine("car is running");
        }
    }

    class Truck: Vehicle
    {
        public override void Run()
        {
            Console.WriteLine("truck is running");
        }
 
    }

    //一个抽象类中，如果所有成员都是抽象的，当然不能有字段（字段必然是具体的）
    //就是一个纯抽象类，可以当成一个接口
    abstract class VehicleBase
    {
        abstract public void Run();
        abstract public void Stop();
    }
    //这种纯抽象类直接搞成一个接口！
    //接口中方法必是public和abstract，所以直接都不用写
    interface IVehicle
    {
        void Run();
        void Stop();
    }

    //紧耦合
    class Engine
    {
        public int Rpm {  get; private set; }

        public void Work(int gas)
        {
            Rpm = 1000 * gas;
        }
    }
    //Motor拥有一个Engine类型的字段，Motor类型就已经依赖Engine类型了
    //二者产生了紧耦合
    //如果引擎出了问题，那么负责车这个类的程序员只能等引擎类修好bug
    //你要想换另一个类来依赖，还得修改Motor类
    class Motor
    {
        private Engine _engine;
        public int Speed { get; private set; }

        public Motor(Engine engine)
        {
            _engine = engine;
        }

        public void Run(int gas)
        {
            _engine.Work(gas);
            Speed = _engine.Rpm / 100;
        }
    }

    //松耦合
    //NokiaPhone类代码出现问题，PhoneUser类也会受影响,但是可以直接换成MiPohne，且不需要修改PhoneUser类
    //这还不是最松的，用反射可以做到只改配置文件，不改C#代码
    interface IPhone
    {
        void Dail();
        void PickUp();
        void Send();
        void Recieve();
    }
    //实现接口可利用vs的自动纠错
    class NokiaPhone:IPhone
    {
        public void Dail()
        {
            Console.WriteLine("Nokia is calling...");
        }

        public void PickUp()
        {
            Console.WriteLine("Nokia is picked up...");

        }

        public void Recieve()
        {
            Console.WriteLine("Nokia recieved...");

        }

        public void Send()
        {
            Console.WriteLine("Nokia is sending...");

        }
    }

    class MiPhone : IPhone
    {
        public void Dail()
        {
            Console.WriteLine("Mi is calling...");
        }

        public void PickUp()
        {
            Console.WriteLine("Mi is picked up...");
        }

        public void Recieve()
        {
            Console.WriteLine("Mi is recieved...");
        }

        public void Send()
        {
            Console.WriteLine("Mi is sending...");
        }
    }

    class PhoneUser
    {
        private IPhone _phone;

        public PhoneUser(IPhone phone)
        {
            _phone = phone;
        }

        public void UsePthone()
        {
            _phone.Dail();
            _phone.PickUp();
            _phone.Recieve();
            _phone.Send();
        }
    }
}

