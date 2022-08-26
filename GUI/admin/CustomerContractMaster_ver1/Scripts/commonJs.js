

function IsValidTime(timeStr) {
// Checks if time is in HH:MM:SS AM/PM format.
if (timeStr!="----"){
	var timePat = /^(\d{1,2}):(\d{2})(:(\d{2}))?(\s?(AM|am|PM|pm))?$/;
	var matchArray = timeStr.match(timePat);
	if (matchArray == null) {
	alert("Time is not in a valid format\n Enter in hh:mm format e.g. 23:45");
	return false;
}

hour = matchArray[1];
minute = matchArray[2];

if (hour < 0  || hour > 23) {
	alert("Hour must be between 0 and 23");
	return false;
}

if (minute<0 || minute > 59) {
	alert ("Minute must be between 0 and 59.");
	return false;
}
return true;
}
}

// Declaring valid date character, minimum year and maximum year
var dtCh= "/";
var minYear=1900;
var maxYear=2100;

function isInteger(s){
	var i;
    for (i = 0; i < s.length; i++){   
        // Check that current character is number.
        var c = s.charAt(i);
        if (((c < "0") || (c > "9"))) return false;
    }
    // All characters are numbers.
    return true;
}

function stripCharsInBag(s, bag){
	var i;
    var returnString = "";
    // Search through string's characters one by one.
    // If character is not in bag, append to returnString.
    for (i = 0; i < s.length; i++){   
        var c = s.charAt(i);
        if (bag.indexOf(c) == -1) returnString += c;
    }
    return returnString;
}

function daysInFebruary (year){
	// February has 29 days in any year evenly divisible by four,
    // EXCEPT for centurial years which are not also divisible by 400.
    return (((year % 4 == 0) && ( (!(year % 100 == 0)) || (year % 400 == 0))) ? 29 : 28 );
}

function DaysArray(n) {
	for (var i = 1; i <= n; i++) {
		this[i] = 31
		if (i==4 || i==6 || i==9 || i==11) {this[i] = 30}
		if (i==2) {this[i] = 29}
   } 
   return this
}

function isDate(dtStr){
	var daysInMonth = DaysArray(12)
	var pos1=dtStr.indexOf(dtCh)
	var pos2=dtStr.indexOf(dtCh,pos1+1)
	var strDay=dtStr.substring(0,pos1)
	var strMonth=dtStr.substring(pos1+1,pos2)
	var strYear=dtStr.substring(pos2+1)
	strYr=strYear
	if (strDay.charAt(0)=="0" && strDay.length>1) strDay=strDay.substring(1)
	if (strMonth.charAt(0)=="0" && strMonth.length>1) strMonth=strMonth.substring(1)
	for (var i = 1; i <= 3; i++) {
		if (strYr.charAt(0)=="0" && strYr.length>1) strYr=strYr.substring(1)
	}
	month=parseInt(strMonth)
	day=parseInt(strDay)
	year=parseInt(strYr)
	if (pos1==-1 || pos2==-1){
		alert("The date format should be : dd/mm/yyyy")
		return false
	}
	if (strMonth.length<1 || month<1 || month>12){
		alert("Please enter a valid month")
		return false
	}
	if (strDay.length<1 || day<1 || day>31 || (month==2 && day>daysInFebruary(year)) || day > daysInMonth[month]){
		alert("Please enter a valid day")
		return false
	}
	if (strYear.length != 4 || year==0 || year<minYear || year>maxYear){
		alert("Please enter a valid 4 digit year between "+minYear+" and "+maxYear)
		return false
	}
	if (dtStr.indexOf(dtCh,pos2+1)!=-1 || isInteger(stripCharsInBag(dtStr, dtCh))==false){
		alert("Please enter a valid date")
		return false
	}
	return true
}

function ValidateForm(obj){
	if (isDate(obj.value)==false){
		obj.focus()
		return false
	}
    return true
}

function toggleBox(szDivID, iState){ // 1 visible, 0 hidden
	if(document.layers){	   //NN4+
		document.layers[szDivID].visibility = iState ? "show" : "hide";
	}
	else if(document.getElementById){	  //gecko + IE 5+
		var obj = document.getElementById(szDivID);
		obj.style.visibility = iState ? "visible" : "hidden";
	} else if(document.all){	// IE 4
		document.all[szDivID].style.visibility = iState ? "visible" : "hidden";
	}

	// obj.visibility = document.layers ? iState ? "show" : "hide" : iState ? "visible" : "hidden";
}

function checkbook(strbook,strbook1,strdely,strdely1){
	if(strbook.checked==true){
	 strbook1.value="Yes"
	 if (strbook1.value=="Yes" || strdely1.value=="Yes") {
		toggleBox('demodiv',1)
	 }
	} else{
		strbook1.value="No"
		if (strbook1.value=="No" && strdely1.value=="No"){
			toggleBox('demodiv',0)
		}
	}

	if(strdely.checked==true){
	 strdely1.value="Yes"
	 if (strbook1.value=="Yes" || strdely1.value=="Yes"){
		toggleBox('demodiv',1)
	 }
	} else {
	 strdely1.value="No"
	 if (strbook1.value=="No" && strdely1.value=="No"){
		toggleBox('demodiv',0)
	 }
	}
}


function check(str,str1){
	if(str.checked==true){
	 str1.value="Yes"
	 if (str.name=='computerised'){
		document.cargo.dataentry.value=""
		document.cargo.dataentry.disabled=true
	 }
	}else{
	 str1.value="No"
	 if (str.name=='computerised'){
		document.cargo.dataentry.disabled=false
	 }
	 if (str.name=='cutoff_YN'){
		document.cargo.bkg_cutofftime.value=""
	 }
	}
}


