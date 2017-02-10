var IE_PRINT_CONTROL_PRODUCTVERSION = "10,2,0,1092"
var NS_PRINT_CONTROL_PRODUCTVERSION = "10.2.0.1092"

function blockEvents() {
    var deadend;
    opener.captureEvents(Event.CLICK, Event.MOUSEDOWN, Event.MOUSEUP, Event.FOCUS);
    opener.onclick = deadend;
    opener.onmousedown = deadend;
    opener.onmouseup = deadend;
    opener.focus = deadend;
}

function unblockEvents() {
    opener.releaseEvents(Event.CLICK, Event.MOUSEDOWN, Event.MOUSEUP, Event.FOCUS);
    opener.onclick = null;
    opener.mousedown = null;
    opener.mouseup = null;
    opener.onfocus = null;
}

function finished() {
    setTimeout("close()", 1000);
}

function installNsPlugin(pluginUrl, clientVersionRegistry) {
    var err = InstallTrigger.compareVersion(clientVersionRegistry, NS_PRINT_CONTROL_PRODUCTVERSION);

    if (err < 0)
    {
        xpi={'Crystal Reports ActiveX Print Control Plug-in':pluginUrl};
        InstallTrigger.install(xpi, callback);
    }
}

function callback(url, status) {
    if (status) {
        alert("Installation of the ActiveX Print Control failed.  Error code: " + status);
    }
}

function checkModal(dlgWindow) {
    if (dlgWindow && !dlgWindow.closed)
    dlgWindow.focus();
}

function cancelPrinting(printControl) {
    if (printControl && printControl.IsBusy) {
        printControl.CancelPrinting();
    }
}

function checkUserCancelledInstallation(printControl) {
    if (printControl && printControl.IsBusy == undefined)
        close();
}
