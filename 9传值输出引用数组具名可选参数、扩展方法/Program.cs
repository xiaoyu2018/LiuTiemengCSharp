using System;

/*学习内容
*1 方法的各类参数
*2 扩展方法（this参数）
*/

//1 方法的各类参数
//值参数
//声明时不带任何修饰符的参数，相当于一个局部变量，初始值来自于调用该方法时提供的实参
//其对传进来的实参仅进行值上的复制，不对原来的实参在 本身的值 上产生任何影响。
//要注意区分值参数本身的数据类型（值类型还是引用类型，如c语言的指针类型，这是老生常谈了）
//引用参数
//声明时使用ref关键字进行修饰
//引用参数直接指向所传进来参数的内存地址,相当于对这一片内存空间起了一个别名,而非像值参数一样开辟一块新的内存空间存一份拷贝的值
//输出参数
//声明时使用out关键字进行修饰
//输出参数和引用参数一样,直接指向实参的内存空间
//不需遵守局部变量使用前赋值的原则,作为输出参数实参可以不先赋值,但输出参数在方法结束前必须要被赋值.
//数组参数
//声明时使用params关键字进行修饰
//一个方法的参数列表中,只能由一个params参数,并且在最后一个
//具名参数
//调用一个方法时,用冒号带着参数名调用
//可选参数
//调用一个方法时,这个参数可写可不写。如果不写,此参数自动获得声明时的默认值

//2 扩展方法
//为一个数据类型增加扩展，不修改其定义的结构体/类，在其他地方为该数据类型实例/变量增加方法
//用于扩展的方法必须是公有的、静态的
//this参数必须是扩展方法的第一个
//扩展方法必须放到一个静态类里（一般命名为SomeTypeExtension）来统一收纳对某种类型的扩展方法

namespace _9传值输出引用数组具名可选参数_扩展方法
{
    class Program
    {
        static void Main(string[] args)
        {
            //Three();

            
        }

        //参数
        static void One()
        {
            Student student = new Student();
            
            //值类型值参数
            int y = 100;
            student.AddOne(y);
            Console.WriteLine(y);

            //引用类型值参数
            student.Name = "cc";
            Console.WriteLine(student.Name);
            Console.WriteLine(student.GetHashCode());
            ChangeName(student);
            Console.WriteLine(student.Name);

            //值类型引用参数
            int x = 1;
            //调用时也要用ref关键字
            IWantSideEffect(ref x);
            Console.WriteLine(x);

            //引用类型引用参数
            Student s1 = new Student();
            Console.WriteLine(s1.GetHashCode());
            GetHash(ref s1);
            Console.WriteLine(s1.GetHashCode());

            //输出参数
            int input;
            Out(out input);
            Console.WriteLine(input);
            //TryParse
            string str1 = "1.3";
            string str2 = "14.2";
            double d1, d2;
            if(double.TryParse(str1, out d1)&& double.TryParse(str2, out d2))
                Console.WriteLine(d1+d2);
            else
                Console.WriteLine("error");
        }

        //参数
        static void Two()
        {
            //数组参数
            int s=CaculateSum(1, 2, 3, 4, 5, 6);
            Console.WriteLine(s);
            
            int x = 1;
            int y = 2;
            int z = x + y;
            Console.WriteLine("{0}+{1}={2}",x,y,z);

            string str = "Tim;Ton.Tan,Tom,adam";
            string[] result = str.Split(',', ';', '.');
            foreach (string ss in result)
            {
                Console.Write(ss+" ");
            }
            Console.WriteLine("");
            //具名参数、可选参数，ID没写使用可选参数默认值
            PrintInfo(age: 11, name: "shit");
        }

        //可选方法
        static void Three()
        {
            double x = 3.1415926;
            double y = Math.Round(x, 4);
            Console.WriteLine(y);

            //想让double类也有这个Round方法，该怎么做？
            //直接改.NET库源码当然可以(需要自己重新编译)
            //更优雅的方法则是扩展方法
            //只接受一个参数，因为this后面的就是第一个参数，即x
            double z=x.Round(2);
            Console.WriteLine(z);
            
        }

        //配合引用类型值参数
        static void ChangeName(Student s)
        {
            s.Name = "chaged name";
            //获取对象的哈希码,这个值参数也指向实参指向的对象,所以会是相同的.
            //不同的是引用变量s本身的地址与实参的地址不同
            Console.WriteLine(s.GetHashCode());
            //实参不会跟这变
            s = new Student();
            Console.WriteLine(s.GetHashCode());
            
        }

        //配合值类型引用参数
        static void IWantSideEffect(ref int x)
        {
            x += 100;
        }

        //配合引用类型引用参数
        static void GetHash(ref Student s)
        {
            Console.WriteLine(s.GetHashCode());
            //实参跟着变
            s = new Student();
            Console.WriteLine(s.GetHashCode());
        }

        //配合输出参数
        static void Out(out int x)
        {
            x = 100;
        }

        //配合数组参数
        static int CaculateSum(params int[] a)
        {
            int s = 0;
            foreach (int i in a)
            {
                s += i;
            }
            return s;
        }

        //配合具名参数、可选参数
        static void PrintInfo(string name,int age,string ID="001")
        {
            Console.WriteLine($"{name} {age} {ID}");
        }
    }

    //配合参数
    class Student
    {
        //值类型的值参数
        public void AddOne(int x)
        {
            x++;
            Console.WriteLine(x);
        }

        //引用类型的值参数
        public string Name { get; set; }
    }

    //配合扩展方法
    //用此静态类扩展double类型的方法
    static class DoubleExtension
    {
        //用this修饰了double，所以double类型的变量/对象都将拥有Round这个扩展方法
        public static double Round(this double input,int digit)
        {
            double result = Math.Round(input, digit);
            return result;
        }
    }

    static class FloatExtension
    {
        public static float Round(this float a,int b)
        {
            return (float)Math.Round((double)a, b);
        }
    }
}
