using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;
using ProductManager.Domain.Commons.Exceptions;
using ProductManager.Domain.Commons.Helpers;
using ProductManager.Domain.Commons.ObjectValues;
using ProductManager.Domain.Entities;
using ProductManager.Domain.Repositories.Contracts;
using ProductManager.Infra.DBContext;
using ProductManager.Infra.Repositories;
using Xunit;

namespace ProductManager.Infra.Tests.Repositories
{

    public class ProductRepositoryTest : IDisposable
    {
        public UnitOfWork Uow { get; }
        public ProductRepositoryTest()
        {
            var context = ConfigureRepositories.Configure();
            ConfigureRepositories.InitialInserts(providers, products);
            Uow = new UnitOfWork(context);
        }

        [Theory(DisplayName = "Deve deletar o produto logicamente")]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        public async Task DeletarProduto_ProdutoExistente_DeveDeletarLogicamente(int id)
        {
            var context = ConfigureRepositories.Configure();
            var repository = new ProductRepository(context);

            await repository.Delete(id);

            Uow.Commit();

            var deletedProduct = await context
                .Produtos
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();



            Assert.True(deletedProduct.IsRemoved);

        }

        [Fact(DisplayName = "Lançar exception para produto não encontrado.")]
        public async Task DeletarProduto_ProdutoInexistente_DeveLancarException()
        {
            var context = ConfigureRepositories.Configure();
            var repository = new ProductRepository(context);
            var id = 90;


            var message = "Produto não encontrado.";

            var ex = await Assert.ThrowsAsync<BusinessException>(() => repository.Delete(id));
            var containsMessageError = ex.Message.Equals(message);

            Assert.True(containsMessageError);

        }

        [Fact(DisplayName = "Deve deletar o fornecedor logicamente")]
        public async Task DeletarFornecedor_FornecedorExistente_DeveDeletarLogicamente()
        {
            var context = ConfigureRepositories.Configure();
            var repository = new ProductRepository(context);
            var id = 4;

            await repository.DeleteProvider(id);

            Uow.Commit();

            var deletedProvider = await context
                .Fornecedores
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();



            Assert.True(deletedProvider.IsRemoved);

        }

        [Fact(DisplayName = "O fornecedor não poderá ser removido, porque já existem produtos cadastrados com ele.")]
        public async Task DeletarFornecedor_FornecedorEmUso_DeveLancarException()
        {
            var context = ConfigureRepositories.Configure();
            var repository = new ProductRepository(context);
            var id = 1;

            var message = "O Fornecedor não poderá ser removido, porque já existem produtos cadastrados com ele.";

            var ex = await Assert.ThrowsAsync<BusinessException>(() => repository.DeleteProvider(id));
            var containsMessageError = ex.Message.Equals(message);

            Assert.True(containsMessageError);

        }

        [Fact(DisplayName = "Fornecedor não encontrado, deve lançar exception.")]
        public async Task DeletarFornecedor_FornecedorInexistente_DeveLancarException()
        {
            var context = ConfigureRepositories.Configure();
            var repository = new ProductRepository(context);
            var id = 90;

            var message = "Fornecedor não encontrado.";

            var ex = await Assert.ThrowsAsync<BusinessException>(() => repository.DeleteProvider(id));
            var containsMessageError = ex.Message.Equals(message);

            Assert.True(containsMessageError);

        }


        [Theory(DisplayName = "Deve Retornar todos os produtos de acordo com filtro de paginação")]
        [InlineData(5, 0, "", true, 5)]
        [InlineData(5, 0, null, true, 5)]
        [InlineData(5, 1, null, true, 3)]
        [InlineData(100, 0, "", true, 8)]
        [InlineData(100, 0, "", false, 9)]
        [InlineData(100, 0, "Produto 01", true, 1)]
        [InlineData(100, 1, "Produto 01", true, 0)]
        public async Task BuscarProdutos_ListarProdutos_DeveListarProdutosSegundoFiltro(int pageSize, int PageIndex, string term, bool onlyIsActive, int TotalOfItems)
        {
            var context = ConfigureRepositories.Configure();
            var repository = new ProductRepository(context);


            var filter = new PaginatedFilter();
            filter.PageSize = pageSize;
            filter.PageIndex = PageIndex;
            filter.Term = term;
            filter.OnlyIsActive = onlyIsActive;


            var res = await repository.FindAll(filter);

            Assert.Equal(res.Items.Count, TotalOfItems);
            Assert.Equal(res.PageIndex, PageIndex);
        }

