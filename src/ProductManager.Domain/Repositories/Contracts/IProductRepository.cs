using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductManager.Domain.Commons.Exceptions;
using ProductManager.Domain.Commons.Helpers;
using ProductManager.Domain.Entities;

namespace ProductManager.Domain.Repositories.Contracts
{
    public interface IProductRepository
    {
        Task<Produto> FindById(int id);
        Task<Fornecedor> FindProviderById(int id);


        Task<PaginatedList<Produto>> FindAll(PaginatedFilter filter);
        Task<PaginatedList<Fornecedor>> FindAllProviders(PaginatedFilter filter);

        Task<int> Insert(Produto product);
        Task<int> InsertProvider(Fornecedor provider);

        Task Delete(int id);
        Task DeleteProvider(int idProvider);


        Task<int> Update(Produto commandUpdate);
        Task<int> UpdateProvider(Fornecedor commandUpdate);
    }
}

