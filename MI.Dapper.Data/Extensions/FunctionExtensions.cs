using System;
using System.Collections.Generic;
using System.Linq;
using MI.Dapper.Data.ViewModels;

namespace MI.Dapper.Data.Extensions
{
    public static class FunctionExtensions
    {
        public static IList<FunctionPermissionViewModel> BuildTree(this IEnumerable<FunctionPermissionViewModel> source)
        {
            try
            {
                var groups = source.GroupBy(i => i.ParentId);
                var enumerable = groups as IGrouping<string, FunctionPermissionViewModel>[] ?? groups.ToArray();
                var roots = enumerable.FirstOrDefault(g => g.Key == null).ToList();
                if (roots.Count > 0)
                {
                    var dict = enumerable.Where(g => g.Key != null).ToDictionary(g => g.Key, g => g.ToList());
                    for (int i = 0; i < roots.Count; i++)
                        AddChildren(roots[i], dict);
                }

                return roots;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private static void AddChildren(FunctionPermissionViewModel node,
            IDictionary<string, List<FunctionPermissionViewModel>> source)
        {
            if (source.ContainsKey(node.Id))
            {
                node.Children = source[node.Id];
                for (int i = 0; i < node.Children.Count; i++)
                    AddChildren(node.Children[i], source);
            }
            else
            {
                node.Children = null;
            }
        }
    }
}