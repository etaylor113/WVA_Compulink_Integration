using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVA_Compulink_Integration.Models.ProductParameters;

namespace WVA_Compulink_Integration.Models.Prescription
{
    public class Prescription
    {
        // These properties are not visible to the client in the data grid
        public CustomerID _CustomerID { get; set; }       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProductCode { get; set; }
        public string SKU { get; set; }
        public string UPC { get; set; }
        public string Price { get; set; }
        public string ID { get; set; }

        // These properties are visible to the client in the data grid
        private string patient;      
        public string Patient
        {
            get { return patient; }
            set { patient = $"{FirstName} {LastName}"; }
        }                    
        public string Date { get; set; }
        public string Eye { get; set; }
        public string Product { get; set; }
        public string Quantity { get; set; }
        public string BaseCurve { get; set; }
        public string Diameter { get; set; }
        public string Sphere { get; set; }
        public string Cylinder { get; set; }
        public string Axis { get; set; }
        public string Add { get; set; }
        public string Color { get; set; }
        public string Multifocal { get; set; }

        // This is the property that controls the value of the row's check box 
        public bool IsChecked { get; set; } = false;

        // Change this string to the path of the Red, Yellow, or Green bubble image to change a row's product match status 
        public string ProductImagePath { get; set; }

        // Assign these "White", "Green", or "Red" to change the corresponding cell's background color
        public string BaseCurveCellColor { get; set; } 
        public string DiameterCellColor { get; set; }
        public string SphereCellColor { get; set; }
        public string CylinderCellColor { get; set; } 
        public string AxisCellColor { get; set; }
        public string AddCellColor { get; set; }
        public string ColorCellColor { get; set; }
        public string MultifocalCellColor { get; set; }
    }
}
