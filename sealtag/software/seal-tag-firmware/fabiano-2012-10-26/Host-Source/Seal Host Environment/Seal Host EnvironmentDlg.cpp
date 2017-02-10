// Seal Host EnvironmentDlg.cpp : implementation file
//

#include "stdafx.h"
#include "Seal Host Environment.h"
#include "Seal Host EnvironmentDlg.h"
#include "feusb.h"
#include "feisc.h"
#include "FeigReader.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/* global variables */
	FeigReader rfRdr;
	int iDeviceHandle;
	int iFeiscHandle;
	bool debug = false;
	CString language = _T("English");

// CSealHostEnvironmentDlg dialog




CSealHostEnvironmentDlg::CSealHostEnvironmentDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CSealHostEnvironmentDlg::IDD, pParent)
	, m_statusText(_T(""))
	, m_comboBox1(_T(""))
	, m_comboBox2(_T(""))
	, m_comboBox3(_T(""))
	, m_comboBox4(_T(""))
	, m_comboBox5(_T(""))
	, m_comboBox6(_T(""))
	, m_comboBox7(_T(""))
	, m_comboBox8(_T(""))
	, m_comboBoxCommand(_T(""))
	, m_comboBox9(_T(""))
	, m_comboBox10(_T(""))
	, m_comboBox11(_T(""))
	, m_comboBox12(_T(""))
	, m_edReset(_T(""))
	, m_edHistory(_T(""))
{
	EnableActiveAccessibility();
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CSealHostEnvironmentDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_EDIT1, statusText);
	DDX_Text(pDX, IDC_EDIT1, m_statusText);
	DDX_Control(pDX, IDC_COMBO1, comboBox1);
	DDX_Control(pDX, IDC_COMBO2, comboBox2);
	DDX_Control(pDX, IDC_COMBO3, comboBox3);
	DDX_Control(pDX, IDC_COMBO4, comboBox4);
	DDX_CBString(pDX, IDC_COMBO1, m_comboBox1);
	DDX_CBString(pDX, IDC_COMBO2, m_comboBox2);
	DDX_CBString(pDX, IDC_COMBO3, m_comboBox3);
	DDX_CBString(pDX, IDC_COMBO4, m_comboBox4);
	DDX_Control(pDX, IDC_BUTTON3, btnRead);
	DDX_Control(pDX, IDC_BUTTON2, btnClose);
	DDX_Control(pDX, IDC_BUTTON1, btnOpen);
	DDX_Control(pDX, IDC_BUTTON4, btnWrite);
	DDX_Control(pDX, IDC_COMBO5, comboBox5);
	DDX_CBString(pDX, IDC_COMBO5, m_comboBox5);
	DDX_Control(pDX, IDC_COMBO6, comboBox6);
	DDX_CBString(pDX, IDC_COMBO6, m_comboBox6);
	DDX_Control(pDX, IDC_COMBO7, comboBox7);
	DDX_CBString(pDX, IDC_COMBO7, m_comboBox7);
	DDX_Control(pDX, IDC_COMBO8, comboBox8);
	DDX_CBString(pDX, IDC_COMBO8, m_comboBox8);
	DDX_Control(pDX, IDC_COMBO9, comboBox9);
	DDX_CBString(pDX, IDC_COMBO9, m_comboBox9);
	DDX_Control(pDX, IDC_COMBO10, comboBox10);
	DDX_CBString(pDX, IDC_COMBO10, m_comboBox10);
	DDX_Control(pDX, IDC_COMBO11, comboBox11);
	DDX_CBString(pDX, IDC_COMBO11, m_comboBox11);
	DDX_Control(pDX, IDC_COMBO12, comboBox12);
	DDX_CBString(pDX, IDC_COMBO12, m_comboBox12);
	DDX_Control(pDX, IDC_BUTTON_START_TIMER, btnStartTimer);
	DDX_Control(pDX, IDC_BUTTON_RTC_CALIBRATE_START, btnRTCCalStart);
	DDX_Control(pDX, IDC_BUTTON_RTC_CALIBRATE_READ, btnRTCCalRead);
	DDX_Control(pDX, IDC_BUTTON_SERIAL, btnReadSerial);
	DDX_Control(pDX, IDC_BUTTON_BATTERY, btnReadBattery);
	DDX_Control(pDX, IDC_BUTTON_UID, btnReadUID);
	DDX_Control(pDX, IDC_BUTTON_STOP_TIMER, btnStopTimer);
	DDX_Control(pDX, IDC_BUTTON_READ_5, btnRead5);
	DDX_Control(pDX, IDC_BUTTON_READ_1, btnRead1);
	DDX_Control(pDX, IDC_BUTTON_FLUSH, btnFlush);
	DDX_Control(pDX, IDC_EDIT2, edReset);
	DDX_Text(pDX, IDC_EDIT2, m_edReset);
	DDV_MaxChars(pDX, m_edReset, 2);
	DDX_Control(pDX, IDC_BUTTON_READ_HISTORY, btnHistory);
	DDX_Control(pDX, IDC_EDIT3, edHistory);
	DDX_Text(pDX, IDC_EDIT3, m_edHistory);
	DDV_MaxChars(pDX, m_edHistory, 3);
	DDX_Control(pDX, IDC_BUTTON_HISTORY2, btnHistory2);
	DDX_Control(pDX, IDC_BUTTON_READ_ALL_HISTORY, btnAllHistory);
	DDX_Control(pDX, IDC_IMG_LOGO, logoGTP);
	DDX_Control(pDX, IDC_LOGO_HUNTER, logoHunter);
}

BEGIN_MESSAGE_MAP(CSealHostEnvironmentDlg, CDialog)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	//}}AFX_MSG_MAP
	ON_BN_CLICKED(IDC_BUTTON1, &CSealHostEnvironmentDlg::OnBnClickedButton1)
	ON_BN_CLICKED(IDC_BUTTON2, &CSealHostEnvironmentDlg::OnBnClickedButton2)
	ON_BN_CLICKED(IDC_BUTTON3, &CSealHostEnvironmentDlg::OnBnClickedButton3)
	ON_BN_CLICKED(IDC_BUTTON4, &CSealHostEnvironmentDlg::OnBnClickedButton4)
	ON_BN_CLICKED(IDC_BUTTON_START_TIMER, &CSealHostEnvironmentDlg::OnBnClickedButtonStartTimer)
	ON_BN_CLICKED(IDC_BUTTON_STOP_TIMER, &CSealHostEnvironmentDlg::OnBnClickedButtonStopTimer)
	ON_BN_CLICKED(IDC_BUTTON_READ_5, &CSealHostEnvironmentDlg::OnBnClickedButtonRead5)
	ON_BN_CLICKED(IDC_BUTTON_READ_1, &CSealHostEnvironmentDlg::OnBnClickedButtonRead1)
	ON_BN_CLICKED(IDC_BUTTON_FLUSH, &CSealHostEnvironmentDlg::OnBnClickedButtonFlush)
	ON_BN_CLICKED(IDC_BUTTON_READ_HISTORY, &CSealHostEnvironmentDlg::OnBnClickedButtonReadHistory)
	ON_BN_CLICKED(IDC_BUTTON_HISTORY2, &CSealHostEnvironmentDlg::OnBnClickedButtonHistory2)
	ON_BN_CLICKED(IDC_BUTTON_READ_ALL_HISTORY, &CSealHostEnvironmentDlg::OnBnClickedButtonReadAllHistory)
	ON_STN_DBLCLK(IDC_RUNTIME, &CSealHostEnvironmentDlg::OnStnDblclickRuntime)
	ON_STN_CLICKED(IDC_RUNTIME, &CSealHostEnvironmentDlg::OnStnClickedRuntime)
	ON_BN_CLICKED(IDC_BUTTON_RTC_CALIBRATE_START, &CSealHostEnvironmentDlg::OnBnClickedButtonRtcCalibrateStart)
	ON_BN_CLICKED(IDC_BUTTON_RTC_CALIBRATE_READ, &CSealHostEnvironmentDlg::OnBnClickedButtonRtcCalibrateRead)
	ON_BN_CLICKED(IDC_BUTTON_SERIAL, &CSealHostEnvironmentDlg::OnBnClickedButtonSerial)
    ON_BN_CLICKED(IDC_BUTTON_BATTERY, &CSealHostEnvironmentDlg::OnBnClickedButtonBattery)
    ON_BN_CLICKED(IDC_BUTTON_UID, &CSealHostEnvironmentDlg::OnBnClickedButtonUid)
END_MESSAGE_MAP()


// CSealHostEnvironmentDlg message handlers

BOOL CSealHostEnvironmentDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	ShowWindow(SW_NORMAL);

	comboBox1.SetCurSel(0);
	comboBox2.SetCurSel(0);
	comboBox3.SetCurSel(0);
	comboBox4.SetCurSel(0);
	comboBox5.SetCurSel(0);
	comboBox6.SetCurSel(1);
	comboBox7.SetCurSel(0);
	comboBox8.SetCurSel(0);
	comboBox9.SetCurSel(0);
	comboBox10.SetCurSel(0);
	comboBox11.SetCurSel(0);
	comboBox12.SetCurSel(0);
	m_edReset.Format(_T("00"));
	edReset.SetWindowTextW(m_edReset);
	m_edHistory.Format(_T("0"));
	edHistory.SetWindowTextW(m_edHistory);

	// TODO: Add extra initialization here

	initApp();
	return TRUE;  // return TRUE  unless you set the focus to a control
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CSealHostEnvironmentDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CSealHostEnvironmentDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}


void CSealHostEnvironmentDlg::OnBnClickedButton1()
{
	m_statusText = "Scanning Devices\r\n";
	statusText.SetWindowTextW(m_statusText);
	char sDeviceHandle[32];
	char sDeviceName[32];
	char sDeviceSerialNumber[32];
	char sDeviceFamilyName[32];
	char sDevicePresence[2];
	DWORD dwDeviceSerialNumber = 0;
	FEUSB_ClearScanList();/* check USB device connected */
	int iErr;
	int timeout = 10;
	while (FEUSB_Scan(FEUSB_SCAN_ALL,NULL) < 0)
	{
		statusText.GetWindowTextW(m_statusText);
		m_statusText.AppendFormat(_T("."));
		statusText.SetWindowTextW(m_statusText);

		timeout--;
		if(timeout == 0){
			break;
		}
	}
	int nDev = FEUSB_GetScanListSize();
	statusText.GetWindowTextW(m_statusText);
	m_statusText.AppendFormat(_T("Found %d devices.\r\n"),nDev);
	statusText.SetWindowTextW(m_statusText);
	if(nDev > 0 || debug){
		/* GetScanListPara : Get Detected FEIG USB device parameters */
		FEUSB_GetScanListPara( 0, "DeviceName", sDeviceName ) ;
		FEUSB_GetScanListPara( 0, "Device-ID", sDeviceSerialNumber ) ;
		FEUSB_GetScanListPara( 0, "DeviceHnd", sDeviceHandle ) ;
		FEUSB_GetScanListPara( 0, "FamilyName", sDeviceFamilyName ) ;
		FEUSB_GetScanListPara( 0, "Present", sDevicePresence ) ;
		/* convert receive data in hexa format */
		sscanf_s((const char*)sDeviceSerialNumber, "%lx",&dwDeviceSerialNumber);
		/* Open Communciation with one FEIG USB device */
		/* and get USB hanle for FEUSB functions */
		iDeviceHandle = FEUSB_OpenDevice(dwDeviceSerialNumber);
		if (iDeviceHandle < 0)
		{
			/* Error : USB connection problem */
			/* Close USB connection */
			iErr = FEUSB_CloseDevice(iDeviceHandle);
			statusText.GetWindowTextW(m_statusText);
			m_statusText.AppendFormat(_T("Error opening device: %d\r\n"),iErr);
			statusText.SetWindowTextW(m_statusText);
//			break;
		}
		/* Link FEIG USB device with FEISC functions */
		/* and get USB hanle */
		iFeiscHandle = FEISC_NewReader( iDeviceHandle );
		if (iFeiscHandle < 0 && !debug)
		{
			/* Error : USB connection problem */
			/* Close USB connection */
			iErr = FEISC_DeleteReader( iFeiscHandle );
			statusText.GetWindowTextW(m_statusText);
			m_statusText.AppendFormat(_T("Error linking device to driver: %d\r\n"),iErr);
			statusText.SetWindowTextW(m_statusText);
//			break;
		}
		else
		{
			/* USB connection OK */
			/* iDeviceHandle is handle for FEUSB functions */
			/* iFeiscHandle is handle for FEISC functions */
			statusText.GetWindowTextW(m_statusText);
			m_statusText.AppendFormat(_T("Connected to device:\r\n"));
			//m_statusText.AppendFormat(_T("\r\n\tFamily Name: %s"),sDeviceFamilyName);
			//m_statusText.AppendFormat(_T("\r\n\tName: %s"),sDeviceName);
			//m_statusText.AppendFormat(_T("\r\n\tSerial Number: %s"),sDeviceSerialNumber);
			m_statusText.AppendFormat(_T("\tDevice Handler: %d\r\n"), iDeviceHandle);
			m_statusText.AppendFormat(_T("\tReader Handler: %d\r\n"), iFeiscHandle);
			statusText.SetWindowTextW(m_statusText);
			btnRead.EnableWindow(TRUE);
			btnWrite.EnableWindow(TRUE);
			btnOpen.EnableWindow(FALSE);
			btnClose.EnableWindow(TRUE);
			comboBox1.EnableWindow(TRUE);
			comboBox2.EnableWindow(TRUE);
			comboBox3.EnableWindow(TRUE);
			comboBox4.EnableWindow(TRUE);
			comboBox5.EnableWindow(TRUE);
			comboBox6.EnableWindow(TRUE);
			comboBox7.EnableWindow(TRUE);
			comboBox8.EnableWindow(TRUE);
			comboBox9.EnableWindow(TRUE);
			comboBox10.EnableWindow(TRUE);
			comboBox11.EnableWindow(TRUE);
			comboBox12.EnableWindow(TRUE);
		}
	} else {
		btnRead.EnableWindow(FALSE);
		btnWrite.EnableWindow(FALSE);
		btnOpen.EnableWindow(TRUE);
		btnClose.EnableWindow(FALSE);
		comboBox1.EnableWindow(FALSE);
		comboBox2.EnableWindow(FALSE);
		comboBox3.EnableWindow(FALSE);
		comboBox4.EnableWindow(FALSE);
		comboBox5.EnableWindow(FALSE);
		comboBox6.EnableWindow(FALSE);
		comboBox7.EnableWindow(FALSE);
		comboBox8.EnableWindow(FALSE);
		comboBox9.EnableWindow(FALSE);
		comboBox10.EnableWindow(FALSE);
		comboBox11.EnableWindow(FALSE);
		comboBox12.EnableWindow(FALSE);
	}
}

