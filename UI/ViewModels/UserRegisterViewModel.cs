using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using BusinessEntities;

namespace UI.ViewModels
{
    public class UserRegisterViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string NormalizedUserName { get; set; }
        public string NormalizedEmail { get; set; }

        [Required(ErrorMessage = "Veuillez entrer un mot de passe valide"),
        MinLength(6, ErrorMessage = "Votre mot de passe doit comprendre au minimum 6 caractères"),
        MaxLength(100, ErrorMessage = "Votre mot de passe ne doit pas depasser 100 caractères"),
        DataType(DataType.Password)]
        public string Password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Veuillez confirmer le mot de passe que vous avez choisi"),
        MinLength(6, ErrorMessage = "Votre mot de passe de confirmation doit comprendre au minimum 6 caractères"),
        MaxLength(100, ErrorMessage = "Votre mot de passe de confirmation ne doit pas depasser 100 caractères"),
        DataType(DataType.Password),
        Compare("Password", ErrorMessage = "Les mots de passe que vous avez entrés ne sont pas identiques")]
        public string PasswordConfirmation { get; set; }        
    }
}