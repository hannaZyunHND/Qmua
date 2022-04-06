using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MI.ES;
using Nest;

namespace MI.ES.BCLES
{
    public class AutocompleteService : ConfigES
    {
        static readonly ElasticClient client = client_HeThong;
        public static async Task<bool> CreateIndexAsync()
        {
            var createIndexDescriptor = new CreateIndexDescriptor(strNameIndex_HeThong)
                .Mappings(ms => ms
                          .Map<ProductES>(m => m
                                .AutoMap()
                                .Properties(ps => ps
                                    .Completion(c => c
                                        .Name(p => p.Suggest))))

                         );

            if (client.IndexExists(strNameIndex_HeThong.ToLowerInvariant()).Exists)
            {
                client.DeleteIndex(strNameIndex_HeThong.ToLowerInvariant());
            }

            ICreateIndexResponse createIndexResponse = await client.CreateIndexAsync(createIndexDescriptor);

            return createIndexResponse.IsValid;
        }

        public static async Task IndexAsync(List<ProductES> products)
        {
            await client.IndexManyAsync(products, strNameIndex_HeThong);
        }

        public static ProductSuggestResponse SuggestAsync(string keyword, string lang, int page, int pageSize, out long totalRows)
        {

            totalRows = 0;
            keyword = keyword.Trim();

            List<ProductSuggest> lstObj = new List<ProductSuggest>();
            if (string.IsNullOrEmpty(lang))
            {
                lang = Utils.Utility.DefaultLang;
            }

            List<QueryContainer> mustQuery = new List<QueryContainer>();
            List<QueryContainer> mustNotQuery = new List<QueryContainer>();
            List<QueryContainer> souldQuery = new List<QueryContainer>();


            if (!String.IsNullOrEmpty(keyword))
            {
                //Thay đổi thêm dấu *
                mustQuery.Add(new QueryStringQuery()
                {
                    Fields = new string[] { "info.name", "info.name.keyword", "info.nameNorm", "info.nameNorm.keyword" },
                    Query = $"*{keyword}*"
                });
            }
            mustQuery.Add(new QueryStringQuery()
            {
                DefaultField = "info.lang.keyword",
                Query = $"\"{lang}\""
            });



            page = page < 1 ? 1 : page;


            SearchRequest req = new SearchRequest(strNameIndex_HeThong);
            req.Query = new QueryContainer(new BoolQuery() { Must = mustQuery });

            List<ISort> sort = new List<ISort>()
            {
                new SortField { Field = "_score", Order = SortOrder.Descending },
                new SortField { Field = "price", Order = SortOrder.Descending },
                new SortField { Field = "discountPrice", Order = SortOrder.Descending },
            };

            req.Sort = sort;
            req.From = (page - 1) * pageSize;
            req.Size = pageSize;
            var res = client.Search<ProductES>(req);
            if (res.IsValid)
            {
                totalRows = res.Total;
                var lst = res.Documents.ToList();

                if (lst.Count > 0)
                {
                    lstObj = lst.Select(x => new ProductSuggest
                    {
                        Id = x.Id,
                        Avatar = Utils.UIHelper.StoreFilePath(x.Avatar),
                        Name = x.Info.FirstOrDefault(d => d.Lang == lang).Name,
                        Url = "/" + x.Info.FirstOrDefault(d => d.Lang == lang).Url,
                        Price = Utils.UIHelper.FormatNumber2(x.Price),
                        DiscountPrice = Utils.UIHelper.FormatNumber2(x.DiscountPrice),
                        Unit = x.Unit,
                    }).ToList();
                }
            }
            return new ProductSuggestResponse
            {
                Suggests = lstObj
            };
            //if (string.IsNullOrEmpty(lang))
            //{
            //    lang = Utils.Utility.DefaultLang;
            //}

            //ISearchResponse<ProductES> searchResponse = await client.SearchAsync<ProductES>(s => s
            //                         .Index(strNameIndex_HeThong)
            //                         .Query(x => x.Term(d => d.Info.Select(a => a.Lang), lang)
            //                          && x.QueryString(d => d.DefaultField(u => u.Info.Select(a => a.Name)).Query($"\"{keyword}\"")))
            //                         .Suggest(su => su
            //                              .Completion("suggestions", c => c
            //                                   .Field(f => f.Suggest)
            //                                   .Prefix(keyword)
            //                                   .Fuzzy(f => f
            //                                       .Fuzziness(Fuzziness.Auto)
            //                                   )
            //                                   .Size(10))
            //                                 ));
            //var data1 = searchResponse.Suggest["suggestions"];

            //var suggests = from suggest in searchResponse.Suggest["suggestions"]
            //               from option in suggest.Options
            //               select new ProductSuggest
            //               {
            //                   Id = option.Source.Id,
            //                   Avatar = Utils.UIHelper.StoreFilePath(option.Source.Avatar),
            //                   Name = option.Source.Info.FirstOrDefault(x => x.Lang == lang).Name,
            //                   Url = "/" + option.Source.Info.FirstOrDefault(x => x.Lang == lang).Url,
            //                   Price = Utils.UIHelper.FormatNumber(option.Source.Price),
            //                   DiscountPrice = Utils.UIHelper.FormatNumber(option.Source.DiscountPrice),
            //                   Unit = option.Source.Unit,
            //                   Score = option.Score
            //               };
            //return new ProductSuggestResponse
            //{
            //    Suggests = suggests
            //};
        }
        public static ProductSuggestResponse SuggestAsyncAll(string keyword, string lang, out long totalRows)
        {

            totalRows = 0;
            List<ProductSuggest> lstObj = new List<ProductSuggest>();
            if (string.IsNullOrEmpty(lang))
            {
                lang = Utils.Utility.DefaultLang;
            }
            List<QueryContainer> mustQuery = new List<QueryContainer>();
            if (!String.IsNullOrEmpty(keyword))
            {
                mustQuery.Add(new QueryStringQuery()
                {
                    Fields = new string[] { "info.name", "info.name.keyword", "info.nameNorm", "info.nameNorm.keyword" },
                    Query = $"*{keyword}*"
                });
            }
            mustQuery.Add(new QueryStringQuery()
            {
                DefaultField = "info.lang.keyword",
                Query = $"\"{lang}\""
            });
            //page = page < 1 ? 1 : page;
            SearchRequest req = new SearchRequest(strNameIndex_HeThong);
            req.Query = new QueryContainer(new BoolQuery() { Must = mustQuery });

            List<ISort> sort = new List<ISort>()
            {
                new SortField { Field = "_score", Order = SortOrder.Descending },
                new SortField { Field = "price", Order = SortOrder.Descending },
                new SortField { Field = "discountPrice", Order = SortOrder.Descending },
            };

            req.Sort = sort;
            req.From = 0;
            req.Size = 3000;
            var res = client.Search<ProductES>(req);
            if (res.IsValid)
            {
                totalRows = res.Total;
                var lst = res.Documents.ToList();

                if (lst.Count > 0)
                {
                    lstObj = lst.Select(x => new ProductSuggest
                    {
                        Id = x.Id,
                        Avatar = Utils.UIHelper.StoreFilePath(x.Avatar),
                        Name = x.Info.FirstOrDefault(d => d.Lang == lang).Name,
                        Url = "/" + x.Info.FirstOrDefault(d => d.Lang == lang).Url,
                        Price = Utils.UIHelper.FormatNumber2(x.Price),
                        DiscountPrice = Utils.UIHelper.FormatNumber2(x.DiscountPrice),
                        Unit = x.Unit,
                        DiscountPercent = x.DiscountPercent
                    }).ToList();
                }
            }
            return new ProductSuggestResponse
            {
                Suggests = lstObj
            };
            //if (string.IsNullOrEmpty(lang))
            //{
            //    lang = Utils.Utility.DefaultLang;
            //}

            //ISearchResponse<ProductES> searchResponse = await client.SearchAsync<ProductES>(s => s
            //                         .Index(strNameIndex_HeThong)
            //                         .Query(x => x.Term(d => d.Info.Select(a => a.Lang), lang)
            //                          && x.QueryString(d => d.DefaultField(u => u.Info.Select(a => a.Name)).Query($"\"{keyword}\"")))
            //                         .Suggest(su => su
            //                              .Completion("suggestions", c => c
            //                                   .Field(f => f.Suggest)
            //                                   .Prefix(keyword)
            //                                   .Fuzzy(f => f
            //                                       .Fuzziness(Fuzziness.Auto)
            //                                   )
            //                                   .Size(10))
            //                                 ));
            //var data1 = searchResponse.Suggest["suggestions"];

            //var suggests = from suggest in searchResponse.Suggest["suggestions"]
            //               from option in suggest.Options
            //               select new ProductSuggest
            //               {
            //                   Id = option.Source.Id,
            //                   Avatar = Utils.UIHelper.StoreFilePath(option.Source.Avatar),
            //                   Name = option.Source.Info.FirstOrDefault(x => x.Lang == lang).Name,
            //                   Url = "/" + option.Source.Info.FirstOrDefault(x => x.Lang == lang).Url,
            //                   Price = Utils.UIHelper.FormatNumber(option.Source.Price),
            //                   DiscountPrice = Utils.UIHelper.FormatNumber(option.Source.DiscountPrice),
            //                   Unit = option.Source.Unit,
            //                   Score = option.Score
            //               };
            //return new ProductSuggestResponse
            //{
            //    Suggests = suggests
            //};
        }

