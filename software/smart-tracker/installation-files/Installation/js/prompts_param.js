
//////////////////////////////
// FOR DEBUGGING ONLY
var debug = false;
function dumpFormFields(formName)
{
    theForm = document.forms[formName];
    for ( idx = 0; idx < theForm.elements.length; ++idx )
        alert ( theForm.elements[idx].name + " - " + theForm.elements[idx].value );
}

//////////////////////////////
// GLOBAL VAR
var needURLEncode = false;  // only need to do url encode in java
var promptPrefix = "promptex-";
var CR_NULL = "_crNULL_";
var CR_USE_VALUE = "_crUseValue_";

var _bver    = parseInt(navigator.appVersion);
var Nav4     = ((navigator.appName == "Netscape") && _bver==4);
var Nav4plus = ((navigator.appName == "Netscape") && _bver >= 4);
var IE4plus  = ((navigator.userAgent.indexOf("MSIE") != -1) && _bver>4);

if (Nav4plus)
    var userLanguage = (navigator.language.substr(0, 2));
else
    var userLanguage = (navigator.browserLanguage.substr(0, 2));

///////////////////////////////
// substitue range string
function ConstructRangeDisplayString(rangeStr, param0, param1)
{
        var newString = rangeStr.replace("{0}", param0);
        newString = newString.replace("{1}", param1);
        return newString;
}

///////////////////////////////
// Display string for range value in ListBox
function GetRangeValueDisplayText(rangeStr, param0, param1, paramType)
{
        if (paramType == "dt" || paramType == "d" || paramType == "t")
        {
                if (param0.length != 0)
                {
                        param0 = NeutralDT2D(param0);
                }

                if (param1.length != 0)
                {
                        param1 = NeutralDT2D(param1);
                }
        }

        return ConstructRangeDisplayString(rangeStr, param0, param1);
}

///////////////////////////////
// properly encode prompt values
function encodePrompt (prompt)
{
    if (needURLEncode)
    {
        return encodeURIComponent(prompt);
    }
    else
    {
        return prompt;
    }
}

function clickSetNullCheckBox(inForm, paramName, suffix)
{
    var nullCtrl = inForm[paramName + "NULL"];
    var textCtrl = inForm[paramName + suffix];
    var hiddenCtrl = inForm[paramName + suffix + "Hidden"];
    var hourCtrl = inForm[paramName + suffix + "Hour"];
    var minuteCtrl = inForm[paramName + suffix + "Minute"];
    var secondCtrl = inForm[paramName + suffix + "Second"];
    var ampmCtrl = inForm[paramName + suffix + "AMPM"];
    if (nullCtrl.checked)
    {
        if (textCtrl != null) textCtrl.disabled = true;
        if (hourCtrl != null) hourCtrl.disabled = true;
        if (minuteCtrl != null) minuteCtrl.disabled = true;
        if (secondCtrl != null) secondCtrl.disabled = true;
        if (ampmCtrl != null) ampmCtrl.disabled = true;
    }
    else
    {
        if (textCtrl != null) textCtrl.disabled = false;
        if (hourCtrl != null) hourCtrl.disabled = false;
        if (minuteCtrl != null) minuteCtrl.disabled = false;
        if (secondCtrl != null) secondCtrl.disabled = false;
        if (ampmCtrl != null) ampmCtrl.disabled = false;
        
    }
}

function ClearListBoxAndSetNULL(theList)
{
    // delete everything in the listbox, add _crNULL_ as an item
    promptEntry = new Option(L_NoValue, CR_NULL,false,false);
    for ( var idx = 0; idx < theList.options.length; )
    {
        theList.options[0] = null;
        idx++;
    }

    theList.options[0] = promptEntry;
    return;
}

