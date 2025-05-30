using LojaDoSeuManoel.Api.Dtos;
using LojaDoSeuManoel.Api.Dtqs;
using LojaDoSeuManoel.Api.Entities;

namespace LojaDoSeuManoel.Api.Services.Interfaces
{
    public interface IRequestedService
    {
        Task<List<RequestedResponseDto>> CreateRequested(ICollection<RequestedDto> requestedDto, string userId);
    }
}
