using System;
using System.ComponentModel.DataAnnotations;
using Api.Models;

namespace Api.Controllers.V1.Principals
{
    public class PrincipalRequestModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }

        public Principal ToPrincipal()
        {
            return new Principal
            {
                Id = this.Id.ToString(),
                FirstName = this.FirstName,
                Surname = this.Surname,
                Email = this.Email
            };
        }
    }
}
