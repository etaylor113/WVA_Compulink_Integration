using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVA_Compulink_Integration.Models.Exam;
using WVA_Compulink_Integration.Models.Patient;

namespace WVA_Compulink_Integration.ViewModels
{
    public class AddToOrderViewModel
    {
        public static string ID { get; set; }
        public static string FirstName { get; set; }
        public static string LastName { get; set; }
        public static string Street { get; set; }
        public static string City { get; set; }
        public static string State { get; set; }
        public static string Zip { get; set; }
        public static string Phone { get; set; }
        public static string Location { get; set; }

        public AddToOrderViewModel(Patient patient)
        {
            ID = patient.PatientID;
            FirstName = patient.FirstName;
            LastName = patient.LastName;
            Street = patient.Street;
            City = patient.City;
            State = patient.State;
            Zip = patient.Zip;
            Phone = patient.Phone;
            Location = patient.Location;
        }

        public AddToOrderViewModel(Exam exam)
        {
            ID = exam.PatientID;
            FirstName = exam.FirstName;
            LastName = exam.LastName;
            Street = exam.Street;
            City = exam.City;
            State = exam.State;
            Zip = exam.Zip;
            Phone = exam.Phone;
            Location = exam.Location;
        }
    }
}
