using System.Text.Json.Serialization;

namespace LojaDoSeuManoel.Api.Dtos
{
    public class RequestedDto
    {
        public ICollection<Guid> Products { get; set; }
    }
}
