using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DReI.BackWeb.Models.Entities;
using DReI.BackWeb.Models.Dto;
using DReI.BackWeb.Data;

namespace DReI.BackWeb.Services
{
    public class ContribuyentesService
    {
        private readonly ApplicationDbContext _context;

        public ContribuyentesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public ContribuyenteDto Obtener(int cuenta, bool traerDadosDeBaja = false)
        {
            var entidad = _context.MAGIC_M03DRI
                .Where(c => c.NRO_INCRIPCION == cuenta &&
                           (traerDadosDeBaja || c.FECHABAJA.Year == 1900))
                .FirstOrDefault();

            if (entidad == null) return null;

            return new ContribuyenteDto
            {
                //COMUNA = entidad.COMUNA,
                NRO_INCRIPCION = entidad.NRO_INCRIPCION,
                //CODEXPL_PRIMARIA = entidad.CODEXPL_PRIMARIA,
                //CODEXPL_SECUN1 = entidad.CODEXPL_SECUN1,
                //CODEXPL_SECUN2 = entidad.CODEXPL_SECUN2,
                //CODEXPL_SECUN3 = entidad.CODEXPL_SECUN3,
                //CODEXPL_SECUN4 = entidad.CODEXPL_SECUN4,
                //CODEXPL_SECUN5 = entidad.CODEXPL_SECUN5,
                RAZONSOCIAL = entidad.RAZONSOCIAL,
                TIPO_DOC = entidad.TIPO_DOC,
                NRO_DOC = entidad.NRO_DOC,
               // POSTAL_PARTIC = entidad.POSTAL_PARTIC,
                CALLE_PARTIC = entidad.CALLE_PARTIC,
                PUERTA_PARTIC = entidad.PUERTA_PARTIC,
                BARRIO_PARTIC = entidad.BARRIO_PARTIC,
                // PISO_PARTIC = entidad.PISO_PARTIC,
                // DPTO_PARTIC = entidad.DPTO_PARTIC,
                // TELEFONO_PARTIC = entidad.TELEFONO_PARTIC,
                // POSTAL_COMERC = entidad.POSTAL_COMERC,
                // CALLE_COMERC = entidad.CALLE_COMERC,
                // PUERTA_COMERC = entidad.PUERTA_COMERC,
                // BARRIO_COMERC = entidad.BARRIO_COMERC,
                // PISO_COMERC = entidad.PISO_COMERC,
                // DPTO_COMERC = entidad.DPTO_COMERC,
                // TELEFONO_COMERC = entidad.TELEFONO_COMERC,
                // CODNAT_JURIDICA = entidad.CODNAT_JURIDICA,
                // CODCARAC_ESTABLEC = entidad.CODCARAC_ESTABLEC,
                // CONV_MULTILATERAL = entidad.CONV_MULTILATERAL,
                // CODEXPL_PAGO = entidad.CODEXPL_PAGO,
                //ESTUDIO_CONTABLE = entidad.ESTUDIO_CONTABLE,
                //NRO_INCRIP_API = entidad.NRO_INCRIP_API,
                //NRO_CUIT_DRI = entidad.NRO_CUIT_DRI,
                // FEC_INICIO = entidad.FEC_INICIO,
                // FEC_CLAUSURA = entidad.FEC_CLAUSURA,
                //FEC_RECEPCION = entidad.FEC_RECEPCION,
                //EXP_LETRA = entidad.EXP_LETRA,
                //EXP_NRO = entidad.EXP_NRO,
                //EXP_DV = entidad.EXP_DV,
                //EXP_FICHERO = entidad.EXP_FICHERO,
                //EXP_TOMO = entidad.EXP_TOMO,
                //MARCA_ALTAA = entidad.MARCA_ALTAA,
                //CUENTA_TRIBUTA = entidad.CUENTA_TRIBUTA,
                //CODESTADO_DRI = entidad.CODESTADO_DRI,
                //CODESTADO_FISCAL = entidad.CODESTADO_FISCAL,
                //AÑO_ANT_DECLJUR = entidad.AÑO_ANT_DECLJUR,
                //AÑO_ACT_DECLJUR = entidad.AÑO_ACT_DECLJUR,
                //OCUP_DOMIN = entidad.OCUP_DOMIN,
                //CODIGO_1 = entidad.CODIGO_1,
                //CODIGO_2 = entidad.CODIGO_2,
                //MOTIVOBAJA = entidad.MOTIVOBAJA,
                FECHABAJA = entidad.FECHABAJA,
                //USUARIOBAJA = entidad.USUARIOBAJA,
                //HORAALTA = entidad.HORAALTA,
                //FECHAALTA = entidad.FECHAALTA,
                //USUARIOALTA = entidad.USUARIOALTA,
                //HORAMODIF = entidad.HORAMODIF,
                //FECHAMODIF = entidad.FECHAMODIF,
                //USUARIOMODIF = entidad.USUARIOMODIF,
                //POSTAL_REAL = entidad.POSTAL_REAL,
                //CALLE_REAL = entidad.CALLE_REAL,
                //PUERTA_REAL = entidad.PUERTA_REAL,
                //BARRIO_REAL = entidad.BARRIO_REAL,
                //PISO_REAL = entidad.PISO_REAL,
                //DPTO_REAL = entidad.DPTO_REAL,
                //TELEFONO_REAL = entidad.TELEFONO_REAL,
                //FECHA_AUTORIZA = entidad.FECHA_AUTORIZA,
                //CANT_UNIDAD = entidad.CANT_UNIDAD,
                //INSCRIP_OFICIO = entidad.INSCRIP_OFICIO,
                //CASO_SOCIAL = entidad.CASO_SOCIAL,
                NOM_FANTASIA = entidad.NOM_FANTASIA,
                //CODEXPL_PRIMARIA_MUN = entidad.CODEXPL_PRIMARIA_MUN,
                //CODEXPL_SECUN1_MUN = entidad.CODEXPL_SECUN1_MUN,
                //CODEXPL_SECUN2_MUN = entidad.CODEXPL_SECUN2_MUN,
                //CODEXPL_SECUN3_MUN = entidad.CODEXPL_SECUN3_MUN,
                //CODEXPL_SECUN4_MUN = entidad.CODEXPL_SECUN4_MUN,
                //CODEXPL_SECUN5_MUN = entidad.CODEXPL_SECUN5_MUN,
                //APELLIDO_CASADA = entidad.APELLIDO_CASADA,
                EMAIL = entidad.EMAIL,
                //FECHAHABIWEB = entidad.FECHAHABIWEB,
                //UltPerJuicio = entidad.UltPerJuicio,
                //Catastro = entidad.Catastro,
                //SubCatastro = entidad.SubCatastro,
                //EfectoresSociales = entidad.EfectoresSociales,
                //FRECEPCIONTRAMITE = entidad.FRECEPCIONTRAMITE,
                //FAPROBACIONTRAMITE = entidad.FAPROBACIONTRAMITE
            };
        }
    }
}