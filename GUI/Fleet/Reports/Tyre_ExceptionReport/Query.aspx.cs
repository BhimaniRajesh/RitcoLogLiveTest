﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class GUI_Fleet_Reports_Tyre_ExceptionReport_Query : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cache.SetExpires(DateTime.MinValue);

            BindDropDown("USP_Active_Vehicle", "VEHNO", "VEHNO_V", ddlVEHNO);
        }
    }
    private void BindDropDown(string strProc, string text, string value, DropDownList d)
    {
        SqlConnection conn = new SqlConnection(Session["SqlProvider"].ToString().Trim());
        string STR = strProc;
        conn.Open();
        SqlCommand cmd = new SqlCommand(STR, conn);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                d.Items.Add(new ListItem(Convert.ToString(dr[text]), Convert.ToString(dr[value])));
            }
        }
        dr.Close();
        conn.Close();
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string FromDT = "", ToDT = "", RO = "", LO = "";
        string final = "";


        FromDT = DT.DateFrom.ToString("dd MMM yyyy");
        ToDT = DT.DateTo.ToString("dd MMM yyyy");

        final += "?FromDT=" + FromDT;
        final += "&ToDT=" + ToDT;
        final += "&RO=" + Fromlc.SelectedRO;
        final += "&LO=" + Fromlc.SelectedLocation;
        final += "&Vehno=" + ddlVEHNO.SelectedValue.ToString().Trim();

        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft= (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open('./Result.aspx" + final + "', null, 'height=700,width=900,status=yes,toolbar=no,scrollbars=yes,resizable=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'');", true);

    }
}