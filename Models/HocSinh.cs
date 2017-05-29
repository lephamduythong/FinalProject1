using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;

namespace Final.Models
{
    [Table("tbl_HocSinh")]
    public class HocSinh
    {
        [Key]
        public int HocSinhId { get; set; }
        [Required]
        [RegularExpression(@"^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$",
            ErrorMessage="Vui lòng nhập đúng email")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$",
        ErrorMessage="Vui lòng nhập mật khẩu ít nhất 8 kí tự, 1 ký tự hoa và một 1 ký tự là số")]
        public string Password { get; set; }
        [Required]
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string Hinh { get; set; }

        public virtual ICollection<HocSinhBaiHoc> HocSinhBaiHocs { get; set; }
        public virtual ICollection<HocSinhBaiTap> HocSinhBaiTaps { get; set; }
        public virtual ICollection<HocPhi> HocPhis { get; set; }

        public int Order { get; set; }
        [DefaultValue(0)]
        public int Hide { get; set; }
    }
}