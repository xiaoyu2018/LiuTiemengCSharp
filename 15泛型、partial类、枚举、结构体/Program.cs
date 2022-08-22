using System;
using System.Collections.Generic;

/*学习内容
*1 泛型
*2 partial类
*3 枚举类型
*4 结构体
*/

//1 泛型
//为什么需要泛型：避免成员或类型膨胀
//泛型类型：类、接口、委托...
//泛型成员：属性、方法、字段...
//泛型无处不在！！！
//所有泛型使用之前都要先特化

//2 partial类
//可以帮助减少派生类
//patial常用于entitiy framework和wpf
//在entitiy framework和wpf等中，有些直接提供的类会随着载入数据库或更新界面等自动刷新
//这些类一定都是partial的，更改只需要自己再写一个同名partial类即可

//3 枚举类型
//值类型
//本质上是人为限定取值范围的整数
//比特位式用法
//4 结构体
//值类型
//可实现接口，不能派生自类、结构体
//不能有显示无参构造器
//可以有方法
namespace _15泛型_partial类_枚举_结构体
{
    class Program
    {
        static void Main(string[] args)
        {
            Six();
            //Car car = new Car();
            //car.Stop();
        }
        //泛型类
        static void One()
        {
            Apple apple = new Apple() { Color = "Red" };
            Box<Apple> box1 = new Box<Apple>() { Cargo = apple };
            Box<Book> box2 = new Box<Book>() { Cargo = new Book() { Name = "No name" } };

            Console.WriteLine(box1.Cargo.Color);
            Console.WriteLine(box2.Cargo.Name);
        }
        //泛型接口
        static void Two()
        {
            Student<int> stu1 = new Student<int>() { ID = 1, Name = "lala" };
            Student<string> stu2 = new Student<string>() { ID = "001", Name = "lala" };

            Console.WriteLine(stu2.ID);
            Console.WriteLine(stu1.ID);
        }
        //各种泛型的数据结构
        static void Three()
        {
            //泛型列表
            List<int> list = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(i);
            }
            foreach (int i in list)
            {
                Console.WriteLine(i);
            }

            //泛型字典
            IDictionary<int, string> dict = new Dictionary<int, string>();
            dict[0] = "Timothy";
            dict[1] = "Mike";
            Console.WriteLine($"Stu No.1 is {dict[1]}");
        }
        //泛型方法
        static void Four()
        {
            int[] a1 = { 1, 2, 3, 4, 5 };
            int[] a2 = { 1, 2, 3, 4, 5 };
            double[] b1 = { 1.1, 2.2, 3.3 };
            double[] b2 = { 1.1, 2.2, 3.3 };

            int[] a3 = Zip<int>(a1, a2);
            double[] a4 = Zip<double>(b1, b2);
            //将不同元素间添加逗号组合成一个字符串
            Console.WriteLine(string.Join(",", a3));
            Console.WriteLine(string.Join(",", a4));
        }

        //泛型方法
        static TArray[]Zip<TArray>(TArray[]a1, TArray[]a2)
        {
            TArray[] zipped = new TArray[a1.Length + a2.Length];
            int ai = 0, bi = 0, zi = 0;
            do
            {
                if (ai < a1.Length) zipped[zi++] = a1[ai++];
                if (bi < a2.Length) zipped[zi++] = a1[bi++];

            } while (ai < a1.Length || bi < a2.Length);

            return zipped;
        }

        //泛型委托
        static void Five()
        {
            Action<string> a1 = Say;
            Action<int> a2 = Mul;

            a1("shit");
            a2.Invoke(20);

            //配合匿名函数
            //lambda表达式中的俩double也可不写
            Func<double, double, double> func1 = (double a, double b) => { return a + b; };

            Console.WriteLine(func1(1.2, 2));
        }
        static void Say(string s)
        {
            Console.WriteLine($"hello {s}");
        }
        static void Mul(int x)
        {
            Console.WriteLine($"{x*100}");
        }

        //枚举
        static void Six()
        {
            Person p1 = new Person() { Level = Level.Boss };
            Person p2 = new Person() { Level = Level.BigBoss };

            Console.WriteLine(p1.Level<p2.Level);
            Console.WriteLine(p1.Level);
            Console.WriteLine((int)Level.Employee);
            Console.WriteLine((int)Level.Manager);
            Console.WriteLine((int)Level.Boss);
            Console.WriteLine((int)Level.BigBoss);

            //比特位用法
            Person Timothy = new Person();
            //Timothy会编程、开车、教学
            //按位或
            Timothy.Skill = Skill.Drive | Skill.Teach | Skill.Program;
            //打印13就是对应二进制
            Console.WriteLine(Timothy.Skill);
            //不是全0就代表会
            Console.WriteLine((Timothy.Skill&Skill.Program)>0);
        }

        //结构体
        static void Seven()
        {
            SSStudent sSStudent = new SSStudent(name: "laozhang", ID: 12);
            Console.WriteLine(sSStudent.ID);
            Console.WriteLine(sSStudent.Name);

        }
    }

    //泛型类
    //<>中写类型参数
    //在调用时，<>中写什么类型，TCargo就变成什么类型
    class Box<TCargo>
    {
        public TCargo Cargo { get; set; }
    }
    class Apple
    {
        public string Color { get; set; }
    }

    class Book
    {
        public string Name { get; set; }

    }

    //泛型接口
    interface IUnique<TId>
    {
        //接口中可以有属性
        TId ID { get; set; }

    }
    //如果一个类实现了一个未被特化的泛型接口
    //那这个类也是泛型的
    class Student<TId> : IUnique<TId>
    {
        public TId ID { get; set; }

        public string Name { get; set; }
    }

    //实现已特化的泛型接口
    //这个类不是泛型类
    class SStudent : IUnique<string>
    {
        //实现接口中的属性
        public string ID { get; set; }
    }

    //partial类
    public partial class Car
    {
        public string ID { get; set; }
        public int Speed { get; set; }
    }
    //partial类必须都在同一个名称空间
    //不需要在同一个文件中，这种添加额外方法的情况也可使用扩展方法实现
    public partial class Car
    {
        public void Run()
        {
            Console.WriteLine("car is running");
        }
    }
    public static class CarExtension
    {
        public static void Stop(this Car car)
        {
            Console.WriteLine("car is stoped");
        }
    }

    //枚举类型
    class Person
    {
        public Level Level { get; set; }
        public Skill Skill { get; set; }
    }
    //最后一个枚举值逗号加不加都可
    enum Level
    {
        Employee=14,
        Manager,      //15
        Boss=56,
        BigBoss,     //57
    }

    //枚举的比特位用法
    enum Skill
    {
        Drive=1,       //0001
        Cook=2,        //0010
        Program =4,    //0100
        Teach = 8      //1000
    }

    //结构体
    //是值类型的，这是和类的一个显著区别
    //注意和引用类型的区别
    //只可以实现接口，不能继承
    interface ISpeak
    {
        void Report();
    }
    struct SSStudent:ISpeak
    {
        public SSStudent(int ID,string name)
        {
            this.ID = ID;
            Name = name;
        }
        public int ID { get; set; }
        public string Name { get; set; }

        public void Report()
        {
            Console.WriteLine($"I'm {Name}");
        }
    }
}
