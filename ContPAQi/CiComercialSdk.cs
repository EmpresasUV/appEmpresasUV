using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using ARSoftware.Contpaqi.Comercial.Sdk;
using ARSoftware.Contpaqi.Comercial.Sdk.Constantes;
using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;
using ARSoftware.Contpaqi.Comercial.Sdk.Extensiones;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace ContPAQi
{
    [Guid("2412a43b-c437-4141-9953-4a8685d5ece3")]
    public interface IComercialSdk
    {
        void setNoCaja(string xCaja);
        string OpenCaja();
    }

    [Guid("8cae3118-d62a-4984-ae0c-2f5dd7236015"),
     InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IComercialSdkEvents
    {
        void setNoCaja(string xCaja);
        string OpenCaja();
    }

    [ComVisible(true)]
    [Guid("9e2f0d24-4261-46b6-a6b8-96c24192c2fa")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComSourceInterfaces(typeof(IComercialSdkEvents))]
    [ProgId("ContPAQi.CiComercialSdk")]
    public class CiComercialSdk : IComercialSdk
    {
        public EventLog eventLog = new EventLog();
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
        public string NoCaja { get; set; }
        public void setNoCaja(string xCaja)
        {
            this.NoCaja = xCaja;
        }

        /*** *************************************************** ***/
        [ComVisible(true)]
        public string OpenCaja()
        {
            try
            {
                var ObjRESP = new Dictionary<string, string>();
                MySQL dbMySQL = new MySQL();
                MySqlDataReader MyCajas = dbMySQL.execSQL("SELECT * FROM tpv_cajas WHERE id = " + this.NoCaja + ";");
                if (MyCajas.Read())
                {
                    //Console_log("0", EventLogEntryType.Warning, 102);
                    open_SDK();
                    //Console_log("1", EventLogEntryType.Warning, 102);
                    ClienteSdk cPublicoGeneral = new ClienteSdk().BuscarClientePorCodigo("XAXX010101000");
                    if (cPublicoGeneral != null) //El cliente publico general existe
                    {
                        //Console_log("2", EventLogEntryType.Warning, 102);
                        //Buscando el folio y la serie de la proxima remisión
                        tLlaveDoc MySerieFolio = DocumentoSdk.BuscarSiguienteSerieYFolio(MyCajas[4].ToString().Trim());
                        ObjRESP.Add("Serie", MySerieFolio.aSerie.ToString());
                        ObjRESP.Add("Folio", MySerieFolio.aFolio.ToString());
                        ObjRESP.Add("cliente_RFC", cPublicoGeneral.Rfc.ToString());
                        ObjRESP.Add("cliente_RAZON", cPublicoGeneral.RazonSocial.ToString());
                    }
                    else
                    {
                        Console_log("Code: 404\nNo es posible cargar el cliente PUBLICO EN GENERAL (XAXX010101000).\nVerifique que está dado de alta en la empresa.\nEmpresa: " + MyCajas[3].ToString().Trim() + "\nConcepto:" + MyCajas[4].ToString().Trim(), EventLogEntryType.Error, 102);
                        return ErrorCode;
                    }
                    //Console_log("3", EventLogEntryType.Warning, 102);
                    close_SDK();
                }
                dbMySQL.Desconectar();
                //Console_log("4", EventLogEntryType.Warning, 102);
                return JsonConvert.SerializeObject(ObjRESP);
            }
            catch (Exception ex) { 
                Console_log(ex.Message + "\n\nTrace:\n" + ex.StackTrace.ToString(), EventLogEntryType.Error, 102);
                return ErrorCode;
            }
            finally { close_SDK(); }
        }
        /*** *************************************************** ***/
        /*** *************************************************** ***/
        /*** *************************************************** ***/
        /*** *************************************************** ***/
        /*** *************************************************** ***/
        /*** *************************************************** ***/
        /*** *************************************************** ***/
        /*** *************************************************** ***/
        /*** *************************************************** ***/
        /*** *************************************************** ***/
        /*** *************************************************** ***/
        /*** *************************************************** ***/
        /*** *************************************************** ***/
        /*** *************************************************** ***/
        /*** *************************************************** ***/
        /*** *************************************************** ***/
        /*** *************************************************** ***/
        /*** *************************************************** ***/
        /*** *************************************************** ***/


        /*** *************************************************** ***/
        public void open_SDK()
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
                MySqlDataReader MyCajas = dbMySQL.execSQL("SELECT * FROM tpv_cajas WHERE id = " + this.NoCaja + ";");
                if (MyCajas.Read())
                {
                    var EmpresaDB = MyCajas[3].ToString().Trim();
                    string rutaEMPRESA_COM = "C:\\Compac\\Empresas\\" + EmpresaDB;
                    Console_log("La caja " + this.NoCaja + " abre la empresa: " + EmpresaDB + " en la ruta: " + rutaEMPRESA_COM, EventLogEntryType.Information, 100);
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
