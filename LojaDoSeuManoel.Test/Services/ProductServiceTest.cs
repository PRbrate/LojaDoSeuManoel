using LojaDoSeuManoel.Api.Dtqs;
using LojaDoSeuManoel.Api.Entities;
using LojaDoSeuManoel.Api.Repositories.Interfaces;
using LojaDoSeuManoel.Api.Services;
using LojaDoSeuManoel.Core;
using Moq;

namespace LojaDoSeuManoel.Test.Services
{
    public class ProductServiceTest
    {
        private readonly Mock<IProductRepository> _productRepository = new Mock<IProductRepository>();
        private readonly Mock<INotifier> _notifier = new Mock<INotifier>();
        private readonly Mock<BoxService> _boxService = new Mock<BoxService>();

        private ProductService CreateService() => new ProductService(_productRepository.Object, _notifier.Object, _boxService.Object);

        [Fact]
        public async Task CreateProductWithInvalidDimentions()
        {
            //Arrange
            var service = CreateService();
            var productDto = new ProductDtq { Name = "Test", Width = 100, Length = 50, Height = 40 };

            //act
            var result = await service.CreateProduct(productDto);

            //assert
            Assert.False(result);
            _notifier.Verify(n => n.Handle(It.Is<Notification>(m => m.Message == "Nenhuma das dimenções pode ser maior que 80")), Times.Once);
        }


        [Fact]
        public async Task CreateProductNotFittingInAnyBox()
        {
            //Arrange
            var service = CreateService();
            var productDto = new ProductDtq { Name = "Test", Width = 30, Length = 30, Height = 30 };
            _boxService.Setup(b => b.VerifyBox(It.IsAny<Product>())).Returns(new List<string>());

            //act
            var result = await service.CreateProduct(productDto);

            //assert
            Assert.False(result);
            _notifier.Verify(n => n.Handle(It.Is<Notification>(m => m.Message == "Não é possivel cadastrar esse produto, pois não caberá em nenhuma caixa")), Times.Once);
        }

        [Fact]
        public async Task CreateProductValid()
        {
            //Arrange
            var service = CreateService();
            var productDto = new ProductDtq { Name = "Test", Width = 30, Length = 30, Height = 30 };
            _boxService.Setup(b => b.VerifyBox(It.IsAny<Product>())).Returns(new List<string>() { "Box 1" });
            _productRepository.Setup(r => r.Create(It.IsAny<Product>())).ReturnsAsync(true);

            //act
            var result = await service.CreateProduct(productDto);

            //assert
            Assert.True(result);
            _notifier.Verify();
        }
        [Fact]
        public async Task DeleteProductProductNotFound()
        {
            //Arrange
            var service = CreateService();
            _productRepository.Setup(r => r.GetById(It.IsAny<Guid>())).ReturnsAsync((Product)null);

            //act
            var result = await service.DeleteProduct(Guid.NewGuid());

            //assert
            Assert.False(result);
            _notifier.Verify(n => n.Handle(It.Is<Notification>(m => m.Message == "Não Foi encontrado Produto com o id passado")), Times.Once);
        }


        [Fact]
        public async Task GetAllProductsReturnList()
        {
            //Arrange
            var service = CreateService();
            _productRepository.Setup(r => r.GetProducts()).ReturnsAsync(new List<Product> { new() });

            //act
            var result = await service.GetAllProducts();

            //assert
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetProductRetunNotFound()
        {
            //Arrange
            var service = CreateService();
            _productRepository.Setup(r => r.GetById(It.IsAny<Guid>())).ReturnsAsync((Product)null);

            //act
            var result = await service.GetProduct(Guid.NewGuid());

            //assert
            Assert.Null(result);
            _notifier.Verify(n => n.Handle(It.Is<Notification>(m => m.Message == "Não Foi encontrado Produto com o id passado")), Times.Once);
        }

        [Fact]
        public async Task UpdateProductProductNotFound()
        {
            //Arrange
            var service = CreateService();
            _productRepository.Setup(r => r.GetById(It.IsAny<Guid>())).ReturnsAsync((Product)null);

            //act
            var result = await service.UpdateProduct(Guid.NewGuid(), new ProductDtq());

            //assert
            Assert.False(result);
            _notifier.Verify(n => n.Handle(It.Is<Notification>(m => m.Message == "Não foi encontrado Produto com o Id informado para a atualização")), Times.Once);
        }

        [Fact]
        public async Task UpdateProductValidUpdate()
        {
            var service = CreateService();
            var existingProduct = new Product { Width = 10, Height = 10, Length = 10, Name = "Teste" };
            _productRepository.Setup(r => r.GetById(It.IsAny<Guid>())).ReturnsAsync(existingProduct);
            _boxService.Setup(b => b.VerifyBox(It.IsAny<Product>())).Returns(new List<string> { "Box 1" });
            _productRepository.Setup(r => r.Update(It.IsAny<Product>())).ReturnsAsync(true);

            var result = await service.UpdateProduct(Guid.NewGuid(), new ProductDtq
            {
                Width = 20,
                Length = 20,
                Height = 20,
                Name = "Novo"
            });

            Assert.True(result);
            _productRepository.Verify(r => r.Update(It.Is<Product>(p => p.Width == 20 && p.Name == "Novo")), Times.Once);
        }
    }
}