////////////////////////////////
// add number, currency, string from dropdown/textbox to list box
// where multiple prompt values are supported
function DateTimePromptValueHelper(type, hiddenCtrl, hourCtrl, minuteCtrl, secondCtrl, ampmCtrl)
{
    var promptValue = "";

    var year;
    var month;
    var day;
    if ((type == "dt") || (type == "d"))
    {
        var d = hiddenCtrl.value;
        if (d.length == 0)
        {
          alert(L_NoDateEntered);
          return "";
        }
        var leftIndex = d.indexOf("(");
        var rightIndex = d.indexOf(")");
        d = d.substr(leftIndex + 1, rightIndex - leftIndex - 1);
        var dArray = d.split(",");
        year = dArray[0];
        month = dArray[1];
        day = dArray[2];
    }

    if (type == "dt")
    {
        promptValue = "DateTime(";
        promptValue += year;
        promptValue += ",";
        promptValue += month;
        promptValue += ",";
        promptValue += day;
        promptValue += ",";

        var hour = 0;
        if (ampmCtrl != undefined)
        {
            var i = 0;
            if (ampmCtrl.selectedIndex == 1)
                i = 1;
            hour = hourCtrl.selectedIndex + 1 + i * 12;
            if (hour == 12) hour = 0;
            else if (hour == 24) hour = 12;
        }
        else
        {
            hour = hourCtrl.selectedIndex;
        }
            
        promptValue += hour;
        promptValue += ",";
        promptValue += minuteCtrl.selectedIndex;
        promptValue += ",";
        promptValue += secondCtrl.selectedIndex;

        promptValue += ")";
    }
    else if (type == "d")
    {
        promptValue = "Date(";
        promptValue += year;
        promptValue += ",";
        promptValue += month;
        promptValue += ",";
        promptValue += day;
        promptValue += ")";
        
    }
    else if (type == "t")
    {
        promptValue = "Time(" 

        var hour = 0;
        if (ampmCtrl != undefined)
        {
            var i = 0;
            if (ampmCtrl.selectedIndex == 1)
                i = 1;
            hour = hourCtrl.selectedIndex + 1 + i * 12;
            if (hour == 12) hour = 0;
            else if (hour == 24) hour = 12;
        }
        else
        {
            hour = hourCtrl.selectedIndex;
        }
       
            
        promptValue += hour;
        promptValue += ",";
        promptValue += minuteCtrl.selectedIndex;
        promptValue += ",";
        promptValue += secondCtrl.selectedIndex;
        promptValue += ")";
    }

    return promptValue;
}


function DateTimeDisplayStringHelper(type, hiddenCtrl, hourCtrl, minuteCtrl, secondCtrl, ampmCtrl)
{
    var neutralstring = DateTimePromptValueHelper(type, hiddenCtrl, hourCtrl, minuteCtrl, secondCtrl, ampmCtrl);
    return NeutralDT2D(neutralstring);
}


function addPromptDiscreteValueHelper(inForm, type, paramName, suffix)
{
    theList = inForm[paramName + "ListBox"];

    // if there is a NULL checkbox and it is set
    if ( inForm[paramName + "NULL"] != null && inForm[paramName + "NULL"].checked )
    {
        // delete everything in the listbox, add _crNULL_ as an item
        ClearListBoxAndSetNULL(theList);
        return;
    }

    // if the listbox already has a NULL value in it, don't do anything, unless user removes NULl value from the listbox
    if (theList.options.length > 0 && theList.options[0].value == CR_NULL)
    {
        alert(L_NoValueAlready);
        return;
    }

    // if defaultdropdownlist selected item is _crNULL_, put it into multiple listbox
    var dropDownListName = "";
    if (suffix == "DiscreteValue")
        dropDownListName = paramName + "SelectValue";
    else
        dropDownListName = paramName + "SelectLowerBound";
    var dropDownListCtrl = inForm[dropDownListName];
    if (dropDownListCtrl != null)
    {
        if (dropDownListCtrl.options[dropDownListCtrl.selectedIndex].value  == CR_NULL)
        {
            // delete everything in the listbox, add _crNULL_ as an item
            ClearListBoxAndSetNULL(theList);
            return;
        }
    }
    
    var textCtrl = inForm[paramName + suffix];
    var hiddenCtrl = inForm[paramName + suffix + "Hidden"];
    var hourCtrl = inForm[paramName + suffix + "Hour"];
    var minuteCtrl = inForm[paramName + suffix + "Minute"];
    var secondCtrl = inForm[paramName + suffix + "Second"];
    var ampmCtrl = inForm[paramName + suffix + "AMPM"];

    var obj;
    var editables = true;

    if (textCtrl == undefined && hourCtrl == undefined)
    {
        //select box not a textbox, hour, minute, and second, ampm dropdown are not there either
        editables = false;
        obj = dropDownListCtrl;
        obj = obj.options[obj.selectedIndex];
    }
    else
    {
        if (type == "t")
            obj = hourCtrl;
        else
            obj = textCtrl;
    }

    if (editables && (type == "dt" || type == "d" || type == "t"))
    {   
        promptValue = DateTimePromptValueHelper(type, hiddenCtrl, hourCtrl, minuteCtrl, secondCtrl, ampmCtrl);
        if (promptValue.length == 0) return false;
        
        displayString = DateTimeDisplayStringHelper(type, hiddenCtrl, hourCtrl, minuteCtrl, secondCtrl, ampmCtrl);   
    }
    else
    {
        promptValue =  encodePrompt(obj.value);
        displayString = ( obj.text ) ? obj.text : obj.value;
    }

    if ( ! checkSingleValue (promptValue, type ) )
    {
        return false;
    }

    promptEntry = new Option(displayString,promptValue,false,false);
    theList.options[theList.length] = promptEntry;
}

function addPromptDiscreteValue ( inForm, type , paramName)
{
    addPromptDiscreteValueHelper(inForm, type, paramName, "DiscreteValue");
}

