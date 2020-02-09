using System.Collections;
using System.Collections.Generic;

namespace OrderService
{
    public class Receipt
    {
        public string Header { get; set; }

        public ICollection<string> Specification { get; set; }

        public string Subtotal { get; set; }

        public string MVA { get; set; }

        public string Total { get; set; }
    }
}