        [Theory(DisplayName = "Deve Retornar todos os fornecedores de acordo com filtro de paginação")]
        [InlineData(5, 0, "", true, 4)]
        [InlineData(5, 0, null, true, 4)]
        [InlineData(5, 1, null, true, 0)]
        [InlineData(100, 0, "Provider 01", true, 1)]
        public async Task BuscarFornecedores_ListarFornecedores_DeveListarFornecedoresSegundoFiltro(int pageSize, int PageIndex, string term, bool onlyIsActive, int TotalOfItems)
        {
            var context = ConfigureRepositories.Configure();
            var repository = new ProductRepository(context);


            var filter = new PaginatedFilter();
            filter.PageSize = pageSize;
            filter.PageIndex = PageIndex;
            filter.Term = term;
            filter.OnlyIsActive = onlyIsActive;


            var res = await repository.FindAllProviders(filter);

            Assert.Equal(res.Items.Count, TotalOfItems);
            Assert.Equal(res.PageIndex, PageIndex);

        }


        [Fact(DisplayName = "Deve lançar exception para produto não encontrado")]
        public async Task BuscarProduto_ProdutoInexistente_DeveLancarException()
        {
            var context = ConfigureRepositories.Configure();
            var repository = new ProductRepository(context);
            var id = 90;

            var message = "Produto não encontrado.";

            var ex = await Assert.ThrowsAsync<BusinessException>(() => repository.FindById(id));
            var containsMessageError = ex.Message.Equals(message);

            Assert.True(containsMessageError);

        }

        [Fact(DisplayName = "Deve encontrar produto")]
        public async Task BuscarProduto_ProdutoExistente_DeveEncontrarProduto()
        {
            var context = ConfigureRepositories.Configure();
            var repository = new ProductRepository(context);
            var id = 2;

            var product = await repository.FindById(id);


            Assert.Equal(product.Id, id);

        }


        [Fact(DisplayName = "Deve lançar exception para fornecedor não encontrado")]
        public async Task BuscarFornecedor_FornecedorInexistente_DeveLancarException()
        {
            var context = ConfigureRepositories.Configure();
            var repository = new ProductRepository(context);
            var id = 90;

            var message = "Fornecedor não encontrado.";

            var ex = await Assert.ThrowsAsync<BusinessException>(() => repository.FindProviderById(id));
            var containsMessageError = ex.Message.Equals(message);

            Assert.True(containsMessageError);

        }

        [Fact(DisplayName = "Fornecedor deverá ser encontrado")]
        public async Task BuscarFornecedor_FornecedorExistente_DeveEncontrarFornecedor()
        {
            var context = ConfigureRepositories.Configure();
            var repository = new ProductRepository(context);
            var id = 2;

            var product = await repository.FindProviderById(id);

            Assert.Equal(product.Id, id);
        }

        [Fact(DisplayName = "Deve inserir produto")]
        public async Task InserirProduto_NovoProduto_DeveInserirProduto()
        {
            var context = ConfigureRepositories.Configure();
            var repository = new ProductRepository(context);
            var newProduct = ProductFactory(0, "Novo produto", 1, false, true);

            var id =  await repository.Insert(newProduct);
            Uow.Commit();

            var productAfterSave =  await repository.FindById(id);

            Assert.Equal(newProduct.Description, productAfterSave.Description);
            Assert.Equal(newProduct.IsActive, productAfterSave.IsActive);
            Assert.Equal(newProduct.IsRemoved, productAfterSave.IsRemoved);
            Assert.Equal(newProduct.ProviderId, productAfterSave.ProviderId);
            Assert.Equal(newProduct.ManufacturingDate, productAfterSave.ManufacturingDate);
            Assert.Equal(newProduct.ExpirationDate, productAfterSave.ExpirationDate);

        }

