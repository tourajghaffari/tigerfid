// CommPage.cpp : implementation file
//

#include "stdafx.h"
#include "TestAPI.h"
#include "TestAPIDlg.h"
#include "AWI_API.h"
#include "CommPage.h"
#include ".\commpage.h"
#include "Debug.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#ifdef _DEBUG_ACTIVEWAVE
extern long DebugEventFn( rfDebugEvent_t* debugEvent);
extern CDebug* debugWin;
#endif
#endif

//#ifdef _DEBUG
   //#undef _DEBUG_ACTIVEWAVE
//#endif

extern CWnd* listBox;
extern CTestAPIDlg* apiDlg;
extern int pktCounter;
extern bool readerRegistered;
extern bool tagRegistered;
extern CCommPage* comPage;
extern HANDLE* hConn;
extern int pktID;

extern long ReaderEvent(long status,
                        HANDLE funcId,
                        rfReaderEvent_t* readerEvent,
                        void* userArg);

extern long TagEvent(long status,
                     HANDLE funcId,
                     rfTagEvent_t* tagEvent,
                     void* userArg);

// CCommPage dialog

IMPLEMENT_DYNAMIC(CCommPage, CDialog)
CCommPage::CCommPage(CWnd* pParent /*=NULL*/)
	: CDialog(CCommPage::IDD, pParent)
{
	comPage = this;
}

CCommPage::~CCommPage()
{
}

void CCommPage::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_GROUP_COMM, m_commGroup);
	DDX_Control(pDX, IDC_BUTTON_RF_CLOSE, m_rs232Button);
	DDX_Control(pDX, IDC_LIST_IP, m_ipList);
	DDX_Control(pDX, IDC_EDIT_IP, m_ip);
}


BEGIN_MESSAGE_MAP(CCommPage, CDialog)
	ON_BN_CLICKED(IDC_BUTTON_RF_REG_READER, OnBnClickedButtonRfRegReader)
	ON_BN_CLICKED(IDC_RADIO_RS232, OnBnClickedRadioRs232)
	ON_BN_CLICKED(IDC_RADIO_NETWORK, OnBnClickedRadioNetwork)
	ON_BN_CLICKED(IDC_BUTTON_RF_REG_TAG, OnBnClickedButtonRfRegTag)
	ON_WM_ACTIVATE()
	ON_BN_CLICKED(IDC_BUTTON_RF_OPEN, OnBnClickedButtonRfOpen)
	ON_BN_CLICKED(IDC_BUTTON_RF_CLOSE, OnBnClickedButtonRfClose)
    ON_WM_CHILDACTIVATE()
    ON_BN_CLICKED(IDC_BUTTON_SCAN_NETWORK, OnBnClickedButtonScanNetwork)
	ON_BN_CLICKED(IDC_BUTTON_RF_OPEN_SOCKET, OnBnClickedButtonRfOpenSocket)
	ON_BN_CLICKED(IDC_BUTTON_RF_CLOSE_SOCKET, OnBnClickedButtonRfCloseSocket)
	ON_WM_ACTIVATEAPP()
	ON_BN_CLICKED(IDC_BUTTON_RF_SCAN_IP, OnBnClickedButtonRfScanIp)
	ON_BN_CLICKED(IDC_RADIO_RADIO_SPECIFIC_IP, OnBnClickedRadioRadioSpecificIp)
	ON_BN_CLICKED(IDC_RADIO_ALL_IPS, OnBnClickedRadioAllIps)
	ON_BN_CLICKED(IDC_BUTTON_RF_RESET_READER_SOCKET, OnBnClickedButtonRfResetReaderSocket)
END_MESSAGE_MAP()

void CCommPage::OnActivateApp(BOOL bActive, DWORD dwThreadID)
{
	CDialog::OnActivateApp(bActive, dwThreadID);

	SetDlgItemInt( IDC_EDIT_COMM_HOST_ID, 1, false);
   ((CButton *)GetDlgItem(IDC_RADIO_ALL_IPS))->SetCheck(1);
   
}

void CCommPage::EnableRS232Group(bool b)
{
    ((CWnd *)GetDlgItem(IDC_GROUP_RS232))->EnableWindow(b);
	((CWnd *)GetDlgItem(IDC_RADIO_115200))->EnableWindow(b);
	((CWnd *)GetDlgItem(IDC_RADIO_9600))->EnableWindow(b);
	((CWnd *)GetDlgItem(IDC_STATIC_00))->EnableWindow(b);
	((CWnd *)GetDlgItem(IDC_EDIT_COM_PORT))->EnableWindow(b);
	((CWnd *)GetDlgItem(IDC_BUTTON_RF_OPEN))->EnableWindow(b);
}

