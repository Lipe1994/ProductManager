using System;
namespace ProductManager.Api.DTOs
{
    public class AtualizarProdutoRequest
    {
        public AtualizarProdutoRequest()
        {
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int ProviderId { get; set; }
    }
}