void CSealHostEnvironmentDlg::OnBnClickedButton2()
{
	if (iDeviceHandle > 0 || debug)
	{
		/* Close USB connection */
		FEUSB_CloseDevice(iDeviceHandle);
		/* Unlink reader */
		FEISC_DeleteReader(iFeiscHandle);
		statusText.GetWindowTextW(m_statusText);
		m_statusText.AppendFormat(_T("Device disconnected.\r\n"));
		statusText.SetWindowTextW(m_statusText);
		btnRead.EnableWindow(FALSE);
		btnWrite.EnableWindow(FALSE);
		btnOpen.EnableWindow(TRUE);
		btnClose.EnableWindow(FALSE);
		comboBox1.EnableWindow(FALSE);
		comboBox2.EnableWindow(FALSE);
		comboBox3.EnableWindow(FALSE);
		comboBox4.EnableWindow(FALSE);
		comboBox5.EnableWindow(FALSE);
		comboBox6.EnableWindow(FALSE);
		comboBox7.EnableWindow(FALSE);
		comboBox8.EnableWindow(FALSE);
		comboBox9.EnableWindow(FALSE);
		comboBox10.EnableWindow(FALSE);
		comboBox11.EnableWindow(FALSE);
		comboBox12.EnableWindow(FALSE);
	}
	//initApp();
	rfRdr.disconnect();
	OnBnClickedButton1();
}

void CSealHostEnvironmentDlg::OnBnClickedButton3()
{
	statusText.GetWindowTextW(m_statusText);
	int i;
	int iRspLength=56;
	int iReqLen,iRspLen,iResult;
	unsigned char sReqData[32]={0};
	unsigned char sRspData[32]={0};

	comboBox1.GetLBText(comboBox1.GetCurSel(),m_comboBox1);
	comboBox2.GetLBText(comboBox2.GetCurSel(),m_comboBox2);
	comboBox3.GetLBText(comboBox3.GetCurSel(),m_comboBox3);
	comboBox4.GetLBText(comboBox4.GetCurSel(),m_comboBox4);

	unsigned char memory[4];
	memory[0] = (m_comboBox1[0] >= 'A' && m_comboBox1[0] <= 'Z') ? m_comboBox1[0] - 'A' + 0xA : m_comboBox1[0] - '0';
	memory[1] = (m_comboBox2[0] >= 'A' && m_comboBox2[0] <= 'Z') ? m_comboBox2[0] - 'A' + 0xA : m_comboBox2[0] - '0';
	memory[2] = (m_comboBox3[0] >= 'A' && m_comboBox3[0] <= 'Z') ? m_comboBox3[0] - 'A' + 0xA : m_comboBox3[0] - '0';
	memory[3] = (m_comboBox4[0] >= 'A' && m_comboBox4[0] <= 'Z') ? m_comboBox4[0] - 'A' + 0xA : m_comboBox4[0] - '0';

	sReqData[0] = (UCHAR)0x02; //Reader parameter
	sReqData[1] = (UCHAR)0x1F; //Reader parameter
	sReqData[2] = (UCHAR)0x0A; //Flag
	sReqData[3] = (UCHAR)0x20; //RF ISO 15693 Read single block command
	sReqData[4] = (unsigned char)(memory[2]*16 + memory[3]);
	sReqData[5] = (unsigned char)(memory[0]*16 + memory[1]);
	//Last byte is first

	iReqLen = 6; //(number of characters :param=2 in request)
	m_statusText.AppendFormat(_T(">>> RF Read at adress %.2X %.2X: \r\n"),sReqData[5], sReqData[4]);
	m_statusText.AppendFormat(_T("\t--> request : 02 1F 0A 20 "));
	for (i=4; i<iReqLen; i++){
		m_statusText.AppendFormat(_T("%.2x"),sReqData[i]);
	}
	m_statusText.AppendFormat(_T("\r\n"));
	/* FEIG USB request in transparent mode */
	iResult = FEISC_0xBF_ISOTranspCmd (iFeiscHandle, //USB Handle 
									   0xFF, //Communication address
									   1, //MODE 1 : read answer 
									   iRspLength,
									   sReqData,//request 
									   iReqLen,//USB request length
									   &sRspData[0],//answer
									   &iRspLen,
									   2); //length format 2 : Number of Bytes
	/* RF REQUEST RESULT */
	if(iResult == 0){
		//m_statusText.AppendFormat(_T("Command Successful\r\n"));
		//m_statusText.AppendFormat(_T("\r\n --> Answer : "));
		if (iRspLen == 0){
			m_statusText.AppendFormat(_T("No tag answer received\r\n"));
		} else {/*
			for (i = 0; i < iRspLen; i += 2){
				m_statusText.AppendFormat(_T("%c%c "),sRspData[i],sRspData[i+1]);
			}
			m_statusText.AppendFormat(_T("\r\n"));*/
			m_statusText.Format(_T("bit[7:0]: %c%c\r\n"),sRspData[2],sRspData[3]);
			m_statusText.AppendFormat(_T("bit[15:8]: %c%c\r\n"),sRspData[4],sRspData[5]);
			m_statusText.AppendFormat(_T("bit[23:16]: %c%c\r\n"),sRspData[6],sRspData[7]);
			m_statusText.AppendFormat(_T("bit[31:24]: %c%c\r\n"),sRspData[8],sRspData[9]);
		}
	} else {
		m_statusText.AppendFormat(_T("Error: %d\r\n"),iResult);
	}
	statusText.SetWindowTextW(m_statusText);
}


void CSealHostEnvironmentDlg::OnBnClickedButton4()
{
	statusText.GetWindowTextW(m_statusText);
	int i;
	int iRspLength=56;
	int iReqLen,iRspLen,iResult;
	unsigned char sReqData[32]={0};
	unsigned char sRspData[32]={0};

	comboBox1.GetLBText(comboBox1.GetCurSel(),m_comboBox1);
	comboBox2.GetLBText(comboBox2.GetCurSel(),m_comboBox2);
	comboBox3.GetLBText(comboBox3.GetCurSel(),m_comboBox3);
	comboBox4.GetLBText(comboBox4.GetCurSel(),m_comboBox4);
	comboBox5.GetLBText(comboBox5.GetCurSel(),m_comboBox5);
	comboBox6.GetLBText(comboBox6.GetCurSel(),m_comboBox6);
	comboBox7.GetLBText(comboBox7.GetCurSel(),m_comboBox7);
	comboBox8.GetLBText(comboBox8.GetCurSel(),m_comboBox8);
	comboBox9.GetLBText(comboBox9.GetCurSel(),m_comboBox9);
	comboBox10.GetLBText(comboBox10.GetCurSel(),m_comboBox10);
	comboBox11.GetLBText(comboBox11.GetCurSel(),m_comboBox11);
	comboBox12.GetLBText(comboBox12.GetCurSel(),m_comboBox12);

	unsigned char memory[4];
	memory[0] = (m_comboBox1[0] >= 'A' && m_comboBox1[0] <= 'Z') ? m_comboBox1[0] - 'A' + 0xA : m_comboBox1[0] - '0';
	memory[1] = (m_comboBox2[0] >= 'A' && m_comboBox2[0] <= 'Z') ? m_comboBox2[0] - 'A' + 0xA : m_comboBox2[0] - '0';
	memory[2] = (m_comboBox3[0] >= 'A' && m_comboBox3[0] <= 'Z') ? m_comboBox3[0] - 'A' + 0xA : m_comboBox3[0] - '0';
	memory[3] = (m_comboBox4[0] >= 'A' && m_comboBox4[0] <= 'Z') ? m_comboBox4[0] - 'A' + 0xA : m_comboBox4[0] - '0';

	unsigned char command[4];
	command[0] = (m_comboBox5[0] >= 'A' && m_comboBox5[0] <= 'Z') ? m_comboBox5[0] - 'A' + 0xA : m_comboBox5[0] - '0';
	command[1] = (m_comboBox6[0] >= 'A' && m_comboBox6[0] <= 'Z') ? m_comboBox6[0] - 'A' + 0xA : m_comboBox6[0] - '0';
	command[2] = (m_comboBox7[0] >= 'A' && m_comboBox7[0] <= 'Z') ? m_comboBox7[0] - 'A' + 0xA : m_comboBox7[0] - '0';
	command[3] = (m_comboBox8[0] >= 'A' && m_comboBox8[0] <= 'Z') ? m_comboBox8[0] - 'A' + 0xA : m_comboBox8[0] - '0';

	unsigned char data[4];
	data[0] = (m_comboBox9[0] >= 'A' && m_comboBox9[0] <= 'Z') ? m_comboBox9[0] - 'A' + 0xA : m_comboBox9[0] - '0';
	data[1] = (m_comboBox10[0] >= 'A' && m_comboBox10[0] <= 'Z') ? m_comboBox10[0] - 'A' + 0xA : m_comboBox10[0] - '0';
	data[2] = (m_comboBox11[0] >= 'A' && m_comboBox11[0] <= 'Z') ? m_comboBox11[0] - 'A' + 0xA : m_comboBox11[0] - '0';
	data[3] = (m_comboBox12[0] >= 'A' && m_comboBox12[0] <= 'Z') ? m_comboBox12[0] - 'A' + 0xA : m_comboBox12[0] - '0';

	sReqData[0] = (UCHAR)0x02; //Reader parameter
	sReqData[1] = (UCHAR)0x1F; //Reader parameter
	sReqData[2] = (UCHAR)0x0A; //Flag
	sReqData[3] = (UCHAR)0x21; //RF ISO 15693 Write single block command
	sReqData[4] = (unsigned char)(memory[2]*16 + memory[3]);
	sReqData[5] = (unsigned char)(memory[0]*16 + memory[1]);
	sReqData[6] = (unsigned char)(command[2]*16 + command[3]);
	sReqData[7] = (unsigned char)(command[0]*16 + command[1]);
	sReqData[8] = (unsigned char)(data[0]*16 + data[1]);
	sReqData[9] = (unsigned char)(data[2]*16 + data[3]);
	iReqLen = 10; // (number of characters :param=2 in request)
	//Last byte is first

	m_statusText.AppendFormat(_T(">>> RF Write at adress %.2X %.2X data %.2X %.2X %.2X %.2X:\r\n"),
							  sReqData[5],
							  sReqData[4],
							  sReqData[6],
							  sReqData[7],
							  sReqData[8],
							  sReqData[9]);
	m_statusText.AppendFormat(_T("\t--> request : 021F0A21 "));
	for (i=4; i<iReqLen; i++){
		m_statusText.AppendFormat(_T("%.2X"),sReqData[i]);
	}
	m_statusText.AppendFormat(_T("\r\n"));
	/* FEIG USB request in transparent mode */
	iResult = FEISC_0xBF_ISOTranspCmd (iFeiscHandle, 0xFF,// USB parameters
									   2,// MODE 2 : write like answer
									   iRspLength,
									   sReqData,// request
									   iReqLen,// USB request length
									   &sRspData[0],// answer
									   &iRspLen,
									   2 );// length format 2 : Number of Bytes
	//RF REQUEST RESULT
	if(iResult == 0){
		m_statusText.AppendFormat(_T("Command Successful\r\n"));
		m_statusText.AppendFormat(_T("\t--> Answer : "));
		if (iRspLen == 0){
			m_statusText.AppendFormat(_T("No tag answer received\r\n"));
		} else {
			for (i=0; i<iRspLen; i+=2){
				m_statusText.AppendFormat(_T("%c%c "),sRspData[i],sRspData[i+1]);
			}
			m_statusText.AppendFormat(_T("\r\n"));
		}
	} else {
		m_statusText.AppendFormat(_T("Error: %d\r\n"),iResult);
	}
	statusText.SetWindowTextW(m_statusText);
}