void CCommPage::OnBnClickedRadioRs232()
{
	EnableRS232Group(true);	
	EnableNetworkGroup(false);
}

void CCommPage::EnableNetworkGroup(bool b)
{
    ((CWnd *)GetDlgItem(IDC_GROUP_NETWORK))->EnableWindow(b);
	((CWnd *)GetDlgItem(IDC_BUTTON_RF_OPEN_SOCKET))->EnableWindow(b);
	((CWnd *)GetDlgItem(IDC_BUTTON_RF_CLOSE_SOCKET))->EnableWindow(b);
	
	((CWnd *)GetDlgItem(IDC_GROUP_IPTYPE))->EnableWindow(b);
	((CWnd *)GetDlgItem(IDC_RADIO_RADIO_SPECIFIC_IP))->EnableWindow(b);
	((CWnd *)GetDlgItem(IDC_RADIO_ALL_IPS))->EnableWindow(b);
	((CWnd *)GetDlgItem(IDC_STATIC_03))->EnableWindow(b);
	((CWnd *)GetDlgItem(IDC_LIST_IP))->EnableWindow(b);
	if (((CButton *)GetDlgItem(IDC_RADIO_RADIO_SPECIFIC_IP))->GetCheck())
	{
		if (b)
		{
	      ((CWnd *)GetDlgItem(IDC_BUTTON_RF_SCAN_IP))->EnableWindow(true);
	      ((CWnd *)GetDlgItem(IDC_BUTTON_SCAN_NETWORK))->EnableWindow(false);
		}
		else
		{
           ((CWnd *)GetDlgItem(IDC_BUTTON_RF_SCAN_IP))->EnableWindow(false);
	       ((CWnd *)GetDlgItem(IDC_BUTTON_SCAN_NETWORK))->EnableWindow(false);
		}
	}
	else
	{
       if (b)
		{
	      ((CWnd *)GetDlgItem(IDC_BUTTON_RF_SCAN_IP))->EnableWindow(false);
	      ((CWnd *)GetDlgItem(IDC_BUTTON_SCAN_NETWORK))->EnableWindow(true);
		}
		else
		{
           ((CWnd *)GetDlgItem(IDC_BUTTON_RF_SCAN_IP))->EnableWindow(false);
	       ((CWnd *)GetDlgItem(IDC_BUTTON_SCAN_NETWORK))->EnableWindow(false);
		} 
	}
}

void CCommPage::CloseDebugWindow()
{
#ifdef _DEBUG_ACTIVEWAVE
	debugWin->CloseWindow();
	delete debugWin;
	debugWin = NULL;
#endif here
}

void CCommPage::OnBnClickedRadioNetwork()
{
	EnableRS232Group(false);	
	EnableNetworkGroup(true);
}

void CCommPage::OnBnClickedButtonRfRegReader()
{
	char buf[10];
	//--------------------------------------------------------------------
	// a way to test toggling Registering/Un-registering events
	if (readerRegistered == true)
	{
		rfRegisterReaderEvent(NULL, NULL);
		readerRegistered = false;

		CString str;
		itoa(++pktCounter, buf, 10);
		str = buf;
		str += " -";
		str += "   rfRegisterReaderEvent()   Un-Registered.";
		((CListBox *)listBox)->InsertString(0, str);

		((CWnd *)GetDlgItem(IDC_GROUP_COMM))->EnableWindow(false);
		((CWnd *)GetDlgItem(IDC_RADIO_RS232))->EnableWindow(false);
		((CWnd *)GetDlgItem(IDC_RADIO_NETWORK))->EnableWindow(false);
	}
	else
	{
		rfRegisterReaderEvent(ReaderEvent, NULL);
		readerRegistered = true;

		CString str;
		itoa(++pktCounter, buf, 10);
		str = buf;
		str += " -";
		str += "   rfRegisterReaderEvent()   Registered.";
		((CListBox *)listBox)->InsertString(0, str);

		((CWnd *)GetDlgItem(IDC_GROUP_COMM))->EnableWindow(true);
		((CWnd *)GetDlgItem(IDC_RADIO_RS232))->EnableWindow(true);
		((CWnd *)GetDlgItem(IDC_RADIO_NETWORK))->EnableWindow(true);
	}
	//--------------------------------------------------------------------

	/*
	rfRegisterReaderEvent(ReaderEvent, NULL);
	readerRegistered = true;

	CString str;
	itoa(++pktCounter, buf, 10);
	str = buf;
	str += " -";
	str += "   rfRegisterReaderEvent()   Registered.";
	((CListBox *)listBox)->InsertString(0, str);

	((CWnd *)GetDlgItem(IDC_GROUP_COMM))->EnableWindow(true);
	((CWnd *)GetDlgItem(IDC_RADIO_RS232))->EnableWindow(true);
	((CWnd *)GetDlgItem(IDC_RADIO_NETWORK))->EnableWindow(true);
	*/
    MessageBeep(0xFFFFFFFF);
	  
}

