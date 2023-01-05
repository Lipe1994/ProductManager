using System;
using System.Collections.Generic;
using ProductManager.Domain.Entities;

namespace ProductManager.Api.DTOs
{
    public class ProdutoResponse
    {
        public ProdutoResponse()
        {
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string Provider { get; set; }
        public string ProviderCNPJ { get; set; }
        public bool IsActive { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int ProviderId { get; set; }

    }
}

