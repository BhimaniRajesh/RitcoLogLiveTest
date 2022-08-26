﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GUI_Fleet_Reports_TyreSale_Query : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        Response.Cache.SetExpires(DateTime.MinValue);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string FromDT = "", ToDT = "", RO = "", LO = "";
        string final = "";


        FromDT = DT.DateFrom.ToString("dd MMM yyyy");
        ToDT = DT.DateTo.ToString("dd MMM yyyy");

        final += "?FromDT=" + FromDT;
        final += "&ToDT=" + ToDT;
        final += "&RO=" + Fromlc.SelectedRO.Trim();
        final += "&LO=" + Fromlc.SelectedLocation.Trim();
        final += "&Status=" + ddlStatus.SelectedValue.Trim();

        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft= (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open('./Result.aspx" + final + "', null, 'height=700,width=900,status=yes,toolbar=no,scrollbars=yes,resizable=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'');", true);
    }
}