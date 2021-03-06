using DogGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Repositories
{
    public interface IOwnerRepository
    {
        List<Owner> GetAllOwners();
        Owner GetOwnerById(int id);
        public Owner GetOwnerByEmail(string email); 
        void AddOwner(Owner owner);
        void DeleteOwner(int ownerId);
        void UpdateOwner(Owner owner);
    }
}
