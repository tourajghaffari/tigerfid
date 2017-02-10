/*
	File Version Start - Do not remove this if you are modifying the file
	Build: 9.5.0
	File Version End
*/
/*
    this function is used to verify whether the 'Enter' key is pressed.
    if the 'Enter' key is pressed, then either call the event handler function passed in as a parameter (evntHdlrName)
    or if evntHdlrName is an empty string, submit the form that contains the input box (that triggers this function).  
    The form name is passed in as the third parameter, if it's an empty string, it's set to 0 by default.
    The first parameter is an event object 
    
    written by Paul Chong, 17th May 01
*/

function keydownfn(e, evntHdlrName, formName)
{	
    var nav4;
    var keyPressed;

    //check if the brower is Netscape Navigator 4 or not
    var nav4 = window.Event ? true : false;
	
	//if browser is Navigator 4, the key pressed is called <event object>.which else it's called <event object>.keyCode
	keyPressed = nav4 ? e.which : e.keyCode;
		
    if (keyPressed == 13)
    {	
        if (evntHdlrName != "")
		{	
			// append empty parentheses if none given
			if(evntHdlrName.substr(evntHdlrName.length-1) != ")")
				evntHdlrName += "()";
			eval(evntHdlrName);
        }
        else
        {
			if (formName == "")
				formName = 0;
            document.forms[formName].submit();
        }
    }
    return true;
}
