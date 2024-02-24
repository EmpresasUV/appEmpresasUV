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
                if ($result = $mysqli -> query("SELECT * FROM becas_beneficiarios WHERE ESTADO='A' AND (ISNULL(huella1) OR ISNULL(huella2))")) {
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
                        $mail->Subject = "ÚLTIMO AVISO para registrar tus huellas dactilares (Comedores UV)";

                        /* fetch associative array */
                        while ($row = $result->fetch_assoc()) {
                                echo "<br>Enviando mensaje a ".utf8_encode($row["nombre"])." ...   ";
                                $mail->AddAddress($row["correo"], utf8_encode($row["nombre"]));

                                $msg_mail = '
                                <div style="width: 85%;margin-left: auto;margin-right: auto;">
                                <p align="justify">Estimado alumno '.utf8_encode($row["nombre"]).',</p>
                                <p align="justify">
                                Es motivo de alegría para nosotros notificarte que tu beca de alimentos se encuentra aprobada e inscrita en el programa de becas de alimentos de los nuevos Comedores Universitarios.
                                </p>

                                <strong><h2>DEBES PASAR A REGISTRAR TUS HUELLAS DACTILARES</h2></strong>
                                <p align="justify">
                                Recuerda que deberás pasar a las oficinas del Fondo de Empresas de la Universidad Veracruzana A.C. para la programación de tus huellas dactilares, sus oficinas se encuentran en:
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