////////////////////////////////////
// adds Range prompt to listbox where multiple values are supported
function addPromptDiscreteRangeValue ( inForm, type , paramName )
{
    lowerOption = inForm[paramName + "SelectLowerOptions"];

    // exactly
    if (lowerOption.selectedIndex == 1)
    {
        // add discrete
        addPromptDiscreteValueHelper(inForm, type, paramName, "LowerBound");
    }
    else
    {
       // add range
       addPromptRangeValue(inForm, type, paramName);
    }
}

////////////////////////////////////
// adds Range prompt to listbox where multiple values are supported
function addPromptRangeValue ( inForm, type , paramName )
{

    theList = inForm[paramName + "ListBox"];

    // if there is a NULL checkbox and it is set
    if ( inForm[paramName + "NULL"] != null && inForm[paramName + "NULL"].checked )
    {
        // delete everything in the listbox, add _crNULL_ as an item
        ClearListBoxAndSetNULL(theList);
        return;
    }

    // if the listbox already has a NULL value in it, don't do anything, unless user removed NULl value from the listbox
    if (theList.options.length > 0 && theList.options[0].value == CR_NULL)
    {
        alert(L_NoValueAlready);
        return;
    }

    // if both defaultdropdownlists selected item is _crNULL_, put it into multiple listbox
    // if one of them is _crNULL_, it is not a valid option, do nothing
    var lowerDropDownListName = paramName + "SelectLowerBound";
    var upperDropDownListName = paramName + "SelectUpperBound";
    var lowerDropDownListCtrl = inForm[lowerDropDownListName];
    var upperDropDownListCtrl = inForm[upperDropDownListName];
    if (lowerDropDownListCtrl != null && upperDropDownListCtrl != null)
    {
        if (lowerDropDownListCtrl.options[lowerDropDownListCtrl.selectedIndex].value  == CR_NULL
            && upperDropDownListCtrl.options[upperDropDownListCtrl.selectedIndex].value  == CR_NULL)
        {
            // delete everything in the listbox, add _crNULL_ as an item
            ClearListBoxAndSetNULL(theList);
            return;
        }
        else if (lowerDropDownListCtrl.options[lowerDropDownListCtrl.selectedIndex].value  == CR_NULL
            || upperDropDownListCtrl.options[upperDropDownListCtrl.selectedIndex].value  == CR_NULL)
        {
           alert(L_BadValue);
           return;
        }
    }

    lowerOption = inForm[paramName + "SelectLowerOptions"];
    upperOption = inForm[paramName + "SelectUpperOptions"];

    lowerBound = inForm[paramName + "LowerBound"];
    upperBound = inForm[paramName + "UpperBound"];

    lowerBoundHidden = inForm[paramName + "LowerBoundHidden"];
    upperBoundHidden = inForm[paramName + "UpperBoundHidden"];

    var lhourCtrl = inForm[paramName+"LowerBound" + "Hour"];
    var lminuteCtrl = inForm[paramName + "LowerBound" + "Minute"];
    var lsecondCtrl = inForm[paramName + "LowerBound" + "Second"];
    var lampmCtrl = inForm[paramName + "LowerBound" + "AMPM"];

    var uhourCtrl = inForm[paramName+"UpperBound" + "Hour"];
    var uminuteCtrl = inForm[paramName + "UpperBound" + "Minute"];
    var usecondCtrl = inForm[paramName + "UpperBound" + "Second"];
    var uampmCtrl = inForm[paramName + "UpperBound" + "AMPM"];

    var editable = true;

    //handle select box, not text box case
    if ( lowerBound == undefined && lhourCtrl == undefined)//either upper or lower, doesn't matter
    {
        editable = false;
        
        lowerBound = inForm[paramName + "SelectLowerBound"];
        upperBound = inForm[paramName + "SelectUpperBound"];
        lowerBound = lowerBound.options[lowerBound.selectedIndex];
        upperBound = upperBound.options[upperBound.selectedIndex];
    }

    lowerUnBounded = (inForm[paramName+"SelectLowerOptions"].selectedIndex == (inForm[paramName + "SelectLowerOptions"].options.length - 1));
    upperUnBounded = (inForm[paramName+"SelectUpperOptions"].selectedIndex == (inForm[paramName + "SelectUpperOptions"].options.length - 1));


    lvalue = uvalue = "";

    if ( ! lowerUnBounded )
    {
        if ((type == "dt" || type == "d" || type == "t") && (editable))
        {   
            lvalue = DateTimePromptValueHelper(type, lowerBoundHidden, lhourCtrl, lminuteCtrl, lsecondCtrl, lampmCtrl);
            if (lvalue.length == 0) return false;
        }
        else
            lvalue = lowerBound.value;

        if ( ! checkSingleValue ( lvalue, type ) ) {
            if ( lowerBound.focus && lowerBound.type.toLowerCase () != "hidden")
                lowerBound.focus ();
            return false;
        }
    }
    if ( ! upperUnBounded )
    {
        if ((type == "dt" || type == "d" || type == "t") && (editable))
        {   
            uvalue = DateTimePromptValueHelper(type, upperBoundHidden, uhourCtrl, uminuteCtrl, usecondCtrl, uampmCtrl);
            if (uvalue.length == 0) return false;
        }
        else
            uvalue = upperBound.value;

        if ( ! checkSingleValue ( uvalue, type ) ) {
            if ( upperBound.focus && upperBound.type.toLowerCase () != "hidden")
                upperBound.focus ();
            return false;
        }
    }

    var ldisplay = "";
    var udisplay = "";
    if ((type == "dt" || type == "d" || type == "t") && (editable))
    {
        if (!lowerUnBounded)
            ldisplay = DateTimeDisplayStringHelper(type, lowerBoundHidden, lhourCtrl, lminuteCtrl, lsecondCtrl, lampmCtrl);
        if (!upperUnBounded)
            udisplay = DateTimeDisplayStringHelper(type, upperBoundHidden, uhourCtrl, uminuteCtrl, usecondCtrl, uampmCtrl);
    }
    else
    {
        ldisplay = lvalue;
        udisplay = uvalue;
    }

    lowerChecked = (inForm[paramName+"SelectLowerOptions"].selectedIndex == 0);
    upperChecked = (inForm[paramName+"SelectUpperOptions"].selectedIndex == 0);
   
    value = lowerUnBounded ? "{" : lowerChecked ? "[" : "(";
    if ( ! lowerUnBounded ) //unbounded is empty string not quoted empty string (e.g not "_crEMPTY_")
        value += encodePrompt(lvalue);
    value += "_crRANGE_"
    if ( ! upperUnBounded )
        value += encodePrompt(uvalue);
    value += upperUnBounded ? "}" : upperChecked ? "]" : ")";
    if ( debug ) alert (value);

    if ( !lowerUnBounded && !upperUnBounded && !checkRangeValue(lvalue, uvalue, type))
    {
                return false;
    }

    var display = "";
    if (lowerChecked && upperUnBounded)
    {
                display = ConstructRangeDisplayString(L_FROM, ldisplay, "");
    }
    else if (lowerUnBounded && upperChecked)
    {
                display = ConstructRangeDisplayString(L_TO, udisplay, "");
    }
    else if (!lowerChecked && upperUnBounded)
    {
                display = ConstructRangeDisplayString(L_AFTER, ldisplay, "");
    }
    else if (lowerUnBounded && !upperChecked)
    {
                display = ConstructRangeDisplayString(L_BEFORE, udisplay, "");
    }
    else if (lowerChecked  && upperChecked)
    {
                display = ConstructRangeDisplayString(L_FROM_TO, ldisplay, udisplay);
    }
    else if (lowerChecked && !upperUnBounded && !upperChecked)
    {
                display = ConstructRangeDisplayString(L_FROM_BEFORE, ldisplay, udisplay);
    }
    else if (!lowerChecked && !lowerUnBounded && upperChecked)
    {
                display = ConstructRangeDisplayString(L_AFTER_TO, ldisplay, udisplay);
    }
    else if (!lowerChecked && !lowerUnBounded && !upperChecked && !upperUnBounded)
    {
                display = ConstructRangeDisplayString(L_AFTER_BEFORE, ldisplay, udisplay);
    }
   
    if ((!lowerUnBounded) || (!upperUnBounded))
    {
        promptEntry = new Option(display,value,false,false);
        theList.options[theList.length] = promptEntry;
    }
    else
    {
        alert(L_BadBound);
    }

}

