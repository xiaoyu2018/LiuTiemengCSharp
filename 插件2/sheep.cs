using System;

namespace 插件2
{
    public class Sheep
    {
        public void Voice(int times)
        {
            for (int i = 0; i < times; i++)
            {
                Console.WriteLine("Baa!");
            }
        }
    }
}
