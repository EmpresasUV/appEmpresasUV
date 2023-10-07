using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Diagnostics;
using ARSoftware.Contpaqi.Comercial.Sdk;
using ARSoftware.Contpaqi.Comercial.Sdk.Constantes;
using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;
using ARSoftware.Contpaqi.Comercial.Sdk.Excepciones;
using ARSoftware.Contpaqi.Comercial.Sdk.Extensiones;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Microsoft.Win32;
using System.IO;
using System.Collections;
using Newtonsoft.Json;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Models;

namespace exceSdk
{
    public class Program
    {
 
        public static void Main(string[] args)
        {
            var VentasSdk = new VentasSdk();
            if (args.Length > 0)
            {
                var funcion = args[0].Trim().Split('/')[1];
                var parametros = args[1].Trim().Split('/')[1];
                switch (funcion)
                {
                    case "init_tpv":
                        var MyParametro = parametros.Split('$');
                        VentasSdk.init_tpv(MyParametro[1].Trim(), int.Parse(MyParametro[1]));
                        break;


                    default:
                        Console.WriteLine("{\"Code\": 400}");
                        break;
                }
            }
            else
            {
                Console.WriteLine("{\"Code\": 400}");
            }

        }

    }
}
