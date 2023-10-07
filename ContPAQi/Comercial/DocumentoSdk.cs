using ARSoftware.Contpaqi.Comercial.Sdk;
using ARSoftware.Contpaqi.Comercial.Sdk.Constantes;
using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;
using ARSoftware.Contpaqi.Comercial.Sdk.Extensiones;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ContPAQi
{
    /** ********************************************************************* **/
    /** INTERFACE PRINCIPAL DE LA CLASE **/
    /** ********************************************************************* **/
    [Guid("587a8148-d363-4635-bd16-ad4c5033aa89")]
    public interface IDocumentoSdk
    {
        string BuscarSiguienteSerieYFolio(int NoCaja);
    }
    /** ********************************************************************* **/
    /** INTERFACE PARA EVENTOS DE LA CLASE **/
    /** ********************************************************************* **/
    [Guid("c36067a6-06fe-4568-811f-6c02985e10df"),
     InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IDocumentoSdkEvents
    {
        string BuscarSiguienteSerieYFolio(int NoCaja);
    }
    /** ********************************************************************* **/
    /** CLASE PRINCIPAL DE IMPLEMENTACIÓN  **/
    /** ********************************************************************* **/
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [ComSourceInterfaces(typeof(IDocumentoSdkEvents))]
    [ProgId("ContPAQi.DocumentoSdk")]
    public class DocumentoSdk : IDocumentoSdk
    {
        public EventLog eventLog = new EventLog();
        public Dictionary<string, string> ObjRESP = new Dictionary<string, string>();
        public const string ErrorCode = "{ \"Code\": 404 }";
        public const string NombreLlaveRegistroComercial = @"SOFTWARE\\WOW6432Node\\Computación en Acción, SA CV\\CONTPAQ I COMERCIAL";
        public const string NombreLlaveRegistroContable = @"SOFTWARE\\Wow6432Node\\Computación en Acción, SA CV\\CONTPAQ I";
        public const string NombrePaqComercial = "CONTPAQ I COMERCIAL";
        public const string NombrePaqContable = "CONTPAQ I CONTABILIDAD";
        public List<string> lista_Almacenes = new List<string>();
        public SqlConnection SQLConexion = new SqlConnection(@"Data Source = DC-CONTABLE\COMPAC; Initial Catalog = cmPuntoVentas; User ID = sa; Password = Promo1002##");
        public JsonSerializerSettings JsonSettingsHTML = new JsonSerializerSettings
        {
            StringEscapeHandling = StringEscapeHandling.EscapeNonAscii
        };

        public int Id { get; set; }
        public int ConceptoId { get; set; }
        public string Serie { get; set; }
        public double Folio { get; set; }
        public DateTime Fecha { get; set; }
        public int ClienteId { get; set; }
        public string Referencia { get; set; }
        public string Observaciones { get; set; }
        public double Total { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Fecha:MM/dd/yyyy} - {ConceptoSdk.BuscarConceptoPorId(ConceptoId).Nombre} - {Serie} - {Folio} - {ClienteSdk.BuscarClientePorId(ClienteId).RazonSocial} - {Total:C}";
        }

        private static DocumentoSdk LeerDatosDocumento()
        {
            // Declarar variables a leer de la base de datos
            var idBd = new StringBuilder(3000);
            var conceptoIdBd = new StringBuilder(3000);
            var serieBd = new StringBuilder(3000);
            var folioBd = new StringBuilder(3000);
            var fechaBd = new StringBuilder(3000);
            var clienteIdBd = new StringBuilder(3000);
            var referenciaBd = new StringBuilder(3000);
            var observacionesBd = new StringBuilder(3000);
            var totalBd = new StringBuilder(3000);

            // Leer los datos del registro donde el SDK esta posicionado
            ComercialSdk.fLeeDatoDocumento("CIDDOCUMENTO", idBd, 3000).TirarSiEsError();
            ComercialSdk.fLeeDatoDocumento("CIDCONCEPTODOCUMENTO", conceptoIdBd, 3000).TirarSiEsError();
            ComercialSdk.fLeeDatoDocumento("CSERIEDOCUMENTO", serieBd, 3000).TirarSiEsError();
            ComercialSdk.fLeeDatoDocumento("CFOLIO", folioBd, 3000).TirarSiEsError();
            ComercialSdk.fLeeDatoDocumento("CFECHA", fechaBd, 3000).TirarSiEsError();
            ComercialSdk.fLeeDatoDocumento("CIDCLIENTEPROVEEDOR", clienteIdBd, 3000).TirarSiEsError();
            ComercialSdk.fLeeDatoDocumento("CREFERENCIA", referenciaBd, 3000).TirarSiEsError();
            ComercialSdk.fLeeDatoDocumento("COBSERVACIONES", observacionesBd, 3000).TirarSiEsError();
            ComercialSdk.fLeeDatoDocumento("CTOTAL", totalBd, 3000).TirarSiEsError();

            // Instanciar un documento y asignar los datos de la base de datos
            return new DocumentoSdk
            {
                Id = int.Parse(idBd.ToString()),
                ConceptoId = int.Parse(conceptoIdBd.ToString()),
                Serie = serieBd.ToString(),
                Folio = double.Parse(folioBd.ToString()),
                Fecha = DateTime.ParseExact(fechaBd.ToString(), FormatosFechaSdk.Comercial, null),
                ClienteId = int.Parse(clienteIdBd.ToString()),
                Referencia = referenciaBd.ToString(),
                Observaciones = observacionesBd.ToString(),
                Total = double.Parse(totalBd.ToString())
            };
        }
        public static DocumentoSdk BuscarDocumentoPorId(int documentoId)
        {
            // Buscar el documento por id
            // Si el documento existe el SDK se posiciona en el registro
            ComercialSdk.fBuscarIdDocumento(documentoId).TirarSiEsError();

            // Leer los datos del registro donde el SDK esta posicionado
            return LeerDatosDocumento();
        }
        public static DocumentoSdk BuscarDocumentoPorLlave(string codigoConcepto, string serie, string folio)
        {
            // Buscar el documento por llave
            // Si el documento existe el SDK se posiciona en el registro
            ComercialSdk.fBuscarDocumento(codigoConcepto, serie, folio).TirarSiEsError();

            // Leer los datos del registro donde el SDK esta posicionado
            return LeerDatosDocumento();
        }
        public static List<DocumentoSdk> BuscarDocumentosPorFiltro(DateTime fechaInicio,
                                                                   DateTime fechaFin,
                                                                   string codigoConcepto,
                                                                   string codigoClienteProveedor)
        {
            var documentosList = new List<DocumentoSdk>();

            // Cancelar filtro
            ComercialSdk.fCancelaFiltroDocumento().TirarSiEsError();

            // Filtrar documentos
            ComercialSdk.fSetFiltroDocumento(fechaInicio.ToString(FormatosFechaSdk.Fecha),
                    fechaFin.ToString(FormatosFechaSdk.Fecha),
                    codigoConcepto,
                    codigoClienteProveedor)
                .TirarSiEsError();

            // Posicionar el SDK en el primer registro
            ComercialSdk.fPosPrimerDocumento().TirarSiEsError();

            // Leer los datos del registro donde el SDK esta posicionado
            documentosList.Add(LeerDatosDocumento());

            // Crear un loop y posicionar el SDK en el siguiente registro
            while (ComercialSdk.fPosSiguienteDocumento() == SdkConstantes.CodigoExito)
            {
                // Leer los datos del registro donde el SDK esta posicionado
                documentosList.Add(LeerDatosDocumento());

                // Checar si el SDK esta posicionado en el ultimo registro
                // Si el SDK esta posicionado en el ultimo registro salir del loop
                if (ComercialSdk.fPosEOF() == 1)
                    break;
            }

            // Cancelar filtro
            ComercialSdk.fCancelaFiltroDocumento().TirarSiEsError();

            return documentosList;
        }
        public static tLlaveDoc BuscarSiguienteSerieYFolio(string codigoConcepto)
        {
            // Declarar variables a asignar por el SDK
            double folioBd = 0;
            var serieBd = new StringBuilder();

            // Buscar el siguiente folio
            ComercialSdk.fSiguienteFolio(codigoConcepto, serieBd, ref folioBd).TirarSiEsError();

            // Instanciar una llave y asignar los datos asignados por el SDK
            return new tLlaveDoc { aCodConcepto = codigoConcepto, aSerie = serieBd.ToString(), aFolio = folioBd };
        }

        /** ********************************************************************* **/
        [ComVisible(true)]
        public string BuscarSiguienteSerieYFolio(int NoCaja)
        {
            try
            {
                var ObjRESP = new Dictionary<string, string>();
                MySQL dbMySQL = new MySQL();
                MySqlDataReader MyCajas = dbMySQL.execSQL("SELECT * FROM tpv_cajas WHERE id = " + NoCaja + ";");
                if (MyCajas.Read())
                {
                    open_SDK(MyCajas[3].ToString().Trim());
                    //Buscando el folio y la serie de la proxima remisión
                    tLlaveDoc MySerieFolio = DocumentoSdk.BuscarSiguienteSerieYFolio(MyCajas[4].ToString().Trim());
                    ObjRESP.Add("Serie", MySerieFolio.aSerie.ToString());
                    ObjRESP.Add("Folio", MySerieFolio.aFolio.ToString());
                    dbMySQL.Desconectar();
                    close_SDK();
                    return JsonConvert.SerializeObject(ObjRESP, JsonSettingsHTML);
                }
                else
                {
                    Console_log("No se puede conectar con tpv_cajas", EventLogEntryType.Error, 102);
                    dbMySQL.Desconectar();
                    return ErrorCode;
                }
            }
            catch (Exception ex)
            {
                Console_log(ex.Message + ";\nTrace:\n" + ex.StackTrace.ToString(), EventLogEntryType.Error, 102);
                return ErrorCode;
            }
            finally { close_SDK(); }
        }
        /** ********************************************************************* **/

        public static int CrearDocumento(DocumentoSdk documento)
        {
            ConceptoSdk concepto = ConceptoSdk.BuscarConceptoPorId(documento.ConceptoId);
            ClienteSdk cliente = null; // ClienteSdk.BuscarClientePorId(documento.ClienteId);

            // Instanciar un documento con la estructura tDocumento del SDK
            var nuevoDocumento = new tDocumento
            {
                aCodConcepto = concepto.Codigo,
                aSerie = documento.Serie,
                aFolio = documento.Folio,
                aFecha = documento.Fecha.ToString(FormatosFechaSdk.Fecha),
                aCodigoCteProv = cliente.Codigo,
                aNumMoneda = 1,
                aTipoCambio = 1,
                aReferencia = documento.Referencia
            };

            // Declarar una variable donde se asignara el id del documento nuevo
            var nuevoDocumentoId = 0;

            // Crear documento nuevo
            ComercialSdk.fAltaDocumento(ref nuevoDocumentoId, ref nuevoDocumento).TirarSiEsError();

            documento.Id = nuevoDocumentoId;

            // Editar los datos extra que no son parte de la estructura tDocumento
            ActualizarDocumento(documento);

            return nuevoDocumentoId;
        }
        public static int CrearDocumentoCargoAbono(DocumentoSdk documento)
        {
            ConceptoSdk concepto = ConceptoSdk.BuscarConceptoPorId(documento.ConceptoId);
            ClienteSdk cliente = null; // ClienteSdk.BuscarClientePorId(documento.ClienteId);

            // Instanciar un documento con la estructura tDocumento del SDK
            var nuevoDocumento = new tDocumento
            {
                aCodConcepto = concepto.Codigo,
                aSerie = documento.Serie,
                aFolio = documento.Folio,
                aFecha = documento.Fecha.ToString(FormatosFechaSdk.Fecha),
                aCodigoCteProv = cliente.Codigo,
                aNumMoneda = 1,
                aTipoCambio = 1,
                aReferencia = documento.Referencia,
                aImporte = documento.Total
            };

            // Crear documento de cargo o abono
            ComercialSdk.fAltaDocumentoCargoAbono(ref nuevoDocumento).TirarSiEsError();

            // Buscar el id del documento.
            var idBd = new StringBuilder(3000);
            ComercialSdk.fLeeDatoDocumento("CIDDOCUMENTO", idBd, 3000).TirarSiEsError();
            int nuevoDocumentoId = int.Parse(idBd.ToString());

            documento.Id = nuevoDocumentoId;

            // Actualizar los datos extra que no son parte de la estructura tDocumento
            ActualizarDocumento(documento);

            return nuevoDocumentoId;
        }
        public static void ActualizarDocumento(DocumentoSdk documento)
        {
            // Buscar el documento
            // Si el documento existe el SDK se posiciona en el registro
            ComercialSdk.fBuscarIdDocumento(documento.Id).TirarSiEsError();

            // Activar el modo de edición
            ComercialSdk.fEditarDocumento().TirarSiEsError();

            // Actualizar los campos del registro donde el SDK esta posicionado
            ComercialSdk.fSetDatoDocumento("COBSERVACIONES", documento.Observaciones).TirarSiEsError();

            // Guardar los cambios realizados al registro
            ComercialSdk.fGuardaDocumento().TirarSiEsError();
        }
        public static void EliminarDocumento(DocumentoSdk documento)
        {
            // Buscar el documento
            // Si el documento existe el SDK se posiciona en el registro
            ComercialSdk.fBuscarIdDocumento(documento.Id).TirarSiEsError();

            // Eliminar el documento
            ComercialSdk.fBorraDocumento().TirarSiEsError();
        }
        public static void SaldarDocumento(tLlaveDoc documentoAPagar, tLlaveDoc documentoPago, double importe, DateTime fecha)
        {
            ComercialSdk.fSaldarDocumento(ref documentoAPagar, ref documentoPago, importe, 1, fecha.ToString(FormatosFechaSdk.Fecha)).TirarSiEsError();
        }


        /** ********************************************************************* **/
        /** FUNCIONES DE INTER-CONECTIVIDAD **/
        /** ********************************************************************* **/
        public void open_SDK(string NoCaja)
        {
            try
            {
                var keySistema = Registry.LocalMachine.OpenSubKey(NombreLlaveRegistroComercial);
                var lEntrada = keySistema.GetValue("DirectorioBase");
                Directory.SetCurrentDirectory(lEntrada.ToString());
                ComercialSdk.fInicioSesionSDK("PUNTOVENTAS", "XiYeme=R6G");
                ComercialSdk.fInicioSesionSDKCONTPAQi("PUNTOVENTAS", "XiYeme=R6G");
                int nStart = ComercialSdk.fSetNombrePAQ(NombrePaqComercial);
                if (nStart != 0) //Hay un error al conectar con el PAQ
                {
                    StringBuilder strMensaje = new StringBuilder(512);
                    ComercialSdk.fError(nStart, strMensaje, 512);
                    Console_log("Error SDK [190]: " + nStart.ToString() + strMensaje.ToString(), EventLogEntryType.Error, 100);
                }

                MySQL dbMySQL = new MySQL();
                MySqlDataReader MyCajas = dbMySQL.execSQL("SELECT * FROM tpv_cajas WHERE id = " + NoCaja + ";");
                if (MyCajas.Read())
                {
                    var EmpresaDB = MyCajas[3].ToString().Trim();
                    string rutaEMPRESA_COM = "C:\\Compac\\Empresas\\" + EmpresaDB;
                    Console_log("Abriendo la empresa: " + EmpresaDB + " en la ruta: " + rutaEMPRESA_COM, EventLogEntryType.Information, 100);
                    nStart = ComercialSdk.fAbreEmpresa(rutaEMPRESA_COM);
                    if (nStart != 0)
                    {
                        StringBuilder strMensaje = new StringBuilder(512);
                        ComercialSdk.fError(nStart, strMensaje, 512);
                        Console_log("Error SDK [191]: " + nStart.ToString() + strMensaje.ToString(), EventLogEntryType.Error, 100);
                    }
                }
                else
                {
                    Console_log("Code: 404\nNo es posible cargar Los datos de la Caja (" + NoCaja + ").", EventLogEntryType.Error, 111);
                }
            }
            catch (Exception ex)
            {
                Console_log(ex.Message + ";\nTrace:\n" + ex.StackTrace.ToString(), EventLogEntryType.Error, 100);
            }
            //finally { ComercialSdk.fCierraEmpresa(); ComercialSdk.fTerminaSDK(); }
        }
        public void close_SDK()
        {
            try
            {
                ComercialSdk.fCierraEmpresa();
                ComercialSdk.fTerminaSDK();
            }
            catch (Exception ex) { Console_log(ex.Message + ";\nTrace:\n" + ex.StackTrace.ToString(), EventLogEntryType.Error, 102); }
            finally { ComercialSdk.fCierraEmpresa(); ComercialSdk.fTerminaSDK(); }
        }
        public void Console_log(string Text_Message, EventLogEntryType TypeE, int EventID)
        {
            try
            {
                if (!EventLog.SourceExists("ContPAQi Web"))
                {
                    EventLog.CreateEventSource("ContPAQi Web", "ContPAQi Web");
                    eventLog.Source = "ContPAQi Web";
                }
                else
                {
                    eventLog.Source = "ContPAQi Web";
                }
                eventLog.WriteEntry(Text_Message, TypeE, EventID, 1);
            }
            catch
            {
                eventLog.Source = "Application";
            }
        }
    }
}
