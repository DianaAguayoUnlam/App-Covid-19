﻿using Entidades;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Servicios;
using WebCovid19.Utilities;
using Entidades.Metadata;
using WebCovid19.Filters;
using Entidades.Views;
using Entidades.Enum;

namespace WebCovid19.Controllers
{

    [LoginFilter]
    public class NecesidadesController : Controller
    {
        ServicioNecesidad servicioNecesidad = new ServicioNecesidad();
        ServicioNecesidadesInsumos servicioInsumo = new ServicioNecesidadesInsumos();
        ServicioNecesidadesMonetarias servicioMonetaria = new ServicioNecesidadesMonetarias();
        // GET: Necesidades
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Crear()
        { 

            NecesidadesMetadata necesidadesMetadata = new NecesidadesMetadata();
            return View(necesidadesMetadata);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(NecesidadesMetadata vmnecesidad)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                {
                    string nombreSignificativo = vmnecesidad.Nombre + " " + Session["Email"];
                    //Guardar Imagen
                    string pathRelativoImagen = ImagenesUtil.Guardar(Request.Files[0], nombreSignificativo);
                    vmnecesidad.Foto = pathRelativoImagen;
                }
                int idUsuario = int.Parse(Session["UserId"].ToString());
                Necesidades necesidad = servicioNecesidad.buildNecesidad(vmnecesidad, idUsuario); 
                TempData["idNecesidad"] = necesidad.IdNecesidad;
                if (Enum.GetName(typeof(TipoDonacion), vmnecesidad.TipoDonacion) == "Insumos")
                {
                    return View("Insumos");
                }
                else
                {
                    return View("Monetaria");
                }
            }

        }

        [HttpGet]
        public ActionResult Insumos()
        {
            NecesidadesDonacionesInsumosMetadata insumos = new NecesidadesDonacionesInsumosMetadata();
            string s = TempData["idNecesidad"].ToString();
            int idNecesidad = int.Parse(s);
            insumos.Necesidades = servicioNecesidad.obtenerNecesidadPorId(idNecesidad);
            return View(insumos);
        }            

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Insumos(NecesidadesDonacionesInsumosMetadata insumos)
        {
            if (!ModelState.IsValid)
            {
                TempData["idNecesidad"] = insumos.Necesidades.IdNecesidad;
                return View();
            }
            servicioInsumo.GuardarInsumos(insumos);
            return View();
        }

        [HttpGet]
        public ActionResult Monetaria()
        {   
            NecesidadesDonacionesMonetarias monetaria = new NecesidadesDonacionesMonetarias();
            string s = TempData["idNecesidad"].ToString();
            int idNecesidad = int.Parse(s);
            monetaria.Necesidades = servicioNecesidad.obtenerNecesidadPorId(idNecesidad);
            return View(monetaria);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Monetaria(NecesidadesDonacionesMonetariasMetadata monetarias)
        {
            if (!ModelState.IsValid)
            {
                TempData["idNecesidad"] = monetarias.Necesidades.IdNecesidad;
                return View();
            }
            servicioMonetaria.GuardarMonetarias(monetarias);
            return View();
        }

        [HttpPost]
        public ActionResult MisNecesidades(string necesidad)
        {
            int idSession = int.Parse(Session["UserId"].ToString());
            List<Necesidades> necesidadesObtenidas = servicioNecesidad.TraerNecesidadesDelUsuario(idSession, necesidad);
            ServicioNecesidadValoraciones servNecesidadValoraciones = new ServicioNecesidadValoraciones();
            //Mantener el checkbox seleccionado o no, dependiendo lo que haya elegido
            TempData["estadoCheckbox"] = necesidad;
            List<NecesidadesValoraciones> valoracionesObtenidas = servNecesidadValoraciones.obtenerValoracionesDelUsuario(idSession);
            VMPublicacion vMPublicacion = new VMPublicacion()
            {
                listaNecesidades = necesidadesObtenidas,
                necesidadesValoraciones = valoracionesObtenidas
            };

            return View(vMPublicacion);
        }

    }
}