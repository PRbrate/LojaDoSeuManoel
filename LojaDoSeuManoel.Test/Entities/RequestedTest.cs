using LojaDoSeuManoel.Api.Entities;

namespace LojaDoSeuManoel.Test.Entities
{
    public class RequestedTest
        {
            [Fact]
            public void ConstructorStartListProduct()
            {
                //Arrange


                //act
                var resquested = new Requested();


                //assert
                Assert.NotNull(resquested.Products);
            }
    }
}
