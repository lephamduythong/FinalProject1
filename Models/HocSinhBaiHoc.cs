using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Final.Models
{
    [Table("tbl_HocSinhBaiHoc")]
    public class HocSinhBaiHoc
    {
        // [Key,Column(Order = 0)]
        public int HocSinhId { get; set; }
        // [Key, Column(Order = 1)]
        public int BaiHocId { get; set; }
        public int HoanThanh { get; set; }

        [ForeignKey("HocSinhId")]
        public virtual HocSinh HocSinh { get; set; }
        [ForeignKey("BaiHocId")]
        public virtual BaiHoc BaiHoc { get; set; }

    }
}