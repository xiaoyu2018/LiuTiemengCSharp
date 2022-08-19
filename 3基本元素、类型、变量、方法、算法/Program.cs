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

    //二叉树
    class Btree
    {
        public int value { get; set; }
        public Btree lc { get; set; }
        public Btree rc { get; set; }

        public Btree(int num)
        {
            value = num;
        }
    }

    abstract class BTreeWithOO
    {
        public string value { get; set; }

        public BTreeWithOO(string v)
        {
            value = v;
        }

        public abstract void Add(BTreeWithOO bTreeWithOO);

        public abstract void Remove(BTreeWithOO bTreeWithOO);

        public abstract void Show();
    }

    class ConcreateTree : BTreeWithOO
    {

        BTreeWithOO lc;
        BTreeWithOO rc;


        public ConcreateTree(string v):base(v)
        {

        }
        public override void Add(BTreeWithOO bTreeWithOO)
        {
            if (lc == null)
                lc = bTreeWithOO;
            else if (rc == null)
                rc = bTreeWithOO;
            else
                Console.WriteLine("this Node has already have two children!");
                
        }

        public override void Remove(BTreeWithOO bTreeWithOO)
        {
            throw new NotImplementedException();
        }

        public override void Show()
        {
            Console.WriteLine($"{value} ");
            lc.Show();
            rc.Show();
        }
    }

    class Leave : BTreeWithOO
    {
        public Leave(string v):base(v)
        {

        }
        public override void Add(BTreeWithOO bTreeWithOO)
        {
            Console.WriteLine("you cannot add a node to one leave!");
        }

        public override void Remove(BTreeWithOO bTreeWithOO)
        {
            throw new NotImplementedException();
        }

        public override void Show()
        {
            Console.WriteLine($"{value} ");
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
            //DiGui(10);

            //Btree bt = CreateBTree();

            //PreOrder(bt);

            //数据类型();

            //TestOOTree();

            int[] a = { 5, 5, 6, 4, 3, 2, 8, 1, 9 };

            MySort.QuickSort(a, 0, a.Length-1);

            foreach (var item in a)
            {
                Console.Write(item.ToString()+" ");
            }
        }

        static void TestOOTree()
        {
            ConcreateTree root = new ConcreateTree("a");
            ConcreateTree branch = new ConcreateTree("b");
            Leave l1 = new Leave("c");
            Leave l2 = new Leave("d");
            Leave l3 = new Leave("e");

            root.Add(branch);
            root.Add(l3);

            branch.Add(l1);
            branch.Add(l2);

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
            var y = 3.0F;
            var z = 3.0;
            Console.WriteLine($"{x.GetType()}{y.GetType()}{z.GetType()}");
        }
        static void 字面值()
        {
            int x = 2;
            long y = 3L;
            //此处F不可省略，3.0默认是双精度浮点数
            float xx = 3.0F;
            double yy = 3.0;
            char c = 'c';
            String s = "sss";
            bool b = true;
            String str = null;
            Console.WriteLine(str);
        }

        static void DiGui(int x)
        {
            if (x < 1)
                return;
            DiGui(x - 1);
            Console.WriteLine(x);
        }

        //创建二叉树
        static Btree CreateBTree()
        {
            string x=Console.ReadLine();
            int num = int.Parse(x);

            if (num!=-1)
            {
                Btree btree = new Btree(num);
                btree.lc = CreateBTree();
                btree.rc = CreateBTree();

                return btree;
            }

            return null;
        }

        static void PreOrder(Btree bt)
        {
            if(bt!=null)
            {
                Console.Write(bt.value.ToString()+" ");
                PreOrder(bt.lc);
                PreOrder(bt.rc);

            }
        }
    }
}
