// export.js
// This file contains the funcitons needed to construct the HTML for the export / print dialog.
//
// Global variable
var print = false;		// default to export, so set print to false
var crystal_postback =
        "<INPUT type=\"hidden\" name=\"reportsource\" id=\"reportsource\"/>" +
        "<INPUT type=\"hidden\" name=\"viewstate\" id=\"viewstate\"/>";

function getPageTitle() {
	if (print) {
		return L_PrintPageTitle;
	}
	else {
		return L_ExportPageTitle;
	}
}

function getOptionsTitle() {
	if (print) {
		return L_PrintOptions;
	}
	else {
		return L_ExportOptions;
	}
}

function getFormatDropdownList() {
	if (print) {
		return "<INPUT type=\"hidden\" name=\"exportformat\" id=\"exportformat\" value=\"PDF\"/>";
	}
	else {
		var list =
		"<TABLE width=\"100%\">" +
		"<TD align=\"center\"><SPAN class=\"exportMessage\"><LABEL for=\"exportFormatList\">" + L_ExportFormat + "</LABEL></SPAN></TD>"  +
		"<TR>" +
		"<TD class=\"exportSelect\" align=\"center\">" +
		"<SELECT id=\"exportFormatList\" class=\"exportSelect\" name=\"exportformat\" onchange=\"checkDisableRange()\">" +
		"<OPTION selected value=\"\">" + L_Formats +"</OPTION>";
		if( rpt )
		{
			list += "<OPTION value=\"CrystalReports\">" + L_CrystalRptFormat + "</OPTION>";
		}
		if( pdf )
		{
			list += "<OPTION value=\"PDF\">" + L_AcrobatFormat + "</OPTION>";
		}
		if( word )
		{
			list += "<OPTION value=\"MSWord\">" + L_WordFormat + "</OPTION>";
		}
		if( xls )
		{
			list += "<OPTION value=\"MSExcel\">" + L_ExcelFormat + "</OPTION>";
		}
		if( recXls )
		{
			list += "<OPTION value=\"RecordToMSExcel\">" + L_ExcelRecordFormat + "</OPTION>";
		}
		if( rtf )
		{
			list += "<OPTION value=\"RTF\">" + L_RTFFormat +"</OPTION>";
		}
		
		list += "</SELECT>" +
		"</TD>" +
		"</TR>" +
		"</TABLE>";
		return list;
	}
}

function getSelectPageRangeSentence() {
	if (print) {
		return L_PrintPageRange;
	}
	else {
		return L_ExportPageRange;
	}
}

function getPrintSteps() {
	if (print) {
		var steps =
		"<TR height=40 valign=\"bottom\">" +
		"<TD><SPAN class=\"exportMessage\">" + L_PrintStep0 + "</SPAN></TD>" +
		"</TR>" +
		"<TR valign=\"top\">" +
		"<TD><SPAN class=\"exportMessage\">" + L_PrintStep1 + "</TD>" +
		"</TR>" +
		"<TR height=40 valign=\"top\">" +
		"<TD><SPAN class=\"exportMessage\">" + L_PrintStep2 + "</SPAN></TD>" +
		"</TR>";
		return steps;
	}
	else {
		return "";
	}
}

function getExportDialog() {
	var exportDialog =
		"<HTML>" +
		"<HEAD>" +
		"<STYLE>" +
		"SPAN.exportMessage {" +
		"   FONT-SIZE: 12pt; FONT-FAMILY: Arial, Helvetica, sans-serif" +
		"}" +
		"SPAN.exportSelect {" +
		"   FONT-SIZE: 10pt; FONT-FAMILY: Arial, Helvetica, sans-serif" +
		"}" +
		"</STYLE>" +
		"<TITLE>" + getPageTitle() + "</TITLE>" +
		"</HEAD>" +
		"<BODY bottomMargin=0 topMargin=5 onload=\"init()\">" +
		"<FORM name=\"Export\" method=\"POST\">" +
		crystal_postback +
		"<TABLE cellSpacing=\"0\" cellPadding=\"3\" width=\"97%\" align=\"center\" border=\"0\">" +
		"<TBODY>" +
		"<TR bgColor=#008080><TD>&nbsp;</TD></TR>" +
		"<TR bgColor=#000000><TD>&nbsp;</TD></TR>" +
		"<FIELDSET style=\"border-style:none\">" +
		"<TR><TD><LEGEND align=\"center\"><SPAN class=\"exportMessage\">" + getOptionsTitle() + "</SPAN></LEGEND></TD></TR>" +
		"<TR>" +
		"<TD align=\"center\">" +
		getFormatDropdownList() +
		"</TD></TR>" +
		"<TR><TD><SPAN class=\"exportMessage\">&nbsp;&nbsp;&nbsp;" + getSelectPageRangeSentence() +
		"</SPAN></TD>" +
		"</TR>" +
		"<TR>" +
		"<TD>" +
		"<TABLE>" +
		"<TR>" +
		"<TD><INPUT type=\"radio\" id=\"radio1\" checked name=\"isRange\" value=\"all\" onclick=\"return toggleRangeFields(this);\"/></TD>" +
		"<TD><SPAN class=\"exportMessage\"><LABEL for=radio1>" + L_All + "</LABEL></SPAN></TD>" +
		"</TR>" +
		"</TABLE>" +
		"</TD>" +
		"</TR>" +
		"<TR>" +
		"<TD>" +
		"<TABLE>" +
		"<TR>" +
		"<TD><INPUT type=\"radio\" id=\"radio2\" name=\"isRange\" value=\"selection\" onclick=\"return toggleRangeFields(this);\"/></TD>" +
		"<TD><SPAN class=\"exportMessage\"><LABEL for=radio2>" + L_Pages + "</LABEL></SPAN></TD>" +
		"</TR>" +
		"</TABLE>" +
		"</TD>" +
		"</TR>" +
		"<TR>" +
		"<TD>" +
		"<TABLE>" +
		"<TR>" +
		"<TD>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>" +
		"<TD><SPAN class=\"exportMessage\"><LABEL for=from>" + L_From + "</LABEL></SPAN></TD>" +
		"<TD><INPUT type=\"text\" width=\"20\" size=\"6\" maxLength=\"6\" name=\"from\" id=\"from\" value=\"1\" disabled></TD>" +
		"<TD><SPAN class=\"exportMessage\"><LABEL for=to>" + L_To + "</LABEL></SPAN></TD>" +
		"<TD><INPUT type=\"text\" width=\"20\" size=\"6\" maxLength=\"6\" name=\"to\" id=\"to\" value=\"1\" disabled></TD>" +
		"</TR>" +
		"</TABLE>" +
		"</TD>" +
		"</TR>" +
		"</FIELDSET>" +
		getPrintSteps() +
		"<TR>" +
		"<TD align=\"center\" colspan=6><BR><INPUT type=\"button\" id=\"submitexport\" width=\"30\" title=\"" + getPageTitle() + "\" value=\"&nbsp;&nbsp;&nbsp;" + L_OK + "&nbsp;&nbsp;&nbsp;\" onclick=\"checkValuesAndSubmit();\"/></TD>" +
		"</TR>" +
		"</TBODY>" +
		"</TABLE>" +
		"</FORM>" +
		"</BODY>" +
		"</HTML>";

		return exportDialog;
}

