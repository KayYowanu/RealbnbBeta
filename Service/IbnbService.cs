using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealbnbBeta.Models;

namespace RealbnbBeta.Service
{
    public interface IbnbService
    {
        /*For Property*/

        Task<IEnumerable<bnbProperties>> GetProperties();
        bnbProperties CreateProperty(bnbProperties bnb);
        //bnbProperties GetProperties2();
        List<bnbProperties> GetProperties2();
        /*bnbProperties UpdateProperty(int Id, bnbProperties bnb);
        bnbProperties SingleProperty(int id);
        Task<bool> DeleteProperty(int Id);*/
    }
}
