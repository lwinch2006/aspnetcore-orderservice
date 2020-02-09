using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace OrderService
{
    public class Order
    {
        private readonly IList<OrderLine> _orderLines = new List<OrderLine>();

        private readonly DiscountSystem _discountSystem = new DiscountSystem();
        
        public string Company { get; set; }

        public IList<OrderLine> OrderLines
        {
            get { return _orderLines; }
        }
        
        public Order(string company)
        {
            _discountSystem.AddDiscountRule(
                new LogicalRule { OperationType = OperationTypes.Equal, ParameterToCompareWith = Product.Prices.OneThousand},
                new LogicalRule { OperationType = OperationTypes.GreaterThen, ParameterToCompareWith = 5}, 
                .9d);
            
            _discountSystem.AddDiscountRule(
                new LogicalRule { OperationType = OperationTypes.Equal, ParameterToCompareWith = Product.Prices.TwoThousand},
                new LogicalRule { OperationType = OperationTypes.GreaterThen, ParameterToCompareWith = 3}, 
                .8d);
            
            Company = company;
        }
        
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
                var thisAmount = line.Quantity * line.Product.Price * _discountSystem.CalculateDiscount(line.Product.Price, line.Quantity);

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
                    var thisAmount = line.Quantity * line.Product.Price * _discountSystem.CalculateDiscount(line.Product.Price, line.Quantity);

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

        public string GenerateJsonReceiptAlternative1()
        {
            return JsonSerializer.Serialize(this);
        }

        public string GenerateJsonReceiptAlternative2()
        {
            var receipt = new Receipt
            {
                Header = $"Order receipt for '{Company}'",
                Specification = new List<string>()
            };
            
            var totalAmount = 0d;
            foreach (var line in _orderLines)
            {
                var thisAmount = line.Quantity * line.Product.Price * _discountSystem.CalculateDiscount(line.Product.Price, line.Quantity);

                receipt.Specification.Add($"{line.Quantity} x {line.Product.ProductType} {line.Product.ProductName} = {thisAmount:C}");
                totalAmount += thisAmount;
            }

            var totalTax = totalAmount * Product.Prices.TaxRate;
            
            receipt.Subtotal = $"{totalAmount:C}";
            receipt.MVA = $"{totalTax:C}";
            receipt.Total = $"{totalAmount + totalTax:C}";

            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            
            return JsonSerializer.Serialize(receipt, options);
        }
    }
}