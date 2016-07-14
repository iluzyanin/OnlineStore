using NUnit.Framework;
using OnlineStore.Core;

namespace OnlineStore.Tests
{
    public class ShoppingCartTests
    {
        [TestFixture]
        public class GetTotalAmountMethod
        {
            private ShoppingCart shoppingCart;

            [SetUp]
            public void SetUp()
            {
                shoppingCart = new ShoppingCart();
            }

            [Test]
            public void When_no_items_Should_return_zero()
            {
                // Act
                decimal result = shoppingCart.CalculateTotalAmount();

                // Assert
                Assert.AreEqual(result, 0m);
            }

            [Test]
            public void When_some_items_And_no_discount_Should_sum_items_prices()
            {
                // Arrange
                shoppingCart.Items.Add(new Item { Id = 1, Name = "Apple", Price = 3 });
                shoppingCart.Items.Add(new Item { Id = 2, Name = "Orange", Price = 2.8m });
                shoppingCart.Items.Add(new Item { Id = 3, Name = "Banana", Price = 1.4m });

                // Act
                decimal result = shoppingCart.CalculateTotalAmount();

                // Assert
                Assert.AreEqual(result, 7.2m);
            }

            [Test]
            public void When_items_sum_more_than_discount_Should_return_items_minus_discount()
            {
                // Arrange
                shoppingCart.Items.Add(new Item { Id = 1, Name = "Apple", Price = 3 });
                shoppingCart.Items.Add(new Item { Id = 2, Name = "Orange", Price = 2.8m });
                shoppingCart.Items.Add(new Item { Id = 3, Name = "Banana", Price = 1.4m });
                shoppingCart.Discount = 1.5m;

                // Act
                decimal result = shoppingCart.CalculateTotalAmount();

                // Assert
                Assert.AreEqual(result, 5.7m);
            }

            [Test]
            public void When_items_sum_less_than_discount_Should__return_zero()
            {
                // Arrange
                shoppingCart.Items.Add(new Item { Id = 3, Name = "Banana", Price = 1.4m });
                shoppingCart.Discount = 1.5m;

                // Act
                decimal result = shoppingCart.CalculateTotalAmount();

                // Assert
                Assert.AreEqual(result, 0);
            }
        }
    }
}