void CSealHostEnvironmentDlg::OnBnClickedButtonStartTimer()
{
	struct tm* localtime_s (const time_t *pt);
	time_t curr;
	tm local;
	time(&curr); // get current time_t value
	local=*(localtime(&curr)); // dereference and assign
	unsigned char dsemana,hora,minuto,segundo,dia,mes,ano;
	dsemana = local.tm_wday + 1;
	hora = local.tm_hour;
	minuto = local.tm_min;
	segundo = local.tm_sec;

	dia = local.tm_mday;
	mes = local.tm_mon + 1;
	ano = local.tm_year - 100;

	unsigned char memoryBank[2] = {0x00,0x00};
	unsigned char data[2] = {0xEE,0xEE};
	unsigned char cmd = 0x02;
	unsigned char status = 0x01;
	unsigned char *resp;
	int retV;

	retV = rfRdr.write(memoryBank,status,cmd,data);
	Sleep(_DELAY);

	// SET RTC

	memoryBank[0] = 0x00;
	memoryBank[1] = 0x02;
	unsigned char rtc[2] = {mes,dsemana};

	retV = rfRdr.write(memoryBank,dia,ano,rtc);
	Sleep(_DELAY);

	memoryBank[0] = 0x00;
	memoryBank[1] = 0x03;
	rtc[0] = hora;
	rtc[1] = 0xFF;
	retV = rfRdr.write(memoryBank,minuto,segundo,rtc);
	Sleep(_DELAY);

	// done
	boolean erro = false;
	int i = 0;
	memoryBank[0] = 0x00;
	memoryBank[1] = 0x00;
	do {
		Sleep(_DELAY);
		retV = rfRdr.read(memoryBank);
		resp = rfRdr.getRsp();
		status = rfRdr.getRsp()[4]*16 + rfRdr.getRsp()[5];
		i++;
	} while(status != '2'  && i < 1);
	if(status != '2'){
		if(!language.CompareNoCase(_T("English"))){
			m_statusText.Format(_T("Error: No answer from the Micro-Controller"));
		} else {
			m_statusText.Format(_T("Erro: Sem resposta do Micro-Controlador"));
		}
		erro = true;
	}

	if(!erro){
		memoryBank[0] = 0x00;
		memoryBank[1] = 0x00;
		status = 0x03;
		retV = rfRdr.write(memoryBank,status,cmd,data);

		i = 0;
		do{
			Sleep(_DELAY * 2);
			retV = rfRdr.read(memoryBank);
			resp = rfRdr.getRsp();
			status = rfRdr.getRsp()[4]*16 + rfRdr.getRsp()[5];
			i++;
		}while((status == '3' || status == '4')  && i < _TIMEOUT);
		if(i == _TIMEOUT) {
			m_statusText.Format(_T("Timeout!"));
			//m_statusText.Format(_T("Error (Timeout) !!\r\n"));
			//m_statusText.AppendFormat(_T("bit[7:0]: %c%c\r\n"),resp[2],resp[3]);
			//m_statusText.AppendFormat(_T("bit[15:8]: %c%c\r\n"),resp[4],resp[5]);
			//m_statusText.AppendFormat(_T("bit[23:16]: %c%c\r\n"),resp[6],resp[7]);
			//m_statusText.AppendFormat(_T("bit[31:24]: %c%c\r\n"),resp[8],resp[9]);
		} else if(rfRdr.getRspLen() > 0 && status == '0') {
			if(!language.CompareNoCase(_T("English"))) {
				m_statusText.Format(_T("Timer started!!\r\n"));
			} else {
				m_statusText.Format(_T("Cronômetro Iniciado!"));
			}
			//m_statusText.Format(_T("Command Successful!!\r\n"));
			//m_statusText.AppendFormat(_T("bit[7:0]: %c%c\r\n"),resp[2],resp[3]);
			//m_statusText.AppendFormat(_T("bit[15:8]: %c%c\r\n"),resp[4],resp[5]);
			//m_statusText.AppendFormat(_T("bit[23:16]: %c%c\r\n"),resp[6],resp[7]);
			//m_statusText.AppendFormat(_T("bit[31:24]: %c%c\r\n"),resp[8],resp[9]);
		} else if(rfRdr.getRspLen() > 0 || status != '0') {
			m_statusText.Format(_T("Error: %s"),getCmdStatus(getNumber(resp[4],resp[5])));
			//m_statusText.Format(_T("Error (Status) !!\r\n"));
			//m_statusText.AppendFormat(_T("bit[7:0]: %c%c\r\n"),resp[2],resp[3]);
			//m_statusText.AppendFormat(_T("bit[15:8]: %c%c\r\n"),resp[4],resp[5]);
			//m_statusText.AppendFormat(_T("bit[23:16]: %c%c\r\n"),resp[6],resp[7]);
			//m_statusText.AppendFormat(_T("bit[31:24]: %c%c\r\n"),resp[8],resp[9]);
		} else {
			m_statusText.Format(_T("Error, move the seal!\r\n"));
		}	
	}
	statusText.SetWindowTextW(m_statusText);
}

void CSealHostEnvironmentDlg::OnBnClickedButtonStopTimer()
{
	unsigned char memoryBank[2] = {0x00,0x00};
	unsigned char data[2] = {0xFF,0xFF};
	unsigned char cmd = 0x03;
	unsigned char status = 0x01;
	unsigned char *resp;

	int retV;

	retV = rfRdr.read(memoryBank);
	resp = rfRdr.getRsp();
	retV = rfRdr.write(memoryBank,status,cmd,data);
	retV = rfRdr.read(memoryBank);
	resp = rfRdr.getRsp();

	status = 0x03;
	Sleep(_DELAY);
	retV = rfRdr.read(memoryBank);
	resp = rfRdr.getRsp();
	retV = rfRdr.write(memoryBank,status,cmd,data);
	retV = rfRdr.read(memoryBank);
	resp = rfRdr.getRsp();
	int i = 0;
	do{
		retV = rfRdr.read(memoryBank);
		status = rfRdr.getRsp()[4]*16 + rfRdr.getRsp()[5];
		Sleep(_DELAY);
		i++;
	}while((status == '3' || status == '4') && i < _TIMEOUT);
	resp = rfRdr.getRsp();
	if(i == _TIMEOUT) {
		m_statusText.Format(_T("Timeout!"));
		/*m_statusText.Format(_T("Error (Timeout) !!\r\n"));
		m_statusText.AppendFormat(_T("bit[7:0]: %c%c\r\n"),resp[2],resp[3]);
		m_statusText.AppendFormat(_T("bit[15:8]: %c%c\r\n"),resp[4],resp[5]);
		m_statusText.AppendFormat(_T("bit[23:16]: %c%c\r\n"),resp[6],resp[7]);
		m_statusText.AppendFormat(_T("bit[31:24]: %c%c\r\n"),resp[8],resp[9]);
		*/
	} else if(rfRdr.getRspLen() > 0  && status == '0'){
		if(!language.CompareNoCase(_T("English"))){
			m_statusText.Format(_T("Timer Stopped!\r\n"));
		} else {
			m_statusText.Format(_T("Cronômetro Parado!\r\n"));
		}
		//m_statusText.Format(_T("Command Successful!!\r\n"));
		//m_statusText.AppendFormat(_T("bit[7:0]: %c%c\r\n"),resp[2],resp[3]);
		//m_statusText.AppendFormat(_T("bit[15:8]: %c%c\r\n"),resp[4],resp[5]);
		//m_statusText.AppendFormat(_T("bit[23:16]: %c%c\r\n"),resp[6],resp[7]);
		//m_statusText.AppendFormat(_T("bit[31:24]: %c%c\r\n"),resp[8],resp[9]);
	} else if(rfRdr.getRspLen() > 0 || status != '0') {
		m_statusText.AppendFormat(_T("Error: %s\r\n"),getCmdStatus(getNumber(resp[4],resp[5])));
		/*
		m_statusText.Format(_T("Error (Status) !!\r\n"));
		m_statusText.AppendFormat(_T("bit[7:0]: %c%c\r\n"),resp[2],resp[3]);
		m_statusText.AppendFormat(_T("bit[15:8]: %c%c\r\n"),resp[4],resp[5]);
		m_statusText.AppendFormat(_T("bit[23:16]: %c%c\r\n"),resp[6],resp[7]);
		m_statusText.AppendFormat(_T("bit[31:24]: %c%c\r\n"),resp[8],resp[9]);
		*/
	} else {
		m_statusText.Format(_T("Error move the seal!\r\n"));
	}
	statusText.SetWindowTextW(m_statusText);
}

void CSealHostEnvironmentDlg::OnBnClickedButtonRead5()
{
	m_statusText.Format(_T("Reading Runtime...\r\n\r\n"));
	edHistory.GetWindowTextW(m_edHistory);

	int qtd2 = 0;

	unsigned char membk1, membk2;
	membk1 = 0x00;
	membk2 = 0x00;

	unsigned char memoryBank[2] = {membk1,membk2};
	unsigned char *resp;
	rfRdr.read(memoryBank);
	resp = rfRdr.getRsp();
	m_statusText.AppendFormat(_T("Command: %s\r\n"),getCmd(getNumber(resp[2],resp[3])));
	m_statusText.AppendFormat(_T("Cmd Status: %s\r\n"),getCmdStatus(getNumber(resp[4],resp[5])));
	m_statusText.AppendFormat(_T("reserved: 0x%c%c\r\n"),resp[6],resp[7]);
	m_statusText.AppendFormat(_T("reserved: 0x%c%c\r\n\r\n"),resp[8],resp[9]);
	if(memoryBank[1] == 0xFF){
		memoryBank[0]++;
		memoryBank[1] = 0x00;
	} else {
		memoryBank[1]++;
	}
	rfRdr.read(memoryBank);
	resp = rfRdr.getRsp();
	m_statusText.AppendFormat(_T("Hist Add[0]: %d\r\n"),getNumber(resp[2],resp[3])/4);
	m_statusText.AppendFormat(_T("Hist Add[1]: %d\r\n"),getNumber(resp[4],resp[5])/4);
	m_statusText.AppendFormat(_T("Hist Num[0]: %d\r\n"),getNumber(resp[6],resp[7]));
	m_statusText.AppendFormat(_T("Hist Num[1]: %d\r\n\r\n"),getNumber(resp[8],resp[9]));
	if(memoryBank[1] == 0xFF){
		memoryBank[0]++;
		memoryBank[1] = 0x00;
	} else {
		memoryBank[1]++;
	}
	rfRdr.read(memoryBank);
	resp = rfRdr.getRsp();
	m_statusText.AppendFormat(_T("rtcYear: %.2d\r\n"),getNumber(resp[2],resp[3]));
	m_statusText.AppendFormat(_T("rtcDay: %.2d\r\n"),getNumber(resp[4],resp[5]));
	m_statusText.AppendFormat(_T("rtcMonth: %.2d\r\n"),getNumber(resp[6],resp[7]));
	m_statusText.AppendFormat(_T("rtcDayName: %s\r\n\r\n"),getWDay(getNumber(resp[8],resp[9])));
	if(memoryBank[1] == 0xFF){
		memoryBank[0]++;
		memoryBank[1] = 0x00;
	} else {
		memoryBank[1]++;
	}
	rfRdr.read(memoryBank);
	resp = rfRdr.getRsp();
	m_statusText.AppendFormat(_T("rtcSec: %.2d\r\n"),getNumber(resp[2],resp[3]));
	m_statusText.AppendFormat(_T("rtcMin: %.2d\r\n"),getNumber(resp[4],resp[5]));
	m_statusText.AppendFormat(_T("rtcHour: %.2d\r\n"),getNumber(resp[6],resp[7]));
	m_statusText.AppendFormat(_T("reserved: %.2d\r\n\r\n"),getNumber(resp[8],resp[9]));
	if(memoryBank[1] == 0xFF){
		memoryBank[0]++;
		memoryBank[1] = 0x00;
	} else {
		memoryBank[1]++;
	}
	rfRdr.read(memoryBank);
	resp = rfRdr.getRsp();
	m_statusText.AppendFormat(_T("empID[0]: 0x%c%c\r\n"),resp[2],resp[3]);
	m_statusText.AppendFormat(_T("empID[1]: 0x%c%c\r\n"),resp[4],resp[5]);
	m_statusText.AppendFormat(_T("reserved: 0x%c%c\r\n"),resp[6],resp[7]);
    unsigned char tag_ver = getNumber(resp[8],resp[9]);
	m_statusText.AppendFormat(_T("Tag Version: 0x%02X v%X.%X\r\n\r\n"), tag_ver, (tag_ver >> 4) & 0xF, tag_ver & 0xF);
	statusText.SetWindowTextW(m_statusText);
	statusText.LineScroll(statusText.GetLineCount());
	if(memoryBank[1] == 0xFF){
		memoryBank[0]++;
		memoryBank[1] = 0x00;
	} else {
		memoryBank[1]++;
	}
	rfRdr.read(memoryBank);
	resp = rfRdr.getRsp();
	m_statusText.AppendFormat(_T("reserved: 0x%c%c\r\n"),resp[2],resp[3]);
	m_statusText.AppendFormat(_T("reserved: 0x%c%c\r\n"),resp[4],resp[5]);
	m_statusText.AppendFormat(_T("Tamper status: 0x%c%c\r\n"),resp[6],resp[7]);

    unsigned char batt = getNumber(resp[8],resp[9]);
    if (tag_ver >= 0x40) {
    	m_statusText.AppendFormat(_T("Battery status: 0x%02X %0.2f volts (%s)\r\n"), batt, (batt & 0xFE) * 0.02f, (batt & 1) ? _T("Low") : _T("Normal"));
    } else {
    	m_statusText.AppendFormat(_T("Battery status: 0x%02X (%s)\r\n"), batt, (batt & 1) ? _T("Low") : _T("Normal"));
    }
	statusText.SetWindowTextW(m_statusText);
	//statusText.LineScroll(statusText.GetLineCount());
}

