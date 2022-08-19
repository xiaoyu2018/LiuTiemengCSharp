using System;
using Baby_Stroller.SDK;

namespace 现代插件2
{
    class Chicken:IAnimals
    {
        public void Voice(int times)
        {
            for (int i = 0; i < times; i++)
            {
                Console.WriteLine("Gu Gu!");
            }
        }
    }
}
