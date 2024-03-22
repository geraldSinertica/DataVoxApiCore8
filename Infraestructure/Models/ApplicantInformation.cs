using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Repository.Models
{
   public class ApplicantInformation
    {
        public PersonalData InformacionPersonal { get; set; }

        public ContactData TelefonosYDirecciones { get; set; }

        public Filiacion Filiaciones  { get; set; }

        public Appointment Nombramientos { get; set; }
    }
}
