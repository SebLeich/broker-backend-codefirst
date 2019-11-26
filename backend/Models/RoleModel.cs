using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    /// <summary>
    /// the identity role model
    /// </summary>
    public class RoleModel
    {
        /// <summary>
        /// the role name
        /// </summary>
        [Required]
        [Display(Name = "Role name")]
        public string RoleName { get; set; }
    }
}