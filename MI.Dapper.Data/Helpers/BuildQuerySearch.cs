using System.Text;
using Utils;

namespace MI.Dapper.Data.Helpers
{
    public static class BuildQuerySearch
    {
        private static string BuildQuerySearchProduct(FilterArticle article)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(" 1 = 1 ");
            if (!string.IsNullOrEmpty(article.keyword))
            {
                stringBuilder.Append(" and");
                stringBuilder.Append("(");
                stringBuilder.Append("a.Name like N");
                stringBuilder.Append("'%");
                stringBuilder.Append(article.keyword);
                stringBuilder.Append("%");
                stringBuilder.Append("'");
                stringBuilder.Append(")");
            }

            if (!string.IsNullOrEmpty(article.ZoneId.ToString()))
            {
                stringBuilder.Append(" and");
                stringBuilder.Append(" aiz.zoneid in");
                stringBuilder.Append("(");
                stringBuilder.Append("select cast(String as int) as ZoneId from ufn_CSVToTable(");
                stringBuilder.Append("'");
                stringBuilder.Append(article.ZoneId.ToString());
                stringBuilder.Append("'");
                stringBuilder.Append(",");
                stringBuilder.Append(" ','");
                stringBuilder.Append(")");
                stringBuilder.Append(")");
            }

            if (article.Status > 0)
            {
                stringBuilder.Append(" and");
                stringBuilder.Append(" a.Status =");
                stringBuilder.Append(article.Status);
            }

            if (article.Type > 0)
            {
                stringBuilder.Append(" and");
                stringBuilder.Append(" a.Type =");
                stringBuilder.Append(article.Type);
            }

            var result = stringBuilder.ToString();
            return result;
        }

        public static string CountTotalAndGetSearchRecord(FilterArticle article)
        {
            var buildQuerySearchProduct = BuildQuerySearchProduct(article);
            StringBuilder builder = new StringBuilder();

            builder.Append(
                "SELECT COUNT(*) FROM Article a INNER JOIN ArticlesInZone AIZ ON a.Id = AIZ.ArticleId INNER JOIN Zone z ON AIZ.ZoneId = z.Id where");
            builder.Append(buildQuerySearchProduct);
            builder.Append(
                " SELECT a.Id ,  a.Name , a.Status , a.CreatedDate , a.CreatedBy , (SELECT    COUNT(1) FROM ArticleInLanguage lang WHERE     lang.ArticleId = a.Id ) AS CountLanguage FROM Article a INNER JOIN ArticlesInZone AIZ ON a.Id = AIZ.ArticleId where");
            builder.Append(buildQuerySearchProduct);
            builder.Append(" order by a.CreatedDate desc");
            builder.Append(" offset ");
            builder.Append(article.pageIndex - 1);
            builder.Append("*");
            builder.Append(article.pageSize);
            builder.Append(" rows");
            builder.Append(" fetch next ");
            builder.Append(article.pageSize);
            builder.Append(" row only");
            var result = builder.ToString();
            return result;
        }
    }
}