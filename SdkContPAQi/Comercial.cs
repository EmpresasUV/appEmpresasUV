using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace SdkContPAQi
{
    /** ********************************************************************* **/
    /** INTERFACE PRINCIPAL DE LA CLASE **/
    /** ********************************************************************* **/
    [ComVisible(true)]
    [Guid("b9390ef0-3af3-4ab4-9212-3cf1218a6402")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IComercial
    {
        string test();
        int open_SDK(string aNombreEmpresa);
        void close_SDK();
        int fAbreEmpresa(string aDirectorioEmpresa);
        int fActivarPrecioCompra(int aActivar);
        int fActualizaClasificacion(int aClasificacionDe, int aNumClasificacion, string aNombreClasificacion);
        int fActualizaCteProv(string aCodigoCteProv, ref tCteProv astCteProv);
        int fActualizaDireccion(ref tDireccion astDireccion);
        int fActualizaProducto(string aCodigoProducto, ref tProducto astProducto);
        int fActualizaUnidad(string aNombreUnidad, ref tUnidad astUnidad);
        int fActualizaValorClasif(string aCodigoValorClasif, ref tValorClasificacion astValorClasif);
        int fAfectaDocto(ref tLlaveDoc aLlaveDocto, bool aAfecta);
        int fAfectaDocto_Param(string aCodConcepto, string aSerie, double aFolio, bool aAfecta);
        int fAfectaSerie(int aIdMovto, string aNumeroSerie);
        int fAgregarRelacionCFDI(string aCodConcepto, string aSerie, string aFolio, string aTipoRelacion, string aConceptoRelacionar, string aSerieRelacionar, string aFolioRelacionar);
        int fAgregarRelacionCFDI2(string aCodConcepto, string aSerie, string aFolio, string aTipoRelacion, string aUUID);
        int fAltaCteProv(ref int aIdCteProv, ref tCteProv astCteProv);
        int fAltaCuentaBancariaCliente(ref int aIdCtaBancaria, string aCuentaBancaria, string aNombreCuenta, string aNombreMoneda, string aClaveBanco, string aClabe, string aRfcBanco, string aNombreBancoExtranjero, string aCodigoCliente);
        int fAltaCuentaBancariaEmpresa(ref int aIdCtaBancaria, string aCuentaBancaria, string aNombreCuenta, string aNombreMoneda, string aClaveBanco, string aClabe, string aRfcBanco, string aNombreBancoExtranjero);
        int fAltaDireccion(ref int aIdDireccion, ref tDireccion astDireccion);
        int fAltaDoctoAjusteIESPSCteProv(string aCodigoClienteProveedor, int aEsCliente, string aFechaDocto, int aIdMoneda, double aTipoCambio, double aImporteIVA, double aTasaIVA, double aImporteIESPS, double aTasaIESPS, int aIdFacturaBase, string aMetodo, string aLugar, ref int aIdDoctoGenerado);
        int fAltaDoctoAjusteIVAClienteProveedor(string aCodigoClienteProveedor, int aEsCliente, int aAbsorberAjusteIVA, string aFechaDocto, int aIdMoneda, double aTipoCambio, double aImporteIVA, double aTasaIVA, int aIdFacturaBase, string aMetodo, string aLugar, ref int aIdDoctoGenerado);
        int fAltaDocumento(ref int aIdDocumento, ref tDocumento aDocumento);
        int fAltaDocumentoCargoAbono(ref tDocumento aDocumento);
        int fAltaDocumentoCargoAbonoExtras(ref tDocumento aDocumento, string aTextoExtra1, string aTextoExtra2, string aTextoExtra3, string aFechaExtra, double aImporteExtra1, double aImporteExtra2, double aImporteExtra3, double aImporteExtra4);
        int fAltaMovimiento(int aIdDocumento, ref int aIdMovimiento, ref tMovimiento astMovimiento);
        int fAltaMovimientoCaracteristicas(int aIdMovimiento, ref int aIdMovtoCaracteristicas, ref tCaracteristicas aCaracteristicas);
        int fAltaMovimientoCaracteristicas_Param(string aIdMovimiento, string aIdMovtoCaracteristicas, string aUnidades, string aValorCaracteristica1, string aValorCaracteristica2, string aValorCaracteristica3);
        int fAltaMovimientoCDesct(int aIdDocumento, ref int aIdMovimiento, ref tMovimientoDesc astMovimiento);
        int fAltaMovimientoEx(ref int aIdMovimiento, ref tTipoProducto aTipoProducto);
        int fAltaMovimientoSeriesCapas(int aIdMovimiento, ref tSeriesCapas aSeriesCapas);
        int fAltaMovimientoSeriesCapas_Param(string aIdMovimiento, string aUnidades, string aTipoCambio, string aSeries, string aPedimento, string aAgencia, string aFechaPedimento, string aNumeroLote, string aFechaFabricacion, string aFechaCaducidad);
        int fAltaMovtoCaracteristicasUnidades(int aIdMovimiento, ref int aIdMovtoCaracteristicas, ref tCaracteristicas aCaracteristicasUnidades);
        int fAltaMovtoCaracteristicasUnidades_Param(string aIdMovimiento, string aIdMovtoCaracteristicas, string aUnidad, string aUnidades, string aUnidadesNC, string aValorCaracteristica1, string aValorCaracteristica2, string aValorCaracteristica3);
        int fAltaProducto(ref int aIdProducto, ref tProducto astProducto);
        int fAltaUnidad(ref int aIdUnidad, ref tUnidad astUnidad);
        int fAltaValorClasif(ref int aIdValorClasif, ref tValorClasificacion astValorClasif);
        int fBorraCteProv();
        int fBorraCuentaBancariaCliente(string aCuentaBancaria, string aCodigoCliente);
        int fBorraCuentaBancariaEmpresa(string aCuentaBancaria);
        int fBorraDocumento();
        int fBorraDocumento_CW();
        int fBorraMovimiento(int aIdDocumento, int aIdMovimiento);
        int fBorraProducto();
        int fBorrarAsociacion(tLlaveDoc aDoctoaPagar, tLlaveDoc aDoctoPago);
        int fBorrarAsociacion_Param(string aCodConcepto_Pagar, string aSerie_Pagar, double aFolio_Pagar, string aCodConcepto_Pago, string aSerie_Pago, double aFolio_Pago);
        int fBorraUnidad();
        int fBorraValorClasif();
        int fBuscaAgente(string aCodigoAgente);
        int fBuscaAlmacen(string aCodigoAlmacen);
        int fBuscaClasificacion(int aClasificacionDe, int aNumClasificacion);
        int fBuscaConceptoDocto(string aCodConcepto);
        int fBuscaCteProv(string aCodCteProv);
        int fBuscaDireccionCteProv(string aCodCteProv, byte aTipoDireccion);
        int fBuscaDireccionDocumento(int aIdDocumento, byte aTipoDireccion);
        int fBuscaDireccionEmpresa();
        int fBuscaDocumento(ref tLlaveDoc aLlaveDocto);
        int fBuscaIdAgente(int aIdAgente);
        int fBuscaIdAlmacen(int aIdAlmacen);
        int fBuscaIdClasificacion(int aIdClasificacion);
        int fBuscaIdConceptoDocto(int aIdConcepto);
        int fBuscaIdCteProv(int aIdCteProv);
        int fBuscaIdProducto(int aIdProducto);
        int fBuscaIdUnidad(int aIdUnidad);
        int fBuscaIdValorClasif(int aIdValorClasif);
        int fBuscaProducto(string aCodProducto);
        int fBuscarDocumento(string aCodConcepto, string aSerie, string aFolio);
        int fBuscarIdDocumento(int aIdDocumento);
        int fBuscarIdMovimiento(int aIdMovimiento);
        int fBuscaUnidad(string aNombreUnidad);
        int fBuscaValorClasif(int aClasificacionDe, int aNumClasificacion, string aCodValorClasif);
        int fCalculaMovtoSerieCapa(int aIdMovimiento);
        int fCancelaCambiosMovimiento();
        int fCancelaComplementoPagoUUID(string aUUID, string aIdDConcepto, string aPass);
        int fCancelaDoctoInfo(string aPass);
        int fCancelaDocumento();
        int fCancelaDocumento_CW();
        int fCancelaDocumentoAdministrativamente();
        int fCancelaDocumentoConMotivo(string aMotivoCancelacion, string aUUIDRemplaza);
        int fCancelaFiltroDocumento();
        int fCancelaFiltroMovimiento();
        int fCancelaNominaUUID(string aUUID, string aIdDConcepto, string aPass);
        int fCancelarModificacionAgente();
        int fCancelarModificacionAlmacen();
        int fCancelarModificacionClasificacion();
        int fCancelarModificacionCteProv();
        int fCancelarModificacionDireccion();
        int fCancelarModificacionDocumento();
        int fCancelarModificacionProducto();
        int fCancelarModificacionUnidad();
        int fCancelarModificacionValorClasif();
        int fCancelaUUID(string aUUID, string aIdDConcepto, string aPass);
        int fCancelaUUID40(string aUUID, string aMotivoCancelacion, string aUUIDReemplaza, string RFCReceptor, double aTotal, string aIdDConcepto, string aPass, ref int aEstatusCancelacion);
        void fCierraEmpresa();
        int fCuentaBancariaEmpresaDoctos(string aCuentaBancaria);
        int fDesbloqueaDocumento();
        int fDocumentoBloqueado(ref int aImpreso);
        int fDocumentoDevuelto(ref int aDevuelto);
        int fDocumentoImpreso(ref bool aImpreso);
        int fDocumentoUUID(StringBuilder aCodConcepto, StringBuilder aSerie, double aFolio, StringBuilder atPtrCFDIUUID);
        int fEditaAgente();
        int fEditaAlmacen();
        int fEditaClasificacion();
        int fEditaConceptoDocto();
        int fEditaCteProv();
        int fEditaDireccion();
        int fEditaMovtoContable();
        int fEditaParametros();
        int fEditaProducto();
        int fEditarDocumento();
        int fEditarDocumentoCheqpaq();
        int fEditarMovimiento();
        int fEditaUnidad();
        int fEditaValorClasif();
        int fEliminarCteProv(string aCodigoCteProv);
        int fEliminarProducto(string aCodigoProducto);
        int fEliminarRelacionesCFDIs(string aCodConcepto, string aSerie, string aFolio);
        int fEliminarUnidad(string aNombreUnidad);
        int fEliminarValorClasif(int aClasificacionDe, int aNumClasificacion, string aCodigoValorClasif);
        int fEmitirDocumento(string aCodConcepto, string aSerie, double aFolio, string aPassword, string aArchivoAdicional);
        int fEntregEnDiscoXML(string aCodConcepto, string aSerie, double aFolio, int aFormato, string aFormatoAmig);
        void fError(int aNumError, StringBuilder aMensaje, int aLen);
        int fGetCantidadParcialidades(StringBuilder atPtrPassword, StringBuilder aCantidadParcialidades);
        int fGetDatosCFDI(StringBuilder aSerieCertEmisor, StringBuilder aFolioFiscalUUID, StringBuilder aSerieCertSAT, StringBuilder aFechaHora, StringBuilder aSelloDigCFDI, StringBuilder aSelloSAAT, StringBuilder aCadOrigComplSAT, StringBuilder aRegimen, StringBuilder aLugarExpedicion, StringBuilder aMoneda, StringBuilder aFolioFiscalOrig, StringBuilder aSerieFolioFiscalOrig, StringBuilder aFechaFolioFiscalOrig, StringBuilder aMontoFolioFiscalOrig);
        int fGetNumParcialidades(StringBuilder atPtrPassword, StringBuilder aNumParcialidades);
        int fGetSelloDigitalYCadena(StringBuilder atPtrPassword, StringBuilder atPtrSelloDigital, StringBuilder atPtrCadenaOriginal);
        int fGetSerieCertificado(StringBuilder atPtrPassword, StringBuilder aSerieCertificado);
        int fGetTamSelloDigitalYCadena(StringBuilder atPtrPassword, ref int aEspSelloDig, ref int aEspCadOrig);
        int fGuardaAgente();
        int fGuardaAlmacen();
        int fGuardaClasificacion();
        int fGuardaConceptoDocto();
        int fGuardaCteProv();
        int fGuardaDireccion();
        int fGuardaDocumento();
        int fGuardaMovimiento();
        int fGuardaMovtoContable();
        int fGuardaParametros();
        int fGuardaProducto();
        int fGuardaUnidad();
        int fGuardaValorClasif();
        int fInformacionCliente(StringBuilder aCodigo, ref int aPermiteCredito, ref double aLimiteCredito, ref int aLimiteDoctosVencidos, ref int aPermiteExcederCredito, StringBuilder aFecha, ref double aSaldo, ref double aSaldoPendiente, ref int aDoctosVencidos);
        int fInicializaLicenseInfo(byte aSistema);
        int fInicializaSDK();
        void fInicioSesionSDK(string aUsuario, string aContrasenia);
        void fInicioSesionSDKCONTPAQi(string aUsuario, string aContrasenia);
        int fInsertaAgente();
        int fInsertaAlmacen();
        int fInsertaCteProv();
        int fInsertaDatoAddendaDocto(int aIdAddenda, int aIdCatalogo, int aNumCampo, string aDato);
        int fInsertaDatoAddendaMovto(int aIdAddenda, int aIdCatalogo, int aNumCampo, string aDato);
        int fInsertaDatoCompEducativo(int aIdServicio, int aNumCampo, string aDato);
        int fInsertaDireccion();
        int fInsertaProducto();
        int fInsertarDocumento();
        int fInsertarMovimiento();
        int fInsertaUnidad();
        int fInsertaValorClasif();
        int fLeeDatoAgente(string aCampo, StringBuilder aValor, int aLen);
        string fLeeDatoAlmacen(string aCampo);
        int fLeeDatoCFDI(StringBuilder aValor, int aDato);
        int fLeeDatoClasificacion(string aCampo, StringBuilder aValor, int aLen);
        int fLeeDatoConceptoDocto(string aCampo, StringBuilder aValor, int aLen);
        int fLeeDatoCteProv(string aCampo, StringBuilder aValor, int aLen);
        int fLeeDatoDireccion(string aCampo, StringBuilder aValor, int aLen);
        int fLeeDatoDocumento(string aCampo, StringBuilder aValor, int aLongitud);
        int fLeeDatoMovimiento(string aCampo, StringBuilder aValor, int aLen);
        int fLeeDatoMovtoContable(string aCampo, StringBuilder aValor, int aLen);
        int fLeeDatoParametros(string aCampo, StringBuilder aValor, int aLen);
        int fLeeDatoProducto(string aCampo, StringBuilder aValor, int aLen);
        int fLeeDatoUnidad(string aCampo, StringBuilder aValor, int aLen);
        int fLeeDatoValorClasif(string aCampo, StringBuilder aValor, int aLen);
        int fLlenaRegistroCteProv(ref tCteProv astCteProv, int aEsAlta);
        int fLlenaRegistroDireccion(ref tDireccion astDireccion, int aEsAlta);
        int fLlenaRegistroProducto(ref tProducto astProducto, int aEsAlta);
        int fLlenaRegistroUnidad(ref tUnidad astUnidad);
        int fLlenaRegistroValorClasif(ref tValorClasificacion astValorClasif);
        int fModificaCostoEntrada(string aIdMovimiento, string aCostoEntrada);
        int fModificaCuentaBancariaCliente(string aCuentaBancaria, string aNombreCuenta, string aNombreMoneda, string aClaveBanco, string aRfcBanco, string aClabe, string aNombreExtranjero, string aCodigoCliente);
        int fModificaCuentaBancariaEmpresa(string aCuentaBancaria, string aNombreCuenta, string aNombreMoneda, string aClaveBanco, string aRfcBanco, string aClabe, string aNombreExtranjero);
        int fObtenCeryKey(int aIdFirmarl, string aRutaKey, string aRutaCer);
        int fObtieneDatosCFDI(string atPtrPassword);
        int fObtieneLicencia(StringBuilder aCodAcvtiva, StringBuilder aCodSitio, StringBuilder aSerie, StringBuilder aTagVersion);
        int fObtienePassProxy(StringBuilder aPassProxy);
        int fObtieneUnidadesPendientes(string aConceptoDocto, string aCodigoProducto, string aCodigoAlmacen, StringBuilder aUnidades);
        int fObtieneUnidadesPendientesCarac(string aConceptoDocto, string aCodigoProducto, string aCodigoAlmacen, string aValorCaracteristica1, string aValorCaracteristica2, string aValorCaracteristica3, StringBuilder aUnidades);
        int fPosAnteriorAgente();
        int fPosAnteriorAlmacen();
        int fPosAnteriorClasificacion();
        int fPosAnteriorConceptoDocto();
        int fPosAnteriorCteProv();
        int fPosAnteriorDireccion();
        int fPosAnteriorDocumento();
        int fPosAnteriorMovimiento();
        int fPosAnteriorProducto();
        int fPosAnteriorUnidad();
        int fPosAnteriorValorClasif();
        int fPosBOF();
        int fPosBOFAgente();
        int fPosBOFAlmacen();
        int fPosBOFClasificacion();
        int fPosBOFConceptoDocto();
        int fPosBOFCteProv();
        int fPosBOFDireccion();
        int fPosBOFProducto();
        int fPosBOFUnidad();
        int fPosBOFValorClasif();
        int fPosEOF();
        int fPosEOFAgente();
        int fPosEOFAlmacen();
        int fPosEOFClasificacion();
        int fPosEOFConceptoDocto();
        int fPosEOFCteProv();
        int fPosEOFDireccion();
        int fPosEOFMovtoContable();
        int fPosEOFProducto();
        int fPosEOFUnidad();
        int fPosEOFValorClasif();
        int fPosMovimientoBOF();
        int fPosMovimientoEOF();
        int fPosPrimerAgente();
        int fPosPrimerAlmacen();
        int fPosPrimerClasificacion();
        int fPosPrimerConceptoDocto();
        int fPosPrimerCteProv();
        int fPosPrimerDireccion();
        int fPosPrimerDocumento();
        int fPosPrimerEmpresa(ref int aIdEmpresa, StringBuilder aNombreEmpresa, StringBuilder aDirectorioEmpresa);
        int fPosPrimerMovimiento();
        int fPosPrimerMovtoContable();
        int fPosPrimerProducto();
        int fPosPrimerUnidad();
        int fPosPrimerValorClasif();
        int fPosSiguienteAgente();
        int fPosSiguienteAlmacen();
        int fPosSiguienteClasificacion();
        int fPosSiguienteConceptoDocto();
        int fPosSiguienteCteProv();
        int fPosSiguienteDireccion();
        int fPosSiguienteDocumento();
        int fPosSiguienteEmpresa(ref int aIdEmpresa, StringBuilder aNombreEmpresa, StringBuilder aDirectorioEmpresa);
        int fPosSiguienteMovimiento();
        int fPosSiguienteMovtoContable();
        int fPosSiguienteProducto();
        int fPosSiguienteUnidad();
        int fPosSiguienteValorClasif();
        int fPosUltimaConceptoDocto();
        int fPosUltimaDireccion();
        int fPosUltimoAgente();
        int fPosUltimoAlmacen();
        int fPosUltimoClasificacion();
        int fPosUltimoCteProv();
        int fPosUltimoDocumento();
        int fPosUltimoMovimiento();
        int fPosUltimoProducto();
        int fPosUltimoUnidad();
        int fPosUltimoValorClasif();
        int fProyectoEmpresaDoctos(string aCodigoProyecto);
        int fRecosteoProducto(string aCodigoProducto, int aEjercicio, int aPeriodo, string aCodigoClasificacion1, string aCodigoClasificacion2, string aCodigoClasificacion3, string aCodigoClasificacion4, string aCodigoClasificacion5, string aCodigoClasificacion6, string aNombreBitacora, int aSobreEscribirBitacora, int aEsCalculoArimetico);
        int fRecuperarRelacionesCFDIs(string aCodConcepto, string aSerie, string aFolio, string aTipoRelacion, StringBuilder aUUIDs, string aRutaNombreArchivoInfo);
        int fRecuperaTipoProducto(ref bool aUnidades, ref bool aSerie, ref bool aLote, ref bool aPedimento, ref bool aCaracteristicas);
        int fRegresaCostoCapa(string aCodigoProducto, string aCodigoAlmacen, double aUnidades, StringBuilder aImporteCosto);
        int fRegresaCostoEstandar(string aCodigoProducto, StringBuilder aCostoEstandar);
        int fRegresaCostoPromedio(string aCodigoProducto, string aCodigoAlmacen, string aAnio, string aMes, string aDia, StringBuilder aCostoPromedio);
        int fRegresaExistencia(string aCodigoProducto, string aCodigoAlmacen, string aAnio, string aMes, string aDia, ref double aExistencia);
        int fRegresaExistenciaCaracteristicas(string aCodigoProducto, string aCodigoAlmacen, string aAnio, string aMes, string aDia, string abreviaturaValorCaracteristica1, string abreviaturaValorCaracteristica2, string abreviaturaValorCaracteristica3, ref double aExistencia);
        int fRegresaExistenciaLotePedimento(string aCodigoProducto, string aCodigoAlmacen, string aPedimento, string aLote, ref double aExistencia);
        int fRegresaIVACargo(tLlaveDoc aLlaveDocto, double aNetoTasa15, double aNetoTasa10, double aNetoTasaCero, double aNetoTasaExcenta, double aNetoOtrasTasas, double aIVATasa15, double aIVATasa10, double aIVAOtrasTasas);
        int fRegresaIVACargo_2010(tLlaveDoc aLlaveDocto, double aNetoTasa15, double aNetoTasa10, double aNetoTasa16, double aNetoTasa11, double aNetoTasaCero, double aNetoTasaExcenta, double aNetoOtrasTasas, double aIVATasa15, double aIVATasa10, double aIVATasa16, double aIVATasa11, double aIVAOtrasTasas);
        int fRegresaIVACargoRet_2010(tLlaveDoc aLlaveDocto, double aNetoTasa15, double aNetoTasa10, double aNetoTasa16, double aNetoTasa11, double aNetoTasaCero, double aNetoTasaExcenta, double aNetoOtrasTasas, double aIVATasa15, double aIVATasa10, double aIVATasa16, double aIVATasa11, double aIVAOtrasTasas, double aRetIVA, double aRetI);
        int fRegresaIVAPago(tLlaveDoc aLlaveDocto, double aNetoTasa15, double aNetoTasa10, double aNetoTasaCero, double aNetoTasaExcenta, double aNetoOtrasTasas, double aIVATasa15, double aIVATasa10, double aIVAOtrasTasas);
        int fRegresaIVAPago_2010(tLlaveDoc aLlaveDocto, double aNetoTasa15, double aNetoTasa10, double aNetoTasa16, double aNetoTasa11, double aNetoTasaCero, double aNetoTasaExcenta, double aNetoOtrasTasas, double aIVATasa15, double aIVATasa10, double aIVATasa16, double aIVATasa11, double aIVAOtrasTasas);
        int fRegresaIVAPagoRet_2010(tLlaveDoc aLlaveDocto, double aNetoTasa15, double aNetoTasa10, double aNetoTasa16, double aNetoTasa11, double aNetoTasaCero, double aNetoTasaExcenta, double aNetoOtrasTasas, double aIVATasa15, double aIVATasa10, double aIVATasa16, double aIVATasa11, double aIVAOtrasTasas, double aRetIVA, double aRetI);
        int fRegresaPrecioVenta(string aCodigoConcepto, string aCodigoCliente, string aCodigoProducto, StringBuilder aPrecioVenta);
        int fRegresaUltimoCosto(string aCodigoProducto, string aCodigoAlmacen, string aAnio, string aMes, string aDia, StringBuilder aUltimoCosto);
        int fRegresPorcentajeImpuesto(int aIdConceptoDocumento, int aIdClienteProveedor, int aIdProducto, ref double aPorcentajeImpuesto);
        int fSaldarDocumento(ref tLlaveDoc aDoctoaPagar, ref tLlaveDoc aDoctoPago, double aImporte, int aIdMoneda, string aFecha);
        int fSaldarDocumento_Param(string aCodConcepto_Pagar, string aSerie_Pagar, double aFolio_Pagar, string aCodConcepto_Pago, string aSerie_Pago, double aFolio_Pago, double aImporte, int aIdMoneda, string aFecha);
        int fSaldarDocumentoCheqPAQ(tLlaveDoc aDoctoaPagar, tLlaveDoc aDoctoPago, double aImporte, int aIdMoneda, string aFecha, double aTipoCambioCheqPAQ);
        int fSetDatoAgente(string aCampo, string aValor);
        int fSetDatoAlmacen(string aCampo, string aValor);
        int fSetDatoClasificacion(string aCampo, string aValor);
        int fSetDatoConceptoDocto(string aCampo, string aValor);
        int fSetDatoCteProv(string aCampo, string aValor);
        int fSetDatoDireccion(string aCampo, string aValor);
        int fSetDatoDocumento(string aCampo, string aValor);
        int fSetDatoMovimiento(string aCampo, string aValor);
        int fSetDatoMovtoContable(string aCampo, string aValor);
        int fSetDatoParametros(string aCampo, StringBuilder aValor);
        int fSetDatoProducto(string aCampo, string aValor);
        int fSetDatoUnidad(string aCampo, string aValor);
        int fSetDatoValorClasif(string aCampo, string aValor);
        int fSetDescripcionProducto(string aCampo, string aValor);
        int fSetFiltroDocumento(string aFechaInicio, string aFechaFin, string aCodigoConcepto, string aCodigoCteProv);
        int fSetFiltroMovimiento(int aIdDocumento);
        void fSetModoImportacion(bool aImportacion);
        int fSetNombrePAQ(string aSistema);
        int fSiguienteFolio(string aCodigoConcepto, StringBuilder aSerie, ref double aFolio);
        void fTerminaSDK();
        int fTimbraComplementoPagoXML(string aRutaXML, string aCodConcepto, StringBuilder aUUID, string aRutaDDA, string aRutaResultado, string aPass, string aRutaFormato);
        int fTimbraComplementoXML(string aRutaXML, string aCodCOncepto, StringBuilder aUUID, string aRutaDDA, string aRutaResultado, string aPass, string aRutaFormato, int aComplemento);
        int fTimbraNominaXML(string aRutaXML, string aCodCOncepto, StringBuilder aUUID, string aRutaDDA, string aRutaResultado, string aPass, string aRutaFormato);
        int fTimbraXML(string aRutaXML, string aCodCOncepto, StringBuilder aUUID, string aRutaDDA, string aRutaResultado, string aPass, string aRutaFormato);
        int InicializarSDK();
        int InicializarSDK(string usuario, string password);
        int InicializarSDK(string usuario, string password, string usuarioContabilidad, string passwordContabilidad);
        string LeeDatoAgente(string dato, int longitud);
        string LeeDatoAlmacen(string dato, int longitud);
        string LeeDatoCfdi(int dato, int longitud);
        string LeeDatoClasificacion(string dato, int longitud);
        string LeeDatoClienteProveedor(string dato, int longitud);
        string LeeDatoConcepto(string dato, int longitud);
        string LeeDatoDireccion(string dato, int longitud);
        string LeeDatoDocumento(string dato, int longitud);
        string LeeDatoMovimiento(string dato, int longitud);
        string LeeDatoParametros(string dato, int longitud);
        string LeeDatoProducto(string dato, int longitud);
        string LeeDatoUnidad(string dato, int longitud);
        string LeeDatoValorClasificacion(string dato, int longitud);
        string LeeMensajeError(int numeroError);
        void SetCurrentDirectory();

    }
    /** ********************************************************************* **/
    /** INTERFACE PARA EVENTOS DE LA CLASE **/
    /** ********************************************************************* **/
    [ComVisible(true)]
    [Guid("7ee9db21-897d-4d87-80ad-57da95b7a520"), InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IComercialEvents
    {
        string test();
        int open_SDK(string aNombreEmpresa);
        void close_SDK();
        int fAbreEmpresa(string aDirectorioEmpresa);
        int fActivarPrecioCompra(int aActivar);
        int fActualizaClasificacion(int aClasificacionDe, int aNumClasificacion, string aNombreClasificacion);
        int fActualizaCteProv(string aCodigoCteProv, ref tCteProv astCteProv);
        int fActualizaDireccion(ref tDireccion astDireccion);
        int fActualizaProducto(string aCodigoProducto, ref tProducto astProducto);
        int fActualizaUnidad(string aNombreUnidad, ref tUnidad astUnidad);
        int fActualizaValorClasif(string aCodigoValorClasif, ref tValorClasificacion astValorClasif);
        int fAfectaDocto(ref tLlaveDoc aLlaveDocto, bool aAfecta);
        int fAfectaDocto_Param(string aCodConcepto, string aSerie, double aFolio, bool aAfecta);
        int fAfectaSerie(int aIdMovto, string aNumeroSerie);
        int fAgregarRelacionCFDI(string aCodConcepto, string aSerie, string aFolio, string aTipoRelacion, string aConceptoRelacionar, string aSerieRelacionar, string aFolioRelacionar);
        int fAgregarRelacionCFDI2(string aCodConcepto, string aSerie, string aFolio, string aTipoRelacion, string aUUID);
        int fAltaCteProv(ref int aIdCteProv, ref tCteProv astCteProv);
        int fAltaCuentaBancariaCliente(ref int aIdCtaBancaria, string aCuentaBancaria, string aNombreCuenta, string aNombreMoneda, string aClaveBanco, string aClabe, string aRfcBanco, string aNombreBancoExtranjero, string aCodigoCliente);
        int fAltaCuentaBancariaEmpresa(ref int aIdCtaBancaria, string aCuentaBancaria, string aNombreCuenta, string aNombreMoneda, string aClaveBanco, string aClabe, string aRfcBanco, string aNombreBancoExtranjero);
        int fAltaDireccion(ref int aIdDireccion, ref tDireccion astDireccion);
        int fAltaDoctoAjusteIESPSCteProv(string aCodigoClienteProveedor, int aEsCliente, string aFechaDocto, int aIdMoneda, double aTipoCambio, double aImporteIVA, double aTasaIVA, double aImporteIESPS, double aTasaIESPS, int aIdFacturaBase, string aMetodo, string aLugar, ref int aIdDoctoGenerado);
        int fAltaDoctoAjusteIVAClienteProveedor(string aCodigoClienteProveedor, int aEsCliente, int aAbsorberAjusteIVA, string aFechaDocto, int aIdMoneda, double aTipoCambio, double aImporteIVA, double aTasaIVA, int aIdFacturaBase, string aMetodo, string aLugar, ref int aIdDoctoGenerado);
        int fAltaDocumento(ref int aIdDocumento, ref tDocumento aDocumento);
        int fAltaDocumentoCargoAbono(ref tDocumento aDocumento);
        int fAltaDocumentoCargoAbonoExtras(ref tDocumento aDocumento, string aTextoExtra1, string aTextoExtra2, string aTextoExtra3, string aFechaExtra, double aImporteExtra1, double aImporteExtra2, double aImporteExtra3, double aImporteExtra4);
        int fAltaMovimiento(int aIdDocumento, ref int aIdMovimiento, ref tMovimiento astMovimiento);
        int fAltaMovimientoCaracteristicas(int aIdMovimiento, ref int aIdMovtoCaracteristicas, ref tCaracteristicas aCaracteristicas);
        int fAltaMovimientoCaracteristicas_Param(string aIdMovimiento, string aIdMovtoCaracteristicas, string aUnidades, string aValorCaracteristica1, string aValorCaracteristica2, string aValorCaracteristica3);
        int fAltaMovimientoCDesct(int aIdDocumento, ref int aIdMovimiento, ref tMovimientoDesc astMovimiento);
        int fAltaMovimientoEx(ref int aIdMovimiento, ref tTipoProducto aTipoProducto);
        int fAltaMovimientoSeriesCapas(int aIdMovimiento, ref tSeriesCapas aSeriesCapas);
        int fAltaMovimientoSeriesCapas_Param(string aIdMovimiento, string aUnidades, string aTipoCambio, string aSeries, string aPedimento, string aAgencia, string aFechaPedimento, string aNumeroLote, string aFechaFabricacion, string aFechaCaducidad);
        int fAltaMovtoCaracteristicasUnidades(int aIdMovimiento, ref int aIdMovtoCaracteristicas, ref tCaracteristicas aCaracteristicasUnidades);
        int fAltaMovtoCaracteristicasUnidades_Param(string aIdMovimiento, string aIdMovtoCaracteristicas, string aUnidad, string aUnidades, string aUnidadesNC, string aValorCaracteristica1, string aValorCaracteristica2, string aValorCaracteristica3);
        int fAltaProducto(ref int aIdProducto, ref tProducto astProducto);
        int fAltaUnidad(ref int aIdUnidad, ref tUnidad astUnidad);
        int fAltaValorClasif(ref int aIdValorClasif, ref tValorClasificacion astValorClasif);
        int fBorraCteProv();
        int fBorraCuentaBancariaCliente(string aCuentaBancaria, string aCodigoCliente);
        int fBorraCuentaBancariaEmpresa(string aCuentaBancaria);
        int fBorraDocumento();
        int fBorraDocumento_CW();
        int fBorraMovimiento(int aIdDocumento, int aIdMovimiento);
        int fBorraProducto();
        int fBorrarAsociacion(tLlaveDoc aDoctoaPagar, tLlaveDoc aDoctoPago);
        int fBorrarAsociacion_Param(string aCodConcepto_Pagar, string aSerie_Pagar, double aFolio_Pagar, string aCodConcepto_Pago, string aSerie_Pago, double aFolio_Pago);
        int fBorraUnidad();
        int fBorraValorClasif();
        int fBuscaAgente(string aCodigoAgente);
        int fBuscaAlmacen(string aCodigoAlmacen);
        int fBuscaClasificacion(int aClasificacionDe, int aNumClasificacion);
        int fBuscaConceptoDocto(string aCodConcepto);
        int fBuscaCteProv(string aCodCteProv);
        int fBuscaDireccionCteProv(string aCodCteProv, byte aTipoDireccion);
        int fBuscaDireccionDocumento(int aIdDocumento, byte aTipoDireccion);
        int fBuscaDireccionEmpresa();
        int fBuscaDocumento(ref tLlaveDoc aLlaveDocto);
        int fBuscaIdAgente(int aIdAgente);
        int fBuscaIdAlmacen(int aIdAlmacen);
        int fBuscaIdClasificacion(int aIdClasificacion);
        int fBuscaIdConceptoDocto(int aIdConcepto);
        int fBuscaIdCteProv(int aIdCteProv);
        int fBuscaIdProducto(int aIdProducto);
        int fBuscaIdUnidad(int aIdUnidad);
        int fBuscaIdValorClasif(int aIdValorClasif);
        int fBuscaProducto(string aCodProducto);
        int fBuscarDocumento(string aCodConcepto, string aSerie, string aFolio);
        int fBuscarIdDocumento(int aIdDocumento);
        int fBuscarIdMovimiento(int aIdMovimiento);
        int fBuscaUnidad(string aNombreUnidad);
        int fBuscaValorClasif(int aClasificacionDe, int aNumClasificacion, string aCodValorClasif);
        int fCalculaMovtoSerieCapa(int aIdMovimiento);
        int fCancelaCambiosMovimiento();
        int fCancelaComplementoPagoUUID(string aUUID, string aIdDConcepto, string aPass);
        int fCancelaDoctoInfo(string aPass);
        int fCancelaDocumento();
        int fCancelaDocumento_CW();
        int fCancelaDocumentoAdministrativamente();
        int fCancelaDocumentoConMotivo(string aMotivoCancelacion, string aUUIDRemplaza);
        int fCancelaFiltroDocumento();
        int fCancelaFiltroMovimiento();
        int fCancelaNominaUUID(string aUUID, string aIdDConcepto, string aPass);
        int fCancelarModificacionAgente();
        int fCancelarModificacionAlmacen();
        int fCancelarModificacionClasificacion();
        int fCancelarModificacionCteProv();
        int fCancelarModificacionDireccion();
        int fCancelarModificacionDocumento();
        int fCancelarModificacionProducto();
        int fCancelarModificacionUnidad();
        int fCancelarModificacionValorClasif();
        int fCancelaUUID(string aUUID, string aIdDConcepto, string aPass);
        int fCancelaUUID40(string aUUID, string aMotivoCancelacion, string aUUIDReemplaza, string RFCReceptor, double aTotal, string aIdDConcepto, string aPass, ref int aEstatusCancelacion);
        void fCierraEmpresa();
        int fCuentaBancariaEmpresaDoctos(string aCuentaBancaria);
        int fDesbloqueaDocumento();
        int fDocumentoBloqueado(ref int aImpreso);
        int fDocumentoDevuelto(ref int aDevuelto);
        int fDocumentoImpreso(ref bool aImpreso);
        int fDocumentoUUID(StringBuilder aCodConcepto, StringBuilder aSerie, double aFolio, StringBuilder atPtrCFDIUUID);
        int fEditaAgente();
        int fEditaAlmacen();
        int fEditaClasificacion();
        int fEditaConceptoDocto();
        int fEditaCteProv();
        int fEditaDireccion();
        int fEditaMovtoContable();
        int fEditaParametros();
        int fEditaProducto();
        int fEditarDocumento();
        int fEditarDocumentoCheqpaq();
        int fEditarMovimiento();
        int fEditaUnidad();
        int fEditaValorClasif();
        int fEliminarCteProv(string aCodigoCteProv);
        int fEliminarProducto(string aCodigoProducto);
        int fEliminarRelacionesCFDIs(string aCodConcepto, string aSerie, string aFolio);
        int fEliminarUnidad(string aNombreUnidad);
        int fEliminarValorClasif(int aClasificacionDe, int aNumClasificacion, string aCodigoValorClasif);
        int fEmitirDocumento(string aCodConcepto, string aSerie, double aFolio, string aPassword, string aArchivoAdicional);
        int fEntregEnDiscoXML(string aCodConcepto, string aSerie, double aFolio, int aFormato, string aFormatoAmig);
        void fError(int aNumError, StringBuilder aMensaje, int aLen);
        int fGetCantidadParcialidades(StringBuilder atPtrPassword, StringBuilder aCantidadParcialidades);
        int fGetDatosCFDI(StringBuilder aSerieCertEmisor, StringBuilder aFolioFiscalUUID, StringBuilder aSerieCertSAT, StringBuilder aFechaHora, StringBuilder aSelloDigCFDI, StringBuilder aSelloSAAT, StringBuilder aCadOrigComplSAT, StringBuilder aRegimen, StringBuilder aLugarExpedicion, StringBuilder aMoneda, StringBuilder aFolioFiscalOrig, StringBuilder aSerieFolioFiscalOrig, StringBuilder aFechaFolioFiscalOrig, StringBuilder aMontoFolioFiscalOrig);
        int fGetNumParcialidades(StringBuilder atPtrPassword, StringBuilder aNumParcialidades);
        int fGetSelloDigitalYCadena(StringBuilder atPtrPassword, StringBuilder atPtrSelloDigital, StringBuilder atPtrCadenaOriginal);
        int fGetSerieCertificado(StringBuilder atPtrPassword, StringBuilder aSerieCertificado);
        int fGetTamSelloDigitalYCadena(StringBuilder atPtrPassword, ref int aEspSelloDig, ref int aEspCadOrig);
        int fGuardaAgente();
        int fGuardaAlmacen();
        int fGuardaClasificacion();
        int fGuardaConceptoDocto();
        int fGuardaCteProv();
        int fGuardaDireccion();
        int fGuardaDocumento();
        int fGuardaMovimiento();
        int fGuardaMovtoContable();
        int fGuardaParametros();
        int fGuardaProducto();
        int fGuardaUnidad();
        int fGuardaValorClasif();
        int fInformacionCliente(StringBuilder aCodigo, ref int aPermiteCredito, ref double aLimiteCredito, ref int aLimiteDoctosVencidos, ref int aPermiteExcederCredito, StringBuilder aFecha, ref double aSaldo, ref double aSaldoPendiente, ref int aDoctosVencidos);
        int fInicializaLicenseInfo(byte aSistema);
        int fInicializaSDK();
        void fInicioSesionSDK(string aUsuario, string aContrasenia);
        void fInicioSesionSDKCONTPAQi(string aUsuario, string aContrasenia);
        int fInsertaAgente();
        int fInsertaAlmacen();
        int fInsertaCteProv();
        int fInsertaDatoAddendaDocto(int aIdAddenda, int aIdCatalogo, int aNumCampo, string aDato);
        int fInsertaDatoAddendaMovto(int aIdAddenda, int aIdCatalogo, int aNumCampo, string aDato);
        int fInsertaDatoCompEducativo(int aIdServicio, int aNumCampo, string aDato);
        int fInsertaDireccion();
        int fInsertaProducto();
        int fInsertarDocumento();
        int fInsertarMovimiento();
        int fInsertaUnidad();
        int fInsertaValorClasif();
        int fLeeDatoAgente(string aCampo, StringBuilder aValor, int aLen);
        string fLeeDatoAlmacen(string aCampo);
        int fLeeDatoCFDI(StringBuilder aValor, int aDato);
        int fLeeDatoClasificacion(string aCampo, StringBuilder aValor, int aLen);
        int fLeeDatoConceptoDocto(string aCampo, StringBuilder aValor, int aLen);
        int fLeeDatoCteProv(string aCampo, StringBuilder aValor, int aLen);
        int fLeeDatoDireccion(string aCampo, StringBuilder aValor, int aLen);
        int fLeeDatoDocumento(string aCampo, StringBuilder aValor, int aLongitud);
        int fLeeDatoMovimiento(string aCampo, StringBuilder aValor, int aLen);
        int fLeeDatoMovtoContable(string aCampo, StringBuilder aValor, int aLen);
        int fLeeDatoParametros(string aCampo, StringBuilder aValor, int aLen);
        int fLeeDatoProducto(string aCampo, StringBuilder aValor, int aLen);
        int fLeeDatoUnidad(string aCampo, StringBuilder aValor, int aLen);
        int fLeeDatoValorClasif(string aCampo, StringBuilder aValor, int aLen);
        int fLlenaRegistroCteProv(ref tCteProv astCteProv, int aEsAlta);
        int fLlenaRegistroDireccion(ref tDireccion astDireccion, int aEsAlta);
        int fLlenaRegistroProducto(ref tProducto astProducto, int aEsAlta);
        int fLlenaRegistroUnidad(ref tUnidad astUnidad);
        int fLlenaRegistroValorClasif(ref tValorClasificacion astValorClasif);
        int fModificaCostoEntrada(string aIdMovimiento, string aCostoEntrada);
        int fModificaCuentaBancariaCliente(string aCuentaBancaria, string aNombreCuenta, string aNombreMoneda, string aClaveBanco, string aRfcBanco, string aClabe, string aNombreExtranjero, string aCodigoCliente);
        int fModificaCuentaBancariaEmpresa(string aCuentaBancaria, string aNombreCuenta, string aNombreMoneda, string aClaveBanco, string aRfcBanco, string aClabe, string aNombreExtranjero);
        int fObtenCeryKey(int aIdFirmarl, string aRutaKey, string aRutaCer);
        int fObtieneDatosCFDI(string atPtrPassword);
        int fObtieneLicencia(StringBuilder aCodAcvtiva, StringBuilder aCodSitio, StringBuilder aSerie, StringBuilder aTagVersion);
        int fObtienePassProxy(StringBuilder aPassProxy);
        int fObtieneUnidadesPendientes(string aConceptoDocto, string aCodigoProducto, string aCodigoAlmacen, StringBuilder aUnidades);
        int fObtieneUnidadesPendientesCarac(string aConceptoDocto, string aCodigoProducto, string aCodigoAlmacen, string aValorCaracteristica1, string aValorCaracteristica2, string aValorCaracteristica3, StringBuilder aUnidades);
        int fPosAnteriorAgente();
        int fPosAnteriorAlmacen();
        int fPosAnteriorClasificacion();
        int fPosAnteriorConceptoDocto();
        int fPosAnteriorCteProv();
        int fPosAnteriorDireccion();
        int fPosAnteriorDocumento();
        int fPosAnteriorMovimiento();
        int fPosAnteriorProducto();
        int fPosAnteriorUnidad();
        int fPosAnteriorValorClasif();
        int fPosBOF();
        int fPosBOFAgente();
        int fPosBOFAlmacen();
        int fPosBOFClasificacion();
        int fPosBOFConceptoDocto();
        int fPosBOFCteProv();
        int fPosBOFDireccion();
        int fPosBOFProducto();
        int fPosBOFUnidad();
        int fPosBOFValorClasif();
        int fPosEOF();
        int fPosEOFAgente();
        int fPosEOFAlmacen();
        int fPosEOFClasificacion();
        int fPosEOFConceptoDocto();
        int fPosEOFCteProv();
        int fPosEOFDireccion();
        int fPosEOFMovtoContable();
        int fPosEOFProducto();
        int fPosEOFUnidad();
        int fPosEOFValorClasif();
        int fPosMovimientoBOF();
        int fPosMovimientoEOF();
        int fPosPrimerAgente();
        int fPosPrimerAlmacen();
        int fPosPrimerClasificacion();
        int fPosPrimerConceptoDocto();
        int fPosPrimerCteProv();
        int fPosPrimerDireccion();
        int fPosPrimerDocumento();
        int fPosPrimerEmpresa(ref int aIdEmpresa, StringBuilder aNombreEmpresa, StringBuilder aDirectorioEmpresa);
        int fPosPrimerMovimiento();
        int fPosPrimerMovtoContable();
        int fPosPrimerProducto();
        int fPosPrimerUnidad();
        int fPosPrimerValorClasif();
        int fPosSiguienteAgente();
        int fPosSiguienteAlmacen();
        int fPosSiguienteClasificacion();
        int fPosSiguienteConceptoDocto();
        int fPosSiguienteCteProv();
        int fPosSiguienteDireccion();
        int fPosSiguienteDocumento();
        int fPosSiguienteEmpresa(ref int aIdEmpresa, StringBuilder aNombreEmpresa, StringBuilder aDirectorioEmpresa);
        int fPosSiguienteMovimiento();
        int fPosSiguienteMovtoContable();
        int fPosSiguienteProducto();
        int fPosSiguienteUnidad();
        int fPosSiguienteValorClasif();
        int fPosUltimaConceptoDocto();
        int fPosUltimaDireccion();
        int fPosUltimoAgente();
        int fPosUltimoAlmacen();
        int fPosUltimoClasificacion();
        int fPosUltimoCteProv();
        int fPosUltimoDocumento();
        int fPosUltimoMovimiento();
        int fPosUltimoProducto();
        int fPosUltimoUnidad();
        int fPosUltimoValorClasif();
        int fProyectoEmpresaDoctos(string aCodigoProyecto);
        int fRecosteoProducto(string aCodigoProducto, int aEjercicio, int aPeriodo, string aCodigoClasificacion1, string aCodigoClasificacion2, string aCodigoClasificacion3, string aCodigoClasificacion4, string aCodigoClasificacion5, string aCodigoClasificacion6, string aNombreBitacora, int aSobreEscribirBitacora, int aEsCalculoArimetico);
        int fRecuperarRelacionesCFDIs(string aCodConcepto, string aSerie, string aFolio, string aTipoRelacion, StringBuilder aUUIDs, string aRutaNombreArchivoInfo);
        int fRecuperaTipoProducto(ref bool aUnidades, ref bool aSerie, ref bool aLote, ref bool aPedimento, ref bool aCaracteristicas);
        int fRegresaCostoCapa(string aCodigoProducto, string aCodigoAlmacen, double aUnidades, StringBuilder aImporteCosto);
        int fRegresaCostoEstandar(string aCodigoProducto, StringBuilder aCostoEstandar);
        int fRegresaCostoPromedio(string aCodigoProducto, string aCodigoAlmacen, string aAnio, string aMes, string aDia, StringBuilder aCostoPromedio);
        int fRegresaExistencia(string aCodigoProducto, string aCodigoAlmacen, string aAnio, string aMes, string aDia, ref double aExistencia);
        int fRegresaExistenciaCaracteristicas(string aCodigoProducto, string aCodigoAlmacen, string aAnio, string aMes, string aDia, string abreviaturaValorCaracteristica1, string abreviaturaValorCaracteristica2, string abreviaturaValorCaracteristica3, ref double aExistencia);
        int fRegresaExistenciaLotePedimento(string aCodigoProducto, string aCodigoAlmacen, string aPedimento, string aLote, ref double aExistencia);
        int fRegresaIVACargo(tLlaveDoc aLlaveDocto, double aNetoTasa15, double aNetoTasa10, double aNetoTasaCero, double aNetoTasaExcenta, double aNetoOtrasTasas, double aIVATasa15, double aIVATasa10, double aIVAOtrasTasas);
        int fRegresaIVACargo_2010(tLlaveDoc aLlaveDocto, double aNetoTasa15, double aNetoTasa10, double aNetoTasa16, double aNetoTasa11, double aNetoTasaCero, double aNetoTasaExcenta, double aNetoOtrasTasas, double aIVATasa15, double aIVATasa10, double aIVATasa16, double aIVATasa11, double aIVAOtrasTasas);
        int fRegresaIVACargoRet_2010(tLlaveDoc aLlaveDocto, double aNetoTasa15, double aNetoTasa10, double aNetoTasa16, double aNetoTasa11, double aNetoTasaCero, double aNetoTasaExcenta, double aNetoOtrasTasas, double aIVATasa15, double aIVATasa10, double aIVATasa16, double aIVATasa11, double aIVAOtrasTasas, double aRetIVA, double aRetI);
        int fRegresaIVAPago(tLlaveDoc aLlaveDocto, double aNetoTasa15, double aNetoTasa10, double aNetoTasaCero, double aNetoTasaExcenta, double aNetoOtrasTasas, double aIVATasa15, double aIVATasa10, double aIVAOtrasTasas);
        int fRegresaIVAPago_2010(tLlaveDoc aLlaveDocto, double aNetoTasa15, double aNetoTasa10, double aNetoTasa16, double aNetoTasa11, double aNetoTasaCero, double aNetoTasaExcenta, double aNetoOtrasTasas, double aIVATasa15, double aIVATasa10, double aIVATasa16, double aIVATasa11, double aIVAOtrasTasas);
        int fRegresaIVAPagoRet_2010(tLlaveDoc aLlaveDocto, double aNetoTasa15, double aNetoTasa10, double aNetoTasa16, double aNetoTasa11, double aNetoTasaCero, double aNetoTasaExcenta, double aNetoOtrasTasas, double aIVATasa15, double aIVATasa10, double aIVATasa16, double aIVATasa11, double aIVAOtrasTasas, double aRetIVA, double aRetI);
        int fRegresaPrecioVenta(string aCodigoConcepto, string aCodigoCliente, string aCodigoProducto, StringBuilder aPrecioVenta);
        int fRegresaUltimoCosto(string aCodigoProducto, string aCodigoAlmacen, string aAnio, string aMes, string aDia, StringBuilder aUltimoCosto);
        int fRegresPorcentajeImpuesto(int aIdConceptoDocumento, int aIdClienteProveedor, int aIdProducto, ref double aPorcentajeImpuesto);
        int fSaldarDocumento(ref tLlaveDoc aDoctoaPagar, ref tLlaveDoc aDoctoPago, double aImporte, int aIdMoneda, string aFecha);
        int fSaldarDocumento_Param(string aCodConcepto_Pagar, string aSerie_Pagar, double aFolio_Pagar, string aCodConcepto_Pago, string aSerie_Pago, double aFolio_Pago, double aImporte, int aIdMoneda, string aFecha);
        int fSaldarDocumentoCheqPAQ(tLlaveDoc aDoctoaPagar, tLlaveDoc aDoctoPago, double aImporte, int aIdMoneda, string aFecha, double aTipoCambioCheqPAQ);
        int fSetDatoAgente(string aCampo, string aValor);
        int fSetDatoAlmacen(string aCampo, string aValor);
        int fSetDatoClasificacion(string aCampo, string aValor);
        int fSetDatoConceptoDocto(string aCampo, string aValor);
        int fSetDatoCteProv(string aCampo, string aValor);
        int fSetDatoDireccion(string aCampo, string aValor);
        int fSetDatoDocumento(string aCampo, string aValor);
        int fSetDatoMovimiento(string aCampo, string aValor);
        int fSetDatoMovtoContable(string aCampo, string aValor);
        int fSetDatoParametros(string aCampo, StringBuilder aValor);
        int fSetDatoProducto(string aCampo, string aValor);
        int fSetDatoUnidad(string aCampo, string aValor);
        int fSetDatoValorClasif(string aCampo, string aValor);
        int fSetDescripcionProducto(string aCampo, string aValor);
        int fSetFiltroDocumento(string aFechaInicio, string aFechaFin, string aCodigoConcepto, string aCodigoCteProv);
        int fSetFiltroMovimiento(int aIdDocumento);
        void fSetModoImportacion(bool aImportacion);
        int fSetNombrePAQ(string aSistema);
        int fSiguienteFolio(string aCodigoConcepto, StringBuilder aSerie, ref double aFolio);
        void fTerminaSDK();
        int fTimbraComplementoPagoXML(string aRutaXML, string aCodConcepto, StringBuilder aUUID, string aRutaDDA, string aRutaResultado, string aPass, string aRutaFormato);
        int fTimbraComplementoXML(string aRutaXML, string aCodCOncepto, StringBuilder aUUID, string aRutaDDA, string aRutaResultado, string aPass, string aRutaFormato, int aComplemento);
        int fTimbraNominaXML(string aRutaXML, string aCodCOncepto, StringBuilder aUUID, string aRutaDDA, string aRutaResultado, string aPass, string aRutaFormato);
        int fTimbraXML(string aRutaXML, string aCodCOncepto, StringBuilder aUUID, string aRutaDDA, string aRutaResultado, string aPass, string aRutaFormato);
        int InicializarSDK();
        int InicializarSDK(string usuario, string password);
        int InicializarSDK(string usuario, string password, string usuarioContabilidad, string passwordContabilidad);
        string LeeDatoAgente(string dato, int longitud);
        string LeeDatoAlmacen(string dato, int longitud);
        string LeeDatoCfdi(int dato, int longitud);
        string LeeDatoClasificacion(string dato, int longitud);
        string LeeDatoClienteProveedor(string dato, int longitud);
        string LeeDatoConcepto(string dato, int longitud);
        string LeeDatoDireccion(string dato, int longitud);
        string LeeDatoDocumento(string dato, int longitud);
        string LeeDatoMovimiento(string dato, int longitud);
        string LeeDatoParametros(string dato, int longitud);
        string LeeDatoProducto(string dato, int longitud);
        string LeeDatoUnidad(string dato, int longitud);
        string LeeDatoValorClasificacion(string dato, int longitud);
        string LeeMensajeError(int numeroError);
        void SetCurrentDirectory();

    }
    /** ********************************************************************* **/
    /** CLASE PRINCIPAL DE IMPLEMENTACIÓN  **/
    /** ********************************************************************* **/
    [ComVisible(true)]
    [Guid("f8075bba-32cd-4468-8389-cb64ccf7869d")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComSourceInterfaces(typeof(IComercialEvents))]
    [ProgId("SdkContPAQi.Comercial")]
    public class Comercial : IComercial
    {
        public EventLog eventLog = new EventLog();
        public const string NombreLlaveRegistroComercial = @"SOFTWARE\\WOW6432Node\\Computación en Acción, SA CV\\CONTPAQ I COMERCIAL";
        public const string NombreLlaveRegistroContable = @"SOFTWARE\\Wow6432Node\\Computación en Acción, SA CV\\CONTPAQ I";
        public const string NombrePaqComercial = "CONTPAQ I COMERCIAL";
        public const string NombrePaqContable = "CONTPAQ I CONTABILIDAD";

        /* ************************************************************
        /* CONTRUCTOR DE LA CLASE PRINCIPAL
        ************************************************************ */
        public Comercial()
        {
            var keySistema = Registry.LocalMachine.OpenSubKey(NombreLlaveRegistroComercial);
            var lEntrada = keySistema.GetValue("DirectorioBase");
            Directory.SetCurrentDirectory(lEntrada.ToString());
        }

        public string test()
        {
            return "Hola.... :)";
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

        public int open_SDK(string aNombreEmpresa)
        {
            int IOResult = 0;
            NativeMethods.fInicioSesionSDK("PUNTOVENTAS", "XiYeme=R6G");
            NativeMethods.fInicioSesionSDKCONTPAQi("PUNTOVENTAS", "XiYeme=R6G");
            int nStart = NativeMethods.fSetNombrePAQ(NombrePaqComercial);
            if (nStart != 0)
            {
                StringBuilder strMensaje = new StringBuilder(512);
                NativeMethods.fError(nStart, strMensaje, 512);
                Console_log("Error ["+ strMensaje.ToString() + "] asignando el nombre del PAQ (fSetNombrePAQ): "+ NombrePaqComercial, EventLogEntryType.Error, 728);
                IOResult = 1;
            }
            else
            {
                string rutaEMPRESA_COM = "C:\\Compac\\Empresas\\" + aNombreEmpresa;
                nStart = NativeMethods.fAbreEmpresa(rutaEMPRESA_COM);
                if (nStart != 0)
                {
                    StringBuilder strMensaje = new StringBuilder(512);
                    NativeMethods.fError(nStart, strMensaje, 512);
                    Console_log("Error [" + strMensaje.ToString() + "] abriendo la empresa (aNombreEmpresa) en la ruta: " + rutaEMPRESA_COM, EventLogEntryType.Error, 739);
                    IOResult = 2;
                }
            }

            return IOResult;
        }

        public void close_SDK()
        {
            NativeMethods.fCierraEmpresa();
            NativeMethods.fTerminaSDK();
        }

        public int fAbreEmpresa(string aDirectorioEmpresa)
        {
            int sdkError = NativeMethods.fAbreEmpresa(aDirectorioEmpresa);
            if(sdkError != 0) //Error detectado en el SDK de Comercial
            {
                StringBuilder strMensaje = new StringBuilder(512);
                NativeMethods.fError(sdkError, strMensaje, 512);
                Console_log("Error fAbreEmpresa (" + aDirectorioEmpresa + "): " + strMensaje, EventLogEntryType.Error, 760);
            }
            return sdkError;
        }

        public int fActivarPrecioCompra(int aActivar)
        {
            return NativeMethods.fActivarPrecioCompra(aActivar);
        }

        public int fActualizaClasificacion(int aClasificacionDe, int aNumClasificacion, string aNombreClasificacion)
        {
            return NativeMethods.fActualizaClasificacion(aClasificacionDe, aNumClasificacion, aNombreClasificacion);
        }

        public int fActualizaCteProv(string aCodigoCteProv, ref tCteProv astCteProv)
        {
            return NativeMethods.fActualizaCteProv(aCodigoCteProv, ref astCteProv);
        }

        public int fActualizaDireccion(ref tDireccion astDireccion)
        {
            return NativeMethods.fActualizaDireccion(ref astDireccion);
        }

        public int fActualizaProducto(string aCodigoProducto, ref tProducto astProducto)
        {
            return NativeMethods.fActualizaProducto(aCodigoProducto, ref astProducto);
        }

        public int fActualizaUnidad(string aNombreUnidad, ref tUnidad astUnidad)
        {
            return NativeMethods.fActualizaUnidad(aNombreUnidad, ref astUnidad);
        }

        public int fActualizaValorClasif(string aCodigoValorClasif, ref tValorClasificacion astValorClasif)
        {
            return NativeMethods.fActualizaValorClasif(aCodigoValorClasif, ref astValorClasif);
        }

        public int fAfectaDocto(ref tLlaveDoc aLlaveDocto, bool aAfecta)
        {
            throw new NotImplementedException("Esta funcion no funciona en CONTPAQi Comercial.");
        }

        public int fAfectaDocto_Param(string aCodConcepto, string aSerie, double aFolio, bool aAfecta)
        {
            throw new NotImplementedException("Esta funcion no funciona en CONTPAQi Comercial.");
        }

        public int fAfectaSerie(int aIdMovto, string aNumeroSerie)
        {
            return NativeMethods.fAfectaSerie(aIdMovto, aNumeroSerie);
        }

        public int fAgregarRelacionCFDI(string aCodConcepto, string aSerie, string aFolio, string aTipoRelacion, string aConceptoRelacionar,
            string aSerieRelacionar, string aFolioRelacionar)
        {
            return NativeMethods.fAgregarRelacionCFDI(aCodConcepto, aSerie, aFolio, aTipoRelacion, aConceptoRelacionar, aSerieRelacionar,
                aFolioRelacionar);
        }

        public int fAgregarRelacionCFDI2(string aCodConcepto, string aSerie, string aFolio, string aTipoRelacion, string aUUID)
        {
            return NativeMethods.fAgregarRelacionCFDI2(aCodConcepto, aSerie, aFolio, aTipoRelacion, aUUID);
        }

        public int fAltaCteProv(ref int aIdCteProv, ref tCteProv astCteProv)
        {
            return NativeMethods.fAltaCteProv(ref aIdCteProv, ref astCteProv);
        }

        public int fAltaCuentaBancariaCliente(ref int aIdCtaBancaria, string aCuentaBancaria, string aNombreCuenta, string aNombreMoneda,
            string aClaveBanco, string aClabe, string aRfcBanco, string aNombreBancoExtranjero, string aCodigoCliente)
        {
            return NativeMethods.fAltaCuentaBancariaCliente(ref aIdCtaBancaria, aCuentaBancaria, aNombreCuenta, aNombreMoneda, aClaveBanco,
                aClabe, aRfcBanco, aNombreBancoExtranjero, aCodigoCliente);
        }

        public int fAltaCuentaBancariaEmpresa(ref int aIdCtaBancaria, string aCuentaBancaria, string aNombreCuenta, string aNombreMoneda,
            string aClaveBanco, string aClabe, string aRfcBanco, string aNombreBancoExtranjero)
        {
            return NativeMethods.fAltaCuentaBancariaEmpresa(ref aIdCtaBancaria, aCuentaBancaria, aNombreCuenta, aNombreMoneda, aClaveBanco,
                aClabe, aRfcBanco, aNombreBancoExtranjero);
        }

        public int fAltaDireccion(ref int aIdDireccion, ref tDireccion astDireccion)
        {
            return NativeMethods.fAltaDireccion(ref aIdDireccion, ref astDireccion);
        }

        public int fAltaDoctoAjusteIESPSCteProv(string aCodigoClienteProveedor, int aEsCliente, string aFechaDocto, int aIdMoneda,
            double aTipoCambio, double aImporteIVA, double aTasaIVA, double aImporteIESPS, double aTasaIESPS, int aIdFacturaBase,
            string aMetodo, string aLugar, ref int aIdDoctoGenerado)
        {
            return NativeMethods.fAltaDoctoAjusteIESPSCteProv(aCodigoClienteProveedor, aEsCliente, aFechaDocto, aIdMoneda, aTipoCambio,
                aImporteIVA, aTasaIVA, aImporteIESPS, aTasaIESPS, aIdFacturaBase, aMetodo, aLugar, ref aIdDoctoGenerado);
        }

        public int fAltaDoctoAjusteIVAClienteProveedor(string aCodigoClienteProveedor, int aEsCliente, int aAbsorberAjusteIVA,
            string aFechaDocto, int aIdMoneda, double aTipoCambio, double aImporteIVA, double aTasaIVA, int aIdFacturaBase, string aMetodo,
            string aLugar, ref int aIdDoctoGenerado)
        {
            return NativeMethods.fAltaDoctoAjusteIVAClienteProveedor(aCodigoClienteProveedor, aEsCliente, aAbsorberAjusteIVA, aFechaDocto,
                aIdMoneda, aTipoCambio, aImporteIVA, aTasaIVA, aIdFacturaBase, aMetodo, aLugar, ref aIdDoctoGenerado);
        }

        public int fAltaDocumento(ref int aIdDocumento, ref tDocumento aDocumento)
        {
            return NativeMethods.fAltaDocumento(ref aIdDocumento, ref aDocumento);
        }

        public int fAltaDocumentoCargoAbono(ref tDocumento aDocumento)
        {
            return NativeMethods.fAltaDocumentoCargoAbono(ref aDocumento);
        }

        public int fAltaDocumentoCargoAbonoExtras(ref tDocumento aDocumento, string aTextoExtra1, string aTextoExtra2, string aTextoExtra3,
            string aFechaExtra, double aImporteExtra1, double aImporteExtra2, double aImporteExtra3, double aImporteExtra4)
        {
            return NativeMethods.fAltaDocumentoCargoAbonoExtras(ref aDocumento, aTextoExtra1, aTextoExtra2, aTextoExtra3, aFechaExtra,
                aImporteExtra1, aImporteExtra2, aImporteExtra3, aImporteExtra4);
        }

        public int fAltaMovimiento(int aIdDocumento, ref int aIdMovimiento, ref tMovimiento astMovimiento)
        {
            return NativeMethods.fAltaMovimiento(aIdDocumento, ref aIdMovimiento, ref astMovimiento);
        }

        public int fAltaMovimientoCaracteristicas(int aIdMovimiento, ref int aIdMovtoCaracteristicas, ref tCaracteristicas aCaracteristicas)
        {
            return NativeMethods.fAltaMovimientoCaracteristicas(aIdMovimiento, ref aIdMovtoCaracteristicas, ref aCaracteristicas);
        }

        public int fAltaMovimientoCaracteristicas_Param(string aIdMovimiento, string aIdMovtoCaracteristicas, string aUnidades,
            string aValorCaracteristica1, string aValorCaracteristica2, string aValorCaracteristica3)
        {
            return NativeMethods.fAltaMovimientoCaracteristicas_Param(aIdMovimiento, aIdMovtoCaracteristicas, aUnidades, aValorCaracteristica1,
                aValorCaracteristica2, aValorCaracteristica3);
        }

        public int fAltaMovimientoCDesct(int aIdDocumento, ref int aIdMovimiento, ref tMovimientoDesc astMovimiento)
        {
            return NativeMethods.fAltaMovimientoCDesct(aIdDocumento, ref aIdMovimiento, ref astMovimiento);
        }

        public int fAltaMovimientoEx(ref int aIdMovimiento, ref tTipoProducto aTipoProducto)
        {
            return NativeMethods.fAltaMovimientoEx(ref aIdMovimiento, ref aTipoProducto);
        }

        public int fAltaMovimientoSeriesCapas(int aIdMovimiento, ref tSeriesCapas aSeriesCapas)
        {
            return NativeMethods.fAltaMovimientoSeriesCapas(aIdMovimiento, ref aSeriesCapas);
        }

        public int fAltaMovimientoSeriesCapas_Param(string aIdMovimiento, string aUnidades, string aTipoCambio, string aSeries,
            string aPedimento, string aAgencia, string aFechaPedimento, string aNumeroLote, string aFechaFabricacion, string aFechaCaducidad)
        {
            return NativeMethods.fAltaMovimientoSeriesCapas_Param(aIdMovimiento, aUnidades, aTipoCambio, aSeries, aPedimento, aAgencia,
                aFechaPedimento, aNumeroLote, aFechaFabricacion, aFechaCaducidad);
        }

        public int fAltaMovtoCaracteristicasUnidades(int aIdMovimiento, ref int aIdMovtoCaracteristicas,
            ref tCaracteristicas aCaracteristicasUnidades)
        {
            return NativeMethods.fAltaMovtoCaracteristicasUnidades(aIdMovimiento, ref aIdMovtoCaracteristicas, ref aCaracteristicasUnidades);
        }

        public int fAltaMovtoCaracteristicasUnidades_Param(string aIdMovimiento, string aIdMovtoCaracteristicas, string aUnidad,
            string aUnidades, string aUnidadesNC, string aValorCaracteristica1, string aValorCaracteristica2, string aValorCaracteristica3)
        {
            return NativeMethods.fAltaMovtoCaracteristicasUnidades_Param(aIdMovimiento, aIdMovtoCaracteristicas, aUnidad, aUnidades, aUnidadesNC,
                aValorCaracteristica1, aValorCaracteristica2, aValorCaracteristica3);
        }

        public int fAltaProducto(ref int aIdProducto, ref tProducto astProducto)
        {
            return NativeMethods.fAltaProducto(ref aIdProducto, ref astProducto);
        }

        public int fAltaUnidad(ref int aIdUnidad, ref tUnidad astUnidad)
        {
            return NativeMethods.fAltaUnidad(ref aIdUnidad, ref astUnidad);
        }

        public int fAltaValorClasif(ref int aIdValorClasif, ref tValorClasificacion astValorClasif)
        {
            return NativeMethods.fAltaValorClasif(ref aIdValorClasif, ref astValorClasif);
        }

        public int fBorraCteProv()
        {
            return NativeMethods.fBorraCteProv();
        }

        public int fBorraCuentaBancariaCliente(string aCuentaBancaria, string aCodigoCliente)
        {
            return NativeMethods.fBorraCuentaBancariaCliente(aCuentaBancaria, aCodigoCliente);
        }

        public int fBorraCuentaBancariaEmpresa(string aCuentaBancaria)
        {
            return NativeMethods.fBorraCuentaBancariaEmpresa(aCuentaBancaria);
        }

        public int fBorraDocumento()
        {
            return NativeMethods.fBorraDocumento();
        }

        public int fBorraDocumento_CW()
        {
            return NativeMethods.fBorraDocumento_CW();
        }

        public int fBorraMovimiento(int aIdDocumento, int aIdMovimiento)
        {
            return NativeMethods.fBorraMovimiento(aIdDocumento, aIdMovimiento);
        }

        public int fBorraProducto()
        {
            return NativeMethods.fBorraProducto();
        }

        public int fBorrarAsociacion(tLlaveDoc aDoctoaPagar, tLlaveDoc aDoctoPago)
        {
            return NativeMethods.fBorrarAsociacion(aDoctoaPagar, aDoctoPago);
        }

        public int fBorrarAsociacion_Param(string aCodConcepto_Pagar, string aSerie_Pagar, double aFolio_Pagar, string aCodConcepto_Pago,
            string aSerie_Pago, double aFolio_Pago)
        {
            return NativeMethods.fBorrarAsociacion_Param(aCodConcepto_Pagar, aSerie_Pagar, aFolio_Pagar, aCodConcepto_Pago, aSerie_Pago,
                aFolio_Pago);
        }

        public int fBorraUnidad()
        {
            return NativeMethods.fBorraUnidad();
        }

        public int fBorraValorClasif()
        {
            return NativeMethods.fBorraValorClasif();
        }

        public int fBuscaAgente(string aCodigoAgente)
        {
            return NativeMethods.fBuscaAgente(aCodigoAgente);
        }

        public int fBuscaAlmacen(string aCodigoAlmacen)
        {
            return NativeMethods.fBuscaAlmacen(aCodigoAlmacen);
        }

        public int fBuscaClasificacion(int aClasificacionDe, int aNumClasificacion)
        {
            return NativeMethods.fBuscaClasificacion(aClasificacionDe, aNumClasificacion);
        }

        public int fBuscaConceptoDocto(string aCodConcepto)
        {
            return NativeMethods.fBuscaConceptoDocto(aCodConcepto);
        }

        public int fBuscaCteProv(string aCodCteProv)
        {
            return NativeMethods.fBuscaCteProv(aCodCteProv);
        }

        public int fBuscaDireccionCteProv(string aCodCteProv, byte aTipoDireccion)
        {
            return NativeMethods.fBuscaDireccionCteProv(aCodCteProv, aTipoDireccion);
        }

        public int fBuscaDireccionDocumento(int aIdDocumento, byte aTipoDireccion)
        {
            return NativeMethods.fBuscaDireccionDocumento(aIdDocumento, aTipoDireccion);
        }

        public int fBuscaDireccionEmpresa()
        {
            return NativeMethods.fBuscaDireccionEmpresa();
        }

        public int fBuscaDocumento(ref tLlaveDoc aLlaveDocto)
        {
            return NativeMethods.fBuscaDocumento(ref aLlaveDocto);
        }

        public int fBuscaIdAgente(int aIdAgente)
        {
            return NativeMethods.fBuscaIdAgente(aIdAgente);
        }

        public int fBuscaIdAlmacen(int aIdAlmacen)
        {
            return NativeMethods.fBuscaIdAlmacen(aIdAlmacen);
        }

        public int fBuscaIdClasificacion(int aIdClasificacion)
        {
            return NativeMethods.fBuscaIdClasificacion(aIdClasificacion);
        }

        public int fBuscaIdConceptoDocto(int aIdConcepto)
        {
            return NativeMethods.fBuscaIdConceptoDocto(aIdConcepto);
        }

        public int fBuscaIdCteProv(int aIdCteProv)
        {
            return NativeMethods.fBuscaIdCteProv(aIdCteProv);
        }

        public int fBuscaIdProducto(int aIdProducto)
        {
            return NativeMethods.fBuscaIdProducto(aIdProducto);
        }

        public int fBuscaIdUnidad(int aIdUnidad)
        {
            return NativeMethods.fBuscaIdUnidad(aIdUnidad);
        }

        public int fBuscaIdValorClasif(int aIdValorClasif)
        {
            return NativeMethods.fBuscaIdValorClasif(aIdValorClasif);
        }

        public int fBuscaProducto(string aCodProducto)
        {
            return NativeMethods.fBuscaProducto(aCodProducto);
        }

        public int fBuscarDocumento(string aCodConcepto, string aSerie, string aFolio)
        {
            return NativeMethods.fBuscarDocumento(aCodConcepto, aSerie, aFolio);
        }

        public int fBuscarIdDocumento(int aIdDocumento)
        {
            return NativeMethods.fBuscarIdDocumento(aIdDocumento);
        }

        public int fBuscarIdMovimiento(int aIdMovimiento)
        {
            return NativeMethods.fBuscarIdMovimiento(aIdMovimiento);
        }

        public int fBuscaUnidad(string aNombreUnidad)
        {
            return NativeMethods.fBuscaUnidad(aNombreUnidad);
        }

        public int fBuscaValorClasif(int aClasificacionDe, int aNumClasificacion, string aCodValorClasif)
        {
            return NativeMethods.fBuscaValorClasif(aClasificacionDe, aNumClasificacion, aCodValorClasif);
        }

        public int fCalculaMovtoSerieCapa(int aIdMovimiento)
        {
            throw new NotImplementedException("Esta funcion no funciona en CONTPAQi Comercial.");
        }

        public int fCancelaCambiosMovimiento()
        {
            return NativeMethods.fCancelaCambiosMovimiento();
        }

        public int fCancelaComplementoPagoUUID(string aUUID, string aIdDConcepto, string aPass)
        {
            throw new NotImplementedException("Esta funcion no funciona en CONTPAQi Comercial.");
        }

        public int fCancelaDoctoInfo(string aPass)
        {
            return NativeMethods.fCancelaDoctoInfo(aPass);
        }

        public int fCancelaDocumento()
        {
            return NativeMethods.fCancelaDocumento();
        }

        public int fCancelaDocumento_CW()
        {
            return NativeMethods.fCancelaDocumento_CW();
        }

        public int fCancelaDocumentoAdministrativamente()
        {
            return NativeMethods.fCancelaDocumentoAdministrativamente();
        }

        public int fCancelaDocumentoConMotivo(string aMotivoCancelacion, string aUUIDRemplaza)
        {
            return NativeMethods.fCancelaDocumentoConMotivo(aMotivoCancelacion, aUUIDRemplaza);
        }

        public int fCancelaFiltroDocumento()
        {
            return NativeMethods.fCancelaFiltroDocumento();
        }

        public int fCancelaFiltroMovimiento()
        {
            return NativeMethods.fCancelaFiltroMovimiento();
        }

        public int fCancelaNominaUUID(string aUUID, string aIdDConcepto, string aPass)
        {
            return NativeMethods.fCancelaNominaUUID(aUUID, aIdDConcepto, aPass);
        }

        public int fCancelarModificacionAgente()
        {
            return NativeMethods.fCancelarModificacionAgente();
        }

        public int fCancelarModificacionAlmacen()
        {
            return NativeMethods.fCancelarModificacionAlmacen();
        }

        public int fCancelarModificacionClasificacion()
        {
            return NativeMethods.fCancelarModificacionClasificacion();
        }

        public int fCancelarModificacionCteProv()
        {
            return NativeMethods.fCancelarModificacionCteProv();
        }

        public int fCancelarModificacionDireccion()
        {
            return NativeMethods.fCancelarModificacionDireccion();
        }

        public int fCancelarModificacionDocumento()
        {
            return NativeMethods.fCancelarModificacionDocumento();
        }

        public int fCancelarModificacionProducto()
        {
            return NativeMethods.fCancelarModificacionProducto();
        }

        public int fCancelarModificacionUnidad()
        {
            return NativeMethods.fCancelarModificacionUnidad();
        }

        public int fCancelarModificacionValorClasif()
        {
            return NativeMethods.fCancelarModificacionValorClasif();
        }

        public int fCancelaUUID(string aUUID, string aIdDConcepto, string aPass)
        {
            return NativeMethods.fCancelaUUID(aUUID, aIdDConcepto, aPass);
        }

        public int fCancelaUUID40(string aUUID, string aMotivoCancelacion, string aUUIDReemplaza, string RFCReceptor, double aTotal,
            string aIdDConcepto, string aPass, ref int aEstatusCancelacion)
        {
            throw new NotImplementedException("Esta funcion no funciona en CONTPAQi Comercial.");
        }

        public void fCierraEmpresa()
        {
            NativeMethods.fCierraEmpresa();
        }

        public int fCuentaBancariaEmpresaDoctos(string aCuentaBancaria)
        {
            return NativeMethods.fCuentaBancariaEmpresaDoctos(aCuentaBancaria);
        }

        public int fDesbloqueaDocumento()
        {
            return NativeMethods.fDesbloqueaDocumento();
        }

        public int fDocumentoBloqueado(ref int aImpreso)
        {
            return NativeMethods.fDocumentoBloqueado(ref aImpreso);
        }

        public int fDocumentoDevuelto(ref int aDevuelto)
        {
            return NativeMethods.fDocumentoDevuelto(ref aDevuelto);
        }

        public int fDocumentoImpreso(ref bool aImpreso)
        {
            return NativeMethods.fDocumentoImpreso(ref aImpreso);
        }

        public int fDocumentoUUID(StringBuilder aCodConcepto, StringBuilder aSerie, double aFolio, StringBuilder atPtrCFDIUUID)
        {
            return NativeMethods.fDocumentoUUID(aCodConcepto, aSerie, aFolio, atPtrCFDIUUID);
        }

        public int fEditaAgente()
        {
            return NativeMethods.fEditaAgente();
        }

        public int fEditaAlmacen()
        {
            return NativeMethods.fEditaAlmacen();
        }

        public int fEditaClasificacion()
        {
            return NativeMethods.fEditaClasificacion();
        }

        public int fEditaConceptoDocto()
        {
            return NativeMethods.fEditaConceptoDocto();
        }

        public int fEditaCteProv()
        {
            return NativeMethods.fEditaCteProv();
        }

        public int fEditaDireccion()
        {
            return NativeMethods.fEditaDireccion();
        }

        public int fEditaMovtoContable()
        {
            return NativeMethods.fEditaMovtoContable();
        }

        public int fEditaParametros()
        {
            throw new NotImplementedException("Esta funcion no funciona en CONTPAQi Comercial.");
        }

        public int fEditaProducto()
        {
            return NativeMethods.fEditaProducto();
        }

        public int fEditarDocumento()
        {
            return NativeMethods.fEditarDocumento();
        }

        public int fEditarDocumentoCheqpaq()
        {
            return NativeMethods.fEditarDocumentoCheqpaq();
        }

        public int fEditarMovimiento()
        {
            return NativeMethods.fEditarMovimiento();
        }

        public int fEditaUnidad()
        {
            return NativeMethods.fEditaUnidad();
        }

        public int fEditaValorClasif()
        {
            return NativeMethods.fEditaValorClasif();
        }

        public int fEliminarCteProv(string aCodigoCteProv)
        {
            return NativeMethods.fEliminarCteProv(aCodigoCteProv);
        }

        public int fEliminarProducto(string aCodigoProducto)
        {
            return NativeMethods.fEliminarProducto(aCodigoProducto);
        }

        public int fEliminarRelacionesCFDIs(string aCodConcepto, string aSerie, string aFolio)
        {
            return NativeMethods.fEliminarRelacionesCFDIs(aCodConcepto, aSerie, aFolio);
        }

        public int fEliminarUnidad(string aNombreUnidad)
        {
            return NativeMethods.fEliminarUnidad(aNombreUnidad);
        }

        public int fEliminarValorClasif(int aClasificacionDe, int aNumClasificacion, string aCodigoValorClasif)
        {
            return NativeMethods.fEliminarValorClasif(aClasificacionDe, aNumClasificacion, aCodigoValorClasif);
        }

        public int fEmitirDocumento(string aCodConcepto, string aSerie, double aFolio, string aPassword, string aArchivoAdicional)
        {
            return NativeMethods.fEmitirDocumento(aCodConcepto, aSerie, aFolio, aPassword, aArchivoAdicional);
        }

        public int fEntregEnDiscoXML(string aCodConcepto, string aSerie, double aFolio, int aFormato, string aFormatoAmig)
        {
            return NativeMethods.fEntregEnDiscoXML(aCodConcepto, aSerie, aFolio, aFormato, aFormatoAmig);
        }

        public void fError(int aNumError, StringBuilder aMensaje, int aLen)
        {
            NativeMethods.fError(aNumError, aMensaje, aLen);
        }

        public int fGetCantidadParcialidades(StringBuilder atPtrPassword, StringBuilder aCantidadParcialidades)
        {
            return NativeMethods.fGetCantidadParcialidades(atPtrPassword, aCantidadParcialidades);
        }

        public int fGetDatosCFDI(StringBuilder aSerieCertEmisor, StringBuilder aFolioFiscalUUID, StringBuilder aSerieCertSAT,
            StringBuilder aFechaHora, StringBuilder aSelloDigCFDI, StringBuilder aSelloSAAT, StringBuilder aCadOrigComplSAT,
            StringBuilder aRegimen, StringBuilder aLugarExpedicion, StringBuilder aMoneda, StringBuilder aFolioFiscalOrig,
            StringBuilder aSerieFolioFiscalOrig, StringBuilder aFechaFolioFiscalOrig, StringBuilder aMontoFolioFiscalOrig)
        {
            return NativeMethods.fGetDatosCFDI(aSerieCertEmisor, aFolioFiscalUUID, aSerieCertSAT, aFechaHora, aSelloDigCFDI, aSelloSAAT,
                aCadOrigComplSAT, aRegimen, aLugarExpedicion, aMoneda, aFolioFiscalOrig, aSerieFolioFiscalOrig, aFechaFolioFiscalOrig,
                aMontoFolioFiscalOrig);
        }

        public int fGetNumParcialidades(StringBuilder atPtrPassword, StringBuilder aNumParcialidades)
        {
            return NativeMethods.fGetNumParcialidades(atPtrPassword, aNumParcialidades);
        }

        public int fGetSelloDigitalYCadena(StringBuilder atPtrPassword, StringBuilder atPtrSelloDigital, StringBuilder atPtrCadenaOriginal)
        {
            return NativeMethods.fGetSelloDigitalYCadena(atPtrPassword, atPtrSelloDigital, atPtrCadenaOriginal);
        }

        public int fGetSerieCertificado(StringBuilder atPtrPassword, StringBuilder aSerieCertificado)
        {
            throw new NotImplementedException("Esta funcion no funciona en CONTPAQi Comercial.");
        }

        public int fGetTamSelloDigitalYCadena(StringBuilder atPtrPassword, ref int aEspSelloDig, ref int aEspCadOrig)
        {
            return NativeMethods.fGetTamSelloDigitalYCadena(atPtrPassword, ref aEspSelloDig, ref aEspCadOrig);
        }

        public int fGuardaAgente()
        {
            return NativeMethods.fGuardaAgente();
        }

        public int fGuardaAlmacen()
        {
            return NativeMethods.fGuardaAlmacen();
        }

        public int fGuardaClasificacion()
        {
            return NativeMethods.fGuardaClasificacion();
        }

        public int fGuardaConceptoDocto()
        {
            return NativeMethods.fGuardaConceptoDocto();
        }

        public int fGuardaCteProv()
        {
            return NativeMethods.fGuardaCteProv();
        }

        public int fGuardaDireccion()
        {
            return NativeMethods.fGuardaDireccion();
        }

        public int fGuardaDocumento()
        {
            return NativeMethods.fGuardaDocumento();
        }

        public int fGuardaMovimiento()
        {
            return NativeMethods.fGuardaMovimiento();
        }

        public int fGuardaMovtoContable()
        {
            return NativeMethods.fGuardaMovtoContable();
        }

        public int fGuardaParametros()
        {
            throw new NotImplementedException("Esta funcion no funciona en CONTPAQi Comercial.");
        }

        public int fGuardaProducto()
        {
            return NativeMethods.fGuardaProducto();
        }

        public int fGuardaUnidad()
        {
            return NativeMethods.fGuardaUnidad();
        }

        public int fGuardaValorClasif()
        {
            return NativeMethods.fGuardaValorClasif();
        }

        public int fInformacionCliente(StringBuilder aCodigo, ref int aPermiteCredito, ref double aLimiteCredito, ref int aLimiteDoctosVencidos,
            ref int aPermiteExcederCredito, StringBuilder aFecha, ref double aSaldo, ref double aSaldoPendiente, ref int aDoctosVencidos)
        {
            return NativeMethods.fInformacionCliente(aCodigo, ref aPermiteCredito, ref aLimiteCredito, ref aLimiteDoctosVencidos,
                ref aPermiteExcederCredito, aFecha, ref aSaldo, ref aSaldoPendiente, ref aDoctosVencidos);
        }

        public int fInicializaLicenseInfo(byte aSistema)
        {
            throw new NotImplementedException("Esta funcion no funciona en CONTPAQi Comercial.");
        }

        public int fInicializaSDK()
        {
            return NativeMethods.fInicializaSDK();
        }

        public void fInicioSesionSDK(string aUsuario, string aContrasenia)
        {
            NativeMethods.fInicioSesionSDK(aUsuario, aContrasenia);
        }

        public void fInicioSesionSDKCONTPAQi(string aUsuario, string aContrasenia)
        {
            NativeMethods.fInicioSesionSDKCONTPAQi(aUsuario, aContrasenia);
        }

        public int fInsertaAgente()
        {
            return NativeMethods.fInsertaAgente();
        }

        public int fInsertaAlmacen()
        {
            return NativeMethods.fInsertaAlmacen();
        }

        public int fInsertaCteProv()
        {
            return NativeMethods.fInsertaCteProv();
        }

        public int fInsertaDatoAddendaDocto(int aIdAddenda, int aIdCatalogo, int aNumCampo, string aDato)
        {
            return NativeMethods.fInsertaDatoAddendaDocto(aIdAddenda, aIdCatalogo, aNumCampo, aDato);
        }

        public int fInsertaDatoAddendaMovto(int aIdAddenda, int aIdCatalogo, int aNumCampo, string aDato)
        {
            return NativeMethods.fInsertaDatoAddendaMovto(aIdAddenda, aIdCatalogo, aNumCampo, aDato);
        }

        public int fInsertaDatoCompEducativo(int aIdServicio, int aNumCampo, string aDato)
        {
            return NativeMethods.fInsertaDatoCompEducativo(aIdServicio, aNumCampo, aDato);
        }

        public int fInsertaDireccion()
        {
            return NativeMethods.fInsertaDireccion();
        }

        public int fInsertaProducto()
        {
            return NativeMethods.fInsertaProducto();
        }

        public int fInsertarDocumento()
        {
            throw new NotImplementedException("Esta funcion no funciona en CONTPAQi Comercial.");
        }

        public int fInsertarMovimiento()
        {
            return NativeMethods.fInsertarMovimiento();
        }

        public int fInsertaUnidad()
        {
            return NativeMethods.fInsertaUnidad();
        }

        public int fInsertaValorClasif()
        {
            return NativeMethods.fInsertaValorClasif();
        }

        public int fLeeDatoAgente(string aCampo, StringBuilder aValor, int aLen)
        {
            return NativeMethods.fLeeDatoAgente(aCampo, aValor, aLen);
        }

        public string fLeeDatoAlmacen(string aCampo)
        {
            StringBuilder aValor = new StringBuilder();
            NativeMethods.fLeeDatoAlmacen(aCampo, aValor, 3000);
            return aValor.ToString();
        }

        public int fLeeDatoCFDI(StringBuilder aValor, int aDato)
        {
            throw new NotImplementedException("Esta funcion no funciona en CONTPAQi Comercial.");
        }

        public int fLeeDatoClasificacion(string aCampo, StringBuilder aValor, int aLen)
        {
            return NativeMethods.fLeeDatoClasificacion(aCampo, aValor, aLen);
        }

        public int fLeeDatoConceptoDocto(string aCampo, StringBuilder aValor, int aLen)
        {
            return NativeMethods.fLeeDatoConceptoDocto(aCampo, aValor, aLen);
        }

        public int fLeeDatoCteProv(string aCampo, StringBuilder aValor, int aLen)
        {
            return NativeMethods.fLeeDatoCteProv(aCampo, aValor, aLen);
        }

        public int fLeeDatoDireccion(string aCampo, StringBuilder aValor, int aLen)
        {
            return NativeMethods.fLeeDatoDireccion(aCampo, aValor, aLen);
        }

        public int fLeeDatoDocumento(string aCampo, StringBuilder aValor, int aLongitud)
        {
            return NativeMethods.fLeeDatoDocumento(aCampo, aValor, aLongitud);
        }

        public int fLeeDatoMovimiento(string aCampo, StringBuilder aValor, int aLen)
        {
            return NativeMethods.fLeeDatoMovimiento(aCampo, aValor, aLen);
        }

        public int fLeeDatoMovtoContable(string aCampo, StringBuilder aValor, int aLen)
        {
            return NativeMethods.fLeeDatoMovtoContable(aCampo, aValor, aLen);
        }

        public int fLeeDatoParametros(string aCampo, StringBuilder aValor, int aLen)
        {
            return NativeMethods.fLeeDatoParametros(aCampo, aValor, aLen);
        }

        public int fLeeDatoProducto(string aCampo, StringBuilder aValor, int aLen)
        {
            return NativeMethods.fLeeDatoProducto(aCampo, aValor, aLen);
        }

        public int fLeeDatoUnidad(string aCampo, StringBuilder aValor, int aLen)
        {
            return NativeMethods.fLeeDatoUnidad(aCampo, aValor, aLen);
        }

        public int fLeeDatoValorClasif(string aCampo, StringBuilder aValor, int aLen)
        {
            return NativeMethods.fLeeDatoValorClasif(aCampo, aValor, aLen);
        }

        public int fLlenaRegistroCteProv(ref tCteProv astCteProv, int aEsAlta)
        {
            return NativeMethods.fLlenaRegistroCteProv(ref astCteProv, aEsAlta);
        }

        public int fLlenaRegistroDireccion(ref tDireccion astDireccion, int aEsAlta)
        {
            return NativeMethods.fLlenaRegistroDireccion(ref astDireccion, aEsAlta);
        }

        public int fLlenaRegistroProducto(ref tProducto astProducto, int aEsAlta)
        {
            return NativeMethods.fLlenaRegistroProducto(ref astProducto, aEsAlta);
        }

        public int fLlenaRegistroUnidad(ref tUnidad astUnidad)
        {
            return NativeMethods.fLlenaRegistroUnidad(ref astUnidad);
        }

        public int fLlenaRegistroValorClasif(ref tValorClasificacion astValorClasif)
        {
            return NativeMethods.fLlenaRegistroValorClasif(ref astValorClasif);
        }

        public int fModificaCostoEntrada(string aIdMovimiento, string aCostoEntrada)
        {
            return NativeMethods.fModificaCostoEntrada(aIdMovimiento, aCostoEntrada);
        }

        public int fModificaCuentaBancariaCliente(string aCuentaBancaria, string aNombreCuenta, string aNombreMoneda, string aClaveBanco,
            string aRfcBanco, string aClabe, string aNombreExtranjero, string aCodigoCliente)
        {
            return NativeMethods.fModificaCuentaBancariaCliente(aCuentaBancaria, aNombreCuenta, aNombreMoneda, aClaveBanco, aRfcBanco, aClabe,
                aNombreExtranjero, aCodigoCliente);
        }

        public int fModificaCuentaBancariaEmpresa(string aCuentaBancaria, string aNombreCuenta, string aNombreMoneda, string aClaveBanco,
            string aRfcBanco, string aClabe, string aNombreExtranjero)
        {
            return NativeMethods.fModificaCuentaBancariaEmpresa(aCuentaBancaria, aNombreCuenta, aNombreMoneda, aClaveBanco, aRfcBanco, aClabe,
                aNombreExtranjero);
        }

        public int fObtenCeryKey(int aIdFirmarl, string aRutaKey, string aRutaCer)
        {
            return NativeMethods.fObtenCeryKey(aIdFirmarl, aRutaKey, aRutaCer);
        }

        public int fObtieneDatosCFDI(string atPtrPassword)
        {
            throw new NotImplementedException("Esta funcion no funciona en CONTPAQi Comercial.");
        }

        public int fObtieneLicencia(StringBuilder aCodAcvtiva, StringBuilder aCodSitio, StringBuilder aSerie, StringBuilder aTagVersion)
        {
            return NativeMethods.fObtieneLicencia(aCodAcvtiva, aCodSitio, aSerie, aTagVersion);
        }

        public int fObtienePassProxy(StringBuilder aPassProxy)
        {
            return NativeMethods.fObtienePassProxy(aPassProxy);
        }

        public int fObtieneUnidadesPendientes(string aConceptoDocto, string aCodigoProducto, string aCodigoAlmacen, StringBuilder aUnidades)
        {
            return NativeMethods.fObtieneUnidadesPendientes(aConceptoDocto, aCodigoProducto, aCodigoAlmacen, aUnidades);
        }

        public int fObtieneUnidadesPendientesCarac(string aConceptoDocto, string aCodigoProducto, string aCodigoAlmacen,
            string aValorCaracteristica1, string aValorCaracteristica2, string aValorCaracteristica3, StringBuilder aUnidades)
        {
            return NativeMethods.fObtieneUnidadesPendientesCarac(aConceptoDocto, aCodigoProducto, aCodigoAlmacen, aValorCaracteristica1,
                aValorCaracteristica2, aValorCaracteristica3, aUnidades);
        }

        public int fPosAnteriorAgente()
        {
            return NativeMethods.fPosAnteriorAgente();
        }

        public int fPosAnteriorAlmacen()
        {
            return NativeMethods.fPosAnteriorAlmacen();
        }

        public int fPosAnteriorClasificacion()
        {
            return NativeMethods.fPosAnteriorClasificacion();
        }

        public int fPosAnteriorConceptoDocto()
        {
            return NativeMethods.fPosAnteriorConceptoDocto();
        }

        public int fPosAnteriorCteProv()
        {
            return NativeMethods.fPosAnteriorCteProv();
        }

        public int fPosAnteriorDireccion()
        {
            return NativeMethods.fPosAnteriorDireccion();
        }

        public int fPosAnteriorDocumento()
        {
            return NativeMethods.fPosAnteriorDocumento();
        }

        public int fPosAnteriorMovimiento()
        {
            return NativeMethods.fPosAnteriorMovimiento();
        }

        public int fPosAnteriorProducto()
        {
            return NativeMethods.fPosAnteriorProducto();
        }

        public int fPosAnteriorUnidad()
        {
            return NativeMethods.fPosAnteriorUnidad();
        }

        public int fPosAnteriorValorClasif()
        {
            return NativeMethods.fPosAnteriorValorClasif();
        }

        public int fPosBOF()
        {
            return NativeMethods.fPosBOF();
        }

        public int fPosBOFAgente()
        {
            return NativeMethods.fPosBOFAgente();
        }

        public int fPosBOFAlmacen()
        {
            return NativeMethods.fPosBOFAlmacen();
        }

        public int fPosBOFClasificacion()
        {
            return NativeMethods.fPosBOFClasificacion();
        }

        public int fPosBOFConceptoDocto()
        {
            return NativeMethods.fPosBOFConceptoDocto();
        }

        public int fPosBOFCteProv()
        {
            return NativeMethods.fPosBOFCteProv();
        }

        public int fPosBOFDireccion()
        {
            return NativeMethods.fPosBOFDireccion();
        }

        public int fPosBOFProducto()
        {
            return NativeMethods.fPosBOFProducto();
        }

        public int fPosBOFUnidad()
        {
            return NativeMethods.fPosBOFUnidad();
        }

        public int fPosBOFValorClasif()
        {
            return NativeMethods.fPosBOFValorClasif();
        }

        public int fPosEOF()
        {
            return NativeMethods.fPosEOF();
        }

        public int fPosEOFAgente()
        {
            return NativeMethods.fPosEOFAgente();
        }

        public int fPosEOFAlmacen()
        {
            return NativeMethods.fPosEOFAlmacen();
        }

        public int fPosEOFClasificacion()
        {
            return NativeMethods.fPosEOFClasificacion();
        }

        public int fPosEOFConceptoDocto()
        {
            return NativeMethods.fPosEOFConceptoDocto();
        }

        public int fPosEOFCteProv()
        {
            return NativeMethods.fPosEOFCteProv();
        }

        public int fPosEOFDireccion()
        {
            return NativeMethods.fPosEOFDireccion();
        }

        public int fPosEOFMovtoContable()
        {
            return NativeMethods.fPosEOFMovtoContable();
        }

        public int fPosEOFProducto()
        {
            return NativeMethods.fPosEOFProducto();
        }

        public int fPosEOFUnidad()
        {
            return NativeMethods.fPosEOFUnidad();
        }

        public int fPosEOFValorClasif()
        {
            return NativeMethods.fPosEOFValorClasif();
        }

        public int fPosMovimientoBOF()
        {
            return NativeMethods.fPosMovimientoBOF();
        }

        public int fPosMovimientoEOF()
        {
            return NativeMethods.fPosMovimientoEOF();
        }

        public int fPosPrimerAgente()
        {
            return NativeMethods.fPosPrimerAgente();
        }

        public int fPosPrimerAlmacen()
        {
            return NativeMethods.fPosPrimerAlmacen();
        }

        public int fPosPrimerClasificacion()
        {
            return NativeMethods.fPosPrimerClasificacion();
        }

        public int fPosPrimerConceptoDocto()
        {
            return NativeMethods.fPosPrimerConceptoDocto();
        }

        public int fPosPrimerCteProv()
        {
            return NativeMethods.fPosPrimerCteProv();
        }

        public int fPosPrimerDireccion()
        {
            return NativeMethods.fPosPrimerDireccion();
        }

        public int fPosPrimerDocumento()
        {
            return NativeMethods.fPosPrimerDocumento();
        }

        public int fPosPrimerEmpresa(ref int aIdEmpresa, StringBuilder aNombreEmpresa, StringBuilder aDirectorioEmpresa)
        {
            return NativeMethods.fPosPrimerEmpresa(ref aIdEmpresa, aNombreEmpresa, aDirectorioEmpresa);
        }

        public int fPosPrimerMovimiento()
        {
            return NativeMethods.fPosPrimerMovimiento();
        }

        public int fPosPrimerMovtoContable()
        {
            return NativeMethods.fPosPrimerMovtoContable();
        }

        public int fPosPrimerProducto()
        {
            return NativeMethods.fPosPrimerProducto();
        }

        public int fPosPrimerUnidad()
        {
            return NativeMethods.fPosPrimerUnidad();
        }

        public int fPosPrimerValorClasif()
        {
            return NativeMethods.fPosPrimerValorClasif();
        }

        public int fPosSiguienteAgente()
        {
            return NativeMethods.fPosSiguienteAgente();
        }

        public int fPosSiguienteAlmacen()
        {
            return NativeMethods.fPosSiguienteAlmacen();
        }

        public int fPosSiguienteClasificacion()
        {
            return NativeMethods.fPosSiguienteClasificacion();
        }

        public int fPosSiguienteConceptoDocto()
        {
            return NativeMethods.fPosSiguienteConceptoDocto();
        }

        public int fPosSiguienteCteProv()
        {
            return NativeMethods.fPosSiguienteCteProv();
        }

        public int fPosSiguienteDireccion()
        {
            return NativeMethods.fPosSiguienteDireccion();
        }

        public int fPosSiguienteDocumento()
        {
            return NativeMethods.fPosSiguienteDocumento();
        }

        public int fPosSiguienteEmpresa(ref int aIdEmpresa, StringBuilder aNombreEmpresa, StringBuilder aDirectorioEmpresa)
        {
            return NativeMethods.fPosSiguienteEmpresa(ref aIdEmpresa, aNombreEmpresa, aDirectorioEmpresa);
        }

        public int fPosSiguienteMovimiento()
        {
            return NativeMethods.fPosSiguienteMovimiento();
        }

        public int fPosSiguienteMovtoContable()
        {
            return NativeMethods.fPosSiguienteMovtoContable();
        }

        public int fPosSiguienteProducto()
        {
            return NativeMethods.fPosSiguienteProducto();
        }

        public int fPosSiguienteUnidad()
        {
            return NativeMethods.fPosSiguienteUnidad();
        }

        public int fPosSiguienteValorClasif()
        {
            return NativeMethods.fPosSiguienteValorClasif();
        }

        public int fPosUltimaConceptoDocto()
        {
            return NativeMethods.fPosUltimaConceptoDocto();
        }

        public int fPosUltimaDireccion()
        {
            return NativeMethods.fPosUltimaDireccion();
        }

        public int fPosUltimoAgente()
        {
            return NativeMethods.fPosUltimoAgente();
        }

        public int fPosUltimoAlmacen()
        {
            return NativeMethods.fPosUltimoAlmacen();
        }

        public int fPosUltimoClasificacion()
        {
            return NativeMethods.fPosUltimoClasificacion();
        }

        public int fPosUltimoCteProv()
        {
            return NativeMethods.fPosUltimoCteProv();
        }

        public int fPosUltimoDocumento()
        {
            return NativeMethods.fPosUltimoDocumento();
        }

        public int fPosUltimoMovimiento()
        {
            return NativeMethods.fPosUltimoMovimiento();
        }

        public int fPosUltimoProducto()
        {
            return NativeMethods.fPosUltimoProducto();
        }

        public int fPosUltimoUnidad()
        {
            return NativeMethods.fPosUltimoUnidad();
        }

        public int fPosUltimoValorClasif()
        {
            return NativeMethods.fPosUltimoValorClasif();
        }

        public int fProyectoEmpresaDoctos(string aCodigoProyecto)
        {
            return NativeMethods.fProyectoEmpresaDoctos(aCodigoProyecto);
        }

        public int fRecosteoProducto(string aCodigoProducto, int aEjercicio, int aPeriodo, string aCodigoClasificacion1,
            string aCodigoClasificacion2, string aCodigoClasificacion3, string aCodigoClasificacion4, string aCodigoClasificacion5,
            string aCodigoClasificacion6, string aNombreBitacora, int aSobreEscribirBitacora, int aEsCalculoArimetico)
        {
            return NativeMethods.fRecosteoProducto(aCodigoProducto, aEjercicio, aPeriodo, aCodigoClasificacion1, aCodigoClasificacion2,
                aCodigoClasificacion3, aCodigoClasificacion4, aCodigoClasificacion5, aCodigoClasificacion6, aNombreBitacora,
                aSobreEscribirBitacora, aEsCalculoArimetico);
        }

        public int fRecuperarRelacionesCFDIs(string aCodConcepto, string aSerie, string aFolio, string aTipoRelacion, StringBuilder aUUIDs,
            string aRutaNombreArchivoInfo)
        {
            return NativeMethods.fRecuperarRelacionesCFDIs(aCodConcepto, aSerie, aFolio, aTipoRelacion, aUUIDs, aRutaNombreArchivoInfo);
        }

        public int fRecuperaTipoProducto(ref bool aUnidades, ref bool aSerie, ref bool aLote, ref bool aPedimento, ref bool aCaracteristicas)
        {
            return NativeMethods.fRecuperaTipoProducto(ref aUnidades, ref aSerie, ref aLote, ref aPedimento, ref aCaracteristicas);
        }

        public int fRegresaCostoCapa(string aCodigoProducto, string aCodigoAlmacen, double aUnidades, StringBuilder aImporteCosto)
        {
            return NativeMethods.fRegresaCostoCapa(aCodigoProducto, aCodigoAlmacen, aUnidades, aImporteCosto);
        }

        public int fRegresaCostoEstandar(string aCodigoProducto, StringBuilder aCostoEstandar)
        {
            return NativeMethods.fRegresaCostoEstandar(aCodigoProducto, aCostoEstandar);
        }

        public int fRegresaCostoPromedio(string aCodigoProducto, string aCodigoAlmacen, string aAnio, string aMes, string aDia,
            StringBuilder aCostoPromedio)
        {
            return NativeMethods.fRegresaCostoPromedio(aCodigoProducto, aCodigoAlmacen, aAnio, aMes, aDia, aCostoPromedio);
        }

        public int fRegresaExistencia(string aCodigoProducto, string aCodigoAlmacen, string aAnio, string aMes, string aDia,
            ref double aExistencia)
        {
            return NativeMethods.fRegresaExistencia(aCodigoProducto, aCodigoAlmacen, aAnio, aMes, aDia, ref aExistencia);
        }

        public int fRegresaExistenciaCaracteristicas(string aCodigoProducto, string aCodigoAlmacen, string aAnio, string aMes, string aDia,
            string abreviaturaValorCaracteristica1, string abreviaturaValorCaracteristica2, string abreviaturaValorCaracteristica3, ref double aExistencia)
        {
            return NativeMethods.fRegresaExistenciaCaracteristicas(aCodigoProducto, aCodigoAlmacen, aAnio, aMes, aDia, abreviaturaValorCaracteristica1,
                abreviaturaValorCaracteristica2, abreviaturaValorCaracteristica3, ref aExistencia);
        }

        public int fRegresaExistenciaLotePedimento(string aCodigoProducto, string aCodigoAlmacen, string aPedimento, string aLote,
            ref double aExistencia)
        {
            return NativeMethods.fRegresaExistenciaLotePedimento(aCodigoProducto, aCodigoAlmacen, aPedimento, aLote, ref aExistencia);
        }

        public int fRegresaIVACargo(tLlaveDoc aLlaveDocto, double aNetoTasa15, double aNetoTasa10, double aNetoTasaCero,
            double aNetoTasaExcenta, double aNetoOtrasTasas, double aIVATasa15, double aIVATasa10, double aIVAOtrasTasas)
        {
            return NativeMethods.fRegresaIVACargo(aLlaveDocto, aNetoTasa15, aNetoTasa10, aNetoTasaCero, aNetoTasaExcenta, aNetoOtrasTasas,
                aIVATasa15, aIVATasa10, aIVAOtrasTasas);
        }

        public int fRegresaIVACargo_2010(tLlaveDoc aLlaveDocto, double aNetoTasa15, double aNetoTasa10, double aNetoTasa16, double aNetoTasa11,
            double aNetoTasaCero, double aNetoTasaExcenta, double aNetoOtrasTasas, double aIVATasa15, double aIVATasa10, double aIVATasa16,
            double aIVATasa11, double aIVAOtrasTasas)
        {
            return NativeMethods.fRegresaIVACargo_2010(aLlaveDocto, aNetoTasa15, aNetoTasa10, aNetoTasa16, aNetoTasa11, aNetoTasaCero,
                aNetoTasaExcenta, aNetoOtrasTasas, aIVATasa15, aIVATasa10, aIVATasa16, aIVATasa11, aIVAOtrasTasas);
        }

        public int fRegresaIVACargoRet_2010(tLlaveDoc aLlaveDocto, double aNetoTasa15, double aNetoTasa10, double aNetoTasa16,
            double aNetoTasa11, double aNetoTasaCero, double aNetoTasaExcenta, double aNetoOtrasTasas, double aIVATasa15, double aIVATasa10,
            double aIVATasa16, double aIVATasa11, double aIVAOtrasTasas, double aRetIVA, double aRetI)
        {
            return NativeMethods.fRegresaIVACargoRet_2010(aLlaveDocto, aNetoTasa15, aNetoTasa10, aNetoTasa16, aNetoTasa11, aNetoTasaCero,
                aNetoTasaExcenta, aNetoOtrasTasas, aIVATasa15, aIVATasa10, aIVATasa16, aIVATasa11, aIVAOtrasTasas, aRetIVA, aRetI);
        }

        public int fRegresaIVAPago(tLlaveDoc aLlaveDocto, double aNetoTasa15, double aNetoTasa10, double aNetoTasaCero, double aNetoTasaExcenta,
            double aNetoOtrasTasas, double aIVATasa15, double aIVATasa10, double aIVAOtrasTasas)
        {
            return NativeMethods.fRegresaIVAPago(aLlaveDocto, aNetoTasa15, aNetoTasa10, aNetoTasaCero, aNetoTasaExcenta, aNetoOtrasTasas,
                aIVATasa15, aIVATasa10, aIVAOtrasTasas);
        }

        public int fRegresaIVAPago_2010(tLlaveDoc aLlaveDocto, double aNetoTasa15, double aNetoTasa10, double aNetoTasa16, double aNetoTasa11,
            double aNetoTasaCero, double aNetoTasaExcenta, double aNetoOtrasTasas, double aIVATasa15, double aIVATasa10, double aIVATasa16,
            double aIVATasa11, double aIVAOtrasTasas)
        {
            return NativeMethods.fRegresaIVAPago_2010(aLlaveDocto, aNetoTasa15, aNetoTasa10, aNetoTasa16, aNetoTasa11, aNetoTasaCero,
                aNetoTasaExcenta, aNetoOtrasTasas, aIVATasa15, aIVATasa10, aIVATasa16, aIVATasa11, aIVAOtrasTasas);
        }

        public int fRegresaIVAPagoRet_2010(tLlaveDoc aLlaveDocto, double aNetoTasa15, double aNetoTasa10, double aNetoTasa16,
            double aNetoTasa11, double aNetoTasaCero, double aNetoTasaExcenta, double aNetoOtrasTasas, double aIVATasa15, double aIVATasa10,
            double aIVATasa16, double aIVATasa11, double aIVAOtrasTasas, double aRetIVA, double aRetI)
        {
            return NativeMethods.fRegresaIVAPagoRet_2010(aLlaveDocto, aNetoTasa15, aNetoTasa10, aNetoTasa16, aNetoTasa11, aNetoTasaCero,
                aNetoTasaExcenta, aNetoOtrasTasas, aIVATasa15, aIVATasa10, aIVATasa16, aIVATasa11, aIVAOtrasTasas, aRetIVA, aRetI);
        }

        public int fRegresaPrecioVenta(string aCodigoConcepto, string aCodigoCliente, string aCodigoProducto, StringBuilder aPrecioVenta)
        {
            return NativeMethods.fRegresaPrecioVenta(aCodigoConcepto, aCodigoCliente, aCodigoProducto, aPrecioVenta);
        }

        public int fRegresaUltimoCosto(string aCodigoProducto, string aCodigoAlmacen, string aAnio, string aMes, string aDia,
            StringBuilder aUltimoCosto)
        {
            return NativeMethods.fRegresaUltimoCosto(aCodigoProducto, aCodigoAlmacen, aAnio, aMes, aDia, aUltimoCosto);
        }

        public int fRegresPorcentajeImpuesto(int aIdConceptoDocumento, int aIdClienteProveedor, int aIdProducto, ref double aPorcentajeImpuesto)
        {
            return NativeMethods.fRegresPorcentajeImpuesto(aIdConceptoDocumento, aIdClienteProveedor, aIdProducto, ref aPorcentajeImpuesto);
        }

        public int fSaldarDocumento(ref tLlaveDoc aDoctoaPagar, ref tLlaveDoc aDoctoPago, double aImporte, int aIdMoneda, string aFecha)
        {
            return NativeMethods.fSaldarDocumento(ref aDoctoaPagar, ref aDoctoPago, aImporte, aIdMoneda, aFecha);
        }

        public int fSaldarDocumento_Param(string aCodConcepto_Pagar, string aSerie_Pagar, double aFolio_Pagar, string aCodConcepto_Pago,
            string aSerie_Pago, double aFolio_Pago, double aImporte, int aIdMoneda, string aFecha)
        {
            return NativeMethods.fSaldarDocumento_Param(aCodConcepto_Pagar, aSerie_Pagar, aFolio_Pagar, aCodConcepto_Pago, aSerie_Pago,
                aFolio_Pago, aImporte, aIdMoneda, aFecha);
        }

        public int fSaldarDocumentoCheqPAQ(tLlaveDoc aDoctoaPagar, tLlaveDoc aDoctoPago, double aImporte, int aIdMoneda, string aFecha,
            double aTipoCambioCheqPAQ)
        {
            return NativeMethods.fSaldarDocumentoCheqPAQ(aDoctoaPagar, aDoctoPago, aImporte, aIdMoneda, aFecha, aTipoCambioCheqPAQ);
        }

        public int fSetDatoAgente(string aCampo, string aValor)
        {
            return NativeMethods.fSetDatoAgente(aCampo, aValor);
        }

        public int fSetDatoAlmacen(string aCampo, string aValor)
        {
            return NativeMethods.fSetDatoAlmacen(aCampo, aValor);
        }

        public int fSetDatoClasificacion(string aCampo, string aValor)
        {
            return NativeMethods.fSetDatoClasificacion(aCampo, aValor);
        }

        public int fSetDatoConceptoDocto(string aCampo, string aValor)
        {
            return NativeMethods.fSetDatoConceptoDocto(aCampo, aValor);
        }

        public int fSetDatoCteProv(string aCampo, string aValor)
        {
            return NativeMethods.fSetDatoCteProv(aCampo, aValor);
        }

        public int fSetDatoDireccion(string aCampo, string aValor)
        {
            return NativeMethods.fSetDatoDireccion(aCampo, aValor);
        }

        public int fSetDatoDocumento(string aCampo, string aValor)
        {
            return NativeMethods.fSetDatoDocumento(aCampo, aValor);
        }

        public int fSetDatoMovimiento(string aCampo, string aValor)
        {
            return NativeMethods.fSetDatoMovimiento(aCampo, aValor);
        }

        public int fSetDatoMovtoContable(string aCampo, string aValor)
        {
            return NativeMethods.fSetDatoMovtoContable(aCampo, aValor);
        }

        public int fSetDatoParametros(string aCampo, StringBuilder aValor)
        {
            throw new NotImplementedException("Esta funcion no funciona en CONTPAQi Comercial.");
        }

        public int fSetDatoProducto(string aCampo, string aValor)
        {
            return NativeMethods.fSetDatoProducto(aCampo, aValor);
        }

        public int fSetDatoUnidad(string aCampo, string aValor)
        {
            return NativeMethods.fSetDatoUnidad(aCampo, aValor);
        }

        public int fSetDatoValorClasif(string aCampo, string aValor)
        {
            return NativeMethods.fSetDatoValorClasif(aCampo, aValor);
        }

        public int fSetDescripcionProducto(string aCampo, string aValor)
        {
            return NativeMethods.fSetDescripcionProducto(aCampo, aValor);
        }

        public int fSetFiltroDocumento(string aFechaInicio, string aFechaFin, string aCodigoConcepto, string aCodigoCteProv)
        {
            return NativeMethods.fSetFiltroDocumento(aFechaInicio, aFechaFin, aCodigoConcepto, aCodigoCteProv);
        }

        public int fSetFiltroMovimiento(int aIdDocumento)
        {
            return NativeMethods.fSetFiltroMovimiento(aIdDocumento);
        }

        public void fSetModoImportacion(bool aImportacion)
        {
            NativeMethods.fSetModoImportacion(aImportacion);
        }

        public int fSetNombrePAQ(string aSistema)
        {
            int sdkError = NativeMethods.fSetNombrePAQ(aSistema);
            if (sdkError != 0) //Error detectado en el SDK de Comercial
            {
                StringBuilder strMensaje = new StringBuilder(512);
                NativeMethods.fError(sdkError, strMensaje, 512);
                Console_log("Error fSetNombrePAQ (" + aSistema + "): " + strMensaje, EventLogEntryType.Error, 2299);
            }
            return sdkError;
        }

        public int fSiguienteFolio(string aCodigoConcepto, StringBuilder aSerie, ref double aFolio)
        {
            return NativeMethods.fSiguienteFolio(aCodigoConcepto, aSerie, ref aFolio);
        }

        public void fTerminaSDK()
        {
            NativeMethods.fTerminaSDK();
        }

        public int fTimbraComplementoPagoXML(string aRutaXML, string aCodConcepto, StringBuilder aUUID, string aRutaDDA, string aRutaResultado,
            string aPass, string aRutaFormato)
        {
            throw new NotImplementedException("Esta funcion no funciona en CONTPAQi Comercial.");
        }

        public int fTimbraComplementoXML(string aRutaXML, string aCodCOncepto, StringBuilder aUUID, string aRutaDDA, string aRutaResultado,
            string aPass, string aRutaFormato, int aComplemento)
        {
            throw new NotImplementedException("Esta funcion no funciona en CONTPAQi Comercial.");
        }

        public int fTimbraNominaXML(string aRutaXML, string aCodCOncepto, StringBuilder aUUID, string aRutaDDA, string aRutaResultado,
            string aPass, string aRutaFormato)
        {
            throw new NotImplementedException("Esta funcion no funciona en CONTPAQi Comercial.");
        }

        public int fTimbraXML(string aRutaXML, string aCodCOncepto, StringBuilder aUUID, string aRutaDDA, string aRutaResultado, string aPass,
            string aRutaFormato)
        {
            throw new NotImplementedException("Esta funcion no funciona en CONTPAQi Comercial.");
        }

        public int InicializarSDK()
        {
            SetCurrentDirectory();
            return fSetNombrePAQ(NombrePaq);
        }

        public int InicializarSDK(string usuario, string password)
        {
            SetCurrentDirectory();
            fInicioSesionSDK(usuario, password);
            return fSetNombrePAQ(NombrePaq);
        }

        public int InicializarSDK(string usuario, string password, string usuarioContabilidad, string passwordContabilidad)
        {
            int sdkResult = InicializarSDK(usuario, password);
            if (sdkResult == SdkResultConstants.Success) fInicioSesionSDKCONTPAQi(usuarioContabilidad, passwordContabilidad);
            return sdkResult;
        }

        public string LeeDatoAgente(string dato, int longitud)
        {
            var valor = new StringBuilder(longitud);
            NativeMethods.fLeeDatoAgente(dato, valor, longitud);
            return valor.ToString();
        }

        public string LeeDatoAlmacen(string dato, int longitud)
        {
            var valor = new StringBuilder(longitud);
            NativeMethods.fLeeDatoAlmacen(dato, valor, longitud);
            return valor.ToString();
        }

        public string LeeDatoCfdi(int dato, int longitud)
        {
            var valor = new StringBuilder(longitud);
            fLeeDatoCFDI(valor, dato);
            return valor.ToString();
        }

        public string LeeDatoClasificacion(string dato, int longitud)
        {
            var valor = new StringBuilder(longitud);
            NativeMethods.fLeeDatoClasificacion(dato, valor, longitud);
            return valor.ToString();
        }

        public string LeeDatoClienteProveedor(string dato, int longitud)
        {
            var valor = new StringBuilder(longitud);
            NativeMethods.fLeeDatoCteProv(dato, valor, longitud);
            return valor.ToString();
        }

        public string LeeDatoConcepto(string dato, int longitud)
        {
            var valor = new StringBuilder(longitud);
            NativeMethods.fLeeDatoConceptoDocto(dato, valor, longitud);
            return valor.ToString();
        }

        public string LeeDatoDireccion(string dato, int longitud)
        {
            var valor = new StringBuilder(longitud);
            NativeMethods.fLeeDatoDireccion(dato, valor, longitud);
            return valor.ToString();
        }

        public string LeeDatoDocumento(string dato, int longitud)
        {
            var valor = new StringBuilder(longitud);
            NativeMethods.fLeeDatoDocumento(dato, valor, longitud);
            return valor.ToString();
        }

        public string LeeDatoMovimiento(string dato, int longitud)
        {
            var valor = new StringBuilder(longitud);
            NativeMethods.fLeeDatoMovimiento(dato, valor, longitud);
            return valor.ToString();
        }

        public string LeeDatoParametros(string dato, int longitud)
        {
            var valor = new StringBuilder(longitud);
            NativeMethods.fLeeDatoParametros(dato, valor, longitud);
            return valor.ToString();
        }

        public string LeeDatoProducto(string dato, int longitud)
        {
            var valor = new StringBuilder(longitud);
            NativeMethods.fLeeDatoProducto(dato, valor, longitud);
            return valor.ToString();
        }

        public string LeeDatoUnidad(string dato, int longitud)
        {
            var valor = new StringBuilder(longitud);
            NativeMethods.fLeeDatoUnidad(dato, valor, longitud);
            return valor.ToString();
        }

        public string LeeDatoValorClasificacion(string dato, int longitud)
        {
            var valor = new StringBuilder(longitud);
            NativeMethods.fLeeDatoValorClasif(dato, valor, longitud);
            return valor.ToString();
        }

        public string LeeMensajeError(int numeroError)
        {
            var mensajeError = new StringBuilder(512);
            fError(numeroError, mensajeError, 512);
            return mensajeError.ToString();
        }

        public string NombreLlaveRegistro => ComercialSdkConstants.NombreLlaveRegistro;

        public string NombrePaq =>  ComercialSdkConstants.NombrePaq;

        public void SetCurrentDirectory()
        {
            string lEntrada = RegistryHelper.GetDirectorioBaseFromRegistry(NombreLlaveRegistro);
            Directory.SetCurrentDirectory(lEntrada);
        }

    }

    /** ********************************************************************* **/
    /** CLASE DE METODOS Y DECLARACIONES NATIVAS  **/
    /** ********************************************************************* **/
    [ComVisible(true)]
    [Guid("f246924c-7489-4f42-ba4d-ba7f63c8cd46")]
    internal static class NativeMethods
    {
        [DllImport("MGWServicios.dll", EntryPoint = "fAbreEmpresa")]
        public static extern int fAbreEmpresa(string aDirectorioEmpresa);

        [DllImport("MGWServicios.dll", EntryPoint = "fActivarPrecioCompra")]
        public static extern int fActivarPrecioCompra(int aActivar);

        [DllImport("MGWServicios.dll", EntryPoint = "fActualizaClasificacion")]
        public static extern int fActualizaClasificacion(int aClasificacionDe, int aNumClasificacion, string aNombreClasificacion);

        [DllImport("MGWServicios.dll", EntryPoint = "fActualizaCteProv")]
        public static extern int fActualizaCteProv(string aCodigoCteProv, ref tCteProv astCteProv);

        [DllImport("MGWServicios.dll", EntryPoint = "fActualizaDireccion")]
        public static extern int fActualizaDireccion(ref tDireccion astDireccion);

        [DllImport("MGWServicios.dll", EntryPoint = "fActualizaProducto")]
        public static extern int fActualizaProducto(string aCodigoProducto, ref tProducto astProducto);

        [DllImport("MGWServicios.dll", EntryPoint = "fActualizaUnidad")]
        public static extern int fActualizaUnidad(string aNombreUnidad, ref tUnidad astUnidad);

        [DllImport("MGWServicios.dll", EntryPoint = "fActualizaValorClasif")]
        public static extern int fActualizaValorClasif(string aCodigoValorClasif, ref tValorClasificacion astValorClasif);

        [DllImport("MGWServicios.dll", EntryPoint = "fAfectaDocto")]
        public static extern int fAfectaDocto(ref tLlaveDoc aLlaveDocto, bool aAfecta);

        [DllImport("MGWServicios.dll", EntryPoint = "fAfectaDocto_Param")]
        public static extern int fAfectaDocto_Param(string aCodConcepto, string aSerie, double aFolio, bool aAfecta);

        [DllImport("MGWServicios.dll", EntryPoint = "fAfectaSerie")]
        public static extern int fAfectaSerie(int aIdMovto, string aNumeroSerie);

        [DllImport("MGWServicios.dll", EntryPoint = "fAgregarRelacionCFDI")]
        public static extern int fAgregarRelacionCFDI(string aCodConcepto, string aSerie, string aFolio, string aTipoRelacion,
            string aConceptoRelacionar, string aSerieRelacionar, string aFolioRelacionar);

        [DllImport("MGWServicios.dll", EntryPoint = "fAgregarRelacionCFDI2")]
        public static extern int fAgregarRelacionCFDI2(string aCodConcepto, string aSerie, string aFolio, string aTipoRelacion, string aUUID);

        [DllImport("MGWServicios.dll", EntryPoint = "fAltaCteProv")]
        public static extern int fAltaCteProv(ref int aIdCteProv, ref tCteProv astCteProv);

        [DllImport("MGWServicios.dll", EntryPoint = "fAltaCuentaBancariaCliente")]
        public static extern int fAltaCuentaBancariaCliente(ref int aIdCtaBancaria, string aCuentaBancaria, string aNombreCuenta,
            string aNombreMoneda, string aClaveBanco, string aClabe, string aRfcBanco, string aNombreBancoExtranjero, string aCodigoCliente);

        [DllImport("MGWServicios.dll", EntryPoint = "fAltaCuentaBancariaEmpresa")]
        public static extern int fAltaCuentaBancariaEmpresa(ref int aIdCtaBancaria, string aCuentaBancaria, string aNombreCuenta,
            string aNombreMoneda, string aClaveBanco, string aClabe, string aRfcBanco, string aNombreBancoExtranjero);

        [DllImport("MGWServicios.dll", EntryPoint = "fAltaDireccion")]
        public static extern int fAltaDireccion(ref int aIdDireccion, ref tDireccion astDireccion);

        [DllImport("MGWServicios.dll", EntryPoint = "fAltaDoctoAjusteIESPSCteProv")]
        public static extern int fAltaDoctoAjusteIESPSCteProv(string aCodigoClienteProveedor, int aEsCliente, string aFechaDocto, int aIdMoneda,
            double aTipoCambio, double aImporteIVA, double aTasaIVA, double aImporteIESPS, double aTasaIESPS, int aIdFacturaBase,
            string aMetodo, string aLugar, ref int aIdDoctoGenerado);

        [DllImport("MGWServicios.dll", EntryPoint = "fAltaDoctoAjusteIVAClienteProveedor")]
        public static extern int fAltaDoctoAjusteIVAClienteProveedor(string aCodigoClienteProveedor, int aEsCliente, int aAbsorberAjusteIVA,
            string aFechaDocto, int aIdMoneda, double aTipoCambio, double aImporteIVA, double aTasaIVA, int aIdFacturaBase, string aMetodo,
            string aLugar, ref int aIdDoctoGenerado);

        [DllImport("MGWServicios.dll", EntryPoint = "fAltaDocumento")]
        public static extern int fAltaDocumento(ref int aIdDocumento, ref tDocumento aDocumento);

        [DllImport("MGWServicios.dll", EntryPoint = "fAltaDocumentoCargoAbono")]
        public static extern int fAltaDocumentoCargoAbono(ref tDocumento aDocumento);

        [DllImport("MGWServicios.dll", EntryPoint = "fAltaDocumentoCargoAbonoExtras")]
        public static extern int fAltaDocumentoCargoAbonoExtras(ref tDocumento aDocumento, string aTextoExtra1, string aTextoExtra2,
            string aTextoExtra3, string aFechaExtra, double aImporteExtra1, double aImporteExtra2, double aImporteExtra3,
            double aImporteExtra4);

        [DllImport("MGWServicios.dll", EntryPoint = "fAltaMovimiento")]
        public static extern int fAltaMovimiento(int aIdDocumento, ref int aIdMovimiento, ref tMovimiento astMovimiento);

        [DllImport("MGWServicios.dll", EntryPoint = "fAltaMovimientoCaracteristicas")]
        public static extern int fAltaMovimientoCaracteristicas(int aIdMovimiento, ref int aIdMovtoCaracteristicas,
            ref tCaracteristicas aCaracteristicas);

        [DllImport("MGWServicios.dll", EntryPoint = "fAltaMovimientoCaracteristicas_Param")]
        public static extern int fAltaMovimientoCaracteristicas_Param(string aIdMovimiento, string aIdMovtoCaracteristicas, string aUnidades,
            string aValorCaracteristica1, string aValorCaracteristica2, string aValorCaracteristica3);

        [DllImport("MGWServicios.dll", EntryPoint = "fAltaMovimientoCDesct")]
        public static extern int fAltaMovimientoCDesct(int aIdDocumento, ref int aIdMovimiento, ref tMovimientoDesc astMovimiento);

        [DllImport("MGWServicios.dll", EntryPoint = "fAltaMovimientoEx")]
        public static extern int fAltaMovimientoEx(ref int aIdMovimiento, ref tTipoProducto aTipoProducto);

        [DllImport("MGWServicios.dll", EntryPoint = "fAltaMovimientoSeriesCapas")]
        public static extern int fAltaMovimientoSeriesCapas(int aIdMovimiento, ref tSeriesCapas aSeriesCapas);

        [DllImport("MGWServicios.dll", EntryPoint = "fAltaMovimientoSeriesCapas_Param")]
        public static extern int fAltaMovimientoSeriesCapas_Param(string aIdMovimiento, string aUnidades, string aTipoCambio, string aSeries,
            string aPedimento, string aAgencia, string aFechaPedimento, string aNumeroLote, string aFechaFabricacion, string aFechaCaducidad);

        [DllImport("MGWServicios.dll", EntryPoint = "fAltaMovtoCaracteristicasUnidades")]
        public static extern int fAltaMovtoCaracteristicasUnidades(int aIdMovimiento, ref int aIdMovtoCaracteristicas,
            ref tCaracteristicas aCaracteristicasUnidades);

        [DllImport("MGWServicios.dll", EntryPoint = "fAltaMovtoCaracteristicasUnidades_Param")]
        public static extern int fAltaMovtoCaracteristicasUnidades_Param(string aIdMovimiento, string aIdMovtoCaracteristicas, string aUnidad,
            string aUnidades, string aUnidadesNC, string aValorCaracteristica1, string aValorCaracteristica2, string aValorCaracteristica3);

        [DllImport("MGWServicios.dll", EntryPoint = "fAltaProducto")]
        public static extern int fAltaProducto(ref int aIdProducto, ref tProducto astProducto);

        [DllImport("MGWServicios.dll", EntryPoint = "fAltaUnidad")]
        public static extern int fAltaUnidad(ref int aIdUnidad, ref tUnidad astUnidad);

        [DllImport("MGWServicios.dll", EntryPoint = "fAltaValorClasif")]
        public static extern int fAltaValorClasif(ref int aIdValorClasif, ref tValorClasificacion astValorClasif);

        [DllImport("MGWServicios.dll", EntryPoint = "fBorraCteProv")]
        public static extern int fBorraCteProv();

        [DllImport("MGWServicios.dll", EntryPoint = "fBorraCuentaBancariaCliente")]
        public static extern int fBorraCuentaBancariaCliente(string aCuentaBancaria, string aCodigoCliente);

        [DllImport("MGWServicios.dll", EntryPoint = "fBorraCuentaBancariaEmpresa")]
        public static extern int fBorraCuentaBancariaEmpresa(string aCuentaBancaria);

        [DllImport("MGWServicios.dll", EntryPoint = "fBorraDocumento")]
        public static extern int fBorraDocumento();

        [DllImport("MGWServicios.dll", EntryPoint = "fBorraDocumento_CW")]
        public static extern int fBorraDocumento_CW();

        [DllImport("MGWServicios.dll", EntryPoint = "fBorraMovimiento")]
        public static extern int fBorraMovimiento(int aIdDocumento, int aIdMovimiento);

        [DllImport("MGWServicios.dll", EntryPoint = "fBorraProducto")]
        public static extern int fBorraProducto();

        [DllImport("MGWServicios.dll", EntryPoint = "fBorrarAsociacion")]
        public static extern int fBorrarAsociacion(tLlaveDoc aDoctoaPagar, tLlaveDoc aDoctoPago);

        [DllImport("MGWServicios.dll", EntryPoint = "fBorrarAsociacion_Param")]
        public static extern int fBorrarAsociacion_Param(string aCodConcepto_Pagar, string aSerie_Pagar, double aFolio_Pagar,
            string aCodConcepto_Pago, string aSerie_Pago, double aFolio_Pago);

        [DllImport("MGWServicios.dll", EntryPoint = "fBorraUnidad")]
        public static extern int fBorraUnidad();

        [DllImport("MGWServicios.dll", EntryPoint = "fBorraValorClasif")]
        public static extern int fBorraValorClasif();

        [DllImport("MGWServicios.dll", EntryPoint = "fBuscaAgente")]
        public static extern int fBuscaAgente(string aCodigoAgente);

        [DllImport("MGWServicios.dll", EntryPoint = "fBuscaAlmacen")]
        public static extern int fBuscaAlmacen(string aCodigoAlmacen);

        [DllImport("MGWServicios.dll", EntryPoint = "fBuscaClasificacion")]
        public static extern int fBuscaClasificacion(int aClasificacionDe, int aNumClasificacion);

        [DllImport("MGWServicios.dll", EntryPoint = "fBuscaConceptoDocto")]
        public static extern int fBuscaConceptoDocto(string aCodConcepto);

        [DllImport("MGWServicios.dll", EntryPoint = "fBuscaCteProv")]
        public static extern int fBuscaCteProv(string aCodCteProv);

        [DllImport("MGWServicios.dll", EntryPoint = "fBuscaDireccionCteProv")]
        public static extern int fBuscaDireccionCteProv(string aCodCteProv, byte aTipoDireccion);

        [DllImport("MGWServicios.dll", EntryPoint = "fBuscaDireccionDocumento")]
        public static extern int fBuscaDireccionDocumento(int aIdDocumento, byte aTipoDireccion);

        [DllImport("MGWServicios.dll", EntryPoint = "fBuscaDireccionEmpresa")]
        public static extern int fBuscaDireccionEmpresa();

        [DllImport("MGWServicios.dll", EntryPoint = "fBuscaDocumento")]
        public static extern int fBuscaDocumento(ref tLlaveDoc aLlaveDocto);

        [DllImport("MGWServicios.dll", EntryPoint = "fBuscaIdAgente")]
        public static extern int fBuscaIdAgente(int aIdAgente);

        [DllImport("MGWServicios.dll", EntryPoint = "fBuscaIdAlmacen")]
        public static extern int fBuscaIdAlmacen(int aIdAlmacen);

        [DllImport("MGWServicios.dll", EntryPoint = "fBuscaIdClasificacion")]
        public static extern int fBuscaIdClasificacion(int aIdClasificacion);

        [DllImport("MGWServicios.dll", EntryPoint = "fBuscaIdConceptoDocto")]
        public static extern int fBuscaIdConceptoDocto(int aIdConcepto);

        [DllImport("MGWServicios.dll", EntryPoint = "fBuscaIdCteProv")]
        public static extern int fBuscaIdCteProv(int aIdCteProv);

        [DllImport("MGWServicios.dll", EntryPoint = "fBuscaIdProducto")]
        public static extern int fBuscaIdProducto(int aIdProducto);

        [DllImport("MGWServicios.dll", EntryPoint = "fBuscaIdUnidad")]
        public static extern int fBuscaIdUnidad(int aIdUnidad);

        [DllImport("MGWServicios.dll", EntryPoint = "fBuscaIdValorClasif")]
        public static extern int fBuscaIdValorClasif(int aIdValorClasif);

        [DllImport("MGWServicios.dll", EntryPoint = "fBuscaProducto")]
        public static extern int fBuscaProducto(string aCodProducto);

        [DllImport("MGWServicios.dll", EntryPoint = "fBuscarDocumento")]
        public static extern int fBuscarDocumento(string aCodConcepto, string aSerie, string aFolio);

        [DllImport("MGWServicios.dll", EntryPoint = "fBuscarIdDocumento")]
        public static extern int fBuscarIdDocumento(int aIdDocumento);

        [DllImport("MGWServicios.dll", EntryPoint = "fBuscarIdMovimiento")]
        public static extern int fBuscarIdMovimiento(int aIdMovimiento);

        [DllImport("MGWServicios.dll", EntryPoint = "fBuscaUnidad")]
        public static extern int fBuscaUnidad(string aNombreUnidad);

        [DllImport("MGWServicios.dll", EntryPoint = "fBuscaValorClasif")]
        public static extern int fBuscaValorClasif(int aClasificacionDe, int aNumClasificacion, string aCodValorClasif);

        [DllImport("MGWServicios.dll", EntryPoint = "fCalculaMovtoSerieCapa")]
        public static extern int fCalculaMovtoSerieCapa(int aIdMovimiento);

        [DllImport("MGWServicios.dll", EntryPoint = "fCancelaCambiosMovimiento")]
        public static extern int fCancelaCambiosMovimiento();

        [DllImport("MGWServicios.dll", EntryPoint = "fCancelaDoctoInfo")]
        public static extern int fCancelaDoctoInfo(string aPass);

        [DllImport("MGWServicios.dll", EntryPoint = "fCancelaDocumento")]
        public static extern int fCancelaDocumento();

        [DllImport("MGWServicios.dll", EntryPoint = "fCancelaDocumento_CW")]
        public static extern int fCancelaDocumento_CW();

        [DllImport("MGWServicios.dll", EntryPoint = "fCancelaDocumentoAdministrativamente")]
        public static extern int fCancelaDocumentoAdministrativamente();

        [DllImport("MGWServicios.dll", EntryPoint = "fCancelaDocumentoConMotivo")]
        public static extern int fCancelaDocumentoConMotivo(string aMotivoCancelacion, string aUUIDRemplaza);

        [DllImport("MGWServicios.dll", EntryPoint = "fCancelaFiltroDocumento")]
        public static extern int fCancelaFiltroDocumento();

        [DllImport("MGWServicios.dll", EntryPoint = "fCancelaFiltroMovimiento")]
        public static extern int fCancelaFiltroMovimiento();

        [DllImport("MGWServicios.dll", EntryPoint = "fCancelaNominaUUID")]
        public static extern int fCancelaNominaUUID(string aUUID, string aIdDConcepto, string aPass);

        [DllImport("MGWServicios.dll", EntryPoint = "fCancelarModificacionAgente")]
        public static extern int fCancelarModificacionAgente();

        [DllImport("MGWServicios.dll", EntryPoint = "fCancelarModificacionAlmacen")]
        public static extern int fCancelarModificacionAlmacen();

        [DllImport("MGWServicios.dll", EntryPoint = "fCancelarModificacionClasificacion")]
        public static extern int fCancelarModificacionClasificacion();

        [DllImport("MGWServicios.dll", EntryPoint = "fCancelarModificacionCteProv")]
        public static extern int fCancelarModificacionCteProv();

        [DllImport("MGWServicios.dll", EntryPoint = "fCancelarModificacionDireccion")]
        public static extern int fCancelarModificacionDireccion();

        [DllImport("MGWServicios.dll", EntryPoint = "fCancelarModificacionDocumento")]
        public static extern int fCancelarModificacionDocumento();

        [DllImport("MGWServicios.dll", EntryPoint = "fCancelarModificacionProducto")]
        public static extern int fCancelarModificacionProducto();

        [DllImport("MGWServicios.dll", EntryPoint = "fCancelarModificacionUnidad")]
        public static extern int fCancelarModificacionUnidad();

        [DllImport("MGWServicios.dll", EntryPoint = "fCancelarModificacionValorClasif")]
        public static extern int fCancelarModificacionValorClasif();

        [DllImport("MGWServicios.dll", EntryPoint = "fCancelaUUID")]
        public static extern int fCancelaUUID(string aUUID, string aIdDConcepto, string aPass);

        [DllImport("MGWServicios.dll", EntryPoint = "fCancelaUUID40")]
        public static extern int fCancelaUUID40(string aUUID, string aMotivoCancelacion, string aUUIDReemplaza, string RFCReceptor,
            double aTotal, string aIdDConcepto, string aPass, ref int aEstatusCancelacion);

        [DllImport("MGWServicios.dll", EntryPoint = "fCierraEmpresa")]
        public static extern void fCierraEmpresa();

        [DllImport("MGWServicios.dll", EntryPoint = "fCuentaBancariaEmpresaDoctos")]
        public static extern int fCuentaBancariaEmpresaDoctos(string aCuentaBancaria);

        [DllImport("MGWServicios.dll", EntryPoint = "fDesbloqueaDocumento")]
        public static extern int fDesbloqueaDocumento();

        [DllImport("MGWServicios.dll", EntryPoint = "fDocumentoBloqueado")]
        public static extern int fDocumentoBloqueado(ref int aImpreso);

        [DllImport("MGWServicios.dll", EntryPoint = "fDocumentoDevuelto")]
        public static extern int fDocumentoDevuelto(ref int aDevuelto);

        [DllImport("MGWServicios.dll", EntryPoint = "fDocumentoImpreso")]
        public static extern int fDocumentoImpreso(ref bool aImpreso);

        [DllImport("MGWServicios.dll", EntryPoint = "fDocumentoUUID")]
        public static extern int fDocumentoUUID(StringBuilder aCodConcepto, StringBuilder aSerie, double aFolio, StringBuilder atPtrCFDIUUID);

        [DllImport("MGWServicios.dll", EntryPoint = "fEditaAgente")]
        public static extern int fEditaAgente();

        [DllImport("MGWServicios.dll", EntryPoint = "fEditaAlmacen")]
        public static extern int fEditaAlmacen();

        [DllImport("MGWServicios.dll", EntryPoint = "fEditaClasificacion")]
        public static extern int fEditaClasificacion();

        [DllImport("MGWServicios.dll", EntryPoint = "fEditaConceptoDocto")]
        public static extern int fEditaConceptoDocto();

        [DllImport("MGWServicios.dll", EntryPoint = "fEditaCteProv")]
        public static extern int fEditaCteProv();

        [DllImport("MGWServicios.dll", EntryPoint = "fEditaDireccion")]
        public static extern int fEditaDireccion();

        [DllImport("MGWServicios.dll", EntryPoint = "fEditaMovtoContable")]
        public static extern int fEditaMovtoContable();

        [DllImport("MGW_SDK.dll", EntryPoint = "fEditaParametros")]
        public static extern int fEditaParametros();

        [DllImport("MGWServicios.dll", EntryPoint = "fEditaProducto")]
        public static extern int fEditaProducto();

        [DllImport("MGWServicios.dll", EntryPoint = "fEditarDocumento")]
        public static extern int fEditarDocumento();

        [DllImport("MGWServicios.dll", EntryPoint = "fEditarDocumentoCheqpaq")]
        public static extern int fEditarDocumentoCheqpaq();

        [DllImport("MGWServicios.dll", EntryPoint = "fEditarMovimiento")]
        public static extern int fEditarMovimiento();

        [DllImport("MGWServicios.dll", EntryPoint = "fEditaUnidad")]
        public static extern int fEditaUnidad();

        [DllImport("MGWServicios.dll", EntryPoint = "fEditaValorClasif")]
        public static extern int fEditaValorClasif();

        [DllImport("MGWServicios.dll", EntryPoint = "fEliminarCteProv")]
        public static extern int fEliminarCteProv(string aCodigoCteProv);

        [DllImport("MGWServicios.dll", EntryPoint = "fEliminarProducto")]
        public static extern int fEliminarProducto(string aCodigoProducto);

        [DllImport("MGWServicios.dll", EntryPoint = "fEliminarRelacionesCFDIs")]
        public static extern int fEliminarRelacionesCFDIs(string aCodConcepto, string aSerie, string aFolio);

        [DllImport("MGWServicios.dll", EntryPoint = "fEliminarUnidad")]
        public static extern int fEliminarUnidad(string aNombreUnidad);

        [DllImport("MGWServicios.dll", EntryPoint = "fEliminarValorClasif")]
        public static extern int fEliminarValorClasif(int aClasificacionDe, int aNumClasificacion, string aCodigoValorClasif);

        [DllImport("MGWServicios.dll", EntryPoint = "fEmitirDocumento")]
        public static extern int fEmitirDocumento([MarshalAs(UnmanagedType.LPStr)] string aCodConcepto,
            [MarshalAs(UnmanagedType.LPStr)] string aSerie, double aFolio, [MarshalAs(UnmanagedType.LPStr)] string aPassword,
            [MarshalAs(UnmanagedType.LPStr)] string aArchivoAdicional);

        [DllImport("MGWServicios.dll", EntryPoint = "fEntregEnDiscoXML")]
        public static extern int fEntregEnDiscoXML(string aCodConcepto, string aSerie, double aFolio, int aFormato, string aFormatoAmig);

        [DllImport("MGWServicios.dll", EntryPoint = "fError")]
        public static extern void fError(int aNumError, StringBuilder aMensaje, int aLen);

        [DllImport("MGWServicios.dll", EntryPoint = "fGetCantidadParcialidades")]
        public static extern int fGetCantidadParcialidades(StringBuilder atPtrPassword, StringBuilder aCantidadParcialidades);

        [DllImport("MGWServicios.dll", EntryPoint = "fGetDatosCFDI")]
        public static extern int fGetDatosCFDI(StringBuilder aSerieCertEmisor, StringBuilder aFolioFiscalUUID, StringBuilder aSerieCertSAT,
            StringBuilder aFechaHora, StringBuilder aSelloDigCFDI, StringBuilder aSelloSAAT, StringBuilder aCadOrigComplSAT,
            StringBuilder aRegimen, StringBuilder aLugarExpedicion, StringBuilder aMoneda, StringBuilder aFolioFiscalOrig,
            StringBuilder aSerieFolioFiscalOrig, StringBuilder aFechaFolioFiscalOrig, StringBuilder aMontoFolioFiscalOrig);

        [DllImport("MGWServicios.dll", EntryPoint = "fGetNumParcialidades")]
        public static extern int fGetNumParcialidades(StringBuilder atPtrPassword, StringBuilder aNumParcialidades);

        [DllImport("MGWServicios.dll", EntryPoint = "fGetSelloDigitalYCadena")]
        public static extern int fGetSelloDigitalYCadena(StringBuilder atPtrPassword, StringBuilder atPtrSelloDigital,
            StringBuilder atPtrCadenaOriginal);

        [DllImport("MGWServicios.dll", EntryPoint = "fGetSerieCertificado")]
        public static extern int fGetSerieCertificado(StringBuilder atPtrPassword, StringBuilder aSerieCertificado);

        [DllImport("MGWServicios.dll", EntryPoint = "fGetTamSelloDigitalYCadena")]
        public static extern int fGetTamSelloDigitalYCadena(StringBuilder atPtrPassword, ref int aEspSelloDig, ref int aEspCadOrig);

        [DllImport("MGWServicios.dll", EntryPoint = "fGuardaAgente")]
        public static extern int fGuardaAgente();

        [DllImport("MGWServicios.dll", EntryPoint = "fGuardaAlmacen")]
        public static extern int fGuardaAlmacen();

        [DllImport("MGWServicios.dll", EntryPoint = "fGuardaClasificacion")]
        public static extern int fGuardaClasificacion();

        [DllImport("MGWServicios.dll", EntryPoint = "fGuardaConceptoDocto")]
        public static extern int fGuardaConceptoDocto();

        [DllImport("MGWServicios.dll", EntryPoint = "fGuardaCteProv")]
        public static extern int fGuardaCteProv();

        [DllImport("MGWServicios.dll", EntryPoint = "fGuardaDireccion")]
        public static extern int fGuardaDireccion();

        [DllImport("MGWServicios.dll", EntryPoint = "fGuardaDocumento")]
        public static extern int fGuardaDocumento();

        [DllImport("MGWServicios.dll", EntryPoint = "fGuardaMovimiento")]
        public static extern int fGuardaMovimiento();

        [DllImport("MGWServicios.dll", EntryPoint = "fGuardaMovtoContable")]
        public static extern int fGuardaMovtoContable();

        [DllImport("MGW_SDK.dll", EntryPoint = "fGuardaParametros")]
        public static extern int fGuardaParametros();

        [DllImport("MGWServicios.dll", EntryPoint = "fGuardaProducto")]
        public static extern int fGuardaProducto();

        [DllImport("MGWServicios.dll", EntryPoint = "fGuardaUnidad")]
        public static extern int fGuardaUnidad();

        [DllImport("MGWServicios.dll", EntryPoint = "fGuardaValorClasif")]
        public static extern int fGuardaValorClasif();

        [DllImport("MGWServicios.dll", EntryPoint = "fInformacionCliente")]
        public static extern int fInformacionCliente(StringBuilder aCodigo, ref int aPermiteCredito, ref double aLimiteCredito,
            ref int aLimiteDoctosVencidos, ref int aPermiteExcederCredito, StringBuilder aFecha, ref double aSaldo, ref double aSaldoPendiente,
            ref int aDoctosVencidos);

        [DllImport("MGWServicios.dll", EntryPoint = "fInicializaLicenseInfo")]
        public static extern int fInicializaLicenseInfo(byte aSistema);

        [DllImport("MGWServicios.dll", EntryPoint = "fInicializaSDK")]
        public static extern int fInicializaSDK();

        [DllImport("MGWServicios.dll", EntryPoint = "fInicioSesionSDK")]
        public static extern void fInicioSesionSDK(string aUsuario, string aContrasenia);

        [DllImport("MGWServicios.dll", EntryPoint = "fInicioSesionSDKCONTPAQi")]
        public static extern void fInicioSesionSDKCONTPAQi(string aUsuario, string aContrasenia);

        [DllImport("MGWServicios.dll", EntryPoint = "fInsertaAgente")]
        public static extern int fInsertaAgente();

        [DllImport("MGWServicios.dll", EntryPoint = "fInsertaAlmacen")]
        public static extern int fInsertaAlmacen();

        [DllImport("MGWServicios.dll", EntryPoint = "fInsertaCteProv")]
        public static extern int fInsertaCteProv();

        [DllImport("MGWServicios.dll", EntryPoint = "fInsertaDatoAddendaDocto")]
        public static extern int fInsertaDatoAddendaDocto(int aIdAddenda, int aIdCatalogo, int aNumCampo, string aDato);

        [DllImport("MGWServicios.dll", EntryPoint = "fInsertaDatoAddendaMovto")]
        public static extern int fInsertaDatoAddendaMovto(int aIdAddenda, int aIdCatalogo, int aNumCampo, string aDato);

        [DllImport("MGWServicios.dll", EntryPoint = "fInsertaDatoCompEducativo")]
        public static extern int fInsertaDatoCompEducativo(int aIdServicio, int aNumCampo, string aDato);

        [DllImport("MGWServicios.dll", EntryPoint = "fInsertaDireccion")]
        public static extern int fInsertaDireccion();

        [DllImport("MGWServicios.dll", EntryPoint = "fInsertaProducto")]
        public static extern int fInsertaProducto();

        [DllImport("MGWServicios.dll", EntryPoint = "fInsertarDocumento")]
        public static extern int fInsertarDocumento();

        [DllImport("MGWServicios.dll", EntryPoint = "fInsertarMovimiento")]
        public static extern int fInsertarMovimiento();

        [DllImport("MGWServicios.dll", EntryPoint = "fInsertaUnidad")]
        public static extern int fInsertaUnidad();

        [DllImport("MGWServicios.dll", EntryPoint = "fInsertaValorClasif")]
        public static extern int fInsertaValorClasif();

        [DllImport("MGWServicios.dll", EntryPoint = "fLeeDatoAgente")]
        public static extern int fLeeDatoAgente(string aCampo, StringBuilder aValor, int aLen);

        [DllImport("MGWServicios.dll", EntryPoint = "fLeeDatoAlmacen")]
        public static extern int fLeeDatoAlmacen(string aCampo, StringBuilder aValor, int aLen);

        [DllImport("MGWServicios.dll", EntryPoint = "fLeeDatoCFDI")]
        public static extern int fLeeDatoCFDI(StringBuilder aValor, int aDato);

        [DllImport("MGWServicios.dll", EntryPoint = "fLeeDatoClasificacion")]
        public static extern int fLeeDatoClasificacion(string aCampo, StringBuilder aValor, int aLen);

        [DllImport("MGWServicios.dll", EntryPoint = "fLeeDatoConceptoDocto")]
        public static extern int fLeeDatoConceptoDocto(string aCampo, StringBuilder aValor, int aLen);

        [DllImport("MGWServicios.dll", EntryPoint = "fLeeDatoCteProv")]
        public static extern int fLeeDatoCteProv(string aCampo, StringBuilder aValor, int aLen);

        [DllImport("MGWServicios.dll", EntryPoint = "fLeeDatoDireccion")]
        public static extern int fLeeDatoDireccion(string aCampo, StringBuilder aValor, int aLen);

        [DllImport("MGWServicios.dll", EntryPoint = "fLeeDatoDocumento")]
        public static extern int fLeeDatoDocumento(string aCampo, StringBuilder aValor, int aLongitud);

        [DllImport("MGWServicios.dll", EntryPoint = "fLeeDatoMovimiento")]
        public static extern int fLeeDatoMovimiento(string aCampo, StringBuilder aValor, int aLen);

        [DllImport("MGWServicios.dll", EntryPoint = "fLeeDatoMovtoContable")]
        public static extern int fLeeDatoMovtoContable(string aCampo, StringBuilder aValor, int aLen);

        [DllImport("MGWServicios.dll", EntryPoint = "fLeeDatoParametros")]
        public static extern int fLeeDatoParametros(string aCampo, StringBuilder aValor, int aLen);

        [DllImport("MGWServicios.dll", EntryPoint = "fLeeDatoProducto")]
        public static extern int fLeeDatoProducto(string aCampo, StringBuilder aValor, int aLen);

        [DllImport("MGWServicios.dll", EntryPoint = "fLeeDatoUnidad")]
        public static extern int fLeeDatoUnidad(string aCampo, StringBuilder aValor, int aLen);

        [DllImport("MGWServicios.dll", EntryPoint = "fLeeDatoValorClasif")]
        public static extern int fLeeDatoValorClasif(string aCampo, StringBuilder aValor, int aLen);

        [DllImport("MGWServicios.dll", EntryPoint = "fLlenaRegistroCteProv")]
        public static extern int fLlenaRegistroCteProv(ref tCteProv astCteProv, int aEsAlta);

        [DllImport("MGWServicios.dll", EntryPoint = "fLlenaRegistroDireccion")]
        public static extern int fLlenaRegistroDireccion(ref tDireccion astDireccion, int aEsAlta);

        [DllImport("MGWServicios.dll", EntryPoint = "fLlenaRegistroProducto")]
        public static extern int fLlenaRegistroProducto(ref tProducto astProducto, int aEsAlta);

        [DllImport("MGWServicios.dll", EntryPoint = "fLlenaRegistroUnidad")]
        public static extern int fLlenaRegistroUnidad(ref tUnidad astUnidad);

        [DllImport("MGWServicios.dll", EntryPoint = "fLlenaRegistroValorClasif")]
        public static extern int fLlenaRegistroValorClasif(ref tValorClasificacion astValorClasif);

        [DllImport("MGWServicios.dll", EntryPoint = "fModificaCostoEntrada")]
        public static extern int fModificaCostoEntrada(string aIdMovimiento, string aCostoEntrada);

        [DllImport("MGWServicios.dll", EntryPoint = "fModificaCuentaBancariaCliente")]
        public static extern int fModificaCuentaBancariaCliente(string aCuentaBancaria, string aNombreCuenta, string aNombreMoneda,
            string aClaveBanco, string aRfcBanco, string aClabe, string aNombreExtranjero, string aCodigoCliente);

        [DllImport("MGWServicios.dll", EntryPoint = "fModificaCuentaBancariaEmpresa")]
        public static extern int fModificaCuentaBancariaEmpresa(string aCuentaBancaria, string aNombreCuenta, string aNombreMoneda,
            string aClaveBanco, string aRfcBanco, string aClabe, string aNombreExtranjero);

        [DllImport("MGWServicios.dll", EntryPoint = "fObtenCeryKey")]
        public static extern int fObtenCeryKey(int aIdFirmarl, string aRutaKey, string aRutaCer);

        [DllImport("MGWServicios.dll", EntryPoint = "fObtieneDatosCFDI")]
        public static extern int fObtieneDatosCFDI(string atPtrPassword);

        [DllImport("MGWServicios.dll", EntryPoint = "fObtieneLicencia")]
        public static extern int fObtieneLicencia(StringBuilder aCodAcvtiva, StringBuilder aCodSitio, StringBuilder aSerie,
            StringBuilder aTagVersion);

        [DllImport("MGWServicios.dll", EntryPoint = "fObtienePassProxy")]
        public static extern int fObtienePassProxy(StringBuilder aPassProxy);

        [DllImport("MGWServicios.dll", EntryPoint = "fObtieneUnidadesPendientes")]
        public static extern int fObtieneUnidadesPendientes(string aConceptoDocto, string aCodigoProducto, string aCodigoAlmacen,
            StringBuilder aUnidades);

        [DllImport("MGWServicios.dll", EntryPoint = "fObtieneUnidadesPendientesCarac")]
        public static extern int fObtieneUnidadesPendientesCarac(string aConceptoDocto, string aCodigoProducto, string aCodigoAlmacen,
            string aValorCaracteristica1, string aValorCaracteristica2, string aValorCaracteristica3, StringBuilder aUnidades);

        [DllImport("MGWServicios.dll", EntryPoint = "fPosAnteriorAgente")]
        public static extern int fPosAnteriorAgente();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosAnteriorAlmacen")]
        public static extern int fPosAnteriorAlmacen();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosAnteriorClasificacion")]
        public static extern int fPosAnteriorClasificacion();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosAnteriorConceptoDocto")]
        public static extern int fPosAnteriorConceptoDocto();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosAnteriorCteProv")]
        public static extern int fPosAnteriorCteProv();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosAnteriorDireccion")]
        public static extern int fPosAnteriorDireccion();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosAnteriorDocumento")]
        public static extern int fPosAnteriorDocumento();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosAnteriorMovimiento")]
        public static extern int fPosAnteriorMovimiento();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosAnteriorProducto")]
        public static extern int fPosAnteriorProducto();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosAnteriorUnidad")]
        public static extern int fPosAnteriorUnidad();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosAnteriorValorClasif")]
        public static extern int fPosAnteriorValorClasif();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosBOF")]
        public static extern int fPosBOF();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosBOFAgente")]
        public static extern int fPosBOFAgente();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosBOFAlmacen")]
        public static extern int fPosBOFAlmacen();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosBOFClasificacion")]
        public static extern int fPosBOFClasificacion();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosBOFConceptoDocto")]
        public static extern int fPosBOFConceptoDocto();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosBOFCteProv")]
        public static extern int fPosBOFCteProv();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosBOFDireccion")]
        public static extern int fPosBOFDireccion();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosBOFProducto")]
        public static extern int fPosBOFProducto();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosBOFUnidad")]
        public static extern int fPosBOFUnidad();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosBOFValorClasif")]
        public static extern int fPosBOFValorClasif();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosEOF")]
        public static extern int fPosEOF();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosEOFAgente")]
        public static extern int fPosEOFAgente();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosEOFAlmacen")]
        public static extern int fPosEOFAlmacen();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosEOFClasificacion")]
        public static extern int fPosEOFClasificacion();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosEOFConceptoDocto")]
        public static extern int fPosEOFConceptoDocto();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosEOFCteProv")]
        public static extern int fPosEOFCteProv();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosEOFDireccion")]
        public static extern int fPosEOFDireccion();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosEOFMovtoContable")]
        public static extern int fPosEOFMovtoContable();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosEOFProducto")]
        public static extern int fPosEOFProducto();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosEOFUnidad")]
        public static extern int fPosEOFUnidad();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosEOFValorClasif")]
        public static extern int fPosEOFValorClasif();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosMovimientoBOF")]
        public static extern int fPosMovimientoBOF();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosMovimientoEOF")]
        public static extern int fPosMovimientoEOF();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosPrimerAgente")]
        public static extern int fPosPrimerAgente();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosPrimerAlmacen")]
        public static extern int fPosPrimerAlmacen();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosPrimerClasificacion")]
        public static extern int fPosPrimerClasificacion();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosPrimerConceptoDocto")]
        public static extern int fPosPrimerConceptoDocto();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosPrimerCteProv")]
        public static extern int fPosPrimerCteProv();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosPrimerDireccion")]
        public static extern int fPosPrimerDireccion();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosPrimerDocumento")]
        public static extern int fPosPrimerDocumento();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosPrimerEmpresa")]
        public static extern int fPosPrimerEmpresa(ref int aIdEmpresa, StringBuilder aNombreEmpresa, StringBuilder aDirectorioEmpresa);

        [DllImport("MGWServicios.dll", EntryPoint = "fPosPrimerMovimiento")]
        public static extern int fPosPrimerMovimiento();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosPrimerMovtoContable")]
        public static extern int fPosPrimerMovtoContable();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosPrimerProducto")]
        public static extern int fPosPrimerProducto();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosPrimerUnidad")]
        public static extern int fPosPrimerUnidad();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosPrimerValorClasif")]
        public static extern int fPosPrimerValorClasif();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosSiguienteAgente")]
        public static extern int fPosSiguienteAgente();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosSiguienteAlmacen")]
        public static extern int fPosSiguienteAlmacen();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosSiguienteClasificacion")]
        public static extern int fPosSiguienteClasificacion();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosSiguienteConceptoDocto")]
        public static extern int fPosSiguienteConceptoDocto();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosSiguienteCteProv")]
        public static extern int fPosSiguienteCteProv();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosSiguienteDireccion")]
        public static extern int fPosSiguienteDireccion();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosSiguienteDocumento")]
        public static extern int fPosSiguienteDocumento();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosSiguienteEmpresa")]
        public static extern int fPosSiguienteEmpresa(ref int aIdEmpresa, StringBuilder aNombreEmpresa, StringBuilder aDirectorioEmpresa);

        [DllImport("MGWServicios.dll", EntryPoint = "fPosSiguienteMovimiento")]
        public static extern int fPosSiguienteMovimiento();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosSiguienteMovtoContable")]
        public static extern int fPosSiguienteMovtoContable();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosSiguienteProducto")]
        public static extern int fPosSiguienteProducto();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosSiguienteUnidad")]
        public static extern int fPosSiguienteUnidad();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosSiguienteValorClasif")]
        public static extern int fPosSiguienteValorClasif();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosUltimaConceptoDocto")]
        public static extern int fPosUltimaConceptoDocto();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosUltimaDireccion")]
        public static extern int fPosUltimaDireccion();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosUltimoAgente")]
        public static extern int fPosUltimoAgente();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosUltimoAlmacen")]
        public static extern int fPosUltimoAlmacen();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosUltimoClasificacion")]
        public static extern int fPosUltimoClasificacion();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosUltimoCteProv")]
        public static extern int fPosUltimoCteProv();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosUltimoDocumento")]
        public static extern int fPosUltimoDocumento();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosUltimoMovimiento")]
        public static extern int fPosUltimoMovimiento();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosUltimoProducto")]
        public static extern int fPosUltimoProducto();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosUltimoUnidad")]
        public static extern int fPosUltimoUnidad();

        [DllImport("MGWServicios.dll", EntryPoint = "fPosUltimoValorClasif")]
        public static extern int fPosUltimoValorClasif();

        [DllImport("MGWServicios.dll", EntryPoint = "fProyectoEmpresaDoctos")]
        public static extern int fProyectoEmpresaDoctos(string aCodigoProyecto);

        [DllImport("MGWServicios.dll", EntryPoint = "fRecosteoProducto")]
        public static extern int fRecosteoProducto(string aCodigoProducto, int aEjercicio, int aPeriodo, string aCodigoClasificacion1,
            string aCodigoClasificacion2, string aCodigoClasificacion3, string aCodigoClasificacion4, string aCodigoClasificacion5,
            string aCodigoClasificacion6, string aNombreBitacora, int aSobreEscribirBitacora, int aEsCalculoArimetico);

        [DllImport("MGWServicios.dll", EntryPoint = "fRecuperarRelacionesCFDIs")]
        public static extern int fRecuperarRelacionesCFDIs(string aCodConcepto, string aSerie, string aFolio, string aTipoRelacion,
            StringBuilder aUUIDs, string aRutaNombreArchivoInfo);

        [DllImport("MGWServicios.dll", EntryPoint = "fRecuperaTipoProducto")]
        public static extern int fRecuperaTipoProducto(ref bool aUnidades, ref bool aSerie, ref bool aLote, ref bool aPedimento,
            ref bool aCaracteristicas);

        [DllImport("MGWServicios.dll", EntryPoint = "fRegresaCostoCapa")]
        public static extern int fRegresaCostoCapa(string aCodigoProducto, string aCodigoAlmacen, double aUnidades,
            StringBuilder aImporteCosto);

        [DllImport("MGWServicios.dll", EntryPoint = "fRegresaCostoEstandar")]
        public static extern int fRegresaCostoEstandar(string aCodigoProducto, StringBuilder aCostoEstandar);

        [DllImport("MGWServicios.dll", EntryPoint = "fRegresaCostoPromedio")]
        public static extern int fRegresaCostoPromedio(string aCodigoProducto, string aCodigoAlmacen, string aAnio, string aMes, string aDia,
            StringBuilder aCostoPromedio);

        [DllImport("MGWServicios.dll", EntryPoint = "fRegresaExistencia")]
        public static extern int fRegresaExistencia(string aCodigoProducto, string aCodigoAlmacen, string aAnio, string aMes, string aDia,
            ref double aExistencia);

        [DllImport("MGWServicios.dll", EntryPoint = "fRegresaExistenciaCaracteristicas")]
        public static extern int fRegresaExistenciaCaracteristicas(string aCodigoProducto, string aCodigoAlmacen, string aAnio, string aMes,
            string aDia, string aValorCaracteristica1, string aValorCaracteristica2, string aValorCaracteristica3, ref double aExistencia);

        [DllImport("MGWServicios.dll", EntryPoint = "fRegresaExistenciaLotePedimento")]
        public static extern int fRegresaExistenciaLotePedimento(string aCodigoProducto, string aCodigoAlmacen, string aPedimento, string aLote,
            ref double aExistencia);

        [DllImport("MGWServicios.dll", EntryPoint = "fRegresaIVACargo")]
        public static extern int fRegresaIVACargo(tLlaveDoc aLlaveDocto, double aNetoTasa15, double aNetoTasa10, double aNetoTasaCero,
            double aNetoTasaExcenta, double aNetoOtrasTasas, double aIVATasa15, double aIVATasa10, double aIVAOtrasTasas);

        [DllImport("MGWServicios.dll", EntryPoint = "fRegresaIVACargo_2010")]
        public static extern int fRegresaIVACargo_2010(tLlaveDoc aLlaveDocto, double aNetoTasa15, double aNetoTasa10, double aNetoTasa16,
            double aNetoTasa11, double aNetoTasaCero, double aNetoTasaExcenta, double aNetoOtrasTasas, double aIVATasa15, double aIVATasa10,
            double aIVATasa16, double aIVATasa11, double aIVAOtrasTasas);

        [DllImport("MGWServicios.dll", EntryPoint = "fRegresaIVACargoRet_2010")]
        public static extern int fRegresaIVACargoRet_2010(tLlaveDoc aLlaveDocto, double aNetoTasa15, double aNetoTasa10, double aNetoTasa16,
            double aNetoTasa11, double aNetoTasaCero, double aNetoTasaExcenta, double aNetoOtrasTasas, double aIVATasa15, double aIVATasa10,
            double aIVATasa16, double aIVATasa11, double aIVAOtrasTasas, double aRetIVA, double aRetI);

        [DllImport("MGWServicios.dll", EntryPoint = "fRegresaIVAPago")]
        public static extern int fRegresaIVAPago(tLlaveDoc aLlaveDocto, double aNetoTasa15, double aNetoTasa10, double aNetoTasaCero,
            double aNetoTasaExcenta, double aNetoOtrasTasas, double aIVATasa15, double aIVATasa10, double aIVAOtrasTasas);

        [DllImport("MGWServicios.dll", EntryPoint = "fRegresaIVAPago_2010")]
        public static extern int fRegresaIVAPago_2010(tLlaveDoc aLlaveDocto, double aNetoTasa15, double aNetoTasa10, double aNetoTasa16,
            double aNetoTasa11, double aNetoTasaCero, double aNetoTasaExcenta, double aNetoOtrasTasas, double aIVATasa15, double aIVATasa10,
            double aIVATasa16, double aIVATasa11, double aIVAOtrasTasas);

        [DllImport("MGWServicios.dll", EntryPoint = "fRegresaIVAPagoRet_2010")]
        public static extern int fRegresaIVAPagoRet_2010(tLlaveDoc aLlaveDocto, double aNetoTasa15, double aNetoTasa10, double aNetoTasa16,
            double aNetoTasa11, double aNetoTasaCero, double aNetoTasaExcenta, double aNetoOtrasTasas, double aIVATasa15, double aIVATasa10,
            double aIVATasa16, double aIVATasa11, double aIVAOtrasTasas, double aRetIVA, double aRetI);

        [DllImport("MGWServicios.dll", EntryPoint = "fRegresaPrecioVenta")]
        public static extern int fRegresaPrecioVenta(string aCodigoConcepto, string aCodigoCliente, string aCodigoProducto,
            StringBuilder aPrecioVenta);

        [DllImport("MGWServicios.dll", EntryPoint = "fRegresaUltimoCosto")]
        public static extern int fRegresaUltimoCosto(string aCodigoProducto, string aCodigoAlmacen, string aAnio, string aMes, string aDia,
            StringBuilder aUltimoCosto);

        [DllImport("MGWServicios.dll", EntryPoint = "fRegresPorcentajeImpuesto")]
        public static extern int fRegresPorcentajeImpuesto(int aIdConceptoDocumento, int aIdClienteProveedor, int aIdProducto,
            ref double aPorcentajeImpuesto);

        [DllImport("MGWServicios.dll", EntryPoint = "fSaldarDocumento")]
        public static extern int fSaldarDocumento(ref tLlaveDoc aDoctoaPagar, ref tLlaveDoc aDoctoPago, double aImporte, int aIdMoneda,
            string aFecha);

        [DllImport("MGWServicios.dll", EntryPoint = "fSaldarDocumento_Param")]
        public static extern int fSaldarDocumento_Param(string aCodConcepto_Pagar, string aSerie_Pagar, double aFolio_Pagar,
            string aCodConcepto_Pago, string aSerie_Pago, double aFolio_Pago, double aImporte, int aIdMoneda, string aFecha);

        [DllImport("MGWServicios.dll", EntryPoint = "fSaldarDocumentoCheqPAQ")]
        public static extern int fSaldarDocumentoCheqPAQ(tLlaveDoc aDoctoaPagar, tLlaveDoc aDoctoPago, double aImporte, int aIdMoneda,
            string aFecha, double aTipoCambioCheqPAQ);

        [DllImport("MGWServicios.dll", EntryPoint = "fSetDatoAgente")]
        public static extern int fSetDatoAgente(string aCampo, string aValor);

        [DllImport("MGWServicios.dll", EntryPoint = "fSetDatoAlmacen")]
        public static extern int fSetDatoAlmacen(string aCampo, string aValor);

        [DllImport("MGWServicios.dll", EntryPoint = "fSetDatoClasificacion")]
        public static extern int fSetDatoClasificacion(string aCampo, string aValor);

        [DllImport("MGWServicios.dll", EntryPoint = "fSetDatoConceptoDocto")]
        public static extern int fSetDatoConceptoDocto(string aCampo, string aValor);

        [DllImport("MGWServicios.dll", EntryPoint = "fSetDatoCteProv")]
        public static extern int fSetDatoCteProv(string aCampo, string aValor);

        [DllImport("MGWServicios.dll", EntryPoint = "fSetDatoDireccion")]
        public static extern int fSetDatoDireccion(string aCampo, string aValor);

        [DllImport("MGWServicios.dll", EntryPoint = "fSetDatoDocumento")]
        public static extern int fSetDatoDocumento(string aCampo, string aValor);

        [DllImport("MGWServicios.dll", EntryPoint = "fSetDatoMovimiento")]
        public static extern int fSetDatoMovimiento(string aCampo, string aValor);

        [DllImport("MGWServicios.dll", EntryPoint = "fSetDatoMovtoContable")]
        public static extern int fSetDatoMovtoContable(string aCampo, string aValor);

        [DllImport("MGW_SDK.dll", EntryPoint = "fSetDatoParametros")]
        public static extern int fSetDatoParametros(string aCampo, StringBuilder aValor);

        [DllImport("MGWServicios.dll", EntryPoint = "fSetDatoProducto")]
        public static extern int fSetDatoProducto(string aCampo, string aValor);

        [DllImport("MGWServicios.dll", EntryPoint = "fSetDatoUnidad")]
        public static extern int fSetDatoUnidad(string aCampo, string aValor);

        [DllImport("MGWServicios.dll", EntryPoint = "fSetDatoValorClasif")]
        public static extern int fSetDatoValorClasif(string aCampo, string aValor);

        [DllImport("MGWServicios.dll", EntryPoint = "fSetDescripcionProducto")]
        public static extern int fSetDescripcionProducto(string aCampo, string aValor);

        [DllImport("MGWServicios.dll", EntryPoint = "fSetFiltroDocumento")]
        public static extern int fSetFiltroDocumento(string aFechaInicio, string aFechaFin, string aCodigoConcepto, string aCodigoCteProv);

        [DllImport("MGWServicios.dll", EntryPoint = "fSetFiltroMovimiento")]
        public static extern int fSetFiltroMovimiento(int aIdDocumento);

        [DllImport("MGWServicios.dll", EntryPoint = "fSetModoImportacion")]
        public static extern void fSetModoImportacion(bool aImportacion);

        [DllImport("MGWServicios.dll", EntryPoint = "fSetNombrePAQ")]
        public static extern int fSetNombrePAQ(string aSistema);

        [DllImport("MGWServicios.dll", EntryPoint = "fSiguienteFolio")]
        public static extern int fSiguienteFolio(string aCodigoConcepto, StringBuilder aSerie, ref double aFolio);

        [DllImport("MGWServicios.dll", EntryPoint = "fTerminaSDK")]
        public static extern void fTerminaSDK();

        [DllImport("MGWServicios.dll", EntryPoint = "fTimbraComplementoXML")]
        public static extern int fTimbraComplementoXML(string aRutaXML, string aCodCOncepto, StringBuilder aUUID, string aRutaDDA,
            string aRutaResultado, string aPass, string aRutaFormato, int aComplemento);

        [DllImport("MGWServicios.dll", EntryPoint = "fTimbraNominaXML")]
        public static extern int fTimbraNominaXML(string aRutaXML, string aCodCOncepto, StringBuilder aUUID, string aRutaDDA,
            string aRutaResultado, string aPass, string aRutaFormato);

        [DllImport("MGWServicios.dll", EntryPoint = "fTimbraXML")]
        public static extern int fTimbraXML(string aRutaXML, string aCodCOncepto, StringBuilder aUUID, string aRutaDDA, string aRutaResultado,
            string aPass, string aRutaFormato);
    }

}
