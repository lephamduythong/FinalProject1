using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Final.Models
{
    [Table("tbl_HocSinhBaiTap")]
    public class HocSinhBaiTap
    {
        //[Key, Column(Order = 0)]
        public int HocSinhId { get; set; }
        //[Key, Column(Order = 1)]
        public int BaiTapId { get; set; }
        public int HoanThanh { get; set; }
        [ForeignKey("HocSinhId")]
        public virtual HocSinh HocSinh { get; set; }
        [ForeignKey("BaiTapId")]
        public virtual BaiTap BaiTap { get; set; }
    }
}