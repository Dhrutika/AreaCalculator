using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestApplication.Models
{
    public class ShapeModel
    {
        [Required]
        public int Shape { get; set; }
        public List<int> Shapes { get; set; }
        [Required(ErrorMessage = "This field is Required.")]
        [RegularExpression("([1-9]*)", ErrorMessage = "This field must be a natural number.")]
        public string Dimension1 { get; set; }
        [RegularExpression("([1-9]*)", ErrorMessage = "This field must be a natural number.")]
        public string Dimension2 { get; set; }
        [RegularExpression("([1-9]*)", ErrorMessage = "This field must be a natural number.")]
        public string Dimension3 { get; set; }
        public string Area { get; set; }
    }

    public enum ShapeEnum
    {
        Square = 1,
        Rectangle = 2,
        Triangle = 3,
        Circle = 4,
        Sector = 5,
        Eclipse = 6,
        Trapezoid = 7,
        Parallelogram = 8
    }
}