function Falphanum(obj){
  var uid=obj.value 
  //var str1="'"
  var str1="'!@#$%^&*()-+~`/\.[]{}:"
  flag="0"
	for(i=0;i<uid.length;i++){
		   for(j=0;j<str1.length;j++){
					if(uid.charAt(i)==str1.charAt(j)){
							flag="1";
							break;
					}else {
							//flag="0"
					}
					if(flag=="1"){
						break;
					}
		 	}
	}
	if(flag=="1"){
		alert("Please Enter Alphabetic or Numeric Values only\n You cannot enter special characters for Location Code")
		obj.focus();
		return false;
	}
						
}

//Added By Chaitanya

	function createXMLHTTPObject() {
	    if (typeof XMLHttpRequest != "undefined") {
			//alert('--1--');
	        doc =  new XMLHttpRequest();
	    } 
	    else if (window.ActiveXObject) {
	      var aVersions = [ "MSXML2.XMLHttp.5.0","MSXML2.XMLHttp.4.0","MSXML2.XMLHttp.3.0","MSXML2.XMLHttp","Microsoft.XMLHttp"];

	      for (var i = 0; i < aVersions.length; i++) {
	        try {
				//alert('--2--' + '==>' + i);
	            var oXmlHttp = new ActiveXObject(aVersions[i]);
	            doc =  oXmlHttp;
	        } catch (oError) {
	            //Do nothing
	        }
	      }
	    }
	    //throw new Error("XMLHttp object could be created.");
	}


	function createXMLHTTPObject_old(){
	    if (window.XMLHttpRequest) { // Mozilla, Safari,...
	        doc = new XMLHttpRequest();
	        if (doc.overrideMimeType) {
	            doc.overrideMimeType('text/xml');
	        }
	    } else if (window.ActiveXObject) { // IE
	        try {
	            doc = new ActiveXObject("Msxml2.XMLHTTP");
	        } catch (e) {
	            try {
	                doc = new ActiveXObject("Microsoft.XMLHTTP");
	            } catch (e) {}
	        }
	    }
	}

	function MyDateDiff( start, end, interval, rounding ) {
		var iOut = 0;
		var bufferA = Date.parse( start ) ;
		var bufferB = Date.parse( end ) ;
		    	
		// check that the start parameter is a valid Date. 
		if ( isNaN (bufferA) || isNaN (bufferB) ) {
			alert( startMsg ) ;
			return null ;
		}
			
		// check that an interval parameter was not numeric. 
		if ( interval.charAt == 'undefined' ) {
			// the user specified an incorrect interval, handle the error. 
			alert( intervalMsg ) ;
			return null ;
		}
		    
		var number = bufferB-bufferA ;
		    
		// what kind of add to do? 
		switch (interval.charAt(0)){
			case 'd': case 'D': 
				iOut = parseInt(number / 86400000) ;
				if(rounding) iOut += parseInt((number % 86400000)/43200001) ;
				break ;
			case 'h': case 'H':
				iOut = parseInt(number / 3600000 ) ;
				if(rounding) iOut += parseInt((number % 3600000)/1800001) ;
				break ;
			case 'm': case 'M':
				iOut = parseInt(number / 60000 ) ;
				if(rounding) iOut += parseInt((number % 60000)/30001) ;
				break ;
			case 's': case 'S':
				iOut = parseInt(number / 1000 ) ;
				if(rounding) iOut += parseInt((number % 1000)/501) ;
				break ;
			default:
				// If we get to here then the interval parameter
				// didn't meet the d,h,m,s criteria.  Handle
				// the error. 		
				alert(intervalMsg) ;
				return null ;
		}
		return iOut ;
	}

	function OpenInWindow(windURL,h,w,l,t){
		var winOpts="scrollbars=yes,resizable=yes,width="+w+",height="+h+"";
		confirmWin=window.open(windURL,"",winOpts);
		//popup = window.open(windURL,"popup","height=" + h + ",width=" + w + ",scrollbars=yes,left=" + l + ",top=" + t);
	}
	
	function submitMyForm(formObjectString,actionString,submitMethod){
		//alert("126");
		eval(formObjectString).action = actionString;
		eval(formObjectString).method = submitMethod;
		eval(formObjectString).submit();
	}

	function Trim(TRIM_VALUE){
		if(TRIM_VALUE.length < 1){
			return"";
		}
		TRIM_VALUE = RTrim(TRIM_VALUE);
		TRIM_VALUE = LTrim(TRIM_VALUE);
		if(TRIM_VALUE==""){
			return "";
		}
		else{
			return TRIM_VALUE;
		}
	} //End Function

	function RTrim(VALUE){
		var w_space = String.fromCharCode(32);
		var v_length = VALUE.length;
		var strTemp = "";
		if(v_length < 0){
			return"";
		}
		var iTemp = v_length -1;

		while(iTemp > -1){
			if(VALUE.charAt(iTemp) == w_space){
		}
		else{
			strTemp = VALUE.substring(0,iTemp +1);
		break;
		}
		iTemp = iTemp-1;

		} //End While
		return strTemp;
	} //End Function

	function LTrim(VALUE){
		var w_space = String.fromCharCode(32);
		if(v_length < 1){
			return"";
		}
		var v_length = VALUE.length;
		var strTemp = "";

		var iTemp = 0;

		while(iTemp < v_length){
			if(VALUE.charAt(iTemp) == w_space){
		}
		else{
			strTemp = VALUE.substring(iTemp,v_length);
		break;
		}
		iTemp = iTemp + 1;
		} //End While
		return strTemp;
	} //End Function