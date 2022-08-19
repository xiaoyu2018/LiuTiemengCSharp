using System;
using System.Collections.Generic;
using System.Text;

namespace 插件2
{
    class Chick
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
