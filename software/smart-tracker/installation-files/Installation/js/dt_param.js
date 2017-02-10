
// TDC is to get DateTime format info for current locale
function TDC() { 
  var d=new Date(1970,0,0,0,0,0,0),d2=new Date(d),s1,s2,api=-1; 
  d.setHours(10); d2.setHours(11); 
  s1=d.toLocaleString(); s2=d2.toLocaleString(); 
  this.displayHour=(s1!=s2); 
  d.setHours(0); d2.setHours(0); 
  d.setMinutes(10); d2.setMinutes(11); 
  this.displayMinute=(d.toLocaleString()!=d2.toLocaleString()); 
  d.setMinutes(0); d2.setMinutes(0); 
  d.setSeconds(10); d2.setSeconds(11); 
  this.displaySecond=(d.toLocaleString()!=d2.toLocaleString()); 
  d.setSeconds(0); d2.setSeconds(0); 
  if (this.displayHour) { 
    for (i=0; i<((s1.length<s2.length)?s1.length:s2.length); i++) { 
      if (s1.charAt(i)!=s2.charAt(i)) { 
        api=i-1; break; 
      } 
    } 
    d.setHours(11); d2.setHours(23); 
    s1=d.toLocaleString(); s2=d2.toLocaleString(); 
    this.twelveHourClock=(s1.substring(api,api+2)==s2.substring(api,api+2)); 
    this.displayAMPM=(s1!=s2); 
    d.setHours(0); d2.setHours(0); 
    d.setHours(9); d2.setHours(11); 
    this.hourLeadZero=(d.toLocaleString().length==d2.toLocaleString().length); 
    d.setHours(0); d2.setHours(0); 
  } else { 
    this.twelveHourClock=false; 
    this.displayAMPM=false; 
    this.hourLeadZero=false; 
  } 
  if (this.displayMinute) { 
    d.setMinutes(9); d2.setMinutes(11); 
    this.minuteLeadZero=(d.toLocaleString().length==d2.toLocaleString().length); 
    d.setMinutes(0); d2.setMinutes(0); 
  } else 
    this.minuteLeadZero=false; 
  if (this.displaySecond) { 
    d.setSeconds(9); d2.setSeconds(11); 
    this.secondLeadZero=(d.toLocaleString().length==d2.toLocaleString().length); 
    d.setSeconds(0); d2.setSeconds(0); 
  } else 
    this.secondLeadZero=false; 
} 

var TD = new TDC(); 

