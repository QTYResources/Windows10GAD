using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Collections.ObjectModel;

namespace WcfService1
{

    public class Service1 : IService1
    {
        public string HelloWCF()
        {
            return "Hello WCF";
        }

        /// <summary>
        /// 获取留言板数据
        /// </summary>
        /// <returns></returns>
        public List<string> GetMessage()
        {
            List<string> Messages = new List<string>();
            OleDbCommand cmd = new OleDbCommand();
            SQLExcute("SELECT * from about order by id desc", cmd);
            OleDbDataAdapter da = new OleDbDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0] != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Messages.Add(ds.Tables[0].Rows[i]["name"].ToString()+"|"+ds.Tables[0].Rows[i]["description"].ToString());
                }
            }
            return Messages;
        }

        /// <summary>
        /// 往留言板插入数据
        /// </summary>
        /// <param name="context"></param>
        public string InsertMessage(string name, string description)
        {
            try
            {
                string sql = "insert into about(name,description) values('" + name + "','" + description + "')";
                SQLExcute(sql);
                return "ok";
            }
            catch (Exception)
            {
                return "no";
            }
        }
        //SQL的操作   
        private void SQLExcute(string SQLCmd)
        {
            //请改成你的路径
            string ConnectionString = "PROVIDER=Microsoft.Jet.OLEDB.4.0;DATA SOURCE=D:\\code\\WcfService1\\WcfService1\\App_Data\\information.mdb";
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            conn.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            cmd.CommandTimeout = 15;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = SQLCmd;
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        //SQL的操作 是SQLExcute的重构
        private void SQLExcute(string SQLCmd, OleDbCommand Cmd)
        {
            //请改成你的路径
            string ConnectionString = "PROVIDER=Microsoft.Jet.OLEDB.4.0;DATA SOURCE=D:\\code\\WcfService1\\WcfService1\\App_Data\\information.mdb";
            OleDbCommand cmd = new OleDbCommand();
            OleDbConnection Conn = new OleDbConnection(ConnectionString);
            Conn.Open();
            Cmd.Connection = Conn;
            Cmd.CommandTimeout = 15;
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandText = SQLCmd;
            Cmd.ExecuteNonQuery();
            Conn.Close();
        }
    }
}
