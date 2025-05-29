using LojaDoSeuManoel.Api.Dtqs;
using LojaDoSeuManoel.Api.Entities;

namespace LojaDoSeuManoel.Api.Services
{
    public class BoxService
    {

        public readonly Dictionary<string, decimal[]> _box = new()
        {
            {"Box 1", new decimal[] {30.0m, 40.0m, 80.0m} },
            {"Box 2", new decimal[] {80.0m, 50.0m, 40.0m} },
            {"Box 3", new decimal[] {50.0m, 80.0m, 60.0m } },
        };

        public List<string> VerifyBox(ProductDtq product)
        {
            decimal[] productDimentions = { product.Height, product.Width, product.Length };
            var result = new List<string>();
            var permutartion = GeneratePermutation(productDimentions);

            foreach (var box in _box)
            {
                if(permutartion.Any(p => FitsInBox(p, box.Value))) result.Add(box.Key);
            }
            return result;
        }


        static bool FitsInBox(decimal[] product, decimal[] box)
        {
            return product[0] <= box[0] &&
                   product[1] <= box[1] &&
                   product[2] <= box[2];
        }
        public static List<decimal[]> GeneratePermutation(decimal[] dimensions)
        {
            var list = new List<decimal[]>();
            Permute(dimensions, 0, list);
            return list;
        }
        public static void Permute(decimal[] array, int k, List<decimal[]> resultado)
        {
            if (k == array.Length)
            {
                resultado.Add((decimal[])array.Clone());
            }
            else
            {
                for (int i = k; i < array.Length; i++)
                {   
                    Replace(ref array[k], ref array[i]);
                    Permute(array, k + 1, resultado);
                    Replace(ref array[k], ref array[i]);
                }
            }
        }

        public static void Replace(ref decimal a, ref decimal b)
        {
            (a,b) = (b,a);
        }
    }
}
