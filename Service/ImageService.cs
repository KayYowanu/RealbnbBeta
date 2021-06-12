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
        /***************/
            IbnbService _bnbService = null;
            public ImageService(IbnbService bnbService)
            {
                _bnbService = bnbService;
            }
        /***************/

        /*For the current user*/
        public string UserName;
        public string SaveInformation(bnbProperties bnbprop, bnbAmenities objbnbA)
            {
            bnbprop = _bnbService.CreateProperty(bnbprop, UserName, objbnbA);
                if (bnbprop.PropertyId > 0)
                {
                    return "Saved Succesfully!";
                }
                return "Failed to Save!";
            }
        public byte[] GetImage(string kBase64String)
        {
            byte[] bytes = null;
            if (!string.IsNullOrEmpty(kBase64String))
            {
                bytes = Convert.FromBase64String(kBase64String);
            }
            return bytes;
        }
    }
}
