using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Final.Models
{
    [Table("tbl_HocPhi")]
    public class HocPhi
    {
        [Key]
        public int HocPhiId { get; set; }
        public int SoTien {get;set;}
        public DateTime NgayThanhToan {get;set;}
        public DateTime NgayHetHan {get;set;}
        public int HocSinhId { get; set; }

        [ForeignKey("HocSinhId")]
        public virtual HocSinh HocSinh { get; set; }

        public int Order { get; set; }
        [DefaultValue(0)]
        public int Hide { get; set; }
    }
}