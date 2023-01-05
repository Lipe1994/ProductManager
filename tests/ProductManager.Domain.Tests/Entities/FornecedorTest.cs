using System;
using System.Reflection;
using ProductManager.Domain.Commons.Exceptions;
using ProductManager.Domain.Commons.ObjectValues;
using ProductManager.Domain.Entities;
using Xunit;

namespace ProductManager.Domain.Tests.Entities
{
    public class FornecedorTest
    {
        public FornecedorTest()
        {
        }
        
        [Fact(DisplayName = "Deve conter uma descrição válida")]
        public void ValidarFornecedor_DescricaoInvalida_DeveLancarException()
        {
            const string messageEx = "O fornecedor não está válido.";
            const string messageError = "Fornecedor do Produto deve conter uma descrição válida";
            var fornecedor = new Fornecedor
            {
                Id = 1,
                CNPJ = new CNPJ("80.739.085/0001-63"),
                Description = "",
                IsRemoved = false,
            };

            var ex = Assert.Throws<BusinessException>(() => fornecedor.Validate());
            var containsMessageError = ex.Validations.Contains(messageError);

            Assert.Equal(ex.Message, messageEx);
            Assert.True(containsMessageError);
        }

        [Fact(DisplayName = "Deve lançar exception se o CNPJ estiver nulo")]
        public void ValidarFornecedor_CNPJNulo_DeveLancarException()
        {
            const string messageEx = "O fornecedor não está válido.";
            const string messageError = "CNPJ não pode estar nulo";
            var fornecedor = new Fornecedor
            {
                Id = 1,
                //CNPJ = new CNPJ("80.739.085/0001-63"),
                Description = "Dell",
                IsRemoved = false,
            };


            var ex = Assert.Throws<BusinessException>(() => fornecedor.Validate());
            var containsMessageError = ex.Validations.Contains(messageError);

            Assert.Equal(ex.Message, messageEx);
            Assert.True(containsMessageError);
        }
    }
}

