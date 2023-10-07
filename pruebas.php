<pre>
<?php
define('ROOT_DIR', $_SERVER['DOCUMENT_ROOT'].'/appEmpresasUV/');
define('NombrePaqComercial', 'CONTPAQ I COMERCIAL');
define('NombrePaqContable', 'CONTPAQ I CONTABILIDAD');

    try {
        $MySdk = new COM("SdkContPAQi.Comercial");

        $lResultado = $MySdk->test();
        echo $lResultado;

        $MySdk->fInicioSesionSDK("PUNTOVENTAS", "XiYeme=R6G");
        $MySdk->fInicioSesionSDKCONTPAQi("PUNTOVENTAS", "XiYeme=R6G");

        $nStart = $MySdk->fSetNombrePAQ(NombrePaqComercial);
        if ($nStart != 0)
        {
            $strMensaje = "";
            $MySdk->fError($nStart, $strMensaje, 512);
            echo("Error detectado: " . $strMensaje);
        }
        else
        {
            $rutaEMPRESA_COM = "C:\\Compac\\Empresas\\cmPuntoVentas";
            echo("Abriendo la empresa: cmPuntoVentas en la ruta: " . $rutaEMPRESA_COM);
            $nStart =  $MySdk->fAbreEmpresa($rutaEMPRESA_COM);
            if ($nStart != 0)
            {
                $strMensaje = "";
                 $MySdk->fError($nStart, $strMensaje, 512);
                echo("Error detectado: " . $strMensaje);
            }
        }

        $MySdk->fCierraEmpresa();
        echo("Empresa cerrada");
        $MySdk->fTerminaSDK();
        echo("SDK Finalizado");



    } catch(Exception $e){
        echo 'error: ' . $e->getMessage(), "\n";
    }

?>
</pre>