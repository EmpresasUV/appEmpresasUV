<?php
define('ROOT_DIR', $_SERVER['DOCUMENT_ROOT'].'/appEmpresasUV/');
header('Content-Type: application/json; charset=UTF8');

    try {
        /* ***************************************************************************** */
        //$MyAlmacenSdk = new COM("ContPAQi.AlmacenSdk");
        //$MyAlmacenSdk->setNoCaja("1");
        //echo $MyAlmacenSdk->http_BuscarPorId("45");
        //echo $MyAlmacenSdk->http_BuscarPorCodigo("110");
        //echo $MyAlmacenSdk->http_BuscarTodos();
        /* ***************************************************************************** */
        $MyProductoSdk = new COM("ContPAQi.ProductoSdk");
        $MyProductoSdk->setNoCaja(1);
        echo $MyProductoSdk->http_BuscarPorCodigo("6005");
        //echo $MyProductoSdk->http_BuscarPorId(1343);
        //echo $MyProductoSdk->http_BuscarTodos();
        //echo $MyProductoSdk->http_ExistenciaAlmacen("5001", "111");
        //echo $MyProductoSdk->http_ExistenciaAlmacenCaracteristicas("6001", "110", "U", "A", "XS"); //Considerar abreviaturas de caracteristicas
        /* ***************************************************************************** */
        //$MyClienteSdk = new COM("ContPAQi.ClienteSdk");
        //$MyClienteSdk->setNoCaja(1);
        //echo $MyClienteSdk->http_BuscarPorCodigo("XAXX010101000");
        //echo $MyClienteSdk->http_BuscarPorId(3);
        //echo $MyClienteSdk->http_BuscarTodos();
        /* ***************************************************************************** */
        //$MyComercial = new COM("ContPAQi.MovimientoSdk");
        //$MyComercial = new COM("ContPAQi.DocumentoSdk");
        //$MyComercial = new COM("ContPAQi.ConceptoSdk");
        //$MyComercial = new COM("ContPAQi.CiComercialSdk");        





    } catch(Exception $e){
        echo 'Error: ' . $e->getMessage() , "\n\nTrace: " . $e->getTraceAsString(), "\n";
    }

?>