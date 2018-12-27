using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVA_Compulink_Integration.Models.ProductParameters;

namespace WVA_Compulink_Integration.Models.Validations
{
    class Validator
    {
        public static bool CheckIfValid(SKU sku)
        {
            if (sku == null)
                return true;
            else if (sku.IsValid)
                return true;
            else
                return false;
        }

        public static bool CheckIfValid(UPC upc)
        {
            if (upc == null)
                return true;
            else if (upc.IsValid)
                return true;
            else
                return false;
        }

        public static bool CheckIfValid(ProductKey productKey)
        {
            if (productKey == null)
                return true;
            else if (productKey.IsValid)
                return true;
            else
                return false;
        }

        public static bool CheckIfValid(ID id)
        {
            if (id == null)
                return true;
            else if (id.IsValid)
                return true;
            else
                return false;
        }

        public static bool CheckIfValid(CustomerID customerID)
        {
            if (customerID == null)
                return true;
            else if (customerID.IsValid)
                return true;
            else
                return false;
        }

        public static bool CheckIfValid(BaseCurve baseCurve)
        {
            if (baseCurve == null)
                return true;
            else if (baseCurve.IsValid)
                return true;
            else
                return false;
        }

        public static bool CheckIfValid(Diameter diameter)
        {
            if (diameter == null)
                return true;
            else if (diameter.IsValid)
                return true;
            else
                return false;
        }

        public static bool CheckIfValid(Sphere sphere)
        {
            if (sphere == null)
                return true;
            else if (sphere.IsValid)
                return true;
            else
                return false;
        }

        public static bool CheckIfValid(Cylinder cylinder)
        {
            if (cylinder == null)
                return true;
            else if (cylinder.IsValid)
                return true;
            else
                return false;
        }

        public static bool CheckIfValid(Axis axis)
        {
            if (axis == null)
                return true;
            else if (axis.IsValid)
                return true;
            else
                return false;
        }

        public static bool CheckIfValid(Add add)
        {
            if (add == null)
                return true;
            else if (add.IsValid)
                return true;
            else
                return false;
        }

        public static bool CheckIfValid(Color color)
        {
            if (color == null)
                return true;
            else if (color.IsValid)
                return true;
            else
                return false;
        }

        public static bool CheckIfValid(Multifocal multifocal)
        {
            if (multifocal == null)
                return true;
            else if (multifocal.IsValid)
                return true;
            else
                return false;
        }

        public static bool CheckIfValid(Help help)
        {
            // If help is null, we don't want to overwrite the old value with null or it won't be shown in F8 help window
            if (help == null)
                return false;
            else
                return true;
        }
    }
}
