using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JeanStation.Entities
{
    public class Jeans
    {
        [Key]
        public string JeansId {  get; set; }
        [ForeignKey("BrandNavigation")]
        public string BrandId {  get; set; }
        public string Type { get; set; }
        public string Color {  get; set; }
        public string Size {  get; set; }
        public int Stock {  get; set; }
        public Brand BrandNavigation {  get; set; }
    }
}