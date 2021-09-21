using System;
using Api.Data.Interfaces.Repositories;
using Api.Data.Interfaces.Commands;
using Api.Models;

namespace Api.Data.Commands
{
    public class CreatePrincipalCommand : ICreatePrincipalCommand
    {
        private IPrincipalsWriteRepository principalsWriteRepository;

        public CreatePrincipalCommand(IPrincipalsWriteRepository principalsWriteRepository)
        {
            this.principalsWriteRepository = principalsWriteRepository;
        }

        public bool CreatePrincipal(Principal principal)
        {
            return this.principalsWriteRepository.CreatePrincipal(principal);
        }
    }
}
