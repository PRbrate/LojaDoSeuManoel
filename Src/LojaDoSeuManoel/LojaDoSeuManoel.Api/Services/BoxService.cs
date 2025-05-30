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

        public virtual List<string> VerifyBox(Product product)
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


        public bool CanFitProductsInBox(List<Product> products, decimal[] boxDimensions)
        {

            decimal totalHeight = 0m;
            foreach (var p in products)
            {
                var permutations = GeneratePermutation(new decimal[] { p.Height, p.Width, p.Length });
                bool fitsInBase = permutations.Any(permutation =>
                    permutation[1] <= boxDimensions[1] && 
                    permutation[2] <= boxDimensions[2]);  

                if (!fitsInBase) return false; 

                totalHeight += p.Height;
            }
            return totalHeight <= boxDimensions[0]; 
        }

        public virtual (List<(string BoxName, List<Product> Products)> Boxes, int Count) PackProducts(ICollection<Product> products)
        {
            var boxes = new List<(string BoxName, List<Product> Products)>();

            foreach (var product in products)
            {
                bool placed = false;

                for (int i = 0; i < boxes.Count; i++)
                {
                    var box = boxes[i];
                    var newProductsList = box.Products.Concat(new[] { product }).ToList();
                    if (CanFitProductsInBox(newProductsList, _box[box.BoxName]))
                    {
                        box.Products.Add(product);
                        boxes[i] = (box.BoxName, box.Products);
                        placed = true;
                        break;
                    }
                }

                if (!placed)
                {
                    var possibleBoxes = VerifyBox(product);
                    if (possibleBoxes.Any())
                    {
                        boxes.Add((possibleBoxes.First(), new List<Product> { product }));
                    }
                }
            }
            return (boxes, boxes.Count);
        }



    }
}
