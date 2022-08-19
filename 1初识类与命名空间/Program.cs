using System;//将名称空间引入到本项目中


//光标放在某个类或方法等上，按F1获取文档。
/*学习内容：
 * 1 初识类和命名空间
 * 2 类库的引用
 * 3 依赖关系
 */

//1 初识类和命名空间
//类是构成程序的主体
//命名空间以树形结构组织类
//vs建立项目时自动生成名称空间，与项目名相同，项目下文件夹会自动当成一级名称空间
//在不知道一个类所属的命名空间时，可以使用vs自己提供的修补程序。
//注意C#语言中不同角色的标志不同，如命名空间是花括号。
//当using多个命名空间，且使用了空间中名称重复的类时，vs会提示出错，此时应在使用类前加上命名空间名字。

//2 类库的引用
//除了在同项目中使用外，类和命名空间（像System这样的）还可以放在类库中。
//引用类库后便可使用类库中相应命名空间
//类库引用有两种方法：黑盒引用、白盒引用
//黑盒引用：dll文件，看不见源码，所以没有文档基本用不了。可以通过MsDocs查找文档引入想使用的dll
//白盒引用：解决方案下的其他项目，能看见源码
//两种引用方式在vs中都从依赖项中添加。
//NuGet就像pip一样，要多多利用！
//创建类库时，在vs创建新项目，选类库，编译出来的就是dll

//3 依赖关系
//高性能的程序追求 高内聚，低耦合
//高内聚：类的功能明确
//低耦合：类之间依赖尽可能的弱

namespace _1初识类与命名空间
{
    class Program
    {
        static void Main(string[] args)
        {
            //using了命名空间System
            Console.WriteLine("hello world!");

            //直接使用其他命名空间下的类和方法
            AnotherSpace.GoodBye.Print();
            
            //相同项目另一文件下
            AnotherNS.Class1.Print();
            
            //不同项目，白盒引用
            ClassLibrary1.Class1.Print();
            
        }
    }
}

//命名空间在被using时直接使用内部类和方法
//命名空间是树形结构，允许套娃。
namespace AnotherSpace
{
    class GoodBye
    {
        static public void Print()
        {
            Console.WriteLine("Goodbye World!");
            new Internal.InternalClass().print();
        }
    }

    namespace Internal
    {
        class InternalClass
        {
            public void print()
            {
                Console.WriteLine("you caught me!");
            }
        }
    }
}
