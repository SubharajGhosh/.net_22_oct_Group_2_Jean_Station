using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JeanStation.Entities;
using JeanStation.Models;

namespace JeanStation.Repository
{
    internal interface IJeansRepository
    {
        List<Jeans> GetAll();
        Jeans GetById(string id);
        void Add(Jeans jeans);
        void UpdateJeans(Jeans jeans);
        void Delete(string id);
        List<JeansDto> GetJeansByBrandName(string brandName);
        void UpdateStock(Jeans jeans);

       
    }
}
