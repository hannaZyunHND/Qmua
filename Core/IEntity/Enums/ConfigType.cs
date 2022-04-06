using System;
using System.Collections.Generic;
using System.Text;

namespace MI.Entity.Enums
{
    public enum ConfigType : byte
    {
        [EnumDescription("Ảnh")]
        Image = 1,
        [EnumDescription("Text")]
        Text = 2,
        [EnumDescription("Ckeditor")]
        Ckeditor = 3,
        [EnumDescription("TextArea")]
        TextArea = 4,
        [EnumDescription("Number")]
        Number = 5,
        [EnumDescription("JsonDate")]
        JsonDate = 6,
    }
}