void CSealHostEnvironmentDlg::OnBnClickedButtonRead1()
{
	statusText.SetWindowTextW(_T(""));
	unsigned char memoryBank[2] = {0x00,0x00};
	unsigned char data[2] = {0xFF,0xFF};
	unsigned char cmd = 0x01;
	unsigned char status = 0x01;
	unsigned char *resp;
	int tamper, retV;

	retV = rfRdr.write(memoryBank,status,cmd,data);
	
	Sleep(_DELAY);

	status = 0x03;
	retV = rfRdr.write(memoryBank,status,cmd,data);

	int i = 0;
	do {
		Sleep(_DELAY);
		retV = rfRdr.read(memoryBank);
		resp = rfRdr.getRsp();
		status = resp[4]*16 + resp[5];
		i++;
	} while((status == '3' || status == '4')  && i < _TIMEOUT);

	if(i == _TIMEOUT) {
		m_statusText.Format(_T("Timeout!"));
		//m_statusText.AppendFormat(_T("bit[7:0]: %c%c\r\n"),resp[2],resp[3]);
		//m_statusText.AppendFormat(_T("bit[15:8]: %c%c\r\n"),resp[4],resp[5]);
		//m_statusText.AppendFormat(_T("bit[23:16]: %c%c\r\n"),resp[6],resp[7]);
		//m_statusText.AppendFormat(_T("bit[31:24]: %c%c\r\n"),resp[8],resp[9]);
	} else if(rfRdr.getRspLen() > 0  && status == '0'){
		memoryBank[0] = 0x00;
		memoryBank[1] = 0x05;
		retV = rfRdr.read(memoryBank);
		resp = rfRdr.getRsp();
		Sleep(_DELAY);
		tamper = getNumber(resp[6],resp[7]);
		CString statusTamper = getTamperStatus(tamper);
		if(!language.CompareNoCase(_T("English"))){
			m_statusText.Format(_T("Read Status: %s\r\n"),statusTamper);
		} else {
			m_statusText.Format(_T("Leitura do Status: %s\r\n"),statusTamper);
		}
		//m_statusText.AppendFormat(_T("bit[7:0]: %c%c\r\n"),resp[2],resp[3]);
		//m_statusText.AppendFormat(_T("bit[15:8]: %c%c\r\n"),resp[4],resp[5]);
		//m_statusText.AppendFormat(_T("bit[23:16]: %c%c\r\n"),resp[6],resp[7]);
		//m_statusText.AppendFormat(_T("bit[31:24]: %c%c\r\n"),resp[8],resp[9]);
	} else if(rfRdr.getRspLen() > 0 && status != '0') {
		m_statusText.AppendFormat(_T("Error: %s\r\n"),getCmdStatus(getNumber(resp[4],resp[5])));
		//m_statusText.AppendFormat(_T("bit[7:0]: %c%c\r\n"),resp[2],resp[3]);
		//m_statusText.AppendFormat(_T("bit[15:8]: %c%c\r\n"),resp[4],resp[5]);
		//m_statusText.AppendFormat(_T("bit[23:16]: %c%c\r\n"),resp[6],resp[7]);
		//m_statusText.AppendFormat(_T("bit[31:24]: %c%c\r\n"),resp[8],resp[9]);
	} else {
		m_statusText.Format(_T("Erro: Move the Seal!\r\n"));
	}
	statusText.SetWindowTextW(m_statusText);
}

void CSealHostEnvironmentDlg::OnBnClickedButtonFlush()
{
	edReset.GetWindowTextW(m_edReset);
	
	unsigned char command[2];
	command[0] = (m_edReset[0] >= 'A' && m_edReset[0] <= 'Z') ? m_edReset[0] - 'A' + 0xA : m_edReset[0] - '0';
	command[1] = (m_edReset[1] >= 'A' && m_edReset[1] <= 'Z') ? m_edReset[1] - 'A' + 0xA : m_edReset[1] - '0';
	unsigned char singleData = (unsigned char)(command[0]*16 + command[1]);

	unsigned char memoryBank[2] = {0x00,0x01};	
	unsigned char data[2] = {singleData,singleData};	

	rfRdr.read(memoryBank);

	unsigned char *resp = rfRdr.getRsp();
	memoryBank[0] = 0x00;
	memoryBank[1] =	0x06;
//	for(int a = 0; a <= 3*(((int)(resp[5]*16 + resp[6]))/4) && a < 30; a++){
	for(int a = 0; a < 60; a++){
		rfRdr.write(memoryBank,singleData,singleData,data);
		Sleep(50);
		if(memoryBank[1] == 0xFF){
			memoryBank[0]++;
			memoryBank[1] = 0x00;
		} else {
			memoryBank[1]++;
		}
	}
	if(!language.CompareNoCase(_T("English"))){
		m_statusText.Format(_T("Factory Defaults Restored!\r\n"));
	} else {
		m_statusText.Format(_T("Padrões de fábrica restaurados!\r\n"));
	}
	statusText.SetWindowTextW(m_statusText);
}

void CSealHostEnvironmentDlg::OnBnClickedButtonReadHistory()
{
	if(!language.CompareNoCase(_T("English"))){
		m_statusText.Format(_T("Reading History...\r\n"));
	} else {
		m_statusText.Format(_T("Lendo Histórico...\r\n"));
	}
	edHistory.GetWindowTextW(m_edHistory);
	int qtd = _tcstod(m_edHistory,0);

	int qtd2 = qtd*3+6;

	unsigned char membk1, membk2;
	membk1 = qtd2 / 256;
	membk2 = qtd2 - membk1*256;

	unsigned char memoryBank[2] = {membk1,membk2};
	unsigned char *resp;
	rfRdr.read(memoryBank);
	resp = rfRdr.getRsp();
	int year = getNumber(resp[2],resp[3]);
	int day = getNumber(resp[4],resp[5]);
	int month = getNumber(resp[6],resp[7]);
	int nDay = getNumber(resp[8],resp[9]);
	if(memoryBank[1] == 0xFF){
		memoryBank[0]++;
		memoryBank[1] = 0x00;
	} else {
		memoryBank[1]++;
	}
	rfRdr.read(memoryBank);
	resp = rfRdr.getRsp();
	int sec = getNumber(resp[2],resp[3]);
	int min = getNumber(resp[4],resp[5]);
	int hr = getNumber(resp[6],resp[7]);
	int res = getNumber(resp[8],resp[9]);
	if(memoryBank[1] == 0xFF){
		memoryBank[0]++;
		memoryBank[1] = 0x00;
	} else {
		memoryBank[1]++;
	}
	rfRdr.read(memoryBank);
	resp = rfRdr.getRsp();
	int empid1 = getNumber(resp[2],resp[3]);
	int empid2 = getNumber(resp[4],resp[5]);
	int tamper = getNumber(resp[6],resp[7]);
	int cmdS = getNumber(resp[8],resp[9]);
	switch(nDay){
		case 1:
			if(!language.CompareNoCase(_T("English"))){
				m_statusText.AppendFormat(_T("Read Status executed on Sunday, "));
			} else {
				m_statusText.AppendFormat(_T("Leitura efetuada no Domingo, "));
			}
			break;
		case 2:
			if(!language.CompareNoCase(_T("English"))){
				m_statusText.AppendFormat(_T("Read Status executed on Monday, "));
			} else {
				m_statusText.AppendFormat(_T("Leitura efetuada na Segunda-Feira, "));
			}
			break;
		case 3:
			if(!language.CompareNoCase(_T("English"))){
				m_statusText.AppendFormat(_T("Read Status executed on Tuesday, "));
			} else {
				m_statusText.AppendFormat(_T("Leitura efetuada na Terça-Feira, "));
			}
			break;
		case 4:
			if(!language.CompareNoCase(_T("English"))){
				m_statusText.AppendFormat(_T("Read Status executed on Wednesday, "));
			} else {
				m_statusText.AppendFormat(_T("Leitura efetuada na Quarta-Feira, "));
			}
			break;
		case 5:
			if(!language.CompareNoCase(_T("English"))){
				m_statusText.AppendFormat(_T("Read Status executed on Thursday, "));
			} else {
				m_statusText.AppendFormat(_T("Leitura efetuada na Quinta-Feira, "));
			}
			break;
		case 6:
			if(!language.CompareNoCase(_T("English"))){
				m_statusText.AppendFormat(_T("Read Status executed on Friday, "));
			} else {
				m_statusText.AppendFormat(_T("Leitura efetuada na Sexta-Feira, "));
			}
			break;
		case 7:
			if(!language.CompareNoCase(_T("English"))){
				m_statusText.AppendFormat(_T("Read Status executed on Saturday, "));
			} else {
				m_statusText.AppendFormat(_T("Leitura efetuada no Sábado, "));
			}
			break;
	}
	m_statusText.AppendFormat(_T("%.2d/%.2d/20%.2d às %.2d:%.2d:%.2d.\r\n"),day,month,year,hr,min,sec);
	switch(tamper){
		case 0:
			if(!language.CompareNoCase(_T("English"))){
				m_statusText.AppendFormat(_T("NOT TAMPERED"));
			} else {
				m_statusText.AppendFormat(_T("Lacre Inviolado"));
			}
			break;
		case 1:
			if(!language.CompareNoCase(_T("English"))){
				m_statusText.AppendFormat(_T("Read Status executed on Saturday, "));
			} else {
				m_statusText.AppendFormat(_T("Lacre Violado, Cronômetro Parado"));
			}
			break;
		case 2:
			if(!language.CompareNoCase(_T("English"))){
				m_statusText.AppendFormat(_T("Read Status executed on Saturday, "));
			} else {
				m_statusText.AppendFormat(_T("Lacre Violado"));
			}
			break;
	}
	statusText.SetWindowTextW(m_statusText);
	statusText.LineScroll(statusText.GetLineCount());
}

void CSealHostEnvironmentDlg::OnBnClickedButtonHistory2()
{
	unsigned char memoryBank[2] = {0x00,0x00};
	unsigned char data[2] = {0xFF,0xFF};
	unsigned char cmd = 0x08;
	unsigned char status = 0x01;
	unsigned char *resp;

	int retV;

	retV = rfRdr.read(memoryBank);
	resp = rfRdr.getRsp();
	retV = rfRdr.write(memoryBank,status,cmd,data);
	retV = rfRdr.read(memoryBank);
	resp = rfRdr.getRsp();

	status = 0x03;
	Sleep(_DELAY);
	retV = rfRdr.read(memoryBank);
	resp = rfRdr.getRsp();
	retV = rfRdr.write(memoryBank,status,cmd,data);
	retV = rfRdr.read(memoryBank);
	resp = rfRdr.getRsp();
	int i = 0;
	do{
		retV = rfRdr.read(memoryBank);
		resp = rfRdr.getRsp();
		status = resp[4]*16 + resp[5];
		{
			wchar_t buf[256] = { 0, }; 
			wsprintf(buf, L"Status %X, Resp[4] %X Resp[5] %X\r\n", status, resp[4], resp[5]);
			OutputDebugString(buf);
		}
		Sleep(_DELAY);
		i++;
	}while(((status == '3') || (status == '4')) && (i < _TIMEOUT * 5));
	resp = rfRdr.getRsp();
	if(i == _TIMEOUT * 5) {
		m_statusText.Format(_T("Timeout!"));
		//m_statusText.AppendFormat(_T("bit[7:0]: %c%c\r\n"),resp[2],resp[3]);
		//m_statusText.AppendFormat(_T("bit[15:8]: %c%c\r\n"),resp[4],resp[5]);
		//m_statusText.AppendFormat(_T("bit[23:16]: %c%c\r\n"),resp[6],resp[7]);
		//m_statusText.AppendFormat(_T("bit[31:24]: %c%c\r\n"),resp[8],resp[9]);
	} else if(rfRdr.getRspLen() > 0  && status == '0'){
		//m_statusText.Format(_T("Command Successful!!\r\n"));
		if(!language.CompareNoCase(_T("English"))){
			m_statusText.Format(_T("Factory Defaults Restored!\r\n"));
		} else {
			m_statusText.Format(_T("Padrões de fábrica restaurados!\r\n"));
		}
		//OnBnClickedButtonFlush();
		//m_statusText.AppendFormat(_T("bit[7:0]: %c%c\r\n"),resp[2],resp[3]);
		//m_statusText.AppendFormat(_T("bit[15:8]: %c%c\r\n"),resp[4],resp[5]);
		//m_statusText.AppendFormat(_T("bit[23:16]: %c%c\r\n"),resp[6],resp[7]);
		//m_statusText.AppendFormat(_T("bit[31:24]: %c%c\r\n"),resp[8],resp[9]);
	} else if(rfRdr.getRspLen() > 0 || status != '0') {
		m_statusText.Format(_T("Erro: %s"), getCmdStatus(getNumber(resp[4],resp[5])));
		//m_statusText.AppendFormat(_T("bit[7:0]: %c%c\r\n"),resp[2],resp[3]);
		//m_statusText.AppendFormat(_T("bit[15:8]: %c%c\r\n"),resp[4],resp[5]);
		//m_statusText.AppendFormat(_T("bit[23:16]: %c%c\r\n"),resp[6],resp[7]);
		//m_statusText.AppendFormat(_T("bit[31:24]: %c%c\r\n"),resp[8],resp[9]);
	} else {
		m_statusText.Format(_T("Error, move Seal!\r\n"));
	}
	statusText.SetWindowTextW(m_statusText);

}

