using System;
using System.Collections.Generic;
using System.Text;

namespace _10委托
{

    interface ILog
    {
        void Log(Product product);
    }

    interface IGetProduct
    {
        Product GetProduct();
    }

    class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }
    class Box
    {
        public Product Product { get; set; }
    }

    //模板方法及回调方法相关
    class Logger:ILog
    {
        public void Log(Product product)
        {
            //低于5价格的产品不会被记账
            if (product.Price < 5)
                return;
            Console.WriteLine($"Product {product.Name} craeted at {DateTime.UtcNow} is ${product.Price}");
        }
    }

    class PizzaFac : IGetProduct
    {
        public Product GetProduct()
        {
            return new Product() { Name = "Pizza", Price = 10 };
        }
    }

    class BoxFac
    {
        public Box WrapWithLog(IGetProduct product,ILog log)
        {
            Box box = new Box();
            Product pro = product.GetProduct();
            box.Product = pro;

            log.Log(pro);
            return box;
        }
    }
}
