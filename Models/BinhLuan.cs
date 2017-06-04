using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace Final.Models
{
    [Table("tbl_BinhLuan")]
    public class BinhLuan
    {
        [Key]
        public int BinhLuanId { get; set; }
        public string NoiDung { get; set; }
        public string TacGia { get; set; }
        public string Hinh {get;set;}
        public int BaiHocId { get; set; }
        [ForeignKey("BaiHocId")]
        public virtual BaiHoc BaiHoc { get; set; }
        
        public int Order { get; set; }
        [DefaultValue(0)]
        public int Hide { get; set; }
    }
}