void CSealHostEnvironmentDlg::initApp(void){
	//int yesno = MessageBoxW(_T("Enable command buttons? (debug buttons will be disabled)"),_T("Command Buttons"),1);
	int yesno = 1965;
	if (yesno == 2){
		btnOpen.EnableWindow(TRUE);
		btnClose.EnableWindow(FALSE);
		btnStartTimer.EnableWindow(FALSE);
		btnStopTimer.EnableWindow(FALSE);
		btnRead5.EnableWindow(FALSE);
		btnRead1.EnableWindow(FALSE);
		btnFlush.EnableWindow(FALSE);
		edReset.EnableWindow(FALSE);
		btnHistory.EnableWindow(FALSE);
		edHistory.EnableWindow(FALSE);
		btnHistory2.EnableWindow(FALSE);
		btnAllHistory.EnableWindow(FALSE);
		btnRTCCalStart.EnableWindow(FALSE);
		btnRTCCalRead.EnableWindow(FALSE);
		btnReadSerial.EnableWindow(FALSE);
        btnReadBattery.EnableWindow(FALSE);
        btnReadUID.EnableWindow(FALSE);
	} else {
		int retV = rfRdr.connect();
		if(retV == 0 || debug) {
			btnStartTimer.EnableWindow(TRUE);
			btnStopTimer.EnableWindow(TRUE);
			btnRead5.EnableWindow(TRUE);
			btnRead1.EnableWindow(TRUE);
			btnFlush.EnableWindow(TRUE);
			edReset.EnableWindow(TRUE);
			btnHistory.EnableWindow(TRUE);
			edHistory.EnableWindow(TRUE);
			btnHistory2.EnableWindow(TRUE);
			btnAllHistory.EnableWindow(TRUE);
			btnClose.EnableWindow(TRUE);
			btnOpen.EnableWindow(FALSE);
			btnRTCCalStart.EnableWindow(TRUE);
			btnRTCCalRead.EnableWindow(TRUE);
			btnReadSerial.EnableWindow(TRUE);
            btnReadBattery.EnableWindow(TRUE);
            btnReadUID.EnableWindow(TRUE);
		} else {
			btnOpen.EnableWindow(TRUE);
			btnClose.EnableWindow(FALSE);
			btnStartTimer.EnableWindow(FALSE);
			btnStopTimer.EnableWindow(FALSE);
			btnRead5.EnableWindow(FALSE);
			btnRead1.EnableWindow(FALSE);
			btnFlush.EnableWindow(FALSE);
			edReset.EnableWindow(FALSE);
			btnHistory.EnableWindow(FALSE);
			edHistory.EnableWindow(FALSE);
			btnHistory2.EnableWindow(FALSE);
			btnAllHistory.EnableWindow(FALSE);
			statusText.GetWindowTextW(m_statusText);
			btnRTCCalStart.EnableWindow(FALSE);
			btnRTCCalRead.EnableWindow(FALSE);
			btnReadSerial.EnableWindow(FALSE);
            btnReadBattery.EnableWindow(FALSE);
            btnReadUID.EnableWindow(FALSE);
			if(!language.CompareNoCase(_T("English"))){
				m_statusText.AppendFormat(_T("Error: Unable to connect to reader: %d. Check cables\r\n"),retV);
			} else {
				m_statusText.AppendFormat(_T("Erro Connectando ao leitor: %d. Cheque as conexões\r\n"),retV);
			}
			statusText.SetWindowTextW(m_statusText);
		}
	}

	if(debug){
		logoGTP.ShowWindow(FALSE);
		logoHunter.ShowWindow(FALSE);
		btnOpen.ShowWindow(TRUE);
		btnRead.ShowWindow(TRUE);
		btnWrite.ShowWindow(TRUE);
		comboBox1.ShowWindow(TRUE);
		comboBox2.ShowWindow(TRUE);
		comboBox3.ShowWindow(TRUE);
		comboBox4.ShowWindow(TRUE);
		comboBox5.ShowWindow(TRUE);
		comboBox6.ShowWindow(TRUE);
		comboBox7.ShowWindow(TRUE);
		comboBox8.ShowWindow(TRUE);
		comboBox9.ShowWindow(TRUE);
		comboBox10.ShowWindow(TRUE);
		comboBox11.ShowWindow(TRUE);
		comboBox12.ShowWindow(TRUE);
		btnStartTimer.ShowWindow(TRUE);
		btnStopTimer.ShowWindow(TRUE);
		btnRead5.ShowWindow(TRUE);
		btnRead1.ShowWindow(TRUE);
		btnFlush.ShowWindow(TRUE);
		edReset.ShowWindow(TRUE);
		btnHistory.ShowWindow(TRUE);
		edHistory.ShowWindow(TRUE);
		btnHistory2.ShowWindow(TRUE);
		btnAllHistory.ShowWindow(TRUE);
		btnClose.ShowWindow(TRUE);
		btnOpen.ShowWindow(FALSE);
	}
}

void CSealHostEnvironmentDlg::OnBnClickedButtonReadAllHistory()
{
	if(!language.CompareNoCase(_T("English"))){
		m_statusText.Format(_T("Start History Reading\r\n"));
	} else {
		m_statusText.Format(_T("Iniciando Leitura do Histórico\r\n"));
	}
	unsigned char membk1, membk2;
	membk1 = 0x00;
	membk2 = 0x01;

	unsigned char memoryBank[2] = {membk1,membk2};
	unsigned char *resp;
	rfRdr.read(memoryBank);
	resp = rfRdr.getRsp();
	int qtd = rfRdr.getRspLen() > 0 ? getNumber(resp[6],resp[7]) : 0;

	if(qtd == 0){}

	memoryBank[0] = 0x00;
	memoryBank[1] = 0x04;

	rfRdr.read(memoryBank);
	resp = rfRdr.getRsp();
	if(!language.CompareNoCase(_T("English"))){
		m_statusText.AppendFormat(_T("Firmware Version: 0x%c%c\r\n\r\n"),resp[8],resp[9]);
	} else {
		m_statusText.AppendFormat(_T("Versão do Firmware: 0x%c%c\r\n\r\n"),resp[8],resp[9]);
	}

	for(int i = 0; i < qtd; i++){
		int qtd2 = i*3+6;

		unsigned char membk1, membk2;
		membk1 = qtd2 / 256;
		membk2 = qtd2 - membk1*256;

		unsigned char memoryBank[2] = {membk1,membk2};
		unsigned char *resp;
		rfRdr.read(memoryBank);
		resp = rfRdr.getRsp();
		int year = getNumber(resp[2],resp[3]);
		int day = getNumber(resp[4],resp[5]);
		int month = getNumber(resp[6],resp[7]);
		int nDay = getNumber(resp[8],resp[9]);
		if(memoryBank[1] == 0xFF){
			memoryBank[0]++;
			memoryBank[1] = 0x00;
		} else {
			memoryBank[1]++;
		}
		rfRdr.read(memoryBank);
		resp = rfRdr.getRsp();
		int sec = getNumber(resp[2],resp[3]);
		int min = getNumber(resp[4],resp[5]);
		int hr = getNumber(resp[6],resp[7]);
		int res = getNumber(resp[8],resp[9]);
		if(memoryBank[1] == 0xFF){
			memoryBank[0]++;
			memoryBank[1] = 0x00;
		} else {
			memoryBank[1]++;
		}
		rfRdr.read(memoryBank);
		resp = rfRdr.getRsp();
		int empid1 = getNumber(resp[2],resp[3]);
		int empid2 = getNumber(resp[4],resp[5]);
		int tamper = getNumber(resp[6],resp[7]);
		int cmdS = getNumber(resp[8],resp[9]);

		CString wDay = getWDay(nDay);
		CString cmd = getCmd(res);
		CString tamperStatus = getTamperStatus(tamper);
	
		if(!language.CompareNoCase(_T("English"))){
			m_statusText.AppendFormat(_T("%s executed %s, "),cmd,wDay);
			m_statusText.AppendFormat(_T("%.2d/%.2d/20%.2d at %.2d:%.2d:%.2d.\r\n"),day,month,year,hr,min,sec);
		} else {
			m_statusText.AppendFormat(_T("%s executada %s, "),cmd,wDay);
			m_statusText.AppendFormat(_T("%.2d/%.2d/20%.2d às %.2d:%.2d:%.2d.\r\n"),day,month,year,hr,min,sec);
		}
		m_statusText.AppendFormat(_T("%s\r\n"),tamperStatus);
		statusText.SetWindowTextW(m_statusText);
		statusText.LineScroll(statusText.GetLineCount());
	}
	if(qtd == 0){
		m_statusText.Format(_T("History Empty\r\n"));
	}
	statusText.SetWindowTextW(m_statusText);
}

int CSealHostEnvironmentDlg::getNumber(unsigned char l, unsigned char r)
{
	int ret = ((l >= 'A' && l <= 'Z') ? l - 'A' + 0xA : l - '0') * 16 + ((r >= 'A' && r <= 'Z') ? r - 'A' + 0xA : r - '0');
	return ret;
}

CString CSealHostEnvironmentDlg::getWDay(int iwDay)
{
	CString wDay;

	switch(iwDay){
		case 1:
			if(!language.CompareNoCase(_T("English"))){
				wDay.Format(_T("Sunday"));
			}else{
				wDay.Format(_T("Domingo"));
			}
			break;
		case 2:
			if(!language.CompareNoCase(_T("English"))){
				wDay.Format(_T("Monday"));
			}else{
				wDay.Format(_T("Segunda-Feira"));
			}
			break;
		case 3:
			if(!language.CompareNoCase(_T("English"))){
				wDay.Format(_T("Tuesday"));
			}else{
				wDay.Format(_T("Terça-Feira"));
			}
			break;
		case 4:
			if(!language.CompareNoCase(_T("English"))){
				wDay.Format(_T("Wednesday"));
			}else{
				wDay.Format(_T("Quarta-Feira"));
			}
			break;
		case 5:
			if(!language.CompareNoCase(_T("English"))){
				wDay.Format(_T("Thursday"));
			}else{
				wDay.Format(_T("Quinta-Feira"));
			}
			break;
		case 6:
			if(!language.CompareNoCase(_T("English"))){
				wDay.Format(_T("Friday"));
			}else{
				wDay.Format(_T("Sexta-Feira"));
			}
			break;
		case 7:
			if(!language.CompareNoCase(_T("English"))){
				wDay.Format(_T("Saturday"));
			}else{
				wDay.Format(_T("Sábado"));
			}
			break;
	}

	return wDay;
}

CString CSealHostEnvironmentDlg::getCmd(int iCmd)
{
	CString cmd;

	switch(iCmd){
		case 1:
			if(!language.CompareNoCase(_T("English"))){
				cmd.Format(_T("Read Status"));
			} else {
				cmd.Format(_T("Leitura do Status"));
			}
			break;
		case 2:
			if(!language.CompareNoCase(_T("English"))){
				cmd.Format(_T("Start Timer"));
			} else {
				cmd.Format(_T("Iniciar Cronômetro"));
			}
			break;
		case 3:
			if(!language.CompareNoCase(_T("English"))){
				cmd.Format(_T("Stop Timer"));
			} else {
				cmd.Format(_T("Interromper Cronômetro"));
			}
			break;
		case 4:
			if(!language.CompareNoCase(_T("English"))){
				cmd.Format(_T("Read History"));
			} else {
				cmd.Format(_T("Leitura do Histórico"));
			}
			break;
		case 5:
			if(!language.CompareNoCase(_T("English"))){
				cmd.Format(_T("Flush History"));
			} else {
				cmd.Format(_T("Limpar Histórico"));
			}
			break;
		case 6:
			if(!language.CompareNoCase(_T("English"))){
				cmd.Format(_T("Read Battery Level"));
			} else {
				cmd.Format(_T("Verificar Nível da Bateria"));
			}
			break;
        case 112:
			if(!language.CompareNoCase(_T("English"))){
				cmd.Format(_T("Read Serial Number"));
			} else {
				cmd.Format(_T("Leitura do Número de Série"));
			}
			break;
        case 113:
			if(!language.CompareNoCase(_T("English"))){
				cmd.Format(_T("Read RF UID"));
			} else {
				cmd.Format(_T("Leitura do RF UID"));
			}
			break;
		case 170:
			if(!language.CompareNoCase(_T("English"))){
				cmd.Format(_T("Tamper Band Open"));
			} else {
				cmd.Format(_T("Abertura do Lacre"));
			}
			break;
		case 184:
			if(!language.CompareNoCase(_T("English"))){
				cmd.Format(_T("Battery Low"));
			} else {
				cmd.Format(_T("Bateria Fraca"));
			}
			break;
	}

	return cmd;
}

