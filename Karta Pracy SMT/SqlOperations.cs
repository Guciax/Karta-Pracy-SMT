using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karta_Pracy_SMT
{
    class SqlOperations
    {
        public static string formatRankString(string rawRank)
        {
            string[] rankSplitted = rawRank.Split('-');
            if (rankSplitted.Length >= 3)
            {
                return rankSplitted[0] + "-" + rankSplitted[1] + "-" + rankSplitted[2];
            }
            else
            {
                return rawRank;
            }
        }
        public static LotData GetLotData(string lotNo)
        {
            double lotDoubleCheck = 0;
            if (double.TryParse(lotNo, out lotDoubleCheck))
            {
                string model = "";
                int orderedQuantity = 0;
                string rankA = "";
                string rankB = "";

                DataTable sqlTableLot = new DataTable();
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = @"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;";

                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                command.CommandText =String.Format(@"SELECT Nr_Zlecenia_Produkcyjnego,NC12_wyrobu,Ilosc_wyrobu_zlecona,RankA,RankB FROM tb_Zlecenia_produkcyjne WHERE Nr_Zlecenia_Produkcyjnego = @Zlecenie;");
                command.Parameters.AddWithValue("@Zlecenie", lotNo);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(sqlTableLot);

                if (sqlTableLot.Rows.Count > 0)
                {
                    model = sqlTableLot.Rows[0]["NC12_wyrobu"].ToString().Replace("LLFML","");
                    int.TryParse(sqlTableLot.Rows[0]["Ilosc_wyrobu_zlecona"].ToString(), out orderedQuantity);
                    rankA = formatRankString(sqlTableLot.Rows[0]["RankA"].ToString());
                    rankB = formatRankString(sqlTableLot.Rows[0]["RankB"].ToString());
                }
                return new LotData(model, orderedQuantity, rankA, rankB);
            }
            else
                return new LotData("error", 0, "lot number", "");
        }

        public static Tuple<double,double,double> MaxRankQty(string model) //rankA, rankB, carrier
        {
            DataTable sqlTableModels = new DataTable();
            SqlConnection connModels = new SqlConnection();
            connModels.ConnectionString = @"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;";

            SqlCommand commandModels = new SqlCommand();
            commandModels.Connection = connModels;
            commandModels.CommandText = String.Format(@"SELECT MODEL_ID,A_PKG_QTY,B_PKG_QTY,SMT_Carrier_QTY FROM tb_MES_models WHERE MODEL_ID = @Model;");
            commandModels.Parameters.AddWithValue("@Model", "LLFML"+model);

            SqlDataAdapter adapterModel = new SqlDataAdapter(commandModels);
            adapterModel.Fill(sqlTableModels);

            double rankA = 0;
            double rankB = 0;
            double carrier = 0;
            double.TryParse(sqlTableModels.Rows[0]["A_PKG_QTY"].ToString(), out rankA);
            double.TryParse(sqlTableModels.Rows[0]["B_PKG_QTY"].ToString(), out rankB);
            double.TryParse(sqlTableModels.Rows[0]["SMT_Carrier_QTY"].ToString(), out carrier);

            return new Tuple<double, double, double>(rankA, rankB, carrier);

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
            command.CommandText = @"SELECT  NC12,ID,Ilosc,LPN_ID,LPN_NC,ZlecenieString,RodzajKOMP FROM DaneBierzaceKompAktualne_FULL WHERE ID = @ledId AND NC12= @nc12;"; 
            command.Parameters.AddWithValue("@ledId", ledID);
            command.Parameters.AddWithValue("@nc12", nc12);
            
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(sqlTable);
            if (sqlTable.Rows.Count > 0)
            {
                nc12 = sqlTable.Rows[0]["NC12"].ToString();
                double ilosc = 0;
                if (!double.TryParse(sqlTable.Rows[0]["Ilosc"].ToString(), out ilosc)) return new ledReelData("error", 0, "error ilosc", "error", "error", "error", "error");
                string LPN_ID = sqlTable.Rows[0]["LPN_ID"].ToString();
                string LPN_NC = sqlTable.Rows[0]["LPN_NC"].ToString();
                string ZlecenieString = sqlTable.Rows[0]["ZlecenieString"].ToString();
                string rank = formatRankString(sqlTable.Rows[0]["RodzajKOMP"].ToString());
                return new ledReelData(nc12, ilosc, LPN_ID, LPN_NC, ZlecenieString, ledID, rank);
            }
            else
            {
                MessageBox.Show("Nie odnaleziono rolki LED w bazie." + Environment.NewLine + $"12NC: {nc12}" + Environment.NewLine + $"ID: {ledID}");
                return new ledReelData("error", 0, "error", "error", "error", "error", "error");
            }
        }

        public static DataTable GetMesModels()
        {
            DataTable sqlTable = new DataTable();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;";

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = String.Format(@"SELECT MODEL_ID,A_PKG_QTY,B_PKG_QTY,CCT_CODE,SMT_Carrier_QTY FROM dbo.tb_MES_models");

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(sqlTable);

            foreach (DataRow row in sqlTable.Rows)
            {
                row["MODEL_ID"] = row["MODEL_ID"].ToString().Replace("LLFML", "");
            }

            return sqlTable;
        }

        public static void SaveRecordToDb(DateTime startDate, DateTime endDate,string smtLine,string operatorSMT,string lotNo,string model,string manufacturedQty,string ngQty,string scrapQty,string firstPieceCheck,string ledLefts, string stencil, string client, double totalLedsUsed )
        {
            bool release = true;

#if DEBUG
           //release = false;
#endif
            if (release)
            {
                using (SqlConnection openCon = new SqlConnection(@"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;"))
                {
                    string save = "INSERT into tb_SMT_Karta_Pracy (DataCzasStart,DataCzasKoniec,LiniaSMT,OperatorSMT,NrZlecenia,Model,IloscWykonana,NGIlosc,ScrapIlosc,Kontrola1szt,KoncowkiLED,StencilQR,Client,Zuzycie_Led) VALUES (@DataCzasStart,@DataCzasKoniec,@LiniaSMT,@OperatorSMT,@NrZlecenia,@Model,@IloscWykonana,@NGIlosc,@ScrapIlosc,@Kontrola1szt,@KoncowkiLED,@StencilQR,@Client,@Zuzycie_Led)";
                    using (SqlCommand querySave = new SqlCommand(save))
                    {
                        
                        querySave.Connection = openCon;
                        querySave.Parameters.Add("@DataCzasStart", SqlDbType.SmallDateTime).Value = startDate;
                        querySave.Parameters.Add("@DataCzasKoniec", SqlDbType.SmallDateTime).Value = endDate;
                        querySave.Parameters.Add("@LiniaSMT", SqlDbType.VarChar, 50).Value = smtLine;
                        querySave.Parameters.Add("@OperatorSMT", SqlDbType.VarChar, 50).Value = operatorSMT;
                        querySave.Parameters.Add("@NrZlecenia", SqlDbType.VarChar, 50).Value = lotNo;
                        querySave.Parameters.Add("@Model", SqlDbType.VarChar, 50).Value = model;
                        querySave.Parameters.Add("@IloscWykonana", SqlDbType.VarChar, 50).Value = manufacturedQty;
                        querySave.Parameters.Add("@NGIlosc", SqlDbType.VarChar, 50).Value = ngQty;
                        querySave.Parameters.Add("@ScrapIlosc", SqlDbType.VarChar, 50).Value = scrapQty;
                        querySave.Parameters.Add("@Kontrola1szt", SqlDbType.VarChar, 50).Value = firstPieceCheck;
                        querySave.Parameters.Add("@KoncowkiLED", SqlDbType.VarChar, 255).Value = ledLefts;
                        querySave.Parameters.Add("@Zuzycie_Led", SqlDbType.Int, 255).Value = totalLedsUsed;
                        querySave.Parameters.Add("@StencilQR", SqlDbType.VarChar, 255).Value = stencil;
                        querySave.Parameters.Add("@Client", SqlDbType.VarChar, 3).Value = client;
                        openCon.Open();
                        querySave.ExecuteNonQuery();
                    }
                }
            }
        }

        public static string[] GetOperatorsArray(int daysAgo)
        {
           DateTime untilDate = System.DateTime.Now.AddDays(daysAgo * (-1));
            DataTable sqlTable = new DataTable();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;";

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = String.Format(@"SELECT OperatorSMT,DataCzasKoniec FROM tb_SMT_Karta_Pracy Where DataCzasKoniec>@Data");
            command.Parameters.AddWithValue("@Data", untilDate.Date);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(sqlTable);

            HashSet<string> operators = new HashSet<string>();
            foreach (DataRow row in sqlTable.Rows)
            {
                operators.Add(row["OperatorSMT"].ToString().Trim());
            }

            return operators.OrderBy(o=>o).ToArray();
        }

        public static string[][] GetTechnicianInspector(int daysAgo)
        {
            DateTime untilDate = System.DateTime.Now.AddDays(daysAgo * (-1));
            DataTable sqlTable = new DataTable();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;";

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = String.Format(@"SELECT Kontrola1szt,DataCzasKoniec FROM tb_SMT_Karta_Pracy WHERE DataCzasKoniec >= @days ");
            command.Parameters.AddWithValue("@days", untilDate.Date);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(sqlTable);

            HashSet<string> technicians = new HashSet<string>();
            HashSet<string> inspectors = new HashSet<string>();
            foreach (DataRow row in sqlTable.Rows)
            {
                if (row["Kontrola1szt"].ToString().Contains(";"))
                {
                    string[] techInsp = row["Kontrola1szt"].ToString().Split(';');
                    if (techInsp.Count() > 1)
                    {
                        technicians.Add(techInsp[0]);
                        inspectors.Add(techInsp[1]);
                    }
                }
            }
            return new string[][] { technicians.OrderBy(o=>o).ToArray(), inspectors.OrderBy(o => o).ToArray() };
        }

        public static bool UpdateLedLeftovers(LedLeftovers ledLeft)
        {
            bool release = true;

            #if DEBUG
            release=false;
            #endif

            bool result = true;

            if (release)
            {
                try
                {
                    List<Tuple<string, string, string>> flatList = new List<Tuple<string, string, string>>();
                    foreach (var led in ledLeft.RankA)
                    {
                        flatList.Add(new Tuple<string, string, string>(led.Nc12, led.ID, led.QtyLeft.ToString()));
                    }
                    foreach (var led in ledLeft.RankB)
                    {
                        flatList.Add(new Tuple<string, string, string>(led.Nc12, led.ID, led.QtyLeft.ToString()));
                    }
                    string lotNo = ledLeft.LotNo;

                    using (SqlConnection conn = new SqlConnection(@"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;"))
                    {
                        conn.Open();
                        foreach (var led in flatList)
                        {
                            SqlCommand cmd = new SqlCommand("sp_Spg_DaneBierzaceKompAktualneFULL_UPD_ilosc", conn);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@NC12", led.Item1));
                            cmd.Parameters.Add(new SqlParameter("@ID", led.Item2));
                            cmd.Parameters.Add(new SqlParameter("@Ilosc", led.Item3));
                            //cmd.Parameters.Add(new SqlParameter("@ZlecenieString", lotNo);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    result = false;
                }
            }
            return result;
        }

        public static Dictionary<string, string[]> lotToRankABQty(string[] lots)
        {
            Dictionary<string, string[]> result = new Dictionary<string, string[]>();
            DataTable sqlTableLot = new DataTable();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;";

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            string querry = @"SELECT Nr_Zlecenia_Produkcyjnego,Ilosc_wyrobu_zlecona,RankA,RankB FROM tb_Zlecenia_produkcyjne WHERE";
            foreach (var lot in lots)
            {
                querry += " Nr_Zlecenia_Produkcyjnego = " + lot + " OR";
            }
            querry = querry.Remove(querry.Length - 3, 3);
            command.CommandText = querry;

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(sqlTableLot);


            foreach (DataRow row in sqlTableLot.Rows)
            {
                if (result.ContainsKey(row["Nr_Zlecenia_Produkcyjnego"].ToString())) continue;
                result.Add(row["Nr_Zlecenia_Produkcyjnego"].ToString(), new string[] { row["RankA"].ToString(), row["RankB"].ToString(), row["Ilosc_wyrobu_zlecona"].ToString() });
            }
            return result;
        }

        public static DataTable GetLgSmtRecordsFromDb(int recordsQty, string line)
        {
            DataTable result = new DataTable();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;";

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = String.Format(@"SELECT TOP 28 DataCzasStart,DataCzasKoniec,LiniaSMT,OperatorSMT,NrZlecenia,Model,IloscWykonana,NGIlosc,ScrapIlosc,Kontrola1szt,KoncowkiLED,StencilQR,Client FROM MES.dbo.tb_SMT_Karta_Pracy WHERE LiniaSMT = @smtLine AND (Client<>'MST' OR Client is null) ORDER BY DataCzasKoniec DESC;");
            command.Parameters.AddWithValue("@qty", recordsQty.ToString());
            command.Parameters.AddWithValue("@smtLine", line);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(result);
            Debug.WriteLine(line + " " + result.Rows.Count);

            DataView dv = result.DefaultView;
            dv.Sort = "DataCzasStart";
            //DataTable sortedDT = dv.ToTable();

            return dv.ToTable();
        }

        public static Dictionary<string, string> nc12ToModelDict (string[] nc12List)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            DataTable sqlTableLot = new DataTable();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=MSTMS010;Initial Catalog=ConnectToMSTDB;User Id=mes;Password=mes;";

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = @"SELECT NC12,Opis FROM v_Item WHERE";
            for(int i=0; i<nc12List.Length;i ++)//     (var nc12 in nc12List)
            {
                if (i > 0)
                {
                    command.CommandText += " OR";
                }

                    command.CommandText += " NC12=@nc12" + i;
                    command.Parameters.AddWithValue("@nc12" + i, nc12List[i]);
            }


            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(sqlTableLot);


            foreach (DataRow row in sqlTableLot.Rows)
            {
                string nc12 = row["NC12"].ToString();
                if (result.ContainsKey(nc12)) continue;
                string opis = row["Opis"].ToString();

                result.Add(nc12, opis);
            }
            return result;
        }

        public static DataTable GetMstSmtRecordsFromDb(int recordsQty, string line)
        {
            DataTable result = new DataTable();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;";

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = String.Format(@"SELECT TOP 28 DataCzasStart,DataCzasKoniec,LiniaSMT,OperatorSMT,NrZlecenia,Model,IloscWykonana,NGIlosc,ScrapIlosc,Kontrola1szt,KoncowkiLED,StencilQR,Client FROM MES.dbo.tb_SMT_Karta_Pracy WHERE LiniaSMT = @smtLine AND Client='MST' ORDER BY DataCzasKoniec DESC;");
            command.Parameters.AddWithValue("@qty", recordsQty.ToString());
            command.Parameters.AddWithValue("@smtLine", line);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(result);
            Debug.WriteLine(line + " " + result.Rows.Count);

            DataView dv = result.DefaultView;
            dv.Sort = "DataCzasStart";
            //DataTable sortedDT = dv.ToTable();

            return dv.ToTable();
        }

        public static DataTable GetSmtRecordsFromDbQuantityOnly(int daysAgo, string line)
        {
            DataTable result = new DataTable();
            DateTime untilDay = DateTime.Now.Date.AddDays(daysAgo * (-1)).AddHours(6);

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;";

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = String.Format(@"SELECT DataCzasKoniec,LiniaSMT,Model,NrZlecenia,IloscWykonana,OperatorSMT FROM MES.dbo.tb_SMT_Karta_Pracy WHERE LiniaSMT = @smtLine and DataCzasKoniec>@until order by [DataCzasKoniec];");
            //command.Parameters.AddWithValue("@qty", recordsQty);
            command.Parameters.AddWithValue("@smtLine", line);
            command.Parameters.AddWithValue("@until", untilDay);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(result);

            return result;
        }

        public static bool IsLotAlreadyInDb(string lotId)
        {
            DataTable result = new DataTable();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;";

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = String.Format(@"SELECT NrZlecenia FROM MES.dbo.tb_SMT_Karta_Pracy WHERE NrZlecenia = @lot;");
            command.Parameters.AddWithValue("@lot", lotId);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(result);

            return result.Rows.Count > 0;
        }

        internal static int GetLastRecordIdForLine(string smtLine)
        {
            int result = 0;
            string connectionString = @"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;";
            string query = @"SELECT TOP(1) id,LiniaSMT,DataCzasStart FROM MES.dbo.tb_SMT_Karta_Pracy WHERE LiniaSMT=@smtLine ORDER BY DataCzasStart DESC";


            using (SqlConnection conn = new SqlConnection(@"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;"))
            {
                using (var cmd = conn.CreateCommand())
                {
                    var parameter = cmd.CreateParameter();
                    parameter.ParameterName = "@smtLine";
                    parameter.Value = smtLine;
                    cmd.Parameters.Add(parameter);

                    cmd.Connection.ConnectionString = connectionString;
                    cmd.CommandText = query;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            int colIndex = rdr.GetOrdinal("id");
                            if (!rdr.IsDBNull(colIndex))
                            {
                                return rdr.GetInt32(colIndex);
                            }
                        }
                    }
                }
                return 0;
            }
        }

        internal static void UpdateCurrentMstOrderQuantity(int newQty, int recordId)
        {
                using (SqlConnection openCon = new SqlConnection(@"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;"))
                {
                    openCon.Open();

                    string updString = "UPDATE tb_SMT_Karta_Pracy SET IloscWykonana=@qty, DataCzasKoniec=@DataCzasKoniec WHERE id = @id";
                    using (SqlCommand querySave = new SqlCommand(updString))
                    {
                        querySave.Connection = openCon;
                        querySave.Parameters.AddWithValue("@qty", newQty);
                        querySave.Parameters.AddWithValue("@id", recordId);
                        querySave.Parameters.AddWithValue("@DataCzasKoniec", DateTime.Now);
                        querySave.ExecuteNonQuery();
                    }
                }
        }
    }
}
