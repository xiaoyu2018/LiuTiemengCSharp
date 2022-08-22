using System;
using System.Collections.Generic;
using System.Text;

namespace _11事件
{

    class TestArgs:EventArgs
    {
        public int Price { get; set; }
        public string Name { get; set; }
    }

    class TCustomer
    {
        private TWaiter waiter=new TWaiter();

        public TCustomer()
        {
            test += waiter.ReAction;
        }

        public event EventHandler<TestArgs> test;
        

        private void OnTest(string name,int price)
        {
            if(test!=null)
            {
                test(this, new TestArgs() { Name=name,Price=price});
            }

        }

        public void Activate()
        {
            OnTest("bread", 3);
        }
    }


    class TWaiter
    {
        internal void ReAction(object sender, TestArgs e)
        {
            if(sender is TCustomer)
            {
                Console.WriteLine($"{e.Name},{e.Price}");
            }
        }
    }
}
