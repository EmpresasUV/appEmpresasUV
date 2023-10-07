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
    [Guid("3a69c508-9e0f-4ff9-ae0c-71f78304deb2")]
    public interface IProductoSdk
    {
        string BuscarProductoPorId(int productoId, string xCaja);
        string BuscarProductoPorCodigo(string productoCodigo, string xCaja);
    }
    /** ********************************************************************* **/
    /** INTERFACE PARA EVENTOS DE LA CLASE **/
    /** ********************************************************************* **/
    [Guid("d9bc51db-f8c0-4aef-9062-a6a8e49b0732"),
     InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IProductoSdkEvents
    {
        string BuscarProductoPorId(int productoId, string xCaja);
        ProductoSdk BuscarProductoPorId(int productoId);
        string BuscarProductoPorCodigo(string productoCodigo, string xCaja);
        ProductoSdk BuscarProductoPorCodigo(string productoCodigo);
    }
    /** ********************************************************************* **/
    /** CLASE PRINCIPAL DE IMPLEMENTACIÓN  **/
    /** ********************************************************************* **/
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [ComSourceInterfaces(typeof(IProductoSdkEvents))]
    [ProgId("ContPAQi.ProductoSdk")]
    public class ProductoSdk : IProductoSdk
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
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public string Estado { get; set; }
        public int Tipo { get; set; }
        public bool TieneCaracteristicas { get; set; }
        public CaracteristicasSdk Caracteristicas { get; set; }

        [ComVisible(true)]
        public override string ToString()
        {
            return $"{Id} - {Codigo} - {Nombre} - {Precio} - {Estado} - {Tipo}";
        }

        [ComVisible(true)]
        public static ProductoSdk LeerDatosProducto()
        {
            // Declarar variables a leer de la base de datos
            var idBd = new StringBuilder(3000);
            var codigoBd = new StringBuilder(3000);
            var nombreBd = new StringBuilder(3000);
            var precioBd = new StringBuilder(3000);
            var estadoBd = new StringBuilder(3000);
            var tipoBd = new StringBuilder(3000);
            var Car1Bd = new StringBuilder(3000);
            var Car2Bd = new StringBuilder(3000);
            var Car3Bd = new StringBuilder(3000);
            var ExentoBd = new StringBuilder(3000);

            // Leer los datos del registro donde el SDK esta posicionado
            ComercialSdk.fLeeDatoProducto("CIDPRODUCTO", idBd, 3000);
            ComercialSdk.fLeeDatoProducto("CCODIGOPRODUCTO", codigoBd, 3000);
            ComercialSdk.fLeeDatoProducto("CNOMBREPRODUCTO", nombreBd, 3000);
            ComercialSdk.fLeeDatoProducto("CPRECIO1", precioBd, 3000);
            ComercialSdk.fLeeDatoProducto("CSTATUSPRODUCTO", estadoBd, 3000);
            ComercialSdk.fLeeDatoProducto("CTIPOPRODUCTO", tipoBd, 3000);
            ComercialSdk.fLeeDatoProducto("CIDPADRECARACTERISTICA1", Car1Bd, 3000);
            ComercialSdk.fLeeDatoProducto("CIDPADRECARACTERISTICA2", Car2Bd, 3000);
            ComercialSdk.fLeeDatoProducto("CIDPADRECARACTERISTICA3", Car3Bd, 3000);
            ComercialSdk.fLeeDatoProducto("CESEXENTO", ExentoBd, 3000);

            var Precio_IVA = double.Parse(precioBd.ToString());

            if (int.Parse(ExentoBd.ToString()) == 0)
            {
                // Asignar el precio con impuesto
                Precio_IVA = double.Parse(precioBd.ToString()) * 1.16;
            }
            

            CaracteristicasSdk MyCaracteristicas = new CaracteristicasSdk(int.Parse(idBd.ToString()));

            // Instanciar un producto y asignar los datos de la base de datos
            return new ProductoSdk
            {
                Id = int.Parse(idBd.ToString()),
                Codigo = codigoBd.ToString(),
                Nombre = nombreBd.ToString(),
                Precio = Math.Round(Precio_IVA, 2, MidpointRounding.AwayFromZero),
                Estado = estadoBd.ToString(),
                Tipo = int.Parse(tipoBd.ToString()),
                Caracteristicas = MyCaracteristicas
            };
        }

        [ComVisible(true)]
        public string BuscarProductoPorId(int productoId, string xCaja)
        {
            open_SDK(xCaja);
            ComercialSdk.fBuscaIdProducto(productoId);
            ProductoSdk MyProducto = LeerDatosProducto();
            close_SDK();
            ObjRESP.Add("Id", MyProducto.Id.ToString());
            ObjRESP.Add("Codigo", MyProducto.Codigo.ToString());
            ObjRESP.Add("Nombre", MyProducto.Nombre.ToString());
            ObjRESP.Add("Precio", MyProducto.Precio.ToString());
            ObjRESP.Add("Estado", MyProducto.Estado.ToString());
            ObjRESP.Add("Tipo", MyProducto.Tipo.ToString());

            return JsonConvert.SerializeObject(ObjRESP, JsonSettingsHTML);
        }
        public static ProductoSdk BuscarProductoPorId(int productoId)
        {

            ComercialSdk.fBuscaIdProducto(productoId);
            return LeerDatosProducto();
        }

        [ComVisible(true)]
        public string BuscarProductoPorCodigo(string productoCodigo, string xCaja)
        {
            try
            {
                open_SDK(xCaja);
                ComercialSdk.fBuscaProducto(productoCodigo).TirarSiEsError();
                ProductoSdk MyProducto = LeerDatosProducto();
                close_SDK();
                ObjRESP.Add("Id", MyProducto.Id.ToString());
                ObjRESP.Add("Codigo", MyProducto.Codigo.ToString());
                ObjRESP.Add("Nombre", MyProducto.Nombre.ToString());
                ObjRESP.Add("Precio", MyProducto.Precio.ToString());
                ObjRESP.Add("Estado", MyProducto.Estado.ToString());
                ObjRESP.Add("Tipo", MyProducto.Tipo.ToString());
                //ObjRESP.Add("Caracteristicas", MyProducto.ToString());

                return JsonConvert.SerializeObject(ObjRESP, JsonSettingsHTML);
            }
            catch (Exception ex)
            {
                Console_log(ex.Message + ";\nTrace:\n" + ex.StackTrace.ToString(), EventLogEntryType.Error, 1000);
                return null;
            }
            finally { ComercialSdk.fCierraEmpresa(); ComercialSdk.fTerminaSDK(); }

        }
        public static ProductoSdk BuscarProductoPorCodigo(string productoCodigo)
        {
            ComercialSdk.fBuscaProducto(productoCodigo);
            return LeerDatosProducto();
        }

        [ComVisible(true)]
        public List<ProductoSdk> BuscarProductos()
        {
            var productosList = new List<ProductoSdk>();

            // Posicionar el SDK en el primer registro
            int resultado = ComercialSdk.fPosPrimerProducto();
            if (resultado == SdkConstantes.CodigoExito)
                // Leer los datos del registro donde el SDK esta posicionado
                productosList.Add(LeerDatosProducto());
            else
                return productosList;

            // Crear un loop y posicionar el SDK en el siguiente registro
            while (ComercialSdk.fPosSiguienteProducto() == SdkConstantes.CodigoExito)
            {
                // Leer los datos del registro donde el SDK esta posicionado
                productosList.Add(LeerDatosProducto());

                // Checar si el SDK esta posicionado en el ultimo registro
                // Si el SDK esta posicionado en el ultimo registro salir del loop
                if (ComercialSdk.fPosEOFProducto() == 1)
                    break;
            }

            return productosList;
        }

        [ComVisible(true)]
        public int CrearProducto(ProductoSdk producto)
        {
            // Instanciar un producto con la estructura tProducto del SDK
            var productoNuevo = new tProducto
            {
                cCodigoProducto = producto.Codigo,
                cNombreProducto = producto.Nombre,
                cTipoProducto = producto.Tipo,
                cFechaAltaProducto = DateTime.Today.ToString(FormatosFechaSdk.Fecha),
                cStatusProducto = 1
            };

            // Declarar una variable donde se asignara el id del producto nuevo
            var productoNuevoId = 0;

            // Crear producto nuevo
            ComercialSdk.fAltaProducto(ref productoNuevoId, ref productoNuevo).TirarSiEsError();

            return productoNuevoId;
        }

        [ComVisible(true)]
        public void ActualizarProducto(ProductoSdk producto)
        {
            // Buscar el producto por código
            // Si el producto existe el SDK se posiciona en el registro
            ComercialSdk.fBuscaProducto(producto.Codigo).TirarSiEsError();

            // Activar el modo de edición
            ComercialSdk.fEditaProducto().TirarSiEsError();

            // Actualizar los campos del registro donde el SDK esta posicionado
            ComercialSdk.fSetDatoProducto("CNOMBREPRODUCTO", producto.Nombre).TirarSiEsError();

            // Guardar los cambios realizados al registro
            ComercialSdk.fGuardaProducto().TirarSiEsError();
        }

        [ComVisible(true)]
        public void EliminarProducto(ProductoSdk producto)
        {
            // Buscar el producto por codigo
            // Si el producto existe el SDK se posiciona en el registro
            ComercialSdk.fBuscaProducto(producto.Codigo).TirarSiEsError();

            // Borrar el registro de la base de datos 
            ComercialSdk.fBorraProducto().TirarSiEsError();
        }

        [ComVisible(true)]
        public string[] ExistenciaAlmacen(string productoCodigo, ref List<string> ListaAlmacenes)
        {
            string[] aExiALM = { "", "", "" };
            double cExistencia = 0;
            DateTime now = DateTime.Now;
            if(productoCodigo.ToUpper() != "BOLETO") //No es un boleto
            {
                foreach (var _almacen in ListaAlmacenes)
                {
                    double temp_existencia = 0;
                    ComercialSdk.fRegresaExistencia(productoCodigo, _almacen, now.ToString("yyyy"), now.ToString("MM"), now.ToString("dd"), ref temp_existencia);
                    if (temp_existencia > 0)
                    {
                        //Console.WriteLine("Existencia de: " + temp_existencia + " en el Almacen: " + _almacen);
                        cExistencia += temp_existencia;
                        aExiALM[0] = cExistencia.ToString();
                        aExiALM[1] = _almacen.ToString();
                    }
                }

            }
            else //Es un boleto
            {
                aExiALM[0] = "50";
                aExiALM[1] = "1";
            }
            return aExiALM;
        }

        [ComVisible(true)]
        public string[] ExistenciaAlmacenCaracteristicas(string productoCodigo, string C_1, string C_2, string C_3, ref List<string> ListaAlmacenes)
        {
            string[] aExiALM = { "", "" };
            double cExistencia = 0;
            DateTime now = DateTime.Now;
            foreach (var _almacen in ListaAlmacenes)
            {
                double temp_existencia = 0;

                ComercialSdk.fRegresaExistenciaCaracteristicas(
                    productoCodigo,
                    _almacen,
                    now.ToString("yyyy"),
                    now.ToString("MM"),
                    now.ToString("dd"),
                    C_1,
                    C_2,
                    C_3,
                    ref temp_existencia
                );
                Console.WriteLine("Existencia: "+temp_existencia);
                if (temp_existencia > 0)
                {
                    //Console.WriteLine("Existencia de: " + temp_existencia + " en el Almacen: " + _almacen);
                    cExistencia += temp_existencia;
                    aExiALM[0] = cExistencia.ToString();
                    aExiALM[1] = _almacen.ToString();
                }
            }
            return aExiALM;
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
