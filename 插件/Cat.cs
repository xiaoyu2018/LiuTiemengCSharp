using System;


//插件式编程
//此程序编译的dll会在14中的主体程序中当作插件
namespace 插件
{
    public class Cat:
    {
        public void Voice(int times)
        {
            for (int i = 0; i < times; i++)
            {
                Console.WriteLine("Meow!");
            }
        }
    }
}