void CCommPage::OnBnClickedButtonRfRegTag()
{
	char buf[10];
	rfRegisterTagEvent(TagEvent, NULL);
	tagRegistered = true;
	CString str;
	itoa(++pktCounter, buf, 10);
	str = buf;
	str += " -";
	str += "   rfRegisterTagEvent()   Registered.";
	((CListBox *)listBox)->InsertString(0, str);
	MessageBeep(0xFFFFFFFF);
}

void CCommPage::OnBnClickedButtonRfOpen()
{
	char buf[10];
    hConn = new (HANDLE);

    int com = GetDlgItemInt(IDC_EDIT_COM_PORT, NULL,true);
	apiDlg->comType = 1;
	
	int ret;
    if (((CButton *)GetDlgItem(IDC_RADIO_115200))->GetCheck())
	{
       ret = rfOpen(115200, com, hConn);
	}
	else
	{
       ret = rfOpen(9600, com, hConn);
	}
	
	CString str;
	itoa(++pktCounter, buf, 10);
	str = buf;
	str += " -";
	str += "   rfOpen()  return Code = ";
	itoa(ret, buf, 10);
	str += buf;
	((CListBox *)listBox)->InsertString(0, str);
	MessageBeep(0xFFFFFFFF);
	((CWnd *)GetDlgItem(IDC_BUTTON_RF_OPEN))->EnableWindow(false);
	((CWnd *)GetDlgItem(IDC_BUTTON_RF_CLOSE))->EnableWindow(true);
}

void CCommPage::OnBnClickedButtonRfClose()
{
	char buf[10];
	int ret;

	//if (apiDlg->comType == 1)
	if (hConn != NULL)
	{
	   ret = rfClose(hConn);
	  if (ret == 0)
	  {
	     delete hConn;
	     hConn = NULL;
	  }
	}
	else 
		return;
	
    CString str;
	itoa(++pktCounter, buf, 10);
	str = buf;
	str += " -";
	str += "   rfClose()  return Code = ";
	itoa(ret, buf, 10);
	str += buf;
	((CListBox *)listBox)->InsertString(0, str);
	MessageBeep(0xFFFFFFFF);
	((CWnd *)GetDlgItem(IDC_BUTTON_RF_OPEN))->EnableWindow(true);
	((CWnd *)GetDlgItem(IDC_BUTTON_RF_CLOSE))->EnableWindow(false);
}

void CCommPage::OnChildActivate()
{
	CDialog::OnChildActivate();

	((CButton *)GetDlgItem(IDC_RADIO_115200))->SetCheck(true);
	((CWnd *)GetDlgItem(IDC_EDIT_COM_PORT))->SetWindowText("1");
	listIPBox = (CListBox *)GetDlgItem(IDC_LIST_IP);
    ((CButton *)GetDlgItem(IDC_RADIO_ALL_IPS))->SetCheck(true);
	((CButton *)GetDlgItem(IDC_RADIO_RS232))->SetCheck(true);

}

void CCommPage::OnBnClickedButtonScanNetwork()
{
	((CWnd *)GetDlgItem(IDC_BUTTON_RF_OPEN_SOCKET))->EnableWindow(true);
	 m_ipList.ResetContent();
	((CListBox *)listIPBox)->UpdateWindow();
	int ret = rfScanNetwork(++pktID);
	apiDlg->Display("rfScanNetwork() ", pktID, ret);
}

