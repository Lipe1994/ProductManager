using System;
using System.Linq;
using ProductManager.Domain.Commons.Exceptions;
using ProductManager.Domain.Commons.ObjectValues;

namespace ProductManager.Domain.Entities
{
    public class Produto
    {
        public Produto(){}
        public int Id { get; set; } = 0;
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsRemoved { get; set; } = false;
        public DateTime ManufacturingDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int ProviderId { get; set; }


        //Navigation property
        public Fornecedor Provider { get; set; }



        public bool Validate()
        {
            var ex = new BusinessException("O produto não está válido.");

            if (ManufacturingDate >= ExpirationDate)
            {
                ex.Validations.Add("Data de fabricação que não pode ser maior ou igual a data de validade.");

            }

            if (string.IsNullOrWhiteSpace(Description))
            {
                ex.Validations.Add("Produto deve conter uma descrição válida");

            }

            if (ex.Validations.Any())
            {
                throw ex;
            }

            return true;

        }
    }
}

