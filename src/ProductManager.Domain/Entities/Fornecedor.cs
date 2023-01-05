using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ProductManager.Domain.Commons.ObjectValues;
using ProductManager.Domain.Commons.Exceptions;
using System.Linq;

namespace ProductManager.Domain.Entities
{
    public class Fornecedor
    {
        public Fornecedor()
        {
        }

        public int Id { get; set; } = 0;

        public CNPJ CNPJ { get; set; }

        public string Description { get; set; }

        public bool IsRemoved { get; set; } = false;


        //Navigation
        public IEnumerable<Produto> Products { get; set; }


        public bool Validate()
        {
            var ex = new BusinessException("O fornecedor não está válido.");


            if (string.IsNullOrWhiteSpace(Description))
            {
                ex.Validations.Add("Fornecedor do Produto deve conter uma descrição válida");

            }

            if (CNPJ == null)
            {
                ex.Validations.Add("CNPJ não pode estar nulo");

            }

            if (ex.Validations.Any())
            {
                throw ex;
            }

            return true;

        }
    }
}

