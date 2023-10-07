using ARSoftware.Contpaqi.Comercial.Sdk;
using ARSoftware.Contpaqi.Comercial.Sdk.Extensiones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContPAQi
{
    public class DatosCfdi
    {
        public string CadenaOriginalComplementoSat { get; set; }
        public string Fecha { get; set; }
        public string FechaFolioFiscal { get; set; }
        public string FolioFiscalOriginal { get; set; }
        public string FolioFiscalUUid { get; set; }
        public string LugarExpedicion { get; set; }
        public string Moneda { get; set; }
        public string MontoFolioFiscal { get; set; }
        public string Regimen { get; set; }
        public string SelloDigital { get; set; }
        public string SelloSat { get; set; }
        public string SerieCertificadoEmisor { get; set; }
        public string SerieCertificadoSat { get; set; }
        public string SerieFolioFiscal { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendLine($"{nameof(CadenaOriginalComplementoSat)} = {CadenaOriginalComplementoSat}");
            builder.AppendLine($"{nameof(Fecha)} = {Fecha}");
            builder.AppendLine($"{nameof(FechaFolioFiscal)} = {FechaFolioFiscal}");
            builder.AppendLine($"{nameof(FolioFiscalOriginal)} = {FolioFiscalOriginal}");
            builder.AppendLine($"{nameof(FolioFiscalUUid)} = {FolioFiscalUUid}");
            builder.AppendLine($"{nameof(LugarExpedicion)} = {LugarExpedicion}");
            builder.AppendLine($"{nameof(Moneda)} = {Moneda}");
            builder.AppendLine($"{nameof(MontoFolioFiscal)} = {MontoFolioFiscal}");
            builder.AppendLine($"{nameof(Regimen)} = {Regimen}");
            builder.AppendLine($"{nameof(SelloDigital)} = {SelloDigital}");
            builder.AppendLine($"{nameof(SelloSat)} = {SelloSat}");
            builder.AppendLine($"{nameof(SerieCertificadoEmisor)} = {SerieCertificadoEmisor}");
            builder.AppendLine($"{nameof(SerieCertificadoSat)} = {SerieCertificadoSat}");
            builder.AppendLine($"{nameof(SerieFolioFiscal)} = {SerieFolioFiscal}");

            return builder.ToString();
        }

        public static DatosCfdi BuscarDatosCfdi(int documentoId)
        {
            // Buscar el documento
            // Si el documento existe el SDK se posiciona en el registro
            ComercialSdk.fBuscarIdDocumento(documentoId).TirarSiEsError();

            // Declarar variables a leer de la base de datos
            var serieCertificadoEmisor = new StringBuilder(3000);
            var folioFiscalUUid = new StringBuilder(3000);
            var serieCertificadoSat = new StringBuilder(3000);
            var fecha = new StringBuilder(3000);
            var selloDigital = new StringBuilder(3000);
            var selloSat = new StringBuilder(3000);
            var cadenaOriginalComplementoSat = new StringBuilder(3000);
            var regimen = new StringBuilder(3000);
            var lugarExpedicion = new StringBuilder(3000);
            var moneda = new StringBuilder(3000);
            var folioFiscalOriginal = new StringBuilder(3000);
            var serieFolioFiscal = new StringBuilder(3000);
            var fechaFolioFiscal = new StringBuilder(3000);
            var montoFolioFiscal = new StringBuilder(3000);

            // Buscar los datos CFDI
            ComercialSdk.fGetDatosCFDI(serieCertificadoEmisor,
                    folioFiscalUUid,
                    serieCertificadoSat,
                    fecha,
                    selloDigital,
                    selloSat,
                    cadenaOriginalComplementoSat,
                    regimen,
                    lugarExpedicion,
                    moneda,
                    folioFiscalOriginal,
                    serieFolioFiscal,
                    fechaFolioFiscal,
                    montoFolioFiscal)
                .TirarSiEsError();

            // Instanciar un objecto DatosCfdi y asignar los valores de la base de datos
            return new DatosCfdi
            {
                SerieCertificadoEmisor = serieCertificadoEmisor.ToString(),
                FolioFiscalUUid = folioFiscalUUid.ToString(),
                SerieCertificadoSat = serieCertificadoSat.ToString(),
                Fecha = fecha.ToString(),
                SelloDigital = selloDigital.ToString(),
                SelloSat = selloSat.ToString(),
                CadenaOriginalComplementoSat = cadenaOriginalComplementoSat.ToString(),
                Regimen = regimen.ToString(),
                LugarExpedicion = lugarExpedicion.ToString(),
                Moneda = moneda.ToString(),
                FolioFiscalOriginal = folioFiscalOriginal.ToString(),
                SerieFolioFiscal = serieFolioFiscal.ToString(),
                FechaFolioFiscal = fechaFolioFiscal.ToString(),
                MontoFolioFiscal = montoFolioFiscal.ToString()
            };
        }

        /// <summary>
        ///     Genera el documento digital de un CFDI.
        /// </summary>
        /// <param name="codigoConceptoDocumento">El codigo de concepto del documento a generar.</param>
        /// <param name="serieDocumento">La serie del documento generar.</param>
        /// <param name="folioDocumento">El folio del documento a generar.</param>
        /// <param name="tipoArchivo">El tipo de archivo. 0 = XML, 1 = PDF</param>
        /// <param name="rutaPlantilla">Ruta de la plantilla cuando se genera el PDF.</param>
        public static void GenerarDocumentoDigital(string codigoConceptoDocumento, string serieDocumento, double folioDocumento, int tipoArchivo,
                                            string rutaPlantilla)
        {
            // Generar el documento digital del CFDI
            ComercialSdk.fEntregEnDiscoXML(codigoConceptoDocumento, serieDocumento, folioDocumento, tipoArchivo, rutaPlantilla)
                .TirarSiEsError();
        }


        /// <summary>
        /// Cancelar un documento.
        /// </summary>
        /// <param name="idDocumento">El id del documento a cancelar.</param>
        /// <param name="contrasenaCertificado">La contrasena del certificado.</param>
        /// <param name="motivoCancelacion">El codigo de motivo de cancelacion.</param>
        /// <param name="uuidRemplazo">El UUID de reemplazo si se requiere.</param>
        public static void CancelarDocumento(int idDocumento, string contrasenaCertificado, string motivoCancelacion, string uuidRemplazo)
        {
            // Buscar el documento
            ComercialSdk.fBuscarIdDocumento(idDocumento).TirarSiEsError();

            // Ingregar la contrasena del certificado
            ComercialSdk.fCancelaDoctoInfo(contrasenaCertificado).TirarSiEsError();

            // Cancelar el documento
            ComercialSdk.fCancelaDocumentoConMotivo(motivoCancelacion, uuidRemplazo).TirarSiEsError();
        }
    }
}
