using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Final.Models
{
    [Table("tbl_ToanVuiMoiNgay")]
    public class ToanVuiMoiNgay
    {
        [Key]
        public int ToanVuiMoiNgayId { get; set; }
        public string Ten { get; set; }
        public string NoiDung { get; set; }
        public int Order { get; set; }
        [DefaultValue(0)]
        public int Hide { get; set; }
    }
}