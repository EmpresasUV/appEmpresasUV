<pre>
<?php
    define('FILE_FOLDER',   getcwd().'/ICONOS');
    define('PATH_ICONOS',   FILE_FOLDER.'/icons');

    $SourceCode = "";

    // Incluir iconos SVG
    foreach(glob(PATH_ICONOS."/*.svg") as $file){
        $name_file = pathinfo($file, PATHINFO_FILENAME);
        //echo "Icono encontrado $name_file <br>";
$SourceCode .= "
.icon-$name_file{
    background:url('icons/$name_file.svg') no-repeat center center;
}
";
    }

    // Incluir iconos SVG
    foreach(glob(PATH_ICONOS."/*.png") as $file){
        $name_file = pathinfo($file, PATHINFO_FILENAME);
        //echo "Icono encontrado $name_file <br>";
$SourceCode .= "
.icon-$name_file{
    background:url('icons/$name_file.png') no-repeat center center;
}
";
    }


                $myFILE_NAME = "icons.css";
                echo "\nCreando el archivo de estilos: ".$myFILE_NAME;
                $myfile = fopen(FILE_FOLDER.'/'.$myFILE_NAME, "w") or die("\tError al generar el archivo!");
                fwrite($myfile, trim($SourceCode));
                fclose($myfile);


    echo $SourceCode;

?>
<pre>