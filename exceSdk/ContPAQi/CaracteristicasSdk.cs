using ARSoftware.Contpaqi.Comercial.Sdk;
using ARSoftware.Contpaqi.Comercial.Sdk.Constantes;
using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;
using ARSoftware.Contpaqi.Comercial.Sdk.Extensiones;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exceSdk
{
    public class CaracteristicasSdk
    {
        public SqlConnection SQLConexion = new SqlConnection(@"Data Source = DC-CONTABLE\COMPAC; Initial Catalog = cmPuntoVentas; User ID = sa; Password = Promo1002##");
        public int Id_Producto { get; set; }
        public string nameCaracteristica1 { get; set; }
        public string valorCaracteristica1 { get; set; }
        public bool existCaracteristica1 { get; set; }
        public string nameCaracteristica2 { get; set; }
        public string valorCaracteristica2 { get; set; }
        public bool existCaracteristica2 { get; set; }
        public string nameCaracteristica3 { get; set; }
        public string valorCaracteristica3 { get; set; }
        public bool existCaracteristica3 { get; set; }
        
        public Dictionary<string, string> listaCaracteristica1 = new Dictionary<string, string>();
        public Dictionary<string, string> listaCaracteristica2 = new Dictionary<string, string>();
        public Dictionary<string, string> listaCaracteristica3 = new Dictionary<string, string>();

         public CaracteristicasSdk(int Id_DB)
        {
            Id_Producto = Id_DB;
            existCaracteristica1 = false;
            existCaracteristica2 = false;
            existCaracteristica3 = false;

            var Car1Bd = new StringBuilder(3000);
            var Car2Bd = new StringBuilder(3000);
            var Car3Bd = new StringBuilder(3000);
            ComercialSdk.fLeeDatoProducto("CIDPADRECARACTERISTICA1", Car1Bd, 3000);
            ComercialSdk.fLeeDatoProducto("CIDPADRECARACTERISTICA2", Car2Bd, 3000);
            ComercialSdk.fLeeDatoProducto("CIDPADRECARACTERISTICA3", Car3Bd, 3000);

            //Analizando las caracteristicas 1
            if (double.Parse(Car1Bd.ToString()) > 0) //Tiene caracteristica 1
            {
                existCaracteristica1 = true;
                SQLConexion.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM admCaracteristicas WHERE CIDPADRECARACTERISTICA=@padre", SQLConexion);
                command.Parameters.AddWithValue("@padre", double.Parse(Car1Bd.ToString()));
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    nameCaracteristica1 = String.Format("{0}", reader["CNOMBRECARACTERISTICA"]);
                    var id_valor = reader["CIDPADRECARACTERISTICA"];
                    reader.Close();
                    SqlCommand cmd_CAR = new SqlCommand("SELECT * FROM admCaracteristicasValores WHERE CIDPADRECARACTERISTICA=@valor", SQLConexion);
                    cmd_CAR.Parameters.AddWithValue("@valor", id_valor.ToString());
                    SqlDataReader cmd_CARreader = cmd_CAR.ExecuteReader();
                    while (cmd_CARreader.Read())
                    {
                        string xCaracteristica = String.Format("{0}", cmd_CARreader["CVALORCARACTERISTICA"]);
                        string nCaracteristica = String.Format("{0}", cmd_CARreader["CNEMOCARACTERISTICA"]);
                        listaCaracteristica1.Add(xCaracteristica, nCaracteristica);
                    }
                }
                else
                {
                    nameCaracteristica1 = "";
                }
                SQLConexion.Close();
            }
            else
            {
                this.nameCaracteristica1 = "";
            }

            //Analizando las caracteristicas 2
            if (double.Parse(Car2Bd.ToString()) > 0) //Tiene caracteristica 2
            {
                existCaracteristica2 = true;
                SQLConexion.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM admCaracteristicas WHERE CIDPADRECARACTERISTICA=@padre", SQLConexion);
                command.Parameters.AddWithValue("@padre", double.Parse(Car2Bd.ToString()));
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    nameCaracteristica2 = String.Format("{0}", reader["CNOMBRECARACTERISTICA"]);
                    var id_valor = reader["CIDPADRECARACTERISTICA"];
                    reader.Close();
                    SqlCommand cmd_CAR = new SqlCommand("SELECT * FROM admCaracteristicasValores WHERE CIDPADRECARACTERISTICA=@valor", SQLConexion);
                    cmd_CAR.Parameters.AddWithValue("@valor", id_valor.ToString());
                    SqlDataReader cmd_CARreader = cmd_CAR.ExecuteReader();
                    while (cmd_CARreader.Read())
                    {
                        string xCaracteristica = String.Format("{0}", cmd_CARreader["CVALORCARACTERISTICA"]);
                        string nCaracteristica = String.Format("{0}", cmd_CARreader["CNEMOCARACTERISTICA"]);
                        listaCaracteristica2.Add(xCaracteristica, nCaracteristica);
                    }

                }
                else
                {
                    nameCaracteristica2 = "";
                }
                SQLConexion.Close();
            }
            else
            {
                this.nameCaracteristica2 = "";
            }


            //Analizando las caracteristicas 3
            if (double.Parse(Car3Bd.ToString()) > 0) //Tiene caracteristica 3
            {
                existCaracteristica3 = true;
                SQLConexion.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM admCaracteristicas WHERE CIDPADRECARACTERISTICA=@padre", SQLConexion);
                command.Parameters.AddWithValue("@padre", double.Parse(Car3Bd.ToString()));
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    nameCaracteristica3 = String.Format("{0}", reader["CNOMBRECARACTERISTICA"]);
                    var id_valor = reader["CIDPADRECARACTERISTICA"];
                    reader.Close();
                    SqlCommand cmd_CAR = new SqlCommand("SELECT * FROM admCaracteristicasValores WHERE CIDPADRECARACTERISTICA=@valor", SQLConexion);
                    cmd_CAR.Parameters.AddWithValue("@valor", id_valor.ToString());
                    SqlDataReader cmd_CARreader = cmd_CAR.ExecuteReader();
                    while (cmd_CARreader.Read())
                    {
                        string xCaracteristica = String.Format("{0}", cmd_CARreader["CVALORCARACTERISTICA"]);
                        string nCaracteristica = String.Format("{0}", cmd_CARreader["CNEMOCARACTERISTICA"]);
                        listaCaracteristica3.Add(xCaracteristica, nCaracteristica);
                    }

                }
                else
                {
                    nameCaracteristica3 = "";
                }
                SQLConexion.Close();
            }
            else
            {
                this.nameCaracteristica3 = "";
            }

        }
    }
}
