using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;

/// <summary>
/// Summary description for myData
/// </summary>
/// 

public class RepositoryBase
{
    private string cs;

    public RepositoryBase()
    {
        cs = WebConfigurationManager.ConnectionStrings["All"].ConnectionString;

    }

    /// <summary>
    /// ذخیره داده در دیتابیس
    /// </summary>
    public bool SetData(SqlCommand cmd)
    {
        SqlConnection con = new SqlConnection(cs);
        cmd.Connection = con;

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            con.Close();
        }

    }




    /// <summary>
    /// ذخیره داده در دیتابیس
    /// </summary>
    public bool SetData(string cmdText, CommandType cmdType, Dictionary<string, object> param)
    {
        SqlConnection con = new SqlConnection(cs);
        SqlCommand cmd = new SqlCommand(cmdText, con);
        cmd.CommandType = cmdType;

        foreach (var p in param)
            if (p.Value == null)
                cmd.Parameters.AddWithValue(p.Key, DBNull.Value);
            else
                cmd.Parameters.AddWithValue(p.Key, p.Value);

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            con.Close();
        }

    }




    /// <summary>
    /// خواندن داده از دیتابیس
    /// </summary>
    public DataSet GetData(SqlCommand cmd)
    {
        SqlConnection con = new SqlConnection(cs);
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        cmd.Connection = con;
        try
        {
            con.Open();
            da.Fill(ds);
            return ds;
        }
        catch
        {
            return null;
        }
        finally
        {
            con.Close();
        }
    }




    /// <summary>
    /// خواندن داده از دیتابیس
    /// </summary>
    /// <param name="cmdText">متن Cmd</param>
    /// <param name="cmdType">نوع cmd</param>
    /// <param name="param">پارامترهای ورودی</param>
    /// <returns></returns>
    public DataSet GetData(string cmdText, CommandType cmdType, Dictionary<string, object> param)
    {
        SqlConnection con = new SqlConnection(cs);
        SqlCommand cmd = new SqlCommand(cmdText, con);
        cmd.CommandType = cmdType;

        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        foreach (var p in param)
            if (p.Value == null)
                cmd.Parameters.AddWithValue(p.Key, DBNull.Value);
            else
                cmd.Parameters.AddWithValue(p.Key, p.Value);

        try
        {
            con.Open();
            da.Fill(ds);
            return ds;
        }
        catch
        {
            return null;
        }
        finally
        {
            con.Close();
        }

    }




    /// <summary>
    /// گرفتن آخرین آیدی درج شده در یک جدول 
    /// </summary>
    /// <param name="TableName"></param>
    /// <returns></returns>
    public string GetLastId(string TableName)
    {
        string id = "";
        SqlConnection con = new SqlConnection(cs);
        SqlCommand cmd = new SqlCommand("select IDENT_CURRENT('" + TableName + "')", con);
        cmd.CommandType = CommandType.Text;

        try
        {
            con.Open();
            id = cmd.ExecuteScalar().ToString();
            return id;
        }
        catch
        {
            return null;
        }
        finally
        {
            con.Close();
        }
    }




}

