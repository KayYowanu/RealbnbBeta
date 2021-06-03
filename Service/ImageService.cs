using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealbnbBeta.Data;
using RealbnbBeta.Models;
using RealbnbBeta.Service;

namespace RealbnbBeta.Service
{
    public class ImageService
    {
        /*private readonly SqlConnectionConfiguration _configuration;
        public ImageService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }*/

        /***************/
            IbnbService _bnbService = null;
            public ImageService(IbnbService bnbService)
            {
                _bnbService = bnbService;
            }
        /***************/


        public string SaveInformation(bnbProperties bnbprop)
            {
            //bnbImg.Imagebnb = fileBytes;
            bnbprop = _bnbService.CreateProperty(bnbprop);
                if (bnbprop.PropertyId > 0)
                {
                    return "Saved Succesfully!";
                }
                return "Failed to Save!";
            }
       /* public List<bnbProperties> GetProperties()
        {
            //var kay = _bnbService.GetProperties2();
            return 
        }*/
       
    }
}
