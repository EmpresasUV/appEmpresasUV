<?php
/**
 * @autor Rixio Danilo Iguarán Chourio.
 * @cédula-profesional 12196442
 * @correo-eletrónico atencion.clientes@obringolfo.com
 * @denominacion OBRAS INFORMÁTICAS DEL GOLFO
 * @empresa OBRINGOLFO S.A.S. DE C.V.
 * @proyecto  goPAC.
 * @copyright 2021
 */


    # CARGANDO LAS CONFIGURACIONES GLOBALES
    require_once $_SERVER['DOCUMENT_ROOT'].'/appEmpresasUV/app/configuraciones/globales.configuracion';
    # CARGANDO EL CONTROLADOR DE ERRORES DEL SISTEMA
    require_once PATH_NUCLEO.'/error.nucleo';
    $errores = new obError();
    # CARGANDO LA BASE PARA LOS CONTROLADORES
    require_once PATH_NUCLEO.'/controlador.nucleo';
    # CARGANDO LAS FUNCIONES DEL CONTROLADOR FRONTAL
    require_once PATH_NUCLEO.'/controlador.libreria';

    //Cargamos controladores y acciones
    if(isset($_GET["goExec"])){
        $controllerObj = goControlador($_GET["goExec"]);
        exeAccion($controllerObj);
    }else{
        $controllerObj = goControlador(CONTROLADOR_DEFECTO);
        exeAccion($controllerObj);
    }
?>