CString CSealHostEnvironmentDlg::getCmdStatus(int iCmd)
{
	CString cmd;

	switch(iCmd){
		case 0:
			if(!language.CompareNoCase(_T("English"))){
				cmd.Format(_T("Completed"));
			} else {
				cmd.Format(_T("Completo"));
			}
			break;
		case 1:
			if(!language.CompareNoCase(_T("English"))){
				cmd.Format(_T("Busy"));
			} else {
				cmd.Format(_T("Ocupado"));
			}
			break;
		case 2:
			if(!language.CompareNoCase(_T("English"))){
				cmd.Format(_T("Received"));
			} else {
				cmd.Format(_T("Recebido"));
			}
			break;
		case 3:
			if(!language.CompareNoCase(_T("English"))){
				cmd.Format(_T("Sent"));
			} else {
				cmd.Format(_T("Enviado"));
			}
			break;
		case 4:
			if(!language.CompareNoCase(_T("English"))){
				cmd.Format(_T("Runnning"));
			} else {
				cmd.Format(_T("Executando"));
			}
			break;
		case 245:
			if(!language.CompareNoCase(_T("English"))){
				cmd.Format(_T("I2C Master Receiver Mode Selected Event Failure"));
			} else {
				cmd.Format(_T("Falha no evento I2C: Selecionar modo recebedor mestre"));
			}
			break;
		case 246:
			if(!language.CompareNoCase(_T("English"))){
				cmd.Format(_T("I2C Master Byte Transmitted Event Failure"));
			} else {
				cmd.Format(_T("Falha no evento I2C: Byte mestre transmitido"));
			}
			break;
		case 247:
			if(!language.CompareNoCase(_T("English"))){
				cmd.Format(_T("I2C Master Byte Transmitting Event Failure"));
			} else {
				cmd.Format(_T("Falha no evento I2C: Transmitindo Byte mestre"));
			}
			break;
		case 248:
			if(!language.CompareNoCase(_T("English"))){
				cmd.Format(_T("I2C Master Transmitter Mode Event Failure"));
			} else {
				cmd.Format(_T("Falha no evento I2C: Modo transmissor"));
			}
			break;
		case 249:
			if(!language.CompareNoCase(_T("English"))){
				cmd.Format(_T("I2C Master Mode Select Event Failure"));
			} else {
				cmd.Format(_T("Falha no evento I2C: Selecionar modo mestre"));
			}
			break;
		case 250:
			if(!language.CompareNoCase(_T("English"))){
				cmd.Format(_T("I2C Bus Busy"));
			} else {
				cmd.Format(_T("Barramento I2C ocupado"));
			}
			break;
		case 251:
			if(!language.CompareNoCase(_T("English"))){
				cmd.Format(_T("Invalid Command Status"));
			} else {
				cmd.Format(_T("Status do Comando Inválido"));
			}
			break;
		case 252:
			if(!language.CompareNoCase(_T("English"))){
				cmd.Format(_T("Invalid Command"));
			} else {
				cmd.Format(_T("Comando Inválido"));
			}
			break;
		case 253:
			if(!language.CompareNoCase(_T("English"))){
				cmd.Format(_T("RTC Configuration is required"));
			} else {
				cmd.Format(_T("Inícar cronômetro requerido"));
			}
			break;
		case 254:
			if(!language.CompareNoCase(_T("English"))){
				cmd.Format(_T("Tamper Band is open while configuring Calendar RTC"));
			} else {
				cmd.Format(_T("Lacre aberto ao executar início do cronômetro"));
			}
			break;
		case 255:
			if(!language.CompareNoCase(_T("English"))){
				cmd.Format(_T("Flush History error if Read History not done first"));
			} else {
				cmd.Format(_T("Não é possível executar a reconfiguração antes da leitura do histórico"));
			}
			break;
		case 256:
			if(!language.CompareNoCase(_T("English"))){
				cmd.Format(_T("Unknown"));
			} else {
				cmd.Format(_T("Desconhecido"));
			}
			break;
		default:
			cmd.Format(_T("%d!"),iCmd);
			break;
	}

	return cmd;
}

CString CSealHostEnvironmentDlg::getTamperStatus(int iTamper)
{
	CString tamperStatus;

	switch(iTamper){
		case 0:
			if(!language.CompareNoCase(_T("English"))){
				tamperStatus.Format(_T("Tamper Band Closed."));
			} else {
				tamperStatus.Format(_T("Lacre Inviolado."));
			}
			break;
		case 1:
			if(!language.CompareNoCase(_T("English"))){
				tamperStatus.Format(_T("Tamper Band Open."));
			} else {
				tamperStatus.Format(_T("Lacre Violado."));
			}
			break;
		case 2:
			if(!language.CompareNoCase(_T("English"))){
				tamperStatus.Format(_T("Tamper Band Open."));
			} else {
				tamperStatus.Format(_T("Lacre Violado."));
			}
			break;
		case 4:
			if(!language.CompareNoCase(_T("English"))){
				tamperStatus.Format(_T("Tamper Band Open (ByPass)."));
			} else {
				tamperStatus.Format(_T("Lacre Violado (ByPass)."));
			}
			break;
	}

	return tamperStatus;
}
void CSealHostEnvironmentDlg::OnStnDblclickRuntime()
{
	OnBnClickedButtonRead5();
}

void CSealHostEnvironmentDlg::OnStnClickedRuntime()
{
	OnBnClickedButtonRead5();
}

SYSTEMTIME RTCStartTime = { 0, };


void CSealHostEnvironmentDlg::OnBnClickedButtonRtcCalibrateStart()
{
#if 0
	struct tm* localtime_s (const time_t *pt);
	time_t curr;
	tm local;
	time(&curr); // get current time_t value
	local=*(localtime(&curr)); // dereference and assign

	unsigned char dsemana,hora,minuto,segundo,dia,mes,ano;
	dsemana = local.tm_wday + 1;
	hora = local.tm_hour;
	minuto = local.tm_min;
	segundo = local.tm_sec;

	dia = local.tm_mday;
	mes = local.tm_mon + 1;
	ano = local.tm_year - 100;
#else
	GetLocalTime(&RTCStartTime);

	unsigned char dsemana,hora,minuto,segundo,dia,mes,ano;
	dsemana = (RTCStartTime.wDayOfWeek == 1) ? 7 : RTCStartTime.wDayOfWeek - 1;
	hora = (unsigned char)RTCStartTime.wHour;
	minuto = (unsigned char)RTCStartTime.wMinute;
	segundo = (unsigned char)RTCStartTime.wSecond;

	dia = (unsigned char)RTCStartTime.wDay;
	mes = (unsigned char)RTCStartTime.wMonth;
	ano = RTCStartTime.wYear - 2000;
#endif

	unsigned char memoryBank[2] = {0x00,0x00};
	unsigned char data[2] = {0xEE,0xEE};
	unsigned char cmd = 0xCC;
	unsigned char status = 0x01;
	unsigned char *resp;
	int retV;

	retV = rfRdr.write(memoryBank,status,cmd,data);
	Sleep(_DELAY);

	// SET RTC

	memoryBank[0] = 0x00;
	memoryBank[1] = 0x02;
	unsigned char rtc[2] = {mes,dsemana};

	retV = rfRdr.write(memoryBank,dia,ano,rtc);
	Sleep(_DELAY);

	memoryBank[0] = 0x00;
	memoryBank[1] = 0x03;
	rtc[0] = hora;
	rtc[1] = 0xFF;
	retV = rfRdr.write(memoryBank,minuto,segundo,rtc);
	Sleep(_DELAY);

	// done
	boolean erro = false;
	int i = 0;
	memoryBank[0] = 0x00;
	memoryBank[1] = 0x00;
	do {
		Sleep(_DELAY);
		retV = rfRdr.read(memoryBank);
		resp = rfRdr.getRsp();
		status = rfRdr.getRsp()[4]*16 + rfRdr.getRsp()[5];
		i++;
	} while(status != '2'  && i < 1);
	if(status != '2'){
		if(!language.CompareNoCase(_T("English"))){
			m_statusText.Format(_T("Error: No answer from the Micro-Controller"));
		} else {
			m_statusText.Format(_T("Erro: Sem resposta do Micro-Controlador"));
		}
		erro = true;
	}

	if(!erro){
		memoryBank[0] = 0x00;
		memoryBank[1] = 0x00;
		status = 0x03;
		retV = rfRdr.write(memoryBank,status,cmd,data);

		i = 0;
		do{
			Sleep(_DELAY * 2);
			retV = rfRdr.read(memoryBank);
			resp = rfRdr.getRsp();
			status = rfRdr.getRsp()[4]*16 + rfRdr.getRsp()[5];
			i++;
		}while((status == '3' || status == '4')  && i < _TIMEOUT);
		if(i == _TIMEOUT) {
			m_statusText.Format(_T("Timeout!"));
			//m_statusText.Format(_T("Error (Timeout) !!\r\n"));
			//m_statusText.AppendFormat(_T("bit[7:0]: %c%c\r\n"),resp[2],resp[3]);
			//m_statusText.AppendFormat(_T("bit[15:8]: %c%c\r\n"),resp[4],resp[5]);
			//m_statusText.AppendFormat(_T("bit[23:16]: %c%c\r\n"),resp[6],resp[7]);
			//m_statusText.AppendFormat(_T("bit[31:24]: %c%c\r\n"),resp[8],resp[9]);
		} else if(rfRdr.getRspLen() > 0 && status == '0') {
			if(!language.CompareNoCase(_T("English"))) {
				m_statusText.Format(_T("RTC calibration started: "));
			} else {
				m_statusText.Format(_T("Iniciando calibração do RTC: "));
			}
			m_statusText.AppendFormat(_T("%.2d/%.2d/20%.2d %.2d:%.2d:%.2d.\r\n"),dia,mes,ano,hora,minuto,segundo);
			//m_statusText.Format(_T("Command Successful!!\r\n"));
			//m_statusText.AppendFormat(_T("bit[7:0]: %c%c\r\n"),resp[2],resp[3]);
			//m_statusText.AppendFormat(_T("bit[15:8]: %c%c\r\n"),resp[4],resp[5]);
			//m_statusText.AppendFormat(_T("bit[23:16]: %c%c\r\n"),resp[6],resp[7]);
			//m_statusText.AppendFormat(_T("bit[31:24]: %c%c\r\n"),resp[8],resp[9]);
		} else if(rfRdr.getRspLen() > 0 || status != '0') {
			m_statusText.Format(_T("Error: %s"),getCmdStatus(getNumber(resp[4],resp[5])));
			//m_statusText.Format(_T("Error (Status) !!\r\n"));
			//m_statusText.AppendFormat(_T("bit[7:0]: %c%c\r\n"),resp[2],resp[3]);
			//m_statusText.AppendFormat(_T("bit[15:8]: %c%c\r\n"),resp[4],resp[5]);
			//m_statusText.AppendFormat(_T("bit[23:16]: %c%c\r\n"),resp[6],resp[7]);
			//m_statusText.AppendFormat(_T("bit[31:24]: %c%c\r\n"),resp[8],resp[9]);
		} else {
			m_statusText.Format(_T("Error, move the seal!\r\n"));
		}	
	}
	statusText.SetWindowTextW(m_statusText);
}

__int64 TimeDelta(const SYSTEMTIME st1, const SYSTEMTIME st2)
{
    FILETIME        fileTime1;
    FILETIME        fileTime2;
    ULARGE_INTEGER  ul1;
    ULARGE_INTEGER  ul2;


    SystemTimeToFileTime(&st1, &fileTime1);
    SystemTimeToFileTime(&st2, &fileTime2);

	RtlCopyMemory(&ul1, &fileTime1, sizeof(FILETIME));
	RtlCopyMemory(&ul2, &fileTime2, sizeof(FILETIME));

	if (ul2.QuadPart > ul1.QuadPart)
		return            (ul2.QuadPart - ul1.QuadPart) / 10000;
	else
		return -((__int64)(ul1.QuadPart - ul2.QuadPart) / 10000);
}

