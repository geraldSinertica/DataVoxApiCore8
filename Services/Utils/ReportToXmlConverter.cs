using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Services.Utils
{
   public class ReportToXmlConverter
    {
        public string ConvertJsonToXml(string json)
        {
         

            try
            {
                var jsonObject = JObject.Parse(json);

                var rootName = "Reporte"; // Puedes cambiar este nombre según tus necesidades


                //string xml = JsonConvert.DeserializeXmlNode(jsonObject.ToString()).InnerXml;
                string xml = JsonConvert.DeserializeXmlNode(json, rootName).InnerXml;

                return xml;
            }
            catch (InvalidOperationException ex)
            {
               throw new InvalidOperationException("Error de serialización XML: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al convertir el objeto Report a XML: " + ex.Message, ex);
            }
        }

       
      
    }
}
