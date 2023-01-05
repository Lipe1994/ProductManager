using System;
using System.Collections.Generic;
using ProductManager.Domain.Commons.ObjectValues;
using ProductManager.Domain.Entities;

namespace ProductManager.Infra.DBContext
{
    public class Seeds
    {
        public Seeds()
        {
        }

        public static List<Fornecedor> ListaDeFornecedores()
        {
            return new List<Fornecedor>
            {
                new Fornecedor
                {
                    CNPJ = new CNPJ("11.068.167/0004-53"),
                    Description = "Acer",
                    IsRemoved = false,
                    Id = 1,
                },
                new Fornecedor
                {
                    CNPJ = new CNPJ("72.381.189/0001-10"),
                    Description = "Dell",
                    IsRemoved = false,
                    Id = 2,
                },
                new Fornecedor
                {
                    CNPJ = new CNPJ("00.623.904/0001-73"),
                    Description = "Apple",
                    IsRemoved = false,
                    Id = 3,
                },
            };
        }


        public static List<Produto> ListaDeProdutos()
        {
            return new List<Produto>
            {
                new Produto
                {
                    Id = 1,
                    Description = "ACER Notebook Gamer Nitro",
                    ExpirationDate = DateTime.Now.AddYears(5),
                    ManufacturingDate = DateTime.Now.AddYears(-1),
                    IsActive = true,
                    IsRemoved = false,
                    ProviderId = 1

                },
                new Produto
                {
                    Id = 2,
                    Description = "ACER Notebook Nitro 5 AN515-44-R4KA",
                    ExpirationDate = DateTime.Now.AddYears(4),
                    ManufacturingDate = DateTime.Now.AddYears(-1),
                    IsActive = true,
                    IsRemoved = false,
                    ProviderId = 1

                },
                new Produto
                {
                    Id = 3,
                    Description = "ACER Notebook Gamer Nitro 5 AN515-55-79X0",
                    ExpirationDate = DateTime.Now.AddYears(6),
                    ManufacturingDate = DateTime.Now.AddYears(-1),
                    IsActive = true,
                    IsRemoved = false,
                    ProviderId = 1

                },
                new Produto
                {
                    Id = 4,
                    Description = "ACER AN517-52-50RS",
                    ExpirationDate = DateTime.Now.AddYears(6),
                    ManufacturingDate = DateTime.Now.AddYears(-1),
                    IsActive = true,
                    IsRemoved = false,
                    ProviderId = 1

                },
                new Produto
                {
                    Id = 5,
                    Description = "Notebook Dell Inspiron 5402",
                    ExpirationDate = DateTime.Now.AddYears(5),
                    ManufacturingDate = DateTime.Now.AddYears(-1),
                    IsActive = true,
                    IsRemoved = false,
                    ProviderId = 2

                },
                new Produto
                {
                    Id = 6,
                    Description = "DELL Notebook Gamer G15-i1000-D20P",
                    ExpirationDate = DateTime.Now.AddYears(5),
                    ManufacturingDate = DateTime.Now.AddYears(-1),
                    IsActive = true,
                    IsRemoved = false,
                    ProviderId = 2

                },
                new Produto
                {
                    Id = 7,
                    Description = "Notebook Dell Inspiron i15-i1100-A70S 15.6",
                    ExpirationDate = DateTime.Now.AddYears(5),
                    ManufacturingDate = DateTime.Now.AddYears(-1),
                    IsActive = true,
                    IsRemoved = false,
                    ProviderId = 2

                },
                new Produto
                {
                    Id = 8,
                    Description = "Notebook Apple MacBook Air (de 13 polegadas)",
                    ExpirationDate = DateTime.Now.AddYears(8),
                    ManufacturingDate = DateTime.Now.AddYears(-2),
                    IsActive = true,
                    IsRemoved = false,
                    ProviderId = 3

                },
                new Produto
                {
                    Id = 9,
                    Description = "Notebook Apple MacBook PRO (de 13 polegadas)",
                    ExpirationDate = DateTime.Now.AddYears(8),
                    ManufacturingDate = DateTime.Now.AddYears(-2),
                    IsActive = true,
                    IsRemoved = false,
                    ProviderId = 3

                },
                new Produto
                {
                    Id = 10,
                    Description = "Notebook Apple MacBook Air de 13 polegadas: Chip M2",
                    ExpirationDate = DateTime.Now.AddYears(8),
                    ManufacturingDate = DateTime.Now.AddYears(-1),
                    IsActive = true,
                    IsRemoved = false,
                    ProviderId = 3

                },
                new Produto
                {
                    Id = 11,
                    Description = "Notebook Apple MacBook Air de 13 polegadas Intel",
                    ExpirationDate = DateTime.Now.AddYears(3),
                    ManufacturingDate = DateTime.Now.AddYears(-3),
                    IsActive = true,
                    IsRemoved = false,
                    ProviderId = 3

                },
            };
        }
    }
}

