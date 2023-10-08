using ARSoftware.Contpaqi.Comercial.Sdk;
using ARSoftware.Contpaqi.Comercial.Sdk.Constantes;
using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;
using ARSoftware.Contpaqi.Comercial.Sdk.Extensiones;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    [Guid("af8e9c87-4138-472d-b5e9-cb5c165b3807")]
    public interface IAlmacenSdk
    {
        void setNoCaja(string xCaja);
        string BuscarAlmacenPorCodigo(string CodigoAlmacen);
        string BuscarAlmacenPorId(int IdAlmacen);
        string BuscarTodosAlmacenes();
    }
    /** ********************************************************************* **/
    /** INTERFACE PARA EVENTOS DE LA CLASE **/
    /** ********************************************************************* **/
    [Guid("76f1a569-91cd-4702-a321-a14b49982993"),
     InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IAlmacenSdkEvents
    {
        void setNoCaja(string xCaja);
        string BuscarAlmacenPorCodigo(string CodigoAlmacen);
        string BuscarAlmacenPorId(int IdAlmacen);
        string BuscarTodosAlmacenes();
    }
    /** ********************************************************************* **/
    /** CLASE PRINCIPAL DE IMPLEMENTACIÓN  **/
    /** ********************************************************************* **/
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [ComSourceInterfaces(typeof(IProductoSdkEvents))]
    [ProgId("ContPAQi.AlmacenSdk")]
    public class AlmacenSdk : IAlmacenSdk
    {
        public EventLog eventLog = new EventLog();
        public Dictionary<string, string> ObjRESP = new Dictionary<string, string>();
        public const string ErrorCode = "{ \"Code\": 404 }";
        public const string NombreLlaveRegistroComercial = @"SOFTWARE\\WOW6432Node\\Computación en Acción, SA CV\\CONTPAQ I COMERCIAL";
        public const string NombreLlaveRegistroContable = @"SOFTWARE\\Wow6432Node\\Computación en Acción, SA CV\\CONTPAQ I";
        public const string NombrePaqComercial = "CONTPAQ I COMERCIAL";
        public const string NombrePaqContable = "CONTPAQ I CONTABILIDAD";
        public JsonSerializerSettings JsonSettingsHTML = new JsonSerializerSettings
        {
            StringEscapeHandling = StringEscapeHandling.EscapeNonAscii
        };

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string NoCaja { get; set; }
        public void setNoCaja(string xCaja)
        {
            this.NoCaja = xCaja;
        }

        private AlmacenSdk LeerDatosAlmacen()
        {
            Dictionary<string, string> objAlmacen = new Dictionary<string, string>();
            // Declarar variables a leer de la base de datos
            var idBd = new StringBuilder(3000);
            var codigoBd = new StringBuilder(3000);
            var nombreBd = new StringBuilder(3000);

            // Leer los datos del registro donde el SDK esta posicionado
            ComercialSdk.fLeeDatoAlmacen("CIDALMACEN", idBd, 3000);
            ComercialSdk.fLeeDatoAlmacen("CCODIGOALMACEN", codigoBd, 3000);
            ComercialSdk.fLeeDatoAlmacen("CNOMBREALMACEN", nombreBd, 3000);

            // Instanciar un almacen y asignar los datos de la base de datos
            return new AlmacenSdk
            {
                Id = int.Parse(idBd.ToString()),
                Codigo = codigoBd.ToString(),
                Nombre = nombreBd.ToString()
            };
        }
        public string BuscarAlmacenPorId(int IdAlmacen)
        {
            try
            {
                open_SDK();
                ComercialSdk.fBuscaIdAlmacen(IdAlmacen).TirarSiEsError();
                AlmacenSdk MyAlmacen = LeerDatosAlmacen();
                close_SDK();
                ObjRESP.Add("Id", MyAlmacen.Id.ToString());
                ObjRESP.Add("Codigo", MyAlmacen.Codigo.ToString());
                ObjRESP.Add("Nombre", MyAlmacen.Nombre.ToString());

                return JsonConvert.SerializeObject(ObjRESP, JsonSettingsHTML);
            }
            catch (Exception ex)
            {
                Console_log(ex.Message + ";\nTrace:\n" + ex.StackTrace.ToString(), EventLogEntryType.Error, 1000);
                return null;
            }
            finally { ComercialSdk.fCierraEmpresa(); ComercialSdk.fTerminaSDK(); }

        }
        public string BuscarAlmacenPorCodigo(string CodigoAlmacen)
        {
            try
            {
                open_SDK();
                ComercialSdk.fBuscaAlmacen(CodigoAlmacen).TirarSiEsError();
                AlmacenSdk MyAlmacen = LeerDatosAlmacen();
                close_SDK();
                ObjRESP.Add("Id", MyAlmacen.Id.ToString());
                ObjRESP.Add("Codigo", MyAlmacen.Codigo.ToString());
                ObjRESP.Add("Nombre", MyAlmacen.Nombre.ToString());

                return JsonConvert.SerializeObject(ObjRESP, JsonSettingsHTML);
            }
            catch (Exception ex)
            {
                Console_log(ex.Message + ";\nTrace:\n" + ex.StackTrace.ToString(), EventLogEntryType.Error, 1000);
                return null;
            }
            finally { ComercialSdk.fCierraEmpresa(); ComercialSdk.fTerminaSDK(); }

        }
        public string BuscarTodosAlmacenes()
        {
            try
            {
                AlmacenSdk MyAlmacen = new AlmacenSdk();
                List<string> ListaAlmacenes = new List<string>();
                Dictionary<string, string> objALMACEN = new Dictionary<string, string>();
                open_SDK();
                //Posicionamos en el primer almacen y leemos sus datos
                ComercialSdk.fPosPrimerAlmacen();
                MyAlmacen = LeerDatosAlmacen();
                objALMACEN.Add("Id", MyAlmacen.Id.ToString());
                objALMACEN.Add("Codigo", MyAlmacen.Codigo.ToString());
                objALMACEN.Add("Nombre", MyAlmacen.Nombre.ToString());
                ListaAlmacenes.Add(JsonConvert.SerializeObject(objALMACEN, JsonSettingsHTML));
                objALMACEN.Clear();
                // Crear un loop y posicionar el SDK en el siguiente registro
                while (ComercialSdk.fPosSiguienteAlmacen() == SdkConstantes.CodigoExito)
                {
                    MyAlmacen = LeerDatosAlmacen();
                    objALMACEN.Add("Id", MyAlmacen.Id.ToString());
                    objALMACEN.Add("Codigo", MyAlmacen.Codigo.ToString());
                    objALMACEN.Add("Nombre", MyAlmacen.Nombre.ToString());
                    ListaAlmacenes.Add(JsonConvert.SerializeObject(objALMACEN, JsonSettingsHTML));
                    objALMACEN.Clear();

                    if (ComercialSdk.fPosEOFAlmacen() == 1)
                        break;
                }

                return JsonConvert.SerializeObject(ListaAlmacenes, JsonSettingsHTML);
            }
            catch (Exception ex)
            {
                Console_log(ex.Message + ";\nTrace:\n" + ex.StackTrace.ToString(), EventLogEntryType.Error, 1000);
                return null;
            }
            finally { ComercialSdk.fCierraEmpresa(); ComercialSdk.fTerminaSDK(); }

        }

        /** ********************************************************************* **/
        /** FUNCIONES DE INTER-CONECTIVIDAD **/
        /** ********************************************************************* **/
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
                    Console_log("La caja " +this.NoCaja+ " abre la empresa: " + EmpresaDB + " en la ruta: " + rutaEMPRESA_COM, EventLogEntryType.Information, 100);
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
