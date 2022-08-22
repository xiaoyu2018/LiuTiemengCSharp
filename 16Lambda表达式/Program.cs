using System;

//"Lambda表达式"是一个匿名函数，是一种高效的类似于函数式编程的表达式。
//它可以包含表达式和语句，并且可用于创建委托，支持带有可绑定到委托或表达式树的输入参数的内联表达式。
//所有Lambda表达式都使用Lambda运算符=>，该运算符读作"goes to"。Lambda运算符的左边是输入参数(如果有)，右边是表达式或语句块。
//Lambda表达式x => x * x读作"x goes to x times x"。

namespace _16Lambda表达式
{
    class Program
    {
        static void Main(string[] args)
        {
            //Action a1 = () => Console.WriteLine("hello");

            //Action<int, int> a2 = (x, y) => Console.WriteLine(x+y);

            //Func<int, int, int> f = (x, y) => { return x + y; };

            //a1.Invoke();
            //a2(1, 2);
            //Console.WriteLine(f(3,4));
            
            //委托类型推断在c#10.0以上支持
            //var test = () => { Console.WriteLine(); };
            One((a, b) => { return a * b; });
            One((a, b) => { return (int)Math.Pow(a, b); });
        }

        static void One(Func<int,int,int> func)
        {
            int a = 1, b = 2;

            Console.WriteLine(func(a,b));
        }

    }


}
