using System;
using ProductManager.Domain.Commons.ObjectValues;

namespace ProductManager.Api.DTOs
{
    public class AtualizarFornecedorRequest
    {
        public AtualizarFornecedorRequest()
        {
        }

        public int Id { get; set; }

        public string CNPJ { get; set; }

        public string Description { get; set; }

    }
}

