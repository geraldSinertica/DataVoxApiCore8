using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AplicationCore.Utils
{
    public class Authentication
    {
        public static dynamic ValidateToken(ClaimsIdentity identity)
        {
			try
			{
                if (identity.Claims.Count() == 0)
                {
                    return 
                    new {
                         StatusCode =401,
                         Message ="Token incorrecto",
                         Data=""
                    };

                }
                else
                {
                    var user = identity.Claims.FirstOrDefault(x=> x.Type == "User").Value;
                    return
                    new
                    {
                        StatusCode = 200,
                        Message = "Token correcto",
                        Data = user
                    };
                }
            }
			catch (Exception ex)
			{

                throw new Exception(ex.Message);
            }
        }
    }
}
