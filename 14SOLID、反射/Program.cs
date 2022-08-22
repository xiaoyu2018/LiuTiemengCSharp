using System;
using System.Reflection;
using System.IO;

//依赖注入包
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Runtime.Loader;
using System.Linq;
using Baby_Stroller.SDK;

/*学习内容
*1 接口隔离原则
*2 反射
*/

//1 接口隔离原则
//接口：甲方不要多要，乙方不要少给
//接口隔离原则就是说甲方别多要，如果一个接口的某个方法一直都没有被用过，就说明甲方要多了
//把本质不同的功能隔离开，一个接口只放他该放的功能

//2 反射
//反射是.NET 的功能
//单元测试、依赖注入、泛型编程都是基于反射机制
//只需使用反射，并不需要了解底层细节，.NET封装的很好
//反射是程序以不变应万变的能力，动态地在程序运行时从内存中拿到对类型的描述，再去创建一个对象。也因此，反射对程序的性能是有影响的
//反射会得到此变量在运行时内存中动态的类型描述信息，按照拿到的动态类型信息创建一个对象。
//反射可以实现依赖注入和插件式编程

namespace _14SOLID_反射
{
    class Program
    {
        static void Main(string[] args)
        {
            Four();
        }

        //接口隔离
        static void One()
        {
            Driver driver1 = new Driver(new Car());
            Driver driver2 = new Driver(new LightTank());
            driver1.Drive();
            driver2.Drive();
            //注意driver无法调用Fire()方法

            //显式和普通实现接口
            //wk无法调用kill方法
            var wk = new WarmKiller();
            wk.Love();
            IKiller killer = wk;
            killer.Kill();

            WarmKiller wkk = killer as WarmKiller;
            wkk.Love();
        }
        //反射：原理和依赖注入
        static void Two()
        {
            IVehicle vehicle = new Truck();
            //================接下来是真正的技术====================
            //会得到此变量在运行时内存中动态的类型描述信息
            var t = vehicle.GetType();
            //按照拿到的动态类型信息创建一个对象，我们并不知道他的静态类型是什么，所以就用Object类型的引用变量准没错
            object o = Activator.CreateInstance(t);
            //我们知道他肯定有Run方法
            MethodInfo runMI = t.GetMethod("Run");
            //在对象o上调用Run()方法，第二个参数是Run()方法需要接收的参数。没参数用null
            runMI.Invoke(o, null);

            //以下是微软封装好的反射，才是更常用的，上面的是原理
            //依赖注入
            //需要使用NuGet安装微软官方依赖注入包
            //ServiceClient是一个容器，放了各种各样的类型信息
            var ServiceClient = new ServiceCollection();
            //参数为服务类型和实现类型
            ServiceClient.AddScoped(typeof(IVehicle),typeof(Car));
            var ServiceProvider = ServiceClient.BuildServiceProvider();
            //===============以上为程序的一次性注册，以下为使用==============
            //上面注册了Ivehicle的服务，这里便可以使用
            IVehicle v = ServiceProvider.GetService<IVehicle>();
            v.Run();
            //依赖注入还有一个好处，便是改一个注册，全部使用就都改过来了
        }

        //反射：简单插件式编程（不变和万变）
        //这是主体程序
        static void Three()
        {
            //主体程序中，加载各类插件，拿到插件在内存中运行的各种方法名等
            //首先获取插件文件夹
            var folder = Path.Combine(Environment.CurrentDirectory, "Animals");
            //把文件夹下所有dll插件读取到进来
            var files = Directory.GetFiles(folder);
            var animalTypes = new List<Type>();
            foreach (var file in files)
            {
                //加载dll文件
                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file);
                //获取文件中的数据类型信息
                var types = assembly.GetTypes();
                //如果这个插件有Voice这个方法，就加入动物类型列表
                //Voice这个方法写在了插件开发手册。
                foreach (var t in types)
                {
                    if (t.GetMethod("Voice") != null)
                        animalTypes.Add(t);
                }
            }

            while(true)
            {
                Console.WriteLine("=================本系统提供以下几种小动物===============");
                for (int i = 0; i < animalTypes.Count; i++)
                {
                    Console.WriteLine($"{i+1} {animalTypes[i].Name}");
                }

                Console.WriteLine("=================请选择要模拟的小动物的索引=================");
        
                try
                {
                    int index = int.Parse(Console.ReadLine());
                    Console.WriteLine("=================请输入叫声次数=================");
                    int times = int.Parse(Console.ReadLine());

                    var t = animalTypes[index - 1];
                    object o = Activator.CreateInstance(t);
                    MethodInfo VMI = t.GetMethod("Voice");
                    //参数要以对象数组的形式传入
                    VMI.Invoke(o, new object[] { times});
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message+" 请重新输入");
                    continue;
                }
            }