////////////////////////////////////
// disable check boxes based on user selection on the range parameters
function disableBoundCheck(lowOrUpBound, inform, paramName) {
    if (lowOrUpBound == 0) {
        if (inform[paramName + "NoLowerBoundCheck"].checked) {
            inform[paramName + "NoUpperBoundCheck"].disabled = true;
            inform[paramName + "LowerCheck"].disabled = true;
            inform[paramName + "LowerBound"].disabled = true;
        }
        else {
            inform[paramName + "NoUpperBoundCheck"].disabled = false;
            inform[paramName + "LowerCheck"].disabled = false;
            inform[paramName + "LowerBound"].disabled = false;
        }
    } else if (lowOrUpBound == 1) {
        if (inform[paramName + "NoUpperBoundCheck"].checked) {
            inform[paramName + "NoLowerBoundCheck"].disabled = true;
            inform[paramName + "UpperCheck"].disabled = true;
            inform[paramName + "UpperBound"].disabled = true;
        } else {
            inform[paramName + "NoLowerBoundCheck"].disabled = false;
            inform[paramName + "UpperCheck"].disabled = false;
            inform[paramName + "UpperBound"].disabled = false;
        }
    }
}

////////////////////////////////////
// puts "select" value into text box for an editable prompt which also has defaults
function setSelectedValue (inForm, selectCtrlName, textCtrlName, type, namePlusFix)
{
    var textCtrl = inForm[textCtrlName];
    var selectCtrl = inForm[selectCtrlName];
    var hiddenCtrl = inForm[namePlusFix+"Hidden"];
    var hourCtrl = inForm[namePlusFix+"Hour"];
    var minuteCtrl = inForm[namePlusFix+"Minute"];
    var secondCtrl = inForm[namePlusFix+"Second"];
    var ampmCtrl = inForm[namePlusFix+"AMPM"];

    // if no editable input fields are there, return, do nothing;
    if (textCtrl == null && hourCtrl == null)
        return;

    // if selectedItem is UseValue,do nothing, and return
    if (selectCtrl.options[selectCtrl.selectedIndex].value == CR_USE_VALUE)
    {
       return;
    }

    if (selectCtrl.options[selectCtrl.selectedIndex].value == CR_NULL)
    {
       if (textCtrl != null) textCtrl.disabled = true;
       if (hourCtrl != null) hourCtrl.disabled = true;
       if (minuteCtrl != null) minuteCtrl.disabled = true;
       if (secondCtrl != null) secondCtrl.disabled = true;
       if (ampmCtrl != null) ampmCtrl.disabled = true;
       return;
    }
    else
    {
       if (textCtrl != null) textCtrl.disabled = false;
       if (hourCtrl != null) hourCtrl.disabled = false;
       if (minuteCtrl != null) minuteCtrl.disabled = false;
       if (secondCtrl != null) secondCtrl.disabled = false;
       if (ampmCtrl != null) ampmCtrl.disabled = false;
    }

    var year, month, day;
    var hour, minute, second;
    if (type == "dt" || type == "d" || type == "t")
    {
        var sDateTime = selectCtrl.options[selectCtrl.selectedIndex].value;
        if (sDateTime.length == 0)
            return;

        var leftIndex = sDateTime.indexOf("(");
        sDateTime = sDateTime.substr(leftIndex+1, sDateTime.length - leftIndex);
        var rightIndex = sDateTime.indexOf(")");
        sDateTime = sDateTime.substr(0, rightIndex);
        var a = sDateTime.split(",");

        if (type == "dt")
        {
            year = a[0];
            month = a[1];
            day = a[2];

            hour = parseInt(a[3]);
            minute = parseInt(a[4]);
            second = parseInt(a[5]);
        }
        else if (type == "d")
        {
            year = a[0];
            month = a[1];
            day = a[2];
        }
        else if (type == "t")
        {   
            hour = parseInt(a[0]);
            minute = parseInt(a[1]);
            second = parseInt(a[2]);
        }
    }

    selectedOption = selectCtrl.options[selectCtrl.selectedIndex];
    if (type == "dt")
    {
        var dt = new Date(year, month - 1, day, hour, minute, second);
        textCtrl.value = GLDT(dt, true, false);
        hiddenCtrl.value = "Date(";
        hiddenCtrl.value += year;
        hiddenCtrl.value += ",";
        hiddenCtrl.value += month;
        hiddenCtrl.value += ",";
        hiddenCtrl.value += day;
        hiddenCtrl.value += ")";

        if (ampmCtrl != undefined)
        {
            if (hour == 0 || hour == 12)
                hourCtrl.selectedIndex = 11;
            else if (hour > 12)
                hourCtrl.selectedIndex = hour - 12 - 1;
            else
                hourCtrl.selectedIndex = hour - 1;
        }
        else
            hourCtrl.selectedIndex = hour;
        minuteCtrl.selectedIndex = minute;
        secondCtrl.selectedIndex = second;
        if (ampmCtrl != null)
        { 
            if ( hour >= 12)
                ampmCtrl.selectedIndex = 1;
            else
                ampmCtrl.selectedIndex = 0;
        }
    }
    else if (type == "d")
    {
        var dt = new Date(year, month - 1, day, 0, 0, 0);
        textCtrl.value = GLDT(dt, true, false);
        hiddenCtrl.value = "Date(";
        hiddenCtrl.value += year;
        hiddenCtrl.value += ",";
        hiddenCtrl.value += month;
        hiddenCtrl.value += ",";
        hiddenCtrl.value += day;
        hiddenCtrl.value += ")";
    }
    else if (type == "t")
    {
        if (ampmCtrl != null)
        {
            if (hour == 0 || hour == 12)
                hourCtrl.selectedIndex = 11;
            else if (hour > 12)
                hourCtrl.selectedIndex = hour - 12 - 1;
            else
                hourCtrl.selectedIndex = hour - 1;
        }
        else
            hourCtrl.selectedIndex = hour;
        minuteCtrl.selectedIndex = minute;
        secondCtrl.selectedIndex = second;
        if (ampmCtrl != null)
        { 
            if ( hour >= 12)
                ampmCtrl.selectedIndex = 1;
            else
                ampmCtrl.selectedIndex = 0;
        }
    }
    else
    {
        textCtrl.value = selectedOption.value;
    }

    if (selectCtrl.options[0].value == CR_USE_VALUE)
    {
        // always show USE_VALUE as selectedItem for the dropdownlist
        selectCtrl.selectedIndex = 0;
    }
}

