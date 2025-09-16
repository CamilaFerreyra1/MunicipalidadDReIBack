using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.Odbc;

namespace DReI.BackWeb.Data
{
    public static class DbContextFactory 
    {
        public static DbContext CreateRafaelaContext()
        {
            return new DbContext();
        }

        // Solo descomenta esto cuando crees AdministracionEntities
        // public static AdministracionEntities CreateAdministracionContext()
        // {
        //     return new AdministracionEntities();
        // }

        public static OdbcConnection CreateDb2Connection()
        {
            var connStr = ConfigurationManager.ConnectionStrings["DB2_Tributos"].ConnectionString;
            return new OdbcConnection(connStr);
        }
    }
}