using JeanStation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JeanStation.Repository
{
    public class JeansRepository : IJeansRepository
    {
        private JeanStationContext _context;
        public JeansRepository()
        {
            _context = new JeanStationContext();
        }
        // Get all Jeans records
        public List<Jeans> GetAll()
        {
            return _context.Jeans.ToList();
        }

        // Get a specific Jeans record by its ID
        public Jeans GetById(string id)
        {
            return _context.Jeans.Find(id);
        }

        // Add a new Jeans record
        public void Add(Jeans jeans)
        {
            _context.Jeans.Add(jeans);
            _context.SaveChanges();
        }

        // Update an existing Jeans record
        public void UpdateJeans(Jeans jeans)
        {
            var existingJeans = _context.Jeans.Find(jeans.JeansId);
            if (existingJeans != null)
            {
                
                existingJeans.Size = jeans.Size;
                existingJeans.Color = jeans.Color;
                existingJeans.BrandId = jeans.BrandId;
                existingJeans.Stock = jeans.Stock;
                _context.SaveChanges();
    
           } 
        }

        // Delete a Jeans record by ID
        public void Delete(string id)
        {
            var jeans = _context.Jeans.SingleOrDefault(x=>x.JeansId == id);
            if (jeans != null)
            {
                _context.Jeans.Remove(jeans);
                _context.SaveChanges();
            }
        }

        // Get a Brand by its name
        public Brand GetByBrandName(string name)
        {
            return _context.Brands.FirstOrDefault(b => b.BrandName==name);
        }

        // Update the stock of a Jeans record
        public void UpdateStock(Jeans jeans)
        {
            var existingJeans = _context.Jeans.Find(jeans.JeansId);
            if (existingJeans != null)
            {
                existingJeans.Stock = jeans.Stock;
                _context.SaveChanges();
            }
        }
    }
}