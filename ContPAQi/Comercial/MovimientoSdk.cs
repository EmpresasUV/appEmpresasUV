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
    [Guid("390564b8-eb55-4936-b668-8c2961fdfca7")]
    public interface IMovimientoSdk
    {

    }
    /** ********************************************************************* **/
    /** INTERFACE PARA EVENTOS DE LA CLASE **/
    /** ********************************************************************* **/
    [Guid("74ecf986-31ea-441a-9d2a-df23b862c460"),
     InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IMovimientoSdkEvents
    {

    }
    /** ********************************************************************* **/
    /** CLASE PRINCIPAL DE IMPLEMENTACIÓN  **/
    /** ********************************************************************* **/
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [ComSourceInterfaces(typeof(IMovimientoSdkEvents))]
    [ProgId("ContPAQi.MovimientoSdk")]
    public class MovimientoSdk : IMovimientoSdk
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
        public int DocumentoId { get; set; }
        public int ProductoId { get; set; }
        public string AlmacenId { get; set; }
        public double Unidades { get; set; }
        public double Precio { get; set; }
        public string Referencia { get; set; }
        public string Observaciones { get; set; }
        public double Total { get; set; }
        public override string ToString()
        {
            return $"{Id} - {ProductoSdk.BuscarProductoPorId(ProductoId).Nombre} - {Unidades} - {Precio} - {Total:C}";
        }
        private static MovimientoSdk LeerDatosMovimiento()
        {
            // Declarar variables a leer de la base de datos
            var idBd = new StringBuilder(3000);
            var documentoIdBd = new StringBuilder(3000);
            var productoIdBd = new StringBuilder(3000);
            var unidadesBd = new StringBuilder(3000);
            var precioBd = new StringBuilder(3000);
            var referenciaBd = new StringBuilder(3000);
            var observacionesBd = new StringBuilder(3000);
            var totalBd = new StringBuilder(3000);

            // Leer los datos del registro donde el SDK esta posicionado
            ComercialSdk.fLeeDatoMovimiento("CIDMOVIMIENTO", idBd, 3000).TirarSiEsError();
            ComercialSdk.fLeeDatoMovimiento("CIDDOCUMENTO", documentoIdBd, 3000).TirarSiEsError();
            ComercialSdk.fLeeDatoMovimiento("CIDPRODUCTO", productoIdBd, 3000).TirarSiEsError();
            ComercialSdk.fLeeDatoMovimiento("CUNIDADES", unidadesBd, 3000).TirarSiEsError();
            ComercialSdk.fLeeDatoMovimiento("CPRECIO", precioBd, 3000).TirarSiEsError();
            ComercialSdk.fLeeDatoMovimiento("CREFERENCIA", referenciaBd, 3000).TirarSiEsError();
            ComercialSdk.fLeeDatoMovimiento("COBSERVAMOV", observacionesBd, 3000).TirarSiEsError();
            ComercialSdk.fLeeDatoMovimiento("CTOTAL", totalBd, 3000).TirarSiEsError();

            // Instanciar un movimiento y asignar los datos de la base de datos
            return new MovimientoSdk
            {
                Id = int.Parse(idBd.ToString()),
                DocumentoId = int.Parse(documentoIdBd.ToString()),
                ProductoId = int.Parse(productoIdBd.ToString()),
                Unidades = double.Parse(unidadesBd.ToString()),
                Precio = double.Parse(precioBd.ToString()),
                Referencia = referenciaBd.ToString(),
                Observaciones = observacionesBd.ToString(),
                Total = double.Parse(totalBd.ToString())
            };
        }
        public static MovimientoSdk BuscarMovimientoPorId(int movimientoId)
        {
            ComercialSdk.fBuscarIdMovimiento(movimientoId).TirarSiEsError();
            return LeerDatosMovimiento();
        }
        public static List<MovimientoSdk> BuscarMovimientosPorFiltro(int documentoId)
        {
            var movimientosList = new List<MovimientoSdk>();
            ComercialSdk.fCancelaFiltroMovimiento().TirarSiEsError();
            ComercialSdk.fSetFiltroMovimiento(documentoId).TirarSiEsError();
            ComercialSdk.fPosPrimerMovimiento().TirarSiEsError();
            movimientosList.Add(LeerDatosMovimiento());
            while (ComercialSdk.fPosSiguienteMovimiento() == SdkConstantes.CodigoExito)
            {
                movimientosList.Add(LeerDatosMovimiento());
                if (ComercialSdk.fPosMovimientoEOF() == 1)
                    break;
            }
            ComercialSdk.fCancelaFiltroMovimiento().TirarSiEsError();
            return movimientosList;
        }
        public static int CrearMovimiento(MovimientoSdk movimiento)
        {
            ProductoSdk producto = ProductoSdk.BuscarProductoPorId(movimiento.ProductoId);
            var nuevoMovimiento = new tMovimiento
            {
                aCodProdSer = producto.Codigo,
                aUnidades = movimiento.Unidades,
                aPrecio = movimiento.Precio,
                aReferencia = movimiento.Referencia,
                aCodAlmacen = movimiento.AlmacenId
            };
            var nuevoMovimientoId = 0;
            ComercialSdk.fAltaMovimiento(movimiento.DocumentoId, ref nuevoMovimientoId, ref nuevoMovimiento).TirarSiEsError();
            movimiento.Id = nuevoMovimientoId;
            ActualizarMovimiento(movimiento);
            return nuevoMovimientoId;
        }
        public static int CrearMovimiento(MovimientoSdk movimiento, int PorcentajeDescuento)
        {
            ProductoSdk producto = ProductoSdk.BuscarProductoPorId(movimiento.ProductoId);
            var nuevoMovimiento = new tMovimientoDesc
            {
                aCodProdSer = producto.Codigo,
                aUnidades = movimiento.Unidades,
                aPrecio = movimiento.Precio,
                aReferencia = movimiento.Referencia,
                aCodAlmacen = movimiento.AlmacenId,
                aPorcDescto1 = PorcentajeDescuento
            };
            var nuevoMovimientoId = 0;
            ComercialSdk.fAltaMovimientoCDesct(movimiento.DocumentoId, ref nuevoMovimientoId, ref nuevoMovimiento).TirarSiEsError();
            movimiento.Id = nuevoMovimientoId;
            ActualizarMovimiento(movimiento);
            return nuevoMovimientoId;
        }
        public static int CrearMovimientoCaracteristicas(MovimientoSdk movimiento, tCaracteristicas caracteristicas)
        {
            ProductoSdk producto = ProductoSdk.BuscarProductoPorId(movimiento.ProductoId);
            var nuevoMovimiento = new tMovimiento
            {
                aCodProdSer = producto.Codigo,
                aUnidades = movimiento.Unidades,
                aPrecio = movimiento.Precio,
                aReferencia = movimiento.Referencia,
                aCodAlmacen = movimiento.AlmacenId
            };
            var nuevoMovimientoId = 0;
            var nuevaCaracteristicas = 0;
            ComercialSdk.fAltaMovimiento(movimiento.DocumentoId, ref nuevoMovimientoId, ref nuevoMovimiento).TirarSiEsError();
            movimiento.Id = nuevoMovimientoId;
            ComercialSdk.fAltaMovimientoCaracteristicas(movimiento.Id, ref nuevaCaracteristicas, ref caracteristicas).TirarSiEsError();
            ActualizarMovimiento(movimiento);
            return nuevoMovimientoId;
        }
        public static int CrearMovimientoCaracteristicas(MovimientoSdk movimiento, tCaracteristicas caracteristicas, int PorcentajeDescuento)
        {
            ProductoSdk producto = ProductoSdk.BuscarProductoPorId(movimiento.ProductoId);
            var nuevoMovimiento = new tMovimientoDesc
            {
                aCodProdSer = producto.Codigo,
                aUnidades = movimiento.Unidades,
                aPrecio = movimiento.Precio,
                aReferencia = movimiento.Referencia,
                aCodAlmacen = movimiento.AlmacenId,
                aPorcDescto1 = PorcentajeDescuento
            };
            var nuevoMovimientoId = 0;
            var nuevaCaracteristicas = 0;
            ComercialSdk.fAltaMovimientoCDesct(movimiento.DocumentoId, ref nuevoMovimientoId, ref nuevoMovimiento).TirarSiEsError();
            movimiento.Id = nuevoMovimientoId;
            ComercialSdk.fAltaMovimientoCaracteristicas(movimiento.Id, ref nuevaCaracteristicas, ref caracteristicas).TirarSiEsError();
            ActualizarMovimiento(movimiento);
            return nuevoMovimientoId;
        }
        public static void ActualizarMovimiento(MovimientoSdk movimiento)
        {
            ComercialSdk.fBuscarIdMovimiento(movimiento.Id).TirarSiEsError();
            ComercialSdk.fEditarMovimiento().TirarSiEsError(); ;
            ComercialSdk.fSetDatoMovimiento("COBSERVAMOV", movimiento.Observaciones).TirarSiEsError();
            ComercialSdk.fGuardaMovimiento().TirarSiEsError();
        }
        public static void EliminarMovimiento(MovimientoSdk movimiento)
        {
            ComercialSdk.fBuscarIdMovimiento(movimiento.Id).TirarSiEsError();
            ComercialSdk.fBorraMovimiento(movimiento.DocumentoId, movimiento.Id).TirarSiEsError();
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
