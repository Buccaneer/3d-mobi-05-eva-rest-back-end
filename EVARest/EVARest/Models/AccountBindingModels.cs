using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using EVARest.Properties;

namespace EVARest.Models
{
    // Models used as parameters to AccountController actions.

    public class AddExternalLoginBindingModel
    {
        [Required(ErrorMessageResourceName = "AddExternalLoginExternalAccessTokenRequired", ErrorMessageResourceType =typeof(Resources))]
        [Display(Name = "ExternalAccessToken", ResourceType =typeof(Resources))]
        public string ExternalAccessToken { get; set; }
    }

    public class ChangePasswordBindingModel
    {
        [Required(ErrorMessageResourceName = "ChangePasswordOldPasswordRequired", ErrorMessageResourceType =typeof(Resources))]
        [DataType(DataType.Password)]
        [Display(Name = "OldPassword", ResourceType =typeof(Resources))]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceName = "ChangePasswordNewPasswordRequired", ErrorMessageResourceType =typeof(Resources))]
        [StringLength(100, ErrorMessageResourceName = "ChangePasswordNewPasswordStringLength", ErrorMessageResourceType =typeof(Resources), MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword", ResourceType =typeof(Resources))]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType =typeof(Resources))]
        [Compare("NewPassword", ErrorMessageResourceName = "ChangePasswordConfirmPasswordCompare", ErrorMessageResourceType =typeof(Resources))]
        public string ConfirmPassword { get; set; }
    }

    public class RegisterBindingModel
    {
        [Required(ErrorMessageResourceName = "RegisterEmailRequired", ErrorMessageResourceType =typeof(Resources))]
        [Display(Name = "Email", ResourceType =typeof(Resources))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "RegisterPasswordRequired", ErrorMessageResourceType =typeof(Resources))]
        [StringLength(100, ErrorMessageResourceName ="RegisterPasswordStringLength", ErrorMessageResourceType =typeof(Resources), MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType =typeof(Resources))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType =typeof(Resources))]
        [Compare("Password", ErrorMessageResourceName ="RegisterConfirmPasswordCompare", ErrorMessageResourceType =typeof(Resources))]
        public string ConfirmPassword { get; set; }
    }

    public class RegisterExternalBindingModel
    {
        [Required(ErrorMessageResourceName = "RegisterExternalEmailRequired", ErrorMessageResourceType =typeof(Resources))]
        [Display(Name = "Email", ResourceType =typeof(Resources))]
        public string Email { get; set; }
    }

    public class RemoveLoginBindingModel
    {
        [Required(ErrorMessageResourceName = "RemoveLoginLoginProviderRequired", ErrorMessageResourceType =typeof(Resources))]
        [Display(Name = "LoginProvider", ResourceType =typeof(Resources))]
        public string LoginProvider { get; set; }

        [Required(ErrorMessageResourceName = "RemoveLoginProviderKeyRequired", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "ProviderKey", ResourceType =typeof(Resources))]
        public string ProviderKey { get; set; }
    }

    public class SetPasswordBindingModel
    {
        [Required(ErrorMessageResourceName = "SetPasswordPasswordRequired", ErrorMessageResourceType =typeof(Resources))]
        [StringLength(100, ErrorMessageResourceName = "SetPasswordNewPasswordStringLength", ErrorMessageResourceType =typeof(Resources), MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword", ResourceType =typeof(Resources))]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmNewPassword", ResourceType =typeof(Resources))]
        [Compare("NewPassword", ErrorMessageResourceName = "SetPasswordConfirmPasswordCompare", ErrorMessageResourceType =typeof(Resources))]
        public string ConfirmPassword { get; set; }
    }
}
