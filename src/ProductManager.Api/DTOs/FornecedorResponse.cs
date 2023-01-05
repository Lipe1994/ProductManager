using System;
using System.Collections.Generic;
using ProductManager.Domain.Commons.ObjectValues;

namespace ProductManager.Api.DTOs
{
    public class FornecedorResponse
    {
        public FornecedorResponse()
        {
        }

        public int Id { get; set; }
        public string CNPJ { get; set; }
        public string Description { get; set; }
    }
}