void CSealHostEnvironmentDlg::OnBnClickedButtonRtcCalibrateRead()
{
	unsigned char memoryBank[2] = {0x00,0x00};
	unsigned char data[2] = {0xFF,0xFF};
	unsigned char cmd = 0xCD;
	unsigned char status = 0x01;
	unsigned char *resp;

	int retV;

	SYSTEMTIME  time;
	GetLocalTime(&time);

	retV = rfRdr.read(memoryBank);
	resp = rfRdr.getRsp();
	retV = rfRdr.write(memoryBank,status,cmd,data);
	retV = rfRdr.read(memoryBank);
	resp = rfRdr.getRsp();

	status = 0x03;
	Sleep(_DELAY);
	retV = rfRdr.read(memoryBank);
	resp = rfRdr.getRsp();
	retV = rfRdr.write(memoryBank,status,cmd,data);
	retV = rfRdr.read(memoryBank);
	resp = rfRdr.getRsp();
	int i = 0;
	do{
		retV = rfRdr.read(memoryBank);
		status = rfRdr.getRsp()[4]*16 + rfRdr.getRsp()[5];
		Sleep(_DELAY);
		i++;
	}while((status == '3' || status == '4') && i < _TIMEOUT);
	resp = rfRdr.getRsp();
	if(i == _TIMEOUT) {
		m_statusText.Format(_T("Timeout!"));
		/*m_statusText.Format(_T("Error (Timeout) !!\r\n"));
		m_statusText.AppendFormat(_T("bit[7:0]: %c%c\r\n"),resp[2],resp[3]);
		m_statusText.AppendFormat(_T("bit[15:8]: %c%c\r\n"),resp[4],resp[5]);
		m_statusText.AppendFormat(_T("bit[23:16]: %c%c\r\n"),resp[6],resp[7]);
		m_statusText.AppendFormat(_T("bit[31:24]: %c%c\r\n"),resp[8],resp[9]);
		*/
		statusText.SetWindowTextW(m_statusText);
		return;
	} else if(rfRdr.getRspLen() > 0  && status == '0'){

	} else if(rfRdr.getRspLen() > 0 || status != '0') {
		m_statusText.AppendFormat(_T("Error: %s\r\n"),getCmdStatus(getNumber(resp[4],resp[5])));
		/*
		m_statusText.Format(_T("Error (Status) !!\r\n"));
		m_statusText.AppendFormat(_T("bit[7:0]: %c%c\r\n"),resp[2],resp[3]);
		m_statusText.AppendFormat(_T("bit[15:8]: %c%c\r\n"),resp[4],resp[5]);
		m_statusText.AppendFormat(_T("bit[23:16]: %c%c\r\n"),resp[6],resp[7]);
		m_statusText.AppendFormat(_T("bit[31:24]: %c%c\r\n"),resp[8],resp[9]);
		*/
		statusText.SetWindowTextW(m_statusText);
		return;
	} else {
		m_statusText.Format(_T("Error move the seal!\r\n"));
		statusText.SetWindowTextW(m_statusText);
		return;
	}


	/* Read First Sector */

	edHistory.GetWindowTextW(m_edHistory);
	int qtd = _tcstod(m_edHistory,0);
	int qtd2 = qtd*3+6;

	unsigned char membk1, membk2;
	membk1 = qtd2 / 256;
	membk2 = qtd2 - membk1*256;

	memoryBank[0] = membk1;
	memoryBank[1] = membk2;
	rfRdr.read(memoryBank);
	resp = rfRdr.getRsp();
	int year = getNumber(resp[2],resp[3]);
	int day = getNumber(resp[4],resp[5]);
	int month = getNumber(resp[6],resp[7]);
	int nDay = getNumber(resp[8],resp[9]);
	if(memoryBank[1] == 0xFF){
		memoryBank[0]++;
		memoryBank[1] = 0x00;
	} else {
		memoryBank[1]++;
	}
	rfRdr.read(memoryBank);
	resp = rfRdr.getRsp();
	int sec = getNumber(resp[2],resp[3]);
	int min = getNumber(resp[4],resp[5]);
	int hr = getNumber(resp[6],resp[7]);
	int res = getNumber(resp[8],resp[9]);
	if(memoryBank[1] == 0xFF){
		memoryBank[0]++;
		memoryBank[1] = 0x00;
	} else {
		memoryBank[1]++;
	}
	rfRdr.read(memoryBank);
	resp = rfRdr.getRsp();
	int empid1 = getNumber(resp[2],resp[3]);
	int empid2 = getNumber(resp[4],resp[5]);
	int tamper = getNumber(resp[6],resp[7]);
	int cmdS = getNumber(resp[8],resp[9]);
	if(!language.CompareNoCase(_T("English"))){
		m_statusText.AppendFormat(_T("RTC Read: "));
	} else {
		m_statusText.AppendFormat(_T("Leitura do RTC: "));
	}

	SYSTEMTIME  rtc;

	rtc.wDayOfWeek = (nDay == 7) ? 1 : nDay + 1;
	rtc.wDay = day;
	rtc.wMonth = month;
	rtc.wYear = year + 2000;
	rtc.wHour = hr;
	rtc.wMinute = min;
	rtc.wSecond = sec;
	rtc.wMilliseconds = 0;

	__int64 running = TimeDelta(RTCStartTime, time);
	__int64 delta   = TimeDelta(time, rtc);

	m_statusText.AppendFormat(_T("%.2d/%.2d/20%.2d  %.2d:%.2d:%.2d, Local Time %.2d/%.2d/%.2d  %.2d:%.2d:%.2d, delta %I64d ms running %I64d ms.\r\n"),day,month,year,hr,min,sec,time.wDay, time.wMonth, time.wYear, time.wHour, time.wMinute,time.wSecond, delta, running);

	statusText.SetWindowTextW(m_statusText);
	statusText.LineScroll(statusText.GetLineCount());
}

void CSealHostEnvironmentDlg::OnBnClickedButtonSerial_Old()
{
	unsigned char memoryBank[2] = {0x00,0x01};
	unsigned char data[2] = {0xFF,0xFF};
	unsigned char cmd;
	unsigned char status;
	unsigned char *resp;
    unsigned char serial[12] = { 0, };
	unsigned char nibble;
    int retry;
	int timeout;
	int retV;


	SYSTEMTIME  time_start,time_end;
	GetLocalTime(&time_start);


    // 96-bit/12-byte serial number (24 nibbles)
	for (unsigned char ix = 0; ix < 24; ix++)
	{
        cmd = 0x70 + ix;

		timeout = 0;
		do {
			retV = rfRdr.read(memoryBank);
			resp = rfRdr.getRsp();
            if (rfRdr.getRspLen() > 0)
                break;
			Sleep(100);
			timeout++;
		} while (timeout < (_TIMEOUT * _DELAY / 100));

		if (timeout == (_TIMEOUT * _DELAY / 100)) {
			m_statusText.Format(_T("Error move the seal!\r\n"));
			statusText.SetWindowTextW(m_statusText);
            return;
		}


        for (retry = 0; retry < 3; retry++) {
		    status = 0x01;
		    retV   = rfRdr.write(memoryBank,status,cmd,data);

            for (timeout = 0; timeout < 2; timeout++) {
			    retV   = rfRdr.read(memoryBank);
			    resp   = rfRdr.getRsp();
			    status = getNumber(resp[4],resp[5]);
			    if (status == 2)
				    break;

			    Sleep(100);
            }
        }

		if ((retry == 3) && (timeout == 2)) {
			m_statusText.AppendFormat(_T("Timeout!\r\n"));
			statusText.SetWindowTextW(m_statusText);
			return;
		} else if ((rfRdr.getRspLen() > 0) && (status == 2)) {

		} else if ((rfRdr.getRspLen() > 0) || (status != 0)) {
			m_statusText.AppendFormat(_T("Error: %s\r\n"),getCmdStatus(status));
			statusText.SetWindowTextW(m_statusText);
			return;
		} else {
			m_statusText.AppendFormat(_T("Error move the seal!\r\n"));
			statusText.SetWindowTextW(m_statusText);
			return;
		}



		status = 0x03;
		retV = rfRdr.write(memoryBank,status,cmd,data);


		timeout = 0;
		do {
			retV   = rfRdr.read(memoryBank);
			resp   = rfRdr.getRsp();
			status = getNumber(resp[4],resp[5]);
			if ((status != 1) && (status != 2) && (status != 3) && (status != 4))
				break;
			Sleep(100);
			timeout++;
		} while (timeout < (_TIMEOUT * _DELAY / 100));

		if (timeout == (_TIMEOUT * _DELAY / 100)) {
			m_statusText.AppendFormat(_T("Timeout!\r\n"));
			statusText.SetWindowTextW(m_statusText);
			return;
		} else if ((rfRdr.getRspLen() > 0) && ((status & 0xF0) == 0x40)) {

		} else if ((rfRdr.getRspLen() > 0) || (status != 0)) {
			m_statusText.AppendFormat(_T("Error: %s\r\n"),getCmdStatus(status));
			statusText.SetWindowTextW(m_statusText);
			return;
		} else {
			m_statusText.AppendFormat(_T("Error move the seal!\r\n"));
			statusText.SetWindowTextW(m_statusText);
			return;
		}



        int shift = (1 - (ix & 1)) * 4;
		nibble  = status & 0xF;
		serial[ix >> 1] |= nibble << shift;
	}


    GetLocalTime(&time_end);

	__int64 delta = TimeDelta(time_start, time_end);

	if(!language.CompareNoCase(_T("English"))){
		m_statusText.AppendFormat(_T("Serial Number: "));
	} else {
		m_statusText.AppendFormat(_T("Número de Série: "));
	}

    for (unsigned char ix = 0; ix < 12; ix++) {
		m_statusText.AppendFormat(_T("%02X"), serial[ix]);
    }

    m_statusText.AppendFormat(_T(" -> Wafer %d, X %d Y %d, Lot %c%c%c%c%c%c%c\r\n"),
        serial[4],
        serial[0] << 8 | serial[1],
        serial[2] << 8 | serial[3],
        serial[5],
        serial[6],
        serial[7],
        serial[8],
        serial[9],
        serial[10],
        serial[11]);

	if(!language.CompareNoCase(_T("English"))){
    	m_statusText.AppendFormat(_T("Time Elapsed: %I64d ms\r\n"), delta);
	} else {
	    m_statusText.AppendFormat(_T("Tempo Transcorrido: %I64d ms\r\n"), delta);
	}

	statusText.SetWindowTextW(m_statusText);
	statusText.LineScroll(statusText.GetLineCount());
}

