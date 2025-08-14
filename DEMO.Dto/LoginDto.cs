using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMO.Dto
{
    public class AdminLoginDto
    {
        [Required(ErrorMessage = "Email/MemberId is required")]
        public string? EmailId { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        public bool RememberMe { get; set; }
    }

    public class AdminLoginResponseDto
    {
        public string? Token { get; set; }
        public string? Name { get; set; }
        public DateTime ExpiryTime { get; set; }
    }
}
