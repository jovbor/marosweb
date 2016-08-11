using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;

namespace Debottlenecking
{
    public class GetFlowTable
    {
        public static FlowTable GetTable()
        {
            using (var connection = new OleDbConnection())
            {
                string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:/Users/jovbor/Desktop/Debottlenecking/FPSO2.mdb;Persist Security Info=False;";
                connection.ConnectionString = connectionString;
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM DocNode WHERE isSource=TRUE";

                var reader = command.ExecuteReader();
                var listOfWells = new Wells();

                while (reader != null && reader.Read())
                {
                    //Reference to Flow Table
                    var NodeID = Convert.ToInt32(reader[2]);
                    var ID = NodeID - 1;

                    //Taking the list of wells with Name, NodeID, capacity, Flow ID
                    listOfWells.Add(new Well { Name = reader[1].ToString(), NodeID = Convert.ToInt32(reader[2]), Capacity = Convert.ToDouble(reader[6]), Units = reader[7].ToString(), FlowID = ID });

                }

                reader?.Close();

                int numberofWells = listOfWells.Count;

                //Taking the number wells to cut down the table 
                string ourString = "(";
                for (int i = 0; i < numberofWells; i++)
                {
                    ourString = $"{ourString}{i},";
                }
                ourString = ourString.TrimEnd(',');
                ourString = $"{ourString})";

                //Taking time periods for the flow table
                //Single product attribute at the moment Tableid=0
                var CommandDate = connection.CreateCommand();
                CommandDate.CommandText = "SELECT RowId,RowYear,RowMonth,RowDay FROM DocFlowTableRow WHERE TableId=0";
                var ReaderTime = CommandDate.ExecuteReader();

                //Creating Flow Table
                var flowTable = new FlowTable();

                //Populating the flow table with timing
                while (ReaderTime != null && ReaderTime.Read())
                {
                    flowTable.Add(new TimePeriod { RowId = Convert.ToInt32(ReaderTime[0]), MyDateTime = new DateTime(Convert.ToInt32(ReaderTime[1]), Convert.ToInt32(ReaderTime[2]), Convert.ToInt32(ReaderTime[3])) });
                }

                ReaderTime?.Close();

                //Taking production rates for the flow table
                //Single product attribute at the moment Tableid=0
                var CommandRate = connection.CreateCommand();
                CommandRate.CommandText = $"SELECT * FROM DocFlowTableValue WHERE Tableid=0 AND ColId in {ourString};";
                var ReaderProductionRate = CommandRate.ExecuteReader();

                //Populating the flow table with flow rates, NodeID and Name per Well
                while (ReaderProductionRate != null && ReaderProductionRate.Read())
                {
                    foreach (var time in flowTable)
                    {
                        if (time.RowId == Convert.ToInt32(ReaderProductionRate[1]))
                        {
                            time.WellRates.Add(new Rates { Rate = Convert.ToInt32(ReaderProductionRate[3]), NodeID = listOfWells.Find(x => x.FlowID == Convert.ToInt32(ReaderProductionRate[2])).NodeID, NodeName = listOfWells.Find(x => x.FlowID == Convert.ToInt32(ReaderProductionRate[2])).Name, });
                        }
                    }
                }
                ReaderProductionRate?.Close();
                connection.Close();
                return flowTable;

            }
        }
    }
}