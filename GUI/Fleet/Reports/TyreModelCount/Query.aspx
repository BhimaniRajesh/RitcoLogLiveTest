<%@ Page Language="C#" MasterPageFile="~/GUI/MasterPage.master" AutoEventWireup="true" CodeFile="Query.aspx.cs" Inherits="GUI_Fleet_Reports_TyreModelCount_Query" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MyCPH1" Runat="Server">
<script src="../../../images/commonJs.js"  language="javascript" type="text/javascript"></script>
<script src="../../../images/CalendarPopup.js"  language="javascript" type="text/javascript"></script>
<script language="javascript" type="text/javascript">  
    function nwOpen()
    {
        window.open("Popup_ModelType.aspx?","pbr","menubar=no,toolbar=no,location=no,resizable=yes,scrollbars=yes,width=387,height=220,status=no,left=220,top=70");
    }
     function nwOpen1()
    {
        window.open("Popup-Vehicle.aspx?","pbr","menubar=no,toolbar=no,location=no,resizable=yes,scrollbars=yes,width=387,height=220,status=no,left=220,top=70");
    }
</script>

<script language="javascript" type="text/javascript">
    var cal = new CalendarPopup("testdiv1"); 
    cal.setCssPrefix("TEST");
    cal.showNavigationDropdowns();
    
    function OnSub_DATACHECK()
    {
      var txtModel=document.getElementById("ctl00_MyCPH1_txtModel");
      var txtVehNo=document.getElementById("ctl00_MyCPH1_txtVehNo");
      var hfVehIntId=document.getElementById("ctl00_MyCPH1_hfVehIntId");
       //alert(hfVehIntId.value);
        window.open("Result.aspx?TyreModel=" + txtModel.value + "&Vehicle=" + txtVehNo.value + "");
        return false;
    }
</script>
rtasre
 <div id="testdiv1" style="position: absolute; visibility: hidden; background-color: white;
    layer-background-color: white; z-index: 99;">
</div>

<asp:UpdateProgress ID="uppMain" runat="server">
<ProgressTemplate>
    <iframe frameborder="0" src="about:blank"
        style="border:0px;position:absolute;z-index:9;left:0px;top:0px;width:expression(this.offsetParent.scrollWidth);height:expression(this.offsetParent.scrollHeight);filter:progid:DXImageTransform.Microsoft.Alpha(Opacity=75, FinishOpacity=0, Style=0, StartX=0, FinishX=100, StartY=0, FinishY=100);"></iframe>
        <div style="position:absolute;z-index:10;left:expression((this.offsetParent.clientWidth/2)-(this.clientWidth/2)+this.offsetParent.scrollLeft);top:expression((this.offsetParent.clientHeight/2)-(this.clientHeight/2)+this.offsetParent.scrollTop);"><img src="../../../images/loading.gif" /><font face=verdana color=blue size=4>&nbsp;<b>Please Wait...</b></font></div>
</ProgressTemplate>
</asp:UpdateProgress>