///////////////////////////////////
// remove value from listbox where multiple value prompts are supported
function removeFromListBox ( inForm, paramName )
{
    lbox = inForm[paramName + "ListBox"];
    for ( var idx = 0; idx < lbox.options.length; )
    {
        if ( lbox.options[idx].selected )
            lbox.options[idx] = null;
        else
            idx++;
    }
}

/////////////////////////////////////
// sets prompt value into the hidden form field in proper format so that it can be submitted
function setPromptSingleValue (inform, type, paramName)
{
    //alert("SetPromptSingleValue");
    hiddenField = inform[promptPrefix + paramName];
    value = "";
    if ( inform[paramName + "NULL"] != null && inform[paramName + "NULL"].checked )
    {
        value = CR_NULL; //NULL is a literal for, uhmm.. a NULL
        hiddenField.value = value;
        return true;
    }

    // if defaultdropdownlist selected item is _crNULL_, put it into multiple listbox
    var dropDownListName = paramName + "SelectValue";
    var dropDownListCtrl = inform[dropDownListName];
    if (dropDownListCtrl != null)
    {
        if (dropDownListCtrl.options[dropDownListCtrl.selectedIndex].value  == CR_NULL)
        {
            hiddenField.value = CR_NULL;
            return true;
        }
    }

        
    discreteVal = inform[paramName + "DiscreteValue"];
    if (discreteVal != undefined || inform[paramName + "DiscreteValueHour"] != undefined)
    { // editable
          
        if ( type == "dt" || type == "d" || type == "t")
        {
           var hiddenCtrl = inform[paramName+"DiscreteValueHidden"];
           var hourCtrl = inform[paramName+"DiscreteValueHour"];
           var minuteCtrl = inform[paramName + "DiscreteValueMinute"];
           var secondCtrl = inform[paramName + "DiscreteValueSecond"];
           var ampmCtrl = inform[paramName + "DiscreteValueAMPM"];
           value = DateTimePromptValueHelper(type, hiddenCtrl, hourCtrl, minuteCtrl, secondCtrl, ampmCtrl);
           if (value.length == 0) return false;
        }
        else
           value = discreteVal.value;
    }
    else
    {
        // not editable
        discreteVal = inform[paramName+"SelectValue"];
        value = discreteVal.options[discreteVal.selectedIndex].value;
        //alert(value);
    }
    if ( ! checkSingleValue ( value, type ) ) {
        if (discreteVal.focus && discreteVal.type.toLowerCase ())
           discreteVal.focus ();
        return false;
    }
    else
        value = encodePrompt(value);

    hiddenField.value = value;
    return true;
}

