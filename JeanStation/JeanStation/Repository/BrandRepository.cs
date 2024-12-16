using JeanStation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JeanStation.Repository
{
    public class BrandRepository : IBrandRepository
    {
        private JeanStationContext _context;
        public BrandRepository()
        {
            _context = new JeanStationContext();
        }
        public void AddBrand(Brand brand)
        {
            var v = _context.Brands.Add(brand);
            _context.SaveChanges();
        }

        public void DeleteBrand(string id)
        {
            var v = _context.Brands.SingleOrDefault(x=>x.BrandId == id);
            _context.Brands.Remove(v);
            _context.SaveChanges();
        }

        public List<Brand> GetAllBrands()
        {
            return _context.Brands.ToList();
        }

        public Brand GetBrandById(string id)
        {
            return _context.Brands.SingleOrDefault(x=>x.BrandId==id);   
        }

        public Brand GetBrandByName(string name)
        {
            return _context.Brands.SingleOrDefault(x=>x.BrandName==name);  
        }

        public void UpdateBrand(Brand brand)
        {
           var v = _context.Brands.SingleOrDefault(x=>x.BrandName==brand.BrandName);
            if (v != null) 
            {
                _context.Brands.Remove(v);
                _context.SaveChanges();
            }
           
        }
    }
}