namespace ProductCatalog.ViewModels
{
    public class RegisterAccountViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password" , ErrorMessage = "Password not matched")]
        [Display(Name = "Confirm Password")]

        public string ConfirmPassword { get; set; }
    }
}
