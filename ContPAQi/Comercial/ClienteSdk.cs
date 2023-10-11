using ARSoftware.Contpaqi.Comercial.Sdk;
using ARSoftware.Contpaqi.Comercial.Sdk.Constantes;
using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;
using ARSoftware.Contpaqi.Comercial.Sdk.Extensiones;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ContPAQi
{
    /** ********************************************************************* **/
    /** INTERFACE PRINCIPAL DE LA CLASE **/
    /** ********************************************************************* **/
    [Guid("493365e4-1c65-4377-90f4-7e02e3141309")]
    public interface IClienteSdk
    {
        void setNoCaja(string xCaja);
        string http_BuscarPorId(int clienteId);
        string http_BuscarPorCodigo(string clienteCodigo);
        string http_BuscarTodos(int soloActivos = 1);


    }
    /** ********************************************************************* **/
    /** INTERFACE PARA EVENTOS DE LA CLASE **/
    /** ********************************************************************* **/
    [Guid("42a00a29-45ce-4855-b222-d7438ca080fd"),
     InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IClienteSdkEvents
    {
        void setNoCaja(string xCaja);
        string http_BuscarPorId(int clienteId);
        string http_BuscarPorCodigo(string clienteCodigo);
        string http_BuscarTodos(int soloActivos = 1);
    }
    /** ********************************************************************* **/
    /** CLASE PRINCIPAL DE IMPLEMENTACIÓN  **/
    /** ********************************************************************* **/
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [ComSourceInterfaces(typeof(IClienteSdkEvents))]
    [ProgId("ContPAQi.ClienteSdk")]
    public class ClienteSdk : IClienteSdk
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
        public string Codigo { get; set; }
        public string RazonSocial { get; set; }
        public string Rfc { get; set; }
        public int Tipo { get; set; }
        public int Status { get; set; }
        public string NoCaja { get; set; }
        public void setNoCaja(string xCaja)
        {
            this.NoCaja = xCaja;
        }

        /** ********************************************************************* **/
        /** FUNCIONES DE APLICACION WEB **/
        /** ********************************************************************* **/
        public string http_BuscarPorCodigo(string clienteCodigo)
        {
            try
            {
                Dictionary<string, string> ObjRESP = new Dictionary<string, string>();
                open_SDK();
                ComercialSdk.fBuscaCteProv(clienteCodigo).TirarSiEsError();
                ClienteSdk MyCliente = LeerDatosCliente();
                close_SDK();
                ObjRESP.Add("Id", MyCliente.Id.ToString());
                ObjRESP.Add("Codigo", MyCliente.Codigo.ToString());
                ObjRESP.Add("Rfc", MyCliente.Rfc.ToString());
                ObjRESP.Add("RazonSocial", MyCliente.RazonSocial.ToString());
                ObjRESP.Add("Tipo", MyCliente.Tipo.ToString());
                ObjRESP.Add("Estado", MyCliente.Status.ToString());

                return JsonConvert.SerializeObject(ObjRESP, JsonSettingsHTML);
            }
            catch (ARSoftware.Contpaqi.Comercial.Sdk.Excepciones.ContpaqiSdkException e)
            {
                Console_log("Error SDK: " + e.CodigoErrorSdk.ToString() + " -> " + e.MensajeErrorSdk.ToString(), EventLogEntryType.Error, 3000);
                return null;
            }
            catch (Exception ex)
            {
                Console_log(ex.Message + "\n\nTrace:\n" + ex.StackTrace.ToString(), EventLogEntryType.Error, 3000);
                return null;
            }
        }
        public string http_BuscarPorId(int clienteId)
        {
            try
            {
                Dictionary<string, string> ObjRESP = new Dictionary<string, string>();
                open_SDK();
                ComercialSdk.fBuscaIdCteProv(clienteId).TirarSiEsError();
                ClienteSdk MyCliente = LeerDatosCliente();
                close_SDK();
                ObjRESP.Add("Id", MyCliente.Id.ToString());
                ObjRESP.Add("Codigo", MyCliente.Codigo.ToString());
                ObjRESP.Add("Rfc", MyCliente.Rfc.ToString());
                ObjRESP.Add("RazonSocial", MyCliente.RazonSocial.ToString());
                ObjRESP.Add("Tipo", MyCliente.Tipo.ToString());
                ObjRESP.Add("Estado", MyCliente.Status.ToString());

                return JsonConvert.SerializeObject(ObjRESP, JsonSettingsHTML);
            }
            catch (ARSoftware.Contpaqi.Comercial.Sdk.Excepciones.ContpaqiSdkException e)
            {
                Console_log("Error SDK: " + e.CodigoErrorSdk.ToString() + " -> " + e.MensajeErrorSdk.ToString(), EventLogEntryType.Error, 3000);
                return null;
            }
            catch (Exception ex)
            {
                Console_log(ex.Message + "\n\nTrace:\n" + ex.StackTrace.ToString(), EventLogEntryType.Error, 3000);
                return null;
            }
        }
        public string http_BuscarTodos(int soloActivos = 1)
        {
            try
            {
                ClienteSdk MyCliente = new ClienteSdk();
                List<string> ListaClientes = new List<string>();
                Dictionary<string, string> objCLIENTE = new Dictionary<string, string>();
                open_SDK();
                if (soloActivos == 1) //Solo buscamos los clientes activos
                {
                    //Posicionamos en el primer almacen y leemos sus datos
                    ComercialSdk.fPosPrimerCteProv();
                    MyCliente = LeerDatosCliente();
                    if (MyCliente.Status == 1)
                    {
                        objCLIENTE.Add("Id", MyCliente.Id.ToString());
                        objCLIENTE.Add("Codigo", MyCliente.Codigo.ToString());
                        objCLIENTE.Add("Rfc", MyCliente.Rfc.ToString());
                        objCLIENTE.Add("RazonSocial", MyCliente.RazonSocial.ToString());
                        objCLIENTE.Add("Tipo", MyCliente.Tipo.ToString());
                        objCLIENTE.Add("Estado", MyCliente.Status.ToString());
                        ListaClientes.Add(JsonConvert.SerializeObject(objCLIENTE, JsonSettingsHTML));
                        objCLIENTE.Clear();
                    }
                    // Crear un loop y posicionar el SDK en el siguiente registro
                    while (ComercialSdk.fPosSiguienteCteProv() == SdkConstantes.CodigoExito)
                    {
                        MyCliente = LeerDatosCliente();
                        if (MyCliente.Status == 1)
                        {
                            objCLIENTE.Add("Id", MyCliente.Id.ToString());
                            objCLIENTE.Add("Codigo", MyCliente.Codigo.ToString());
                            objCLIENTE.Add("Rfc", MyCliente.Rfc.ToString());
                            objCLIENTE.Add("RazonSocial", MyCliente.RazonSocial.ToString());
                            objCLIENTE.Add("Tipo", MyCliente.Tipo.ToString());
                            objCLIENTE.Add("Estado", MyCliente.Status.ToString());
                            ListaClientes.Add(JsonConvert.SerializeObject(objCLIENTE, JsonSettingsHTML));
                            objCLIENTE.Clear();
                        }
                        if (ComercialSdk.fPosEOFCteProv() == 1)
                            break;
                    }

                }
                else //Buscamos todos los prosuctos (Activos e Inactivos)
                {
                    //Posicionamos en el primer almacen y leemos sus datos
                    ComercialSdk.fPosPrimerCteProv();
                    MyCliente = LeerDatosCliente();
                    objCLIENTE.Add("Id", MyCliente.Id.ToString());
                    objCLIENTE.Add("Codigo", MyCliente.Codigo.ToString());
                    objCLIENTE.Add("Rfc", MyCliente.Rfc.ToString());
                    objCLIENTE.Add("RazonSocial", MyCliente.RazonSocial.ToString());
                    objCLIENTE.Add("Tipo", MyCliente.Tipo.ToString());
                    objCLIENTE.Add("Estado", MyCliente.Status.ToString());
                    ListaClientes.Add(JsonConvert.SerializeObject(objCLIENTE, JsonSettingsHTML));
                    objCLIENTE.Clear();
                    // Crear un loop y posicionar el SDK en el siguiente registro
                    while (ComercialSdk.fPosSiguienteCteProv() == SdkConstantes.CodigoExito)
                    {
                        MyCliente = LeerDatosCliente();
                        objCLIENTE.Add("Id", MyCliente.Id.ToString());
                        objCLIENTE.Add("Codigo", MyCliente.Codigo.ToString());
                        objCLIENTE.Add("Rfc", MyCliente.Rfc.ToString());
                        objCLIENTE.Add("RazonSocial", MyCliente.RazonSocial.ToString());
                        objCLIENTE.Add("Tipo", MyCliente.Tipo.ToString());
                        objCLIENTE.Add("Estado", MyCliente.Status.ToString());
                        ListaClientes.Add(JsonConvert.SerializeObject(objCLIENTE, JsonSettingsHTML));
                        objCLIENTE.Clear();
                        if (ComercialSdk.fPosEOFCteProv() == 1)
                            break;
                    }

                }
                close_SDK();
                return JsonConvert.SerializeObject(ListaClientes, JsonSettingsHTML);
            }
            catch (Exception ex)
            {
                Console_log(ex.Message + "\n\nTrace:\n" + ex.StackTrace.ToString(), EventLogEntryType.Error, 8000);
                return null;
            }
            finally { ComercialSdk.fCierraEmpresa(); ComercialSdk.fTerminaSDK(); }

        }
        /** ********************************************************************* **/
        /** FUNCIONES DEL CICLO INTERNO **/
        /** ********************************************************************* **/
        public ClienteSdk LeerDatosCliente()
        {
            try
            {
                // Declarar variables a leer de la base de datos
                var idBd = new StringBuilder(3000);
                var codigoBd = new StringBuilder(3000);
                var razonSocialBd = new StringBuilder(3000);
                var rfcBd = new StringBuilder(3000);
                var tipoBd = new StringBuilder(3000);
                var statusBd = new StringBuilder(3000);

                // Leer los datos del registro donde el SDK esta posicionado
                ComercialSdk.fLeeDatoCteProv("CIDCLIENTEPROVEEDOR", idBd, 3000).TirarSiEsError();
                ComercialSdk.fLeeDatoCteProv("CCODIGOCLIENTE", codigoBd, 3000).TirarSiEsError();
                ComercialSdk.fLeeDatoCteProv("CRAZONSOCIAL", razonSocialBd, 3000).TirarSiEsError();
                ComercialSdk.fLeeDatoCteProv("CRFC", rfcBd, 3000).TirarSiEsError();
                ComercialSdk.fLeeDatoCteProv("CTIPOCLIENTE", tipoBd, 3000).TirarSiEsError();
                ComercialSdk.fLeeDatoCteProv("CESTATUS", statusBd, 3000).TirarSiEsError();
            
                // Instanciar un cliente y asignar los datos de la base de datos
                return new ClienteSdk
                {
                    Id = int.Parse(idBd.ToString()),
                    Codigo = codigoBd.ToString(),
                    RazonSocial = razonSocialBd.ToString(),
                    Rfc = rfcBd.ToString(),
                    Tipo = int.Parse(tipoBd.ToString()),
                    Status = int.Parse(statusBd.ToString())
                };
            }
            catch (ARSoftware.Contpaqi.Comercial.Sdk.Excepciones.ContpaqiSdkException e)
            {
                Console_log("Error SDK: " + e.CodigoErrorSdk.ToString() + " -> " + e.MensajeErrorSdk.ToString(), EventLogEntryType.Error, 3000);
                return null;
            }
            catch (Exception ex)
            {
                Console_log(ex.Message + "\n\nTrace:\n" + ex.StackTrace.ToString(), EventLogEntryType.Error, 3000);
                return null;
            }

        }
        public ClienteSdk BuscarClientePorId(int clienteId)
        {
            try
            {
                ComercialSdk.fBuscaIdCteProv(clienteId).TirarSiEsError();
                return LeerDatosCliente();
            }
            catch (ARSoftware.Contpaqi.Comercial.Sdk.Excepciones.ContpaqiSdkException)
            {
                return null;
            }
        }
        public ClienteSdk BuscarClientePorCodigo(string clienteCodigo)
        {
            try
            {

                ComercialSdk.fBuscaCteProv(clienteCodigo).TirarSiEsError();
                return LeerDatosCliente();

            }
            catch (AccessViolationException)
            {
                return null;
            }
            catch (ARSoftware.Contpaqi.Comercial.Sdk.Excepciones.ContpaqiSdkException)
            {
                return null;
            }
        }
        public List<ClienteSdk> BuscarClientes()
        {
            var clientesList = new List<ClienteSdk>();

            // Posicionar el SDK en el primer registro
            int resultado = ComercialSdk.fPosPrimerCteProv();
            if (resultado == SdkConstantes.CodigoExito)
                // Leer los datos del registro donde el SDK esta posicionado
                clientesList.Add(LeerDatosCliente());
            else
                return clientesList;

            // Crear un loop y posicionar el SDK en el siguiente registro
            while (ComercialSdk.fPosSiguienteCteProv() == SdkConstantes.CodigoExito)
            {
                // Leer los datos del registro donde el SDK esta posicionado
                clientesList.Add(LeerDatosCliente());

                // Checar si el SDK esta posicionado en el ultimo registro
                // Si el SDK esta posicionado en el ultimo registro salir del loop
                if (ComercialSdk.fPosEOFCteProv() == 1)
                    break;
            }

            return clientesList;
        }
        public int CrearCliente(ClienteSdk cliente)
        {
            // Instanciar un cliente con la estructura tCteProv del SDK
            var clienteNuevo = new tCteProv
            {
                cCodigoCliente = cliente.Codigo,
                cRazonSocial = cliente.RazonSocial,
                cRFC = cliente.Rfc,
                cTipoCliente = cliente.Tipo,
                cFechaAlta = DateTime.Today.ToString(FormatosFechaSdk.Fecha),
                cEstatus = 1
            };

            // Declarar una variable donde se asignara el id del cliente nuevo
            var clienteNuevoId = 0;

            // Crear cliente nuevo
            ComercialSdk.fAltaCteProv(ref clienteNuevoId, ref clienteNuevo).TirarSiEsError();

            return clienteNuevoId;
        }
        public void ActualizarCliente(ClienteSdk cliente)
        {
            // Buscar el cliente por código
            // Si el cliente existe el SDK se posiciona en el registro
            ComercialSdk.fBuscaCteProv(cliente.Codigo).TirarSiEsError();

            // Activar el modo de edición
            ComercialSdk.fEditaCteProv().TirarSiEsError();

            // Actualizar los campos del registro donde el SDK esta posicionado
            ComercialSdk.fSetDatoCteProv("CRAZONSOCIAL", cliente.RazonSocial).TirarSiEsError();
            ComercialSdk.fSetDatoCteProv("CRFC", cliente.Rfc).TirarSiEsError();

            // Guardar los cambios realizados al registro
            ComercialSdk.fGuardaCteProv().TirarSiEsError();
        }
        public void EliminarCliente(ClienteSdk cliente)
        {
            // Buscar el cliente por código
            // Si el cliente existe el SDK se posiciona en el registro
            ComercialSdk.fBuscaCteProv(cliente.Codigo).TirarSiEsError();

            // Borrar el registro de la base de datos 
            ComercialSdk.fBorraCteProv().TirarSiEsError();
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

