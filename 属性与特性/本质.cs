using System;

//.NET平台的所有语言，其编译结果都是通用的，如c#编译的dll文件交给VB.NET可以直接使用
//通过查看C#中间代码，发现Attribute并不是修饰符，而是一个有着独特实例化形式的类！
//Attribute的实例一构造出来就必需“粘”在一个什么目标上,不使用new操作符来产生实例，而是使用在方括号里调用构造函数的来产生实例。
//构造函数的参数是一定要写的——有几个就得写几个——因为你不写的话实例就无法构造出来。

namespace OysterAttributeSample
{
    //构建自定义特性时，需继承自System.Attribute
    //此特性指定该自定义特性能够附着在什么上
    //不写默认为ALL
    //特性附着在类上时是否随着继承关系也附着在派生类上
    //同一个attribute的多个实例是否能附着在同一个目标上
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method | AttributeTargets.Field,
        Inherited =false,AllowMultiple =true)]
    class Oyster :Attribute
    {
        private string kind;

        public string Kind
        {
            get { return kind; }
            set { kind = value; }
        }

        private uint age;

        public uint Age
        {
            get { return age; }
            set { age = value; }
        }

        public Oyster(string arg)
        {
            kind = arg;
        }

    }
    //构造函数的值必须都要传入
    //而属性则可像具名参数一般选择性传入（不传也有默认值）
    //牡蛎附着在船上
    [Oyster("Thorny",Age =3)]
    class Ship
    {
        //牡蛎附着在船舵上，没传Age则为默认的0
        [Oyster("Saddle")]
        public string Rudder;
    }
}
