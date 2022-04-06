using MI.Bo.Bussiness;
using MI.ES;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MI.Service
{
    public class SyncProductToES
    {
        public static void Run()
        {
            ProductBCL productBCL = new ProductBCL();
            ZoneBCL categoryBCL = new ZoneBCL();
            ArticleBCL articleBCL = new ArticleBCL();
            ProductSpecificationsBCL specificationsBCL = new ProductSpecificationsBCL();
            ProductPriceInLocationBCL productInPriceBCL = new ProductPriceInLocationBCL();
            RatingBCL rateBCL = new RatingBCL();
            LocationBCL localtionBCL = new LocationBCL();
            var product = productBCL.FindAll(x => x.Status == 1, x => x.ProductInLanguage);

            //var Special = specificationsBCL.FindAll(x => x.IsShowLayout == true && product.Select(d => d.Id).Contains(x.ProductId ?? 0)).GroupBy(x => x.ProductId).ToDictionary(x => x.Key, x => x.Select(d => d.Value));

            //var Prices = productInPriceBCL.FindAll(x => product.Select(d => d.Id).Contains(x.ProductId)).GroupBy(x => x.ProductId).ToDictionary(x => x.Key, x => x.Select(a => new PriceES() { LocationId = a.LocationId, Price = a.Price ?? 0, SalePrice = a.SalePrice ?? 0 }));

            //var Rating = rateBCL.FindAll(x => x.ObjectId > 0 && product.Select(d => d.Id).Contains(x.ObjectId ?? 0) && x.Type == (int)MI.Entity.Enums.TypeRating.Product).GroupBy(x => x.ObjectId.Value).ToDictionary(x => x.Key, x => new KeyValuePair<decimal, int>(Math.Round(x.Sum(a => a.Rate ?? 0) / x.Count(), 2), x.Count()));

            //var Location = localtionBCL.GetAll();

            var products = product.Select(x => new ProductES
            {
                Id = x.Id,
                Code = x.Code,
                Avatar = x.Avatar,
                Price = x.Price ?? 0,
                DiscountPrice = x.DiscountPrice ?? 0,
                Unit = String.IsNullOrEmpty(x.Unit) ? "/m2" : "/" + x.Unit,
                Info = x.ProductInLanguage.Select(d => new InfoES { Lang = d.LanguageCode.Trim(), NameNorm = Utils.Utility.bodauTiengViet(d.Title.ToLower()), Name = d.Title.ToLower(), Url = d.Url + ".html" }).ToList(),
                //Spectifications = Special.ContainsKey(x.Id) ? Special[x.Id].FirstOrDefault() : "",
                //ManufacturerId = x.ManufacturerId ?? 0,
                //Prices = BuildListPriceByLocation(x, Location, Prices.ContainsKey(x.Id) ? Prices[x.Id].ToDictionary(d => d.LocationId, d => d) : new Dictionary<int, PriceES>()),
                //Rate = Rating.ContainsKey(x.Id) ? (double)Rating[x.Id].Key : 0,
                //Evaluate = Rating.ContainsKey(x.Id) ? (double)Rating[x.Id].Value : 0,

                //Suggest = new CompletionField()
                //{
                //    Input = Utils.Utility.SplitStr(x.ProductInLanguage.Select(a => a.Title).ToList())
                //}
            }).ToList();
            bool isCreated = MI.ES.BCLES.AutocompleteService.CreateIndexAsync().Result;

            if (isCreated)
            {
                var data = MI.ES.BCLES.AutocompleteService.IndexAsync(products);
            }

        }
        public static void RunEnterBuy()
        {
            ProductBCL productBCL = new ProductBCL();
            ZoneBCL categoryBCL = new ZoneBCL();
            ArticleBCL articleBCL = new ArticleBCL();
            ProductSpecificationsBCL specificationsPrBCL = new ProductSpecificationsBCL();
            SpecificationsBCL specificationsBCL = new SpecificationsBCL();
            ProductPriceInLocationBCL productInPriceBCL = new ProductPriceInLocationBCL();
            RatingBCL rateBCL = new RatingBCL();
            LocationBCL localtionBCL = new LocationBCL();
            var product = productBCL.FindAll(x => x.Status == 1, x => x.ProductInLanguage);

            var Prices = productInPriceBCL.FindAll(x => product.Select(d => d.Id).Contains(x.ProductId)).GroupBy(x => x.ProductId).ToDictionary(x => x.Key, x => x.Select(a => new PriceES() { LocationId = a.LocationId, Price = a.Price ?? 0, SalePrice = a.SalePrice ?? 0 }));

            var Rating = rateBCL.FindAll(x => x.ObjectId > 0 && product.Select(d => d.Id).Contains(x.ObjectId ?? 0) && x.Type == (int)MI.Entity.Enums.TypeRating.Product).GroupBy(x => x.ObjectId.Value).ToDictionary(x => x.Key, x => new KeyValuePair<decimal, int>(Math.Round(x.Sum(a => a.Rate ?? 0) / x.Count(), 2), x.Count()));

            var Location = localtionBCL.FindAll();

            var products = product.Select(x => new ProductES
            {
                Id = x.Id,
                Code = x.Code,
                Avatar = x.Avatar,
                Price = x.Price ?? 0,
                DiscountPrice = x.DiscountPrice ?? 0,
                Unit = String.IsNullOrEmpty(x.Unit) ? "/Chiếc" : "/" + x.Unit,
                Info = x.ProductInLanguage.Select(d => new InfoES { Lang = d.LanguageCode.Trim(), NameNorm = Utils.Utility.bodauTiengViet(d.Title.ToLower()), Name = d.Title.ToLower(), Url = d.Url + ".html" }).ToList(),
                ManufacturerId = x.ManufacturerId ?? 0,
                Prices = BuildListPriceByLocation(x, Location, Prices.ContainsKey(x.Id) ? Prices[x.Id].ToDictionary(d => d.LocationId, d => d) : new Dictionary<int, PriceES>()),
                Rate = Rating.ContainsKey(x.Id) ? (double)Rating[x.Id].Key : 5,
                Evaluate = Rating.ContainsKey(x.Id) ? (double)Rating[x.Id].Value : 0,
                Properties = x.PropertyId,
                DiscountPercent = x.DiscountPercent
            }).ToList();
            bool isCreated = MI.ES.BCLES.AutocompleteService.CreateIndexAsync().Result;

            if (isCreated)
            {
                var data = MI.ES.BCLES.AutocompleteService.IndexAsync(products);
            }

        }
        public static List<PriceES> BuildListPriceByLocation(Entity.Models.Product product, List<Entity.Models.Location> lstLocation, Dictionary<int, PriceES> dicPrices)
        {
            List<PriceES> lstPriceInLocation = new List<PriceES>();
            foreach (var item in lstLocation)
            {
                if (dicPrices.ContainsKey(item.Id))
                {
                    lstPriceInLocation.Add(dicPrices[item.Id]);
                }
                else
                {
                    lstPriceInLocation.Add(new PriceES() { LocationId = item.Id, Price = product.Price ?? 0, SalePrice = product.DiscountPrice ?? 0 });
                }
            }
            return new List<PriceES>();
        }
    }
}
