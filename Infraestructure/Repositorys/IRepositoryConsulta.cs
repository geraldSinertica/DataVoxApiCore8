﻿using Infraestructure.Models;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositorys
{
    public interface IRepositoryConsulta
    {
        List<Consulta> GetAllByPerson(int PersonId);
        void AddConsultaYFacturarion(Facturacion facturacion, Consulta consulta);
    }
}
