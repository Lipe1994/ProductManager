using System;
using System.Reflection;
using ProductManager.Domain.Commons.Exceptions;
using ProductManager.Domain.Commons.ObjectValues;
using ProductManager.Domain.Entities;
using Xunit;

namespace ProductManager.Domain.Tests.Entities
{
    public class ProdutoTest
    {
        public ProdutoTest()
        {
        }


        [Fact(DisplayName = "Produto deve estar válido.")]
        public void ValidaCNPJ_CNPJValido_DeveValidarCNPJ()
        {
            var product = new Produto()
            {
                Id = 1,
                Description = "Computador",
                ExpirationDate = DateTime.Now.AddYears(5),
                IsActive = true,
                IsRemoved = false,
                ManufacturingDate = DateTime.Now,
                ProviderId = 1,
                Provider = new Fornecedor {
                    Id = 1,
                    CNPJ = new CNPJ("80.739.085/0001-63"),
                    Description = "Dell",
                    IsRemoved = false,
                }
            };

            var valid = product.Validate();

            Assert.True(valid);

        }


        [Fact(DisplayName = "Deve lancar exception, data de fabricação não pode ser maior ou igual a data de validade.")]
        public void ValidaProduto_ProdutoInvalido_DeveLancarException()
        {
            const string messageEx = "O produto não está válido.";
            const string messageError = "Data de fabricação que não pode ser maior ou igual a data de validade.";
            var product = new Produto()
            {
                Id = 1,
                Description = "Computador",
                ExpirationDate = DateTime.Now,
                IsActive = true,
                IsRemoved = false,
                ManufacturingDate = DateTime.Now,
                ProviderId = 1,
                Provider = new Fornecedor
                {
                    Id = 1,
                    CNPJ = new CNPJ("80.739.085/0001-63"),
                    Description = "Dell",
                    IsRemoved = false,
                }
            };


            var ex = Assert.Throws<BusinessException>(() => product.Validate());
            var containsMessageError = ex.Validations.Contains(messageError);

            Assert.Equal(ex.Message, messageEx);
            Assert.True(containsMessageError);

        }

        [Fact(DisplayName = "Deve conter uma descrição válida")]
        public void ValidaProduto_DescricaoInvalida_DeveLancarException()
        {
            const string messageEx = "O produto não está válido.";
            const string messageError = "Produto deve conter uma descrição válida";
            var product = new Produto()
            {
                Id = 1,
                Description = "",
                ExpirationDate = DateTime.Now.AddYears(5),
                IsActive = true,
                IsRemoved = false,
                ManufacturingDate = DateTime.Now,
                ProviderId = 1,
                Provider = new Fornecedor
                {
                    Id = 1,
                    CNPJ = new CNPJ("80.739.085/0001-63"),
                    Description = "Dell",
                    IsRemoved = false,
                }
            };


            var ex = Assert.Throws<BusinessException>(() => product.Validate());
            var containsMessageError = ex.Validations.Contains(messageError);

            Assert.Equal(ex.Message, messageEx);
            Assert.True(containsMessageError);
        }

    }
}