        public static ProductSuggestResponse SuggestEnterBuyAsync(string keyword, string lang, int page, int pageSize, out long totalRows, int ManufacturerId = 0, List<string> lstKey = null)
        {
            totalRows = 0;
            keyword = keyword.Trim();
            List<ProductSuggest> lstObj = new List<ProductSuggest>();
            if (string.IsNullOrEmpty(lang))
            {
                lang = Utils.Utility.DefaultLang;
            }
            List<QueryContainer> mustQuery = new List<QueryContainer>();
            if (!String.IsNullOrEmpty(keyword))
            {
                mustQuery.Add(new QueryStringQuery()
                {
                    Fields = new string[] { "info.name", "info.name.keyword", "info.nameNorm", "info.nameNorm.keyword" },
                    Query = $"*{keyword}*"
                });
            }
            mustQuery.Add(new QueryStringQuery()
            {
                DefaultField = "info.lang.keyword",
                Query = $"\"{lang}\""
            });
            if (ManufacturerId > 0)
            {
                mustQuery.Add(new TermQuery()
                {
                    Field = "manufacturerId",
                    Value = ManufacturerId
                });
            }
            if (lstKey != null && lstKey.Count > 0)
            {
                mustQuery.Add(new TermsQuery()
                {
                    Field = "spectifications.keyword",
                    Terms = lstKey.Select(x => (object)x).ToList()
                });
            }
            page = page < 1 ? 1 : page;
            SearchRequest req = new SearchRequest(strNameIndex_HeThong);
            req.Query = new QueryContainer(new BoolQuery() { Must = mustQuery });

            List<ISort> sort = new List<ISort>()
            {
                new SortField { Field = "_score", Order = SortOrder.Descending },
                new SortField { Field = "price", Order = SortOrder.Descending },
                new SortField { Field = "discountPrice", Order = SortOrder.Descending },
            };

            req.Sort = sort;
            req.From = (page - 1) * pageSize;
            req.Size = pageSize;
            var res = client.Search<ProductES>(req);
            if (res.IsValid)
            {
                totalRows = res.Total;
                var lst = res.Documents.ToList();

                if (lst.Count > 0)
                {
                    lstObj = lst.Select(x => new ProductSuggest
                    {
                        Id = x.Id,
                        Avatar = Utils.UIHelper.StoreFilePath(x.Avatar),
                        Name = x.Info.FirstOrDefault(d => d.Lang == lang).Name,
                        Url = Utils.BaseBA.UrlProduct("", x.Info.FirstOrDefault(d => d.Lang == lang).Url),
                        Price = Utils.UIHelper.FormatNumber2(x.Price),
                        DiscountPrice = Utils.UIHelper.FormatNumber2(x.DiscountPrice),
                        Unit = x.Unit,
                        Evaluate = x.Evaluate,
                        Rate = x.Rate,
                        Spectificat = x.Spectifications

                    }).ToList();
                }
            }
            return new ProductSuggestResponse
            {
                Suggests = lstObj
            };
        }
    }
}