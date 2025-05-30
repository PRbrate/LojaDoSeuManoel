using LojaDoSeuManoel.Api.Entities;
using System.Collections.ObjectModel;

namespace LojaDoSeuManoel.Api.Dtos
{
    public class RequestedResponseDto
    {

        public Guid Id { get; set; }
        public string UserId { get; set; }
        public ICollection<ProductDto> Products { get; set; }
        public int NumBox { get; set; }
        public List<string> BoxNames { get; set; }
    }
}
