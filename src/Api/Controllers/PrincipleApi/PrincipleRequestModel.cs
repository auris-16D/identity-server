using System;
using System.ComponentModel.DataAnnotations;
using Api.Models;

namespace Api.Controllers.PrincipleApi
{
    public class PrincipleRequestModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }

        public Principle ToPrinciple()
        {
            return new Principle
            {
                Id = this.Id.ToString(),
                FirstName = this.FirstName,
                Surname = this.Surname,
                Email = this.Email
            };
        }
    }
}