void CSealHostEnvironmentDlg::OnBnClickedButtonSerial()
{
	unsigned char memoryBank[2];
	unsigned char histBank[2];
	unsigned char data[2] = {0xFF,0xFF};
	unsigned char cmd;
	unsigned char status;
	unsigned char *resp;
    unsigned char serial[12] = { 0, };
    int retry;
	int timeout;
	int retV;


	SYSTEMTIME  time_start,time_end;
	GetLocalTime(&time_start);


    cmd = 0x70;

    memoryBank[0] = 0x00;
    memoryBank[1] = 0x01;

	timeout = 0;
	do {
		retV = rfRdr.read(memoryBank);
		resp = rfRdr.getRsp();
        if (rfRdr.getRspLen() > 0)
            break;
		Sleep(100);
		timeout++;
	} while (timeout < (_TIMEOUT * _DELAY / 100));

	if (timeout == (_TIMEOUT * _DELAY / 100)) {
		m_statusText.Format(_T("Error move the seal!\r\n"));
		statusText.SetWindowTextW(m_statusText);
        return;
	}


    unsigned short histAddr = (getNumber(resp[4],resp[5]) << 8) |
                               getNumber(resp[2],resp[3]);
    histAddr >>= 2;



    memoryBank[0] = 0x00;
    memoryBank[1] = 0x00;

    for (retry = 0; retry < 3; retry++) {
		status = 0x01;
		retV   = rfRdr.write(memoryBank,status,cmd,data);

        if (retry > 0)
            Sleep(100);

        for (timeout = 0; timeout < 3; timeout++) {
			retV   = rfRdr.read(memoryBank);
			resp   = rfRdr.getRsp();
			status = getNumber(resp[4],resp[5]);
			if (status == 2)
				break;

			Sleep(100 + timeout * 100);
        }
    }

	if ((retry == 3) && (timeout == 3)) {
		m_statusText.AppendFormat(_T("Timeout!\r\n"));
		statusText.SetWindowTextW(m_statusText);
		return;
	} else if ((rfRdr.getRspLen() > 0) && (status == 2)) {

	} else if ((rfRdr.getRspLen() > 0) || (status != 0)) {
		m_statusText.AppendFormat(_T("Error: %s\r\n"),getCmdStatus(status));
		statusText.SetWindowTextW(m_statusText);
		return;
	} else {
		m_statusText.AppendFormat(_T("Error move the seal!\r\n"));
		statusText.SetWindowTextW(m_statusText);
		return;
	}



	status = 0x03;
	retV = rfRdr.write(memoryBank,status,cmd,data);


	timeout = 0;
	do {
		retV   = rfRdr.read(memoryBank);
		resp   = rfRdr.getRsp();
		status = getNumber(resp[4],resp[5]);
		if ((status != 1) && (status != 2) && (status != 3) && (status != 4))
			break;
		Sleep(100);
		timeout++;
	} while (timeout < (_TIMEOUT * _DELAY / 100));

	if (timeout == (_TIMEOUT * _DELAY / 100)) {
		m_statusText.AppendFormat(_T("Timeout!\r\n"));
		statusText.SetWindowTextW(m_statusText);
		return;
	} else if ((rfRdr.getRspLen() > 0) && (status == 0)) {

	} else if ((rfRdr.getRspLen() > 0) || (status != 0)) {
		m_statusText.AppendFormat(_T("Error: %s\r\n"),getCmdStatus(status));
		statusText.SetWindowTextW(m_statusText);
		return;
	} else {
		m_statusText.AppendFormat(_T("Error move the seal!\r\n"));
		statusText.SetWindowTextW(m_statusText);
		return;
	}



    for (unsigned short ix = 0; ix < 3; ix++) {
        unsigned short addr = histAddr + ix;

        histBank[0] = (addr >> 8) & 0xFF;
        histBank[1] =  addr       & 0xFF;

	    timeout = 0;
	    do {
		    retV = rfRdr.read(histBank);
		    resp = rfRdr.getRsp();
            if (rfRdr.getRspLen() > 0)
                break;
		    Sleep(100);
		    timeout++;
	    } while (timeout < (_TIMEOUT * _DELAY / 100));

	    if (timeout == (_TIMEOUT * _DELAY / 100)) {
		    m_statusText.Format(_T("Error move the seal!\r\n"));
		    statusText.SetWindowTextW(m_statusText);
            return;
	    }

        serial[(ix << 2) + 0] = getNumber(resp[2],resp[3]);
        serial[(ix << 2) + 1] = getNumber(resp[4],resp[5]);
        serial[(ix << 2) + 2] = getNumber(resp[6],resp[7]);
        serial[(ix << 2) + 3] = getNumber(resp[8],resp[9]);

	    retV = rfRdr.write(histBank,0xFF,0xFF,data);
    }

    GetLocalTime(&time_end);

	__int64 delta = TimeDelta(time_start, time_end);

	if(!language.CompareNoCase(_T("English"))){
		m_statusText.AppendFormat(_T("Serial Number: "));
	} else {
		m_statusText.AppendFormat(_T("Número de Série: "));
	}

    for (unsigned char ix = 0; ix < 12; ix++) {
		m_statusText.AppendFormat(_T("%02X"), serial[ix]);
    }

    m_statusText.AppendFormat(_T(" -> Wafer %d, X %d Y %d, Lot %c%c%c%c%c%c%c\r\n"),
        serial[4],
        serial[0] << 8 | serial[1],
        serial[2] << 8 | serial[3],
        serial[5],
        serial[6],
        serial[7],
        serial[8],
        serial[9],
        serial[10],
        serial[11]);

	if(!language.CompareNoCase(_T("English"))){
    	m_statusText.AppendFormat(_T("Time Elapsed: %I64d ms\r\n"), delta);
	} else {
	    m_statusText.AppendFormat(_T("Tempo Transcorrido: %I64d ms\r\n"), delta);
	}

	statusText.SetWindowTextW(m_statusText);
	statusText.LineScroll(statusText.GetLineCount());
}


void CSealHostEnvironmentDlg::OnBnClickedButtonBattery()
{
	unsigned char memoryBank[2];
	unsigned char data[2] = {0xFF,0xFF};
	unsigned char cmd;
	unsigned char status;
	unsigned char *resp;
    unsigned char serial[12] = { 0, };
    int retry;
	int timeout;
	int retV;


    memoryBank[0] = 0x00;
    memoryBank[1] = 0x04;

	timeout = 0;
	do {
		retV = rfRdr.read(memoryBank);
		resp = rfRdr.getRsp();
        if (rfRdr.getRspLen() > 0)
            break;
		Sleep(100);
		timeout++;
	} while (timeout < (_TIMEOUT * _DELAY / 100));

	if (timeout == (_TIMEOUT * _DELAY / 100)) {
		m_statusText.Format(_T("Error move the seal!\r\n"));
		statusText.SetWindowTextW(m_statusText);
        return;
	}

    unsigned char tag_ver = getNumber(resp[8], resp[9]);

    memoryBank[0] = 0x00;
    memoryBank[1] = 0x00;

    cmd = 0x06;

    for (retry = 0; retry < 3; retry++) {
		status = 0x01;
		retV   = rfRdr.write(memoryBank,status,cmd,data);

        if (retry > 0)
            Sleep(100);

        for (timeout = 0; timeout < 3; timeout++) {
			retV   = rfRdr.read(memoryBank);
			resp   = rfRdr.getRsp();
			status = getNumber(resp[4],resp[5]);
			if (status == 2)
				break;

			Sleep(100 + timeout * 100);
        }
    }

	if ((retry == 3) && (timeout == 3)) {
		m_statusText.AppendFormat(_T("Timeout!\r\n"));
		statusText.SetWindowTextW(m_statusText);
		return;
	} else if ((rfRdr.getRspLen() > 0) && (status == 2)) {

	} else if ((rfRdr.getRspLen() > 0) || (status != 0)) {
		m_statusText.AppendFormat(_T("Error: %s\r\n"),getCmdStatus(status));
		statusText.SetWindowTextW(m_statusText);
		return;
	} else {
		m_statusText.AppendFormat(_T("Error move the seal!\r\n"));
		statusText.SetWindowTextW(m_statusText);
		return;
	}



	status = 0x03;
	retV = rfRdr.write(memoryBank,status,cmd,data);


	timeout = 0;
	do {
		retV   = rfRdr.read(memoryBank);
		resp   = rfRdr.getRsp();
		status = getNumber(resp[4],resp[5]);
		if ((status != 1) && (status != 2) && (status != 3) && (status != 4))
			break;
		Sleep(100);
		timeout++;
	} while (timeout < (_TIMEOUT * _DELAY / 100));

	if (timeout == (_TIMEOUT * _DELAY / 100)) {
		m_statusText.AppendFormat(_T("Timeout!\r\n"));
		statusText.SetWindowTextW(m_statusText);
		return;
	} else if ((rfRdr.getRspLen() > 0) && (status == 0)) {

	} else if ((rfRdr.getRspLen() > 0) || (status != 0)) {
		m_statusText.AppendFormat(_T("Error: %s\r\n"),getCmdStatus(status));
		statusText.SetWindowTextW(m_statusText);
		return;
	} else {
		m_statusText.AppendFormat(_T("Error move the seal!\r\n"));
		statusText.SetWindowTextW(m_statusText);
		return;
	}



    memoryBank[0] = 0x00;
    memoryBank[1] = 0x05;


	timeout = 0;
	do {
		retV   = rfRdr.read(memoryBank);
		resp   = rfRdr.getRsp();
		if (getNumber(resp[2],resp[3]) == 0x25)
			break;
		Sleep(100);
		timeout++;
	} while (timeout < (_TIMEOUT * _DELAY / 100));

	if (timeout == (_TIMEOUT * _DELAY / 100)) {
		m_statusText.AppendFormat(_T("Timeout!\r\n"));
		statusText.SetWindowTextW(m_statusText);
		return;
    }

    unsigned char  batt = getNumber(resp[8], resp[9]);

	if(!language.CompareNoCase(_T("English"))){
		m_statusText.AppendFormat(_T("Battery status: 0x%02X %0.2f volts (%s)\r\n"), batt, (batt & 0xFE) * 0.02f, (batt & 1) ? _T("Low") : _T("Normal"));
	} else {
		m_statusText.AppendFormat(_T("Nível Bateria: 0x%02X %0.2f volts (%s)\r\n"), batt, (batt & 0xFE) * 0.02f, (batt & 1) ? _T("Fraca") : _T("Normal"));
	}

	statusText.SetWindowTextW(m_statusText);
	statusText.LineScroll(statusText.GetLineCount());
}


void CSealHostEnvironmentDlg::OnBnClickedButtonUid()
{
	unsigned char memoryBank[2];
	unsigned char histBank[2];
	unsigned char data[2] = {0xFF,0xFF};
	unsigned char cmd;
	unsigned char status;
	unsigned char *resp;
    unsigned char uid[8] = { 0, };
    int retry;
	int timeout;
	int retV;


	SYSTEMTIME  time_start,time_end;
	GetLocalTime(&time_start);


    cmd = 0x71;

    memoryBank[0] = 0x00;
    memoryBank[1] = 0x01;

	timeout = 0;
	do {
		retV = rfRdr.read(memoryBank);
		resp = rfRdr.getRsp();
        if (rfRdr.getRspLen() > 0)
            break;
		Sleep(100);
		timeout++;
	} while (timeout < (_TIMEOUT * _DELAY / 100));

	if (timeout == (_TIMEOUT * _DELAY / 100)) {
		m_statusText.Format(_T("Error move the seal!\r\n"));
		statusText.SetWindowTextW(m_statusText);
        return;
	}


    unsigned short histAddr = (getNumber(resp[4],resp[5]) << 8) |
                               getNumber(resp[2],resp[3]);
    histAddr >>= 2;



    memoryBank[0] = 0x00;
    memoryBank[1] = 0x00;

    for (retry = 0; retry < 3; retry++) {
		status = 0x01;
		retV   = rfRdr.write(memoryBank,status,cmd,data);

        if (retry > 0)
            Sleep(100);

        for (timeout = 0; timeout < 3; timeout++) {
			retV   = rfRdr.read(memoryBank);
			resp   = rfRdr.getRsp();
			status = getNumber(resp[4],resp[5]);
			if (status == 2)
				break;

			Sleep(100 + timeout * 100);
        }
    }

	if ((retry == 3) && (timeout == 3)) {
		m_statusText.AppendFormat(_T("Timeout!\r\n"));
		statusText.SetWindowTextW(m_statusText);
		return;
	} else if ((rfRdr.getRspLen() > 0) && (status == 2)) {

	} else if ((rfRdr.getRspLen() > 0) || (status != 0)) {
		m_statusText.AppendFormat(_T("Error: %s\r\n"),getCmdStatus(status));
		statusText.SetWindowTextW(m_statusText);
		return;
	} else {
		m_statusText.AppendFormat(_T("Error move the seal!\r\n"));
		statusText.SetWindowTextW(m_statusText);
		return;
	}



	status = 0x03;
	retV = rfRdr.write(memoryBank,status,cmd,data);


	timeout = 0;
	do {
		retV   = rfRdr.read(memoryBank);
		resp   = rfRdr.getRsp();
		status = getNumber(resp[4],resp[5]);
		if ((status != 1) && (status != 2) && (status != 3) && (status != 4))
			break;
		Sleep(100);
		timeout++;
	} while (timeout < (_TIMEOUT * _DELAY / 100));

	if (timeout == (_TIMEOUT * _DELAY / 100)) {
		m_statusText.AppendFormat(_T("Timeout!\r\n"));
		statusText.SetWindowTextW(m_statusText);
		return;
	} else if ((rfRdr.getRspLen() > 0) && (status == 0)) {

	} else if ((rfRdr.getRspLen() > 0) || (status != 0)) {
		m_statusText.AppendFormat(_T("Error: %s\r\n"),getCmdStatus(status));
		statusText.SetWindowTextW(m_statusText);
		return;
	} else {
		m_statusText.AppendFormat(_T("Error move the seal!\r\n"));
		statusText.SetWindowTextW(m_statusText);
		return;
	}



    for (unsigned short ix = 0; ix < 2; ix++) {
        unsigned short addr = histAddr + ix;

        histBank[0] = (addr >> 8) & 0xFF;
        histBank[1] =  addr       & 0xFF;

	    timeout = 0;
	    do {
		    retV = rfRdr.read(histBank);
		    resp = rfRdr.getRsp();
            if (rfRdr.getRspLen() > 0)
                break;
		    Sleep(100);
		    timeout++;
	    } while (timeout < (_TIMEOUT * _DELAY / 100));

	    if (timeout == (_TIMEOUT * _DELAY / 100)) {
		    m_statusText.Format(_T("Error move the seal!\r\n"));
		    statusText.SetWindowTextW(m_statusText);
            return;
	    }

        uid[(ix << 2) + 0] = getNumber(resp[2],resp[3]);
        uid[(ix << 2) + 1] = getNumber(resp[4],resp[5]);
        uid[(ix << 2) + 2] = getNumber(resp[6],resp[7]);
        uid[(ix << 2) + 3] = getNumber(resp[8],resp[9]);

	    retV = rfRdr.write(histBank,0xFF,0xFF,data);
    }

    GetLocalTime(&time_end);

	__int64 delta = TimeDelta(time_start, time_end);

	m_statusText.AppendFormat(_T("UID: "));

    for (unsigned char ix = 0; ix < 8; ix++) {
		m_statusText.AppendFormat(_T("%02X"), uid[ix]);
    }

    m_statusText.AppendFormat(_T("\r\n"));

	if(!language.CompareNoCase(_T("English"))){
    	m_statusText.AppendFormat(_T("Time Elapsed: %I64d ms\r\n"), delta);
	} else {
	    m_statusText.AppendFormat(_T("Tempo Transcorrido: %I64d ms\r\n"), delta);
	}

	statusText.SetWindowTextW(m_statusText);
	statusText.LineScroll(statusText.GetLineCount());
}
