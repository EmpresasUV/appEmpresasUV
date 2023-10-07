using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics;

namespace exceSdk
{
    public class MySQL
    {
        public EventLog eventLog = new EventLog();
        public MySQL()
        {
            try
            {
                if (!EventLog.SourceExists("Punto de ventas"))
                {
                    EventLog.CreateEventSource("Punto de ventas", "Punto de ventas");
                    eventLog.Source = "Punto de ventas";
                }
                else
                {
                    eventLog.Source = "Punto de ventas";
                }
            }
            catch
            {
                eventLog.Source = "Application";
            }
        }

        public string strCONECTION = "server=localhost;uid=root;pwd=Promo1002##;database=sistemas_empresasuv;";
        MySqlConnection sConectar = new MySqlConnection();
        public MySqlCommand MySQLComando = new MySqlCommand();
        public long LastID { get; set; }
        public MySqlConnection Conectar()
        {

            try
            {
                if (sConectar.State == System.Data.ConnectionState.Closed)
                {
                    sConectar.ConnectionString = strCONECTION;
                    sConectar.Open();
                    return sConectar;
                }
                else
                {
                    return sConectar;
                }

            }
            catch (MySqlException ex)
            {
                eventLog.WriteEntry(ex.Message, EventLogEntryType.Error, 1301, 1);
                Console.WriteLine("{\"Code\":"+ex.Code+", \"Message\":"+ex.Message+"}");
                return null;
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry(ex.Message, EventLogEntryType.Error, 1302, 1);
                Console.WriteLine("{\"Code\":404, \"Message\":" + ex.Message + "}");
                return null;
            }

        }
        public void Desconectar()
        {
            try
            {
                sConectar.Close();
            }
            catch (MySqlException ex)
            {
                eventLog.WriteEntry(ex.Message, EventLogEntryType.Error, 1303, 1);
                Console.WriteLine("{\"Code\":" + ex.Code + ", \"Message\":" + ex.Message + "}");
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry(ex.Message, EventLogEntryType.Error, 1304, 1);
                Console.WriteLine("{\"Code\":404, \"Message\":" + ex.Message + "}");
            }

        }
        public MySqlDataReader execSQL(string sSQLCommand)
        {
            try
            {
                //Console.WriteLine(sSQLCommand);
                var sCommand = sSQLCommand.Split(' ');
                if (sCommand[0].ToUpper() == "SELECT")
                {
                    MySqlCommand MyComando = new MySqlCommand(string.Format(sSQLCommand), Conectar());
                    MySqlDataReader MyLeector = MyComando.ExecuteReader();
                    return MyLeector;
                }
                else if (sCommand[0].ToUpper() == "INSERT")
                {
                    string pCierre = sSQLCommand.Substring(sSQLCommand.Length - 1, 1);
                    if (pCierre == ";") { sSQLCommand += " SELECT LAST_INSERT_ID() as LastID;"; }
                    else { sSQLCommand += "; SELECT LAST_INSERT_ID() as LastID;"; }
                    MySqlCommand sComando = new MySqlCommand(string.Format(sSQLCommand), Conectar());
                    MySqlDataReader IDreader = sComando.ExecuteReader();
                    if ((IDreader.HasRows) && (IDreader.Read()))
                    {
                        this.LastID = IDreader.GetInt64(0);
                    }
                    IDreader.Close();
                    return null;
                }
                else
                {
                    int sAfectados = 0;
                    MySqlCommand sComando = new MySqlCommand(string.Format(sSQLCommand), Conectar());
                    sAfectados = sComando.ExecuteNonQuery();
                    return null;
                }
            }
            catch (MySqlException ex)
            {
                eventLog.WriteEntry(ex.Message, EventLogEntryType.Error, 1305, 1);
                Console.WriteLine("{\"Code\":" + ex.Code + ", \"Message\":" + ex.Message + "}");
                return null;
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry(ex.Message, EventLogEntryType.Error, 1306, 1);
                Console.WriteLine("{\"Code\":404, \"Message\":" + ex.Message + "}");
                return null;
            }

        }
        public MySqlDataReader execSQL(string sSQLCommand, MySqlConnection xConetion)
        {
            try
            {
                var sCommand = sSQLCommand.Split(' ');
                if (sCommand[0].ToUpper() == "SELECT")
                {
                    MySqlCommand MyComando = new MySqlCommand(string.Format(sSQLCommand), xConetion);
                    MySqlDataReader MyLeector = MyComando.ExecuteReader();
                    return MyLeector;
                }
                else if (sCommand[0].ToUpper() == "INSERT")
                {
                    string pCierre = sSQLCommand.Substring(sSQLCommand.Length - 1, 1);
                    if (pCierre == ";") { sSQLCommand += " SELECT LAST_INSERT_ID() as LastID;"; }
                    else { sSQLCommand += "; SELECT LAST_INSERT_ID() as LastID;"; }
                    MySqlCommand sComando = new MySqlCommand(string.Format(sSQLCommand), Conectar());
                    MySqlDataReader IDreader = sComando.ExecuteReader();
                    if ((IDreader.HasRows) && (IDreader.Read()))
                    {
                        this.LastID = IDreader.GetInt64(0);
                    }
                    IDreader.Close();
                    return null;
                }
                else
                {
                    int sAfectados = 0;
                    MySqlCommand sComando = new MySqlCommand(string.Format(sSQLCommand), Conectar());
                    sAfectados = sComando.ExecuteNonQuery();
                    return null;
                }
            }
            catch (MySqlException ex)
            {
                eventLog.WriteEntry(ex.Message, EventLogEntryType.Error, 1307, 1);
                Console.WriteLine("{\"Code\":" + ex.Code + ", \"Message\":" + ex.Message + "}");
                return null;
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry(ex.Message, EventLogEntryType.Error, 1308, 1);
                Console.WriteLine("{\"Code\":404, \"Message\":" + ex.Message + "}");
                return null;
            }

        }
        public static string Encrypt(string encryptString)
        {
            string EncryptionKey = "6rINuNEGif4Pr69ispaMEHo#WiFOrochoCHLfRIjaCLvuKAVOkiCRI3u6a0lSt1z";  //we can change the code converstion key as per our requirement    
            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
                    0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptString;
        }
        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "6rINuNEGif4Pr69ispaMEHo#WiFOrochoCHLfRIjaCLvuKAVOkiCRI3u6a0lSt1z";  //we can change the code converstion key as per our requirement, but the decryption key should be same as encryption key    
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
                    0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }
}
