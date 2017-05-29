using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Final.Models
{
    [Table("tbl_Lop")]
    public class Lop
    {
        [Key]
        public int LopId { get; set; }
        public string Ten { get; set; }
        public virtual ICollection<Chuong> Chuongs { get; set; }

        public int Order { get; set; }
        [DefaultValue(0)]
        public int Hide { get; set; }
    }
}