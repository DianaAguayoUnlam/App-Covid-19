<<<<<<< HEAD
﻿/*using System;
=======
﻿using System;
>>>>>>> master
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
<<<<<<< HEAD
using WebCovid19;
using WebCovid19.Models.Views;

namespace DAO
{
    public class DonacionMonetariaDao
    {
        TpDBContext context = new TpDBContext();

        public void agregarFotoComprobante(VMDonacionMonetaria foto)
        {
            //ToDo: Verificar conexion a la bd, xq esto devuelve null
            context.SaveChanges();

        }
    }
}
*/
=======
using DAO.Context;
using Entidades.Views;
using Entidades;
using DAO.Abstract;

namespace DAO
{
    public class DonacionMonetariaDao : Crud<DonacionesMonetarias> //Uso de Generics
    {
        public override DonacionesMonetarias Actualizar(DonacionesMonetarias generics)
        {
            throw new NotImplementedException();
        }

        public override DonacionesMonetarias Crear(DonacionesMonetarias Dinero)
        {
            DonacionesMonetarias donacionesMonetarias = context.DonacionesMonetarias.Add(Dinero);
            decimal valor = context.SaveChanges();

            if (valor >= 100m)
            {
                return donacionesMonetarias;
            }
            else
            {
                return null;
            }
        }

        public DonacionesMonetarias Guardar(DonacionesMonetarias Dinero)
        {
            DonacionesMonetarias donacionesMonetarias = context.DonacionesMonetarias.Add(Dinero);
            context.SaveChanges();
            return donacionesMonetarias;

        }

        public override DonacionesMonetarias ObtenerPorID(int generics)
        {
            throw new NotImplementedException();
        }

        public List<DonacionesMonetarias> ObtenerPorId(int IdNeceDonacionMonetaria)
        {
            List<DonacionesMonetarias> listarCantidadNecesariaADonar =
            context.DonacionesMonetarias.Where(a => a.IdNecesidadDonacionMonetaria == IdNeceDonacionMonetaria).ToList();

            return listarCantidadNecesariaADonar;
        }

        public NecesidadesDonacionesMonetarias CantidadSolicitada(int IdNecesidadDonacionMonetaria)
        {
            NecesidadesDonacionesMonetarias traerDineroPorId =
            context.NecesidadesDonacionesMonetarias.Find(IdNecesidadDonacionMonetaria);
            return traerDineroPorId;
        }
    }
}
>>>>>>> master
