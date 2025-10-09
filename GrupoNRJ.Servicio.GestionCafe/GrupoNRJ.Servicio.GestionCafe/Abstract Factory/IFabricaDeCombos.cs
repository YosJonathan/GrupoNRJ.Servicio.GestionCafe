// <copyright file="IFabricaDeCombos.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe.Abstract_Factory
{
    public interface IFabricaDeCombos
    {
        ICafe CrearCafe();

        ITasa CrearTaza();

        IFiltro CrearFiltro();
    }
}
