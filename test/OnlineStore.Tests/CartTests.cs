using NUnit.Framework;
using OnlineStore.Core.Models;

namespace OnlineStore.Tests
{
    public class CartTests
    {
        [TestFixture]
        public class GetTotalAmountMethod
        {
            private Cart cart;

            [SetUp]
            public void SetUp()
            {
                this.cart = new Cart();
            }

            [Test]
            public void When_no_items_And_Should_return_zero()
            {
                // Act
                decimal result = this.cart.CalculateTotalAmount();

                // Assert
                Assert.AreEqual(result, 0m);
            }

            [Test]
            public void When_some_items_and_no_discount_Should_sum_items_prices()
            {
                // Arrange
                this.cart.AddItem(TestItems.Apple, 1);
                this.cart.AddItem(TestItems.Orange, 1);
                this.cart.AddItem(TestItems.Banana, 1);

                // Act
                decimal result = this.cart.CalculateTotalAmount();

                // Assert
                Assert.AreEqual(result, 7.2m);
            }

            [Test]
            public void When_items_sum_more_than_discount_Should_return_items_sum_minus_discount()
            {
                // Arrange
                this.cart.AddItem(TestItems.Apple, 1);
                this.cart.AddItem(TestItems.Orange, 1);
                this.cart.AddItem(TestItems.Banana, 1);
                this.cart.Discount = 1.5m;

                // Act
                decimal result = this.cart.CalculateTotalAmount();

                // Assert
                Assert.AreEqual(result, 5.7m);
            }

            [Test]
            public void When_items_sum_less_than_discount_Should__return_zero()
            {
                // Arrange
                this.cart.AddItem(TestItems.Banana, 1);
                this.cart.Discount = 1.5m;

                // Act
                decimal result = this.cart.CalculateTotalAmount();

                // Assert
                Assert.AreEqual(result, 0);
            }
        }

        [TestFixture]
        public class AddItemMethod
        {
            private Cart cart;

            [SetUp]
            public void SetUp()
            {
                this.cart = new Cart();
            }

            [Test]
            public void When_item_is_null_Should_throw_exception()
            {
                // Assert
                Assert.That(() => this.cart.AddItem(null, 1), Throws.ArgumentNullException);
            }

            [Test]
            public void When_item_is_new_Should_add_it()
            {
                // Act
                this.cart.AddItem(TestItems.Apple, 1);

                // Assert
                Assert.AreEqual(this.cart.CartItems.Count, 1);
                Assert.AreEqual(this.cart.CartItems[0].Item.Id, TestItems.Apple.Id);
                Assert.AreEqual(this.cart.CartItems[0].Quantity, 1);
            }

            [Test]
            public void When_item_quantity_more_than_one_Should_add_all_items()
            {
                // Act
                this.cart.AddItem(TestItems.Apple, 2);
                
                // Assert
                Assert.AreEqual(this.cart.CartItems.Count, 1);
                Assert.AreEqual(this.cart.CartItems[0].Quantity, 2);
            }

            [Test]
            public void When_item_exists_Should_sum_quantity()
            {
                // Arrange
                this.cart.AddItem(TestItems.Apple, 1);
                
                // Act
                this.cart.AddItem(TestItems.Apple, 2);
                
                // Assert
                Assert.AreEqual(this.cart.CartItems.Count, 1);
                Assert.AreEqual(this.cart.CartItems[0].Quantity, 3);
            }

            [Test]
            public void When_item_quantity_zero_Should_not_add_it()
            {
                // Act
                this.cart.AddItem(TestItems.Apple, 0);
                
                // Assert
                Assert.AreEqual(this.cart.CartItems.Count, 0);
            }
        }
    }
}