using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.IO;
using Microsoft.ApplicationBlocks.Data;
using WebX.Controllers;
using WebX.Entity;

public partial class GUI_Operation_DocketEntry_Docket : System.Web.UI.Page
{

    MyFunctions fn;
    public string dkt_call = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        //Page.Theme 
        // GETTING OBJECT OF LIABRARY
        fn = new MyFunctions();

        // GETTING SQL CONNECTION AND OPENING IT
        try
        {
            // STORES NAME OF CLIENT, FOR CLIENT SPECIFIC SETTINGS
            hdnclient.Value = Session["client"].ToString().ToUpper();

            // NOMACLEDGER, NAME OF DOCKET IS DIFFERENT FOR EACH CLIENT, SO GETTING FROM SESSION
            dkt_call = Convert.ToString(Session["DocketCalledAs"]);

            // STORING NAME OF EMPLOYEE THAT ENTERES DOCKET
            hdnemployee.Value = Convert.ToString(Session["empcd"]);

            // NAME OF CURRENT LOCATION SELECTED BY USER,i.e. ORIGIN OF DOCKET
            lblorgnloc.Text = Convert.ToString(Session["brcd"]);
        }
        catch (Exception ex)
        {
            string exmess = ex.Message.ToString().Replace('\n', '_');
            Response.Redirect("../ErrorPage.aspx?heading=Session Expired&detail1=Session TimeOut Occured&detail2=" + exmess + "&suggestion2=Docket Entry");
        }


