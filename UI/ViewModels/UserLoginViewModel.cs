using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using BusinessEntities;

namespace UI.ViewModels
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "Le Login est obligatoire"), Column(TypeName = "varchar(256)"),
        StringLength(20, ErrorMessage = "Le login doit etre 5 et 20 caractères en texte simple", MinimumLength = 5),
        RegularExpression(@"^[a-zA-Z][a-zA-Z0-9]*$", ErrorMessage = "Le login doit commencer par une lettre et comporter que des caractères alphanumérique")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Veuillez entrer un mot de passe valide"),
        MinLength(6, ErrorMessage = "Votre mot de passe doit comprendre au minimum 6 caractères"),
        MaxLength(100, ErrorMessage = "Votre mot de passe ne doit pas depasser 100 caractères"),
        DataType(DataType.Password)]
        public string Password { get; set; }
    }
}