        [Fact(DisplayName = "Deve lancar exception ao inserir produto com fornecedor inválido")]
        public async Task InserirProduto_IdFornecedorInvalido_DeveLancarException()
        {
            var context = ConfigureRepositories.Configure();
            var repository = new ProductRepository(context);
            var newProduct = ProductFactory(1, "Novo produto", 90, false, true);
            var messageError = $"Fornecedor com Id {newProduct.ProviderId} não existe.";

            var ex = await Assert.ThrowsAsync<BusinessException>(() => repository.Insert(newProduct));
            var containsMessageError = ex.Message.Contains(messageError);


            Assert.True(containsMessageError);

        }

        [Fact(DisplayName = "Deve inserir novo fornecedor")]
        public async Task InserirFornecedor_NovoFornecedor_DeveInserirNovoFornecedor()
        {
            var context = ConfigureRepositories.Configure();
            var repository = new ProductRepository(context);
            var newProvider = ProviderFactory(1, "Novo provider", "09.247.986/0001-73");


            var id = await repository.InsertProvider(newProvider);
            Uow.Commit();

            var providerAfterSave = await repository.FindProviderById(id);


            Assert.Equal(newProvider.Description, providerAfterSave.Description);
            Assert.Equal(newProvider.CNPJ, providerAfterSave.CNPJ);
            Assert.Equal(newProvider.IsRemoved, providerAfterSave.IsRemoved);

        }

        [Fact(DisplayName = "Deve lançar exception ao tentar inserir fornecedor com CNPJ duplicado")]
        public async Task InserirFornecedor_NovoFornecedorComCNPJDuplicado_DeveLancarException()
        {
            var context = ConfigureRepositories.Configure();
            var repository = new ProductRepository(context);

            var newProvider = ProviderFactory(1, "Novo provider", "45.341.409/0001-00");
            var messageError = $"Já existe um fornecedor com CNPJ {newProvider.CNPJ}";

            var ex = await Assert.ThrowsAsync<BusinessException>(() => repository.InsertProvider(newProvider));
            var containsMessageError = ex.Message.Contains(messageError);


            Assert.True(containsMessageError);
        }

        [Fact(DisplayName = "Deve atualizar produto")]
        public async Task AtualizarProduto_ProdutoValido_DeveAtualizarProduto()
        {
            var context = ConfigureRepositories.Configure();
            var repository = new ProductRepository(context);
            var id = 4;

            var product = new Produto();
            product.Id = id;
            product.Description = "Nova Descricao para product";
            product.IsActive = false;
            product.ExpirationDate = product.ExpirationDate;
            product.ManufacturingDate = product.ManufacturingDate;
            product.ProviderId = 2;


            var res = await repository.Update(product);
            Uow.Commit();

            var productAfterUpdate = await repository.FindById(id);


            Assert.Equal(res, id);
            Assert.Equal(productAfterUpdate.Description, product.Description);
            Assert.Equal(productAfterUpdate.IsActive, product.IsActive);
            Assert.Equal(productAfterUpdate.ManufacturingDate, product.ManufacturingDate);
            Assert.Equal(productAfterUpdate.ExpirationDate, product.ExpirationDate);
            Assert.Equal(productAfterUpdate.ProviderId, product.ProviderId);

        }

        [Fact(DisplayName = "Deve lançar exception para produto com id inválido")]
        public async Task AtualizarProduto_ProdutoComIdInválido_DeveLancarException()
        {
            var context = ConfigureRepositories.Configure();
            var repository = new ProductRepository(context);
            var id = 90;

            var product = ProductFactory(id, "asdf", 1, false, true);

            var messageError = $"Produto com Id {product.Id} não existe.";


            var ex = await Assert.ThrowsAsync<BusinessException>(() => repository.Update(product));
            var containsMessageError = ex.Message.Contains(messageError);


            Assert.True(containsMessageError);

        }

        [Fact(DisplayName = "Deve lançar exception para produto com fornecedorId inválido")]
        public async Task AtualizarProduto_ProdutoComFornecedorInvalido_DeveLancarException()
        {
            var context = ConfigureRepositories.Configure();
            var repository = new ProductRepository(context);
            var id = 1;
            var providerId = 90;

            var product = ProductFactory(id, "asdf", providerId, false, true);

            var messageError = $"Fornecedor com Id {product.ProviderId} não existe.";


            var ex = await Assert.ThrowsAsync<BusinessException>(() => repository.Update(product));
            var containsMessageError = ex.Message.Contains(messageError);


            Assert.True(containsMessageError);

        }

