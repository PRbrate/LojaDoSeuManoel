using LojaDoSeuManoel.Api.Entities;
using LojaDoSeuManoel.Api.Services;

namespace LojaDoSeuManoel.Test.Services
{
    public class BoxServiceTest
    {
        private readonly BoxService _boxService = new BoxService();


        [Fact]
        public void VerifyBoxProductFitsInMultipleBoxes()
        {
            //Arrange
            var product = new Product { Name = "Teste", Height = 30, Width = 30, Length = 40 };

            //act
            var result = _boxService.VerifyBox(product);

            //assert
            Assert.Contains("Box 1", result);
            Assert.Contains("Box 2", result);
            Assert.Contains("Box 3", result);
        }


        [Fact]
        public void CanFitProductsInBoxAllProductsFit()
        {
            //arrange
            var box = new decimal[] { 80, 50, 40 };
            var products = new List<Product>
            {
                new Product {Name="Product1", Height = 30, Width = 30, Length = 40 },
                new Product {Name="Product2", Height = 40, Width = 30, Length = 40 },
            };

            //act
            bool result = _boxService.CanFitProductsInBox(products, box);

            //assert
            Assert.True(result);
        }

        [Fact]
        public void PackProducts_MultipleProducts_ReturnsCorrectBoxCount()
        {
            //arrange

            var products = new List<Product>
            {
                new Product {Name="Product1", Height = 30, Width = 30, Length = 40 },
                new Product {Name="Product2", Height = 40, Width = 30, Length = 40 },
                new Product {Name="Product3", Height = 50, Width = 50, Length = 60 },
            };

            //act
            var (boxes, count) = _boxService.PackProducts(products);


            //assert
            Assert.Equal(3, count);
            Assert.Contains(boxes, b => b.BoxName == "Box 1" || b.BoxName == "Box 2");
            Assert.Contains(boxes, b => b.BoxName == "Box 3");
        }

    }
}
