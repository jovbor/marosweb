using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;

namespace Debottlenecking
{
    public class GetNodeInfo
    {
        public static Wells NodeInfo()
        {
            using (var connection = new OleDbConnection())
            {
                string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:/Users/jovbor/Desktop/Debottlenecking/FPSO2.mdb;Persist Security Info=False;";
                connection.ConnectionString = connectionString;
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM DocNode";

                var reader = command.ExecuteReader();

                var Nodes = new Wells();

                while (reader != null && reader.Read())
                {
                    //Reference to Flow Table
                    var NodeID = Convert.ToInt32(reader[2]);
                    var ID = NodeID - 1;

                    //Taking the list of wells with Name, NodeID, capacity, Flow ID
                    Nodes.Add(new Well { Name = reader[1].ToString(), NodeID = Convert.ToInt32(reader[2]), Capacity = Convert.ToDouble(reader[6]), Units = reader[7].ToString(), FlowID = ID });
                 
                }

                return Nodes;
            }
        }

    }
}