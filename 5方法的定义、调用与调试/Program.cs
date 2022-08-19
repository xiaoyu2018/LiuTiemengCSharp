using System;
/*学习内容
 *1 方法的由来
 *2 方法的定义与调用
 *3 构造器（一种特殊的方法）
 *4 方法的重载
 *5 方法的调用与栈
 */

//1 方法的由来
//方法是面向对象范畴的概念，对标非面向对象语言中的函数。
//方法以类的成员出现时，便称之为方法。
//C#语言中任何函数、变量都不可以独立与类之外。方法永远都是类或者结构体的成员。
//为什么使用方法：隐藏复杂逻辑、把大算法分解为小算法、方法复用。

//2 方法的定义与调用
//C#中方法声明与定义不分家
//方法名要是动词或动词短语，使用Pascal法命名
//“()”就是调用一个方法的操作符
//声明方法时括号内是形参，调用方法时括号内是实参。

//3构造器（一种特殊的方法）
//即构造函数
//声明的类中没有手动编写构造器时，编译器会默认提供一个。
//ctor是构造器的快捷短语

//4 方法的重载
//重载的方法名字必须完全一致，但签名不能相同（按顺序的参数类型和种类（ref和out）以及类型形参（泛型）不能相同，不包括返回值）。

//5 方法的调用与栈
//调用方法时，像c语言一样会在栈中开辟内存。其局部变量、参数都会存入栈中。

namespace _5方法的定义_调用与调试
{
      
    
    class Program
    {
        static void Main(string[] args)
        {
            //One();
            Two();

        }


        //复用
        static void One()
        {
            Console.WriteLine(new Calculator().GetCylinderVolume(2.0, 1.0));
        }

        //构造器
        static void Two()
        {
            Student st1 = new Student();

            Student st2 = new Student(10,"NoName");

            //两种控制台输出方法
            Console.WriteLine($"{st1.name} {st1.ID}");
            Console.WriteLine("{1} {0}", st2.name, st2.ID);
        }
    }

    //方法复用
    class Calculator
    {
        const double PI = Math.PI;
        public double GetCircleArea(double r)
        {
            return PI * r * r;
        }

        public double GetCylinderVolume(double r,double h)
        {
            return this.GetCircleArea(r) * h;
        }
    }

    //构造器与重载
    class Student
    {
        public int ID;
        public String name;

        //构造器没有返回值类型，访问级别一般设置public
        public Student()
        {
            this.ID = 1;
            this.name = "MR.OK";
        }

        public Student(int ID,String name)
        {
            this.ID = ID;
            this.name = name;
        }

        //重载
        void Add(int a,double b)
        {

        }
        void Add(double a,int b)
        {

        }
        void Add<T>(int a, double b)
        {
            
        }
        void Add(ref int a, out double b)
        {
            b = 1;
        }

    }
}
