using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace OtomatikMail
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string cs = "Data Source=DESKTOP-T11FMIO;Initial Catalog=master;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
            string sql = @"select * from Orders where OrderDate>=DATEADD(DAY,-11720,CONVERT(date,sysdatetime()))";
            SqlDataAdapter sda=new SqlDataAdapter(sql,cs);
            DataTable dt=new DataTable();
            sda.Fill(dt);

            string mailBody = "";
            foreach (DataRow dr in dt.Rows)
            {
                mailBody += dr["OrderDate"]+" " + dr["CustomerID"];
            }
            mailGonder(mailBody);
        }

        private static void mailGonder(string mailBody)
        {
            MailMessage eposta = new MailMessage();
            eposta.From = new MailAddress("nihatbaydag@gmail.com"); 
            eposta.To.Add("baydagnurhat31@gmail.com");
            eposta.Subject = "son siparişler";
            eposta.Body = mailBody;
            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new System.Net.NetworkCredential("nihatbaydag@gmail.com", "şifre");
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.Send(eposta);
        }
    }
}
