namespace DateSelectorCSharp.DateSelector
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	
	
	/// <summary>
	///		Summary description for DateSelector.
	/// </summary>
	public abstract class DateSelector : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lbl_Date;
		protected System.Web.UI.WebControls.TextBox txt_Date;
		protected System.Web.UI.WebControls.Image imgCalendar;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			string scriptStr = "javascript:return popUpCalendar(this," + getClientID() + @", 'mm/dd/yyyy', '__doPostBack(\'" + getClientID() + @"\')')";
			imgCalendar.Attributes.Add("onclick", scriptStr);
		}


		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		// Get the id of the control rendered on client side
		// Very essential for Javascript Calendar scripts to locate the textbox
		public string getClientID()
		{
			return txt_Date.ClientID;
			}

		// This propery sets/gets the calendar date
		public string CalendarDate
		{
			get
			{
				return txt_Date.Text;
			}
			set
			{
				txt_Date.Text = value;
			}
		}
		// This Property sets or gets the the label for 
		// Dateselector user control
		public string Text
		{
			get
			{
				return lbl_Date.Text;
			}
			set
			{
				lbl_Date.Text = value;
			}
		}

	}

}
