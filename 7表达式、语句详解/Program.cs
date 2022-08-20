using System;
using System.Timers;
using System.Collections;

/*学习内容
 *1 表达式定义
 *2 各类表达式实例
 *3 语句定义
 *4 语句详解
 */

//1 表达式定义
//表达式是一种专门用于求值的语法实体
//c#语言中，表达式是由一个或多个操作数+零个或多个操作符组装成的式子。任何能拿到一个值的语句都是表达式，操作符存在的意义就是为了构成表达式。
//表达式是算法逻辑的最小单元，表达一定算法意图。

//3 语句定义
//语句是命令式编程语言中最小的独立可执行元素，用于控制逻辑走向，陈述算法思想。注意独立可执行。
//程序便是由一系列语句组成的，表达式是语句的内部组件。
//C#中语句以分号结尾，但由分号结尾的不一定是语句（如using指令、字段声明等）。
//语句一定出现在方法体。

//4 语句详解
//语句分为：标签语句、声明语句、嵌入式语句
namespace _7表达式_语句详解
{
    class Program
    {
        static void Main(string[] args)
        {
            //Three();

            Two();
           
        }

        //表达式实例
        static void One()
        {
            int x;
            //表达式得到的值是数
            x = 100;
            x++;++x;
            //表达式得到的值是对象
            new Timer();
            //表达式得到的值是方法
            //Console.WriteLine这个表达式得到的是WriteLine方法，其中操作符是成员访问操作符"."
            Action action = new Action(Console.WriteLine);
            //表达式得到的值是名称空间
            //System.Windows

            //表达式得到一组方法，如果不写括号，会得到Console.WriteLine的所有重载方法。
            //此表达式有两个操作符，.和()。二者为同优先级操作符，执行顺序从左到右。
            Console.WriteLine("hello");
        }

        //语句详解
        static void Two()
        {
            //if语句
            int score = 100;
            if(score>=60)
                if(score>=85)
                    Console.WriteLine("Good!");
                else
                    Console.WriteLine("Pass!");
            else
                Console.WriteLine("failed!");

            //声明语句，var变量必须赋初值且不能为null
            int? a = 1, b = 2, c = 3+5;
            var xx = 1;
            int[] myarray = { 1, 2, 3 };
            const string s = "shit";

            //块语句，用于只允许使用单个语句的上下文中编写多条语句
            {
                //声明语句
                int x = 100;

                if (x>80)
                {
                    Console.WriteLine(x);
                }

            hello: Console.WriteLine("hello");
                //goto hello;
                //注意作用域
                Console.WriteLine(a);
            }

            //switch语句
            //括号内成为switch表达式，其类型只能为整数类型、布尔型、字符型、字符串型、枚举类型及前面类型的可空类型。
            //C#中case语句不能像c语言中从一个case贯穿到另一个case，但可以方便地使用goto
            //case后跟的必须是一个常量值
            //一旦case下有语句，就必须最终有break结尾（goto到其他case的break也行）
            int sscore = 101;
            String @class = "error";
            switch (sscore/10)
            {
                case 10:
                    if (sscore == 100)
                        goto case 8;
                    goto default;
                case 9: 
                case 8: 
                    @class = "A";break;
                case 7:
                case 6:
                    @class = "B";break;
                case 5:
                case 4:
                    @class = "C";break;
                default:
                    if(sscore>=0&&sscore<=100)
                        @class = "D";break;
            }
            Console.WriteLine(@class);
            //对枚举类型，switch会自动生成代码。
            Level level = Level.Hgih;
            switch (level)
            {
                case Level.Hgih:
                    break;
                case Level.Mid:
                    break;
                case Level.Low:
                    break;
                default:
                    break;
            }

            //try语句
            //尝试执行一个语句块，捕捉执行时的异常。
            //静悄悄的吃掉异常，不导致程序的崩溃
            MyCalculator calculator = new MyCalculator();
            int r = 0;
            try
            {
                r = calculator.Add("9999999999999999999999999", "2");
                Console.WriteLine(r);
            }
            catch (OverflowException e)
            {
                Console.WriteLine(e.Message);
            } 
        }

        //语句详解
        static void Three()
        {
            //迭代语句
            int a = 50;
            int b = 50;
            int score = 0;
            //while语句
            while((a+b)==100)
            {
                a = int.Parse( Console.ReadLine());
                b = int.Parse(Console.ReadLine());
               
                score++;
            }
            Console.WriteLine($"你的得分为{score-1}！");
            //do语句
            do
            {
                try
                {
                    a = int.Parse(Console.ReadLine());
                    b = int.Parse(Console.ReadLine());
                }
                catch 
                {

                    Console.WriteLine("输入错误，请重新输入！");
                    continue;
                }
                
                score++;
            } while ((a + b) == 100);
            Console.WriteLine($"你的得分为{score - 1}！");
            //for语句
            //i++部分在每一次执行for语句后执行
            //int i = 0只在第一次执行for语句之前执行
            //i < 50在每一次执行for语句之前执行
            for (int i = 0; i < 50; i++)
            {
                Console.WriteLine(i+1);
            }

            for (int i = 1; i <= 9; i++)
            {
                Console.WriteLine();
                for (int j = 1; j <= i; j++)
                {
                    Console.Write($"{i}x{j}={i*j} ");
                }
            }
            //foreach语句
            //c#中所有实现了IEnumerable的类都可以被遍历。
            int[] array = { 1, 23, 43, 34, 5, 34, 65 };
            foreach (var i in array)
            {
                Console.WriteLine(i);
            }

            //迭代器
            IEnumerator enumerator = array.GetEnumerator();
            while(enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }
            enumerator.Reset();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }

        }
        //return语句
        static void Greeting(string name)
        {
            //尽早return，if语句更加短小，异常情况显示明显
            if (string.IsNullOrEmpty(name))
                return;
            Console.WriteLine($"hello {name}");
        }

    }
    

    //配合try
    class MyCalculator
    {
        public int Add(String s1,String s2)
        {
            int a = 0;
            int b = 0;
            bool has_error = false;
            try
            {
                a = int.Parse(s1);
                b = int.Parse(s2);
            }
            //这样写也能精确的显示具体异常信息
            //catch(Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
            //若不写括号，则是捕捉全部类型异常
            catch (ArgumentException)
            {
                Console.WriteLine("Your argument(s) are null !");
                has_error = true;
            }
            //也可以把异常标识符填上，来获取异常更具体的信息。
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Your argument(s) are not number !");
                has_error = true;
            }
            //抛出异常：谁调用此方法，谁来处理这个异常
            catch (OverflowException e)
            {
                //Console.WriteLine("Out of reange !");
                //has_error = true;
                throw e;
            }
            //无论是否异常，finally语句永远都会被执行
            //一般用于释放系统资源和显示程序执行成功与否
            finally
            {
                if(has_error == true)
                    Console.WriteLine("exit with EXCEPTION!");
                else
                    Console.WriteLine("sucessfully done!");
            }

            int result = checked(a+b);
            return result;
        }
    }

    //配合switch
    enum Level
    {
        Hgih,
        Mid,
        Low
    }

}
