<?php
define('ROOT_DIR', $_SERVER['DOCUMENT_ROOT'].'/appEmpresasUV/');
require_once(ROOT_DIR . 'app/librerias/php/fpdf/fpdf.php');
require_once(ROOT_DIR . 'app/librerias/php/fpdi/autoload.php');
use setasign\Fpdi\Fpdi;
use setasign\Fpdi\PdfReader;


                                    //Generar el PDF, con el calendario
                                    $pdf = new Fpdi();

                                    $pageCount = $pdf->setSourceFile(ROOT_DIR . 'docs/becados/pdf_modelo.pdf');
                                    $pageId = $pdf->importPage(1, PdfReader\PageBoundaries::MEDIA_BOX);

                                    $pdf->addPage();
                                    $pdf->useImportedPage($pageId, 0, 0, 200);
                                    $pdf->SetMargins(30, 30, 30);
                                    //Asignando la fuente y color
                                    $pdf->SetFont('Helvetica', '','8');
                                    $Ytop = 57;

                                    $pdf->SetTextColor(0, 0, 0);
                                    ob_get_clean();
                                    //Escribiendo la MATRICULA DEL BECARIO
                                    $pdf->SetXY(30, $Ytop);
                                    $pdf->Write(30, utf8_decode("Tú matricula: "));
                                    $pdf->SetXY(80, $Ytop);
                                    $pdf->Write(30, utf8_decode("XXXXXXXXXXXXXXXXXX"));

                                    //Escribiendo el nombre del becario
                                    $Ytop += 5;
                                    $pdf->SetFont('Helvetica', '','8');
                                    $pdf->SetXY(30, $Ytop);
                                    $pdf->Write(30, utf8_decode("Tú nombre: "));
                                    $pdf->SetXY(80, $Ytop);
                                    $pdf->Write(30, utf8_decode("XXXXXXXX YYYYYYYYYYYYYY ZZZZZZZZZZZZZ WWWWWWWWWWW"));

                                    //Escribiendo el apellido MATERNO
                                    $Ytop += 5;
                                    $pdf->SetXY(30, $Ytop);
                                    $pdf->Write(30, utf8_decode("Alimentos programados: "));
                                    $pdf->SetXY(80, $Ytop);
                                    $pdf->Write(30, utf8_decode("000 Alimentos"));

                                    //Escribiendo el NOMBRE
                                    $Ytop += 5;
                                    $pdf->SetXY(30, $Ytop);
                                    $pdf->Write(30, utf8_decode("Área que gestiona tu beca: "));
                                    $pdf->SetXY(80, $Ytop);
                                    $pdf->Write(30, utf8_decode("XXXXXXXXX XXXXXXXXX XXXXXXXXXXX"));

                                    //Escribiendo el CORREO
                                    $Ytop += 5;
                                    $pdf->SetFont('Helvetica', '','8');
                                    $pdf->SetXY(30, $Ytop);
                                    $pdf->Write(30, utf8_decode("Tú correo de contacto: "));
                                    $pdf->SetXY(80, $Ytop);
                                    $pdf->Write(30, utf8_decode("XXXXXXXX@XXXXXXXXXX"));

                                    //Escribiendo el TELEFONO
                                    $Ytop += 5;
                                    $pdf->SetXY(30, $Ytop);
                                    $pdf->Write(30, utf8_decode("Comedor para entrega de alimentos: "));
                                    $pdf->SetXY(80, $Ytop);
                                    $pdf->Write(30, utf8_decode("XXXXXXXXXXX XXXXXXX"));


                                    $FDesayunos = "00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | ";

                                    //Escribiendo los días de DESAYUNO
                                    $Ytop += 40;
                                    $pdf->SetXY(30, $Ytop);
                                    $pdf->Write(5, utf8_decode($FDesayunos));


                                    $FComidas = "00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | 00/00/0000 | ";

                                    //Escribiendo los días de COMIDAS
                                    $Ytop += 60;
                                    $pdf->SetXY(30, $Ytop);
                                    $pdf->Write(5, utf8_decode($FComidas));


                                    $pdf->Output(ROOT_DIR . '/_calendario.pdf', 'F');








?>