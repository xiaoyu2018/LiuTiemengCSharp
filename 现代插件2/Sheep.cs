using System;
using Baby_Stroller.SDK;

namespace 现代插件2
{
    public class Sheep:IAnimals
    {
        public void Voice(int times)
        {
            for (int i = 0; i < times; i++)
            {
                Console.WriteLine("Ba Ba!");
            }
        }
    }
}
