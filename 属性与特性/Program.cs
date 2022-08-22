//c#中，其宏定义必须放在所有代码之前
//#define NoBUG 
#define LI 
//#define ZHANG 
#define BUGED 

using System;
using OysterAttributeSample;
using System.Diagnostics;//为了应用条件特性
//Attribute:特性
//Property:属性

//Attribute是一种可由用户自由定义的修饰符（Modifier），可以用来修饰各种需要被修饰的目标
//Attribute的作用是为它们的附着体追加上一些额外的信息（这些信息就保存在附着物的体内）——比如“这个类是我写的”或者“这个函数以前出过问题”等等。
//其会被编译器编译进程序集（Assembly）的元数据（Metadata）里，在程序运行的时候，你随时可以从元数据里提取出这些附加信息来决策程序的运行。


namespace 属性与特性
{
    class Program
    {
        static void Main(string[] args)
        {
            //Two();
            One();
        }
        static void One()
        {
            ToolKit.Func1();
            ToolKit.Func2();
            ToolKit.Func3();
            ToolKit.Func4();
        }

        static void Two()
        {
            var t = typeof(Ship);
            
            foreach (var i in t.GetCustomAttributes(false))
            {
                Console.WriteLine((i as Oyster).Age);
            }
        }
    }

    class ToolKit
    {
        [ConditionalAttribute("BUGED")]//长记法
        [Conditional("ZHANG")]//短记法
        public static void Func1()
        {
            Console.WriteLine("func1...");
        }
        //逗号叠加
        [Conditional("BUGED"), Conditional("LI")]
        public static void Func2()
        {
            Console.WriteLine("func2...");

        }
        [Conditional("NOBUG")]
        [Conditional("LI")]
        public static void Func3()
        {
            Console.WriteLine("func3...");

        }
        [Conditional("NOBUG")]
        [Conditional("ZHANG")]
        public static void Func4()
        {
            Console.WriteLine("func4...");

        }
    }
}
