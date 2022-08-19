using System;
using System.Collections.Generic;
using System.Text;

//规定了插件开发商开发守则，相当于一个契约
//提供给此项目的dll文件给插件开发商即可
namespace Baby_Stroller.SDK
{
    public interface IAnimals
    {
        void Voice(int times);
    }
}