// create time editables, hour dropdown, minute dropdown, second dropdown, and am/pm dropdown
function CreateTimeEditables(paramName, postFix, enabled, cssClass, style, hour, minute, second)
{
   var properties = "";
   if (!enabled)
   {
      properties += " disabled='disabled' ";
   }
      
   if (cssClass.length != 0)
   {
	 properties += " class='";
	 properties += cssClass;
	 properties += "' ";
   }
      
   if (style.length != 0)
   {
     properties += " style='";
     properties += style;
     properties += "' ";
   }
     
   var time_sep_span = "";
   time_sep_span += "<span ";
   time_sep_span += properties;
   time_sep_span += ">";
   time_sep_span += L_TIME_SEPARATOR;
   time_sep_span += "</span>";

   var editablesHtml = "";
   if (TD.displayHour)
   {
      editablesHtml +=  "<SELECT ";
      editablesHtml += properties;      
      editablesHtml +=  " name='";
      editablesHtml += paramName;
      editablesHtml += postFix;
      editablesHtml += "Hour'>";
      
      var minHour;
      var maxHour;
      if (TD.twelveHourClock)
      {
         minHour = 1;
         maxHour = 12;
      }
      else
      {
         minHour = 0;
         maxHour = 23;
      }
 
      for (i = minHour; i <= maxHour ; i++){
	      editablesHtml += " <option value='";
          editablesHtml += i;
          editablesHtml += "'";

          if (!TD.twelveHourClock)
	  {  
	     if (i == hour)
	     	editablesHtml += " selected ";
          }
          else
          {
	     if (( hour == 0) && (i == 12))  
 		editablesHtml += " selected ";
             else if ((hour == 12) && (i == 12))
		editablesHtml += " selected ";
             else if ((hour > 12) && (i == hour - 12))
		editablesHtml += " selected ";
	     else if (i == hour)
                editablesHtml += " selected ";
          }

          if ((i < 10) && (TD.hourLeadZero))
          {
             editablesHtml += "> 0";
          }
          else
	  {
             editablesHtml += "> ";
             
          }
          editablesHtml += i;
          editablesHtml += "</option>\n";
      }
       
      editablesHtml +=  "        </SELECT>";
   }

   if (TD.displayMinute)
   {
      if (editablesHtml.length != 0) 
      {
         editablesHtml += time_sep_span;
      }
 
      editablesHtml += "<SELECT  ";
      editablesHtml += properties;	   
      editablesHtml += " name='";
      editablesHtml += paramName;
      editablesHtml += postFix;
      editablesHtml += "Minute'>";
      
 
      for (i = 0; i <= 59; i++){
	      editablesHtml += " <option value='";
          editablesHtml += i;
          editablesHtml += "'";

          if (i == minute)
	         editablesHtml += " selected ";
          if ((i < 10) && (TD.minuteLeadZero))
          {
             
             editablesHtml += "> 0";
             
          }
          else
          {
             editablesHtml += "> ";
          }
          editablesHtml += i;
          editablesHtml += "</option>\n";
      }
       
      editablesHtml +=  "        </SELECT>";
   }
 
   if (TD.displaySecond)
   {
      if (editablesHtml.length != 0)
         editablesHtml += time_sep_span;
      
      editablesHtml += "<SELECT ";
      editablesHtml += properties;  
      editablesHtml += " name='";
      editablesHtml += paramName;
      editablesHtml += postFix;
      editablesHtml += "Second'>";
      
 
      for (i = 0; i <= 59; i++){
          editablesHtml += " <option value='";
          editablesHtml += i;
	      editablesHtml += "'";
 
          if (i == second)
	         editablesHtml += " selected ";

          if ((i < 10) && (TD.secondLeadZero))
             editablesHtml += "> 0";
          else
             editablesHtml += "> ";;
          editablesHtml += i;
          editablesHtml += "</option>\n";
      }
       
      editablesHtml +=  "        </SELECT>";
   }
   
   if (TD.twelveHourClock)
   {
      editablesHtml +=  "<SELECT ";
      editablesHtml += properties;
      editablesHtml += " name='";
      editablesHtml += paramName;
      editablesHtml += postFix;
      editablesHtml += "AMPM' >";
      editablesHtml += " <option value='0'";
      if (hour < 12)
	    editablesHtml += " selected ";
      editablesHtml += "> ";
      editablesHtml += L_AM_DESIGNATOR;
      editablesHtml += "</option>\n";
      editablesHtml += " <option value='1'"; 
      if (hour >= 12)
	    editablesHtml += " selected ";
      editablesHtml += "> ";
      editablesHtml += L_PM_DESIGNATOR;
      editablesHtml += "</option>\n";
      editablesHtml +=  "        </SELECT>";
   }
  
   return editablesHtml;
}

