using Xunit;
using Bangazon.Orders;
using System;
using System.Collections.Generic;

namespace Bangazon.Tests
{
    public class OrderTests
    {
        [Fact]
        public void TestTheTestingFramework()
        {
            Assert.True(true);
        }

        [Fact]
        public void OrdersCanExist()
        {
            Order ord = new Order();
            Assert.NotNull(ord);
        }

        [Fact]
        public void NewOrdersHaveAGuid()
        {
            Order ord = new Order();
            Assert.NotNull(ord.orderNumber);
            Assert.IsType<Guid>(ord.orderNumber);
        }

        [Fact]
        public void NewOrdersShouldHaveAnEmptyProductList()
        {
            Order ord = new Order();
            Assert.NotNull(ord.products);
            Assert.IsType<List<string>>(ord.products);
            Assert.Empty(ord.products);
        }

        [Theory]
        [InlineDataAttribute("Banana")]
        [InlineDataAttribute("32434324")]
        [InlineDataAttribute("A product with spaces")]
        [InlineDataAttribute("Product, that has, a comma?")]
        public void OrdersCanHaveProductsAddedToThem(string product)
        {
            Order ord = new Order();
            ord.addProduct(product);
            Assert.Equal(1, ord.products.Count);
            Assert.Contains<string>(product, ord.products);
        }

        [Theory]
        [InlineDataAttribute("product")]
        [InlineDataAttribute("product,another product")]
        [InlineDataAttribute("a first product,someother,yet another")]
        [InlineDataAttribute("prod 1,prod 2,prod 3,prod 4")]
        public void OrdersCanHaveMultipleProductsAddedToThem(string productsStr)
        {
         string[] products = productsStr.Split(new char[] { ',' });
        
         Order ord = new Order();

         foreach (string product in products)
         {
             ord.addProduct(product);
         }
         Assert.Equal(products.Length, ord.products.Count);

         foreach (string product in products)
         {
             Assert.Contains<string>(product, ord.products);
         }
        }
        
        [Theory]
        [InlineDataAttribute("product")]
        [InlineDataAttribute("product,another product")]
        [InlineDataAttribute("a first product,someother,yet another")]
        [InlineDataAttribute("prod 1,prod 2,prod 3,prod 4")]
        public void OrdersCanListProductsForTerminalDisplay(string productsStr)
        {
         string[] products = productsStr.Split(new char[] { ',' });
        
         Order ord = new Order();

         foreach (string product in products)
         {
             ord.addProduct(product);
         }

         foreach (string product in products)
         {
             Assert.Contains($"\nYou ordered {product}", ord.listProducts());
         }
        }

        [Fact]
        public void OrdersCanHaveAProductRemovedFromThem()
        {
            Order ord = new Order();
            ord.addProduct("Product1");
            ord.addProduct("Product2");
            ord.addProduct("Product3");

            ord.removeProduct("Product1");

            Assert.Equal(2, ord.products.Count);
            Assert.DoesNotContain<string>("Product1", ord.products);
        }

        [Fact]
        public void OrdersCanNotRemoveAProductThatDoesNotExist()
        {
            Order ord = new Order();
            ord.addProduct("Product1");
            ord.addProduct("Product2");
            ord.addProduct("Product3");

            ord.removeProduct("Product5");

            Assert.Equal(3, ord.products.Count);
        }

        [Theory]
        [InlineDataAttribute("Product1")]
        [InlineDataAttribute("Product5")]
        public void RemoveMethodReturnsBooleanIndicatingIfProductWasRemoved(string product)
        {
            Order ord = new Order();
            ord.addProduct("Product1");
            ord.addProduct("Product2");
            ord.addProduct("Product3");

            bool removed = ord.removeProduct(product);

            if(product == "Product1")
            {
                Assert.True(removed);
            }
            if(product == "Product5")
            {
                Assert.False(removed);
            }
        }

        [Fact]
        public void AllProductsFromAnOrderCanBeDeleted()
        {
            Order ord = new Order();
            ord.addProduct("Product1");
            ord.addProduct("Product2");
            ord.addProduct("Product3");

            ord.removeProduct();

            Assert.Empty(ord.products);
        }
    }
}