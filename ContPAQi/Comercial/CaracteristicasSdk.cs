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
    [Guid("4480d1a4-4fe3-4a82-ab74-e9f799c5b3e1")]
    public interface ICaracteristicasSdk
    {

    }
    /** ********************************************************************* **/
    /** INTERFACE PARA EVENTOS DE LA CLASE **/
    /** ********************************************************************* **/
    [Guid("80abb718-c5a6-4190-8883-f89e5f867f41"),
     InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface ICaracteristicasSdkEvents
    {

    }
    /** ********************************************************************* **/
    /** CLASE PRINCIPAL DE IMPLEMENTACIÓN  **/
    /** ********************************************************************* **/
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [ComSourceInterfaces(typeof(ICaracteristicasSdkEvents))]
    [ProgId("ContPAQi.CaracteristicasSdk")]
    public class CaracteristicasSdk : ICaracteristicasSdk
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

        //public SqlConnection SQLConexion = new SqlConnection(@"Data Source = DC-CONTABLE\COMPAC; Initial Catalog = cmPuntoVentas; User ID = sa; Password = Promo1002##");
        public int Id_Producto { get; set; }
        public string nameCaracteristica1 { get; set; }
        public string valorCaracteristica1 { get; set; }
        public bool existCaracteristica1 { get; set; }
        public string nameCaracteristica2 { get; set; }
        public string valorCaracteristica2 { get; set; }
        public bool existCaracteristica2 { get; set; }
        public string nameCaracteristica3 { get; set; }
        public string valorCaracteristica3 { get; set; }
        public bool existCaracteristica3 { get; set; }
        
        public Dictionary<string, string> listaCaracteristica1 = new Dictionary<string, string>();
        public Dictionary<string, string> listaCaracteristica2 = new Dictionary<string, string>();
        public Dictionary<string, string> listaCaracteristica3 = new Dictionary<string, string>();

        public CaracteristicasSdk(int Id_DB)
        {
            Id_Producto = Id_DB;
            existCaracteristica1 = false;
            existCaracteristica2 = false;
            existCaracteristica3 = false;

            var Car1Bd = new StringBuilder(3000);
            var Car2Bd = new StringBuilder(3000);
            var Car3Bd = new StringBuilder(3000);
            ComercialSdk.fLeeDatoProducto("CIDPADRECARACTERISTICA1", Car1Bd, 3000);
            ComercialSdk.fLeeDatoProducto("CIDPADRECARACTERISTICA2", Car2Bd, 3000);
            ComercialSdk.fLeeDatoProducto("CIDPADRECARACTERISTICA3", Car3Bd, 3000);

            //Analizando las caracteristicas 1
            if (double.Parse(Car1Bd.ToString()) > 0) //Tiene caracteristica 1
            {
                existCaracteristica1 = true;
                SQLConexion.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM admCaracteristicas WHERE CIDPADRECARACTERISTICA=@padre", SQLConexion);
                command.Parameters.AddWithValue("@padre", double.Parse(Car1Bd.ToString()));
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    nameCaracteristica1 = String.Format("{0}", reader["CNOMBRECARACTERISTICA"]);
                    var id_valor = reader["CIDPADRECARACTERISTICA"];
                    reader.Close();
                    SqlCommand cmd_CAR = new SqlCommand("SELECT * FROM admCaracteristicasValores WHERE CIDPADRECARACTERISTICA=@valor", SQLConexion);
                    cmd_CAR.Parameters.AddWithValue("@valor", id_valor.ToString());
                    SqlDataReader cmd_CARreader = cmd_CAR.ExecuteReader();
                    while (cmd_CARreader.Read())
                    {
                        string xCaracteristica = String.Format("{0}", cmd_CARreader["CVALORCARACTERISTICA"]);
                        string nCaracteristica = String.Format("{0}", cmd_CARreader["CNEMOCARACTERISTICA"]);
                        listaCaracteristica1.Add(xCaracteristica, nCaracteristica);
                    }
                }
                else
                {
                    nameCaracteristica1 = "";
                }
                SQLConexion.Close();
            }
            else
            {
                this.nameCaracteristica1 = "";
            }

            //Analizando las caracteristicas 2
            if (double.Parse(Car2Bd.ToString()) > 0) //Tiene caracteristica 2
            {
                existCaracteristica2 = true;
                SQLConexion.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM admCaracteristicas WHERE CIDPADRECARACTERISTICA=@padre", SQLConexion);
                command.Parameters.AddWithValue("@padre", double.Parse(Car2Bd.ToString()));
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    nameCaracteristica2 = String.Format("{0}", reader["CNOMBRECARACTERISTICA"]);
                    var id_valor = reader["CIDPADRECARACTERISTICA"];
                    reader.Close();
                    SqlCommand cmd_CAR = new SqlCommand("SELECT * FROM admCaracteristicasValores WHERE CIDPADRECARACTERISTICA=@valor", SQLConexion);
                    cmd_CAR.Parameters.AddWithValue("@valor", id_valor.ToString());
                    SqlDataReader cmd_CARreader = cmd_CAR.ExecuteReader();
                    while (cmd_CARreader.Read())
                    {
                        string xCaracteristica = String.Format("{0}", cmd_CARreader["CVALORCARACTERISTICA"]);
                        string nCaracteristica = String.Format("{0}", cmd_CARreader["CNEMOCARACTERISTICA"]);
                        listaCaracteristica2.Add(xCaracteristica, nCaracteristica);
                    }

                }
                else
                {
                    nameCaracteristica2 = "";
                }
                SQLConexion.Close();
            }
            else
            {
                this.nameCaracteristica2 = "";
            }


            //Analizando las caracteristicas 3
            if (double.Parse(Car3Bd.ToString()) > 0) //Tiene caracteristica 3
            {
                existCaracteristica3 = true;
                SQLConexion.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM admCaracteristicas WHERE CIDPADRECARACTERISTICA=@padre", SQLConexion);
                command.Parameters.AddWithValue("@padre", double.Parse(Car3Bd.ToString()));
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    nameCaracteristica3 = String.Format("{0}", reader["CNOMBRECARACTERISTICA"]);
                    var id_valor = reader["CIDPADRECARACTERISTICA"];
                    reader.Close();
                    SqlCommand cmd_CAR = new SqlCommand("SELECT * FROM admCaracteristicasValores WHERE CIDPADRECARACTERISTICA=@valor", SQLConexion);
                    cmd_CAR.Parameters.AddWithValue("@valor", id_valor.ToString());
                    SqlDataReader cmd_CARreader = cmd_CAR.ExecuteReader();
                    while (cmd_CARreader.Read())
                    {
                        string xCaracteristica = String.Format("{0}", cmd_CARreader["CVALORCARACTERISTICA"]);
                        string nCaracteristica = String.Format("{0}", cmd_CARreader["CNEMOCARACTERISTICA"]);
                        listaCaracteristica3.Add(xCaracteristica, nCaracteristica);
                    }

                }
                else
                {
                    nameCaracteristica3 = "";
                }
                SQLConexion.Close();
            }
            else
            {
                this.nameCaracteristica3 = "";
            }

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
