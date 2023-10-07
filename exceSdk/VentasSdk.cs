using ARSoftware.Contpaqi.Comercial.Sdk;
using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exceSdk
{
    public class VentasSdk
    {
        public EventLog eventLog = new EventLog();
        public const string NombreLlaveRegistroComercial = @"SOFTWARE\\WOW6432Node\\Computación en Acción, SA CV\\CONTPAQ I COMERCIAL";
        public const string NombreLlaveRegistroContable = @"SOFTWARE\\Wow6432Node\\Computación en Acción, SA CV\\CONTPAQ I";
        public const string NombrePaqComercial = "CONTPAQ I COMERCIAL";
        public const string NombrePaqContable = "CONTPAQ I CONTABILIDAD";
        public List<string> lista_Almacenes = new List<string>();
        public SqlConnection SQLConexion = new SqlConnection(@"Data Source = DC-CONTABLE\COMPAC; Initial Catalog = cmPuntoVentas; User ID = sa; Password = Promo1002##");

        public void w_log(string strMensaje, EventLogEntryType tipo, int numero)
        {
            try
            {
                if (!EventLog.SourceExists("Punto de ventas web"))
                {
                    EventLog.CreateEventSource("Punto de ventas web", "Punto de ventas web");
                    eventLog.Source = "Punto de ventas web";
                }
                else
                {
                    eventLog.Source = "Punto de ventas web";
                }
            }
            catch
            {
                eventLog.Source = "Application";
            }

            eventLog.WriteEntry("Error detectado: " + strMensaje, tipo, numero, 1);
        }
        public void AbrirEmpresa(string dbEmpresa)
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
                    Console.WriteLine("Error detectado: " + strMensaje);
                }
                else
                {
                    string rutaEMPRESA_COM = "C:\\Compac\\Empresas\\" + dbEmpresa;
                    Console.WriteLine();
                    w_log("Abriendo la empresa: " + dbEmpresa + " en la ruta: " + rutaEMPRESA_COM, EventLogEntryType.Information, 143);
                    nStart = ComercialSdk.fAbreEmpresa(rutaEMPRESA_COM);
                    if (nStart != 0)
                    {
                        StringBuilder strMensaje = new StringBuilder(512);
                        ComercialSdk.fError(nStart, strMensaje, 512);
                        w_log("Error detectado: " + strMensaje, EventLogEntryType.Error, 143);
                    }
                }

            }
            catch (Exception e)
            {
                w_log(e.Message, EventLogEntryType.Error, 143);
            }
            finally
            {
                ComercialSdk.fCierraEmpresa();
                ComercialSdk.fTerminaSDK();
            }
        }
        public void CerrarEmpresa()
        {
            ComercialSdk.fCierraEmpresa();
        }
        public void init_tpv(string dbEmpresa, int caja)
        {
            AbrirEmpresa(dbEmpresa);
            var jsonRESP = new Dictionary<string, string>();
            MySQL dbMySQL = new MySQL();
            MySqlDataReader MyCaja = dbMySQL.execSQL("SELECT * FROM tpv_cajas WHERE id = " + caja + ";");
            var dataCaja = new Dictionary<string, string>();
            while (MyCaja.Read())//Buscamos si la caja tiene almacenes
            {
                dataCaja.Add("id", MyCaja["id"].ToString());
                dataCaja.Add("id_usuario", MyCaja["id_usuario"].ToString());
                dataCaja.Add("nombre", MyCaja["nombre"].ToString());
                dataCaja.Add("empresa_SDK", MyCaja["empresa_SDK"].ToString());
                dataCaja.Add("concepto_SDK", MyCaja["concepto_SDK"].ToString());
            }
            dbMySQL.Desconectar();
            ClienteSdk cPublicoGeneral = ClienteSdk.BuscarClientePorCodigo("XAXX010101000");
            if (cPublicoGeneral != null) //El cliente publico general existe
            {
                tLlaveDoc MySerieFolio = DocumentoSdk.BuscarSiguienteSerieYFolio(dataCaja["concepto_SDK"].Trim());
                jsonRESP.Add("remision_SERIE", MySerieFolio.aSerie.ToString());
                jsonRESP.Add("remision_FOLIO", MySerieFolio.aFolio.ToString());
                jsonRESP.Add("cliente_RFC", cPublicoGeneral.Codigo.ToString());
                jsonRESP.Add("cliente_RAZON", cPublicoGeneral.RazonSocial.ToString());

                Console.WriteLine(JsonConvert.SerializeObject(jsonRESP));
            }
            else //No existe el cliente [Público en General]
            {

                Console.WriteLine(JsonConvert.SerializeObject(dataCaja));
            }
        }



    }
}
