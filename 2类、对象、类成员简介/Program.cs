using System;

/*学习内容
 *1 类与对象的关系
 *2 类的三大成员
 *3 类的静态成员与实例成员
 */

//1 类与对象的关系
//对象是类经过实例化后在内存中得到的实体
//有些类不能被实例化，如Math类。
//类的实例使用new操作符来创建。
//实例化出的对象可赋值给引用变量，无引用变量。

//2 类的三大成员
//属性：存储数据，组合起来表示类或对象当前状态。
//方法：表示类或对象能做什么。
//事件：类或对象通知其他类或对象的机制。

//3 类的静态成员与实例成员
//静态成员是类的成员。（人类平均身高）
//实例（非静态）成员是对象的成员。（某个人类实例的身高）
//操作符"."表示对成员的访问

//类的所有成元包括：常量、字段、方法、属性、索引器、事件、运算符、构造函数、析构函数、类型（嵌套类）
namespace _2类_对象_类成员简介
{
    class Program
    {
        static void Main(string[] args)
        {
            //实例化对象，引用变量mm指向实例化出的对象。
            MyClass mm1 = new MyClass();
            MyClass mm2 = mm1;
            //Update是一个实例方法
            mm1.AddOne();
            mm2.AddOne();
            //引用变量是引用型的变量，而非数值型的
            change1(mm1);
            //WriteLine是一个静态方法
            Console.WriteLine(mm1.num);
            change2(mm2);
            Console.WriteLine(mm1.num);


            Console.WriteLine(Math.PI);

        }

        static void change1(MyClass mm)
        {
            mm.AddOne();
        }

        static void change2(MyClass mm)
        {
            mm.num += 2;
        }

    }


    class MyClass
    {
        internal int num { get; set; }

        internal void AddOne()
        {
            ++num;
        }


    }
}
