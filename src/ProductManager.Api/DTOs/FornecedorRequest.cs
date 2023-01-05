using System;
using ProductManager.Domain.Commons.ObjectValues;

namespace ProductManager.Api.DTOs
{
    public class FornecedorRequest
    {
        public FornecedorRequest()
        {
        }

        public string CNPJ { get; set; }

        public string Description { get; set; }

    }
}

