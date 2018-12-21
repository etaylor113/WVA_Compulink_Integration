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
        // Hidden in client interface but still need these
        public CustomerID _CustomerID { get; set; }       
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Properties are visible to client in the table
        private string patient;   

        public string Patient
        {
            get { return patient; }
            set { patient = $"{FirstName} {LastName}"; }
        }

        public string ProductImagePath { get; set; } 
        public bool IsChecked { get; set; } = false;
        public string ProductCode { get; set; }
        public string Date { get; set; }
        public string Eye { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public string BaseCurve { get; set; }
        public string Diameter { get; set; }
        public string Sphere { get; set; }
        public string Cylinder { get; set; }
        public string Axis { get; set; }
        public string Add { get; set; }
        public string Color { get; set; }
        public string Multifocal { get; set; }
    }
}
