using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Final.Models
{
    [Table("tbl_CauHoi")]
    public class CauHoi
    {
        [Key]
        public int CauHoiId { get; set; }
        public string NoiDung { get; set; }
        public string CauTraLoi1 { get; set; }
        public string CauTraLoi2 { get; set; }
        public string CauTraLoi3 { get; set; }
        public string CauTraLoi4 { get; set; }
        public int CauDung { get; set; }
        public int BaiTapId { get; set; }
        [ForeignKey("BaiTapId")]
        public virtual BaiTap BaiTap { get; set; }

        public int Order { get; set; }
        [DefaultValue(0)]
        public int Hide { get; set; }
    }
}