        // SOME OBJECT DECLARATION
        if (!IsPostBack)
        {
            // FINANCIAL YEAR TO BE PASSED AT ACCOUNTING METHODS
            hdnfinstartyear.Value = Session["finyear"].ToString();

            // DATE RULE
            hdnbookdaterule.Value = DocketController.GetBookDateRule();

            // SETTING PARCEL
            applyComputerization();
            hdnflagroundoff.Value = DocketRules.GetDefaultValue("FLAG_ROUND");
            /*****************************************************************************************************
             *      DOCKET ENTRY HAS CONTRACT SPECIFIC BEHAVIOR
             *      GETTING RULES FROM DATABASE
             *****************************************************************************************************/
            hdncontractsets.Value = DocketRules.GetDefaultValue("P01CONTRACT");
            hdncontractsets.Value = hdncontractsets.Value + "," + DocketRules.GetDefaultValue("P02CONTRACT");
            hdncontractsets.Value = hdncontractsets.Value + "," + DocketRules.GetDefaultValue("P03CONTRACT");

            // PAGE LOAD VISIBILITY SETTINGS
            txtpartycode.BackColor = System.Drawing.Color.FromName("#e6e6e6");
            divpartycsgne.Style["display"] = "none";
            btnpartypopup.Style["display"] = "none";

            hdnfoundcontract.Value = "W";

            trtransmode.Style["display"] = "none";
            trpickupdel.Style["display"] = "none";
            trpacktype.Style["display"] = "none";
            trvolumetric.Style["display"] = "none";
            trlocal.Style["display"] = "none";
            trdacc.Style["display"] = "none";
            trbtnstep2.Style["display"] = "none";
            lblsourcedockname.Style["display"] = "none";
            txtsourcedockno.Style["display"] = "none";
            imgsourcedock.Style["display"] = "none";
            trmultipoint.Style["display"] = "none";

            divmpsources.Style["display"] = "none";
            lblsourcedockname.Text = "Source" + hdndockname.Value;

            // ALSO JAVASCRIPT USES DOCKET NAME,EASY TO RETRIEVE IN JAVASCRIPT VIA HIDDENFIELD
            hdndockname.Value = dkt_call;

            // GETTING DATEBASE SERVER'S TODAYS'S DATE IN dd/MM/yyyy FORMAT
            hdnserverdate.Value = DataProvider.GetServerDate();
            txtdockdate.Text = hdnserverdate.Value;

            // GETTING DATEBASE SERVER'S TODAYS'S DATE IN dd/MM/yyyy FORMAT
            hdnserverhour.Value = DataProvider.GetServerHour();

            DocketController.BindMaster("PAYTYP", cmbpaybas);

            // DISABLING PARTYCODE FOR THE FIRST TIME
            txtpartycode.Enabled = false;


            /*************************************************************************************************
             *  RULE OF CITIES FROM MASTER
            *************************************************************************************************/
            string rule_from = "Y", rule_to = "Y";
            rule_from = DocketRules.GetDefaultValue("FCITY_MSTR");
            rule_to = DocketRules.GetDefaultValue("TCITY_MSTR");

            if (rule_from.CompareTo("Y") == 0)
                txtfrom.Attributes.Add("onblur", "javascript:return cityTest('from',this.getAttribute('id'))");
            else
                txtfrom.Attributes.Add("onblur", "this.value=this.value.toUpperCase()");

            if (rule_to.CompareTo("Y") == 0)
                txtto.Attributes.Add("onblur", "javascript:return cityTest('to',this.getAttribute('id'))");
            else
                txtto.Attributes.Add("onblur", "this.value=this.value.toUpperCase()");
            /*************************************************************************************************/



            /*******************************************************************************************
             *      CLIENT SPECIFIC AREA THAT MAKES DIFFERENT SETTINGS CLIENTS
             ******************************************************************************************/
            cmbratetype.DataSource = DocketController.BindMaster("RATETYPE", new DropDownList());
            cmbratetype.DataBind();

            txtdockno.Focus();

        } // IsPostBack ENDS HERE ------------------------------------------------------------------------>



    }
    // Page_Load ENDS HERE ------------------------------------------------------------------------>

    protected void btnstep1_Click(object sender, EventArgs e)
    {
        // VALIDATION OF VALUES THAT ARE ENERED AT STEP1
        string strval = DocketController.ValidateStep1(optentrytype.SelectedValue, txtdockno.Text, txtdockdate.Text, cmbpaybas.SelectedValue, txtpartycode.Text, lblorgnloc.Text, txtdelloc.Text, hdncontractsets.Value, hdnbookdaterule.Value);
        if (strval.CompareTo("TRUE") != 0)
        {
            Response.Redirect("../ErrorPage.aspx?heading=" + strval + "&detail1=Terminated From Step 1&suggestion1=Re-Enter " + dkt_call);
            return;
        }

        // STEP 1 VISIBILIY, DISABLING ALL CONTRACTS AT STEP1
        disableStep1();


        /*************************************************************************************************
        * FINDING CONTRACT FOR THE PARTY
        **************************************************************************************************/
        hdncontractid.Value = DocketController.GetContractID(txtpartycode.Text, cmbpaybas.SelectedValue, fn.Mydate1(txtdockdate.Text));

        if (hdncontractid.Value.CompareTo("") == 0)
        {
            string search = DocketRules.GetDefaultValue(cmbpaybas.SelectedValue + "CONTRACT", cmbpaybas.SelectedValue);
            string behave = DocketRules.GetDefaultValue(cmbpaybas.SelectedValue + "PROCEED", cmbpaybas.SelectedValue);
            if (behave.CompareTo("S") == 0 && search.CompareTo("C") == 0)
            {
                Response.Redirect("../ErrorPage.aspx?heading=Contract or Contract Services Not Found&suggesion1=Please Add or Complete Contract&suggesion2=Please Select another Party");
                return;
            }
            hdncontractid.Value = cmbpaybas.SelectedValue + "8888";
        }
        /*************************************************************************************************/


        /*************************************************************************************************
        * LOADING DATA AS PER CONTRACT(DEFAUL OR CUSTOMER WISE)
        **************************************************************************************************/
        ContractServices cser = DocketController.GetContractServices(hdncontractid.Value);

        if (cser.FlagFound == false)
        {
            Response.Redirect("../ErrorPage.aspx?heading=Contract Services not Found.&Suggestion=Please Complete Contract");
            return;
        }

        DocketController.BindTransMode(cser.FlagAir, cser.FlagRoad, cser.FlagTrain, cser.FlagExpress, cmbtransmode);
        DocketController.BindServiceType(cser.FlagSundry, cser.FlagFTL, cmbservicetype);
        DocketController.BindFTLType(hdncontractid.Value, cmbftltype);
        DocketController.BindPickUpDel(cser.PickUpDel, cmbpickupdel);

        chkcoddod.Enabled = cser.FlagCODDODEnable;
        chkdacc.Enabled = cser.FlagDACCEnable;
        chkvolumetric.Enabled = cser.FlagVolumetric;

        txtcubicrate.Text = cser.CFT2KG.ToString("F2");
        hdnvolmeasuretype.Value = cser.VolMeasureType;
        txtbilledat.Text = cser.BillingLoc;

        hdnfovtype.Value = cser.FOVType;
        hdnfovcharge.Value = cser.FOVCharge.ToString("F2");
        hdnfovownerrate.Value = cser.FOVOwnerRate.ToString("F2");
        hdnfovcarrierrate.Value = cser.FOVCarrierRate.ToString("F2");

        txtcoddodcharged.Text = cser.CODDODCharged.ToString("F2");
        txtdacccharged.Text = cser.DACCCharged.ToString("F2");
        /**************************************************************************************************/


        /***********************************************************************************************
         *  LOADING CONTROLS
        **********************************************************************************************/
        DocketController.BindMaster("PKGS", cmbpacktype);
        DocketController.BindMaster("PROD", cmbprodtype);
        DocketController.BindMaster("LOT", cmbloadtype);
        /**********************************************************************************************/

        if (optentrytype.SelectedValue.CompareTo("N") == 0)
            DocketController.SetExternalBusinessType("DKT", txtdockno.Text, cmbtransmode, cmbbusinesstype);
        else
            DocketController.SetExternalBusinessType("CMP", txtdockno.Text, cmbtransmode, cmbbusinesstype);


        Step1Settings(cser.RiskType);

    } // btnstep1_Click ENDS HERE



    private void Step1Settings(string risktype)
    {
        if (risktype.CompareTo("C") == 0)
        {
            optcarrierrisk.Checked = true;
            optownerrisk.Checked = false;
        }
        else
        {
            optcarrierrisk.Checked = false;
            optownerrisk.Checked = true;
        }

        hdnflagloadtype.Value = DocketRules.GetDefaultValue("FLAG_LOT");
        hdnflagmultipickup.Value = DocketRules.GetDefaultValue("FLAG_MULTI_PICKUPDEL");

        if (hdnflagmultipickup.Value.CompareTo("Y") == 0 && cmbpaybas.SelectedValue.CompareTo("P02") == 0)
            loadMPSources();
        else
            trmultipoint.Style["display"] = "none";

        if (hdnflagloadtype.Value.CompareTo("Y") == 0)
        {
            cmbloadtype.Style["display"] = "marker";
            lblloadtypename.Style["display"] = "marker";
        }

        if (hdnvolmeasuretype.Value.CompareTo("") == 0 || hdnvolmeasuretype.Value.CompareTo("-1") == 0)
        {
            hdnvolmeasuretype.Value = "INCHES";
            txtcubicrate.Text = "0";
        }


        // EXCEPTION FOR BILLED AT Billed At Location is not available then Enable text box
        SetBillingLoc();

        cmbtransmode.Focus();
    }



    private void SetBillingLoc()
    {
        string billlocrule = DocketRules.GetDefaultValue("BILL_LOC", cmbpaybas.SelectedValue);
        string billlocenable = DocketRules.GetEnableValue("BILL_LOC", cmbpaybas.SelectedValue);


        if (billlocrule.CompareTo("O") == 0)
        {
            txtbilledat.Text = lblorgnloc.Text.Trim();
        }
        else if (billlocrule.CompareTo("D") == 0)
        {
            txtbilledat.Text = txtdelloc.Text.Trim();
        }

        if (billlocenable.CompareTo("N") == 0)
            txtbilledat.Enabled = false;
        else
            txtbilledat.Enabled = true;
    }

    private void SetExternalBusinessType()
    {
        string dcr_business = "N", dcr_transmode = "N";
        dcr_business = DocketRules.GetDefaultValue("DCR_BUSINESS");
        dcr_transmode = DocketRules.GetDefaultValue("DCR_TRANSMODE");
        if (optentrytype.SelectedValue.CompareTo("N") == 0)
        {
            DocketController.BindMaster("BUT", cmbbusinesstype);
            if (dcr_business.CompareTo("Y") == 0)
            {
                DocketController.SelectDCRBusinessType(txtdockno.Text, cmbbusinesstype);
            }

            if (dcr_business.CompareTo("Y") == 0 && dcr_transmode.CompareTo("Y") == 0)
            {
                DocketController.BindDCRBusinessTransMode(cmbtransmode, cmbbusinesstype.SelectedValue);
            }

        }
        else if (optentrytype.SelectedValue.CompareTo("C") == 0)
        {
            DocketController.BindComputerisedBusinessType(cmbbusinesstype);
        }
    }

    protected void btnstep2_Click(object sender, EventArgs e)
    {
        // VALIDATION OF VALUES WHICH WERE ENTERED IN STEP 2

        string strval = DocketController.ValidateStep2(lblorgnloc.Text, txtdelloc.Text, cmbtransmode.SelectedValue, cmbservicetype.SelectedValue, cmbftltype.SelectedValue, txtfrom.Text, txtto.Text, cmbpickupdel.SelectedValue, cmbprodtype.SelectedValue, cmbpacktype.SelectedValue, cmbbusinesstype.SelectedValue, cmbloadtype.SelectedValue, ((chkmultidelivery.Checked == true || chkmultipickup.Checked) == true ? true : false), txtsourcedockno.Text);
        if (strval.CompareTo("TRUE") != 0)
        {
            Response.Redirect("../ErrorPage.aspx?heading=" + strval + "&detail1=Terminated From Step 2&suggestion1=Re-Enter " + dkt_call);
            return;
        }


		if (cmbservicetype.SelectedValue=="0" || cmbservicetype.SelectedValue=="")
        {
            Response.Redirect("../ErrorPage.aspx?heading=Invalid Service Type Entered&detail1=Terminated From Step 2&suggestion1=Re-Enter " + dkt_call);
            return;
        }

		if (cmbpacktype.SelectedValue=="0" || cmbpacktype.SelectedValue=="")
        {
            Response.Redirect("../ErrorPage.aspx?heading=Invalid Packaging Type Entered&detail1=Terminated From Step 2&suggestion1=Re-Enter " + dkt_call);
            return;
        }

		if (cmbprodtype.SelectedValue=="0" || cmbprodtype.SelectedValue=="")
        {
            Response.Redirect("../ErrorPage.aspx?heading=Invalid Product Type Entered&detail1=Terminated From Step 2&suggestion1=Re-Enter " + dkt_call);
            return;
        }

        // VISIBILITY AT STEP 2
        disableStep2();

        /*************************************************************************************************
        *  RULE OF CONSIGNOR-CONSIGNEE FROM MASTER
        **************************************************************************************************/
        hdnflagmstrcsgn.Value = DocketRules.GetDefaultValue("CSGN_MSTR", cmbpaybas.SelectedValue);
        hdnflagmstrcsge.Value = DocketRules.GetDefaultValue("CSGE_MSTR", cmbpaybas.SelectedValue);
        applyMSTRCSGNERule(hdnflagmstrcsgn.Value, hdnflagmstrcsge.Value);
        /*************************************************************************************************/


        Step2Settings();



        /*****************************************************************************************************
        * STATE WISE DOCUMENT DETAILS
        *****************************************************************************************************/
        DocketController.GetDocumentDetails(txtfrom.Text, txtto.Text, lblorgnloc.Text, txtdelloc.Text, grvdoc);
        /*****************************************************************************************************/


        /********************************************************************************************************
         *  SETTING SERVICE TAX PAYER
        ********************************************************************************************************/
        setStaxPaidBy();
        /********************************************************************************************************/


        // SETTING FOCUS, HELPS USER WHO USE ONLY KEY BOARD
        txtcsgncd.Focus();


    } // btnstep2_Click ENDS HERE

    private void Step2Settings()
    {
        // THIS FUNCTION CHECKS WHERE THE TOCITY IS ODA OR NOT
        chkoda.Checked = DocketController.GetODAFlag(txtto.Text);

        // To Show and Hide Cubic Feet Panel
        if (chkvolumetric.Checked == true)
            trcubicfeet.Style["display"] = "marker";
        else
            trcubicfeet.Style["display"] = "none";


        // LOADING INVOICE GRID
        DataTable dt = new DataTable();
        dt.Columns.Add("srno", typeof(string));
        dt.Rows.Add();
        foreach (DataRow dtr in dt.Rows)
        {
            dtr["srno"] = "1";
        }

        grvinvoice.DataSource = dt;
        grvinvoice.DataBind();

        if (chkvolumetric.Checked == true)
            txtcubicrate.Focus();
        else
            txtinvoicerows.Focus();

        // CLIENT SPECIFIC SETTINGS
        string flag_dopi = "N";
        flag_dopi = DocketRules.GetDefaultValue("FLAG_DOPI");
        // WHETHER DOPI NO IS IN SYTEM OR NOT
        if (flag_dopi.CompareTo("Y") == 0)
        {
            lbldopinoname.Style["display"] = "marker";
            txtdopino.Style["display"] = "marker";
        }
        if (flag_dopi.CompareTo("Y") == 0)
        {
            trentrysheetno.Style["display"] = "marker";
        }

        if (hdnflagmultipickup.Value.CompareTo("Y") == 0)
        {
            if (cmbservicetype.SelectedValue.CompareTo("2") == 0)
            {
                trmultipoint.Style["display"] = "block";
            }

            if (chkmultidelivery.Checked == true || chkmultidelivery.Checked == true)
            {
                lblsourcedockname.Style["display"] = "block";
                txtsourcedockno.Style["display"] = "block";
                imgsourcedock.Style["display"] = "block";
            }
        }

        // BY DEFAULT CUSTOMER REFERENCE NO IS DOCKET NUMBER
        txtcustrefno.Text = txtdockno.Text;
    }

    // INVOICE FUNCTIONS STARTS HERE
    protected void grvinvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (chkvolumetric.Checked == false)
            {
                e.Row.Cells[3].Style["display"] = "none"; // Length
                e.Row.Cells[4].Style["display"] = "none"; // Breadth
                e.Row.Cells[5].Style["display"] = "none"; // Height
                e.Row.Cells[8].Style["display"] = "none"; // Total
            }
            else
            {
                Label lbllength = (Label)e.Row.FindControl("lbllength");
                Label lblbreadth = (Label)e.Row.FindControl("lblbreadth");
                Label lblheight = (Label)e.Row.FindControl("lblheight");

                if (hdnvolmeasuretype.Value.CompareTo("CM") == 0)
                {
                    lbllength.Text = "Length(in CMs)";
                    lblbreadth.Text = "Breadth(in CMs)";
                    lblheight.Text = "Height(in CMs)";
                }
                else
                {
                    lbllength.Text = "Length(in Inches)";
                    lblbreadth.Text = "Breadth(in Inches)";
                    lblheight.Text = "Height(in Inches)";
                }
            }
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {

            string s = "cal.select(document.aspnetForm." + ((TextBox)e.Row.FindControl("txtinvoicedate")).ClientID.ToString() + ",'" + ((LinkButton)e.Row.FindControl("lnkinvoicedate")).ClientID.ToString() + "','dd/MM/yyyy'); return false;";
            ((LinkButton)e.Row.FindControl("lnkinvoicedate")).Attributes.Add("onclick", s);


            if (chkvolumetric.Checked == false)
            {
                e.Row.Cells[3].Style["display"] = "none"; // Length
                e.Row.Cells[4].Style["display"] = "none"; // Breadth
                e.Row.Cells[5].Style["display"] = "none"; // Height
                e.Row.Cells[8].Style["display"] = "none"; // Total
            }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            if (chkvolumetric.Checked == false)
            {
                e.Row.Cells[3].Style["display"] = "none"; // Length
                e.Row.Cells[4].Style["display"] = "none"; // Breadth
                e.Row.Cells[5].Style["display"] = "none"; // Height
                e.Row.Cells[8].Style["display"] = "none"; // Total
            }
        }

    }

    protected void btnaddrows_click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("SRNO", typeof(string));
        int rows = Convert.ToInt16(txtinvoicerows.Text);
        if (chkvolumetric.Checked == true)
            trcubicfeet.Style["display"] = "marker";
        else
            trcubicfeet.Style["display"] = "none";
        dt.Rows.Clear();
        for (int i = 0; i < rows; i++)
        {
            dt.Rows.Add();
        }

        int j = 0;
        foreach (DataRow dtr in dt.Rows)
        {
            dtr["srno"] = ++j;
        }
        grvinvoice.DataSource = dt;
        grvinvoice.DataBind();
    }

    protected void btnpayment_Click(object sender, EventArgs e)
    {
        disableStep3();
        disableStep4();

        divpayment.Style["display"] = "none";
        divcharges.Style["display"] = "marker";

        // VALIDATION OF CONSIGNOR CONSIGNEE DEATILS AND INVOICE DEATAILS
        string[] arrcsgn = new string[7];
        string[] arrcsge = new string[7];

        arrcsgn[0] = txtcsgncd.Text;
        arrcsgn[1] = txtcsgnname.Text;
        arrcsgn[2] = txtcsgnaddress.Text;
        arrcsgn[3] = txtcsgncity.Text;
        arrcsgn[4] = txtcsgnpin.Text;
        arrcsgn[5] = txtcsgnphone.Text;
        arrcsgn[6] = txtcsgnemail.Text;

        arrcsge[0] = txtcsgecd.Text;
        arrcsge[1] = txtcsgename.Text;
        arrcsge[2] = txtcsgeaddress.Text;
        arrcsge[3] = txtcsgecity.Text;
        arrcsge[4] = txtcsgepin.Text;
        arrcsge[5] = txtcsgephone.Text;
        arrcsge[6] = txtcsgeemail.Text;


        //RITCO DOESN'T WANT TO HAVE FIELDS MANDATORY
        string strval = "";
        if (hdnclient.Value.CompareTo("RITCO") == 0)
            strval = DocketController.ValidateStep3(false, false, arrcsgn, arrcsge, WebXConvert.ToInt64(txttotchargedweight.Text), WebXConvert.ToInt64(hdntotnopkgs.Value), WebXConvert.ToInt64(hdntotdeclval.Value), grvinvoice);
        else
            strval = DocketController.ValidateStep3(false, arrcsgn, arrcsge, WebXConvert.ToInt64(txttotchargedweight.Text), WebXConvert.ToInt64(hdntotnopkgs.Value), WebXConvert.ToInt64(hdntotdeclval.Value), grvinvoice);
        if (strval.CompareTo("TRUE") != 0)
        {
            Response.Redirect("../ErrorPage.aspx?heading=" + strval + "&detail1=Terminated From Step 3&suggestion1=Re-Enter " + dkt_call);
            return;

        }
        /*****************************************************************************************************
        *  RETRIEVING BASED ON CHARGE RULE
        ******************************************************************************************************/
        hdnchargerule.Value = DocketRules.GetDefaultValue("CHRG_RULE");

        if (hdnchargerule.Value.CompareTo("NONE") == 0)
            hdnbasecode1.Value = "NONE";
        else if (hdnchargerule.Value.CompareTo("SVCTYP") == 0)
            hdnbasecode1.Value = cmbservicetype.SelectedValue;
        else if (hdnchargerule.Value.CompareTo("BUT") == 0)
            hdnbasecode1.Value = cmbbusinesstype.SelectedValue;

        hdnchargesubrule.Value = ChargeMasterController.GetChargeSubRule("DKT", hdnchargerule.Value, hdnbasecode1.Value);
        if (hdnchargesubrule.Value.CompareTo("PROD") == 0)
            hdnbasecode2.Value = cmbprodtype.SelectedValue;
        else if (hdnchargesubrule.Value.CompareTo("NONE") == 0)
            hdnbasecode2.Value = "NONE";
        /******************************************************************************************************/

        applyCODDODSetting();
        applyDACCSetting();


        hdncontractdepth.Value = DocketRules.GetDefaultValue(cmbpaybas.SelectedValue + "DEPTH", cmbpaybas.SelectedValue);
        string flagproceed = DocketRules.GetDefaultValue(cmbpaybas.SelectedValue + "PROCEED", cmbpaybas.SelectedValue);


        ContractKeys ckeys = new ContractKeys();
        ckeys.BasedOn1 = hdnchargerule.Value;
        ckeys.BaseCode1 = hdnbasecode1.Value;
        ckeys.BasedOn2 = hdnchargesubrule.Value;
        ckeys.BaseCode2 = hdnbasecode2.Value;
        ckeys.ChargedWeight = returnDouble(txttotchargedweight.Text);
        ckeys.ContractID = hdncontractid.Value;
        ckeys.DelLoc = txtdelloc.Text;
        ckeys.Depth = hdncontractdepth.Value;
        ckeys.FlagProceed = flagproceed;
        ckeys.FromCity = txtfrom.Text;
        ckeys.FTLType = cmbftltype.SelectedValue;
        ckeys.NoOfPkgs = returnDouble(hdntotnopkgs.Value);
        ckeys.OrgnLoc = lblorgnloc.Text;
        ckeys.PayBase = cmbpaybas.SelectedValue;
        ckeys.ServiceType = cmbservicetype.SelectedValue;
        ckeys.ToCity = txtto.Text;
        ckeys.TransMode = cmbtransmode.SelectedValue;
        DocketController.ExecuteFreightContract(ckeys);

        hdnfoundcontract.Value = ckeys.FoundContract;

        txtfreightcharge.Text = ckeys.FreightCharge.ToString("F2");
        txtfreightrate.Text = ckeys.FreightRate.ToString("F2");
        try
        {
            cmbratetype.SelectedValue = ckeys.RateType;
        }
        catch (Exception ex) { }

        hdndbfreightrate.Value = ckeys.FreightRate.ToString("F2");
        hdntrdays.Value = ckeys.TRDays.ToString();

        hdnminfreighttype.Value = ckeys.MinFreightType;
        hdnminfreightbas.Value = ckeys.MinFreightBase;
        hdnminfreightbasrate.Value = ckeys.MinFreightBaseRate.ToString("F2");

        hdnminfreightrate.Value = ckeys.MinFreightRate.ToString("F2");
        hdnlowlimfreightrate.Value = ckeys.FreightRateLowerLimit.ToString("F2");
        hdnupplimfreightrate.Value = ckeys.FreightRateUpperLimit.ToString("F2");

        hdnminsubtotal.Value = ckeys.MinSubTotal.ToString("F2");
        hdnlowlimsubtotal.Value = ckeys.SubTotalLowerLimit.ToString("F2");
        hdnupplimsubtotal.Value = ckeys.SubTotalUpperLimit.ToString("F2");

        hdnflagminfreightper.Value = ckeys.FlagFreight;
        hdnflagminsubtotalper.Value = ckeys.FlagSutotal;


        if (ckeys.FlagProceed.CompareTo("NIL_CONTRACT") == 0)
        {
            Response.Redirect("../ErrorPage.aspx?heading=" + ckeys.Description + " &detail1=Strict Contract Validation&detail2=&suggestion2=Enter Valid Contract");
            return;
        }
        else if (ckeys.FlagProceed.CompareTo("EXC_CNT_LOC_FRT") == 0)
        {
            Response.Redirect("../ErrorPage.aspx?heading=Problem in Location Wise Customer Contract retrieval&detail1=" + ckeys.Description + "&detail2=&suggestion2=Enter Valid Contract");
            return;
        }
        else if (ckeys.FlagProceed.CompareTo("NIL_CNT_LOC_FRT") == 0)
        {
            Response.Redirect("../ErrorPage.aspx?heading=Location to Location Combination not Found in Customer Contract&detail1=" + ckeys.Description + "&detail2=&suggestion2=Enter Valid Contract");
            return;
        }
        else if (ckeys.FlagProceed.CompareTo("EXC_DEF_LOC_FRT") == 0)
        {
            Response.Redirect("../ErrorPage.aspx?heading=Problem in Location to Location Wise Default Contract Retrieval&detail1=" + ckeys.Description + "&detail2=&suggestion2=Enter Valid Contract");
            return;
        }
        else if (ckeys.FlagProceed.CompareTo("NIL_DEF_LOC_FRT") == 0)
        {
            Response.Redirect("../ErrorPage.aspx?heading=Location to Location Combination not in Default Contract Found&detail1=" + ckeys.Description + "&detail2=&suggestion2=Enter Valid Contract");
            return;
        }
        else if (ckeys.FlagProceed.CompareTo("EXC_MIN_FRT") == 0)
        {
            Response.Redirect("../ErrorPage.aspx?heading=Problem in Minimum Freight Finding&detail1=" + ckeys.Description + "&detail2=&suggestion2=Enter Valid Contract");
            return;
        }


        if (flagproceed.CompareTo("T") == 0)
        {
            if (chkmultipickup.Checked == false && chkmultidelivery.Checked == false)
            {
                if (returnDouble(txtfreightcharge.Text) == 0 || returnDouble(txtfreightcharge.Text) == 0)
                {
                    Response.Redirect("../ErrorPage.aspx?heading=Contract Freight Charge is not Found. Can't Enter " + dkt_call + " &detail1=Strict Contract Validation&detail2=&suggestion2=Enter Valid Contract");
                    return;
                }
            }
        }

        /************************************************************************************************
       *   APPLYING YIELD RATE i.e. MOST IMPORTANT FOR MINIMUM FREIGHT RATE FROM CONTRACT
       ************************************************************************************************/
        hdnyieldsubtotalrate.Value = ckeys.FreightRate.ToString("F2");
        if (hdncontractdepth.Value.CompareTo("M") == 0)
        {
            checkFreightLimits();
        }
        /***********************************************************************************************/


        /********************************************************************************************************
        *  CALCULATE FOV CHARGE FROM DELCARED VALUE AND CONTRACT
        ********************************************************************************************************/

        FOVTable fovtbl = new FOVTable();
        fovtbl.FlagFOV = "N";
        fovtbl.ChargeRule = hdnchargerule.Value;
        fovtbl.BaseCode = hdnbasecode1.Value;
        fovtbl.RiskType = (optownerrisk.Checked == true ? "O" : "C");
        fovtbl.FOVType = hdnfovtype.Value;
        fovtbl.DeclareValue = returnDouble(hdntotdeclval.Value);
        fovtbl.OwenerRate = returnDouble(hdnfovownerrate.Value);
        fovtbl.CarrierRate = returnDouble(hdnfovownerrate.Value);
        fovtbl.FOVRate = 0;
        fovtbl.FOVCharge = returnDouble(hdnfovcharge.Value);

        fovtbl = DocketController.BindFOVCharge(fovtbl);

        if (fovtbl.FlagFOV.CompareTo("Y") == 0)
        {
            txtfovrate.Text = fovtbl.FOVRate.ToString("F2");
            txtfovcalculated.Text = fovtbl.FOVCharge.ToString("F2");
            txtfovcharged.Text = fovtbl.FOVCharge.ToString("F2");
        }
        else
        {
            trfov.Style["display"] = "none";
            txtfovcharged.Text = "0.00";
            txtfovcalculated.Text = "0.00";
            txtfovrate.Text = "0.00";
        }
        /********************************************************************************************************/




        /********************************************************************************************************
        *  CALCULATE PRORATA RATE AND  SETTING FREIGHT
        ********************************************************************************************************/
        hdnflagprorata.Value = DocketRules.GetDefaultValue("FLAG_PRORATA");
        if (hdnflagprorata.Value.CompareTo("Y") == 0 && ckeys.RateType.CompareTo("F") == 0)
        {
            double freightcharge, maxcapacity, chargedweight;
            freightcharge = DocketController.ToDouble(txtfreightcharge.Text);
            maxcapacity = DocketController.GetFTLMaxCapacity(cmbftltype.SelectedValue);
            chargedweight = DocketController.ToDouble(txttotchargedweight.Text);
            hdnftlmaxcapacity.Value = maxcapacity.ToString("F2");
            if (cmbpaybas.SelectedValue.CompareTo("P02") == 0 && cmbservicetype.SelectedValue.CompareTo("2") == 0)
            {
                freightcharge = freightcharge + DocketController.GetProRataCharge(freightcharge, chargedweight, maxcapacity);
                txtfreightcharge.Text = freightcharge.ToString("F2");
            }
        }
        /********************************************************************************************************/



        /********************************************************************************************************
        *      FILLING OTHER CHARGES TABLE
        ********************************************************************************************************/
        DataTable dt = new DataTable();
        dt = DocketController.GetChargeTable(hdnchargerule.Value, hdnbasecode1.Value, cmbpaybas.SelectedValue);
        dt = DocketController.GetContractFlatOtherCharges(hdnfoundcontract.Value, hdnchargerule.Value, hdnbasecode1.Value, "NONE", "NONE", hdncontractid.Value, dt);

        if (dt == null)
        {
            Response.Redirect("../ErrorPage.aspx?heading=Critical Error.Problem in Charge Filling from DataBase &detail1=&detail2=&suggestion2=Docket Entry");
            return;
        }

        GetOtherCharges(hdnchargerule.Value, hdnbasecode1.Value, hdnchargesubrule.Value, hdnbasecode2.Value, dt);
        /*******************************************************************************************************/




        /*****************************************************************************************************
        *      CALCULATING EDD RULE WISE
        *****************************************************************************************************/
        string flag_transit = DocketRules.GetDefaultValue("EDD_TRANSIT");
        if (flag_transit.CompareTo("C") == 0)
        {
            // BY DEFAULT TAKEN FROM CUSTOMER CONTRACT MASTER
            // SPACE LEFT FOR FUTURE CODE
        }
        else if (flag_transit.CompareTo("CT") == 0)
        {
            if (hdntrdays.Value.CompareTo("0") == 0 || hdntrdays.Value.CompareTo("") == 0)
            {
                hdntrdays.Value = DocketController.GetTRDaysTransitZoneMaster(lblorgnloc.Text, txtdelloc.Text, cmbtransmode.SelectedValue).ToString();
                if (hdntrdays.Value.CompareTo("0") == 0 || hdntrdays.Value.CompareTo("") == 0)
                {

                    if (hdncontractdepth.Value.CompareTo("R") == 0)
                    {
                        hdncontractdepth.Value = DocketController.GetTRDaysTransitZoneMaster(lblorgnloc.Text, txtdelloc.Text, cmbtransmode.SelectedValue).ToString();
                    }
                }
            }
        }
        else if (flag_transit.CompareTo("T") == 0)
        {
            hdntrdays.Value = DocketController.GetTRDaysTransitZoneMaster(lblorgnloc.Text, txtdelloc.Text, cmbtransmode.SelectedValue).ToString();
            if (hdntrdays.Value.CompareTo("0") == 0 || hdntrdays.Value.CompareTo("") == 0)
            {
                if (hdncontractdepth.Value.CompareTo("R") == 0)
                {
                    hdncontractdepth.Value = DocketController.GetTRDaysTransitZoneMaster(lblorgnloc.Text, txtdelloc.Text, cmbtransmode.SelectedValue).ToString();
                }
            }
        }

        int ndays = 0;
        try
        {
            ndays = Convert.ToInt16(DocketRules.GetDefaultValue("EDD_NDAYS"));
        }
        catch (Exception ex) { ndays = 0; }
        ndays = ndays + DocketController.ToInteger(hdntrdays.Value);

        txtedd.Text = returnEDD(ndays.ToString());
        /***************************************************************************************************/

        DocketController.SetManualEntry(txtfreightcharge, txtfreightrate, cmbratetype, txtbilledat, txtedd, returnDouble(txtfreightcharge.Text), returnDouble(txtfreightrate.Text), cmbpaybas.SelectedValue);
        try
        {
            cmbstaxpaidby.SelectedValue = DocketController.GetStaxPayer(hdncontractid.Value);
        }
        catch (Exception ex) { }


        CalculateTotal();


        if (hdntotnopkgs.Value.CompareTo("") == 0 || WebXConvert.ToInt64(hdntotnopkgs.Value) == 0)
        {
            Response.Redirect("../ErrorPage.aspx?heading=Critical Error. No of Packages are Zero&detail1=Programming Error&detail2=" + "&suggestion2=Docket Entry");
        }

        if (hdntotdeclval.Value.CompareTo("") == 0 || WebXConvert.ToInt64(hdntotdeclval.Value) == 0)
        {
            Response.Redirect("../ErrorPage.aspx?heading=Critical Error. Total Declared value is Zero&detail1=Programming Error&detail2=" + "&suggestion2=Docket Entry");
        }

        if (txttotchargedweight.Text.CompareTo("") == 0 || WebXConvert.ToInt64(txttotchargedweight.Text) == 0)
        {
            Response.Redirect("../ErrorPage.aspx?heading=Critical Error. Total Charged Weight is Zero&detail1=Programming Error&detail2=" + "&suggestion2=Docket Entry");
        }


        // FOR PAID DOCKET PAYMENT IS ALREADY DONE SO BILLED AT NOT MANDATORY
        if (cmbpaybas.SelectedValue.CompareTo("P01") == 0 || cmbpaybas.SelectedValue.CompareTo("P03") == 0)
        {
            lblbilledat.Style["display"] = "none";
            txtbilledat.Style["display"] = "none";
        }

        if (chkservicetaxexemp.Checked == true)
        {
            txtservicetax.Enabled = true;
            txteducess.Enabled = true;
            txtheducess.Enabled = true;
        }
        else
        {
            txtservicetax.Enabled = false;
            txteducess.Enabled = false;
            txtheducess.Enabled = false;
        }


        if (cmbpaybas.SelectedValue.CompareTo("P04") == 0)
        {
            btnsubmit_Click(sender, e);
        }

        /// Focusing to Control
        if (cmbratetype.Enabled == true)
            cmbratetype.Focus();
        else if (cmbstaxpaidby.Enabled == true)
            cmbstaxpaidby.Focus();

    }

    private void CalculateTotal()
    {
        /////////////////////////////////// MAKING DOCKET TOTAL /////////////////////////////////
        double subtotal = 0.00, dockettotal = 0.00;
        // ADDING FREIGHT, FOV, COD/DOD, DACC INTO SUBTOTAL
        subtotal = subtotal + returnDouble(txtfreightcharge.Text);

        // FOV charged
        subtotal = subtotal + returnDouble(txtfovcharged.Text);

        if (chkcoddod.Checked == true)
            subtotal = subtotal + returnDouble(txtcoddodcharged.Text);

        if (chkdacc.Checked == true)
            subtotal = subtotal + returnDouble(txtdacccharged.Text);


        // NOW ADDING STRAIGHT CHARGES TO SUBTOTAL

        foreach (GridViewRow grv in grvcharges.Rows)
        {
            TextBox txtcharge1 = (TextBox)grv.FindControl("txtcharge1");
            TextBox txtcharge2 = (TextBox)grv.FindControl("txtcharge2");
            HiddenField hdnchargecode1 = (HiddenField)grv.FindControl("hdnchargecode1");
            HiddenField hdnchargecode2 = (HiddenField)grv.FindControl("hdnchargecode2");

            HiddenField hdnoperator1 = (HiddenField)grv.FindControl("hdnoperator1");
            HiddenField hdnoperator2 = (HiddenField)grv.FindControl("hdnoperator2");

            if (hdnoperator1.Value.CompareTo("+") == 0)
                subtotal = subtotal + returnDouble(txtcharge1.Text);
            else
                subtotal = subtotal - returnDouble(txtcharge1.Text);

            if (hdnoperator2.Value.CompareTo("+") == 0)
                subtotal = subtotal + returnDouble(txtcharge2.Text);
            else
                subtotal = subtotal - returnDouble(txtcharge2.Text);
        }


        //Subtotal applited to textbox////////////////////////////////////////////////////////////////////
        if (hdnflagroundoff.Value.CompareTo("Y") == 0)
            txtsubtotal.Text = returnDouble(Convert.ToString(subtotal)).ToString("F0");
        else
            txtsubtotal.Text = returnDouble(Convert.ToString(subtotal)).ToString("F2");


        //Getting Service Tax

        System.Globalization.CultureInfo enGB = new System.Globalization.CultureInfo("en-GB");

        DocketServiceTax dst = new DocketServiceTax();
        dst.Date = Convert.ToDateTime(txtdockdate.Text, enGB);
        dst.SubTotal = DocketController.ToDouble(txtsubtotal.Text);
        dst.TransMode = cmbtransmode.SelectedValue;
        dst.ServiceType = cmbservicetype.SelectedValue;
        dst.StaxPayer = cmbstaxpaidby.SelectedValue;

        dst = DocketController.GetServiceTax(dst);

        hdnservicetaxrate.Value = dst.StdSTaxRate.ToString("F2");
        lblservicetaxrate.Text = dst.STaxRate.ToString("F2");
        lbleducessrate.Text = dst.StdEduCessRate.ToString("F2");
        lblheducessrate.Text = dst.StdHEduCessRate.ToString("F2");

        if (hdnflagroundoff.Value.CompareTo("Y") == 0)
        {
            txtservicetax.Text = dst.ServiceTax.ToString("F0");
            txteducess.Text = dst.EduCess.ToString("F0");
            txtheducess.Text = dst.HEduCess.ToString("F0");

            txtservicetaxsub.Text = dst.ServiceTax.ToString("F0");
            txteducesssub.Text = dst.EduCess.ToString("F0");
            txtheducesssub.Text = dst.HEduCess.ToString("F0");
        }
        else
        {
            txtservicetax.Text = dst.ServiceTax.ToString("F2");
            txteducess.Text = dst.EduCess.ToString("F2");
            txtheducess.Text = dst.HEduCess.ToString("F2");

            txtservicetaxsub.Text = dst.ServiceTax.ToString("F2");
            txteducesssub.Text = dst.EduCess.ToString("F2");
            txtheducesssub.Text = dst.HEduCess.ToString("F2");
        }

        // Generating and Applying Docket Total
        dockettotal = subtotal + returnDouble(txtservicetax.Text) + returnDouble(txteducess.Text) + returnDouble(txtheducess.Text);


        if (hdnflagroundoff.Value.CompareTo("Y") == 0)
            txtdockettotal.Text = returnDouble(Convert.ToString(dockettotal)).ToString("F0");
        else
            txtdockettotal.Text = returnDouble(Convert.ToString(dockettotal)).ToString("F2");
    }

    protected void grvcharges_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnchargecode1 = (HiddenField)e.Row.FindControl("hdnchargecode1");
            HiddenField hdnchargecode2 = (HiddenField)e.Row.FindControl("hdnchargecode2");

            HiddenField hdnenabled1 = (HiddenField)e.Row.FindControl("hdnenabled1");
            HiddenField hdnenabled2 = (HiddenField)e.Row.FindControl("hdnenabled2");

            Label lbloperator1 = (Label)e.Row.FindControl("lbloperator1");
            Label lbloperator2 = (Label)e.Row.FindControl("lbloperator2");

            TextBox txtcharge1 = (TextBox)e.Row.FindControl("txtcharge1");
            TextBox txtcharge2 = (TextBox)e.Row.FindControl("txtcharge2");

            if (hdnenabled1.Value.CompareTo("Y") == 0)
                txtcharge1.Enabled = true;
            else
                txtcharge1.Enabled = false;

            if (hdnenabled2.Value.CompareTo("Y") == 0)
                txtcharge2.Enabled = true;
            else
                txtcharge2.Enabled = false;

            lbloperator1.Text = "(" + DataBinder.Eval(e.Row.DataItem, "operator1") + ")";
            lbloperator2.Text = "(" + DataBinder.Eval(e.Row.DataItem, "operator2") + ")";

            if (hdnchargecode2.Value.CompareTo("") == 0)
            {
                lbloperator2.Style["display"] = "none";
                txtcharge2.Style["display"] = "none";
            }
        }
    }

    // Inner Function convert string to double,(incase string is NULL or blank it handles) and  keep Code clear
    protected double returnDouble(string num)
    {
        if (num.ToString().CompareTo("") == 0 || num is DBNull)
        {
            num = "0";
        }

        try
        {
            return Convert.ToDouble(num);
        }
        catch (Exception ex)
        {
            return 0.00;
        }
    }

    protected void GetOtherCharges(string basedon1, string basecode1, string basedon2, string basecode2, DataTable dtcharges)
    {
        grvcharges.DataSource = dtcharges;
        grvcharges.DataBind();

        foreach (GridViewRow grvchrg in grvcharges.Rows)
        {
            HiddenField hdnchargecode1 = (HiddenField)grvchrg.FindControl("hdnchargecode1");
            HiddenField hdnchargecode2 = (HiddenField)grvchrg.FindControl("hdnchargecode2");

            TextBox txt1 = (TextBox)grvchrg.FindControl("txtcharge1");
            TextBox txt2 = (TextBox)grvchrg.FindControl("txtcharge2");

            HiddenField hdnvarybyloc1 = (HiddenField)grvchrg.FindControl("hdnvarybyloc1");
            HiddenField hdnvarybyloc2 = (HiddenField)grvchrg.FindControl("hdnvarybyloc2");

            string outcharges = "SCHG11,SCHG12,SCHG13,SCHG19,SCHG20,UCHG01,UCHG02,UCHG03,UCHG04";

            string[] _param_othercharge_req = new string[14];
            _param_othercharge_req[1] = "BKG";
            _param_othercharge_req[2] = basedon1;
            _param_othercharge_req[3] = basecode1;
            _param_othercharge_req[4] = "NONE";
            _param_othercharge_req[5] = "NONE";
            _param_othercharge_req[6] = hdncontractid.Value;
            _param_othercharge_req[7] = txtfrom.Text;
            _param_othercharge_req[8] = txtto.Text;
            _param_othercharge_req[9] = lblorgnloc.Text;
            _param_othercharge_req[10] = txtdelloc.Text;
            _param_othercharge_req[11] = cmbtransmode.SelectedValue;
            _param_othercharge_req[12] = txttotchargedweight.Text;
            _param_othercharge_req[13] = hdntotnopkgs.Value;


            if (outcharges.IndexOf(hdnchargecode1.Value) < 0 && hdnvarybyloc1.Value.CompareTo("Y") == 0)
            {
                _param_othercharge_req[0] = hdnchargecode1.Value;
                txt1.Text = DocketController.GetOtherCharge(_param_othercharge_req).ToString("F2");
            }

            if (outcharges.IndexOf(hdnchargecode1.Value) < 0 && hdnvarybyloc2.Value.CompareTo("Y") == 0)
            {
                _param_othercharge_req[0] = hdnchargecode2.Value;
                txt2.Text = DocketController.GetOtherCharge(_param_othercharge_req).ToString("F2");
            }

            if (hdnchargecode1.Value.CompareTo("SCHG19") == 0 && chkoda.Checked == true)
            {
                double[] oda = new double[2];
                oda = DocketController.BindODACharge(hdncontractid.Value, txtto.Text, DocketController.ToDouble(txttotchargedweight.Text), DocketController.ToDouble(hdntotnopkgs.Value), DocketController.ToInteger(hdntrdays.Value));
                txt1.Text = oda[0].ToString("F2");
                hdntrdays.Value = oda[1].ToString("F2");
            }


            if (hdnchargecode2.Value.CompareTo("SCHG19") == 0 && chkoda.Checked == true)
            {
                double[] oda = new double[2];
                oda = DocketController.BindODACharge(hdncontractid.Value, txtto.Text, DocketController.ToDouble(txttotchargedweight.Text), DocketController.ToDouble(hdntotnopkgs.Value), DocketController.ToInteger(hdntrdays.Value));
                txt2.Text = oda[0].ToString("F2");
                hdntrdays.Value = oda[1].ToString("F2");
            }

            // FUEL SURCHARGE
            if (hdnchargecode1.Value.CompareTo("SCHG20") == 0)
            {
                txt1.Text = applyFuelSurCharge();
                HiddenField hdnchargevalue1 = (HiddenField)grvchrg.FindControl("hdnchargevalue1");
                hdnchargevalue1.Value = txt1.Text;
            }

            if (hdnchargecode2.Value.CompareTo("SCHG20") == 0)
            {
                txt2.Text = applyFuelSurCharge();
                HiddenField hdnchargevalue2 = (HiddenField)grvchrg.FindControl("hdnchargevalue2");
                hdnchargevalue2.Value = txt2.Text;
            }

            // MULTIPICKUP AND DELIVERY CHARGES APPLICABLE
            if (hdnflagmultipickup.Value.CompareTo("Y") == 0 && cmbpaybas.SelectedValue.CompareTo("P02") == 0)
            {
                // MULTIPOINT PICKUP CHARGES
                if (hdnchargecode1.Value.CompareTo("UCHG01") == 0)
                    txt1.Text = applyMultiPointPickUp();

                if (hdnchargecode2.Value.CompareTo("UCHG01") == 0)
                    txt2.Text = applyMultiPointPickUp();

                // MULTIPOINT DELIVERY CHARGES
                if (hdnchargecode1.Value.CompareTo("UCHG03") == 0)
                    txt1.Text = applyMultiPointDelivery();

                if (hdnchargecode2.Value.CompareTo("UCHG03") == 0)
                    txt2.Text = applyMultiPointDelivery();

                // LOADING CHARGES
                if (hdnchargecode1.Value.CompareTo("UCHG04") == 0)
                    txt1.Text = applyLoadingCharge();

                if (hdnchargecode2.Value.CompareTo("UCHG04") == 0)
                    txt2.Text = applyLoadingCharge();

                // UNLOADING CHARGES
                if (hdnchargecode1.Value.CompareTo("UCHG02") == 0)
                    txt1.Text = applyUnLoadingCharge();

                if (hdnchargecode2.Value.CompareTo("UCHG02") == 0)
                    txt2.Text = applyUnLoadingCharge();

                // IF MULTI POINT PICKUP DOCKET THEN
                if (chkmultipickup.Checked == true || chkmultidelivery.Checked == true)
                {
                    txtfreightcharge.Text = "0.00";
                    txtfreightrate.Text = "0.00";
                    lblminfreightalert.Style["display"] = "none";

                    if (hdnchargecode1.Value.CompareTo("UCHG02") == 0)
                        txt1.Text = "0.00";

                    if (hdnchargecode2.Value.CompareTo("UCHG02") == 0)
                        txt2.Text = "0.00";

                    if (hdnchargecode1.Value.CompareTo("UCHG04") == 0)
                        txt1.Text = "0.00";

                    if (hdnchargecode2.Value.CompareTo("UCHG04") == 0)
                        txt2.Text = "0.00";
                }

            } // if MULTIPICK IS THERE OR NOT
        } // GRIDVIEW ROW ENDS HERE
    }

    protected string returnEDD(string trdays)
    {
        string[] arrdate = new string[3];
        arrdate = txtdockdate.Text.Split('/');

        DateTime dt = Convert.ToDateTime(arrdate[1] + "/" + arrdate[0] + "/" + arrdate[2]);
        for (int i = 0; i < returnDouble(trdays); i++)
        {
            if (dt.DayOfWeek.ToString().ToUpper().CompareTo("SUNDAY") == 0)
                dt = dt + TimeSpan.FromDays(1);
            dt = dt + TimeSpan.FromDays(1);
        }

        if (dt.DayOfWeek.ToString().ToUpper().CompareTo("SUNDAY") == 0)
            dt = dt + TimeSpan.FromDays(1);

        DateTime edd = dt;

        return edd.ToString("dd/MM/yyyy");
    }

    // EDD CALCULATION FUNCTION RETURN TYPE : string (EXPECTED DATE OF DELIVERY)
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        btnsubmit.Style["display"] = "none";
        if (cmbpaybas.SelectedValue.ToUpper().CompareTo("P04") != 0)
        {
            if (chkmultipickup.Checked == false && chkmultidelivery.Checked == false)
            {
                if (txtfreightcharge.Text.CompareTo("") == 0 || returnDouble(txtfreightcharge.Text) < 1)
                {
                    Response.Redirect("../ErrorPage.aspx?heading=Critical Error. Freight Charge is Zero&detail1=Internal Error&detail2=" + "&suggestion2=Docket Entry");
                }

                if (txtfreightcharge.Text.CompareTo("") == 0 || returnDouble(txtfreightcharge.Text) < 1)
                {
                    Response.Redirect("../ErrorPage.aspx?heading=Critical Error. Freight Charge is Zero&detail1=Internal Error&detail2=" + "&suggestion2=Docket Entry");
                }
            }

            if (txtsubtotal.Text.CompareTo("") == 0 || returnDouble(txtsubtotal.Text) < 1)
            {
                Response.Redirect("../ErrorPage.aspx?heading=Critical Error. " + dkt_call + " Subtotal is Zero&detail1=Internal Error&detail2=" + "&suggestion2=Docket Entry");
            }

            if (txtdockettotal.Text.CompareTo("") == 0 || returnDouble(txtdockettotal.Text) < 1)
            {
                Response.Redirect("../ErrorPage.aspx?heading=Critical Error. " + dkt_call + " Total is Zero&detail1=Internal Error&detail2=" + "&suggestion2=Docket Entry");
            }
        }


        if (hdnflagroundoff.Value.CompareTo("Y") == 0)
        {
            if (Math.Round(returnDouble(txtsubtotal.Text)) > returnDouble(txtdockettotal.Text))
            {
                Response.Redirect("../ErrorPage.aspx?heading=InValid User Entry. Subtoal is Detacted Greater than Docket Total.&detail1=Internal Error&detail2=" + "&suggestion2=Docket Entry");
                return;
            }
        }
        else
        {

            if (returnDouble(txtsubtotal.Text) > returnDouble(txtdockettotal.Text))
            {
                Response.Redirect("../ErrorPage.aspx?heading=InValid User Entry. Subtoal is Detacted Greater than Docket Total.&detail1=Internal Error&detail2=" + "&suggestion2=Docket Entry");
                return;
            }
        }


        if (!DataValidations.IsActiveLocation(txtbilledat.Text))
        {
            if (cmbpaybas.SelectedValue.CompareTo("P03") == 0)
                txtbilledat.Text = txtdelloc.Text;
            else
                txtbilledat.Text = lblorgnloc.Text;
        }

        /****************************************************************************************************/
        Docket docket = GetDocketObject();
        DocketCharges docketcharges = GetChargesObject();
        DocketInvoice[] docketinvoice = GetInvoiceObject();
        DocketDocument[] docketdoc = GetDocumentObject();
        DataTable dt = new DataTable();
        if (docket.BACode.CompareTo("") == 0)
        {
            Response.Redirect("../ErrorPage.aspx?heading=Problem in finding BA Code. Please Try Again&detail1=May Session TimeOut or BA not found from DCR", false);
            return;
        }

        try
        {
            dt = DocketController.MasterDocketEntry(docket, docketcharges, docketdoc, docketinvoice, hdnfinstartyear.Value);
        }
        catch (Exception ex)
        {
            Response.Redirect("../ErrorPage.aspx?heading=DataBase Entry Failed.&detail1=" + ex.Message.Replace('\n', '_')+ "&suggestion1=Re-Enter " + dkt_call);
            return;
        }
        /**************************************************************************************************/

        if (dt.Rows.Count == 0)
        {
            Response.Redirect("../ErrorPage.aspx?heading=DataBase Entry Failed&detail1=No record Entered");
            return;
        }

        if (cmbpaybas.SelectedValue.CompareTo("P01") == 0)
        {
            Response.Redirect("docket_submit_done.aspx?editflag=ENTRY&dockno=" + dt.Rows[0]["dockno"].ToString() + "&Mrsno=NA&billno=" + dt.Rows[0]["billno"].ToString());
        }
        else
        {
            Response.Redirect("docket_submit_done.aspx?editflag=ENTRY&dockno=" + dt.Rows[0]["dockno"].ToString() + "&Mrsno=NA&billno=NA");
        }
    }


    protected Docket GetDocketObject()
    {
        Docket docket = new Docket();
        docket.DockNo = txtdockno.Text;
        try
        {
            docket.DockDate = Convert.ToDateTime(fn.Mydate1(txtdockdate.Text.Trim()));
        }
        catch (Exception ex) { }
        docket.PayBase = cmbpaybas.SelectedValue;
        docket.PartyCode = txtpartycode.Text;
        docket.OrgnLoc = lblorgnloc.Text.Trim();
        docket.DelLoc = txtdelloc.Text.Trim();
        docket.TransMode = cmbtransmode.SelectedValue;
        docket.ServiceType = cmbservicetype.SelectedValue;
        docket.FTLType = cmbftltype.SelectedValue;
        docket.PickUpDel = cmbpickupdel.SelectedValue;
        docket.FromCity = txtfrom.Text;
        docket.ToCity = txtto.Text;
        docket.PackType = cmbpacktype.SelectedValue;
        docket.ProdType = cmbprodtype.SelectedValue;
        docket.Remarks = txtsplinstruct.Text;
        docket.FlagVolumetric = (chkvolumetric.Checked == true ? "Y" : "N");
        docket.FlagCODDOD = (chkcoddod.Checked == true ? "Y" : "N");
        docket.FlagODA = (chkoda.Checked == true ? "Y" : "N");
        docket.FlagLocal = (chklocal.Checked == true ? "Y" : "N");
        docket.FlagDACC = (chkdacc.Checked == true ? "Y" : "N");
        docket.FlagPermit = (chkpermit.Checked == true) ? "Y" : "N";
        docket.LoadType = cmbloadtype.SelectedValue;
        docket.DopiNo = txtdopino.Text;
        docket.BusinessType = cmbbusinesstype.SelectedValue;

        docket.ConsignorCode = (txtcsgncd.Text.CompareTo("") == 0 ? "8888" : txtcsgncd.Text.Trim());
        docket.ConsignorName = (txtcsgncd.Text.CompareTo("") == 0 ? txtcsgnname.Text : txtcsgnnm.Text);
        docket.ConsignorAddress = txtcsgnaddress.Text;
        docket.ConsignorCity = txtcsgncity.Text;
        docket.ConsignorPinCode = txtcsgnpin.Text;
        docket.ConsignorPhone = txtcsgnphone.Text;
        docket.ConsignorEmail = txtcsgnemail.Text;

        docket.ConsigneeCode = (txtcsgecd.Text.CompareTo("") == 0 ? "8888" : txtcsgecd.Text.Trim());
        docket.ConsigneeName = (txtcsgecd.Text.CompareTo("") == 0 ? txtcsgename.Text : txtcsgenm.Text);
        docket.ConsigneeAddress = txtcsgeaddress.Text;
        docket.ConsigneeCity = txtcsgecity.Text;
        docket.ConsigneePinCode = txtcsgepin.Text;
        docket.ConsigneePhone = txtcsgephone.Text;
        docket.ConsigneeEmail = txtcsgeemail.Text;
        docket.EntrySheetNo = txtentrysheetno.Text;
        docket.OBDNo = txtobdno.Text;
        docket.PermitNo = txtpermitno.Text;
        try
        {
            docket.PermitDate = Convert.ToDateTime(fn.Mydate1(txtpermitdate.Text));
        }
        catch (Exception ex) { }
        try
        {
            docket.PermitValidityDate = Convert.ToDateTime(fn.Mydate1(txtpermitdate.Text));
        }
        catch (Exception ex) { }
        try
        {
            docket.PermitRecieveDate = Convert.ToDateTime(fn.Mydate1(txtpermitdate.Text));
        }
        catch (Exception ex) { }


        docket.PermitRecievedAt = cmbpermitrecvdat.SelectedValue;

        /// IT IS TO LIMIT THE SCOPE OF INVOICE VARIABLES
        {
            TextBox txtinvoiceno = (TextBox)(grvinvoice.Rows[0].FindControl("txtinvoiceno"));
            TextBox txtinvoicedate = (TextBox)(grvinvoice.Rows[0].FindControl("txtinvoicedate"));
            docket.InvoiceNo = txtinvoiceno.Text;
            try
            {
                docket.InvoiceDate = Convert.ToDateTime(fn.Mydate1(txtinvoicedate.Text));
            }
            catch (Exception ex) { }

            docket.InvoiceAmount = WebXConvert.ToInt64(hdntotdeclval.Value);
        }

        docket.BACode = (optentrytype.SelectedValue.CompareTo("C") == 0 ? hdnemployee.Value : DocketController.GetBACode(txtdockno.Text));
        docket.RiskType = (optcarrierrisk.Checked == true ? "C" : "O");
        docket.PolicyNo = txtpolicyno.Text;

        try
        {
            docket.PolicyDate = Convert.ToDateTime(fn.Mydate1(txtpolicydate.Text));
        }
        catch (Exception ex) { }

        docket.InternalCovers = DocketController.ToDouble(txtinternalcovers.Text);
        docket.ModvatCovers = DocketController.ToDouble(txtmodvatcovers.Text);
        docket.CustomerRefNo = txtcustrefno.Text;
        docket.PrivateMark = txtprivatemark.Text;
        docket.TPNumber = txttpnumber.Text;
        docket.NoOfPkgs = WebXConvert.ToInt64(hdntotnopkgs.Value);
        docket.ActualWeight = WebXConvert.ToInt64(hdntotactualweight.Value);
        docket.ChargedWeight = WebXConvert.ToInt64(txttotchargedweight.Text);
        docket.CODDODCharged = (chkcoddod.Checked == true ? DocketController.ToDouble(txtcoddodcharged.Text) : 0);
        docket.CODDODAmount = (chkcoddod.Checked == true ? DocketController.ToDouble(txtcoddodcollected.Text) : 0);
        try
        {
            docket.EDD = Convert.ToDateTime(fn.Mydate1(txtedd.Text));
        }
        catch (Exception ex) { }

        docket.STaxPaidBy = cmbstaxpaidby.SelectedValue;
        docket.STaxRegNo = txtstaxregno.Text;
        docket.BilledAt = txtbilledat.Text;
        docket.SourceDockNo = txtsourcedockno.Text;
        docket.FlagMultiPickUp = (chkmultipickup.Checked == true ? "Y" : "N");
        docket.FlagMultiDelivery = (chkmultidelivery.Checked == true ? "Y" : "N");
        docket.FlagStaxExmpt = (chkservicetaxexemp.Checked == true ? "Y" : "N");
        docket.PartyAs = (optpartycsge.Checked == true ? "CSGE" : "CSGN");
        if (optentrytype.SelectedValue.CompareTo("N") == 0)
            docket.DocType = "DKT";
        else
            docket.DocType = "CMP";
        docket.EntryBy = hdnemployee.Value;
        try
        {
            docket.EntryDate = Convert.ToDateTime(fn.Mydate1(hdnserverdate.Value));
        }
        catch (Exception ex) { }

        return docket;
    }

    protected MR GetMRObject(Docket docket)
    {
        MR mr = new MR();
        mr.BACode = docket.BACode;
        mr.ChargedWeight = docket.ChargedWeight;
        mr.DelLoc = docket.DelLoc;
        mr.DockDate = docket.DockDate;
        mr.DockNo = docket.DockNo;
        mr.DockSF = ".";
        mr.EduCess = DocketController.ToDouble(txteducess.Text);
        mr.EduCessRate = DocketController.ToDouble(lbleducessrate.Text);
        mr.EDD = docket.EDD;
        mr.EntryBy = hdnemployee.Value;
        mr.EntryDate = docket.EntryDate;
        mr.FlagReBook = "N";
        mr.Freight = DocketController.ToDouble(txtfreightcharge.Text);
        mr.HEduCess = DocketController.ToDouble(txtheducess.Text);
        mr.HEduCessRate = DocketController.ToDouble(lblheducessrate.Text);
        mr.MRAmount = Math.Round(DocketController.ToDouble(txtdockettotal.Text));
        mr.MRBr = docket.OrgnLoc;
        mr.MRDate = docket.DockDate;
        mr.MRNo = docket.MRNo;
        mr.MRSF = ".";
        mr.MRType = "1";
        mr.NoOfPkgs = Convert.ToInt32(docket.NoOfPkgs);
        mr.OrgnLoc = docket.OrgnLoc;
        mr.PartyAddress = txtcsgnaddress.Text;
        if (txtcsgncd.Text.CompareTo("") == 0)
        {
            mr.PartyCode = "8888";
            mr.PartyName = txtcsgnname.Text;
        }
        else
        {
            mr.PartyCode = txtcsgncd.Text;
            mr.PartyName = txtcsgnnm.Text;
        }
        mr.PartyPhone = txtcsgnphone.Text;
        mr.PartyPinCode = txtcsgnpin.Text;
        mr.PayBas = docket.PayBase;
        mr.ServiceTax = DocketController.ToDouble(txtservicetax.Text);
        mr.STaxRate = DocketController.ToDouble(lblservicetaxrate.Text);

        return mr;
    }

    protected DocketCharges GetChargesObject()
    {
        string[] arrchargenames = new string[34];
        double[] arrcharges = new double[34];

        for (int i = 1; i < 24; i++)
        {
            arrchargenames[i] = "SCHG" + i.ToString().PadLeft(2, '0');
        }
        for (int i = 1; i < 11; i++)
        {
            arrchargenames[i + 23] = "UCHG" + i.ToString().PadLeft(2, '0');
        }

        foreach (GridViewRow grv in grvcharges.Rows)
        {
            TextBox txtcharge1 = (TextBox)grv.FindControl("txtcharge1");
            TextBox txtcharge2 = (TextBox)grv.FindControl("txtcharge2");
            HiddenField hdnchargecode1 = (HiddenField)grv.FindControl("hdnchargecode1");
            HiddenField hdnchargecode2 = (HiddenField)grv.FindControl("hdnchargecode2");

            for (int i = 1; i < 34; i++)
            {
                if (arrchargenames[i].CompareTo(hdnchargecode1.Value) == 0)
                {
                    arrcharges[i] = DocketController.ToDouble(txtcharge1.Text.Trim());
                }
                if (arrchargenames[i].CompareTo(hdnchargecode2.Value) == 0)
                {
                    arrcharges[i] = DocketController.ToDouble(txtcharge2.Text.Trim());
                }
            }
        }

        //Exceptional Charges
        arrcharges[11] = DocketController.ToDouble(txtfovcharged.Text);
        if (chkcoddod.Checked == true)
            arrcharges[12] = DocketController.ToDouble(txtcoddodcharged.Text);
        else
            arrcharges[12] = 0;

        if (chkdacc.Checked == true)
            arrcharges[13] = DocketController.ToDouble(txtdacccharged.Text);
        else
            arrcharges[13] = 0;

        DocketCharges docketcharges = new DocketCharges();
        docketcharges.DockNo = txtdockno.Text;
        docketcharges.RateType = cmbratetype.SelectedValue;
        docketcharges.FreightRate = DocketController.ToDouble(txtfreightrate.Text);
        docketcharges.Freight = DocketController.ToDouble(txtfreightcharge.Text);
        docketcharges.Charges = arrcharges;
        docketcharges.SubTotal = DocketController.ToDouble(txtsubtotal.Text);
        docketcharges.ServiceTax = DocketController.ToDouble(txtservicetax.Text);
        docketcharges.EduCess = DocketController.ToDouble(txteducess.Text);
        docketcharges.HEduCess = DocketController.ToDouble(txtheducess.Text);
        docketcharges.DocketTotal = DocketController.ToDouble(txtdockettotal.Text);

        return docketcharges;
    }

    protected DocketDocument[] GetDocumentObject()
    {
        DocketDocument[] docketdoc = new DocketDocument[grvdoc.Rows.Count];
        foreach (GridViewRow grv in grvdoc.Rows)
        {
            docketdoc[grv.RowIndex] = new DocketDocument();
            HiddenField hdnsrno = (HiddenField)grv.FindControl("Doc_srno");
            TextBox txtdocumentno = (TextBox)grv.FindControl("txtdocumentno");

            docketdoc[grv.RowIndex].DockNo = txtdockno.Text;
            docketdoc[grv.RowIndex].DockSF = ".";
            docketdoc[grv.RowIndex].SRNo = DocketController.ToDouble(hdnsrno.Value);
            docketdoc[grv.RowIndex].DocumentNo = txtdocumentno.Text;
        }

        return docketdoc;
    }

    protected DocketInvoice[] GetInvoiceObject()
    {
        DocketInvoice[] dockinv = new DocketInvoice[grvinvoice.Rows.Count];


        foreach (GridViewRow grv in grvinvoice.Rows)
        {
            dockinv[grv.RowIndex] = new DocketInvoice();

            TextBox txtinvoiceno = (TextBox)grv.FindControl("txtinvoiceno");
            TextBox txtinvoicedate = (TextBox)grv.FindControl("txtinvoicedate");
            TextBox txtdeclval = (TextBox)grv.FindControl("txtdeclval");
            TextBox txtnopkgs = (TextBox)grv.FindControl("txtnopkgs");
            TextBox txtactualweight = (TextBox)grv.FindControl("txtactualweight");
            TextBox txtlength = (TextBox)grv.FindControl("txtlength");
            TextBox txtbreadth = (TextBox)grv.FindControl("txtbreadth");
            TextBox txtheight = (TextBox)grv.FindControl("txtheight");
            TextBox txtcftval = (TextBox)grv.FindControl("txtcft");

            dockinv[grv.RowIndex].DockNo = txtdockno.Text;
            dockinv[grv.RowIndex].InvoiceNo = txtinvoiceno.Text;

            try
            {
                dockinv[grv.RowIndex].InvoiceDate = Convert.ToDateTime(fn.Mydate1(txtinvoicedate.Text));
            }
            catch (Exception ex) { }

            dockinv[grv.RowIndex].DeclaredValue = DocketController.ToDouble(txtdeclval.Text);
            dockinv[grv.RowIndex].NoOfPkgs = DocketController.ToDouble(txtnopkgs.Text);
            dockinv[grv.RowIndex].ActualWeight = DocketController.ToDouble(txtactualweight.Text);
            dockinv[grv.RowIndex].Vol_Length = DocketController.ToDouble(txtlength.Text);
            dockinv[grv.RowIndex].Vol_Breadth = DocketController.ToDouble(txtbreadth.Text);
            dockinv[grv.RowIndex].Vol_Height = DocketController.ToDouble(txtheight.Text);
            dockinv[grv.RowIndex].Vol_CFT = DocketController.ToDouble(txtcftval.Text);
        }

        return dockinv;

    }

    protected string applyMultiPointPickUp()
    {
        string flat = "", loc = "", sqlstr = "", rateype = "", multipickupcharge = "0.00";
        double pickuprate = 0.00;

        sqlstr = "SELECT flatmultipickup_yn,locmultipickup_yn FROM webx_custcontract_charge WHERE contractid='" + hdncontractid.Value + "'";
        SqlDataReader dr = SqlHelper.ExecuteReader(SessionUtilities.ConnectionString, CommandType.Text, sqlstr);
        while (dr.Read())
        {
            flat = Convert.ToString(dr["flatmultipickup_yn"]);
            loc = Convert.ToString(dr["locmultipickup_yn"]);
        }
        dr.Close();


        if (loc.CompareTo("Y") == 0)
        {
            sqlstr = "SELECT pickup_chrg,delivery_chrg FROM webx_custcontract_multipointcharges ";
            sqlstr = sqlstr + " WHERE contractid='" + hdncontractid.Value + "' AND ftltype='" + cmbftltype.SelectedValue.Trim() + "'";
            sqlstr = sqlstr + " AND from_loc='" + lblorgnloc.Text.Trim() + "' AND to_loc='" + txtdelloc.Text.Trim() + "'";
            dr = SqlHelper.ExecuteReader(SessionUtilities.ConnectionString, CommandType.Text, sqlstr);
            while (dr.Read())
            {
                pickuprate = returnDouble(Convert.ToString(dr["pickup_chrg"]));
                rateype = Convert.ToString(dr["ratetype"]);
            }
            dr.Close();

        }
        else if (flat.CompareTo("Y") == 0)
        {
            sqlstr = "SELECT pickup,ratetype,delivery FROM webx_custcontract_flatmultipoint ";
            sqlstr = sqlstr + " WHERE contractid='" + hdncontractid.Value + "' AND ftltype='" + cmbftltype.SelectedValue.Trim() + "'";
            dr = SqlHelper.ExecuteReader(SessionUtilities.ConnectionString, CommandType.Text, sqlstr);
            while (dr.Read())
            {
                pickuprate = returnDouble(Convert.ToString(dr["pickup"]));
                rateype = Convert.ToString(dr["ratetype"]);

            }
            dr.Close();
        }

        if (rateype.ToUpper().CompareTo("W") == 0)
        {
            multipickupcharge = returnDouble(Convert.ToString(pickuprate * returnDouble(txttotchargedweight.Text))).ToString("F2");
        }
        else if (rateype.ToUpper().CompareTo("P") == 0)
        {
            multipickupcharge = returnDouble(Convert.ToString(pickuprate * returnDouble(hdntotnopkgs.Value))).ToString("F2");
        }
        else if (rateype.ToUpper().CompareTo("T") == 0)
        {
            multipickupcharge = returnDouble(Convert.ToString(pickuprate * returnDouble(txttotchargedweight.Text) / 1000)).ToString("F2");
        }
        else if (rateype.ToUpper().CompareTo("F") == 0)
        {
            multipickupcharge = pickuprate.ToString("F2");
        }

        if (chkmultipickup.Checked == false)
        {
            multipickupcharge = "0.00";
        }

        if (multipickupcharge.CompareTo("") == 0)
            multipickupcharge = "0.00";

        return multipickupcharge;
    }

    protected string applyMultiPointDelivery()
    {

        string flat = "", loc = "", sqlstr = "", rateype = "", multideliverycharge = "0.00";
        double deliveryrate = 0.00;

        sqlstr = "SELECT flatmultidelivery_yn,locmultidelivery_yn FROM webx_custcontract_charge WHERE contractid='" + hdncontractid.Value + "'";
        SqlDataReader dr = SqlHelper.ExecuteReader(SessionUtilities.ConnectionString, CommandType.Text, sqlstr);
        while (dr.Read())
        {
            flat = Convert.ToString(dr["flatmultidelivery_yn"]);
            loc = Convert.ToString(dr["locmultidelivery_yn"]);
        }
        dr.Close();

        if (loc.CompareTo("") == 0 && flat.CompareTo("") == 0)
        {
            multideliverycharge = "0.00";
        }

        if (loc.CompareTo("Y") == 0)
        {
            sqlstr = "SELECT pickup_chrg,delivery_chrg FROM webx_custcontract_multipointcharges ";
            sqlstr = sqlstr + " WHERE contractid='" + hdncontractid.Value + "' AND ftltype='" + cmbftltype.SelectedValue.Trim() + "'";
            sqlstr = sqlstr + " AND from_loc='" + lblorgnloc.Text.Trim() + "' AND to_loc='" + txtdelloc.Text.Trim() + "'";
            dr = SqlHelper.ExecuteReader(SessionUtilities.ConnectionString, CommandType.Text, sqlstr);
            while (dr.Read())
            {

                deliveryrate = returnDouble(Convert.ToString(dr["delivery_chrg"]));
                rateype = Convert.ToString(dr["ratetype"]);
            }
            dr.Close();

        }
        else if (flat.CompareTo("Y") == 0)
        {
            sqlstr = "SELECT pickup,ratetype,delivery FROM webx_custcontract_flatmultipoint ";
            sqlstr = sqlstr + " WHERE contractid='" + hdncontractid.Value + "' AND ftltype='" + cmbftltype.SelectedValue.Trim() + "'";
            dr = SqlHelper.ExecuteReader(SessionUtilities.ConnectionString, CommandType.Text, sqlstr);
            while (dr.Read())
            {

                deliveryrate = returnDouble(Convert.ToString(dr["delivery"]));
                rateype = Convert.ToString(dr["ratetype"]);

            }
            dr.Close();
        }

        if (rateype.ToUpper().CompareTo("W") == 0)
        {
            multideliverycharge = returnDouble(Convert.ToString(deliveryrate * returnDouble(txttotchargedweight.Text))).ToString("F2");
        }
        else if (rateype.ToUpper().CompareTo("P") == 0)
        {
            multideliverycharge = returnDouble(Convert.ToString(deliveryrate * returnDouble(hdntotnopkgs.Value))).ToString("F2");
        }
        else if (rateype.ToUpper().CompareTo("T") == 0)
        {
            multideliverycharge = returnDouble(Convert.ToString(deliveryrate * returnDouble(txttotchargedweight.Text) / 1000)).ToString("F2");
        }
        else if (rateype.ToUpper().CompareTo("F") == 0)
        {
            multideliverycharge = deliveryrate.ToString("F2");
        }

        if (chkmultidelivery.Checked == false)
        {
            multideliverycharge = "0.00";
        }


        if (multideliverycharge.CompareTo("") == 0)
            multideliverycharge = "0.00";

        return multideliverycharge;

    }

    protected string applyLoadingCharge()
    {
        string flatloading = "", locloading = "", sqlstr = "", loadingcharge = "0.00";
        string loadingratetype = "";
        double loadingrate = 0.00;
        sqlstr = "SELECT flatloading_yn,flatunloading_yn,locloading_yn,locunloading_yn FROM webx_custcontract_charge WHERE contractid='" + hdncontractid.Value + "'";
        SqlDataReader dr = SqlHelper.ExecuteReader(SessionUtilities.ConnectionString, CommandType.Text, sqlstr);
        while (dr.Read())
        {
            flatloading = Convert.ToString(dr["flatloading_yn"]);
            locloading = Convert.ToString(dr["locloading_yn"]);
        }
        dr.Close();



        int reccnt = 0;
        // RETRIEVING LOCATION WISE LOADING CHARGES
        if (locloading.CompareTo("Y") == 0)
        {
            if (cmbservicetype.SelectedValue.CompareTo("1") == 0)
                sqlstr = "SELECT airrate AS rate,ratetype FROM webx_custcontract_loadingcharges WHERE contractid='" + hdncontractid.Value + "' AND loctype='C' AND location='" + txtfrom.Text + "' AND service_class='" + cmbservicetype.SelectedValue + "'";
            else if (cmbservicetype.SelectedValue.CompareTo("2") == 0)
                sqlstr = "SELECT airrate AS rate,ratetype FROM webx_custcontract_loadingcharges WHERE contractid='" + hdncontractid.Value + "' AND loctype='C' AND location='" + txtfrom.Text + "' AND service_class='" + cmbservicetype.SelectedValue + "' AND ftltype='" + cmbftltype.SelectedValue + "'";
            dr = SqlHelper.ExecuteReader(SessionUtilities.ConnectionString, CommandType.Text, sqlstr);
            while (dr.Read())
            {
                loadingrate = returnDouble(Convert.ToString(dr["rate"]));
                loadingratetype = Convert.ToString(dr["ratetype"]).ToUpper();
                reccnt++;
            }
            dr.Close();

            if (reccnt == 0)
            {
                if (cmbservicetype.SelectedValue.CompareTo("1") == 0)
                    sqlstr = "SELECT airrate AS rate,ratetype FROM webx_custcontract_loadingcharges WHERE contractid='" + hdncontractid.Value + "' AND loctype='L' AND location='" + lblorgnloc.Text + "' AND service_class='" + cmbservicetype.SelectedValue + "'";
                else if (cmbservicetype.SelectedValue.CompareTo("2") == 0)
                    sqlstr = "SELECT airrate AS rate,ratetype FROM webx_custcontract_loadingcharges WHERE contractid='" + hdncontractid.Value + "' AND loctype='L' AND location='" + lblorgnloc.Text + "' AND service_class='" + cmbservicetype.SelectedValue + "' AND ftltype='" + cmbftltype.SelectedValue + "'";
                dr = SqlHelper.ExecuteReader(SessionUtilities.ConnectionString, CommandType.Text, sqlstr);
                while (dr.Read())
                {
                    loadingrate = returnDouble(Convert.ToString(dr["rate"]));
                    loadingratetype = Convert.ToString(dr["ratetype"]).ToUpper();
                    reccnt++;
                }
                dr.Close();
            } // END OF CITY NOT FOUND IF

        } // END OF LOADING IF

        if (flatloading.ToUpper().CompareTo("Y") == 0)
        {
            if (cmbservicetype.SelectedValue.CompareTo("1") == 0)
            {
                sqlstr = "SELECT rate,ratetype FROM webx_custcontract_flatloading WHERE contractid='" + hdncontractid.Value + "' AND service_class='" + cmbservicetype.SelectedValue + "'";
            }
            else if (cmbservicetype.SelectedValue.CompareTo("2") == 0)
            {
                sqlstr = "SELECT rate,ratetype FROM webx_custcontract_flatloading WHERE contractid='" + hdncontractid.Value + "' AND service_class='" + cmbservicetype.SelectedValue + "' AND ftltype='" + cmbftltype.SelectedValue + "'";
            }
            dr = SqlHelper.ExecuteReader(SessionUtilities.ConnectionString, CommandType.Text, sqlstr);
            while (dr.Read())
            {
                loadingrate = returnDouble(Convert.ToString(dr["rate"]));
                loadingratetype = Convert.ToString(dr["ratetype"]);
            }
            dr.Close();

        }


        if (loadingratetype.CompareTo("P") == 0)
        {
            loadingcharge = returnDouble(Convert.ToString(loadingrate * returnDouble(hdntotnopkgs.Value))).ToString("F2");
        }
        else if (loadingratetype.CompareTo("W") == 0)
        {
            loadingcharge = returnDouble(Convert.ToString(loadingrate * returnDouble(txttotchargedweight.Text))).ToString("F2");
        }
        else if (loadingratetype.CompareTo("T") == 0)
        {
            loadingcharge = returnDouble(Convert.ToString(loadingrate * returnDouble(txttotchargedweight.Text) / 1000)).ToString("F2");
        }
        else if (loadingratetype.CompareTo("F") == 0)
        {
            loadingcharge = loadingrate.ToString("F2");
        }


        if (returnDouble(loadingcharge) == 0)
            loadingcharge = "0.00";

        return loadingcharge;

    }

    protected string applyUnLoadingCharge()
    {
        string flatunloading = "", locunloading = "", sqlstr = "", unloadingcharge = "0.00";
        string unloadingratetype = "";
        double unloadingrate = 0.00;
        sqlstr = "SELECT flatloading_yn,flatunloading_yn,locloading_yn,locunloading_yn FROM webx_custcontract_charge WHERE contractid='" + hdncontractid.Value + "'";
        SqlDataReader dr = SqlHelper.ExecuteReader(SessionUtilities.ConnectionString, CommandType.Text, sqlstr);
        while (dr.Read())
        {
            flatunloading = Convert.ToString(dr["flatunloading_yn"]);
            locunloading = Convert.ToString(dr["locunloading_yn"]);
        }
        dr.Close();

        int reccnt = 0;
        // RETRIEVING LOCATION WISE UNLOADING CHARGES
        if (locunloading.CompareTo("Y") == 0)
        {
            reccnt = 0;
            if (cmbservicetype.SelectedValue.CompareTo("1") == 0)
                sqlstr = "SELECT airrate AS rate,ratetype FROM webx_custcontract_unloadingcharges WHERE contractid='" + hdncontractid.Value + "' AND loctype='C' AND location='" + txtto.Text + "' AND service_class='" + cmbservicetype.SelectedValue + "'";
            else if (cmbservicetype.SelectedValue.CompareTo("2") == 0)
                sqlstr = "SELECT airrate AS rate,ratetype FROM webx_custcontract_unloadingcharges WHERE contractid='" + hdncontractid.Value + "' AND loctype='C' AND location='" + txtto.Text + "' AND service_class='" + cmbservicetype.SelectedValue + "' AND ftltype='" + cmbftltype.SelectedValue + "'";
            dr = SqlHelper.ExecuteReader(SessionUtilities.ConnectionString, CommandType.Text, sqlstr);
            while (dr.Read())
            {
                unloadingrate = returnDouble(Convert.ToString(dr["rate"]));
                unloadingratetype = Convert.ToString(dr["ratetype"]).ToUpper();
                reccnt++;
            }
            dr.Close();


            if (reccnt == 0)
            {
                if (cmbservicetype.SelectedValue.CompareTo("1") == 0)
                    sqlstr = "SELECT airrate AS rate,ratetype FROM webx_custcontract_unloadingcharges WHERE contractid='" + hdncontractid.Value + "' AND loctype='L' AND location='" + txtdelloc.Text + "' AND service_class='" + cmbservicetype.SelectedValue + "'";
                else if (cmbservicetype.SelectedValue.CompareTo("2") == 0)
                    sqlstr = "SELECT airrate AS rate,ratetype FROM webx_custcontract_unloadingcharges WHERE contractid='" + hdncontractid.Value + "' AND loctype='L' AND location='" + txtdelloc.Text + "' AND service_class='" + cmbservicetype.SelectedValue + "' AND ftltype='" + cmbftltype.SelectedValue + "'";
                dr = SqlHelper.ExecuteReader(SessionUtilities.ConnectionString, CommandType.Text, sqlstr);
                while (dr.Read())
                {
                    unloadingrate = returnDouble(Convert.ToString(dr["rate"]));
                    unloadingratetype = Convert.ToString(dr["ratetype"]).ToUpper();
                    reccnt++;
                }
                dr.Close();
            }
        } // END OF LOCATION UNLOADING



        if (flatunloading.ToUpper().CompareTo("Y") == 0)
        {
            if (cmbservicetype.SelectedValue.CompareTo("1") == 0)
            {
                sqlstr = "SELECT rate,ratetype FROM webx_custcontract_flatunloading WHERE contractid='" + hdncontractid.Value + "' AND service_class='" + cmbservicetype.SelectedValue + "'";
            }
            else if (cmbservicetype.SelectedValue.CompareTo("2") == 0)
            {
                sqlstr = "SELECT rate,ratetype FROM webx_custcontract_flatunloading WHERE contractid='" + hdncontractid.Value + "' AND service_class='" + cmbservicetype.SelectedValue + "' AND ftltype='" + cmbftltype.SelectedValue + "'";
            }
            dr = SqlHelper.ExecuteReader(SessionUtilities.ConnectionString, CommandType.Text, sqlstr);
            while (dr.Read())
            {
                unloadingrate = returnDouble(Convert.ToString(dr["rate"]));
                unloadingratetype = Convert.ToString(dr["ratetype"]);
            }
            dr.Close();

        }

        if (unloadingratetype.CompareTo("P") == 0)
        {
            unloadingcharge = returnDouble(Convert.ToString(unloadingrate * returnDouble(hdntotnopkgs.Value))).ToString("F2");
        }
        else if (unloadingratetype.CompareTo("W") == 0)
        {
            unloadingcharge = returnDouble(Convert.ToString(unloadingrate * returnDouble(txttotchargedweight.Text))).ToString("F2");
        }
        else if (unloadingratetype.CompareTo("T") == 0)
        {
            unloadingcharge = returnDouble(Convert.ToString(unloadingrate * returnDouble(txttotchargedweight.Text) / 1000)).ToString("F2");
        }
        else if (unloadingratetype.CompareTo("F") == 0)
        {
            unloadingcharge = unloadingrate.ToString("F2");
        }


        if (returnDouble(unloadingcharge) == 0)
            unloadingcharge = "0.00";

        return unloadingcharge;
    }

    protected string applyFuelSurCharge()
    {
        string fuelsurchargeflag = "", fuelsurchargebasair = "", fuelsurchargebasroad = "", fuelsurchargebastrain = "", fuelsurchargebasexpress = "";
        double fuelsurchargeair = 0, fuelminchargeair = 0, fuelmaxchargeair = 0;
        double fuelsurchargeroad = 0, fuelminchargeroad = 0, fuelmaxchargeroad = 0;
        double fuelsurchargetrain = 0, fuelminchargetrain = 0, fuelmaxchargetrain = 0;
        double fuelsurchargeexpress = 0, fuelminchargeexpress = 0, fuelmaxchargeexpress = 0;

        double totalsurcharge = 0.00, freightchrg = 0.00;
        freightchrg = returnDouble(txtfreightcharge.Text.Trim());

        string sqlstr = "SELECT * FROM webx_custcontract_charge WHERE contractid='" + hdncontractid.Value + "'";
        SqlDataReader dr = SqlHelper.ExecuteReader(SessionUtilities.ConnectionString, CommandType.Text, sqlstr);
        while (dr.Read())
        {
            fuelsurchargeflag = Convert.ToString(dr["diesel_hike_yn"]);
            fuelminchargeair = returnDouble(Convert.ToString(dr["min_fuelsurchrgair"]));
            fuelmaxchargeair = returnDouble(Convert.ToString(dr["max_fuelsurchrgair"]));
            fuelminchargeroad = returnDouble(Convert.ToString(dr["min_fuelsurchrgroad"]));
            fuelmaxchargeroad = returnDouble(Convert.ToString(dr["max_fuelsurchrgroad"]));
            fuelminchargetrain = returnDouble(Convert.ToString(dr["min_fuelsurchrgtrain"]));
            fuelmaxchargetrain = returnDouble(Convert.ToString(dr["max_fuelsurchrgtrain"]));
            fuelminchargeexpress = returnDouble(Convert.ToString(dr["min_fuelsurchrgexpress"]));
            fuelmaxchargeexpress = returnDouble(Convert.ToString(dr["max_fuelsurchrgexpress"]));

            fuelsurchargebasair = Convert.ToString(dr["fuelsurchrgbasair"]);
            fuelsurchargebasroad = Convert.ToString(dr["fuelsurchrgbasroad"]);
            fuelsurchargebastrain = Convert.ToString(dr["fuelsurchrgbastrain"]);
            fuelsurchargebasexpress = Convert.ToString(dr["fuelsurchrgbasexpress"]);

            fuelsurchargeair = returnDouble(Convert.ToString(dr["air_fuelsurchrg"]));
            fuelsurchargeroad = returnDouble(Convert.ToString(dr["road_fuelsurchrg"]));
            fuelsurchargetrain = returnDouble(Convert.ToString(dr["train_fuelsurchrg"]));
            fuelsurchargeexpress = returnDouble(Convert.ToString(dr["express_fuelsurchrg"]));
        }
        dr.Close();

        if (fuelsurchargeflag.ToUpper().CompareTo("Y") == 0)
        {
            switch (cmbtransmode.SelectedValue)
            {
                case "1":
                    switch (fuelsurchargebasair)
                    {
                        case "F":
                            totalsurcharge = fuelsurchargeair;
                            break;
                        case "%":
                            totalsurcharge = (freightchrg * fuelsurchargeair) / 100;
                            break;
                        case "W":
                            totalsurcharge = (returnDouble(txttotchargedweight.Text) * fuelsurchargeair);
                            break;
                    }
                    hdnfuelsurchargebas.Value = fuelsurchargebasair;
                    hdnfuelsurchargerate.Value = fuelsurchargeair.ToString("F2");
                    hdnfuelsurmincharge.Value = fuelminchargeair.ToString("F2");
                    hdnfuelsurmaxcharge.Value = fuelmaxchargeair.ToString("F2");
                    break;
                case "2":
                    switch (fuelsurchargebasroad)
                    {
                        case "F":
                            totalsurcharge = fuelsurchargeroad;
                            break;
                        case "%":
                            totalsurcharge = (freightchrg * fuelsurchargeroad) / 100;
                            break;
                        case "W":
                            totalsurcharge = (returnDouble(txttotchargedweight.Text) * fuelsurchargeroad);
                            break;
                    }
                    hdnfuelsurchargebas.Value = fuelsurchargebasroad;
                    hdnfuelsurchargerate.Value = fuelsurchargeroad.ToString("F2");
                    hdnfuelsurmincharge.Value = fuelminchargeroad.ToString("F2");
                    hdnfuelsurmaxcharge.Value = fuelmaxchargeroad.ToString("F2");
                    break;
                case "3":
                    switch (fuelsurchargebastrain)
                    {
                        case "F":
                            totalsurcharge = fuelsurchargetrain;
                            break;
                        case "%":
                            totalsurcharge = (freightchrg * fuelsurchargetrain) / 100;
                            break;
                        case "W":
                            totalsurcharge = (returnDouble(txttotchargedweight.Text) * fuelsurchargetrain);
                            break;
                    }
                    hdnfuelsurchargebas.Value = fuelsurchargebastrain;
                    hdnfuelsurchargerate.Value = fuelsurchargetrain.ToString("F2");
                    hdnfuelsurmincharge.Value = fuelminchargetrain.ToString("F2");
                    hdnfuelsurmaxcharge.Value = fuelmaxchargetrain.ToString("F2");
                    break;
                case "4":
                    switch (fuelsurchargebasexpress)
                    {
                        case "F":
                            totalsurcharge = fuelsurchargeexpress;
                            break;
                        case "%":
                            totalsurcharge = (freightchrg * fuelsurchargeexpress) / 100;
                            break;
                        case "W":
                            totalsurcharge = (returnDouble(txttotchargedweight.Text) * fuelsurchargeexpress);
                            break;
                    }
                    hdnfuelsurchargebas.Value = fuelsurchargebasexpress;
                    hdnfuelsurchargerate.Value = fuelsurchargeexpress.ToString("F2");
                    hdnfuelsurmincharge.Value = fuelminchargeexpress.ToString("F2");
                    hdnfuelsurmaxcharge.Value = fuelmaxchargeexpress.ToString("F2");
                    break;
            } // TRANS MODE SWITCH
        }

        // APPLYING MIN-MAX
        switch (cmbtransmode.SelectedValue)
        {
            case "1":
                if (totalsurcharge < fuelminchargeair)
                    totalsurcharge = fuelminchargeair;
                if (totalsurcharge > fuelmaxchargeair)
                    totalsurcharge = fuelmaxchargeair;

                break;
            case "2":
                if (totalsurcharge < fuelminchargeroad)
                    totalsurcharge = fuelminchargeroad;
                if (totalsurcharge > fuelmaxchargeroad)
                    totalsurcharge = fuelmaxchargeroad;

                break;
            case "3":
                if (totalsurcharge < fuelminchargetrain)
                    totalsurcharge = fuelminchargetrain;
                if (totalsurcharge > fuelmaxchargetrain)
                    totalsurcharge = fuelmaxchargetrain;
                break;
            case "4":
                if (totalsurcharge < fuelminchargeexpress)
                    totalsurcharge = fuelminchargeexpress;
                if (totalsurcharge > fuelmaxchargeexpress)
                    totalsurcharge = fuelmaxchargeexpress;
                break;

        }

        return totalsurcharge.ToString("F2");
    }

    protected void loadMPSources()
    {
        string sqlstr = "SELECT dockno,dockdt FROM webx_master_docket ";
        sqlstr = sqlstr + " WHERE convert(varchar,dockdt,106)='" + fn.Mydate1(txtdockdate.Text.Trim()) + "' AND service_class='2' ORDER BY dockno";
        SqlDataReader dr = SqlHelper.ExecuteReader(SessionUtilities.ConnectionString, CommandType.Text, sqlstr);
        int cnt = 0;
        TableRow tr = new TableRow();
        tr.BackColor = System.Drawing.Color.White;
        while (dr.Read())
        {
            if (cnt == 0)
            {
                tr = new TableRow();
                tr.BackColor = System.Drawing.Color.White;
            }

            TableCell tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.VerticalAlign = VerticalAlign.Middle;
            tc.BackColor = System.Drawing.Color.White;
            LinkButton lnk = new LinkButton();
            lnk.CssClass = "blackfnt";
            lnk.Text = dr["dockno"].ToString();
            lnk.Attributes.Add("onclick", "javascript:return selectMPSource('" + dr["dockno"].ToString() + "')");
            lnk.Attributes.Add("onmouseover", "this.style.cursor='pointer'");
            lnk.ToolTip = "Click to Select Source " + hdndockname.Value;
            tc.Controls.Add(lnk);
            tr.Cells.Add(tc);

            cnt++;
            if (cnt == 5)
            {
                tblsources.Rows.Add(tr);
                cnt = 0;
            }
        }
        dr.Close();

        if (cnt > 0)
        {
            for (int i = 0; i < 5 - cnt; i++)
            {
                TableCell tc = new TableCell();
                tc.HorizontalAlign = HorizontalAlign.Center;
                tr.Cells.Add(tc);
            }
            tblsources.Rows.Add(tr);
        }
    }

    protected void setStaxPaidBy()
    {
        cmbstaxpaidby.Items.Clear();
        try
        {
            string strstaxpaidby = DocketRules.GetDefaultValue("STAX_PAIDBY", cmbpaybas.SelectedValue);

            string[] arrstaxpaidby = new string[3];
            arrstaxpaidby = strstaxpaidby.Split('|');

            string[] arrdefaults = new string[4];
            arrdefaults = arrstaxpaidby[1].Split(',');

            string[] arrenabled = new string[4];
            arrenabled = arrstaxpaidby[2].Split(',');

            if (arrstaxpaidby[0].IndexOf(cmbtransmode.SelectedValue + "CO") >= 0)
                cmbstaxpaidby.Items.Add(new ListItem("Consignor", "CO"));

            if (arrstaxpaidby[0].IndexOf(cmbtransmode.SelectedValue + "CE") >= 0)
                cmbstaxpaidby.Items.Add(new ListItem("Consignee", "CO"));

            if (arrstaxpaidby[0].IndexOf(cmbtransmode.SelectedValue + "P") >= 0)
                cmbstaxpaidby.Items.Add(new ListItem("Billing Party", "P"));

            if (arrstaxpaidby[0].IndexOf(cmbtransmode.SelectedValue + "T") >= 0)
                cmbstaxpaidby.Items.Add(new ListItem("Transporter", "T"));

            try
            {
                cmbstaxpaidby.SelectedValue = arrdefaults[Convert.ToInt16(cmbtransmode.SelectedValue) - 1];
            }
            catch (Exception ex) { }

            if (arrenabled[Convert.ToInt16(cmbtransmode.SelectedValue) - 1].CompareTo("Y") == 0)
                cmbstaxpaidby.Enabled = true;
            else
                cmbstaxpaidby.Enabled = false;
        }
        catch (Exception ex) { }
    }

    protected void checkFreightLimits()
    {
        double minfreight = 0.00;
        if (hdnminfreighttype.Value.CompareTo("B") == 0)
        {
            double pkgsno = 0, chargedweight = 0, ton = 0, freightrate = 0, freightcharge = 0;
            pkgsno = DocketController.ToDouble(hdntotnopkgs.Value);
            chargedweight = DocketController.ToDouble(txttotchargedweight.Text);
            ton = DocketController.ToDouble(txttotchargedweight.Text) / 1000;
            freightcharge = DocketController.ToDouble(txtfreightcharge.Text);
            freightrate = DocketController.ToDouble(txtfreightrate.Text);

            if (hdnminfreightbas.Value.CompareTo("RS") == 0)
            {
                if (freightcharge < DocketController.ToDouble(hdnminfreightbasrate.Value))
                {
                    freightcharge = DocketController.ToDouble(hdnminfreightbasrate.Value);
                    lblminfreightalert.Text = "Minimm Freight Base Wise Flat (in RS) Applied.";
                    lblminfreightalert.Style["display"] = "block";
                    cmbratetype.SelectedValue = "F";
                }
            }
            else if (hdnminfreightbas.Value.CompareTo("KG") == 0)
            {
                if (chargedweight < DocketController.ToDouble(hdnminfreightbasrate.Value))
                {
                    lblminfreightalert.Style["display"] = "block";
                    chargedweight = DocketController.ToDouble(hdnminfreightbasrate.Value);
                    txttotchargedweight.Text = chargedweight.ToString("F0");
                    lblminfreightalert.Text = "Minimm Freight Base Wise Min " + chargedweight.ToString("F0") + " KGs Applied.";
                }
            }
            else if (hdnminfreightbas.Value.CompareTo("PKG") == 0)
            {
                if (pkgsno < DocketController.ToDouble(hdnminfreightbasrate.Value))
                {
                    lblminfreightalert.Style["display"] = "block";
                    pkgsno = DocketController.ToDouble(hdnminfreightbasrate.Value);
                    hdntotnopkgs.Value = pkgsno.ToString("F0");
                    lblminfreightalert.Text = "Minimm Freight Base Wise Min " + pkgsno.ToString("F0") + " PKGs Applied.";

                }
            }
            else if (hdnminfreightbas.Value.CompareTo("T") == 0)
            {
                if (ton < DocketController.ToDouble(hdnminfreightbasrate.Value))
                {

                    lblminfreightalert.Style["display"] = "block";
                    ton = DocketController.ToDouble(hdnminfreightbasrate.Value);
                    chargedweight = ton * 1000;
                    txttotchargedweight.Text = chargedweight.ToString("F0");
                    lblminfreightalert.Text = "Minimm Freight Base Wise Min " + ton.ToString("F2") + " TONs Applied.";
                }
            }

            if (cmbratetype.SelectedValue.CompareTo("F") == 0)
            {
                txtfreightcharge.Text = freightcharge.ToString("F2");
                txtfreightrate.Text = freightcharge.ToString("F2");
            }
            else if (cmbratetype.SelectedValue.CompareTo("W") == 0)
            {
                freightcharge = chargedweight * freightrate;
                txttotchargedweight.Text = chargedweight.ToString("F0");
                txtfreightcharge.Text = freightcharge.ToString("F2");
                txtfreightrate.Text = freightrate.ToString("F2");
            }
            else if (cmbratetype.SelectedValue.CompareTo("P") == 0)
            {
                freightcharge = pkgsno * freightrate;
                hdntotnopkgs.Value = pkgsno.ToString("F0");
                txtfreightcharge.Text = freightcharge.ToString("F2");
                txtfreightrate.Text = freightrate.ToString("F2");
            }
            else if (cmbratetype.SelectedValue.CompareTo("T") == 0)
            {
                freightcharge = ton * freightrate;
                txttotchargedweight.Text = chargedweight.ToString("F0");
                txtfreightcharge.Text = freightcharge.ToString("F2");
                txtfreightrate.Text = freightrate.ToString("F2");
            }

        }
        else if (hdnminfreighttype.Value.CompareTo("%") == 0)
        {
            minfreight = WebXConvert.ToDouble(hdnminfreightrate.Value);

            if (WebXConvert.ToDouble(txtfreightcharge.Text) < minfreight)
            {
                txtfreightcharge.Text = minfreight.ToString("F2");
                if (cmbratetype.SelectedValue.CompareTo("W") == 0)
                    txtfreightrate.Text = WebXConvert.ToDouble(Convert.ToString(WebXConvert.ToDouble(txtfreightcharge.Text) / WebXConvert.ToInt64(txttotchargedweight.Text))).ToString("F2");
                else if (cmbratetype.SelectedValue.CompareTo("P") == 0)
                    txtfreightrate.Text = WebXConvert.ToDouble(Convert.ToString(WebXConvert.ToDouble(txtfreightcharge.Text) * WebXConvert.ToInt64(hdntotnopkgs.Value))).ToString("F2");
                else if (cmbratetype.SelectedValue.CompareTo("T") == 0)
                    txtfreightrate.Text = WebXConvert.ToDouble(Convert.ToString(WebXConvert.ToDouble(txtfreightcharge.Text) / ((WebXConvert.ToInt64(txttotchargedweight.Text) * 1000)))).ToString("F2");
                else if (cmbratetype.SelectedValue.CompareTo("F") == 0)
                    txtfreightrate.Text = txtfreightcharge.Text;

                lblminfreightalert.Text = "Minimm Freight Percent Wise applied. Minimum Amount " + minfreight.ToString("F2") + " RS Applied.";
                lblminfreightalert.Style["display"] = "block";
            }
        }
        else
        {
            Response.Redirect("../ErrorPage.aspx?heading=Minimum Freight Not Set Please complete Contract &detail1=Problem Occured on Payment Click Button&detail2=" + "&suggestion2=" + dkt_call + " Entry Failed");
            return;
        }
    }

    protected void btndockcri_Click(object sender, EventArgs e)
    {

    }

    protected void applyComputerization()
    {
        string flag_parcel = DocketRules.GetDefaultValue("FLAG_COMPUTERISED");
        bool flag_computerized = DataValidations.IsComputerizedLocation(lblorgnloc.Text);

        if (flag_parcel.CompareTo("Y") == 0 && flag_computerized == true)
        {
            optentrytype.SelectedValue = "C";
            txtdockno.Style["display"] = "none";
        }
        else
        {
            optentrytype.SelectedValue = "N";
            lbldockno.Text = "";
            trentrytype.Style["display"] = "none";
        }
    }

    protected void applyCODDODSetting()
    {
        // CODDOD SETTINGS
        if (chkcoddod.Checked == true)
            trcoddod.Style["display"] = "marker";
        else
        {
            txtcoddodcharged.Text = "0.00";
            txtcoddodcollected.Text = "0.00";
            trcoddod.Style["display"] = "none";
        }
    }

    protected void applyDACCSetting()
    {
        if (chkdacc.Checked == true)
            trdacccharge.Style["display"] = "marker";
        else
        {
            txtdacccharged.Text = "0.00";
            txtdacccollected.Text = "0.00";
            trdacccharge.Style["display"] = "none";
        }
    }

    protected void disableStep1()
    {
        txtdockno.Enabled = false;
        txtdockdate.Enabled = false;
        cmbpaybas.Enabled = false;
        txtpartycode.Enabled = false;
        txtdelloc.Enabled = false;
        optentrytype.Enabled = false;
        btndelpopup.Disabled = true;
        btnpartypopup.Disabled = true;

        trtransmode.Style["display"] = "marker";
        trpickupdel.Style["display"] = "marker";
        trpacktype.Style["display"] = "marker";
        trvolumetric.Style["display"] = "marker";
        trlocal.Style["display"] = "marker";
        trdacc.Style["display"] = "marker";
        trbtnstep2.Style["display"] = "marker";
        trbtnstep1.Style["display"] = "none";

        // SETTING CONSIGNOR/CONSIGNEE
        divpartycsgne.Style["display"] = "none";
        if (optpartycsgn.Checked == true)
        {
            txtcsgncd.Text = txtpartycode.Text;
            txtcsgnnm.Text = txtpartyname.Text;
        }
        else
        {
            txtcsgecd.Text = txtpartycode.Text;
            txtcsgenm.Text = txtpartyname.Text;
        }

        // LOCAL DOCKET SETTINGS 
        if (lblorgnloc.Text.ToUpper().CompareTo(txtdelloc.Text.ToUpper()) == 0)
            chklocal.Enabled = true;
        else
        {
            chklocal.Checked = false;
            chklocal.Enabled = false;
        }
    }

    protected void disableStep2()
    {
        cmbtransmode.Enabled = false;
        cmbservicetype.Enabled = false;
        cmbftltype.Enabled = false;
        cmbpickupdel.Enabled = false;
        txtfrom.Enabled = false;
        txtto.Enabled = false;
        cmbpacktype.Enabled = false;
        cmbprodtype.Enabled = false;
        txtsplinstruct.Enabled = false;
        chkvolumetric.Enabled = false;
        chkcoddod.Enabled = false;
        chkdacc.Enabled = false;
        chklocal.Enabled = false;
        chkpermit.Enabled = false;
        cmbloadtype.Enabled = false;
        txtdopino.Enabled = false;
        cmbbusinesstype.Enabled = false;
        chkdacc.Enabled = false;
        chkmultipickup.Enabled = false;
        chkmultidelivery.Enabled = false;
        txtsourcedockno.Enabled = false;

        btnfrompopup.Disabled = true;
        btntopopup.Disabled = true;

        trbtnstep2.Style["display"] = "none";
        imgsourcedock.Style["display"] = "none";
        divstep4.Style["display"] = "marker";
        divpayment.Style["display"] = "marker";
        trbtnstep3.Style["display"] = "none";

        if (chkpermit.Checked == true)
            tblpermit.Style["display"] = "marker";
        else
            tblpermit.Style["display"] = "none";

        if (chkmultipickup.Checked == true)
        {
            txtsourcedockno.Style["display"] = "block";
            trmultipoint.Style["display"] = "block";
            lblsourcedockname.Style["display"] = "block";
        }

        divstep3.Style["display"] = "marker";
    }

    protected void disableStep3()
    {
        txtcsgncd.Enabled = false;
        txtcsgnaddress.Enabled = false;
        txtcsgncity.Enabled = false;
        txtcsgnemail.Enabled = false;
        txtcsgnname.Enabled = false;
        txtcsgnnm.Enabled = false;
        txtcsgnphone.Enabled = false;
        txtcsgnpin.Enabled = false;
        btncsgncitypopup.Disabled = true;

        txtcsgecd.Enabled = false;
        txtcsgeaddress.Enabled = false;
        txtcsgecity.Enabled = false;
        txtcsgeemail.Enabled = false;
        txtcsgename.Enabled = false;
        txtcsgenm.Enabled = false;
        txtcsgephone.Enabled = false;
        txtcsgepin.Enabled = false;
        btncsgecitypopup.Disabled = true;

        optcarrierrisk.Enabled = false;
        optownerrisk.Enabled = false;
        txtpolicydate.Enabled = false;
        txtpolicyno.Enabled = false;
        txtprivatemark.Enabled = false;
        txtinternalcovers.Enabled = false;
        txtmodvatcovers.Enabled = false;
        txtcustrefno.Enabled = false;
        txttpnumber.Enabled = false;

        btncsgnpopup.Disabled = true;
        btncsgepopup.Disabled = true;

        foreach (GridViewRow grv in grvdoc.Rows)
        {
            ((TextBox)grv.FindControl("txtdocumentno")).Enabled = false;
        }

        lblpermitdate.Enabled = false;
        lblpermitvaliditydate.Enabled = false;
        lblpermitrecieveddate.Enabled = false;

        txtpermitdate.Enabled = false;
        txtpermitrecvddate.Enabled = false;
        txtpermitvaldate.Enabled = false;
    }

    protected void disableStep4()
    {
        txtcubicrate.Enabled = false;
        txtinvoicerows.Enabled = false;
        btnaddrows.Enabled = false;

        foreach (GridViewRow grv in grvinvoice.Rows)
        {
            ((TextBox)grv.FindControl("txtinvoiceno")).Enabled = false;
            ((TextBox)grv.FindControl("txtinvoicedate")).Enabled = false;
            ((LinkButton)grv.FindControl("lnkinvoicedate")).Enabled = false;
            ((TextBox)grv.FindControl("txtlength")).Enabled = false;
            ((TextBox)grv.FindControl("txtbreadth")).Enabled = false;
            ((TextBox)grv.FindControl("txtheight")).Enabled = false;
            ((TextBox)grv.FindControl("txtdeclval")).Enabled = false;
            ((TextBox)grv.FindControl("txtnopkgs")).Enabled = false;
            ((TextBox)grv.FindControl("txtactualweight")).Enabled = false;
        }
        txttotchargedweight.Enabled = false;
    }

    protected void applyMSTRCSGNERule(string flagmstrcsgn, string flagmstrcsge)
    {
        if (flagmstrcsgn.CompareTo("Y") == 0)
        {
            if (cmbpaybas.SelectedValue.CompareTo("P02") == 0)
            {
                if (optpartycsgn.Checked == true)
                {
                    txtcsgnname.Enabled = false;
                    txtcsgnaddress.Enabled = false;
                    txtcsgncity.Enabled = false;
                    txtcsgnpin.Enabled = false;
                    txtcsgnphone.Enabled = false;
                    txtcsgnemail.Enabled = false;

                    txtcsgnname.BackColor = System.Drawing.Color.FromName("#e6e6e6");
                    txtcsgnaddress.BackColor = System.Drawing.Color.FromName("#e6e6e6");
                    txtcsgncity.BackColor = System.Drawing.Color.FromName("#e6e6e6");
                    txtcsgnpin.BackColor = System.Drawing.Color.FromName("#e6e6e6");
                    txtcsgnphone.BackColor = System.Drawing.Color.FromName("#e6e6e6");
                    txtcsgnemail.BackColor = System.Drawing.Color.FromName("#e6e6e6");
                }
            }
            else
            {
                txtcsgnname.Enabled = false;
                txtcsgnaddress.Enabled = false;
                txtcsgncity.Enabled = false;
                txtcsgnpin.Enabled = false;
                txtcsgnphone.Enabled = false;
                txtcsgnemail.Enabled = false;

                txtcsgnname.BackColor = System.Drawing.Color.FromName("#e6e6e6");
                txtcsgnaddress.BackColor = System.Drawing.Color.FromName("#e6e6e6");
                txtcsgncity.BackColor = System.Drawing.Color.FromName("#e6e6e6");
                txtcsgnpin.BackColor = System.Drawing.Color.FromName("#e6e6e6");
                txtcsgnphone.BackColor = System.Drawing.Color.FromName("#e6e6e6");
                txtcsgnemail.BackColor = System.Drawing.Color.FromName("#e6e6e6");
            }
        }

        if (flagmstrcsge.CompareTo("Y") == 0)
        {

            if (cmbpaybas.SelectedValue.CompareTo("P02") == 0)
            {
                if (optpartycsge.Checked == true)
                {
                    txtcsgename.Enabled = false;
                    txtcsgeaddress.Enabled = false;
                    txtcsgecity.Enabled = false;
                    txtcsgepin.Enabled = false;
                    txtcsgephone.Enabled = false;
                    txtcsgeemail.Enabled = false;

                    txtcsgename.BackColor = System.Drawing.Color.FromName("#e6e6e6");
                    txtcsgeaddress.BackColor = System.Drawing.Color.FromName("#e6e6e6");
                    txtcsgecity.BackColor = System.Drawing.Color.FromName("#e6e6e6");
                    txtcsgepin.BackColor = System.Drawing.Color.FromName("#e6e6e6");
                    txtcsgephone.BackColor = System.Drawing.Color.FromName("#e6e6e6");
                    txtcsgeemail.BackColor = System.Drawing.Color.FromName("#e6e6e6");
                }
            }
            else
            {
                txtcsgename.Enabled = false;
                txtcsgeaddress.Enabled = false;
                txtcsgecity.Enabled = false;
                txtcsgepin.Enabled = false;
                txtcsgephone.Enabled = false;
                txtcsgeemail.Enabled = false;

                txtcsgename.BackColor = System.Drawing.Color.FromName("#e6e6e6");
                txtcsgeaddress.BackColor = System.Drawing.Color.FromName("#e6e6e6");
                txtcsgecity.BackColor = System.Drawing.Color.FromName("#e6e6e6");
                txtcsgepin.BackColor = System.Drawing.Color.FromName("#e6e6e6");
                txtcsgephone.BackColor = System.Drawing.Color.FromName("#e6e6e6");
                txtcsgeemail.BackColor = System.Drawing.Color.FromName("#e6e6e6");
            }
        }
    }


}



