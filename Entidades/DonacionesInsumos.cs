
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------


namespace Entidades
{

using System;
    using System.Collections.Generic;
    
public partial class DonacionesInsumos
{

    public int IdDonacionInsumo { get; set; }

    public int IdNecesidadDonacionInsumo { get; set; }

    public int IdUsuario { get; set; }

    public int Cantidad { get; set; }

    public System.DateTime FechaCreacion { get; set; }



    public virtual NecesidadesDonacionesInsumos NecesidadesDonacionesInsumos { get; set; }

    public virtual Usuarios Usuarios { get; set; }

}

}
