using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Final.Models
{
    [Table("tbl_Chuong")]
    public class Chuong
    {
        [Key]
        public int ChuongId { get; set; }
        public string Ten { get; set; }
        public int LopId { get; set; }
        [ForeignKey("LopId")]
        public virtual Lop Lop { get; set; }
        public virtual ICollection<BaiHoc> BaiHocs { get; set; }

        public int Order { get; set; }
        [DefaultValue(0)]
        public int Hide { get; set; }
    }
}