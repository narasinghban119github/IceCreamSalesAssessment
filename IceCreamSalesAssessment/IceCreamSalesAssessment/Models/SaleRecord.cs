using System;
using System.Collections.Generic;
using System.Text;

namespace IceCreamSalesAssessment.Models
{
    
        public class SaleRecord
        {
            public DateTime Date { get; set; }

            public string Sku { get; set; } = string.Empty;

            public decimal UnitPrice { get; set; }

            public int Quantity { get; set; }

            public decimal TotalPrice { get; set; }
        }
    }