<table cellspacing="1" style="width: 100%">
        <tr style="background-color: white">
    <td align="left">
                <asp:Label ID="lblPageHead" CssClass="blackfnt" Font-Bold="True" runat="server">Fleet Module  > Reports >New Tyre Stock</asp:Label>
            </td> 
            
             <td align="right">

          </td>      
        </tr>
    </table>
    
    <hr align="center" size="1" color="#8ba0e5">
      <br> 
    
    <table cellspacing="1" cellpadding="2" style="width:700px" class="boxbg">
    
            <tr class="bgbluegrey">
                <td colspan="3" align="center">
                    <asp:Label ID="Label3" CssClass="blackfnt" Font-Bold="true" runat="server">Search Criteria</asp:Label>
                </td>
            </tr>
           <tr style="background-color: white">
                <td align="right" valign="center" style="width: 154px; text-align: left;">
                <asp:Label ID="Label5" CssClass="blackfnt" runat="server" Font-Bold="True">Tyre Model</asp:Label>
                </td>
                <td align="left" colspan="2">
                <asp:TextBox ID="txtModel" runat="server"></asp:TextBox>&nbsp;
                <ajaxToolkit:AutoCompleteExtender
                            runat="server" 
                            BehaviorID="AutoCompleteEx1"
                            ID="autoComplete5" 
                            TargetControlID="txtModel"
                            ServicePath="~/services/FleetAutoComplet.asmx" 
                            ServiceMethod="GetTyreModelNo"
                            MinimumPrefixLength="1" 
                            CompletionInterval="1000"
                            EnableCaching="true"
                            CompletionSetCount="20"
                            CompletionListCssClass="autocomplete_completionListElement" 
                            CompletionListItemCssClass="autocomplete_listItem" 
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                            DelimiterCharacters=";, :"
                            ShowOnlyCurrentWordInCompletionListItem="true" >
                            <Animations>
                                    <OnShow>
                                    <Sequence>
                                    <%-- Make the completion list transparent and then show it --%>
                                    <OpacityAction Opacity="0" />
                                    <HideAction Visible="true" />

                                    <%--Cache the original size of the completion list the first time
                                    the animation is played and then set it to zero --%>
                                    <ScriptAction Script="
                                    // Cache the size and setup the initial size
                                    var behavior = $find('AutoCompleteEx1');
                                    if (!behavior._height) {
                                    var target = behavior.get_completionList();
                                    behavior._height = target.offsetHeight - 2;
                                    target.style.height = '0px';
                                    }" />

                                    <%-- Expand from 0px to the appropriate size while fading in --%>
                                    <Parallel Duration=".4">
                                    <FadeIn />
                                    <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteEx1')._height" />
                                    </Parallel>
                                    </Sequence>
                                    </OnShow>
                                    <OnHide>
                                    <%-- Collapse down to 0px and fade out --%>
                                    <Parallel Duration=".4">
                                    <FadeOut />
                                    <Length PropertyKey="height" StartValueScript="$find('AutoCompleteEx1')._height" EndValue="0" />
                                    </Parallel>
                                    </OnHide>
                            </Animations>
                            </ajaxToolkit:AutoCompleteExtender>
                   <input id="MODEL_NO" runat="server"   type="button" value="..." onclick="javascript:nwOpen('Popup_ModelType.aspx',null,'height=250,width=300,left=350,top=200,status=no,resizable=no,scrollbars=yes,toolbar=no,location=no,menubar=no');" />
                <%--<asp:Label ID="lblPopup" runat="server" BorderStyle="Groove" TabIndex="18" Text='<a href="javascript:nwOpen(1)">...</a>'></asp:Label> --%>
                </td>
              </tr>
              <tr style="background-color: white">
                <td align="right" valign="center" style="width: 154px; text-align: left;">
                <asp:Label ID="Label1" CssClass="blackfnt" runat="server" Font-Bold="True">Vehicle No</asp:Label>
                </td>
                <td align="left" colspan="2">
                <asp:TextBox ID="txtVehNo" runat="server"></asp:TextBox>
                <asp:HiddenField id="hfVehIntId" runat="server" />
                <ajaxToolkit:AutoCompleteExtender
                            runat="server" 
                            BehaviorID="AutoCompleteEx2"
                            ID="AutoCompleteExtender1" 
                            TargetControlID="txtVehNo"
                            ServicePath="~/services/FleetAutoComplet.asmx" 
                            ServiceMethod="GetVehicleName"
                            MinimumPrefixLength="1" 
                            CompletionInterval="1000"
                            EnableCaching="true"
                            CompletionSetCount="20"
                            CompletionListCssClass="autocomplete_completionListElement" 
                            CompletionListItemCssClass="autocomplete_listItem" 
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                            DelimiterCharacters=";, :"
                            ShowOnlyCurrentWordInCompletionListItem="true" >
                            <Animations>
                                    <OnShow>
                                    <Sequence>
                                    <%-- Make the completion list transparent and then show it --%>
                                    <OpacityAction Opacity="0" />
                                    <HideAction Visible="true" />

                                    <%--Cache the original size of the completion list the first time
                                    the animation is played and then set it to zero --%>
                                    <ScriptAction Script="
                                    // Cache the size and setup the initial size
                                    var behavior = $find('AutoCompleteEx2');
                                    if (!behavior._height) {
                                    var target = behavior.get_completionList();
                                    behavior._height = target.offsetHeight - 2;
                                    target.style.height = '0px';
                                    }" />

                                    <%-- Expand from 0px to the appropriate size while fading in --%>
                                    <Parallel Duration=".4">
                                    <FadeIn />
                                    <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteEx2')._height" />
                                    </Parallel>
                                    </Sequence>
                                    </OnShow>
                                    <OnHide>
                                    <%-- Collapse down to 0px and fade out --%>
                                    <Parallel Duration=".4">
                                    <FadeOut />
                                    <Length PropertyKey="height" StartValueScript="$find('AutoCompleteEx2')._height" EndValue="0" />
                                    </Parallel>
                                    </OnHide>
                            </Animations>
                            </ajaxToolkit:AutoCompleteExtender>
                   <%--<input id="VehNo" runat="server"   type="button" value="..."  />--%>
                   <asp:Button ID="btn_PopUp1" runat="server" Text="..." /> 
                <%--<asp:Label ID="lblPopup" runat="server" BorderStyle="Groove" TabIndex="18" Text='<a href="javascript:nwOpen(1)">...</a>'></asp:Label> --%>
                </td>
              </tr>
             
            
              <tr style="background-color: white">
               <td align="right" style="vertical-align: top; width: 154px; text-align: left">
               </td>
               <td align="left" colspan="2" style="text-align: right">
                <asp:UpdatePanel ID="UpdatePanel8" UpdateMode="Conditional" RenderMode="Inline" runat="server">
                    <ContentTemplate>
                       <%-- <asp:Button ID="btnShow" runat="server" Text="Submit" OnClientClick="javascript:return Validation()"  OnClick="btnShow_Click" /> --%>
                        <asp:Button ID="btnShow" runat="server" Text="Submit"  OnClientClick="Javascript: return OnSub_DATACHECK();"    /> 
                    </ContentTemplate>
                </asp:UpdatePanel>
               </td>
           </tr>

       </table>
        
      <br/>

</asp:Content>  

