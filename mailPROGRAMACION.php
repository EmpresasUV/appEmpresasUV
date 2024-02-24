<?php
define('ROOT_DIR', $_SERVER['DOCUMENT_ROOT'].'/appEmpresasUV/');
define('SECURITY_PEPPER', 'c1isvFdxMDdmjOlvxpecFw');
require_once(ROOT_DIR . "app/librerias/php/phpmailer/src/PHPMailer.php");
require_once(ROOT_DIR . "app/librerias/php/phpmailer/src/SMTP.php");
require_once(ROOT_DIR . "app/librerias/php/phpmailer/src/Exception.php");
use PHPMailer\PHPMailer\PHPMailer;
use PHPMailer\PHPMailer\SMTP;
use PHPMailer\PHPMailer\Exception;
ini_set("max_execution_time", "900");
    try {

        $mysqli = new mysqli("localhost","root","Promo1002##","sistemas_empresasuv");

        // Check connection
        if ($mysqli -> connect_errno) {
          echo "Failed to connect to MySQL: " . $mysqli -> connect_error;
          exit();
        }      

        // Perform query
        if ($result = $mysqli -> query("SELECT * FROM becas_beneficiarios WHERE ESTADO='A' AND calendarizo=0")) {
            echo "Beneficiarios activos sin programación : " . $result -> num_rows . "<br><br>";
            $mail = new PHPMailer();
            $mail->CharSet = 'UTF-8';
            $mail->isSMTP();
            $mail->Host = 'mail.comedoresuv.com';
            $mail->SMTPAuth = true;
            $mail->Username = 'sistemas@comedoresuv.com';
            $mail->Password = 'wiOrM7if1EX3ypE';
            //$mail->SMTPSecure = 'ssl';
            $mail->Port = 587;                          
            $mail->SetFrom('becas@comedoresuv.com', 'Programa de becas de alimentos');
            $mail->AddReplyTo("becas@comedoresuv.com","Programa de becas de alimentos");
            //Copia oculta para
            $mail->addBCC('informatica@empresasuv.mx');
            $mail->Subject = "ÚLTIMO AVISO para la programación de tus alimentos (Comedores UV)";

            /* fetch associative array */
            while ($row = $result->fetch_assoc()) {
                echo "<br>Enviando mensaje a ".utf8_encode($row["nombre"])." ...   ";
                $mail->AddAddress($row["correo"], utf8_encode($row["nombre"]));

                $comb = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                $shfl = str_shuffle($comb);
                $pwd = substr($shfl, 0, 12);

                $pwd_peppered = hash_hmac("sha256", $pwd, SECURITY_PEPPER);
                $pwd_hashed = password_hash($pwd_peppered, PASSWORD_DEFAULT);
                $mysqli -> query("UPDATE becas_beneficiarios SET secreto='".$pwd_hashed."' WHERE matricula='".$row["matricula"]."'");

                $msg_mail = '
                <div style="width: 85%;margin-left: auto;margin-right: auto;">
                <p align="justify">Estimado alumno '.utf8_encode($row["nombre"]).',</p>
                <p align="justify">
                Es motivo de alegría para nosotros notificarte que tu beca de alimentos se encuentra aprobada e inscrita en el programa de becas de alimentos de los nuevos Comedores Universitarios.
                </p>
                
                <strong><h2>PASO 1</h2></strong>
                <p align="justify">
                Será necesario que nos ayudes a programar tus alimentos y de esta forma asegurar que estarán disponibles en tiempo y forma, de conformidad con tus horarios escolares; es por esta razón que te invitamos a hacer clic en la siguiente dirección, donde encontrarás un calendario en el cual podrás realizar esta programación.
                </p>
                <p align="justify">
                Es importante que recuerdes que solo podrás llenar el calendario en <u>una sola ocasión</u>; por lo que te pedimos amablemente que prestes mucha atención a las fechas y días en los que programarás tus alimentos.
                </p>
                <p align="justify">
                LOS DATOS DE ACCESO A TU CALENDARIO SON:
                <br>Portal: <a href="https://sistemas.empresasuv.mx/index.php?goExec=Becas&goAcc=registro" target="_blank">Calendario de alimentos</a>
                <br>Tu Matricula: <strong>'.$row["matricula"].'</strong>
                <br>La contraseña de acceso será: <strong>'.$pwd.'</strong>
                <br>Área que te gestiona tu beca: <strong>'.utf8_encode($row["dependencia"]).'</strong>
                </p>
                
                <strong><h2>PASO 2</h2></strong>
                <p align="justify">
                Una vez programados tus alimentos, deberás pasar a las oficinas del Fondo de Empresas de la Universidad Veracruzana A.C. para la programación de tus huellas dactilares, sus oficinas se encuentran en:
                </p>
                <p align="justify">
                <strong>Circuito Gonzalo Aguirre Beltrán S/N, Edificio del Centro de Estudios Universitarios, Mezanine, col. Zona Universitaria, Xalapa Ver.</strong>
                </p>
                <p align="justify">
                Este proceso se llevará a cabo <u>únicamente</u> los días <strong style="color:red;">Jueves 22 y Viernes 23 de febrero</strong>, en horario de <strong style="color:red;">9:00H a 14:00H y de 16:00H a 17:30H</strong>; agradecemos que organices tus actividades para asistir.
                </p>
                <p align="justify">
                Como referencias, nuestras oficinas se ubican arriba de la Tienda UV, a un costado de la Facultad de Contaduría y Administración (FCA), es un edificio de cristales.
                <br>También podrás preguntar por nuestras oficinas dentro de la Tienda UV.
                </p>
                <p align="justify">
                Si tienes alguna duda o problema, puedes escribirnos a: <a href="mailto:becas@comedoresuv.com">becas@comedoresuv.com</a>
                <br>O márcanos al 228 812 2099 Ext. 120 ó 130
                </p>
                <p align="justify">
                Muchas gracias por tu atención.
                <br>El equipo de Comedores Universitarios.
                </p>
                </div>
                ';

                $mail->Body = $msg_mail;
                $mail->IsHTML(true);

                if(!$mail->send()) {
                    echo 'Error...'.$mail->ErrorInfo;
                } else {
                    echo 'Enviado!...';
                    $mail->ClearAddresses();
                }
            }          

            // Free result set
            $result -> free_result();
        }        
        $mysqli -> close();        

    } catch(Exception $e){
        echo 'Error: ' . $e->getMessage() , "\n\nTrace: " . $e->getTraceAsString(), "\n";
    }

?>
