using FluentValidation;

namespace LojaDoSeuManoel.Api.Entities.Validations
{
    public class ProductValidation: AbstractValidator<Product>
    {
        public ProductValidation()
        {
            RuleFor(p => p.Width)
                .NotNull()
                    .WithMessage("Campo obrigatório")
                .GreaterThan(0)
                    .WithMessage("O valor não pode ser 0");
            RuleFor(p => p.Height)
                .NotNull()
                    .WithMessage("Campo obrigatório")
                .GreaterThan(0)
                    .WithMessage("O valor não pode ser 0");
            RuleFor(p => p.Length)
                .NotNull()
                    .WithMessage("Campo obrigatório")
                .GreaterThan(0)
                    .WithMessage("O valor não pode ser 0");
            RuleFor(p => p.Name)
                .NotEqual("string")
                    .WithMessage("Deve ser passado um nome para o produto");
        }
    }
}
