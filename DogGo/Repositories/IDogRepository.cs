using DogGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Repositories
{
    public interface IDogRepository
    {
        List<Dog> GetAll();
        Dog GetDogById(int id);
        void AddDog(Dog dog);
        void DeleteDog(int dogId);
        void UpdateDog(Dog dog);

    }
}
