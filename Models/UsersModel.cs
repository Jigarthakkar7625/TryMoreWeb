using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace TryMoreWeb.Models
{

    //public enum UserType
    //{
    //    Therapist = 1,
    //    Patient = 2

    //}


    public enum WeekDays
    {
        Monday = 0,
        Tuesday = 1,
        Wednesday = 2,
        Thursday = 3,
        Friday = 4,
        Saturday = 5,
        Sunday = 6
    }


    public class UsersModel : BaseModel
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(100)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }

        //[Required]
        //[Display(Name = "Password")]
        //[StringLength(100)]
        //public string Password { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        public int RefID { get; set; }

        
        public int RefType { get; set; }

        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Postal Code is required")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Registration No is required")]
        public string RegistrationNo { get; set; }

        public int Approve { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [StringLength(3)]
        public int? Age { get; set; }

        [Required(ErrorMessage = "Insurance No is required")]
        [StringLength(100)]
        public string InsuranceNo { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ExpDate { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DOB { get; set; }

        public HttpPostedFileBase files { get; set; }

        public string ProfileImage { get; set; }

        // New Field
        [Required(ErrorMessage = "Address is required")]
        public string AddrLine1 { get; set; }

        
        public string AddrLine2 { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string ServiceID { get; set; }

        [Required]
        public string BankDetail { get; set; }
        [Required(ErrorMessage = "Skill is required")]
        public string Skill { get; set; }

        [Required]
        public string FromTime { get; set; }
        [Required]
        public string ToTime { get; set; }

        public string Gender { get; set; }
     

        public int UserType { get; set; }
        public UserTokenViewModel UserTokenInfo { get; set; }

        public bool isFiles { get; set; }
        public string FileExt { get; set; }
    }
    public class UserTokenViewModel
    {
        public string access_token { set; get; }
        public string expires_in { set; get; }
        public string refresh_token { set; get; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

}