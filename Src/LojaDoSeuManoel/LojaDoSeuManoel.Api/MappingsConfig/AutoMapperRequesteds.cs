using Azure.Core;
using LojaDoSeuManoel.Api.Dtos;
using LojaDoSeuManoel.Api.Entities;

namespace LojaDoSeuManoel.Api.MappingsConfig
{
    public static class AutoMapperRequesteds
    {
        public static Requested Map(this RequestedDto requestedDto) => new Requested
        {
            Id = Guid.NewGuid(),
            Products = new List<Product>() 
        };

        public static Requested Map(this RequestedResponseDto requestedDto) => new Requested
        {
            Id = new Guid(),
            UserId = requestedDto.UserId,
            Products = requestedDto.Products.Select(p => p.Map()).ToList()


        };

        public static RequestedResponseDto Map(this Requested requested) => new RequestedResponseDto
        {
            Id = requested.Id,
            UserId = requested.UserId,
            Products = requested.Products.Select(p => p.Map()).ToList()

        };


    }
}
