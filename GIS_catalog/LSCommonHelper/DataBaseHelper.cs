using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace LSCommonHelper
{
    public class DataBaseHelper
    {
        // Fields
        public static string AccessConnectionString = "";
        public static string Connstring = "";
        public static string OracleConnectionString = "";

        // Methods
        public static string GetAccess2003ConnectionString(string sFilePath)
        {
            return ("Provider=Microsoft.JET.OLEDB.4.0;data source=" + sFilePath);
        }

        public static string GetAccess2007ConnectionString(string sFilePath)
        {
            return ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + sFilePath );
        }

        public static void getcmd(string sqlStr)
        {
            OleDbConnection connection = getConn();
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                OleDbCommand command = new OleDbCommand(sqlStr, connection);
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
                connection.Dispose();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
                connection = null;
            }
        }

        public static OleDbConnection getConn()
        {
            try
            {
                return new OleDbConnection(Connstring);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DataSet getDataSet(string sqlStr, string sTable)
        {
            DataSet set2;
            OleDbConnection selectConnection = getConn();
            try
            {
                //selectConnection.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(sqlStr, selectConnection);
                DataSet dataSet = new DataSet();
                if (dataSet.Tables.Contains(sTable))
                {
                    dataSet.Tables[sTable].Clear();
                }
                adapter.Fill(dataSet, sTable);
                set2 = dataSet;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                selectConnection.Close();
                selectConnection = null;
            }
            return set2;
        }

        public static DataRow GetOnlyRow(string sql, string sTable)
        {
            DataRow row = null;
            DataSet set = getDataSet(sql, sTable);
            if (set.Tables[sTable].Rows.Count > 0)
            {
                row = set.Tables[sTable].Rows[0];
            }
            return row;
        }

        public static string GetOnlyRowValue(string sql, string sTable)
        {
            string str = "";
            DataSet set = getDataSet(sql, sTable);
            if (set.Tables[sTable].Rows.Count > 0)
            {
                DataRow row = set.Tables[sTable].Rows[0];
                str = row[0].ToString();
            }
            return str;
        }

        public static string GetOracleBlobConnectionString(string sUser, string sPassword, string sServiceName)
        {
            return ("server=oratest;Data Source=" + sServiceName + ";User ID=" + sUser + ";Password=" + sPassword);
        }

        public static string GetOracleConnectionString(string sUser, string sPassword, string sServiceName)
        {
            return ("Provider=OraOLEDB.Oracle.1;Data Source=" + sServiceName + ";User ID=" + sUser + ";Password=" + sPassword);
        }
    }


}
