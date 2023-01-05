using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ProductManager.Api.DTOs;
using ProductManager.Domain.Commons.Helpers;
using ProductManager.Domain.Entities;
using static ProductManager.Api.AutoMapper.ConfigProfile;

namespace ProductManager.Api.AutoMapper
{
    public class ConfigProfile : Profile
    {
        public ConfigProfile()
        {
            CreateMap<ProdutoResponse, Produto> ()
                .ForPath(p => p.Provider.Description, opt => opt.MapFrom(src => src.Provider))
                .ForPath(p => p.Provider.CNPJ.Value, opt => opt.MapFrom(src => src.ProviderCNPJ))
                .ReverseMap();

            CreateMap<Produto, ProdutoRequest>().ReverseMap();
            CreateMap<Produto, AtualizarProdutoRequest>().ReverseMap();

            CreateMap<PaginatedList<Produto>, PaginatedList<ProdutoResponse>>()
                .ConvertUsing(new PaginatedListProducctTypeConverter());          


            CreateMap<FornecedorResponse, Fornecedor>()
                .ReverseMap();

            CreateMap<FornecedorResponse, Fornecedor>()
                .ForPath(p => p.CNPJ.Value, opt => opt.MapFrom(src => src.CNPJ))
                .ReverseMap();

            CreateMap<Fornecedor, FornecedorRequest>().ReverseMap();
            CreateMap<Fornecedor, AtualizarFornecedorRequest>().ReverseMap();

            CreateMap<PaginatedList<Fornecedor>, PaginatedList<FornecedorResponse>>()
                .ConvertUsing(new PaginatedListProviderTypeConverter());
        }



        public class PaginatedListProducctTypeConverter : ITypeConverter<PaginatedList<Produto>, PaginatedList<ProdutoResponse>>
        {
            public PaginatedList<ProdutoResponse> Convert(PaginatedList<Produto> source, PaginatedList<ProdutoResponse> destination, ResolutionContext context)
            {
                var res =  new PaginatedList<ProdutoResponse>(source.Items.Select(
                    p => new ProdutoResponse()
                        {
                            Description = p.Description,
                            ExpirationDate = p.ExpirationDate,
                            Id = p.Id,
                            IsActive = p.IsActive,
                            ManufacturingDate = p.ManufacturingDate,
                            ProviderId = p.ProviderId,
                            Provider = p.Provider.Description,
                            ProviderCNPJ = p.Provider.CNPJ.Value,
                        })
                    .ToList(),
                    source.PageIndex,
                    source.TotalCount,
                    source.TotalPages
                  );

                return res;
            }
        }


        public class PaginatedListProviderTypeConverter : ITypeConverter<PaginatedList<Fornecedor>, PaginatedList<FornecedorResponse>>
        {

            public PaginatedList<FornecedorResponse> Convert(PaginatedList<Fornecedor> source, PaginatedList<FornecedorResponse> destination, ResolutionContext context)
            {
                var res = new PaginatedList<FornecedorResponse>(source.Items.Select(
                    p => new FornecedorResponse()
                    {
                        Description = p.Description,
                        CNPJ = p.CNPJ.Value,
                        Id = p.Id,
                    })
                    .ToList(),
                    source.PageIndex,
                    source.TotalCount,
                    source.TotalPages
                  );

                return res;
            }
        }

    }
}

