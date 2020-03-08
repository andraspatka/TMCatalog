// ------------------------------------------------------------------------------------------------------------------
// <copyright file="CatalogController.cs" company="DVSE GmbH">
//   Copyright (c) by DVSE Gesellschaft für Datenverarbeitung, Service und Entwicklung mbH. All rights reserved.
// </copyright>
// <author>Szilveszter Gorgicze</author>
// ------------------------------------------------------------------------------------------------------------------

namespace TMCatalog.Logic
{
    using TMCatalog.Model.DBContext;
    public class CatalogController
    {
        private TMCatalogDB catalogDatabase;

        public CatalogController()
        {
            this.catalogDatabase = new TMCatalogDB();
        }
    }
}
