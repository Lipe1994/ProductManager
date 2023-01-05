using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductManager.Domain.Commons.Exceptions;
using ProductManager.Domain.Commons.Helpers;
using ProductManager.Domain.Entities;
using ProductManager.Domain.Repositories.Contracts;
using ProductManager.Infra.DBContext;

namespace ProductManager.Infra.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public ProductManagerDBContext Context { get; set; }

        public ProductRepository(ProductManagerDBContext context)
        {
            Context = context;
        }

        public async Task Delete(int id) {
            var product = await Context.Produtos.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (product == null)
            {
                throw new BusinessException("Produto não encontrado.");
            }

            product.IsRemoved = true;

        }

        public async Task DeleteProvider(int idProvider) {
            var provider = await Context.Fornecedores.Include(p => p.Products).Where(x => x.Id == idProvider).FirstOrDefaultAsync();

            if (provider == null)
            {
                throw new BusinessException("Fornecedor não encontrado.");
            }

            if (provider.Products.Any())
            {
                throw new BusinessException("O Fornecedor não poderá ser removido, porque já existem produtos cadastrados com ele.");
            }

            provider.IsRemoved = true;
        }

        public async Task<PaginatedList<Produto>> FindAll(PaginatedFilter filter)
        {
            var products = Context
                .Produtos
                .Include(p => p.Provider)
                .Where(p => filter.OnlyIsActive ? p.IsActive : true)
                .Where(p => !p.IsRemoved)
                .Where(p =>
                    string.IsNullOrWhiteSpace(filter.Term) ||
                    p.Description.ToLower().Contains(filter.Term.ToLower()
                 ))
                .AsNoTracking();

            var totalCount = products.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)filter.PageSize);


            var paginatedProducts = await products
                .Skip(filter.PageIndex * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            var paganatedProducts = new PaginatedList<Produto>(paginatedProducts, filter.PageIndex, totalCount, totalPages);
            return paganatedProducts;
        }

        public async Task<PaginatedList<Fornecedor>> FindAllProviders(PaginatedFilter filter)
        {
            var providers = Context
                .Fornecedores
                .Include(p => p.Products)
                .Where(p => !p.IsRemoved)
                .Where(p =>
                    string.IsNullOrWhiteSpace(filter.Term) ||
                    p.Description.ToLower().Contains(filter.Term.ToLower()
                 ))
                .AsNoTracking();

            var totalCount = providers.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)filter.PageSize);


            var paginatedProducts = await providers.Skip(filter.PageIndex * filter.PageSize).Take(filter.PageSize).ToListAsync();
            var paganatedProviders = new PaginatedList<Fornecedor>(paginatedProducts, filter.PageIndex, totalCount, totalPages);

            return paganatedProviders;
        }

        public async Task<Produto> FindById(int id)
        {
            var product = await Context.Produtos
                .Include(p => p.Provider)
                .Where(x => x.Id == id)
                .Where(p => !p.IsRemoved)

                .FirstOrDefaultAsync();

            if (product == null)
            {
                throw new BusinessException("Produto não encontrado.");
            }

            return product;
        }

        public async Task<int> Insert(Produto product)
        {
            var exists = await Context.Fornecedores.Where(x => x.Id == product.ProviderId)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

            if (exists == null)
            {
                throw new BusinessException($"Fornecedor com Id {product.ProviderId} não existe.");
            }

            product.Id = 0;
            var res = await Context.Produtos.AddAsync(product);

            return res.Entity.Id;
            
        }

        public async Task<int> InsertProvider(Fornecedor provider)
        {
            var exists = await Context
                    .Fornecedores
                    .Where(x => x.CNPJ == provider.CNPJ)
                    .AsNoTracking()
                    .ToListAsync();

            if (exists.Any())
            {
                throw new BusinessException($"Já existe um fornecedor com CNPJ {provider.CNPJ}");
            }

            provider.Id = 0;
            var res = await Context.Fornecedores.AddAsync(provider);

            return res.Entity.Id;
        }

        public async Task<Fornecedor> FindProviderById(int id)
        {
            var fornecedor = await Context.Fornecedores
                .Where(p => p.Id == id)
                .Where(p => !p.IsRemoved)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (fornecedor == null)
            {
                throw new BusinessException("Fornecedor não encontrado.");
            }

            return fornecedor;
        }

        public async Task<int> Update(Produto commandUpdate)
        {

            var providerExists = await Context.Fornecedores
                .Where(x => x.Id == commandUpdate.ProviderId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (providerExists == null)
            {
                throw new BusinessException($"Fornecedor com Id {commandUpdate.ProviderId} não existe.");
            }

            var product = await Context.Produtos.Where(x => x.Id == commandUpdate.Id).FirstOrDefaultAsync();

            if (product == null)
            {
                throw new BusinessException($"Produto com Id {commandUpdate.Id} não existe.");
            }

            product.Description = commandUpdate.Description;
            product.IsActive = commandUpdate.IsActive;
            product.ProviderId = commandUpdate.ProviderId;
            product.ExpirationDate = commandUpdate.ExpirationDate;
            product.ManufacturingDate = commandUpdate.ManufacturingDate;

            var res = Context.Produtos.Update(product);

            return res.Entity.Id;

        }

        public async Task<int> UpdateProvider(Fornecedor commandUpdate)
        {
            var provider = await Context.Fornecedores
                .Where(x => x.Id == commandUpdate.Id)
                .FirstOrDefaultAsync();

            if (provider == null)
            {
                throw new BusinessException($"Fornecedor com id {commandUpdate.Id} não encontrado");
            }

            if (provider.CNPJ.Value != commandUpdate.CNPJ.Value)
            {
                throw new BusinessException($"Não é possível alterar o CNPJ do fornecedor");
            }

            provider.Description = commandUpdate.Description;

            var res = Context.Fornecedores.Update(provider);

            return res.Entity.Id;
        }
    }
}

