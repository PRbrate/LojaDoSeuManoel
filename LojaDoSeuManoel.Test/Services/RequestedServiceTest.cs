using LojaDoSeuManoel.Api.Dtos;
using LojaDoSeuManoel.Api.Entities;
using LojaDoSeuManoel.Api.Repositories.Interfaces;
using LojaDoSeuManoel.Api.Services;
using LojaDoSeuManoel.Core;
using Moq;

namespace LojaDoSeuManoel.Test.Services
{
    public class RequestedServiceTest
    {
        private readonly Mock<IProductRepository> _productRepository = new Mock<IProductRepository>();
        private readonly Mock<BoxService> _boxService = new Mock<BoxService>();
        private readonly Mock<INotifier> _notifier = new Mock<INotifier>();
        private readonly RequestedService _requestedService;

        public RequestedServiceTest()
        {
            _requestedService = new RequestedService(
                _notifier.Object,
                _productRepository.Object,
                _boxService.Object
            );
        }

        [Fact]
        public async Task CreateRequestedWithValidProducts()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product { Id = productId, Width = 10, Height = 10, Length = 10, Name = "Product 1" };

            _productRepository
                .Setup(repo => repo.GetById(productId))
                .ReturnsAsync(product);

            _boxService
                .Setup(box => box.PackProducts(It.IsAny<List<Product>>()))
                .Returns((
                    new List<(string BoxName, List<Product> Products)>
                    {
                ("Box 1", new List<Product> { product })
                    },
                    1
                ));

            var dto = new RequestedDto
            {
                Products = new List<Guid> { productId }
            };

            // Act
            var result = await _requestedService.CreateRequested(new List<RequestedDto> { dto }, "user_test");

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].NumBox);
            Assert.Contains("Box 1", result[0].BoxNames);
            Assert.Equal("user_test", result[0].UserId);
        }

        [Fact]
        public async Task CreateRequested_ProductNotFound_NotifiesErrorAndReturnsEmpty()
        {
            // Arrange
            var fakeId = Guid.NewGuid();

            _productRepository
                .Setup(repo => repo.GetById(fakeId))
                .ReturnsAsync((Product)null!);

            var dto = new RequestedDto
            {
                Products = new List<Guid> { fakeId }
            };

            // Act
            var result = await _requestedService.CreateRequested(new List<RequestedDto> { dto }, "user_test");

            // Assert
            Assert.Empty(result);

            _notifier.Verify(n =>
                n.Handle(It.Is<Notification>(x => x.Message.Contains("não foram encontrados"))),
                Times.Once);
        }

        [Fact]
        public async Task CreateRequested_ProductFitsInNoBox_ReturnsNumBoxZero()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product { Id = productId, Width = 20, Height = 20, Length = 20, Name = "Product 2" };

            _productRepository
                .Setup(repo => repo.GetById(productId))
                .ReturnsAsync(product);

            _boxService
                .Setup(box => box.PackProducts(It.IsAny<List<Product>>()))
                .Returns((new List<(string BoxName, List<Product>)>(), 0));

            var dto = new RequestedDto
            {
                Products = new List<Guid> { productId }
            };

            // Act
            var result = await _requestedService.CreateRequested(new List<RequestedDto> { dto }, "user_test");

            // Assert
            Assert.Single(result);
            Assert.Equal(0, result[0].NumBox);
        }

    }
}
