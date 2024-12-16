using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JeanStation.Entities;

namespace JeanStation.Repository
{
    public interface IBrandRepository
    {
        List<Brand> GetAllBrands();
        void AddBrand(Brand brand);
        void UpdateBrand(Brand brand);
        void DeleteBrand(string id);
        Brand GetBrandById(string id);
        Brand GetBrandByName(string name);
       
    }
}