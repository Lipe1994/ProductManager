using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductManager.Api.DTOs;
using ProductManager.Domain.Commons.Helpers;
using ProductManager.Domain.Entities;
using ProductManager.Domain.Repositories.Contracts;

namespace ProductManager.Api.Controllers
{
    [ApiController]
    [Route("v1/[Controller]")]
    public class ProdutoController : ControllerBase
    {
        public IProductRepository repository { get; }
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public ProdutoController(IProductRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("produtos")]
        public async Task<PaginatedList<ProdutoResponse>> Produtos([FromQuery]PaginatedFilter filter)
        {
            var products = await repository.FindAll(filter);

            var mapOfProducts = mapper.Map<PaginatedList<ProdutoResponse>>(products);

            
            return mapOfProducts;
        }


        [HttpGet("{id}/produto")]
        public async Task<ProdutoResponse> Produto(int id)
        {
            var produto = await repository.FindById(id);
            var map = mapper.Map<ProdutoResponse>(produto);

            return map;
        }

        [HttpPost("produto")]
        public async Task InserirProduto(ProdutoRequest produtoRequest)
        {
            var produto = mapper.Map<Produto>(produtoRequest);

            produto.Validate();

            await repository.Insert(produto);
            unitOfWork.Commit();
        }

        [HttpPut("produto")]
        public async Task AtualizarProduto(AtualizarProdutoRequest produtoRequest)
        {
            var produto = mapper.Map<Produto>(produtoRequest);

            produto.Validate();

            await repository.Update(produto);
            unitOfWork.Commit();
        }


        [HttpDelete("{id}/produto")]
        public  async Task RemoverProduto(int id)
        {
            await repository.Delete(id);
            unitOfWork.Commit();
        }



    }
}

