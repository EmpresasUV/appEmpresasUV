﻿using ARSoftware.Contpaqi.Comercial.Sdk;
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
    /// <summary>
    ///     Tabla admDocumentos – Tabla de Documentos
    /// </summary>
    public sealed class DocumentoSdk
    {
        /// <summary>
        ///     Campo CIDDOCUMENTO - Identificador del documento.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Campo CIDCONCEPTODOCUMENTO - Identificador del concepto del documento.
        /// </summary>
        public int ConceptoId { get; set; }

        /// <summary>
        ///     Campo CSERIEDOCUMENTO - Serie del documento.
        /// </summary>
        public string Serie { get; set; }

        /// <summary>
        ///     Campo CFOLIO - Folio del documento.
        /// </summary>
        public double Folio { get; set; }

        /// <summary>
        ///     Campo CFECHA - Fecha del documento.
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        ///     Campo CIDCLIENTEPROVEEDOR - Identificador del cliente o proveedor del documento.
        /// </summary>
        public int ClienteId { get; set; }

        /// <summary>
        ///     Campo CREFERENCIA - Referencia del documento.
        /// </summary>
        public string Referencia { get; set; }

        /// <summary>
        ///     Campo COBSERVACIONES - Observaciones del documento.
        /// </summary>
        public string Observaciones { get; set; }

        /// <summary>
        ///     Campo CTOTAL - Importe del total de los totales de los movimientos para el documento.
        /// </summary>
        public double Total { get; set; }

        public override string ToString()
        {
            return
                $"{Id} - {Fecha:MM/dd/yyyy} - {ConceptoSdk.BuscarConceptoPorId(ConceptoId).Nombre} - {Serie} - {Folio} - {ClienteSdk.BuscarClientePorId(ClienteId).RazonSocial} - {Total:C}";
        }

        /// <summary>
        ///     Lee los datos del documento donde el SDK esta posicionado.
        /// </summary>
        /// <returns>Un documento con los sus datos asignados.</returns>
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

        /// <summary>
        ///     Busca un documento por id.
        /// </summary>
        /// <param name="documentoId">El id del documento a buscar.</param>
        /// <returns>El documento a buscar.</returns>
        public static DocumentoSdk BuscarDocumentoPorId(int documentoId)
        {
            // Buscar el documento por id
            // Si el documento existe el SDK se posiciona en el registro
            ComercialSdk.fBuscarIdDocumento(documentoId).TirarSiEsError();

            // Leer los datos del registro donde el SDK esta posicionado
            return LeerDatosDocumento();
        }

        /// <summary>
        ///     Busca un documento por llave.
        /// </summary>
        /// <param name="codigoConcepto">El código de concepto del documento a buscar.</param>
        /// <param name="serie">La serie del documento a buscar.</param>
        /// <param name="folio">El folio del documento a buscar.</param>
        /// <returns>El documento a buscar.</returns>
        public static DocumentoSdk BuscarDocumentoPorLlave(string codigoConcepto, string serie, string folio)
        {
            // Buscar el documento por llave
            // Si el documento existe el SDK se posiciona en el registro
            ComercialSdk.fBuscarDocumento(codigoConcepto, serie, folio).TirarSiEsError();

            // Leer los datos del registro donde el SDK esta posicionado
            return LeerDatosDocumento();
        }

        /// <summary>
        ///     Filtra y busca los documentos de un cliente.
        /// </summary>
        /// <param name="fechaInicio">Fecha de inicio.</param>
        /// <param name="fechaFin">Fecha fin.</param>
        /// <param name="codigoConcepto">Código del concepto de documento.</param>
        /// <param name="codigoClienteProveedor">Código del cliente.</param>
        /// <returns>Lista de documentos con sus datos asignados.</returns>
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

        /// <summary>
        ///     Busca el siguiente serie y folio del concepto.
        /// </summary>
        /// <param name="codigoConcepto">El código del concepto de documento.</param>
        /// <returns>La llave con el siguiente serie y folio.</returns>
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

        /// <summary>
        ///     Crea un documento nuevo.
        /// </summary>
        /// <param name="documento">El documento a crear.</param>
        /// <returns>El id del documento creado.</returns>
        public static int CrearDocumento(DocumentoSdk documento)
        {
            ConceptoSdk concepto = ConceptoSdk.BuscarConceptoPorId(documento.ConceptoId);
            ClienteSdk cliente = ClienteSdk.BuscarClientePorId(documento.ClienteId);

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

        /// <summary>
        ///     Crea un documento de cargo o abono.
        /// </summary>
        /// <param name="documento">El documento a crear.</param>
        /// <returns>El id del documento creado.</returns>
        public static int CrearDocumentoCargoAbono(DocumentoSdk documento)
        {
            ConceptoSdk concepto = ConceptoSdk.BuscarConceptoPorId(documento.ConceptoId);
            ClienteSdk cliente = ClienteSdk.BuscarClientePorId(documento.ClienteId);

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

        /// <summary>
        ///     Actualiza los datos de un documento.
        /// </summary>
        /// <param name="documento">Documento con los datos a actualizar.</param>
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

        /// <summary>
        ///     Elimina un documento.
        /// </summary>
        /// <param name="documento">El documento a eliminar.</param>
        public static void EliminarDocumento(DocumentoSdk documento)
        {
            // Buscar el documento
            // Si el documento existe el SDK se posiciona en el registro
            ComercialSdk.fBuscarIdDocumento(documento.Id).TirarSiEsError();

            // Eliminar el documento
            ComercialSdk.fBorraDocumento().TirarSiEsError();
        }

        /// <summary>
        ///     Salda un documento.
        /// </summary>
        /// <param name="documentoAPagar">Documento al que se le va a aplicar el pago.</param>
        /// <param name="documentoPago">El documento de pago.</param>
        /// <param name="importe">El importe que se va a aplicar.</param>
        /// <param name="fecha">La fecha en que se va a aplicar el pago.</param>
        public static void SaldarDocumento(tLlaveDoc documentoAPagar, tLlaveDoc documentoPago, double importe, DateTime fecha)
        {
            ComercialSdk.fSaldarDocumento(ref documentoAPagar, ref documentoPago, importe, 1, fecha.ToString(FormatosFechaSdk.Fecha)).TirarSiEsError();
        }


    }
}
