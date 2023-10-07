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
        string OpenCaja(int NoCaja);
        string get_Producto(string Codigo, int Cantidad, int NoCaja);
    }

    [Guid("8cae3118-d62a-4984-ae0c-2f5dd7236015"),
     InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IComercialSdkEvents
    {
        string OpenCaja(int NoCaja);
        string get_Producto(string Codigo, int Cantidad, int NoCaja);
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

        [ComVisible(true)]
        public CaracteristicasSdk sdkCaracteristicas;
        [ComVisible(true)]
        public ClienteSdk sdkClientes;
        [ComVisible(true)]
        public ConceptoSdk sdkConceptos;
        [ComVisible(true)]
        public DatosCfdi sdkDatosCfdi;
        [ComVisible(true)]
        public DocumentoSdk sdkDocumentos;
        [ComVisible(true)]
        public MovimientoSdk sdkMovimientos;
        [ComVisible(true)]
        public ProductoSdk sdkProducto;
        /*** *************************************************** ***/
        public void open_SDK(string EmpresaDB)
        {
            try
            {
                var keySistema = Registry.LocalMachine.OpenSubKey(NombreLlaveRegistroComercial);
                var lEntrada = keySistema.GetValue("DirectorioBase");
                Directory.SetCurrentDirectory(lEntrada.ToString());
                ComercialSdk.fInicioSesionSDK("PUNTOVENTAS", "XiYeme=R6G");
                ComercialSdk.fInicioSesionSDKCONTPAQi("PUNTOVENTAS", "XiYeme=R6G");
                int nStart = ComercialSdk.fSetNombrePAQ(NombrePaqComercial);
                if (nStart != 0)
                {
                    StringBuilder strMensaje = new StringBuilder(512);
                    ComercialSdk.fError(nStart, strMensaje, 512);
                    Console_log("Error SDK [190]: " + strMensaje, EventLogEntryType.Error, 100);
                }
                else
                {
                    string rutaEMPRESA_COM = "C:\\Compac\\Empresas\\" + EmpresaDB;
                    Console_log("Abriendo la empresa: " + EmpresaDB + " en la ruta: " + rutaEMPRESA_COM, EventLogEntryType.Information, 100);
                    nStart = ComercialSdk.fAbreEmpresa(rutaEMPRESA_COM);
                    if (nStart != 0)
                    {
                        StringBuilder strMensaje = new StringBuilder(512);
                        ComercialSdk.fError(nStart, strMensaje, 512);
                        Console_log("Error SDK [191]: " + strMensaje, EventLogEntryType.Error, 100);
                    }
                }

            }
            catch (Exception ex)
            {
                Console_log(ex.Message + ";\nTrace:\n" + ex.StackTrace.ToString(), EventLogEntryType.Error, 100);
            }
            //finally { ComercialSdk.fCierraEmpresa(); ComercialSdk.fTerminaSDK(); }
        }
        public void open_SDK(int NoCaja)
        {
            try
            {
                MySQL dbMySQL = new MySQL();
                MySqlDataReader MyCajas = dbMySQL.execSQL("SELECT * FROM tpv_cajas WHERE id = " + NoCaja + ";");
                if (MyCajas.Read())
                {
                    var keySistema = Registry.LocalMachine.OpenSubKey(NombreLlaveRegistroComercial);
                    var lEntrada = keySistema.GetValue("DirectorioBase");
                    Directory.SetCurrentDirectory(lEntrada.ToString());
                    ComercialSdk.fInicioSesionSDK("PUNTOVENTAS", "XiYeme=R6G");
                    ComercialSdk.fInicioSesionSDKCONTPAQi("PUNTOVENTAS", "XiYeme=R6G");
                    int nStart = ComercialSdk.fSetNombrePAQ(NombrePaqComercial);
                    if (nStart != 0)
                    {
                        StringBuilder strMensaje = new StringBuilder(512);
                        ComercialSdk.fError(nStart, strMensaje, 512);
                        Console_log("Error SDK [191]: " + strMensaje, EventLogEntryType.Error, 100);
                    }
                    else
                    {
                        var EmpresaDB = MyCajas[3].ToString().Trim();
                        string rutaEMPRESA_COM = "C:\\Compac\\Empresas\\" + EmpresaDB;
                        Console_log("Abriendo la empresa: " + EmpresaDB + " en la ruta: " + rutaEMPRESA_COM, EventLogEntryType.Information, 110);
                        nStart = ComercialSdk.fAbreEmpresa(rutaEMPRESA_COM);
                        if (nStart != 0)
                        {
                            StringBuilder strMensaje = new StringBuilder(512);
                            ComercialSdk.fError(nStart, strMensaje, 512);
                            Console_log("Error SDK [191]: " + strMensaje, EventLogEntryType.Error, 100);
                        }
                    }
                }
                else
                {
                    Console_log("Code: 404\nNo es posible cargar Los datos de la Caja ("+ NoCaja + ").", EventLogEntryType.Error, 111);
                }
            }
            catch (Exception ex)
            {
                Console_log(ex.Message + ";\nTrace:\n" + ex.StackTrace.ToString(), EventLogEntryType.Error, 112);
            }
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
        /*** *************************************************** ***/
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
        /*** *************************************************** ***/
        [ComVisible(true)]
        public string OpenCaja(int NoCaja)
        {
            try
            {
                var ObjRESP = new Dictionary<string, string>();
                MySQL dbMySQL = new MySQL();
                MySqlDataReader MyCajas = dbMySQL.execSQL("SELECT * FROM tpv_cajas WHERE id = " + NoCaja + ";");
                if (MyCajas.Read())
                {
                    open_SDK(MyCajas[3].ToString().Trim());
                    ClienteSdk cPublicoGeneral = null; // ClienteSdk.BuscarClientePorCodigo("XAXX010101000");
                    if (cPublicoGeneral != null) //El cliente publico general existe
                    {
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
                    close_SDK();
                }
                dbMySQL.Desconectar();
                return JsonConvert.SerializeObject(ObjRESP);
            }
            catch (Exception ex) { 
                Console_log(ex.Message + ";\nTrace:\n" + ex.StackTrace.ToString(), EventLogEntryType.Error, 102);
                return ErrorCode;
            }
            finally { close_SDK(); }
        }
        /*** *************************************************** ***/
        [ComVisible(true)]
        public string get_Producto(string Codigo, int Cantidad, int NoCaja)
        {
            try
            {
                open_SDK(NoCaja);
                var ObjRESP = new Dictionary<string, string>();
                ProductoSdk MyProducto = ProductoSdk.BuscarProductoPorCodigo(Codigo.ToString());
                if (MyProducto.Codigo != "") //Producto encontrado
                {
                    if (MyProducto.Estado == "1") //Producto activo
                    {
                        ObjRESP.Add("Code", "0");
                        //Código
                        ObjRESP.Add("Codigo", MyProducto.Codigo);
                        //Nombre
                        ObjRESP.Add("Nombre", MyProducto.Nombre);
                        //Precio Unitario
                        ObjRESP.Add("Precio", MyProducto.Precio.ToString("N", new CultureInfo("es-MX")));
                        //Total con IVA
                        ObjRESP.Add("Total", (Convert.ToSingle(Cantidad.ToString()) * MyProducto.Precio).ToString("N", new CultureInfo("es-MX")));

                        //Buscamos los almacenes que se encuentran asignados a la caja
                        MySQL dbMySQL = new MySQL();
                        MySqlDataReader MyAlmacenes = dbMySQL.execSQL("SELECT * FROM tpv_cajas_almacenes WHERE id_caja = " + NoCaja + ";");
                        while (MyAlmacenes.Read())//Buscamos si la caja tiene almacenes
                        {
                            lista_Almacenes.Add(MyAlmacenes["id_SDK"].ToString());
                        }
                        dbMySQL.Desconectar();

                        //Asignando las caracteristicas del producto en su caso.
                        if ((MyProducto.Caracteristicas.existCaracteristica1) || (MyProducto.Caracteristicas.existCaracteristica2) || (MyProducto.Caracteristicas.existCaracteristica3))
                        {
                            //Tiene caracteristicas
                            ObjRESP.Add("Caracteristicas", "1");
                            if (MyProducto.Caracteristicas.nameCaracteristica1.ToString() != "")
                            {
                                ObjRESP.Add("C1_NAME", MyProducto.Caracteristicas.nameCaracteristica1.ToString());
                                ObjRESP.Add("C1_VALUES", JsonConvert.SerializeObject(MyProducto.Caracteristicas.listaCaracteristica1, JsonSettingsHTML));
                            }
                            if (MyProducto.Caracteristicas.nameCaracteristica2.ToString() != "")
                            {
                                ObjRESP.Add("C2_NAME", MyProducto.Caracteristicas.nameCaracteristica2.ToString());
                                ObjRESP.Add("C2_VALUES", JsonConvert.SerializeObject(MyProducto.Caracteristicas.listaCaracteristica2, JsonSettingsHTML));
                            }
                            if (MyProducto.Caracteristicas.nameCaracteristica3.ToString() != "")
                            {
                                ObjRESP.Add("C3_NAME", MyProducto.Caracteristicas.nameCaracteristica3.ToString());
                                ObjRESP.Add("C3_VALUES", JsonConvert.SerializeObject(MyProducto.Caracteristicas.listaCaracteristica3, JsonSettingsHTML));
                            }
                        }
                        else
                        {
                            ObjRESP.Add("Caracteristicas", "0");
                            ObjRESP.Add("C1_NAME", "");
                            ObjRESP.Add("C1_VALUES", "");
                            ObjRESP.Add("C2_NAME", "");
                            ObjRESP.Add("C2_VALUES", "");
                            ObjRESP.Add("C3_NAME", "");
                            ObjRESP.Add("C3_VALUES", "");

                        }

                    }
                    else
                    {
                        //El producto no está activo
                        ObjRESP.Add("Code", "1997");
                        Console_log("El producto (" + Codigo + ") no está activo.", EventLogEntryType.Error, 1000);
                    }
                }
                else
                {
                    //El producto no se encuentra
                    ObjRESP.Add("Code", "1996");
                    Console_log("El producto (" + Codigo + ") no se encuentra registrado.", EventLogEntryType.Error, 1000);

                }

                close_SDK();
                return JsonConvert.SerializeObject(ObjRESP);

            }
            catch (Exception ex)
            {
                Console_log(ex.Message + ";\nTrace:\n" + ex.StackTrace.ToString(), EventLogEntryType.Error, 1000);
                return ErrorCode;
            }
            finally { close_SDK(); }

        }
        /*** *************************************************** ***/
        public string get_Existencia(string Codigo, int Cantidad, int NoCaja)
        {
            try
            {
                open_SDK(NoCaja);
                var ObjRESP = new Dictionary<string, string>();


                close_SDK();
                return JsonConvert.SerializeObject(ObjRESP);

            }
            catch (Exception ex)
            {
                Console_log(ex.Message + ";\nTrace:\n" + ex.StackTrace.ToString(), EventLogEntryType.Error, 1001);
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
    }
}
