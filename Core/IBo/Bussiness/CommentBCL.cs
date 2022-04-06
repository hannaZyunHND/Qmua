using MI.Dal.IDbContext;
using MI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MI.Bo.Bussiness
{
    public class CommentBCL : Base<Comment>
    {
        public CommentBCL()
        {

        }
        public List<Comment> Find(Utils.FilterComment filter, out int total)
        {
            total = 0;
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    IQueryable<Comment> items = _context.Set<Comment>();

                    if (!String.IsNullOrEmpty(filter.keyword))
                        items = items.Where(x => x.Id.ToString() == filter.keyword || x.Content.Contains(filter.keyword));

                    items = items.Where(x => x.ParentId == 0);

                    if (filter.status != Entity.Enums.StatusComment.All)
                    {
                        items = items.Where(x => x.Status == (int)filter.status);
                    }
                    if (filter.type != Entity.Enums.CommentType.All)
                    {
                        items = items.Where(x => x.ObjectType == ((int)filter.type).ToString());
                    }

                    total = items.Count();
                    return items.OrderByDescending(x => x.CreatedDate).Skip((filter.pageIndex - 1) * filter.pageSize).Take(filter.pageSize).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return new List<Comment>();
        }

        public List<Comment> GetByUrl()
        {
            return new List<Comment>();
        }
        public bool UpdateStatus(Comment comment)
        {
            using (IDbContext _context = new IDbContext())
            {

                _context.Comment.Attach(comment);
                var entry = _context.Entry(comment);
                entry.Property(x => x.Status).IsModified = true;
                _context.SaveChanges();

                return true;
            }
        }
        public List<Comment> ListCommentDashBoard()
        {
            List<Comment> lstData = new List<Comment>();
            try
            {
                using (IDbContext dbContext = new IDbContext())
                {
                    lstData = dbContext.Comment.Where(n => n.ParentId == 0 && n.ObjectType == "1" && n.Status == 1).OrderByDescending(n => n.CreatedDate).Take(10).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return lstData;
        }

    }
}
