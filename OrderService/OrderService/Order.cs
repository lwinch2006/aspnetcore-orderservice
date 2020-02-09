using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderService
{
    public class Order
    {
        private readonly IList<OrderLine> _orderLines = new List<OrderLine>();

        public Order(string company)
        {
            Company = company;
        }

        public string Company { get; set; }

        public void AddLine(OrderLine orderLine)
        {
            _orderLines.Add(orderLine);
        }

        public string GenerateReceipt()
        {
            var totalAmount = 0d;
            var result = new StringBuilder($"Order receipt for '{Company}'{Environment.NewLine}");
            foreach (var line in _orderLines)
            {
                var thisAmount = 0d;
                switch (line.Product.Price)
                {
                    case Product.Prices.OneThousand:
                        if (line.Quantity >= 5)
                            thisAmount += line.Quantity * line.Product.Price * .9d;
                        else
                            thisAmount += line.Quantity * line.Product.Price;
                        break;
                    case Product.Prices.TwoThousand:
                        if (line.Quantity >= 3)
                            thisAmount += line.Quantity * line.Product.Price * .8d;
                        else
                            thisAmount += line.Quantity * line.Product.Price;
                        break;
                }

                result.AppendLine(
                    $"\t{line.Quantity} x {line.Product.ProductType} {line.Product.ProductName} = {thisAmount:C}");
                totalAmount += thisAmount;
            }

            result.AppendLine($"Subtotal: {totalAmount:C}");
            var totalTax = totalAmount * Product.Prices.TaxRate;
            result.AppendLine($"MVA: {totalTax:C}");
            result.Append($"Total: {totalAmount + totalTax:C}");
            return result.ToString();
        }

        public string GenerateHtmlReceipt()
        {
            var totalAmount = 0d;
            var result = new StringBuilder($"<html><body><h1>Order receipt for '{Company}'</h1>");
            if (_orderLines.Any())
            {
                result.Append("<ul>");
                foreach (var line in _orderLines)
                {
                    var thisAmount = 0d;
                    switch (line.Product.Price)
                    {
                        case Product.Prices.OneThousand:
                            if (line.Quantity >= 5)
                                thisAmount += line.Quantity * line.Product.Price * .9d;
                            else
                                thisAmount += line.Quantity * line.Product.Price;
                            break;
                        case Product.Prices.TwoThousand:
                            if (line.Quantity >= 3)
                                thisAmount += line.Quantity * line.Product.Price * .8d;
                            else
                                thisAmount += line.Quantity * line.Product.Price;
                            break;
                    }

                    result.Append(
                        $"<li>{line.Quantity} x {line.Product.ProductType} {line.Product.ProductName} = {thisAmount:C}</li>");
                    totalAmount += thisAmount;
                }

                result.Append("</ul>");
            }

            result.Append($"<h3>Subtotal: {totalAmount:C}</h3>");
            var totalTax = totalAmount * Product.Prices.TaxRate;
            result.Append($"<h3>MVA: {totalTax:C}</h3>");
            result.Append($"<h2>Total: {totalAmount + totalTax:C}</h2>");
            result.Append("</body></html>");
            return result.ToString();
        }
    }
}