        [Fact(DisplayName = "Deve atualizar fornecedor")]
        public async Task AtualizarFornecedor_FornecedorValido_DeveAtualizarFornecedor()
        {
            var context = ConfigureRepositories.Configure();
            var repository = new ProductRepository(context);
            var id = 4;

            var provider = await repository.FindProviderById(id);
            provider.Description = "Nova Descricao para provider";


            var res = await repository.UpdateProvider(provider);
            Uow.Commit();

            var providerAfterUpdate = await repository.FindProviderById(id);


            Assert.Equal(res, id);
            Assert.Equal(providerAfterUpdate.Description, provider.Description);

        }

        [Fact(DisplayName = "Deve lançar exception ao tentar atualizar CNPJ")]
        public async Task AtualizarFornecedor_FornecedorInvalido_DeveLancarException()
        {
            var context = ConfigureRepositories.Configure();
            var repository = new ProductRepository(context);
            var id = 4;

            var provider = ProviderFactory(id, "Nova Descricao para provider", "45.341.409/0001-00");
            var messageError = $"Não é possível alterar o CNPJ do fornecedor";


            var ex = await Assert.ThrowsAsync<BusinessException>(() => repository.UpdateProvider(provider));
            var containsMessageError = ex.Message.Contains(messageError);


            Assert.True(containsMessageError);

        }


        [Fact(DisplayName = "Deve lançar exception ao tentar atualizar fornecedor inexistente")]
        public async Task AtualizarFornecedor_FornecedorInexistente_DeveLancarException()
        {
            var context = ConfigureRepositories.Configure();
            var repository = new ProductRepository(context);
            var id = 90;

            var provider = ProviderFactory(id, "Nova Descricao para provider", "42.466.098/0001-17");
            var messageError = $"Fornecedor com id {id} não encontrado";


            var ex = await Assert.ThrowsAsync<BusinessException>(() => repository.UpdateProvider(provider));
            var containsMessageError = ex.Message.Contains(messageError);


            Assert.True(containsMessageError);

        }



        private List<Produto> products = new List<Produto>
        {
            ProductFactory(1, "Produto 01", 1, false, true),
            ProductFactory(2, "Produto 02 ", 1, false, true),
            ProductFactory(4, "Produto 04 ", 2, false, true),
            ProductFactory(7, "Produto 07 ", 3, false, true),
            ProductFactory(8, "Produto 08 ", 3, false, true),
            ProductFactory(9, "Produto 09 ", 3, false, true),
            ProductFactory(10, "Produto 10 ", 3, false, true),
            ProductFactory(12, "Produto 12 ", 3, false, true),

            ProductFactory(3, "Produto 03 [Removido]", 1, true, true),
            ProductFactory(5, "Produto 05 [Removido]", 2, true, true),
            ProductFactory(11, "Produto 11 [Inativo]", 3, false, false),
        };

        private List<Fornecedor> providers = new List<Fornecedor>
        {
            ProviderFactory(1, "Provider 01", "45.341.409/0001-00"),
            ProviderFactory(2, "Provider 02", "17.825.678/0001-06"),
            ProviderFactory(3, "Provider 03", "47.437.002/0001-06"),
            ProviderFactory(4, "Provider 04", "42.466.098/0001-17"),
        };

        private static Produto ProductFactory(int id, String description, int providerId,bool isRemoved, bool isActive)
        {
            return new Produto()
            {
                Id = id,
                Description = description,
                ExpirationDate = DateTime.Now.AddYears(5),
                IsActive = isActive,
                IsRemoved = isRemoved,
                ManufacturingDate = DateTime.Now,
                ProviderId = providerId,
            };
        }


        private static Fornecedor ProviderFactory(int id, String description, string cnpj)
        {
            return new Fornecedor()
            {
                Id = id,
                Description = description,
                CNPJ = new CNPJ(cnpj),
                IsRemoved = false,
            };
        }

        public void Dispose()
        {
            var context = ConfigureRepositories.Configure();
            ConfigureRepositories.ClearDatabase(providers, products);
        }
    }
}

