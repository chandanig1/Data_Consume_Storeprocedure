using Data_consume_Storeprocedure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Data_consume_Storeprocedure.Controllers
{
    public class ValuesController : ApiController
    {
        string cs = ConfigurationManager.ConnectionStrings["dbc"].ConnectionString;
        Studentdata stdata = new Studentdata();
        // GET api/values
        public List<Studentdata> Get()
        {
            SqlConnection con = new SqlConnection(cs);
            SqlDataAdapter da = new SqlDataAdapter("spdataconsume", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@action", "Select1");
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Studentdata> stdata = new List<Studentdata>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Studentdata st = new Studentdata();
                    st.Id = Convert.ToInt32(dt.Rows[i]["ID"]);
                    st.Name = dt.Rows[i]["Name"].ToString();
                    st.Email = dt.Rows[i]["Email"].ToString();
                    st.Mobile = dt.Rows[i]["Mobile"].ToString();
                    st.Password = dt.Rows[i]["Password"].ToString();
                    st.Gender = dt.Rows[i]["Gender"].ToString();
                    stdata.Add(st);
                }
            }
            if (stdata.Count > 0)
            {
                return stdata;
            }
            else
            {
                return null;
            }
        }
        // GET api/values/5
        public Studentdata Get(int id)
        {
            SqlConnection con = new SqlConnection(cs);
            SqlDataAdapter da = new SqlDataAdapter("spdataconsume", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@action", "Select");
            da.SelectCommand.Parameters.AddWithValue("@Id", id);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Studentdata st = new Studentdata();
            if (dt.Rows.Count > 0)
            {
                st.Id = Convert.ToInt32(dt.Rows[0]["ID"]);
                st.Name = dt.Rows[0]["Name"].ToString();
                st.Email = dt.Rows[0]["Email"].ToString();
                st.Mobile = dt.Rows[0]["Mobile"].ToString();
                st.Password = dt.Rows[0]["Password"].ToString();
                st.Gender = dt.Rows[0]["Gender"].ToString();
            }
            if (st != null)
            {
                return st;
            }
            else
            {
                return null;
            }
        }


        // POST api/values
        public string Post(Studentdata stdata)
        {
            string msg = "";
            SqlConnection con = new SqlConnection(cs);
            if (stdata != null)
            {
                SqlCommand cmd = new SqlCommand("spdataconsume", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", "Insert");
                cmd.Parameters.AddWithValue("@Id", stdata.Id);
                cmd.Parameters.AddWithValue("@Name", stdata.Name);
                cmd.Parameters.AddWithValue("@Email", stdata.Email);
                cmd.Parameters.AddWithValue("@Mobile", stdata.Mobile);
                cmd.Parameters.AddWithValue("@Password", stdata.Password);
                cmd.Parameters.AddWithValue("@Gender", stdata.Gender);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i > 0)
                {
                    msg = "data has been inserted successfully";
                }
                else
                {
                    msg = "Faild";
                }
            }
            return msg;
        }

        // PUT api/values/5
        public string Put(int id, Studentdata std)
        {
            string msg = "";
            SqlConnection con = new SqlConnection(cs);
            if (std != null)
            {
                SqlCommand cmd = new SqlCommand("spdataconsume", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", "Update");
                cmd.Parameters.AddWithValue("@Id",id);
                cmd.Parameters.AddWithValue("@Name", std.Name);
                cmd.Parameters.AddWithValue("@Email", std.Email);
                cmd.Parameters.AddWithValue("@Mobile", std.Mobile);
                cmd.Parameters.AddWithValue("@Password", std.Password);
                cmd.Parameters.AddWithValue("@Gender", std.Gender);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i > 0)
                {
                    msg = "data has been updated successfully";
                }
                else
                {
                    msg = "Faild";
                }
            }
            return msg;
        }


        // DELETE api/values/5
        public string Delete(int id)
        {
            string msg = "";
            SqlConnection con = new SqlConnection(cs);
            if (stdata != null)
            {
                SqlCommand cmd = new SqlCommand("spdataconsume", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", "Update");
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i > 0)
                {
                    msg = "data has been Deleted successfully";
                }
                else
                {
                    msg = "Faild";
                }
            }

                return msg;
            
        }
    }
}


