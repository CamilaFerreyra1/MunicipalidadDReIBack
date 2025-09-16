using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using DReI.BackWeb.Models.Entities;

namespace DReI.BackWeb.Data
{
    /// <summary>
    /// Clase principal de acceso a datos para la base de datos de Rafaela.
    /// Hereda de DbContext de Entity Framework y define los DbSet para las entidades principales.
    /// También incluye una implementación mejorada de SaveChanges() para mostrar mensajes de error claros.
    /// </summary>
    public class DbContext : System.Data.Entity.DbContext
    {
        public DbContext()
            : base("name=rafaelagovarEntities")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Contribuyente> DRI_CatMonoContrib { get; set; }
        public DbSet<Vigencias> DRI_CatMonoVigencias { get; set; }
        public DbSet<ConfiguracionTributaria> DRI_Configuracion { get; set; }
        public DbSet<ContribuyenteDetalleMagic> MAGIC_M03DRI { get; set; }
        public DbSet<DDJJCabecera> DRI_DJ_CAB { get; set; }
        public DbSet<DDJJRenglon> DRI_DJ_REN { get; set; }
        public DbSet<RegimenExcepcion> DRI_RegimenExepciones { get; set; }
        public DbSet<Rubros> DRI_Rubros { get; set; }
        public DbSet<RubroActividad> DRI_RubrosActividades { get; set; }
        public DbSet<RubroContribuyente> DRI_RubrosContrib { get; set; }
        public DbSet<CaratulaMagic> MAGIC_C03CARAA { get; set; }
        public DbSet<ActividadContribuyente> DRI_ActividadesContrib { get; set; }
        public DbSet<DatoContribuyente> DRI__DatosContrib { get; set; }
        public DbSet<ExplotacionMagic> M03EXPLO1_orig { get; set; }
        public DbSet<Nomenclador> DRI_Nomencladores { get; set; }
        public DbSet<ConfiguracionSistema> SIS_Configuracion { get; set; }
        public DbSet<ActividadExcluyente> DRI_RegSimplif_ActividadesExcluyentes { get; set; }
        public DbSet<ContribuyenteConDeuda> DRI_RegSimplif_ContribConDeuda { get; set; }
        public DbSet<ConfiguracionActividad> DRI_ConfiguracionActividades { get; set; }
        public DbSet<ContribuyenteBaja> DRI_CatMonoContrib_BAJAS { get; set; }

        /// <summary>
        /// Clase principal de acceso a datos para la base de datos de Rafaela.
        /// Hereda de DbContext de Entity Framework y define los DbSet para las entidades principales.
        /// También incluye una implementación mejorada de SaveChanges() para mostrar mensajes de error claros.
        /// </summary>
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat("Hay errores de validación. Éstos son: ", fullErrorMessage);

                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }
    }
}
