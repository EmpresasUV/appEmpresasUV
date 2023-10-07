using ARSoftware.Contpaqi.Comercial.Sdk;
using ARSoftware.Contpaqi.Comercial.Sdk.Constantes;
using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;
using ARSoftware.Contpaqi.Comercial.Sdk.Extensiones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace exceSdk
{
    public class ProductoSdk
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public string Estado { get; set; }
        public int Tipo { get; set; }
        public bool TieneCaracteristicas { get; set; }
        public CaracteristicasSdk Caracteristicas { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Codigo} - {Nombre} - {Precio} - {Estado} - {Tipo}";
        }

        private static ProductoSdk LeerDatosProducto()
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
            
            // Asignar el precio con impuesto
            var Precio_IVA = double.Parse(precioBd.ToString()) * 1.16;

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
        public static ProductoSdk BuscarProductoPorId(int productoId)
        {
            ComercialSdk.fBuscaIdProducto(productoId);
            return LeerDatosProducto();
        }
        public static ProductoSdk BuscarProductoPorCodigo(string productoCodigo)
        {
            ComercialSdk.fBuscaProducto(productoCodigo);
            return LeerDatosProducto();
        }
        public static List<ProductoSdk> BuscarProductos()
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
        public static int CrearProducto(ProductoSdk producto)
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
        public static void ActualizarProducto(ProductoSdk producto)
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
        public static void EliminarProducto(ProductoSdk producto)
        {
            // Buscar el producto por codigo
            // Si el producto existe el SDK se posiciona en el registro
            ComercialSdk.fBuscaProducto(producto.Codigo).TirarSiEsError();

            // Borrar el registro de la base de datos 
            ComercialSdk.fBorraProducto().TirarSiEsError();
        }
        public static string[] ExistenciaAlmacen(string productoCodigo, ref List<string> ListaAlmacenes)
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
                /*
                using (var frm_Pago = new frmBoletos())
                {
                    var result = frm_Pago.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        aExiALM[2] = frm_Pago.mPago;
                    }
                    else
                    {
                        aExiALM[2] = "0";
                    }
                }
                */
            }
            return aExiALM;
        }
        public static string[] ExistenciaAlmacenCaracteristicas(string productoCodigo, string C_1, string C_2, string C_3, ref List<string> ListaAlmacenes)
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

    }
}
