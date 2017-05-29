using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace Final.Models
{
    [Table("tbl_BaiTap")]
    public class BaiTap
    {
        [Key]
        public int BaiTapId { get; set; }
        public string Ten { get; set; }
        public int BaiHocId { get; set; }

        [ForeignKey("BaiHocId")]
        public virtual BaiHoc BaiHoc { get; set; }

        public ICollection<HocSinhBaiTap> HocSinhBaiTaps { get; set; }
        public ICollection<CauHoi> CauHois { get; set; }

        public int Order { get; set; }
        [DefaultValue(0)]
        public int Hide { get; set; }
    }
}