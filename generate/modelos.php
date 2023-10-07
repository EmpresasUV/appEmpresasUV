<pre>
<?php
    #CONFIGURACION DE LOS PARAMETROS DE CONEXIÓN CON LA BASE DE DATOS
    define('DB_DRIVER',     "mysql");
    define('DB_HOST',       "localhost");
    define('DB_USUARIO',    "root");
    define('DB_SECRETO',    "Promo1002##");
    define('DB_NOMBRE',     "sistemas_empresasuv");
    define('DB_CATALOGO',   "information_schema");
    define('DB_CHARSET',    "utf8");
    define("DISABLE_FOREIGN_KEY_CHECKS", true);
    define('FILE_FOLDER',   getcwd().'/MODELOS');
    define('APP_FOLDER',  $_SERVER['DOCUMENT_ROOT'].'/appEmpresasUV/app/modelos');

    date_default_timezone_set("America/Mexico_City");

    $SourceCode = "";
    $MySQL = new mysqli(DB_HOST, DB_USUARIO, DB_SECRETO, DB_NOMBRE);
    if ($MySQL->connect_errno) {
        exit("Falló al conectar a MySQL: " . $MySQL->connect_error);
    }else{
        echo "Conectado a la base de datos.";
        echo "\n\n-----------------------------------------\n";
        #BUSCANDO LAS TABLAS DENTRO DE LA BASE DE DATOS
        $MySQL->select_db(DB_CATALOGO);
        if ($xTablas = $MySQL->query("SELECT * FROM TABLES WHERE TABLE_SCHEMA='".DB_NOMBRE."'")) {
            //echo "\nNúmero de tablas en la DB: " . $xTablas->num_rows;
            while($row = $xTablas->fetch_assoc()) {
                //echo "\n-> ".$row['TABLE_NAME'];
                $T_NAME = $row['TABLE_NAME'];
$SourceCode = "
<|?php
/**
* @autor Rixio Danilo Iguarán Chourio.
* @cédula-profesional 12196442
* @correo-eletrónico atencion.clientes@obringolfo.com
* @denominacion OBRAS INFORMÁTICAS DEL GOLFO
* @empresa OBRINGOLFO S.A.S. DE C.V.
* @proyecto  OBRINGOLFO.
* @copyright ".date("Y")."
*/

class ".ucwords($T_NAME)." extends ModeloBase{
    /* ***************************************************************************
    * Registrando las variables de la clase
    *************************************************************************** */
";

                if ($xCampos = $MySQL->query("SELECT * FROM COLUMNS WHERE TABLE_NAME='".$row['TABLE_NAME']."' AND TABLE_SCHEMA='".DB_NOMBRE."'")) {
                    //echo "\nNúmero de campos en " . $row['TABLE_NAME'] . ": " .$xCampos->num_rows;
                    $T_CAMPOS = array();
                    while($campo = $xCampos->fetch_assoc()) {
                        //echo "\n\t-> ".$campo['COLUMN_NAME'];
                        array_push($T_CAMPOS, $campo['COLUMN_NAME']);
$SourceCode .= "    private \$".$campo['COLUMN_NAME'].";\n";
                    }
$SourceCode .= "    private \$table;\n";
$SourceCode .= "
    /* ***************************************************************************
    * Generando los metodos SET y GET para las variables
    *************************************************************************** */
";
foreach($T_CAMPOS as $C){
$SourceCode .= "    /* Setters y Getters para la propiedad [ ".ucwords($C)." ] */
    public function get".ucwords($C)."(){ return \$this->".$C."; }
    public function set".ucwords($C)."($".$C."){ \$this->".$C." = $".$C."; }

";
}

$SourceCode .= "
    /* ***************************************************************************
    * @FUNCTION constructor de clase
    * @Param \$adapter
    * Inicializa y registra la clase dentro de la aplicación
    *************************************************************************** */
    public function __construct(\$adapter) {
        try {

            \$this->table = '".$T_NAME."';
            parent::__construct(\$this->table, \$adapter);

        } catch (Exception \$e) {
            mostrar_error(\$e);
        }
    }

    /* ***************************************************************************
    * OTROS MÉTODOS Y FUNCIONES PARA ÉSTE MODELO EN ESPECÍFICO
    *************************************************************************** */

    /** *********************************************************************** */
    /** *********************************************************************** */
    /** *********************************************************************** */
    /** *********************************************************************** */
}
?|>

";
                //echo FILE_FOLDER;
                if(!is_dir(FILE_FOLDER)){
                    mkdir(FILE_FOLDER);
                }

                $SourceCode = str_replace("|", "", $SourceCode);
                $myFILE_NAME = strtolower($T_NAME).".modelo";
                echo "\nCreando el archivo de clase: ".$myFILE_NAME;
                $myfile = fopen(FILE_FOLDER.'/'.$myFILE_NAME, "w") or die("\tError al generar el archivo!");
                fwrite($myfile, trim($SourceCode));
                fclose($myfile);





                }
                //echo $SourceCode;
            }
            // Free result set
            $xTablas->free_result();
            echo "\n\n-----------------------------------------\n";
            //* COPIANDO LOS ARCHIVOS CREADOS AL SISTEMA
            //borrando los modelos actuales de la app
            $files = glob(APP_FOLDER.'/*.modelo'); //obtenemos todos los nombres de los modelos
            foreach($files as $file){
                if(is_file($file)){
                    unlink($file); //elimino el fichero
                    echo "\nEliminando el modelo anterior: ".$file;
                }
            }
            echo "\n\n-----------------------------------------\n";
            //Moviendo los nuevos modelos a su destino
            $files = glob(FILE_FOLDER.'/*.modelo'); //obtenemos todos los nombres de los modelos
            foreach($files as $file){
                if(is_file($file)){
                    $moved = rename($file, APP_FOLDER."/".basename($file));
                    if($moved)
                    {
                        echo "\n".$file." Movido correctamente.";
                    }
                }
            }
            echo "\n\n-----------------------------------------\n";


        }


    }
    $MySQL->close();
?>
<pre>