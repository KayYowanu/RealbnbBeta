using RealbnbBeta.Service;
using RealbnbBeta.Models;
using System;

namespace RealbnbBeta.Imageclass
{
    public class ImagePage
    {
        IbnbService _bnbService = null;
        public ImagePage(IbnbService bnbService)
        {
            _bnbService = bnbService;
        }

        /*For the current user*/
        public string UserName;
        public string SaveInformation(byte[] fileBytes, bnbProperties bnbImg)
        {
           // UserName = currentUser.HttpContext.User.Identity.Name;
            bnbImg.Imagebnb = fileBytes;
            bnbImg = _bnbService.CreateProperty(bnbImg, UserName);
            if (bnbImg.PropertyId > 0)
            {
                return "Saved Succesfully!";
            }
            return "Failed to Save!";
        }

        /*public bnbProperties GetProperties()
        {
            var bnb2 = _bnbService.GetProperties(); ;
            bnb2.Imagebnb = this.GetImage(Convert.ToBase64String(bnb2.Imagebnb));
            bnb2.ImageUrl = string.Format("data:image/jpg;base64," + Convert.ToBase64String(bnb2.Imagebnb));
            return bnb2;
        }*/
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