/////////////////////////////////////
// sets prompt value for a range into the hidden form field in proper format so that it can be submitted
function setPromptRangeValue (inform, type, paramName)
{

    hiddenField = inform[promptPrefix + paramName];

    if ( inform[paramName + "NULL"] != null && inform[paramName + "NULL"].checked )
    {
        value = CR_NULL; //NULL is a literal for, uhmm.. a NULL
        hiddenField.value = value;
        return true;
    }

        // if both defaultdropdownlists selected item is _crNULL_, put it into hiddenfield
    // if one of them is _crNULL_, it is not a valid option, do nothing
    var lowerDropDownListName = paramName + "SelectLowerBound";
    var upperDropDownListName = paramName + "SelectUpperBound";
    var lowerDropDownListCtrl = inform[lowerDropDownListName];
    var upperDropDownListCtrl = inform[upperDropDownListName];
    if (lowerDropDownListCtrl != null && upperDropDownListCtrl != null)
    {
        if (lowerDropDownListCtrl.options[lowerDropDownListCtrl.selectedIndex].value  == CR_NULL
            && upperDropDownListCtrl.options[upperDropDownListCtrl.selectedIndex].value  == CR_NULL)
        {
            hiddenField.value = "_crNULL_";
            return true;
        }
        else if (lowerDropDownListCtrl.options[lowerDropDownListCtrl.selectedIndex].value  == CR_NULL
            || upperDropDownListCtrl.options[upperDropDownListCtrl.selectedIndex].value  == CR_NULL)
        {
            alert(L_BadValue);
            return false;
        }
    }

    lowerOptions = inform[paramName + "SelectLowerOptions"];
    upperOptions = inform[paramName + "SelectUpperOptions"];

    lowerBound = inform[paramName + "LowerBound"];
    upperBound = inform[paramName + "UpperBound"];

    lowerBoundHidden = inform[paramName + "LowerBoundHidden"];
    upperBoundHidden = inform[paramName + "UpperBoundHidden"];

    lowerBoundDropdown = inform[paramName + "SelectLowerBound"];
    upperBoundDropdown = inform[paramName + "SelectUpperBound"];

    var editables = true;

    //handle select box, not text box case
    if (lowerBound == undefined && inform[paramName + "LowerBound" + "Hour"] == undefined)
    {
        editables = false;
        lowerBound = lowerBoundDropdown;
        upperBound = upperBoundDropdown;
        lowerBound = lowerBound.options[lowerBound.selectedIndex];
        upperBound = upperBound.options[upperBound.selectedIndex];
    }

    lowerUnBounded = (lowerOptions.selectedIndex == (lowerOptions.length - 1));
    upperUnBounded = (upperOptions.selectedIndex == (upperOptions.length - 1));

    lowerChecked = (lowerOptions.selectedIndex == 0);
    upperChecked = (upperOptions.selectedIndex == 0);

    uvalue = lvalue = "";

    if ( ! lowerUnBounded )
    {
        if ( (type == "dt" || type == "d" || type == "t") && (editables))
        {
            var lhourCtrl = inform[paramName+"LowerBound" + "Hour"];
            var lminuteCtrl = inform[paramName + "LowerBound" + "Minute"];
            var lsecondCtrl = inform[paramName + "LowerBound" + "Second"];
            var lampmCtrl = inform[paramName + "LowerBound" + "AMPM"];
            lvalue = DateTimePromptValueHelper(type, lowerBoundHidden, lhourCtrl, lminuteCtrl, lsecondCtrl, lampmCtrl);
            if (lvalue.length == 0) return false;
        }
        else
            lvalue = lowerBound.value;

        if ( ! checkSingleValue ( lvalue, type ) ) {
            if (lowerBound != undefined && lowerBound.focus)
                lowerBound.focus();
            return false;
        }
    }
    if ( ! upperUnBounded )
    {
        if (( type == "dt" || type == "d" || type == "t") &&  (editables))
        {
            var uhourCtrl = inform[paramName+"UpperBound" + "Hour"];
            var uminuteCtrl = inform[paramName + "UpperBound" + "Minute"];
            var usecondCtrl = inform[paramName + "UpperBound" + "Second"];
            var uampmCtrl = inform[paramName + "UpperBound" + "AMPM"];
            uvalue = DateTimePromptValueHelper(type, upperBoundHidden, uhourCtrl, uminuteCtrl, usecondCtrl, uampmCtrl);
            if (uvalue.length == 0) return false;
        }
        else
            uvalue = upperBound.value;

        if ( ! checkSingleValue ( uvalue, type ) ) {
            if (upperBound != undefined && upperBound.focus)
                upperBound.focus();
            return false;
        }
    }
    
    value = lowerUnBounded ? "{" : lowerChecked ? "[" : "(";
    if ( ! lowerUnBounded )
        value += encodePrompt(lvalue);
    value += "_crRANGE_"
    if ( ! upperUnBounded )
        value += encodePrompt(uvalue);
    value += upperUnBounded ? "}" : upperChecked ? "]" : ")";
    if ( debug )
        alert (value);

    if (lowerUnBounded && upperUnBounded)
    {
        alert(L_BadBound);
        return false;
    }

    if (!lowerUnBounded && !upperUnBounded && !checkRangeValue(lvalue, uvalue, type))
    {
            return false;
    }

    hiddenField.value = value;
    return true;
}

