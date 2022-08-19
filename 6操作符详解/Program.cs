using System;
using System.Collections.Generic;

/*学习内容
 *1 操作符概览
 *2 操作符本质
 *3 示例
 */

//1 操作符概览
//优先级由上至下递减
//同等级优先级操作符运算时，赋值操作符运算顺序从右向左，其他操作符从左向右。
//基本：. F() a[] x++ x-- new typeof default checked unchecked delegate sizeof ->
//一元：+ - ! ~ ++x --x (T)x await &x *x
//乘法：* / %
//加减：+ -
//移位：>> <<
//关系和类型检测：< > <= >= is as
//判等：== !=
//逻辑：& ^ |
//条件：&& ||
//null合并：??
//三元运算：?:
//赋值及lambada：= *= /= %= += -= <<= >>= &= ^= |= =>

//2 操作符本质
//操作符是函数的简记法。
//操作符不能够脱离与他关联的数据类型，操作符所应用的数据类型不同会执行不同功能。
//C#中可以为自定义数据类型创建操作符
namespace _6操作符详解
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


            Hunam h = new Teacher();
            Console.WriteLine(h is Teacher);
        }

        //直接调用方法
        static void One()
        {
            Person p1 = new Person() { name = "赵琼" };
            Person p2 = new Person() { name = "马阳" };

            List<Person> people = new List<Person>();

            people = Person.GetMarry(p1, p2);

            foreach (var p in people)
            {
                Console.WriteLine(p.name);
            }
        }

        //将方法变为操作符
        static void Two()
        {
            Person p1 = new Person() { name = "赵琼" };
            Person p2 = new Person() { name = "马阳" };

            List<Person> people = new List<Person>();

           /*************************/
            people = p1 + p2;
           /*************************/

            foreach (var p in people)
            {
                Console.WriteLine(p.name);
            }

            //操作符重载了，不会影响其他的功能
            Console.WriteLine(1+2);
        }

        //操作符示例
        static void Three()
        {
            //方法访问操作符 .
            System.IO.File.Create("D:/HelloWorld.txt");

            //方法调用操作符 ()
            Console.WriteLine("123");
            //不调用方法，只使用方法名
            Func<double,double> func = new Func<double, double>(Math.Sqrt);
            func(4);

            //元素访问操作符 []
            //数组
         
            int[] my_array1 = new int[10];
            int[] my_array2 = new int[] { 1,2,3,4,5};
            int[] my_array3 = new int[6] { 1, 2, 3, 4, 5 ,6};
            int[] my_array4 = { 1, 2, 3, 4 };
            Console.WriteLine(my_array3[0]);
            //字典
            Dictionary<string, Student> stu_dic = new Dictionary<string, Student>();
            for (int i = 1; i <= 3; i++)
            {
                Student student = new Student() { name = i.ToString() + "哥", Score = i + 90 };

                stu_dic.Add(student.name, student);
            }
            Console.WriteLine(stu_dic["1哥"].Score);

            //后置自增与自减号 x++ x--
            int x = 100;
            int y = x++;
            Console.WriteLine($"{x} {y}");

            //获取类型操作符 typeof
            Type type = typeof(Student);
            Console.WriteLine(type.FullName);
            Console.WriteLine(type.Namespace);
            foreach (var f in type.GetMethods())
            {
                Console.WriteLine(f);
            }

            //获取类型默认值操作符 default
            //引用类型时null，值类型是0（注意枚举类型是值类型）
            int a = default(int);
            String s = default(String);
            Student st = default(Student);
            Level level = default(Level);
            Console.WriteLine($"{a} {s} {st} {level}");
        }
        
        //操作符示例
        static void Four()
        {
            //new操作符
            //在内存中创建一个实例，并立即调用实例的构造器，最后返回该实例在堆中的首地址。
            //new 还可以调用实例的初始化器{}
            new Person() { name = "111" };
            //因为语法糖，string类和数组可以不需要使用new操作符来创建实例。
            //为匿名类型创建对象
            var person=new { name = "Mr.ok", age = 34 };
            Console.WriteLine(person.name);
            Console.WriteLine(person.GetType().Name);
            //new作为关键字还有其他作用

            //checked与unchecked
            //是否检查一个值在内存中是否有溢出
            uint x = uint.MaxValue;
            Console.WriteLine(x);
            String sx = Convert.ToString(x, 2);
            Console.WriteLine(sx);
            try
            {
                uint y = checked(x + 1);
                Console.WriteLine(y);
            }
            catch (OverflowException e)
            {

                Console.WriteLine("溢出！！！");
            }

            //delegate是为了实现lambda表达式，现在已经过时了。delegate现在常当作关键字来声明委托。

            //sizeof操作符用于获取一个类型在内存所占字节数
            //自定义类型字节长度需要在unsafe下获取
            int len = sizeof(int);
            Console.WriteLine(len);

            //-> 指针相关
        }

        //操作符示例
        static void Five()
        {
            //+ - ! ~
            int x = 100;
            int y = -(-x);
            //按位取反
            int z = ~x;
            //逻辑取反
            bool m = !true;

            //强制类型转换操作符(T)x
            //隐式类型转换：不丢失精度的转换、子类向父类的转换(多态)、装箱
            int xx = int.MaxValue;
            long yy = xx;

            Teacher t = new Teacher();
            Hunam h = t;
            Animal a = t;
            a.Eat();
            h.Think();
            t.Teach();
            //显式类型转换：可能丢失精度的转换、拆箱、Convert类、ToString方法以及各数据类型Parse/TryParse方法
            double d = 1.2;
            int i = (int)d;

            String str1 = Console.ReadLine();
            String str2 = Console.ReadLine();
            Console.WriteLine(str1 + str2);
            //Parse遇到错误格式的输入时直接报错，建议使用TryParse
            double d1 = double.Parse(str1);
            double d2 = double.Parse(str2);
            Console.WriteLine(d1 + d2);
            int i1 = Convert.ToInt32(str1);
            int i2 = Convert.ToInt32(str2);
            Console.WriteLine(i2 + i1);
            Console.WriteLine(d.ToString());
            //自定义类型转换操作符
            Stone s = new Stone() { age = 5000 };
            Monky WuKong = (Monky)s;
            Console.WriteLine(WuKong.age);

            //加减乘除
            //最后结果按算式中数字精度大的来
            //浮点型0作为除数不会报错，会给出无穷
            double wei_ding_xing = double.PositiveInfinity / double.PositiveInfinity;
            Console.WriteLine(wei_ding_xing);

            //移位操作符<< >>，在二进制层面上对一个数移位
            int shit = 2;
            shit <<= 2;//乘4

            //关系运算操作符
            String s1 = "Abc";
            String s2 = "abc";
            Console.WriteLine(s1.ToLower()==s2.ToLower());
            //如c语言中的字符串比较
            Console.WriteLine(String.Compare(s1,s2));

            //类型检验操作符is as
            Teacher t1 = new Teacher();
            Teacher t2 = null;
            Hunam h1 = new Teacher();
            //is 判断的是实例的类型，也就是堆中存放的数据类型，在判断时一切以实例真实类型为准
            //true
            Console.WriteLine(t1 is Teacher);
            //false
            Console.WriteLine(t2 is Teacher);
            //true
            Console.WriteLine(t1 is Hunam);
            //true
            Console.WriteLine(h1 is Hunam);
            //true
            Console.WriteLine(h1 is Teacher);
            //此时把一个Teacher类型的实例的地址交给了o，o本质上是一个Teacher类型的实例的引用变量，但C#将其功能限制为Object
            Object o = new Teacher();
            Teacher tt;
            if (o is Teacher)
            {
                tt = (Teacher)o;
                tt.Teach();
            }
            else
            {
                tt = null;
            }
            //等同于上面的操作
            tt = o as Teacher;

            //可空类型操作符 ？??
            int? xxx = null;
            //如果xxx是null则算成1
            int yyy = xxx ?? 1;
            Console.WriteLine(xxx);
            Console.WriteLine(yyy);

            //二元表达式 ?:
            int score = 61;
            string res = score >= 60 ? "passed" : "failed";
            Console.WriteLine(res);


        }
    }

    //自定义类型转换操作符
    class Stone
    {
        public int age;

        public static explicit operator Monky(Stone stone)
        {
            Monky monky = new Monky();
            monky.age = stone.age / 500;
            return monky;
        }
    }
    class Monky
    {
        public int age;
    }

    //配合子类向父类的转换,类型检验操作符
    class Animal
    {
        public void Eat()
        {
            Console.WriteLine("Eat!");
        }
    }
    class Hunam:Animal
    {
        public void Think()
        {
            Console.WriteLine("Think!");
        }
    }
    class Teacher:Hunam
    {
        new public void Teach()
        {
            Console.WriteLine("Teach!");
        }
    }
    class Car
    {
        public void Run()
        {
            Console.WriteLine("Run!");
        }
    }

    //配合default
    enum Level
    {
        Mid=1,
        Low=0,
        Hihg=2
    }

    //配合字典
    class Student
    {
        public String name;
        public int Score;
    }

    //创建操作符
    class Person
    {
        public String name;

        //原方法
        public static List<Person> GetMarry(Person p1,Person p2)
        {
            List<Person> people = new List<Person>();

            people.Add(p1);
            people.Add(p2);

            for (int i = 0; i < 11; i++)
            {
                Person child = new Person();

                child.name = p1.name + "'s " + i.ToString();

                people.Add(child);
            }

            return people;
        }

        //变为操作符
        //参数之一必须为包含类型
        public static List<Person> operator +(Person p1, Person p2)
        {
            List<Person> people = new List<Person>();

            people.Add(p1);
            people.Add(p2);

            for (int i = 0; i < 11; i++)
            {
                Person child = new Person();

                child.name = p1.name + "'s " + i.ToString();

                people.Add(child);
            }

            return people;
        }
    }
}
