using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karta_Pracy_SMT
{
    class SqlOperations
    {
        public static LotData GetLotData(string lotNo)
        {
            string model = "";
            int orderedQuantity = 0;
            string rankA = "";
            string rankB = "";

            DataTable sqlTable = new DataTable();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;";

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText =
                String.Format(@"SELECT Nr_Zlecenia_Produkcyjnego,NC12_wyrobu,Ilosc_wyrobu_zlecona,RankA,RankB FROM tb_Zlecenia_produkcyjne WHERE Nr_Zlecenia_Produkcyjnego ='{0}';", lotNo);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(sqlTable);

            if (sqlTable.Rows.Count>0)
            {
                model = sqlTable.Rows[0]["NC12_wyrobu"].ToString();
                int.TryParse(sqlTable.Rows[0]["Ilosc_wyrobu_zlecona"].ToString(), out orderedQuantity);
                rankA = sqlTable.Rows[0]["RankA"].ToString();
                rankB = sqlTable.Rows[0]["RankB"].ToString();
            }

            return new LotData(model, orderedQuantity, rankA, rankB);
        }

        public static ledReelData GetLedDataFromSparing(string qr)
        {
            string ledID = "";
            string nc12 = "";

            if (qr.Split('\t').Length > 5)
            {
                ledID = qr.Split('\t')[5];
                nc12 = qr.Split('\t')[0];
            }
            else
                return new ledReelData("error", 0, "error string", "error", "error", "error", "error");


            DataTable sqlTable = new DataTable();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=MSTMS010;Initial Catalog=ConnectToMSTDB;User Id=mes;Password=mes;";

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText =
            String.Format(@"SELECT  NC12,ID,Ilosc,LPN_ID,LPN_NC,ZlecenieString,RodzajKOMP FROM DaneBierzaceKompAktualne_FULL WHERE ID ='{0}' AND NC12='{1}';", ledID, nc12);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(sqlTable);

            nc12 = sqlTable.Rows[0]["NC12"].ToString();
            double ilosc = 0;
            if (!double.TryParse(sqlTable.Rows[0]["Ilosc"].ToString(), out ilosc)) return new ledReelData("error", 0, "error ilosc", "error", "error", "error", "error");
            string LPN_ID = sqlTable.Rows[0]["LPN_ID"].ToString();
            string LPN_NC = sqlTable.Rows[0]["LPN_NC"].ToString();
            string ZlecenieString = sqlTable.Rows[0]["ZlecenieString"].ToString();
            string rank = sqlTable.Rows[0]["RodzajKOMP"].ToString();

            return new ledReelData(nc12, ilosc, LPN_ID, LPN_NC, ZlecenieString, ledID, rank);
        }
    }
}
