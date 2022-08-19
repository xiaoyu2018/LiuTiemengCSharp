using System;

/*学习内容
 *1 什么是数据类型
 *2 数据类型在c#中作用
 *3 c#语言的类型系统
 *4 变量、对象与内存
 */

//1 什么是数据类型
//是数据在内存中存储时的型号，不同数据类型还具有对相应类型数据的一组操作。
//小内存容纳大尺寸数据丢失精度，发生错误。
//大内存容纳小尺寸数据导致浪费。

//2 数据类型在c#中作用
//告知存储此类型变量所需内存空间大小
//告知此类型的值的最大值最小值范围
//告知此类型包含的成员（方法、属性、事件等）
//此类型由何基类派生
//运行时此类型变量分配在内存何处
//此类型允许的操作（运算）

/*堆与栈
 * 堆：比较大，程序员为变量手动分配的内存。c#语言不同于c语言，其有垃圾回收策略，当堆中一部分内存不被任何引用变量或指针指向时，便释放该部分内存。
 * 栈：比较小，直接声明的变量分配的内存。
 */

//3 c#语言的类型系统
//（1）引用类型：类、接口、委托
//（2）值类型：结构体、枚举
//结构体包含Int32、Double、char等数据类型
//String是引用类型

//4 变量、对象与内存
//变量的用途就是存储数据。其本质上，变量的名字表明了存储位置，变量的类型决定什么样的值能够存入变量。
//变量分类：静态变量、成员变量（实例变量，字段）、数组元素、值参数、引用参数、输出形参、局部变量。
//变量的声明：有效的修饰符组合 类型 变量名 初始化器

//值类型的变量：变量里仅存值，值类型没有对象，所谓对象与变量是一体的。
//引用类型的变量：变量里存储的是对象的内存地址。
//局部变量和调用时的方法的参数存在内存的栈中（如值类型的局部变量和作为局部变量的引用类型的变量等）
//对象（包括实例中的变量）存在内存的堆中
//引用/值类型决定变量存的是纯数据还是地址
//局部变量和调用时的方法的参数/对象决定变量存在栈中还是堆中
//局部变量在使用前必须有初始值
//其他类型的变量，其所占的内存默认值为全0

//常量修饰符 const

//装箱：Object类型的变量要引用的不是一个对象而是一个值类型变量时，编译器会把该值复制到堆上一块新开辟的堆空间，并将该空间地址放入Object类型的变量。
//拆箱：将装箱的Object类型的变量指向的堆空间中数据重新放入一个局部变量之中。
//装箱由栈到堆，拆箱由堆到栈。
namespace 详解类型_变量_对象
{
    class Program
    {
        static void Main(string[] args)
        {
            //One();
            //Two();
            //Three();
            //Four();
            //Five();

            //int[] x=new int[100];
            //Console.WriteLine(x[0]);

            //Six();



        }
        static void One()
        {
            int x;
            
            x = 100;
            long y;
            y = 100L;

            //x = y;
            y = x;

            Console.WriteLine(y);
        }
        //var与dynamic的区别
        static void Two()
        {
            var x = 100;
            //x = "123";

            dynamic y = 100;
            Console.WriteLine(y);
            y = "hello shit";
            Console.WriteLine(y);
        }
        //类型类
        static void Three()
        {
            Type my_type = typeof(Temp);

            foreach(var p in my_type.GetProperties())
            {
                Console.WriteLine(p.Name);
            }

            foreach(var p in my_type.GetMembers())
            {
                Console.WriteLine(p.Name);
            }
        }
        //类型的操作（运算）
        static void Four()
        {
            double result = 3 / 4;
            Console.WriteLine(result);
            result = 3.0 / 4;
            Console.WriteLine(result);
        }
        //不良操作引起栈溢出
        static void Five()
        {
            var bg = new BadGuy();
            bg.BadMethod();
           
        }
        //装箱与拆箱
        static void Six()
        {
            //装箱
            int x = 100;
            object obj = x;
            
            Console.WriteLine(obj);

            //拆箱
            int y = (int)obj;

            Console.WriteLine(y);
        }

    }

    //变量类型
    class Student
    {
        //静态变量
        public static int Amount;

        //成员变量
        //实例化后在堆中存age的数据
        public int age;
        //实例化后在堆中存name的地址，因为String是引用变量
        public String name;

        //数组
        int[] array = new int[100];

        public void One()
        {
            array[0] = 0;
            array[99] = 99;

        }

        //值参数变量
        public double Add(double a,double b)
        {
            //局部变量
            int c = 0;
            return a + b;
        }

        //引用参数变量
        public void Change(ref int a)
        {
            a++;
        }

        //输出参数变量
        public void Output(out int a)
        {
            a = 1;
        }
    }


    class BadGuy
    {
        public void BadMethod()
        {
            //爆栈
            int x = 100;
            BadMethod();
        }
    }
    class Temp
    {
        public int a;
        int b;
        String s;
        public sbyte sb;
        public string code
        {
            get
            {
                return code;
            }
            set
            {
                code = value;
            }
        }
    }
}
