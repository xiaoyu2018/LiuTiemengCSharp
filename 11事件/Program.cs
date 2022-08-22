using System;
using System.Timers;
/*学习内容
*1 初步了解事件
*2 事件的应用
*3 深入理解事件
*4 事件的声明
*5 问题辨析
*/

//1 初步了解事件
//定义：能够发生的什么事情
//角色：使对象或类具有通知能力，是一种类的成员
//     对象o拥有事件e表达的思想是：事件的拥有者内部某些逻辑触发当事件e发生时，o有能力通知别的对象
//使用：用于对象或类间的动作协调与信息传递（消息推送）
//原理：事件模型中两个“5”
//     "发生-->响应"中5个部分：(闹钟)(响)了(->)(你)(起床)，->指订阅关系
//     事件拥有者(对象)、事件(成员)、事件响应者(对象)、事件处理器(成员函数，本质上是一个回调方法)、事件订阅(+=) 
//     "发生-->响应"中5个动作：（1）我有一个事件
//                          （2）一个人或一群人关心我的事件
//                          （3）我的事件发生了
//                          （4）关心此事件的人被依次通知到
//                          （5）被通知到的人根据拿到的事件信息对事件进行响应
//提示：
//事件多用于桌面、手机等开发的客户端编程，因为这些程序经常是用户通过事件来驱动的
//各种编程语言对这个机制的实现方法也不尽相同
//JAVA中没有事件这种成员，也没有委托这种数据类型，其事件通过“接口”来实现
//MVC、MVP、MVVM是事件模式更高级的应用
//日常开发中更常使用的是c#提供的已有事件，自己声明事件的机会较少
//与回调的区分：（1）回调：你告诉我你想让我做什么，我做完自己的事后做你交代的事情。
//            （2）事件：我的事情做完了会通知你，你按照我的通知，做你任何想做的事。

//2 事件的应用
//C#已经为我们封装好了很多事件，如按钮点击事件等，这个事件由用户点击鼠标，操作系统再向按钮传递信息，按钮类实例再发出事件通知
//使用事件时，事件的五个部分可以有不同的组合方式，如：事件拥有者和事件响应者完全独立、事件的拥有者同时也是事件的响应者、
//事件的拥有者是事件响应者的一个字段成员（按钮和窗口）、事件的响应者是事件拥有者的一个字段成员（非重点），详见代码
//本部分只介绍了事件的使用，并未提及事件如何声明
//一个事件可以挂接多个事件处理器，一个事件处理器也可被多个事件挂接

//3 事件的声明
//即自定义事件
//分完整声明方式和简略声明方式

//4 总结
//事件的本质：是委托字段的一个包装器，对委托字段起限制作用，并不是一种特殊的委托类型的字段
//事件对外界隐藏了委托实例的大部分功能，仅暴露添加/移除事件处理器的功能
//用于声明事件的委托类型的命名规定：用于声明FOO事件的委托，一般命名为FooEventHandler
//FooEventHandler的参数一般有两个，第一个为Object类型，名为sender，实际上就是事件的拥有者
//                               第二个为EventArgs类的派生类，名为e，即事件参数
//直接触发事件的方法一般命名为OnFoo，访问级别一般要设置为protected，不能为public，否则又有可能借刀杀人了。
//事件的命名：带有时态的动词或动词短语，如click、closed、closing
//属性与事件：
//属性不是字段，是字段的包装器，保护字段不被滥用
//事件不是委托字段，是委托字段的包装器，保护委托字段不被滥用
//包装器永远都不可能是被包装的东西
namespace _11事件
{
    class Program
    {

        static void Main(string[] args)
        {
            //Four();
            TCustomer customer = new TCustomer();
            customer.Activate();
        }

        //事件的应用:事件拥有者和事件响应者完全独立
        static void One()
        {
            //timer是事件的拥有者
            Timer timer = new Timer();
            
            //每过一千毫秒便触发一次elapsed事件
            timer.Interval = 1000;
            //事件响应者
            Boy boy = new Boy();
            
            //Elapsed是事件，DoSomething是事件处理器
            //在订阅事件时，直接写事件响应者不存在的事件处理器时，使用vs的自动纠错能自动生成事件处理器。
            timer.Elapsed += boy.DoSomething;
            
            timer.Start();

            Console.ReadLine();
        }

        //事件的应用:事件的拥有者是事件响应者的一个字段成员
        static void Two()
        {
            
            Girl girl = new Girl();
            Console.ReadLine();
        }