void CCommPage::OnBnClickedButtonRfOpenSocket()
{
	CString str;
	unsigned short fType;
	bool encryption = false;
	Byte ip[20];
	
	int hostID = GetDlgItemInt(IDC_EDIT_HOST_ID, NULL,true);
    
	if(((CButton *)GetDlgItem(IDC_RADIO_ALL_IPS))->GetCheck())
		fType = ALL_IPS;
	else
		fType = SPECIFIC_IP;

	if (fType == SPECIFIC_IP)
	{
		if (m_ip.LineLength(0) == 0)
		{
		   MessageBox("No IP address.", "API", MB_ICONHAND);
	       return;
		}
	    m_ip.GetLine(0, (char*)ip);
	}

	//int ret = rfOpenSocket((Byte *)str.GetBuffer(str.GetLength()), hostID, encryption, fType, pktID);
	int ret = rfOpenSocket((Byte *)ip, /*hostID*/1, encryption, fType, ++pktID);
	
	apiDlg->comType = 2;
	
	MessageBeep(0xFFFFFFFF);
}

void CCommPage::OnBnClickedButtonRfCloseSocket()
{
	char buf[10];
	Byte ip[20];
	Byte ip1[20];
	unsigned short fType;

	for (int i=0; i<20; i++)
       ip[i]=ip1[i]=0;

	if (apiDlg->comType == 2)
	{
	   if(((CButton *)GetDlgItem(IDC_RADIO_ALL_IPS))->GetCheck() == BST_CHECKED)
		  fType = ALL_IPS;
	   else
	   {
		  fType = SPECIFIC_IP;

		  if (fType == SPECIFIC_IP)
		  {
			if (m_ip.LineLength(0) == 0)
			{
				MessageBox("No IP address.", "API", MB_ICONHAND);
				return;
			}
			m_ip.GetWindowText((char*)ip, 20);
		  }
	   }

	   int ret = rfCloseSocket(ip, fType);
       CString str;
 
	   itoa(pktCounter, buf, 10);
	   str = buf;
	   str += " -";
	   str = "   rfCloseSocket()  return Code = ";
	   itoa(ret, buf, 10);
	   str += buf;
	   ((CListBox *)listBox)->InsertString(0, str);
	   if (ret == 0)
	   {
		   if (fType == ALL_IPS)
		   {
			   m_ipList.ResetContent();
		   }
		   else
		   {
			   for (int i=0; i < m_ipList.GetCount(); i++)
			   {    
					m_ipList.GetText(i, (char*)ip1);
					if (strcmp((char*)ip1, (char*)ip) == 0)
					{
						m_ipList.DeleteString(i);
					    break;
					}
				}
			    
		   }

		   MessageBeep(0xFFFFFFFF);
	   }
	}
}

void CCommPage::OnBnClickedButtonRfScanIp()
{
	Byte ip[20];
	m_ip.GetLine(0, (char*)ip);
	int ret = rfScanIP(ip, ++pktID); 
}

void CCommPage::OnBnClickedRadioRadioSpecificIp()
{
	((CWnd *)GetDlgItem(IDC_EDIT_IP))->EnableWindow(true);
	((CWnd *)GetDlgItem(IDC_BUTTON_RF_SCAN_IP))->EnableWindow(true);
	((CWnd *)GetDlgItem(IDC_BUTTON_RF_RESET_READER_SOCKET))->EnableWindow(true);
	((CWnd *)GetDlgItem(IDC_BUTTON_SCAN_NETWORK))->EnableWindow(false);
}

void CCommPage::OnBnClickedRadioAllIps()
{
	((CWnd *)GetDlgItem(IDC_EDIT_IP))->EnableWindow(false);
	((CWnd *)GetDlgItem(IDC_BUTTON_RF_SCAN_IP))->EnableWindow(false);
	((CWnd *)GetDlgItem(IDC_BUTTON_RF_RESET_READER_SOCKET))->EnableWindow(false);
	((CWnd *)GetDlgItem(IDC_BUTTON_SCAN_NETWORK))->EnableWindow(true);
}

void CCommPage::OnBnClickedButtonRfResetReaderSocket()
{
	CString str;
	//unsigned short fType;
	bool encryption = false;
	Byte ip[20];
	
	int hostID = GetDlgItemInt(IDC_EDIT_HOST_ID, NULL,true);
    
	if (m_ip.LineLength(0) == 0)
	{
		MessageBox("No IP address.", "API", MB_ICONHAND);
	    return;
	}

	m_ip.GetLine(0, (char*)ip);
	
	int ret = rfResetReaderSocket(1, (Byte *)ip, ++pktID);
	apiDlg->Display("rfResetReaderSocket() ", pktID, ret);
	apiDlg->comType = 2;
	
	MessageBeep(0xFFFFFFFF);
}
