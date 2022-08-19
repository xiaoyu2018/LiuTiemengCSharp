using System;



/*学习内容
 *1 构成C#语言的基本元素
 *2 类型、变量和方法
 *3 算法简介
 */

//1 构成C#语言的基本元素
//（1）关键字
//关键字是预定义的保留标识符，对编译器有特殊意义。 除非前面有 @ 前缀，否则不能在程序中用作标识符。 例如，@if 是有效标识符，而 if 则不是，因为 if 是关键字。
//
//（2）操作符
//用于数值计算和逻辑计算
//（3）标识符
//为变量、类、类的成员等取的名字
//取名不仅要合法且要遵守规范
//合法：不与关键字冲突，使用数字、字母、下划线且不能以数字开头
//规范：要保证可读性，符合通用规范，首字母要大写。
//（4）标点符号
//";"、"{"等是符号但不参与运算。
//（5）文本（字面值）
//整数、实数、字符、布尔、空（null）
//（6）注释与空白
//ctrl+k,c注释选中
//ctrl+k,u解注释选中
//2 数据类型、变量和方法
//变量用于存放数据
//方法即成员函数


namespace _3基本元素_类型_变量_方法_算法
{

    //快速排序
    class MySort
    {
        static public void QuickSort(int[] array,int front,int rear)
        {
            if (front >= rear)
                return;
            int p = front;
            int q = rear;
            int temp = array[p];
            while (p<q)
            {

                for (; array[q] > temp && p < q; --q) ;
                if(p < q)
                {
                    array[p++] = array[q];
                }

                for (; array[p] < temp && p < q; ++p) ;
                if (p < q)
                {
                    array[q--] = array[p];
                }
            }
            array[p] = temp;
            QuickSort(array, front, q - 1);
            QuickSort(array, p+1, rear);
        }
    }

    //二叉树节点
    abstract class BaseNode
    {
        public string value { get; set; }
        public BaseNode lc { get; set; }
        public BaseNode rc { get; set; }

        public BaseNode(string v)
        {
            value = v;
        }
        public BaseNode()
        {
            value="";
            lc=rc=null;
        }

        public abstract void AddNode(BaseNode node);
        public void RemoveVal(string val)
        {
            if (value == val)
                value = "";
        }
        public abstract void Show();

    }


    class InternalNode : BaseNode
    {


        public InternalNode(string v):base(v)
        {

        }
        public override void AddNode(BaseNode node)
        {
            if (lc == null)
                lc = node;
            else if (rc == null)
                rc = node;
            else
                Console.WriteLine("this Node has already have two children!");
                
        }


        public override void Show()
        {
            Console.Write($"{value} ");
            lc.Show();
            rc.Show();
        }
    }

    class Leave : BaseNode
    {
        public Leave(string v):base(v)
        {

        }
        public override void AddNode(BaseNode node)
        {
            Console.WriteLine("you cannot add a node to one leave!");
        }

        //叶子节点不需输出子节点
        public override void Show()
        {
            Console.Write($"{value} ");
        }
    }
    class Program
    {


        static void Main(string[] args)
        {
            //字面值();
            //数据类型();
            /*int a = 1;
            int b = 2;
            Add(ref a, b);
            Console.WriteLine(a);*/

            //Console.WriteLine(GetToday());
            //Console.WriteLine(Fibo(5));

            //Btree bt = CreateBTree();

            //PreOrder(bt);

            //TestBTree();

            int[] a = { 5, 5, 6, 4, 3, 2, 8, 1, 9 };

            MySort.QuickSort(a, 0, a.Length - 1);

            foreach (var item in a)
            {
                Console.Write(item.ToString() + " ");
            }
        }

        static void TestBTree()
        {
            BaseNode root = new InternalNode("a");
            BaseNode branch = new InternalNode("b");
            Leave l1 = new Leave("c");
            Leave l2 = new Leave("d");
            Leave l3 = new Leave("e");

            root.AddNode(branch);
            root.AddNode(l3);

            branch.AddNode(l1);
            branch.AddNode(l2);

            root.Show();
        }

        static String GetToday()
        {
            int day = DateTime.Now.Day;
            return day.ToString();
        }
        static void Add(ref int a,int b)
        {
            Console.WriteLine(a+b);
            a++;
            Console.WriteLine(a);
        }
        static void 数据类型()
        {
            var x = 3;
            var y = 3.0f;
            //默认是双精度
            var z = 3.0;
            Console.WriteLine($"{x.GetType()}{y.GetType()}{z.GetType()}");
        }
        static void 字面值()
        {
            int x = 2;
            long y = 3l;
            //此处f不可省略，3.0默认是双精度浮点数
            float xx = 3.0f;
            double yy = 3.0;
            char c = 'c';
            String s = "sss";
            bool b = true;
            String str = null;
            Console.WriteLine(str);
        }

        static int Fibo(int n)
        {
            if(n < 1)
                return 0;
            if (n == 1 || n == 2)
                return 1;
            return Fibo(n - 1) + Fibo(n - 2);
        }

    }
}
