<?php
define('ROOT_DIR', $_SERVER['DOCUMENT_ROOT'].'/appEmpresasUV/');
header('Content-Type: application/json; charset=UTF8');

    try {
        //$MyAlmacenSdk = new COM("ContPAQi.AlmacenSdk");
        //$MyAlmacenSdk->setNoCaja("1");
        //echo $MyAlmacenSdk->BuscarAlmacenPorId("45");
        //echo $MyAlmacenSdk->BuscarAlmacenPorCodigo("110");
        //echo $MyAlmacenSdk->BuscarTodosAlmacenes();



        //$MyComercial = new COM("ContPAQi.CiComercialSdk");
        //$MyComercial = new COM("ContPAQi.ProductoSdk");
        //$MyComercial = new COM("ContPAQi.MovimientoSdk");
        //$MyComercial = new COM("ContPAQi.DocumentoSdk");
        //$MyComercial = new COM("ContPAQi.ConceptoSdk");
        //$MyComercial = new COM("ContPAQi.ClienteSdk");
        //$MyComercial = new COM("ContPAQi.CaracteristicasSdk");





    } catch(Exception $e){
        echo 'Error: ' . $e->getMessage() , "\n\nTrace: " . $e->getTraceAsString(), "\n";
    }

?>