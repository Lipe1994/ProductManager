using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using ProductManager.Domain.Commons.Exceptions;
using ProductManager.Domain.Commons.ObjectValues;
using Xunit;

namespace ProductManager.Domain.Tests.Commons.ObjectValues
{
    public class CNPJTest
    {
        public CNPJTest()
        {

        }


        [Fact(DisplayName = "CNPJ deve estar válido.")]
        public void ValidarCNPJ_CNPJValido_CNPJDeveEstarValido()
        {
            
            var inputCNPJ = "25.252.713/0001-73";
            var cnpj = new CNPJ(inputCNPJ);


            
            var formatedCNPJ = inputCNPJ.Trim();
            formatedCNPJ = inputCNPJ.Replace(".", "").Replace("-", "").Replace("/", "");


            
            Assert.Equal(cnpj.Value, formatedCNPJ);
        }

        [Fact(DisplayName = "Deve lançar exception para CNPJ inválido")]
        public void ValidarCNPJ_CNPJInvalido_DeveLancarException()
        {
            
            var inputCNPJ = "25.252.713/0001-70";

            
            var ex = Assert.Throws<BusinessException>(()=> new CNPJ(inputCNPJ));

            Assert.NotEmpty(ex.Message);
        }

        [Fact(DisplayName = "CNPJ deve estar com primeiro dígito verificador inválido.")]
        public void ValidarCNPJ_CNPJPrimeiroDigitoInvalido_DeveLancarException()
        {
            const string message = "O CNPJ está inválido. O primeiro dígito verificador não está válido.";
            var inputCNPJ = "25.252.713/0001-00";


            var ex = Assert.Throws<BusinessException>(() => new CNPJ(inputCNPJ));

            Assert.Equal(ex.Message, message);
        }

        [Fact(DisplayName = "CNPJ deve estar com segundo dígito verificador inválido.")]
        public void ValidarCNPJ_CNPJSegundoDigitoInvalido_DeveLancarException()
        {

            var inputCNPJ = "25.252.713/0001-71";
            const string message = "O CNPJ está inválido. O segundo dígito verificador não está válido.";

            var ex = Assert.Throws<BusinessException>(() => new CNPJ(inputCNPJ));

            Assert.Equal(ex.Message, message);
        }

        [Fact(DisplayName = "CNPJ deve estar inválido por estar mencionado na lista de patterns inválidos.")]
        public void ValidarCNPJ_CNPJComPatternInvalido_DeveLancarException()
        {
            const string message = "O CNPJ está inválido, não deve conter somente o mesmo dígito repetido 14 vezes.";
            var inputCNPJ = "11.111.111/1111-11";


            var ex = Assert.Throws<BusinessException>(() => new CNPJ(inputCNPJ));

            Assert.Equal(ex.Message, message);
        }

        [Fact(DisplayName = "O CNPJ não pode estar nulo ou vazio.")]
        public void ValidarCNPJ_CNPJVazio_DeveLancarException()
        {

            const string message = "O CNPJ não pode estar nulo ou vazio.";

            var inputCNPJ = "";


            var ex = Assert.Throws<BusinessException>(() => new CNPJ(inputCNPJ));

            Assert.Equal(ex.Message, message);
        }

        [Fact(DisplayName = "O CNPJ está inválido, deve conter somente números.")]
        public void ValidarCNPJ_CNPJComLetras_DeveLancarException()
        {
            const string message = "O CNPJ está inválido, deve conter somente números.";
            var inputCNPJ = "bb.asd.111/1111-11";


            var ex = Assert.Throws<BusinessException>(() => new CNPJ(inputCNPJ));

            Assert.Equal(ex.Message, message);
        }
    }
}
