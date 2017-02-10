// Debug.cpp : implementation file
//

#include "stdafx.h"
#include "TestAPI.h"
#include "Debug.h"
#include "CommPage.h"
#include ".\debug.h"

// CDebug dialog
extern CCommPage* comPage;

IMPLEMENT_DYNAMIC(CDebug, CDialog)
CDebug::CDebug(CWnd* pParent /*=NULL*/)
	: CDialog(CDebug::IDD, pParent)
{
	count = 0;
	rxStopDebug = false;
	txStopDebug = false;
}

CDebug::~CDebug()
{
}

void CDebug::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, RX_IDC_LIST, m_recvList);
	DDX_Control(pDX, TX_IDC_LIST, m_sendList);
	DDX_Control(pDX, IDC_BUTTON_RX_STOP, m_rxStop);
	DDX_Control(pDX, IDC_BUTTON_TX_STOP, m_txStop);
}


BEGIN_MESSAGE_MAP(CDebug, CDialog)
	ON_BN_CLICKED(IDC_BUTTON_RX_CLEAR, OnBnClickedButtonRxClear)
	ON_BN_CLICKED(IDC_BUTTON_TX_CLEAR, OnBnClickedButtonTxClear)
	ON_BN_CLICKED(IDC_BUTTON_RX_STOP, OnBnClickedButtonRxStop)
	ON_BN_CLICKED(IDC_BUTTON_TX_STOP, OnBnClickedButtonTxStop)
//	ON_WM_CLOSE()
ON_BN_CLICKED(IDC_BUTTON_DEBUG_CLOSE, OnBnClickedButtonDebugClose)
END_MESSAGE_MAP()


// CDebug message handlers

void CDebug::DisplayRecPackets(char buf[260], int len, char ip[20], bool frameFlag, bool crcFlag)
{

   if (rxStopDebug)
	   return;

   count++;
   int index = 60;

   //-----------Debug Window
   CString str;
   char tbuf[10];
   int length = len; 

   itoa(count, tbuf, 10);
   if (count < 10)
   {
      str = "0";
      str += tbuf;
   }
   else
      str = tbuf;
   str += " - ";
   buf[len+1] = '\0';

  if (frameFlag)
        str += "fEr ";

  if (crcFlag)
     str += "crcEr ";
        
   CString str2;
   for (int i=0; i<length; i++)
   {
	  str2.Format("%02x", ((int)(unsigned char)buf[i]));
	  str += str2;
      str += ' ';
   }

   if (str.GetLength() > index)
   {
       CString str1;
       str1 = str.Mid(index, str.GetLength() - index);

       if (!str1.IsEmpty())
       {
          index += 1;
          str1 = str.Mid(index, str.GetLength() - index);
       }

       if (!str1.IsEmpty())
       {
          index += 1;
          str1 = str.Mid(index, str.GetLength() - index);
       }

       if (!str1.IsEmpty())
       {
          index += 1;
          str1 = str.Mid(index, str.GetLength() - index);
       }
       str1 = "     " + str1;
	   m_recvList.InsertString(0, str1);
       str1 = str.Mid(1, index);
	   str1 += "     ";
	   str1 += ip;
	   m_recvList.InsertString(0, str1);
    }
    else
    {
	   str += "     ";
	   str += ip;
	   m_recvList.InsertString(0, str);
       str.Empty();
    }
}
void CDebug::DisplaySendPackets(char buf[260], int len, char ip[20], bool frameFlag, bool crcFlag)
{

   if (txStopDebug)
      return;

   count++;
   int index = 60;

   //-----------Debug Window
   CString str;
   char tbuf[10];
   int length = len;  

    itoa(count, tbuf, 10);
    if (count < 10)
    {
       str = "0";
       str += tbuf;
    }
    else
       str = tbuf;
    str += " - ";
    buf[len+1] = '\0';

    if (frameFlag)
      str += "fEr ";

    if (crcFlag)
      str += "crcEr ";
        
	CString str2;
    for (int i=0; i<length; i++)
    {
		str2.Format("%02x", ((int)(unsigned char)buf[i]));
		str += str2;
        str += ' ';
    }

    if (str.GetLength() > index)
    {
        CString str1;
        str1 = str.Mid(index, str.GetLength() - index);

        if (!str1.IsEmpty())
        {
            index += 1;
            str1 = str.Mid(index, str.GetLength() - index);
        }

        if (!str1.IsEmpty())
        {
            index += 1;
            str1 = str.Mid(index, str.GetLength() - index);
        }

        if (!str1.IsEmpty())
        {
            index += 1;
            str1 = str.Mid(index, str.GetLength() - index);
        }
        str1 = "     " + str1;
		m_sendList.InsertString(0, str1);
        str1 = str.Mid(1, index);
		str1 += "     ";
		str1 += ip;
		m_sendList.InsertString(0, str1);
     }
     else
     {
	    str += "     ";
		str += ip;
		m_sendList.InsertString(0, str);
        str.Empty();
     }
}

void CDebug::OnBnClickedButtonRxClear()
{
	m_recvList.ResetContent();
	count = 0;
}

void CDebug::OnBnClickedButtonTxClear()
{
	m_sendList.ResetContent();
	count = 0;
}

void CDebug::OnBnClickedButtonRxStop()
{
	CString caption;
	m_rxStop.GetDlgItemText(IDC_BUTTON_RX_STOP, caption);
	if (caption == "Stop")
	{
		rxStopDebug = true;
	    m_rxStop.SetDlgItemText(IDC_BUTTON_RX_STOP, "Start");
	}
	else
	{
        rxStopDebug = false;
	    m_rxStop.SetDlgItemText(IDC_BUTTON_RX_STOP, "Start");
	}
}

void CDebug::OnBnClickedButtonTxStop()
{
	CString caption;
	m_rxStop.GetDlgItemText(IDC_BUTTON_TX_STOP, caption);
	if (caption == "Stop")
	{
		txStopDebug = true;
	    m_txStop.SetDlgItemText(IDC_BUTTON_TX_STOP, "Start");
	}
	else
	{
        txStopDebug = false;
	    m_txStop.SetDlgItemText(IDC_BUTTON_TX_STOP, "Stop");
	}
}

void CDebug::OnBnClickedButtonDebugClose()
{
	comPage->CloseDebugWindow();
}
