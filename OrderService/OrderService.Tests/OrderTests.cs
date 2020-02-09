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
                $"<html><body><h1>Order receipt for 'Test Company'</h1><ul><li>1 x Car Insurance Basic = kr 1{NumberFormatInfo.CurrentInfo.NumberGroupSeparator}000,00</li></ul><h3>Subtotal: kr 1{NumberFormatInfo.CurrentInfo.NumberGroupSeparator}000,00</h3><h3>MVA: kr 250,00</h3><h2>Total: kr 1{NumberFormatInfo.CurrentInfo.NumberGroupSeparator}250,00</h2></body></html>";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void can_generate_html_receipt_for_motor_super()
        {
            var order = new Order("Test Company");
            order.AddLine(new OrderLine(MotorSuper, 1));
            var actual = order.GenerateHtmlReceipt();

            var expected =
                $"<html><body><h1>Order receipt for 'Test Company'</h1><ul><li>1 x Car Insurance Super = kr 2{NumberFormatInfo.CurrentInfo.NumberGroupSeparator}000,00</li></ul><h3>Subtotal: kr 2{NumberFormatInfo.CurrentInfo.NumberGroupSeparator}000,00</h3><h3>MVA: kr 500,00</h3><h2>Total: kr 2{NumberFormatInfo.CurrentInfo.NumberGroupSeparator}500,00</h2></body></html>";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void can_generate_receipt_for_motor_basic()
        {
            var order = new Order("Test Company");
            order.AddLine(new OrderLine(MotorBasic, 1));
            var actual = order.GenerateReceipt();
            var expected =
                $"Order receipt for 'Test Company'\r\n\t1 x Car Insurance Basic = kr 1{NumberFormatInfo.CurrentInfo.NumberGroupSeparator}000,00\r\nSubtotal: kr 1{NumberFormatInfo.CurrentInfo.NumberGroupSeparator}000,00\r\nMVA: kr 250,00\r\nTotal: kr 1{NumberFormatInfo.CurrentInfo.NumberGroupSeparator}250,00";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void can_generate_receipt_for_motor_super()
        {
            var order = new Order("Test Company");
            order.AddLine(new OrderLine(MotorSuper, 1));
            var actual = order.GenerateReceipt();
            var expected =
                $"Order receipt for 'Test Company'\r\n\t1 x Car Insurance Super = kr 2{NumberFormatInfo.CurrentInfo.NumberGroupSeparator}000,00\r\nSubtotal: kr 2{NumberFormatInfo.CurrentInfo.NumberGroupSeparator}000,00\r\nMVA: kr 500,00\r\nTotal: kr 2{NumberFormatInfo.CurrentInfo.NumberGroupSeparator}500,00";

            Assert.AreEqual(expected, actual);
        }
    }
}