            //以上仅仅是最简单的插件式编程
        }
        //反射：现代插件式编程（不变和万变）
        static void Four()
        {
            //为了方便插件开发人员，避免命名等开发问题，可使用接口作为契约
            //插件开发人员只需实现接口，就不会出现问题
            //主体程序商提供包含的SDK，插件开发商在开发插件时引用SDK即可！

            //主体厂商直接引用BabyStroller.SDK项目即可
            //插件厂商引用响应dll文件，因为肯定不希望插件厂商更改SDK内容

            //引用dll文件的项目会显示在依赖项的assemblies中
            //引用项目文件的的项目会显示在依赖项中的projects中

            //主体程序中，加载各类插件，拿到插件在内存中运行的各种方法名等
            //首先获取插件文件夹
            var folder = Path.Combine(Environment.CurrentDirectory, "Animals");
            //把文件夹下所有dll插件读取到进来
            var files = Directory.GetFiles(folder);
            var animalTypes = new List<Type>();
            foreach (var file in files)
            {
                //加载dll文件
                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file);
                //获取文件中的数据类型信息
                var types = assembly.GetTypes();
                //因为有SDK，所以dll文件中一定有Voice方法
                
                foreach (var t in types)
                {
                    if (t.GetInterfaces().Contains(typeof(IAnimals)))
                        animalTypes.Add(t);
                }
            }

            while (true)
            {
                Console.WriteLine("=================本系统提供以下几种小动物===============");
                for (int i = 0; i < animalTypes.Count; i++)
                {
                    Console.WriteLine($"{i + 1} {animalTypes[i].Name}");
                }

                Console.WriteLine("=================请选择要模拟的小动物的索引=================");

                try
                {
                    int index = int.Parse(Console.ReadLine());
                    Console.WriteLine("=================请输入叫声次数=================");
                    int times = int.Parse(Console.ReadLine());

                    var t = animalTypes[index - 1];
                    object o = Activator.CreateInstance(t);
                    MethodInfo VMI = t.GetMethod("Voice");

                    //此处可以直接类型转换,因为有了接口规范
                    IAnimals animal = o as IAnimals;
                    animal.Voice(times);
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + " 请重新输入");
                    continue;
                }
            }
        }
    }

    //接口隔离
    //一般开车的不敢发坦克大炮！
    //能开炮可以想想再写一个TankDriver类，其继承Driver
    class Driver
    {
        private IVehicle _vehicle;
        public Driver(IVehicle vehicle)
        {
            _vehicle = vehicle;
        }

        public void Drive()
        {
            _vehicle.Run();
        }
    }

    interface IVehicle
    {
        void Run();
        
    }

    class Car:IVehicle
    {
        public void Run()
        {
            Console.WriteLine("Car is running");
        }
    }

    class Truck : IVehicle
    {
        public void Run()
        {
            Console.WriteLine("Truck is running");
        }
    }

    //如果你写一个ITank接口，包括Fire()和Run()，便违反了接口隔离原则
    interface IWeapon
    {
        void Fire();
    }

    class LightTank : IVehicle, IWeapon
    {
        public void Fire()
        {
            Console.WriteLine("lightly Fire!");
        }

        public void Run()
        {
            Console.WriteLine("Light tank is running!");
        }
    }
    class GiantTank : IVehicle, IWeapon
    {
        public void Fire()
        {
            Console.WriteLine("heavily Fire!");
        }

        public void Run()
        {
            Console.WriteLine("Giant tank is running!");
        }
    }

    //显式和隐式实现接口
    interface IGentleMan
    {
        void Love();

    }
    interface IKiller
    {
        void Kill();
    }

    class WarmKiller : IGentleMan,IKiller
    {
       
        //普通实现接口
        public void Love()
        {
            Console.WriteLine("I love u");
        }
        //显式实现接口
        //只有IKiller类型引用该实例时，才能调用此方法
        void IKiller.Kill()
        {
            Console.WriteLine("Killing spread");
        }
    }

    //反射
    //同样用了Driver类那些东西

}
