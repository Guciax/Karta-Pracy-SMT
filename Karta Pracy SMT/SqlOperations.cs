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
    }
}
