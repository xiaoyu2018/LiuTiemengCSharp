using System;
using System.Threading;
using System.Threading.Tasks;
/*学习内容
*1 什么是委托
*2 委托声明（自定义委托）
*3 委托的使用
*/

//1 什么是委托
//委托是函数指针的升级版
//一切皆地址：变量（数据）是以某个地址为起点的一段内存中所存储的值；函数（算法）：是以某个地址为起点的一段内存中所存储的一组机器语言的指令。
//直接调用：通过函数名来调用函数，CPU通过函数名直接获得函数所在地址并执行。
//间接调用：通过函数指针来调用函数，CPU通过读取函数指针存储的值获得函数所在地址并执行。
//简单的委托：Action和Func

//2 委托声明（自定义委托）
//委托是一种类，其数据类型是引用类型

//3 委托的使用
//模板方法
//借用指定的外部方法来产生结果
//相当于填空题、常位于代码中部、委托有返回值
//回调方法
//动态地调用指定的外部方法，例如我给你一个名片，你在有需要的时候按名片上的信息再找我。
//相当于流水线、常位于代码末尾、委托无返回值

//注意委托的几个缺点
//产生方法级别的紧耦合
//可读性下降，debug难度增加
//把委托回调、异步调用和多线程纠缠在一起，会让代码变得难以维护和阅读
//委托使用不当可能造成内存泄漏和程序性能下降

//委托的高级使用
//多播
//隐式异步调用
//同步：你做完了我在你的基础上接着做（程序的顺序执行、进程同步等）
//异步：两个人同时做，不用同步信息或互相等待（每一次执行程序，其结果不一定相同）
//直接同步调用：使用方法名
//间接同步调用：使用单播或多播的委托
//隐式异步调用：使用委托的BeginInvoke（不受支持，无法使用）
//显式异步调用：使用Thread或Task

//在编程中应该适时地用接口取代委托的功能
namespace _10委托
{
    class Program
    {
        static void Main(string[] args)
        {
            //Six();
            //Two();

            BoxFac fac = new BoxFac();
            fac.WrapWithLog(new PizzaFac(), new Logger());
        }

        //由c#类库准备好的，两个泛型委托Action和Func
        static void One()
        {
            Calculator calculator = new Calculator();
            
            //Action用于接收有或无参数，无返回值的方法
            Action action = new Action(Calculator.Reprot);
            //直接调用
            Calculator.Reprot();
            //间接调用
            action.Invoke();
            action();

            //Func委托，泛型中最后一个代表返回值类型，前面n-1个代表接受方法的参数
            Func<double, double, double> func1 = new Func<double, double, double>(calculator.Add);
            Func<double, double, double> func2 = new Func<double, double, double>(calculator.Sub);
            double x = 100;
            double y = 200;
            double z =func1.Invoke(x, y);
            Console.WriteLine(z);
            z = func2(x, y);
            Console.WriteLine(z);
            
        }

        //委托声明
        static void Two()
        {
            Calculator calculator = new Calculator();
            Calc calc = new Calc(calculator.Add);
            calc +=calculator.Sub;
            calc +=calculator.Add;
            calc += calculator.Multi;
            calc += calculator.Div;
            double x = 100;
            double y = 200;
            double z = 0;

            //返回最后订阅的函数的返回值
            z = calc.Invoke(x, y);
            Console.WriteLine(z);

        }

        //模板方法
        static void Three()
        {
            WrapFctory wrapFctory = new WrapFctory();
            ProductFactory pf = new ProductFactory();

            Func<Product> func2 = new Func<Product>(pf.MakeToyCar);
            Func<Product> func1 = new Func<Product>(pf.MakePizza);
            Box b1 = wrapFctory.WrapProduct(func1);
            Console.WriteLine(b1.Product.Name);
            Box b2 = wrapFctory.WrapProduct(func2);
            Console.WriteLine(b2.Product.Name);

            //此为语法糖
            b1 =wrapFctory.WrapProduct(pf.MakePizza);
            Console.WriteLine(b1.Product.Name);
            b2 = wrapFctory.WrapProduct(pf.MakeToyCar);
            Console.WriteLine(b2.Product.Name);
        }

