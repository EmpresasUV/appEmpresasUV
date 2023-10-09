<?php
define('ROOT_DIR', $_SERVER['DOCUMENT_ROOT'].'/appEmpresasUV/');
header('Content-Type: application/json; charset=UTF8');

    try {
        //$MyAlmacenSdk = new COM("ContPAQi.AlmacenSdk");
        //$MyAlmacenSdk->setNoCaja("1");
        //echo $MyAlmacenSdk->http_BuscarAlmacenPorId("45");
        //echo $MyAlmacenSdk->http_BuscarAlmacenPorCodigo("110");
        //echo $MyAlmacenSdk->http_BuscarTodosAlmacenes();

        $MyProductoSdk = new COM("ContPAQi.ProductoSdk");
        $MyProductoSdk->setNoCaja(1);
        //echo $MyProductoSdk->http_BuscarProductoPorCodigo("5001");
        //echo $MyProductoSdk->http_BuscarProductoPorId(1422);
        //echo $MyProductoSdk->http_BuscarTodosProductos();



        //$MyComercial = new COM("ContPAQi.MovimientoSdk");
        //$MyComercial = new COM("ContPAQi.DocumentoSdk");
        //$MyComercial = new COM("ContPAQi.ConceptoSdk");
        //$MyComercial = new COM("ContPAQi.ClienteSdk");
        //$MyComercial = new COM("ContPAQi.CaracteristicasSdk");
        //$MyComercial = new COM("ContPAQi.CiComercialSdk");        





    } catch(Exception $e){
        echo 'Error: ' . $e->getMessage() , "\n\nTrace: " . $e->getTraceAsString(), "\n";
    }

?>