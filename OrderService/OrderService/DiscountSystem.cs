using System;
using System.Collections;
using System.Collections.Generic;

namespace OrderService
{
    public class DiscountSystem
    {
        private readonly ICollection<(LogicalRule ruleForPrice, LogicalRule ruleForQuantity, double discount)> _discountRules 
            = new List<(LogicalRule ruleForPrice, LogicalRule ruleForQuantity, double discount)>();
        
        public double CalculateDiscount(int price, int quantity)
        {
            var discount = 1.0d;

            foreach (var discountRule in _discountRules)
            {
                if (discountRule.ruleForPrice.IsTrue(price) && discountRule.ruleForQuantity.IsTrue(quantity))
                {
                    discount = discountRule.discount;
                }
            }

            return discount;
        }

        public void AddDiscountRule(LogicalRule ruleForPrice, LogicalRule ruleForQuantity, double discount)
        {
            _discountRules.Add((ruleForPrice, ruleForQuantity, discount));
        }
    }
}