using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JeanStation.Entities
{
    public class Brand
    {
        [Key]
        public string BrandId {  get; set; }
        public string BrandName {  get; set; }
    }
}