        //事件的应用:事件的拥有者同时也是事件的响应者
        static void Three()
        {
            MyTimer timer = new MyTimer();
            timer.Interval = 1000;
            timer.Elapsed += timer.DoSomething;

            timer.Start();
            Console.Read();
        }

        //事件的完整与简略声明
        static void Four()
        {
            Customer customer = new Customer();
            Waiter waiter = new Waiter();
            
            //若Order是一个事件，则使用时只能出现在+=/-=左面，所以避免了借刀杀人
            customer.Order += waiter.Action;

            customer.Aciton();
            
            //借刀杀人
            //Customer joker = new Customer();
            //joker.Order += waiter.Action;
            //joker.Order.Invoke(customer, new OrderEventArgs() { DishName = "shit" , Size = "large" });

            
            customer.PayTheBill();
        }

    }

    //事件的应用:事件拥有者和事件响应者完全独立
    class Boy
    {
        //事件处理器
        internal void DoSomething(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Jump!");
        }
    }

    //事件的应用:事件的拥有者是事件响应者的一个字段成员
    class Girl
    {
        //事件拥有者字段
        private Timer timer;
        public Girl()
        {
            
            this.timer = new Timer();
            this.timer.Interval = 1000;
            this.timer.Elapsed += this.DoSomething;
            this.timer.Start();
        }
        //事件处理器
        internal void DoSomething(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Sing!");

        }
    }

    //事件的应用:事件的拥有者同时也是时间的响应者
    class MyTimer : Timer
    {
        internal void DoSomething(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("I respond an event from myself!");
        }
    }



    //事件完整和简略声明
    //委托和委托接收参数的类访问级别必须一致
    //事件参数
    public class OrderEventArgs:EventArgs
    {
        public string DishName { get; set; }
        public string Size { get; set; }
    }
    //首先声明一个委托类型
    //最简单的方法甚至不用声明这个委托,直接用C#提供的EventHandler声明事件，再在事件处理器中用as做类型转换
    public delegate void OrderEventHandler(Customer customer, OrderEventArgs e);
    public class Customer
    {
        //用于完整声明事件
        ////委托字段，用于存储事件处理器
        //private OrderEventHandler orderEventHandler;
        ////点菜事件
        ////事件是基于委托的
        //public event OrderEventHandler Order
        //{
        //    //value指外部委托
        //    add
        //    {
        //        this.orderEventHandler += value;
        //    }
        //    remove
        //    {
        //        this.orderEventHandler -= value;
        //    }
        //}
        
        //简略声明事件，更简便的使用C#提供的EventHandler
        public event OrderEventHandler Order;
        
        //不标为事件他就是一个委托字段
        //这样是不安全的，会出现借刀杀人的情况，其他人点菜给你。
        //public OrderEventHandler Order;

        public double Bill { get; set; }

        public void WalkIn()
        {
            Console.WriteLine("The customer walks into the restaurant");
        }
        public void SitDown()
        {
            Console.WriteLine("The customer Sits on the seat");
        }
        //事件拥有者触发事件的内部逻辑
        public void Think()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Let me think...");
                System.Threading.Thread.Sleep(1000);
            }

            OnOrder("Kongpao Chicken", "large");
        }

        //防止借刀杀人
        protected void OnOrder(string dish,string size)
        {
            //这里在完整声明时需要用orderEventHandler
            //因为事件只能+=/-=左面！！！！！！
            //这里事件出现在别的操作符左面是微软为了方便事件的简略声明而增加的语法糖！
            //有人订阅了点菜事件
            if (Order != null)
            {
                OrderEventArgs e = new OrderEventArgs() { DishName = dish, Size = size };
                this.Order.Invoke(this, e);
            }
        }
        

        public void Aciton()
        {
            Console.ReadLine();
            this.WalkIn();
            this.SitDown();
            this.Think();
        }
        public void PayTheBill()
        {
            Console.WriteLine($"I will pay {Bill} dollars.");
        }
    }
    public class Waiter
    {
        //若使用更简便的EventHandler，参数应该为Object,EventArgs
        //然后在方法体内使用as进行类型转换即可
        public void Action(Customer customer, OrderEventArgs e)
        {

            Console.WriteLine("I will serve u the dish {0}",e.DishName);
            double price = 10;
            switch (e.Size)
            {
                case "small":
                    price *= 0.5;break;
                case "large":
                    price *= 1.5; break; 
                default:
                    break;
            }

            customer.Bill += price;
        }
    }
}
