using System;
using System.Collections.Generic;

namespace ProductManager.Api.DTOs
{
    public class ProdutoRequest
    {
        public ProdutoRequest()
        {
        }

        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime ManufacturingDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int ProviderId { get; set; }

    }
}