        //回调方法
        static void Four()
        {
            WrapFctory w = new WrapFctory();
            ProductFactory p = new ProductFactory();
            Logger logger = new Logger();
            Box b1 = w.WrapProductWithLog(p.MakePizza,logger.Log);
            Box b2 = w.WrapProductWithLog(p.MakeToyCar, logger.Log);

            Console.WriteLine(b1.Product.Name);
            Console.WriteLine(b2.Product.Name);

        }
        //多播
        static void Five()
        {
            Student s1 = new Student() { ID = 1, PenColor = ConsoleColor.Red };
            Student s2 = new Student() { ID = 2, PenColor = ConsoleColor.Blue };
            Student s3 = new Student() { ID = 3, PenColor = ConsoleColor.Cyan };

            //直接同步调用
            s2.DoHomeWork();
            //间接同步调用
            //多播委托执行顺序是按封装方法的先后顺序来的
            Action action = new Action(s1.DoHomeWork);
            action += s2.DoHomeWork;
            action += s3.DoHomeWork;
            action();
        }

        //多线程（三个学生同时写作业）
        static void Six()
        {
            Student s1 = new Student() { ID = 1, PenColor = ConsoleColor.Red };
            Student s2 = new Student() { ID = 2, PenColor = ConsoleColor.Blue };
            Student s3 = new Student() { ID = 3, PenColor = ConsoleColor.Cyan };

            Action a1 = new Action(s1.DoHomeWork);
            Action a2 = new Action(s2.DoHomeWork);
            Action a3 = new Action(s3.DoHomeWork);

            //使用thread显式异步调用
            //Thread thread1 = new Thread(new ThreadStart(a1));
            //Thread thread2 = new Thread(new ThreadStart(s2.DoHomeWork));
            //Thread thread3 = new Thread(new ThreadStart(a3));
            //thread1.Start();
            //thread2.Start();
            //thread3.Start();

            //使用Task显式异步调用,推荐使用Task
            Task t1 = new Task(new Action(s1.DoHomeWork));
            Task t2 = new Task(s2.DoHomeWork);
            Task t3 = new Task(s3.DoHomeWork);
            t1.Start();
            t2.Start();
            t3.Start();


            Console.Read();

        }
    }

    //多播相关
    class Student
    {
        private static bool _mutex = true;
        public int ID { get; set; }
        public ConsoleColor PenColor { get; set; }

        public void DoHomeWork()
        {
            for (int i = 0; i < 3; i++)
            {
                while (!_mutex) ;
                _mutex = false;
                Console.ForegroundColor = this.PenColor;
                Console.WriteLine($"Student{ID} doing homework {i} hours");
                _mutex = true;
                Thread.Sleep(1000);
            }
        }
    }

    
    class WrapFctory
    {
        //模板方法
        //这里用一个制造产品接口可以替代委托
        public Box WrapProduct(Func<Product> getProduct)
        {
            Box box = new Box();
            Product product = getProduct.Invoke();
            box.Product = product;
            return box;
        }

        //回调方法
        public Box WrapProductWithLog(Func<Product> getProduct,Action<Product> logCallBck)
        {
            Box box = new Box();
            Product product = getProduct.Invoke();
            box.Product = product;

            logCallBck(product);
            return box;
        }
    }
    class ProductFactory
    {
        public Product MakePizza()
        {
            Product product = new Product() { Name = "Pizza" ,Price = 3};
            return product;
        }

        public Product MakeToyCar()
        {
            Product product = new Product() { Name = "ToyCar",Price = 9 };
            return product;
        }
    }

    //委托声明(两个参数为double，返回值为double)
    public delegate double Calc(double x, double y);

    //委托声明相关
    class Calculator
    {
        public static void Reprot()
        {
            Console.WriteLine("I have 3 methods.");
        }

        public double Add(double a, double b)
        {
            return a + b;
        }

        public double Sub(double a, double b)
        {
            return a - b;
        }

        public double Multi(double a, double b)
        {
            return a * b;
        }

        public double Div(double a, double b)
        {
            return a / b;
        }

    }
}