// get Locale Date/Time
// d: input Date
// return only Date part if includeDate==true, and includeTime== false
// return only Time part if includeDate==false and includeTime== true
// return datetime string if includeDate==true and includeTime == true
function GLDT(d, includeDate, includeTime) { // Get Locale Date/Time 
  // Returns date/and or time from locale string. 
  // Assumes that date appears before time in string. 
  if (includeDate && includeTime) return d.toLocaleString(); 

  var d2 = new Date(d);
  var ds;
  var ds2;
  var ml; 
  d2.setMilliseconds(d.getMilliseconds()<=887 ? d.getMilliseconds()+111 : d.getMilliseconds() - 111); 
  d2.setSeconds(d.getSeconds()<=49 ? d.getSeconds()+11 : d.getSeconds()-11); 
  d2.setMinutes(d.getMinutes()<=49 ? d.getMinutes()+11 : d.getMinutes()-11); 
  
  // although TD.twelveHourClock could be true, but d.getHours will always get hours in 24
  // only the localized the date string will be in twelve hour clock
  // here we want d2's hour's first digit different from d1's hour's first digit
  // also if d1 is morning, d2 will be in afternoon, vice versa thus we get different strings for AM, and PM
  // because AM/PM could be before hours (eg. in Korean)
  if (TD.twelveHourClock) 
  {
	var hour = d.getHours();
	if (hour == 0 || hour == 1 || hour == 10 || hour == 11)
	{
		d2.setHours(14);
	} 
	else if (hour == 12 || hour == 13 || hour == 22 || hour == 23)
	{
		d2.setHours(2);
	}
	else if (hour < 12)
	{
		d2.setHours(13);
	}
	else 
	{
		d2.setHours(1);
	}
  }
  else 
  { 
    d2.setHours( d.getHours()<12 ? (d.getHours() == 1 ? 2 : d.getHours() + 12) : (d.getHours()<20 ? 20 : 11)); 
  } 
    
  ds = d.toLocaleString(); 

  ds2 = d2.toLocaleString(); 

  ml = ds.length < ds2.length ? ds.length : ds2.length ;
  for (i=0; i<ml; i++) { 
    if (ds.charAt(i) != ds2.charAt(i)) 
	{
		// put the iterator back to the beginning of the word
		while (ds.charAt(i) != ' ') i--;
		i++;
		break;
	}
  } 
  if (includeDate) 
    return ds.substring(0, i-1);
  else if (includeTime) 
    return ds.substring(i); 
  else 
    return ""; 
} 

// construct localized datetime/date/time string 
// from DateTime(year, month, day, hour, minute, second)
// or   Date(year, month, day)
// or   Time(hour, minute, second)
function NeutralDT2D(s) { 
  var d="";
  var td = new Date();
  var a; 
  if (s.indexOf("DateTime")!=-1) { 
    a = s.replace("DateTime","").replace("(","").replace(")","").split(",") ;
    d = GLDT(new Date(a[0], a[1]-1, a[2], a[3], a[4], a[5], 0), true, true); 
  } else if (s.indexOf("Date")!=-1) { 
    a = s.replace("Date","").replace("(","").replace(")","").split(",") ;
    d = GLDT(new Date(a[0], a[1]-1, a[2], 0, 0, 0, 0), true, false); 
  } else if (s.indexOf("Time")!=-1) { 
    a = s.replace("Time","").replace("(","").replace(")","").split(","); 
    d = GLDT(new Date(td.getYear(), td.getMonth(), td.getDate(), a[0], a[1], a[2]), false, true); 
  }
  return d; 
}

function NeutralDT2Date(s){
  var d="";
  var td = new Date();
  var a; 
  if (s.indexOf("DateTime")!=-1) { 
    a = s.replace("DateTime","").replace("(","").replace(")","").split(",") ;
    return new Date(a[0], a[1]-1, a[2], a[3], a[4], a[5], 0);
  } else if (s.indexOf("Date")!=-1) { 
    a = s.replace("Date","").replace("(","").replace(")","").split(",") ;
    return new Date(a[0], a[1]-1, a[2], 0, 0, 0, 0);
  } else if (s.indexOf("Time")!=-1) { 
    a = s.replace("Time","").replace("(","").replace(")","").split(","); 
    return new Date(td.getYear(), td.getMonth(), td.getDate(), a[0], a[1], a[2]);
  }
  return d; 
}
