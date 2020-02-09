using System;
using System.Globalization;
using NUnit.Framework;

namespace OrderService.Tests
{
    [TestFixture]
    public class OrderTests
    {
        private static readonly Product MotorSuper = new Product("Car Insurance", "Super", Product.Prices.TwoThousand);
        private static readonly Product MotorBasic = new Product("Car Insurance", "Basic", Product.Prices.OneThousand);

        [Test]
        public void can_generate_html_receipt_for_motor_basic()
        {
            var order = new Order("Test Company");
            order.AddLine(new OrderLine(MotorBasic, 1));
            var actual = order.GenerateHtmlReceipt();

            var expected =
                $"<html><body><h1>Order receipt for 'Test Company'</h1><ul><li>1 x Car Insurance Basic = {1000:C}</li></ul><h3>Subtotal: {1000:C}</h3><h3>MVA: {250:C}</h3><h2>Total: {1250:C}</h2></body></html>";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void can_generate_html_receipt_for_motor_super()
        {
            var order = new Order("Test Company");
            order.AddLine(new OrderLine(MotorSuper, 1));
            var actual = order.GenerateHtmlReceipt();

            var expected =
                $"<html><body><h1>Order receipt for 'Test Company'</h1><ul><li>1 x Car Insurance Super = {2000:C}</li></ul><h3>Subtotal: {2000:C}</h3><h3>MVA: {500:C}</h3><h2>Total: {2500:C}</h2></body></html>";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void can_generate_receipt_for_motor_basic()
        {
            var order = new Order("Test Company");
            order.AddLine(new OrderLine(MotorBasic, 1));
            var actual = order.GenerateReceipt();
            var expected =
                $"Order receipt for 'Test Company'{Environment.NewLine}\t1 x Car Insurance Basic = {1000:C}{Environment.NewLine}Subtotal: {1000:C}{Environment.NewLine}MVA: {250:C}{Environment.NewLine}Total: {1250:C}";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void can_generate_receipt_for_motor_super()
        {
            var order = new Order("Test Company");
            order.AddLine(new OrderLine(MotorSuper, 1));
            var actual = order.GenerateReceipt();
            var expected =
                $"Order receipt for 'Test Company'{Environment.NewLine}\t1 x Car Insurance Super = {2000:C}{Environment.NewLine}Subtotal: {2000:C}{Environment.NewLine}MVA: {500:C}{Environment.NewLine}Total: {2500:C}";

            Assert.AreEqual(expected, actual);
        }
    }
}