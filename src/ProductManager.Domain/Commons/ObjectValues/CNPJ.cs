using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ProductManager.Domain.Commons.Exceptions;

namespace ProductManager.Domain.Commons.ObjectValues
{
    public class CNPJ
    {
        public CNPJ(string cnpj)
        {
            var formatedCNPJ = cnpj.Trim();
            formatedCNPJ = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (ValidateCNPJ(formatedCNPJ))
            {
                _cnpj = formatedCNPJ;
            }
        }

        private string _cnpj;

        public string Value {
            get => _cnpj;
        }


        bool ValidateCNPJ(string cnpj)
        {

            if (string.IsNullOrWhiteSpace(cnpj))
            {
                throw new BusinessException("O CNPJ não pode estar nulo ou vazio.");
            }

            if (new Regex(@"^[0-9]").IsMatch(cnpj) == false)
            {
                throw new BusinessException("O CNPJ está inválido, deve conter somente números.");
            }

            var blackList = new List<string>{
                "00000000000000",
                "11111111111111",
                "22222222222222",
                "33333333333333",
                "44444444444444",
                "55555555555555",
                "66666666666666",
                "77777777777777",
                "88888888888888",
                "99999999999999"
            };

            if (blackList.Contains(cnpj))
            {
                throw new BusinessException("O CNPJ está inválido, não deve conter somente o mesmo dígito repetido 14 vezes.");
            }


            if (cnpj.Count() != 14)
            {
                throw new BusinessException("O CNPJ está inválido, deve conter 14 dígitos.");
            }

            //TODO melhorar essa verificacao

            var cnpjArrayOfDigits = cnpj.Select(d => int.Parse(d.ToString())).ToArray();
            var checkers = cnpjArrayOfDigits.TakeLast(2);

            if (calculateCheckDigit(cnpjArrayOfDigits.Take(12).Reverse().ToArray()) != checkers.First())
            {
                throw new BusinessException("O CNPJ está inválido. O primeiro dígito verificador não está válido.");
            }

            if (calculateCheckDigit(cnpjArrayOfDigits.Take(13).Reverse().ToArray()) != checkers.Last())
            {
                throw new BusinessException("O CNPJ está inválido. O segundo dígito verificador não está válido.");
            }

            return true;
        }

        int calculateCheckDigit(int[] digitsOfCnpj)
        {
            int[] multiplier = new[] { 2,3,4,5,6,7,8,9};
            int acc = 0;
            int multiplierIndice = 0;

            foreach(var digit in digitsOfCnpj)
            {
                if (multiplierIndice > 7)
                {
                    multiplierIndice = 0;
                }

                acc += digit * multiplier[multiplierIndice];

                multiplierIndice++;
            }

            var calculatedDigit = (acc % 11);

            return calculatedDigit < 2 ? 0 : 11 - calculatedDigit;
        }


        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is CNPJ))
            {
                return false;
            }
            return (this.Value == ((CNPJ)obj).Value);
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode() * 17;
        }
    }
}

