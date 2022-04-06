using System.Collections.Generic;
using MI.Dapper.Data.ViewModels;

namespace MI.Dapper.Data.Constant
{
    public static class ListConstant
    {
        public static readonly List<StatusViewModel> ListStatusViewModel = new List<StatusViewModel>()
        {
            new StatusViewModel()
            {
                Id = 0,
                Label = "Tất cả"
            },
            new StatusViewModel()
            {
                Id = 1,
                Label = "Chưa xuất bản"
            },
            new StatusViewModel()
            {
                Id = 2,
                Label = "Đã xuất bản"
            }
        };

        public static readonly List<TypeViewModel> ListTypeViewModel = new List<TypeViewModel>()
        {
            new TypeViewModel()
            {
                Id = 0,
                Label = "Chọn loại bài viết"
            },
            new TypeViewModel()
            {
                Id = 0,
                Label = "Tất cả"
            },
            new TypeViewModel()
            {
                Id = 1,
                Label = "Bài viết sản phẩm"
            },
            new TypeViewModel()
            {
                Id = 2,
                Label = "Bài viết tuyển dụng"
            }
        };
    }
}