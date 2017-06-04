using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Final.Models
{
    [Table("tbl_BaiHoc")]
    public class BaiHoc
    {
        [Key]
        public int BaiHocId { get; set; }
        public string Ten { get; set; }
        public int ChuongId { get; set; }
        [ForeignKey("ChuongId")]
        public virtual Chuong Chuong { get; set; }
        public string NoiDung { get; set; }

        public virtual ICollection<BinhLuan> BinhLuans { get; set; }
        public virtual ICollection<HocSinhBaiHoc> HocSinhBaiHocs { get; set; }
        public virtual ICollection<BaiTap> BaiTaps { get; set; }

        public int Order { get; set; }
        [DefaultValue(0)]
        public int Hide { get; set; }
    }
}