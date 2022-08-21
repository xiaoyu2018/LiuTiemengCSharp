using System;
using System.Collections.Generic;
/*学习内容
*1 字段详解
*2 属性详解
*3 索引器详解
*4 常量详解
*/

//1 字段详解
//字段是类型的成员
//旧称为成员变量，与对象关联的字段是实例字段，与类型关联的字段是静态字段。
//字段的名字应是名词。

//2 属性详解
//用于访问对象或类型的特征的成员，特征反映了状态
//属性是字段的自然扩展，由Get方法和Set方法进化而来
//对外：暴露数据，数据可以是存储在字段内的，也可以是动态计算出来的，属性可以向外部实时提供更新的值
//对内：保护字段不被非法值污染
//属性实质上是一个语法糖
//代码提示为：简略声明prop；完整声明propfull
//vs重构功能可以将私有字段快速重构为属性！
//建议向外暴露数据时，永远使用属性，而字段永远是private或protected的

//索引器详解
//是类中的一种成员：它使对象能够像数组一样（即使用下标）被索引
//没有静态索引器
//代码提示为：indexer
//索引器一般都用在集合里

//常量详解
//常量作为类成员时，其隶属于类型，即不存在实例常量
//实例常量由只读实例字段来担任
//常量还有局部常量，注意与成员常量区分
//数据类型只能是基本数据类型，不能是自定义类、结构体

//各种只读的总结
//为了提高程序可读性和执行效率，性能最高，因为编译器直接用常量值代替常量标识符--常量
//为了防止对象的值被改变--只读字段
//向外部暴露不允许修改的数据--只读属性（静态或非静态），功能与常量有重叠
//想用常量自定义类型时--静态只读字段
namespace _8字段_属性_索引器_常量
{
    class Program
    {
        static void Main(string[] args)
        {
            //Three();

           
        }
    
        //字段详解
        static void One()
        {
            
            Student student1 = new Student(40,90);
            Student student2 = new Student(22,60);

            Student.ReportAmount();
            Student.ReportAverageAge();
            Student.ReportAverageScore();
        }
        //属性详解
        static void Two()
        {
            SSstudent sSstudent = new SSstudent();
            sSstudent.Age = 1;
            Console.WriteLine(sSstudent.Age);
            SSstudent.Amount = 100;
            Console.WriteLine(SSstudent.Amount);
            Console.WriteLine(sSstudent.MyProperty);
            Console.WriteLine(sSstudent.CanWork);
        }

        //索引器
        static void Three()
        {
            SSStudent sSStudent = new SSStudent();
            var math_score = sSStudent["math"];
            Console.WriteLine(math_score==null);

            sSStudent["physics"] = 90;
            Console.WriteLine(sSStudent["physics"]);
        }
        //常量详解
        static void Four()
        {
            //类型下的常量
            Console.WriteLine(WASPEC.WEBSITEURL);
        }

    }

    //索引器相关
    class SSStudent
    {
        private Dictionary<string, int> score_dic = new Dictionary<string, int>();
        //索引器的定义
        public int? this[string subject]
        {
            get
            {
                if (this.score_dic.ContainsKey(subject))
                    return score_dic[subject];
                return null;
            }
            set
            {
                if (value == null)
                {
                    throw new Exception("error");
                }
                //因为value可空，所以写value.Value
                if (this.score_dic.ContainsKey(subject))
                    score_dic[subject] = value.Value;
                else
                    score_dic.Add(subject, value.Value);
            }
        }
    }


    
   
    //常量相关
    class WASPEC
    {
        //附属于类，而非实例
        public const string WEBSITEURL = "http://www.shit.com";
    }

    //属性相关
    class SSstudent
    {
        //完整声明
        private int age;
        public int Age
        {
            //属性封装了私有字段age
            get { return this.age; }
            set
            {
                //value在此上下文中是一个关键字
                if (value >= 0 && value <= 120)
                    this.age = value;
                else
                    throw new Exception("Age value has error");
            }
        }

        //简略声明，不能详细写get和set
        //此为只读属性，类内外都不能访问
        public int MyProperty { get; }
        
        //非只读属性，只能在类内部更改此属性。
        public int MyProperty1 { get; private set; }
        
        //静态完整声明
        private static int amount;
        public static int Amount
        {
            get { return amount; }
            set 
            {
                if (value >= 0)
                    amount = value;
                else
                    throw new Exception("error");
            }
        }

        //动态计算值的属性
        public bool CanWork 
        {
            //属性并未封装一个字段
            //在外部获取此属性时，实时更新
            get
            {
                if (this.age >= 16)
                    return true;
                return false;
            }
        }
    }

    //字段相关
    class Student
    {
        //在声明时初始化等价于在实例构造函数中赋值
        //实例字段在每次创建对象时都会调用初始化器
        public int age = 0;
        public int score;
        //只读字段只能在实例化时用构造函数赋值，或者用初始化器赋初值
        public readonly int ID=0;

        ////在声明时初始化等价于在静态构造函数中赋值。
        //静态字段只会在类加载时调用一次初始化器
        public static double average_age;
        public static double average_score;
        public static double amount;

        //静态构造器
        //类加载时执行，只执行一次
        static Student()
        {

        }

        //实例构造器
        //实例化时执行，每次实例化都执行
        public Student(int age,int score)
        {
            amount++;
            this.score = score;
            this.age = age;
            average_score = (average_score * (amount-1) + score) / amount;
            average_age = (average_age * (amount - 1) + age) / amount;
        }

        public Student(int ID)
        {
            this.ID = ID;
        }
        public static void ReportAmount()
        {
            Console.WriteLine(amount);
        }

        public static void ReportAverageAge()
        {
            Console.WriteLine(average_age);

        }
        public static void ReportAverageScore()
        {
            Console.WriteLine(average_score);

        }
    }
}
