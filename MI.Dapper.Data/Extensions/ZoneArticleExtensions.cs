using System.Collections.Generic;
using System.Linq;
using MI.Dapper.Data.Models;

namespace MI.Dapper.Data.Extensions
{
    public static class ZoneArticleExtensions
    {
        public static IList<Group> BuildTree(this IEnumerable<Group> source)
        {
            var groups = source.GroupBy(i => i.ParentId);
            var enumerable = groups as IGrouping<int?, Group>[] ?? groups.ToArray();
            var roots = enumerable.FirstOrDefault(g => g.Key == 0).ToList();
            if (roots.Count > 0)
            {
                var dict = enumerable.Where(g => g.Key.HasValue).ToDictionary(g => g.Key.Value, g => g.ToList());
                for (int i = 0; i < roots.Count; i++)
                    AddChildren(roots[i], dict);
            }

            return roots;
        }

        private static void AddChildren(Group node, IDictionary<int, List<Group>> source)
        {
            if (source.ContainsKey(node.Id))
            {
                node.Children = source[node.Id];
                for (int i = 0; i < node.Children.Count; i++)
                    AddChildren(node.Children[i], source);
            }
            else
            {
                node.Children = new List<Group>();
            }
        }
    }
}