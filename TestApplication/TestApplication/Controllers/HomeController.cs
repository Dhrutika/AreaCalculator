using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TestApplication.Models;

namespace TestApplication.Controllers
{
    public class HomeController : Controller
    {
        public static double PI = 3.14;
        public ActionResult Index()
        {
            try
            {
                string Shape = string.Empty;
                string Dimensions = string.Empty;
                string Area = string.Empty;
                string fileName = Convert.ToString(DateTime.Now.ToString("yyyyMMdd"));
                string path = Path.Combine(Server.MapPath("/Data/"), fileName + ".txt");
                if (!System.IO.File.Exists(path))
                {
                    using (StreamWriter sw = System.IO.File.CreateText(path))
                    {
                        sw.WriteLine("=========================================");
                        sw.WriteLine("Shape: " + Shape);
                        sw.WriteLine("Dimensions: " + Dimensions);
                        sw.WriteLine("Calculated Area: " + Area);
                        sw.WriteLine("=========================================");
                    }
                }
                else
                {
                    using (StreamWriter sw = System.IO.File.AppendText(path))
                    {
                        sw.WriteLine("=========================================");
                        sw.WriteLine("Shape: " + Shape);
                        sw.WriteLine("Dimensions: " + Dimensions);
                        sw.WriteLine("Calculated Area: " + Area);
                        sw.WriteLine("=========================================");
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
            return View();
        }

        public ActionResult Area()
        {
            List<SelectListItem> shapeList = ShapeList();
            ViewData["ShapesDetail"] = shapeList;
            return View();
        }

        [HttpPost]
        public ActionResult Area(ShapeModel objShape)
        {
            List<SelectListItem> shapeList = ShapeList();
            ViewData["ShapesDetail"] = shapeList;
            if (objShape != null && (objShape.Shape == 2 || objShape.Shape == 3 || objShape.Shape == 5 || objShape.Shape == 6 || objShape.Shape == 7 || objShape.Shape == 8 || objShape.Shape == 9) && objShape.Dimension2 == null)
            {
                ModelState.AddModelError("Dimension2", "This field is Required.");
                return View(objShape);
            }
            if (objShape != null && (objShape.Shape == 7) && objShape.Dimension3 == null)
            {
                ModelState.AddModelError("Dimension3", "This field is Required.");
                return View(objShape);
            }
            if (ModelState.IsValid)
            {
                objShape = CalculateArea(objShape);
                SaveArea(objShape);
            }
            return View(objShape);
        }

        public List<SelectListItem> ShapeList()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="Square", Value = "1", Selected = true},
                new SelectListItem{ Text="Rectangle", Value = "2" },
                new SelectListItem{ Text="Triangle", Value = "3" },
                new SelectListItem{ Text="Circle", Value = "4" },
                new SelectListItem{ Text="Sector", Value = "5" },
                new SelectListItem{ Text="Eclips", Value = "6" },
                new SelectListItem{ Text="Trapezoid", Value = "7" },
                new SelectListItem{ Text="Parallelogram", Value = "8" }
            };
            return list;
        }

        public ShapeModel CalculateArea(ShapeModel objShape)
        {
            double Area = 0;

            if (objShape != null)
            {
                int D1 = Convert.ToInt32(objShape.Dimension1);
                if (objShape.Shape == Convert.ToInt32(ShapeEnum.Square))
                {
                    Area = (D1 * D1);
                    
                }
                else if (objShape.Shape == Convert.ToInt32(ShapeEnum.Rectangle))
                {
                    int D2 = Convert.ToInt32(objShape.Dimension2);
                    Area = (D1 * D2);
                }
                else if (objShape.Shape == Convert.ToInt32(ShapeEnum.Triangle))
                {
                    int D2 = Convert.ToInt32(objShape.Dimension2);
                    Area = (D1 * D2) / 2;
                }
                else if (objShape.Shape == Convert.ToInt32(ShapeEnum.Circle))
                {
                    Area = PI * (D1 * D1);
                }
                else if (objShape.Shape == Convert.ToInt32(ShapeEnum.Sector))
                {
                    int D2 = Convert.ToInt32(objShape.Dimension2);
                    Area = ((D1 * D1) * D2) / 2;
                }
                else if (objShape.Shape == Convert.ToInt32(ShapeEnum.Eclipse))
                {
                    int D2 = Convert.ToInt32(objShape.Dimension2);
                    Area = PI * (D1 * D2);
                }
                else if (objShape.Shape == Convert.ToInt32(ShapeEnum.Trapezoid))
                {
                    int D2 = Convert.ToInt32(objShape.Dimension2);
                    int D3 = Convert.ToInt32(objShape.Dimension3);
                    Area = ((D1 + D2) * D3) / 2;
                }
                else if (objShape.Shape == Convert.ToInt32(ShapeEnum.Parallelogram))
                {
                    int D2 = Convert.ToInt32(objShape.Dimension2);
                    Area = (D1 * D2);
                }
                objShape.Area = "Area of " + (ShapeEnum)objShape.Shape + " is " + Convert.ToString(Area) + ".";
            }
            return objShape;
        }

        public void SaveArea(ShapeModel objShape)
        {
            try
            {
                string Shape = Convert.ToString((ShapeEnum)objShape.Shape);
                string Area = objShape.Area;
                string fileName = Convert.ToString(DateTime.Now.ToString("yyyyMMdd"));
                string path = Path.Combine(Server.MapPath("/Data/"), fileName + ".txt");
                if (!System.IO.File.Exists(path))
                {
                    using (StreamWriter sw = System.IO.File.CreateText(path))
                    {
                        sw.WriteLine("=========================================");
                        sw.WriteLine("Date Time: " + DateTime.Now.ToString());
                        sw.WriteLine("Shape: " + Shape);
                        sw.WriteLine(Area);
                        sw.WriteLine("=========================================");
                    }
                }
                else
                {
                    using (StreamWriter sw = System.IO.File.AppendText(path))
                    {
                        sw.WriteLine("=========================================");
                        sw.WriteLine("Date Time: " + DateTime.Now.ToString());
                        sw.WriteLine("Shape: " + Shape);
                        sw.WriteLine(Area);
                        sw.WriteLine("=========================================");
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }
    }
}