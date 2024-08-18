using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.JSInterop;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Net.Http;
using Microsoft.Data.SqlClient;
using System;
using System.Data.SqlClient;

namespace Projem2.Pages
{
    public class AsiBase : ComponentBase
    {
        public string Hastanin_Turu { get; set; }
        public string Hastanin_Cinsi { get; set; }
        public string Mikrocip_No { get; set; }
        public string Asi_Nedeni { get; set; }
        public string Sahibinin_ismi { get; set; }
        public string Sahibinin_soyismi { get; set; }
        public string Telefon_No { get; set; }
      public DateTime Randevu_Tarihi { get; set; } = new DateTime(2023, 1, 1);
  

        static string connectionString = "Server=localhost;Database=Veteriner_Data;Integrated Security=SSPI;TrustServerCertificate=true;";

        static string insertQuery = "INSERT INTO Asi_Randevu_Table (Hastanın_Turu,Hastanin_Cinsi,Mikrocip_No,Asi_Nedeni,Sahibinin_ismi,Sahibinin_Soyismi,Telefon_No,Randevu_Tarihi) VALUES (@Hastanin_Turu,@Hastanin_Cinsi, @Mikrocip_No, @Asi_Nedeni, @Sahibinin_ismi, @Sahibinin_soyismi, @Telefon_No, @Randevu_Tarihi)";
        static SqlConnection conn = new SqlConnection(connectionString);

        public async Task Kayıt()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Hastanin_Turu", Hastanin_Turu);
                        cmd.Parameters.AddWithValue("@Hastanin_Cinsi",Hastanin_Cinsi);
                        cmd.Parameters.AddWithValue("@Mikrocip_No",Mikrocip_No);
                        cmd.Parameters.AddWithValue("@Asi_Nedeni", Asi_Nedeni);
                        cmd.Parameters.AddWithValue("@Sahibinin_ismi", Sahibinin_ismi);
                        cmd.Parameters.AddWithValue("@Sahibinin_soyismi", Sahibinin_soyismi);
                        cmd.Parameters.AddWithValue("@Telefon_No", Telefon_No);
                        cmd.Parameters.AddWithValue("@Randevu_Tarihi", Randevu_Tarihi);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                Temizle();
                await Yenile();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Veritabanı işlem hatası: " + ex.Message);
            }
        }

        private void Temizle()
        {
            Hastanin_Turu = "";
            Hastanin_Cinsi = "";
            Mikrocip_No = "";
            Asi_Nedeni = "";
            Sahibinin_ismi = "";
            Sahibinin_soyismi = "";
            Telefon_No = "";
            Randevu_Tarihi = DateTime.Now;

        }

        private async Task Yenile()
        {
            await InvokeAsync(StateHasChanged);
        }
    }
}
