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
    public class FornecedorController : ControllerBase
    {
        public IProductRepository repository { get; }
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public FornecedorController(IProductRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }


        [HttpGet("fornecedores")]
        public async Task<PaginatedList<FornecedorResponse>> Fornecedores([FromQuery] PaginatedFilter filter)
        {
            var fornecedores = await repository.FindAllProviders(filter);

            var mapOfProviders = mapper.Map<PaginatedList<FornecedorResponse>>(fornecedores);

        
            return mapOfProviders;
        }

        [HttpGet("{id}/fornecedor")]
        public async Task<FornecedorResponse> Fornecedor(int id)
        {
            var fornecedor = await repository.FindProviderById(id);
            var map = mapper.Map<FornecedorResponse>(fornecedor);
            return map;
        }

        [HttpPost("fornecedor")]
        public async Task InserirFornecedor(FornecedorRequest fornecedorRequest)
        {
            var fornecedor = mapper.Map<Fornecedor>(fornecedorRequest);
            fornecedor.Validate();

            await repository.InsertProvider(fornecedor);

            unitOfWork.Commit();
        }

        [HttpPut("fornecedor")]
        public async Task AtualizarFornecedor(AtualizarFornecedorRequest fornecedorRequest)
        {
            var fornecedor = mapper.Map<Fornecedor>(fornecedorRequest);
            fornecedor.Validate();

            await repository.UpdateProvider(mapper.Map<Fornecedor>(fornecedor));
            unitOfWork.Commit();
        }

        [HttpDelete("{id}/fornecedor")]
        public async Task RemoverFornecedor(int id)
        {
            await repository.DeleteProvider(id);
            unitOfWork.Commit();
        }
    }
}