/////////////////////////////////////
// sets prompt value into the hidden form field in proper format so that it can be submitted
function setPromptMultipleValue (inform, type, paramName)
{
    hiddenField = inform[promptPrefix + paramName];
    values = inform[paramName + "ListBox"].options;

    if ( values.length == 0 )
    {
        value = "_crEMPTY_";     //if value is empty, set to empty string
    }
    else
    {
        value = "";
        for ( idx = 0; idx < values.length; ++idx )
        {
            // first value could be empty string, then chcking value.length != 0 could miss one empty string
            if ( idx != 0 )
                value += "_crMULT_"
            value += values[idx].value;
        }
    }
    
    if ( debug )
        alert (value);
    
    hiddenField.value = value;
    //NOTE: we'll always return true as the validation is done before values are added to select box
    return true;
}

///////////////////////////////////////
// check and alert user about any errors based on type of prompt
var regNumber    = /^(\+|-)?((\d+(\.|,| |\u00A0)?\d*)+|(\d*(\.|,| |\u00A0)?\d+)+)$/
var regCurrency  = regNumber;
var regDate  = /^(D|d)(A|a)(T|t)(E|e) *\( *\d{4} *, *(0?[1-9]|1[0-2]) *, *((0?[1-9]|[1-2]\d)|3(0|1)) *\)$/
var regDateTime  = /^(D|d)(A|a)(T|t)(E|e)(T|t)(I|i)(M|m)(E|e) *\( *\d{4} *, *(0?[1-9]|1[0-2]) *, *((0?[1-9]|[1-2]\d)|3(0|1)) *, *([0-1]?\d|2[0-3]) *, *[0-5]?\d *, *[0-5]?\d *\)$/
var regTime  = /^(T|t)(I|i)(M|m)(E|e) *\( *([0-1]?\d|2[0-3]) *, *[0-5]?\d *, *[0-5]?\d *\)$/

function checkSingleValue ( value, type )
{
    if ( type == 'n' && ! regNumber.test ( value ) )
    {
        alert ( L_BadNumber );
        return false;
    }
    else if ( type == 'c' && ! regCurrency.test ( value ) )
    {
        alert ( L_BadCurrency );
        return false;
    }
    /*else if ( type == 'd' && ! regDate.test ( value ) )
    {
        alert ( L_BadDate );
        return false;
    }
    else if ( type == "dt" && ! regDateTime.test ( value ) )
    {
        alert ( L_BadDateTime );
        return false;
    }
    else if ( type == 't' && ! regTime.test ( value ) )
    {
        alert ( L_BadTime );
        return false;
    }*/

    //by default let it go...
    return true;
}

function checkRangeValue (fvalue, tvalue, type)
{
    // determine if the start is smaller than the end
    if ((type == "n") || (type == "c"))
    {
       // Is a number, or currency
        if (eval (DelocalizeNum (fvalue)) > eval (DelocalizeNum (tvalue)))
        {
            alert(L_RangeError);
            return false;
        }
    }
    else if (type == "d"){
        //Is a Date
        if (eval("new " + fvalue) > eval("new " + tvalue)){
            alert(L_RangeError);
            return false;
        }         
    }else if (type == "t"){
        //Is a Time
        var comp1 = eval("new Date(0,0,0," + fvalue.substring(fvalue.indexOf('(') + 1, fvalue.indexOf(')') + 1));
        var comp2 = eval("new Date(0,0,0," + tvalue.substring(tvalue.indexOf('(') + 1, tvalue.indexOf(')') + 1));
        if (comp1 > comp2){
            alert(L_RangeError);
            return false;
        }         
    }else if (type == "dt"){
        //Is a DateTime
        var comp1 = eval("new Date" + fvalue.substring(8, fvalue.length));
        var comp2 = eval("new Date" + tvalue.substring(8, tvalue.length));
        if (comp1 > comp2){
            alert(L_RangeError);
            return false;
        }         
    }
    else if (type == "text")
    {
         // is a string
         if (fvalue.toLowerCase() > tvalue.toLowerCase())
         {
            alert(L_RangeError);
            return false;
         }
    }
    // otherwise, let it go

        return true;
}

function DelocalizeNum(value)
{
        // trim spaces first
        var numStr = value.replace(/\s/g, "");
                
        // get rid of grouping first    
        var tempStr = "";
        var index = numStr.indexOf(groupSep);
        
        while (index != -1)
        {
                tempStr += numStr.substr(0, index);
                numStr = numStr.substr(index + groupSep.length, numStr.length - index - groupSep.length);
                index = numStr.indexOf(groupSep);
        }
        
        tempStr += numStr;

        index = tempStr.indexOf(decimalSep);
        var neutralStr = "";
        if (index != -1)
        {       
                neutralStr += tempStr.substr(0, index);
                neutralStr += ".";
                neutralStr += tempStr.substr(index + decimalSep.length, tempStr.length - index - decimalSep.length);
        }
        else
        {
                neutralStr = tempStr;
        }

        return neutralStr;
}

// Disable enter key checking for multibyte languages since the enter key is used for committing characters
var isEnabledLanguage = (! ((userLanguage == "ja") || (userLanguage == "ko") || (userLanguage == "zh")) )

var isNav = (navigator.appName == "Netscape");
if (isEnabledLanguage)
{
    if(isNav) {
        document.captureEvents(Event.KEYUP);
    }
    document.onkeyup = checkValue;
}

function checkValue(evt) {
  var buttonVal = "";
  if (isNav) {
    if (evt.which == 13 && (evt.target.type == "text" || evt.target.type == "password")) {
        buttonVal = evt.target.value;
    }
  } else {
    if (window.event.keyCode == 13 && (window.event.srcElement.type == "text" || window.event.srcElement.type == "password")) {
        buttonVal = window.event.srcElement.value;
    }
  }

  if (buttonVal != "") {
    submitParameterValues ();
  }
}
