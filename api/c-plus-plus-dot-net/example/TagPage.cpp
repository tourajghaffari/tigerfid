// TagPage.cpp : implementation file
//

#include "stdafx.h"
#include "TestAPI.h"
#include "TagPage.h"
#include "TestAPIDlg.h"
#include "AWI_API.h"
#include ".\tagpage.h"


extern CWnd* listBox;
extern CTestAPIDlg* apiDlg;
extern CTagPage* tagPage;
extern rfTagTemp_t* tagTemp;
extern int pktCounter;
extern rfTagSelect_t* tagSelect;
extern int pktID;
extern int HexToInt(char * buf, int size);

// CTagPage dialog
IMPLEMENT_DYNAMIC(CTagPage, CDialog)
CTagPage::CTagPage(CWnd* pParent /*=NULL*/)
	: CDialog(CTagPage::IDD, pParent)
{
	tagPage = this;
}

CTagPage::~CTagPage()
{
}

void CTagPage::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}


BEGIN_MESSAGE_MAP(CTagPage, CDialog)
	ON_BN_CLICKED(IDC_RADIO_TEMP, OnBnClickedRadioTemp)
	ON_BN_CLICKED(IDC_RADIO_TEMP_CALIB, OnBnClickedRadioTempCalib)
	ON_BN_CLICKED(IDC_RADIO_LED_CONFIG, OnBnClickedRadioLedConfig)
//	ON_BN_CLICKED(IDC_RADIO_SPEAKER_CONFIG, OnBnClickedRadioSpeakerConfig)
	ON_BN_CLICKED(IDC_BUTTON_CALL_TAG, OnBnClickedButtonCallTag)
	ON_BN_CLICKED(IDC_BUTTON_QUERY_TAG, OnBnClickedButtonQueryTag)
	ON_BN_CLICKED(IDC_BUTTON_ENABLE_TAG, OnBnClickedButtonEnableTag)
	ON_BN_CLICKED(IDC_BUTTON_DISABLE_TAG, OnBnClickedButtonDisableTag)
	ON_BN_CLICKED(IDC_BUTTON_GET_TEMP_CONFIG, OnBnClickedButtonGetTempConfig)
	ON_BN_CLICKED(IDC_BUTTON_GET_TAG_TEMP_CALIB, OnBnClickedButtonGetTagTempCalib)
	ON_BN_CLICKED(IDC_BUTTON_SET_TAG_TEMP_CALIB, OnBnClickedButtonSetTagTempCalib)
	ON_BN_CLICKED(IDC_BUTTON_GET_TAG_LED_CONFIG, OnBnClickedButtonGetTagLedConfig)
	ON_BN_CLICKED(IDC_BUTTON_SET_TAG_LED_CONFIG, OnBnClickedButtonSetTagLedConfig)
	ON_WM_CHILDACTIVATE()
	ON_BN_CLICKED(IDC_BUTTON_SET_TEMP_CONFIG, OnBnClickedButtonSetTempConfig)
	ON_BN_CLICKED(IDC_BUTTON_GET_TAG_TEMP, OnBnClickedButtonGetTagTemp)
	ON_BN_CLICKED(IDC_BUTTON_READ_TAG, OnBnClickedButtonReadTag)
	ON_BN_CLICKED(IDC_RADIO_WRITE_TAG, OnBnClickedRadioWriteTag)
	ON_BN_CLICKED(IDC_RADIO_READ_TAG, OnBnClickedRadioReadTag)
	ON_BN_CLICKED(IDC_BUTTON_WRITE_TAG, OnBnClickedButtonWriteTag)
//	ON_BN_CLICKED(IDC_CHECK_MODIFY_ID, OnBnClickedCheckModifyId)
//	ON_BN_CLICKED(IDC_RADIO_CONFIG, OnBnClickedRadioConfig)
//	ON_BN_CLICKED(IDC_CHECK_MODIFY_TYPE, OnBnClickedCheckModifyType)
//	ON_BN_CLICKED(IDC_CHECK_MODIFY_SEND_TIME, OnBnClickedCheckModifySendTime)
//	ON_BN_CLICKED(IDC_CHECK_MODIFY_RPT_TAMPER, OnBnClickedCheckModifyRptTamper)
//	ON_BN_CLICKED(IDC_CHECK_MODIFY_FACTORY, OnBnClickedCheckModifyFactory)
//	ON_BN_CLICKED(IDC_BUTTON_CONFIG_TAG, OnBnClickedButtonConfigTag)
	//ON_BN_CLICKED(IDC_CHECK_ANY_ID, OnBnClickedCheckAnyId)
//	ON_BN_CLICKED(IDC_CHECK_MODIFY_TIFGC, OnBnClickedCheckModifyTifgc)
//ON_NOTIFY(BCN_HOTITEMCHANGE, IDC_BUTTON_WRITE_TAG, OnBnHotItemChangeButtonWriteTag)
ON_BN_CLICKED(IDC_BUTTON_SET_TEMP_LOG_TIMESTAMP, OnBnClickedButtonSetTempLogTimestamp)
ON_BN_CLICKED(IDC_BUTTON_GET_TEMP_LOG_TIMESTAMP2, OnBnClickedButtonGetTempLogTimestamp2)
END_MESSAGE_MAP()


void CTagPage::OnBnClickedRadioTemp()
{
	EnableTempGroupBox(true);
	EnableTempCalibGroupBox(false);
	EnableLEDGroupBox(false);
	EnableReadTagGroupBox(false);
	EnableWriteTagGroupBox(false);
}

void CTagPage::EnableTempGroupBox(bool b)
{
   ((CWnd *)GetDlgItem(IDC_GROUP_TEMP))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_CHECK_REPORT_TEMP_UNDER))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_CHECK_REPORT_TEMP_UPPER))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_CHECK_REPORT_TEMP_PERIODIC))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_STATIC_00))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_STATIC_01))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_STATIC_02))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_EDIT_LOW_LIMIT_TEMP))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_EDIT_UP_LIMIT_TEMP))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_COMBO_NUM_READ_AVE))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_STATIC_03))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_EDIT_PERIODIC_REPORT_TIME))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_RADIO_TIME_HOUR))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_RADIO_TIME_MINUTE))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_BUTTON_GET_TEMP_CONFIG))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_BUTTON_SET_TEMP_CONFIG))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_BUTTON_GET_TAG_TEMP))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_CHECK_TAG_TEMP_LOGGING))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_BUTTON_SET_TEMP_LOG_TIMESTAMP))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_BUTTON_GET_TEMP_LOG_TIMESTAMP2))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_CHECK_WARP_AROUND))->EnableWindow(b); 
}

void CTagPage::OnBnClickedRadioTempCalib()
{
	EnableTempGroupBox(false);
	EnableTempCalibGroupBox(true);
	EnableLEDGroupBox(false);
	EnableReadTagGroupBox(false);
	EnableWriteTagGroupBox(false);
}

void CTagPage::EnableTempCalibGroupBox(bool b)
{
   ((CWnd *)GetDlgItem(IDC_GROUP_TEMP_CALIB))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_BUTTON_GET_TAG_TEMP_CALIB))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_BUTTON_SET_TAG_TEMP_CALIB))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_STATIC_04))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_EDIT_TAG_TEMP_CALIB))->EnableWindow(b);
}

void CTagPage::OnBnClickedRadioLedConfig()
{
	EnableTempGroupBox(false);
	EnableTempCalibGroupBox(false);
	EnableLEDGroupBox(true);
	EnableReadTagGroupBox(false);
	EnableWriteTagGroupBox(false);
}

void CTagPage::EnableLEDGroupBox(bool b)
{
   ((CWnd *)GetDlgItem(IDC_GROUP_LED_CONFIG))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_BUTTON_GET_TAG_LED_CONFIG))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_BUTTON_SET_TAG_LED_CONFIG))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_STATIC_05))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_EDIT_TAG_LED))->EnableWindow(b);
}

void CTagPage::OnBnClickedRadioWriteTag()
{
	EnableTempGroupBox(false);
	EnableTempCalibGroupBox(false);
	EnableLEDGroupBox(false);
	EnableReadTagGroupBox(false);
	EnableWriteTagGroupBox(true);
}

void CTagPage::EnableWriteTagGroupBox(bool b)
{
   ((CWnd *)GetDlgItem(IDC_STATIC_08))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_STATIC_16))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_STATIC_12))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_STATIC_13))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_STATIC_14))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_EDIT_MEM_START_ADDR_WRITE))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_EDIT_MEM_DATA))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_EDIT_MEM_DATA_LEN_WRITE))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_BUTTON_WRITE_TAG))->EnableWindow(b); 
   ((CWnd *)GetDlgItem(IDC_GROUP_WRITE_TAG))->EnableWindow(b);  
}

void CTagPage::EnableReadTagGroupBox(bool b)
{
   ((CWnd *)GetDlgItem(IDC_STATIC_07))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_STATIC_05))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_STATIC_09))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_STATIC_10))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_STATIC_15))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_EDIT_MEM_START_ADDR_READ))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_EDIT_MEM_DATA_LEN_READ))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_BUTTON_READ_TAG))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_GROUP_READ_TAG))->EnableWindow(b);
}

void CTagPage::OnBnClickedRadioReadTag()
{
	EnableTempGroupBox(false);
	EnableTempCalibGroupBox(false);
	EnableLEDGroupBox(false);
	EnableReadTagGroupBox(true);
	EnableWriteTagGroupBox(false);
}






//void CTagPage::OnBnClickedCheckModifyId()
//{
//	if (((CButton *)GetDlgItem(IDC_CHECK_MODIFY_ID))->GetCheck() == BST_CHECKED)
//	{
//       ((CWnd *)GetDlgItem(IDC_EDIT_NEW_TAG_ID))->EnableWindow(true);
//       ((CWnd *)GetDlgItem(IDC_STATIC_CFG_01))->EnableWindow(true);
//
//	    ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_TIFGC))->EnableWindow(false);
//		((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_SEND_TIME))->EnableWindow(false);
//		((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_RPT_TAMPER))->EnableWindow(false);
//		((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_FACTORY))->EnableWindow(false);   
//	}
//	else
//	{
//       ((CWnd *)GetDlgItem(IDC_EDIT_NEW_TAG_ID))->EnableWindow(false);
//       ((CWnd *)GetDlgItem(IDC_STATIC_CFG_01))->EnableWindow(false);
//
//	   if (((CButton *)GetDlgItem(IDC_CHECK_MODIFY_TYPE))->GetCheck() == BST_UNCHECKED)
//	   {
//			((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_TIFGC))->EnableWindow(true);
//			((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_SEND_TIME))->EnableWindow(true);
//			((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_RPT_TAMPER))->EnableWindow(true);
//			((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_FACTORY))->EnableWindow(true);
//			((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_TYPE))->EnableWindow(true); 
//	   }
//	}	
//}

//void CTagPage::OnBnClickedCheckModifyType()
//{
//	if (((CButton *)GetDlgItem(IDC_CHECK_MODIFY_TYPE))->GetCheck() == BST_CHECKED)
//	{
//       ((CWnd *)GetDlgItem(IDC_COMBO_NEW_TAG_TYPE))->EnableWindow(true);
//       ((CWnd *)GetDlgItem(IDC_STATIC_CFG_02))->EnableWindow(true);
// 
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_TIFGC))->EnableWindow(false);
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_SEND_TIME))->EnableWindow(false);
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_RPT_TAMPER))->EnableWindow(false);
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_FACTORY))->EnableWindow(false); 
//	}
//	else
//	{
//       ((CWnd *)GetDlgItem(IDC_COMBO_NEW_TAG_TYPE))->EnableWindow(false);
//       ((CWnd *)GetDlgItem(IDC_STATIC_CFG_02))->EnableWindow(false);
//
//	   if (((CButton *)GetDlgItem(IDC_CHECK_MODIFY_ID))->GetCheck() == BST_UNCHECKED)
//	   {
//			((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_ID))->EnableWindow(true); 
//			((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_TIFGC))->EnableWindow(true);
//			((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_SEND_TIME))->EnableWindow(true);
//			((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_RPT_TAMPER))->EnableWindow(true);
//			((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_FACTORY))->EnableWindow(true);
//	   }
//	}
//}

//void CTagPage::OnBnClickedCheckModifySendTime()
//{
//	if (((CButton *)GetDlgItem(IDC_CHECK_MODIFY_SEND_TIME))->GetCheck() == BST_CHECKED)
//	{ 
//	   ((CWnd *)GetDlgItem(IDC_GROUP_RESEND_TIME))->EnableWindow(true);
//	   ((CWnd *)GetDlgItem(IDC_STATIC_CFG_05))->EnableWindow(true);
//       ((CWnd *)GetDlgItem(IDC_EDIT_NEW_RESEND_TIME))->EnableWindow(true);
//       ((CWnd *)GetDlgItem(IDC_STATIC_CFG_05))->EnableWindow(true);
//	   ((CWnd *)GetDlgItem(IDC_RADIO_CFG_SEC))->EnableWindow(true);
//	   ((CWnd *)GetDlgItem(IDC_RADIO_CFG_MIN))->EnableWindow(true);
//	   ((CWnd *)GetDlgItem(IDC_RADIO_CFG_HOUR))->EnableWindow(true);
//
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_ID))->EnableWindow(false);
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_TYPE))->EnableWindow(false); 
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_TIFGC))->EnableWindow(false);
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_RPT_TAMPER))->EnableWindow(false);
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_FACTORY))->EnableWindow(false); 
//	}
//	else
//	{
//	   ((CWnd *)GetDlgItem(IDC_GROUP_RESEND_TIME))->EnableWindow(false);
//       ((CWnd *)GetDlgItem(IDC_EDIT_NEW_RESEND_TIME))->EnableWindow(false);
//       ((CWnd *)GetDlgItem(IDC_STATIC_CFG_05))->EnableWindow(false);
//	   ((CWnd *)GetDlgItem(IDC_RADIO_CFG_SEC))->EnableWindow(false);
//	   ((CWnd *)GetDlgItem(IDC_RADIO_CFG_MIN))->EnableWindow(false);
//	   ((CWnd *)GetDlgItem(IDC_RADIO_CFG_HOUR))->EnableWindow(false);
//
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_ID))->EnableWindow(true);
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_TYPE))->EnableWindow(true); 
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_TIFGC))->EnableWindow(true);
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_RPT_TAMPER))->EnableWindow(true);
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_FACTORY))->EnableWindow(true); 
//	}
//}

//void CTagPage::OnBnClickedCheckModifyRptTamper()
//{
//	if (((CButton *)GetDlgItem(IDC_CHECK_MODIFY_RPT_TAMPER))->GetCheck() == BST_CHECKED)
//	{
//	   ((CWnd *)GetDlgItem(IDC_GROUP_TAMPER))->EnableWindow(true);
//       ((CWnd *)GetDlgItem(IDC_RADIO_REPORT_TAMPER))->EnableWindow(true);
//	   ((CWnd *)GetDlgItem(IDC_RADIO_CFG_RPT_TAMPER_HISTORY))->EnableWindow(true);
//	   ((CWnd *)GetDlgItem(IDC_RADIO_NO_REPORT))->EnableWindow(true);
//
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_ID))->EnableWindow(false);
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_TYPE))->EnableWindow(false); 
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_TIFGC))->EnableWindow(false);
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_SEND_TIME))->EnableWindow(false);
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_FACTORY))->EnableWindow(false); 
//	}
//	else
//	{
//	   ((CWnd *)GetDlgItem(IDC_GROUP_TAMPER))->EnableWindow(false);
//       ((CWnd *)GetDlgItem(IDC_RADIO_REPORT_TAMPER))->EnableWindow(false);
//	   ((CWnd *)GetDlgItem(IDC_RADIO_CFG_RPT_TAMPER_HISTORY))->EnableWindow(false);
//	   ((CWnd *)GetDlgItem(IDC_RADIO_NO_REPORT))->EnableWindow(false);
//
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_ID))->EnableWindow(true);
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_TYPE))->EnableWindow(true); 
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_TIFGC))->EnableWindow(true);
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_SEND_TIME))->EnableWindow(true);
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_FACTORY))->EnableWindow(true); 
//	}
//}

//void CTagPage::OnBnClickedCheckModifyFactory()
//{
//	if (((CButton *)GetDlgItem(IDC_CHECK_MODIFY_FACTORY))->GetCheck() == BST_CHECKED)
//	{
//       ((CWnd *)GetDlgItem(IDC_CHECK_SET_FACTORY))->EnableWindow(true);
//
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_ID))->EnableWindow(false);
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_TYPE))->EnableWindow(false); 
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_TIFGC))->EnableWindow(false);
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_SEND_TIME))->EnableWindow(false);
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_RPT_TAMPER))->EnableWindow(false); 
//	}
//	else
//	{
//       ((CWnd *)GetDlgItem(IDC_CHECK_SET_FACTORY))->EnableWindow(false);
//
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_ID))->EnableWindow(true);
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_TYPE))->EnableWindow(true); 
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_TIFGC))->EnableWindow(true);
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_SEND_TIME))->EnableWindow(true);
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_RPT_TAMPER))->EnableWindow(true); 
//	}
//}

void CTagPage::OnBnClickedButtonCallTag()
{
	  char buf[64];
    unsigned short cmdType;
	  int size = 0;
	  unsigned int numTag = 0;
	  bool setTxTimeInt;
	  bool timeInt;

	  int rdrID = GetDlgItemInt(IDC_EDIT_RDR_ID, NULL,true);
    int hostID = GetDlgItemInt(IDC_EDIT_HOST_ID, NULL,true);

		CString str;
		GetDlgItemText(IDC_COMB_SELECT_TYPE, str);
		if (str.IsEmpty())
		{
				MessageBox("No Tag Function Type", "API", MB_ICONHAND);
			return;
		}

	  GetDlgItemText(IDC_COMB_READER_FUNC_TYPE, buf, 50);
    if (strcmp(buf, "SPECIFIC_READER") == 0)
      cmdType = SPECIFIC_READER;
    else if (strcmp(buf, "ALL_READERS") == 0)
      cmdType = ALL_READERS;
    else
    {
	  MessageBox("No Reader Function Type", "API", MB_ICONHAND);
      return;
    }

		if(((CButton *)GetDlgItem(IDC_RADIO_Long_INTERVAL))->GetCheck() == BST_CHECKED)
		{
			setTxTimeInt = true;
			timeInt = true; //long
		}
		else if(((CButton *)GetDlgItem(IDC_RADIO_SHORT_INTERVAL))->GetCheck() == BST_CHECKED)
		{
			setTxTimeInt = true;
			timeInt = false; //short
		}
		else
		{
			setTxTimeInt = false;
			timeInt = false; 
		}
	
    tagSelect = new rfTagSelect_t;

    if(((CButton *)GetDlgItem(IDC_CHECK_TAG_LED))->GetCheck() == BST_CHECKED)
        tagSelect->ledOn = true;
	  else
        tagSelect->ledOn = false;

		if(((CButton *)GetDlgItem(IDC_CHECK_TAG_SPEAKER))->GetCheck() == BST_CHECKED)
				tagSelect->speakerOn = true;
		else
				tagSelect->speakerOn = false;

		if (str == "RF_SELECT_FIELD")
		{
			tagSelect->selectType = RF_SELECT_FIELD;
			int ret = rfCallTags(1, rdrID, 0, 0, tagSelect, setTxTimeInt, timeInt, cmdType, ++pktID);
			apiDlg->Display("rfCallTag()", pktID, ret);
			delete tagSelect;
			return;
		}
		else
		{ 
				if ((str == "RF_SELECT_TAG_ID") || (str == "RF_SELECT_TAG_RANGE"))
				{
						UINT tagID = GetDlgItemInt(IDC_EDIT_TAG_ID_1, NULL, false); 
						if (tagID == 0)
						{
							 MessageBox("No Tag ID", "API", MB_ICONHAND);
							 delete tagSelect;
							 return;
						}
						else
						{
							tagSelect->tagList[0] = tagID;
						   numTag++;
						}

            if (str == "RF_SELECT_TAG_RANGE")
            {
						   tagSelect->selectType = RF_SELECT_TAG_RANGE;
               tagSelect->rangeIndex = (Byte)GetDlgItemInt(IDC_COMB_ID_RANGE, NULL, true);
            }
						else
							tagSelect->selectType = RF_SELECT_TAG_ID;

						/*tagID = GetDlgItemInt(IDC_EDIT_TAG_ID_2, NULL, false); 
						if (tagID != 0)
						{
								tagSelect->tagList[1] = tagID;
								numTag++;
						}

						tagID = GetDlgItemInt(IDC_EDIT_TAG_ID_3, NULL, false); 
						if (tagID != 0)
						{
								tagSelect->tagList[2] = tagID;
								numTag++;
						}*/

						tagSelect->numTags = numTag;
				}
				else
						tagSelect->selectType = RF_SELECT_TAG_TYPE;

				if (((CButton *)GetDlgItem(IDC_RADIO_ACCESS))->GetCheck() == BST_CHECKED)
						tagSelect->tagType = ACCESS_TAG;
				else if (((CButton *)GetDlgItem(IDC_RADIO_ASSET))->GetCheck() == BST_CHECKED)
						tagSelect->tagType = ASSET_TAG;
				else if (((CButton *)GetDlgItem(IDC_RADIO_INVENTORY))->GetCheck() == BST_CHECKED)
						tagSelect->tagType = INVENTORY_TAG;
				else if (((CButton *)GetDlgItem(IDC_RADIO_FACTORY))->GetCheck() == BST_CHECKED)
						tagSelect->tagType = FACTORY_TAG;
				else
				{
						MessageBox("No Tag Type", "API", MB_ICONHAND);
					  delete tagSelect;
						return;
				}
	  }
  
	  int ret = rfCallTags(hostID, rdrID, 0, 0, tagSelect, setTxTimeInt, timeInt, cmdType, ++pktID);

	  apiDlg->Display("rfCallTag()", pktID, ret);
	  delete tagSelect;
}

void CTagPage::OnBnClickedButtonQueryTag()
{
		char buf[64];
    unsigned short cmdType;
		int size = 0;
		unsigned int numTag = 0;
		bool setTxTimeInt;
		bool timeInt;

		GetDlgItemText(IDC_COMB_READER_FUNC_TYPE, buf, 50);
    if (strcmp(buf, "SPECIFIC_READER") == 0)
      cmdType = SPECIFIC_READER;
    else if (strcmp(buf, "ALL_READERS") == 0)
      cmdType = ALL_READERS;
    else
    {
				MessageBox("No Reader Function Type", "API", MB_ICONHAND);
				return;
    }

		CString str;
		tagSelect = new rfTagSelect_t;

		if(((CButton *)GetDlgItem(IDC_CHECK_TAG_LED))->GetCheck() == BST_CHECKED)
				tagSelect->ledOn = true;
		else
				tagSelect->ledOn = false;

		if(((CButton *)GetDlgItem(IDC_CHECK_TAG_SPEAKER))->GetCheck() == BST_CHECKED)
				tagSelect->speakerOn = true;
		else
				tagSelect->speakerOn = false;

		GetDlgItemText(IDC_COMB_SELECT_TYPE, str);
		if (str == "RF_SELECT_FIELD")
		{
				tagSelect->selectType = RF_SELECT_FIELD;
		}
		else if ((str == "RF_SELECT_TAG_ID") || (str == "RF_SELECT_TAG_RANGE"))
		{
       UINT tagID = GetDlgItemInt(IDC_EDIT_TAG_ID_1, NULL, false); 
			 if (tagID == 0)
			 {
          MessageBox("No Tag ID", "API", MB_ICONHAND);
					delete tagSelect;
					return;
			 }
			 else
			 {
	            tagSelect->tagList[0] = tagID;
			    numTag++;
			 }

		   if (str == "RF_SELECT_TAG_RANGE")
           {
			   tagSelect->selectType = RF_SELECT_TAG_RANGE;
               tagSelect->rangeIndex = (Byte)GetDlgItemInt(IDC_COMB_ID_RANGE, NULL, true);
           }
		  else
			   tagSelect->selectType = RF_SELECT_TAG_ID;

	     tagSelect->numTags = numTag;
		}
		else if (str == "RF_SELECT_TAG_TYPE")
		{
        tagSelect->selectType = RF_SELECT_TAG_TYPE;
		}
		else
		{
       MessageBox("No Tag Function Type", "API", MB_ICONHAND);
			 delete tagSelect;
       return;
		}
  
	  if (str != "RF_SELECT_FIELD")
		{
				if (((CButton *)GetDlgItem(IDC_RADIO_ACCESS))->GetCheck() == BST_CHECKED)
						tagSelect->tagType = ACCESS_TAG;
				else if (((CButton *)GetDlgItem(IDC_RADIO_ASSET))->GetCheck() == BST_CHECKED)
					  tagSelect->tagType = ASSET_TAG;
				else if (((CButton *)GetDlgItem(IDC_RADIO_INVENTORY))->GetCheck() == BST_CHECKED)
					  tagSelect->tagType = INVENTORY_TAG;
				else if (((CButton *)GetDlgItem(IDC_RADIO_FACTORY))->GetCheck() == BST_CHECKED)
					  tagSelect->tagType = FACTORY_TAG;
				else if (((CButton *)GetDlgItem(IDC_RADIO_ANY_TYPE))->GetCheck() == BST_CHECKED)
					  tagSelect->tagType = ALL_TAGS;
				else
				{
						MessageBox("No Tag Type", "API", MB_ICONHAND);
						delete tagSelect;
						return;
				}
		}

		UINT tagID = GetDlgItemInt(IDC_EDIT_TAG_ID_1, NULL, false);
		numTag++;

		int rdrID = GetDlgItemInt(IDC_EDIT_RDR_ID, NULL,true);
    int hostID = GetDlgItemInt(IDC_EDIT_HOST_ID, NULL,true);

		if(((CButton *)GetDlgItem(IDC_RADIO_Long_INTERVAL))->GetCheck() == BST_CHECKED)
		{
			 setTxTimeInt = true;
			 timeInt = true; //long
		}
		else if(((CButton *)GetDlgItem(IDC_RADIO_SHORT_INTERVAL))->GetCheck() == BST_CHECKED)
		{
			 setTxTimeInt = true;
			 timeInt = false; //short
		}
		else
		{
       setTxTimeInt = false;
			 timeInt = false; 
		}
	
		int ret = rfQueryTags(hostID, rdrID, 0, tagSelect, setTxTimeInt, timeInt, cmdType, ++pktID);

		apiDlg->Display("rfQueryTag()", pktID, ret);
		delete tagSelect;
}

void CTagPage::OnBnClickedButtonEnableTag()
{
		char buf[64];
        unsigned short cmdType;
		bool setTxTimeInt;
		bool timeInt;

		CString str;
		GetDlgItemText(IDC_COMB_SELECT_TYPE, str);
		if (str.IsEmpty())
		{
				MessageBox("No Function Type", "API", MB_ICONHAND);
				return;
		}

		int rdrID = GetDlgItemInt(IDC_EDIT_RDR_ID, NULL,true);
		int hostID = GetDlgItemInt(IDC_EDIT_HOST_ID, NULL,true);

		GetDlgItemText(IDC_COMB_READER_FUNC_TYPE, buf, 50);
		if (strcmp(buf, "SPECIFIC_READER") == 0)
			cmdType = SPECIFIC_READER;
		else if (strcmp(buf, "ALL_READERS") == 0)
			cmdType = ALL_READERS;
		else
		{
			MessageBox("No Reader Function Type", "API", MB_ICONHAND);
			return;
		}

		tagSelect = new rfTagSelect_t;

		if(((CButton *)GetDlgItem(IDC_CHECK_TAG_LED))->GetCheck() == BST_CHECKED)
			tagSelect->ledOn = true;
		else
			tagSelect->ledOn = false;

		if(((CButton *)GetDlgItem(IDC_CHECK_TAG_SPEAKER))->GetCheck() == BST_CHECKED)
			tagSelect->speakerOn = true;
		else
			tagSelect->speakerOn = false;

		GetDlgItemText(IDC_COMB_SELECT_TYPE, str);
		if (str == "RF_SELECT_FIELD")
		{
			tagSelect->selectType = RF_SELECT_FIELD;
		}
		else if ((str == "RF_SELECT_TAG_ID") || (str == "RF_SELECT_TAG_RANGE"))
		{
				UINT tagID = GetDlgItemInt(IDC_EDIT_TAG_ID_1, NULL, false); 
				if (tagID == 0)
				{
						MessageBox("No Tag ID", "API", MB_ICONHAND);
						delete tagSelect;
						return;
				}
       
				if (str == "RF_SELECT_TAG_RANGE")
				{
						tagSelect->selectType = RF_SELECT_TAG_RANGE;
						tagSelect->rangeIndex = (Byte)GetDlgItemInt(IDC_COMB_ID_RANGE, NULL, true);
				}
				else
						tagSelect->selectType = RF_SELECT_TAG_ID;

				if (((CButton *)GetDlgItem(IDC_RADIO_ACCESS))->GetCheck() == BST_CHECKED)
						tagSelect->tagType = ACCESS_TAG;
				else if (((CButton *)GetDlgItem(IDC_RADIO_ASSET))->GetCheck() == BST_CHECKED)
					 tagSelect->tagType = ASSET_TAG;
				else if (((CButton *)GetDlgItem(IDC_RADIO_INVENTORY))->GetCheck() == BST_CHECKED)
						tagSelect->tagType = INVENTORY_TAG;
				else if (((CButton *)GetDlgItem(IDC_RADIO_FACTORY))->GetCheck() == BST_CHECKED)
						tagSelect->tagType = FACTORY_TAG;
				else
				{
					//if (tagSelect->selectType == RF_SELECT_FIELD)
					//{
						MessageBox("No Tag Type", "API", MB_ICONHAND);
						delete tagSelect;
						return;
					//}
				}
		}
		else if (str == "RF_SELECT_TAG_TYPE")
		{
			tagSelect->selectType = RF_SELECT_TAG_TYPE;
			GetDlgItemText(IDC_COMB_TAG_TYPE, str);
			if (str.IsEmpty())
			{
				MessageBox("No Tag Type", "API", MB_ICONHAND);
				delete tagSelect;
				return;
			 }
		}
		else
		{
			MessageBox("No Tag Function Type", "API", MB_ICONHAND);
			delete tagSelect;
			return;
		}
  
		if (((CButton *)GetDlgItem(IDC_RADIO_ACCESS))->GetCheck() == BST_CHECKED)
				tagSelect->tagType = ACCESS_TAG;
		else if (((CButton *)GetDlgItem(IDC_RADIO_ASSET))->GetCheck() == BST_CHECKED)
				tagSelect->tagType = ASSET_TAG;
		else if (((CButton *)GetDlgItem(IDC_RADIO_INVENTORY))->GetCheck() == BST_CHECKED)
				tagSelect->tagType = INVENTORY_TAG;
		else if (((CButton *)GetDlgItem(IDC_RADIO_FACTORY))->GetCheck() == BST_CHECKED)
				tagSelect->tagType = FACTORY_TAG;
		else
		{
			if (tagSelect->selectType != RF_SELECT_FIELD)
			{
				MessageBox("No Tag Type", "API", MB_ICONHAND);
				delete tagSelect;
				return;
			}
		}

		int size = 0;
		unsigned int numTag = 0;

		UINT tagID = GetDlgItemInt(IDC_EDIT_TAG_ID_1, NULL, false);
		numTag++;

		tagSelect->tagList[0] = tagID;
		tagSelect->numTags = numTag;

		if(((CButton *)GetDlgItem(IDC_RADIO_Long_INTERVAL))->GetCheck() == BST_CHECKED)
		{
			setTxTimeInt = true;
			timeInt = true; //long
		}
		else if(((CButton *)GetDlgItem(IDC_RADIO_SHORT_INTERVAL))->GetCheck() == BST_CHECKED)
		{
			setTxTimeInt = true;
			timeInt = false; //short
		}
		else
		{
			setTxTimeInt = false;
			timeInt = false; 
		}
	
		int ret = rfEnableTags(hostID, rdrID, 0, tagSelect, true, setTxTimeInt, timeInt, cmdType, ++pktID);

		apiDlg->DisplayTag("rfEnableTags() ", pktID, ret, tagID, str);
		delete tagSelect;
}

void CTagPage::OnBnClickedButtonDisableTag()
{
		char buf[64];
    unsigned short cmdType;
		bool setTxTimeInt;
		bool timeInt;

		CString str;
		GetDlgItemText(IDC_COMB_SELECT_TYPE, str);
		if (str.IsEmpty())
		{
				MessageBox("No Function Type", "API", MB_ICONHAND);
				return;
		}

		int rdrID = GetDlgItemInt(IDC_EDIT_RDR_ID, NULL,true);
    int hostID = GetDlgItemInt(IDC_EDIT_HOST_ID, NULL,true);

		GetDlgItemText(IDC_COMB_READER_FUNC_TYPE, buf, 50);
    if (strcmp(buf, "SPECIFIC_READER") == 0)
				cmdType = SPECIFIC_READER;
    else if (strcmp(buf, "ALL_READERS") == 0)
				cmdType = ALL_READERS;
    else
    {
				MessageBox("No Reader Function Type", "API", MB_ICONHAND);
				return;
    }

		GetDlgItemText(IDC_COMB_SELECT_TYPE, str);
		tagSelect = new rfTagSelect_t;

		if(((CButton *)GetDlgItem(IDC_CHECK_TAG_LED))->GetCheck() == BST_CHECKED)
        tagSelect->ledOn = true;
		else
        tagSelect->ledOn = false;

		if(((CButton *)GetDlgItem(IDC_CHECK_TAG_SPEAKER))->GetCheck() == BST_CHECKED)
        tagSelect->speakerOn = true;
		else
        tagSelect->speakerOn = false;

		if (str == "RF_SELECT_FIELD")
		{
				tagSelect->selectType = RF_SELECT_FIELD;
		}
		else if ((str == "RF_SELECT_TAG_ID") || (str == "RF_SELECT_TAG_RANGE"))
		{
				UINT tagID = GetDlgItemInt(IDC_EDIT_TAG_ID_1, NULL, false); 
				if (tagID == 0)
				{
           MessageBox("No Tag ID", "API", MB_ICONHAND);
					 delete tagSelect;
					 return;
				}
			 
			  if (str == "RF_SELECT_TAG_RANGE")
        {
						tagSelect->selectType = RF_SELECT_TAG_RANGE;
            tagSelect->rangeIndex = (Byte)GetDlgItemInt(IDC_COMB_ID_RANGE, NULL, true);
        }
				else
					tagSelect->selectType = RF_SELECT_TAG_ID;

				if (((CButton *)GetDlgItem(IDC_RADIO_ACCESS))->GetCheck() == BST_CHECKED)
						tagSelect->tagType = ACCESS_TAG;
				else if (((CButton *)GetDlgItem(IDC_RADIO_ASSET))->GetCheck() == BST_CHECKED)
						tagSelect->tagType = ASSET_TAG;
				else if (((CButton *)GetDlgItem(IDC_RADIO_INVENTORY))->GetCheck() == BST_CHECKED)
						tagSelect->tagType = INVENTORY_TAG;
				else if (((CButton *)GetDlgItem(IDC_RADIO_FACTORY))->GetCheck() == BST_CHECKED)
						tagSelect->tagType = FACTORY_TAG;
				else
				{
						MessageBox("No Tag Type", "API", MB_ICONHAND);
						delete tagSelect;
						return;
				}
		}
		else if (str == "RF_SELECT_TAG_TYPE")
		{
        tagSelect->selectType = RF_SELECT_TAG_TYPE;
				GetDlgItemText(IDC_COMB_TAG_TYPE, str);
				if (str.IsEmpty())
				{
						MessageBox("No Tag Type", "API", MB_ICONHAND);
						return;
				}
		}
		else
		{
       MessageBox("No Tag Function Type", "API", MB_ICONHAND);
       return;
		}
  
    if (((CButton *)GetDlgItem(IDC_RADIO_ACCESS))->GetCheck() == BST_CHECKED)
				tagSelect->tagType = ACCESS_TAG;
		else if (((CButton *)GetDlgItem(IDC_RADIO_ASSET))->GetCheck() == BST_CHECKED)
       tagSelect->tagType = ASSET_TAG;
		else if (((CButton *)GetDlgItem(IDC_RADIO_INVENTORY))->GetCheck() == BST_CHECKED)
       tagSelect->tagType = INVENTORY_TAG;
		else if (((CButton *)GetDlgItem(IDC_RADIO_FACTORY))->GetCheck() == BST_CHECKED)
       tagSelect->tagType = FACTORY_TAG;
		else
		{
        MessageBox("No Tag Type", "API", MB_ICONHAND);
				delete tagSelect;
				return;
		}

		int size = 0;
		unsigned int numTag = 0;

		UINT tagID = GetDlgItemInt(IDC_EDIT_TAG_ID_1, NULL, false);
		numTag++;

		tagSelect->tagList[0] = tagID;
		tagSelect->numTags = numTag;

		if(((CButton *)GetDlgItem(IDC_RADIO_Long_INTERVAL))->GetCheck() == BST_CHECKED)
		{
				setTxTimeInt = true;
				timeInt = true; //long
		}
		else if(((CButton *)GetDlgItem(IDC_RADIO_SHORT_INTERVAL))->GetCheck() == BST_CHECKED)
		{
				setTxTimeInt = true;
				timeInt = false; //short
		}
		else
		{
				setTxTimeInt = false;
				timeInt = false; 
		}
	
		int ret = rfEnableTags(hostID, rdrID, 0, tagSelect, false, setTxTimeInt, timeInt, cmdType, ++pktID);

		apiDlg->DisplayTag("rfEnableTags() ", pktID, ret, tagID, str);
		delete tagSelect;
}

void CTagPage::OnBnClickedButtonGetTempConfig()
{
	char buf[64];
    unsigned short cmdType;

	CString str;
	GetDlgItemText(IDC_COMB_SELECT_TYPE, str);
	if (str.IsEmpty())
	{
		MessageBox("No Tag Function Type", "API", MB_ICONHAND);
		return;
	}

	GetDlgItemText(IDC_COMB_READER_FUNC_TYPE, buf, 50);
    if (strcmp(buf, "SPECIFIC_READER") == 0)
	cmdType = SPECIFIC_READER;
    else if (strcmp(buf, "ALL_READERS") == 0)
		cmdType = ALL_READERS;
    else
    {
		MessageBox("No Reader Function Type", "API", MB_ICONHAND);
		return;
    }

	tagSelect = new rfTagSelect_t;

	if (str == "RF_SELECT_FIELD")
	{
		tagSelect->selectType = RF_SELECT_FIELD;
	}
	else if (str == "RF_SELECT_TAG_TYPE")
	{
       tagSelect->selectType = RF_SELECT_TAG_TYPE;
	   GetDlgItemText(IDC_COMB_TAG_TYPE, str);
	   if (str.IsEmpty())
	   {
          MessageBox("No Tag Type", "API", MB_ICONHAND);
		  delete tagSelect;
	      return;
	   }
	}
	else if ((str == "RF_SELECT_TAG_ID") || (str == "RF_SELECT_TAG_RANGE"))
	{
	   if (str == "RF_SELECT_TAG_RANGE")
       {
		  tagSelect->selectType = RF_SELECT_TAG_RANGE;
          tagSelect->rangeIndex = (Byte)GetDlgItemInt(IDC_COMB_ID_RANGE, NULL, true);
       }
	   else
		  tagSelect->selectType = RF_SELECT_TAG_ID;
	}
	else  
	{
       MessageBox("No Tag Function Type", "API", MB_ICONHAND);
	   delete tagSelect;
       return;
	}
  
	if (((CButton *)GetDlgItem(IDC_RADIO_ACCESS))->GetCheck() == BST_CHECKED)
			tagSelect->tagType = ACCESS_TAG;
	else if (((CButton *)GetDlgItem(IDC_RADIO_ASSET))->GetCheck() == BST_CHECKED)
			tagSelect->tagType = ASSET_TAG;
	else if (((CButton *)GetDlgItem(IDC_RADIO_INVENTORY))->GetCheck() == BST_CHECKED)
			tagSelect->tagType = INVENTORY_TAG;
	else if (((CButton *)GetDlgItem(IDC_RADIO_FACTORY))->GetCheck() == BST_CHECKED)
			tagSelect->tagType = FACTORY_TAG;
	else
	{
		MessageBox("No Tag Type", "API", MB_ICONHAND);
		delete tagSelect;
		return;
	}

	int size = 0;
	unsigned int numTag = 0;

	UINT tagID = GetDlgItemInt(IDC_EDIT_TAG_ID_1, NULL, false); 
	if (tagID == 0)
	{
		MessageBox("No Tag ID", "API", MB_ICONHAND);
		delete tagSelect;
		return;
	}
	else
	{
		tagSelect->tagList[0] = tagID;
		numTag++;
	}
	tagSelect->numTags = numTag;

	int rdrID = GetDlgItemInt(IDC_EDIT_RDR_ID, NULL,true);
	int hostID = GetDlgItemInt(IDC_EDIT_HOST_ID, NULL,true);
	
	bool interval = false; 
	if(((CButton *)GetDlgItem(IDC_RADIO_Long_INTERVAL))->GetCheck() == BST_CHECKED)
		interval = true;
	int ret = rfGetTagTempConfig(hostID, rdrID, 0, tagSelect, false, false, cmdType, ++pktID);

	apiDlg->Display("rfGetTagTempConfig()", pktID, ret);
	delete tagSelect;
}

void CTagPage::OnBnClickedButtonGetTagTempCalib()
{
	CString str, str1;
	char buf[11];
	float f = rfGetTagTempCalib();
	itoa(++pktCounter, buf, 10);
	str = buf;
	str += " -";
	str += "   rfGetTagTempCalib()  calibration = ";
	str1.Format("%.2f", f);
	str += str1;
	((CListBox *)listBox)->InsertString(0, str);
    MessageBeep(0xFFFFFFFF);
}

void CTagPage::OnBnClickedButtonSetTagTempCalib()
{
	char buf[10];
	CString str;
	itoa(++pktCounter, buf, 10);
	str = buf;
	str += " -";
	str += "   rfSetTagTempCalib()  calibration = ";
	GetDlgItemText(IDC_EDIT_TAG_TEMP_CALIB, buf, 10);
	str += buf;
	float f = (float)atof(buf);
    int ret = rfSetTagTempCalib(f);
	str += "  return Code = ";
	itoa(ret, buf, 10);
	str += buf;
	((CListBox *)listBox)->InsertString(0, str);
    MessageBeep(0xFFFFFFFF);
}

void CTagPage::OnBnClickedButtonGetTagLedConfig()
{
	char buf[64];
    unsigned short cmdType;
	bool timeInt;

	GetDlgItemText(IDC_COMB_READER_FUNC_TYPE, buf, 50);
    if (strcmp(buf, "SPECIFIC_READER") == 0)
      cmdType = SPECIFIC_READER;
    else if (strcmp(buf, "ALL_READERS") == 0)
      cmdType = ALL_READERS;
    else
    {
	  MessageBox("No Reader Function Type", "API", MB_ICONHAND);
      return;
    }

	int rdrID = GetDlgItemInt(IDC_EDIT_RDR_ID, NULL,true);
    int hostID = GetDlgItemInt(IDC_EDIT_HOST_ID, NULL,true);

	GetDlgItemText(IDC_COMB_READER_FUNC_TYPE, buf, 50);
    if (strcmp(buf, "SPECIFIC_READER") == 0)
      cmdType = SPECIFIC_READER;
    else if (strcmp(buf, "ALL_READERS") == 0)
      cmdType = ALL_READERS;
    else
    {
	  MessageBox("No Reader Function Type", "API", MB_ICONHAND);
      return;
    }

	tagSelect = new rfTagSelect_t;
	CString str;
	GetDlgItemText(IDC_COMB_SELECT_TYPE, str);
	if (str == "RF_SELECT_FIELD")
	{
		tagSelect->selectType = RF_SELECT_FIELD;
	}
	else if (str == "RF_SELECT_TAG_ID")
	{
        tagSelect->selectType = RF_SELECT_TAG_ID;

		UINT tagID = GetDlgItemInt(IDC_EDIT_TAG_ID_1, NULL, false); 
	    if (tagID == 0)
	    {
           MessageBox("No Tag ID", "API", MB_ICONHAND);
		   delete tagSelect;
	       return;
	    }

		if (((CButton *)GetDlgItem(IDC_RADIO_ACCESS))->GetCheck() == BST_CHECKED)
	       tagSelect->tagType = ACCESS_TAG;
	    else if (((CButton *)GetDlgItem(IDC_RADIO_ASSET))->GetCheck() == BST_CHECKED)
           tagSelect->tagType = ASSET_TAG;
	    else if (((CButton *)GetDlgItem(IDC_RADIO_INVENTORY))->GetCheck() == BST_CHECKED)
           tagSelect->tagType = INVENTORY_TAG;
	    else if (((CButton *)GetDlgItem(IDC_RADIO_FACTORY))->GetCheck() == BST_CHECKED)
           tagSelect->tagType = FACTORY_TAG;
	    else
	    {
           MessageBox("No Tag Type", "API", MB_ICONHAND);
		   delete tagSelect;
	       return;
	    }
	}
	else if (str == "RF_SELECT_TAG_TYPE")
	{
        tagSelect->selectType = RF_SELECT_TAG_TYPE;
		GetDlgItemText(IDC_COMB_TAG_TYPE, str);
	    if (str.IsEmpty())
	    {
           MessageBox("No Tag Type", "API", MB_ICONHAND);
		   delete tagSelect;
	       return;
	    }
	}
	else
	{
       MessageBox("No Tag Function Type", "API", MB_ICONHAND);
	   delete tagSelect;
       return;
	}
  
    if (((CButton *)GetDlgItem(IDC_RADIO_ACCESS))->GetCheck() == BST_CHECKED)
	   tagSelect->tagType = ACCESS_TAG;
	else if (((CButton *)GetDlgItem(IDC_RADIO_ASSET))->GetCheck() == BST_CHECKED)
       tagSelect->tagType = ASSET_TAG;
	else if (((CButton *)GetDlgItem(IDC_RADIO_INVENTORY))->GetCheck() == BST_CHECKED)
       tagSelect->tagType = INVENTORY_TAG;
	else if (((CButton *)GetDlgItem(IDC_RADIO_FACTORY))->GetCheck() == BST_CHECKED)
       tagSelect->tagType = FACTORY_TAG;
	else
	{
		if (tagSelect->selectType != RF_SELECT_FIELD)
		{
			MessageBox("No Tag Type", "API", MB_ICONHAND);
			delete tagSelect;
			return;
		}
	}

	int size = 0;
	unsigned int numTag = 0;

	UINT tagID = GetDlgItemInt(IDC_EDIT_TAG_ID_1, NULL, false);
	numTag++;

	tagSelect->tagList[0] = tagID;
	tagSelect->numTags = numTag;

	if(((CButton *)GetDlgItem(IDC_RADIO_Long_INTERVAL))->GetCheck() == BST_CHECKED)
		timeInt = true; //long
	else if(((CButton *)GetDlgItem(IDC_RADIO_SHORT_INTERVAL))->GetCheck() == BST_CHECKED)
		timeInt = false; //short
	else
	   timeInt = false; 
	
	int ret = rfGetTagLEDConfig(hostID, rdrID, 0, tagSelect, timeInt, cmdType, ++pktID);

	apiDlg->DisplayTag("rfGetTagLEDConfig() ", pktID, ret, tagID, str);
	delete tagSelect;
}

void CTagPage::OnBnClickedButtonSetTagLedConfig()
{
	char buf[64];
    unsigned short cmdType;
	bool timeInt;

	CString str;
	GetDlgItemText(IDC_COMB_SELECT_TYPE, str);
	if (str.IsEmpty())
	{
       MessageBox("No Function Type", "API", MB_ICONHAND);
	   return;
	}

	int rdrID = GetDlgItemInt(IDC_EDIT_RDR_ID, NULL,true);
    int hostID = GetDlgItemInt(IDC_EDIT_HOST_ID, NULL,true);

	GetDlgItemText(IDC_COMB_READER_FUNC_TYPE, buf, 50);
    if (strcmp(buf, "SPECIFIC_READER") == 0)
      cmdType = SPECIFIC_READER;
    else if (strcmp(buf, "ALL_READERS") == 0)
      cmdType = ALL_READERS;
    else
    {
	  MessageBox("No Reader Function Type", "API", MB_ICONHAND);
      return;
    }

	tagSelect = new rfTagSelect_t;
	GetDlgItemText(IDC_COMB_SELECT_TYPE, str);
	if (str == "RF_SELECT_FIELD")
	{
		tagSelect->selectType = RF_SELECT_FIELD;
	}
	else if (str == "RF_SELECT_TAG_ID")
	{
        tagSelect->selectType = RF_SELECT_TAG_ID;

		UINT tagID = GetDlgItemInt(IDC_EDIT_TAG_ID_1, NULL, false); 
	    if (tagID == 0)
	    {
           MessageBox("No Tag ID", "API", MB_ICONHAND);
		   delete tagSelect;
	       return;
	    }

		if (((CButton *)GetDlgItem(IDC_RADIO_ACCESS))->GetCheck() == BST_CHECKED)
	       tagSelect->tagType = ACCESS_TAG;
	    else if (((CButton *)GetDlgItem(IDC_RADIO_ASSET))->GetCheck() == BST_CHECKED)
           tagSelect->tagType = ASSET_TAG;
	    else if (((CButton *)GetDlgItem(IDC_RADIO_INVENTORY))->GetCheck() == BST_CHECKED)
           tagSelect->tagType = INVENTORY_TAG;
	    else if (((CButton *)GetDlgItem(IDC_RADIO_FACTORY))->GetCheck() == BST_CHECKED)
           tagSelect->tagType = FACTORY_TAG;
	    else
	    {
           MessageBox("No Tag Type", "API", MB_ICONHAND);
		   delete tagSelect;
	       return;
	    }
	}
	else if (str == "RF_SELECT_TAG_TYPE")
	{
        tagSelect->selectType = RF_SELECT_TAG_TYPE;
		GetDlgItemText(IDC_COMB_TAG_TYPE, str);
	    if (str.IsEmpty())
	    {
           MessageBox("No Tag Type", "API", MB_ICONHAND);
		   delete tagSelect;
	       return;
	    }
	}
	else
	{
       MessageBox("No Tag Function Type", "API", MB_ICONHAND);
	   delete tagSelect;
       return;
	}
  
    if (((CButton *)GetDlgItem(IDC_RADIO_ACCESS))->GetCheck() == BST_CHECKED)
	   tagSelect->tagType = ACCESS_TAG;
	else if (((CButton *)GetDlgItem(IDC_RADIO_ASSET))->GetCheck() == BST_CHECKED)
       tagSelect->tagType = ASSET_TAG;
	else if (((CButton *)GetDlgItem(IDC_RADIO_INVENTORY))->GetCheck() == BST_CHECKED)
       tagSelect->tagType = INVENTORY_TAG;
	else if (((CButton *)GetDlgItem(IDC_RADIO_FACTORY))->GetCheck() == BST_CHECKED)
       tagSelect->tagType = FACTORY_TAG;
	else
	{
        MessageBox("No Tag Type", "API", MB_ICONHAND);
		delete tagSelect;
	    return;
	}

	int size = 0;
	unsigned int numTag = 0;

	UINT tagID = GetDlgItemInt(IDC_EDIT_TAG_ID_1, NULL, false);
	numTag++;

	tagSelect->tagList[0] = tagID;
	tagSelect->numTags = numTag;

	if(((CButton *)GetDlgItem(IDC_RADIO_Long_INTERVAL))->GetCheck() == BST_CHECKED)
		timeInt = true; //long
	else if(((CButton *)GetDlgItem(IDC_RADIO_SHORT_INTERVAL))->GetCheck() == BST_CHECKED)
		timeInt = false; //short
	else
	   timeInt = false;

	UInt16 numFlash = GetDlgItemInt(IDC_EDIT_TAG_LED, NULL, false);
	
	int ret = rfSetTagLEDConfig(hostID, rdrID, 0, tagSelect, numFlash, timeInt, cmdType, ++pktID);

	apiDlg->DisplayTag("rfSetTagLEDConfig() ", pktID, ret, tagID, str);
	delete tagSelect;
}

/*void CTagPage::OnBnClickedButtonGetTagSpeakerConfig()
{
	char buf[64];
    unsigned short cmdType;
	bool timeInt;

	CString str;
	GetDlgItemText(IDC_COMB_READER_FUNC_TYPE, str);
	if (str.IsEmpty())
	{
       MessageBox("No Function Type", "API", MB_ICONHAND);
	   return;
	}

	int rdrID = GetDlgItemInt(IDC_EDIT_RDR_ID, NULL,true);
    int hostID = GetDlgItemInt(IDC_EDIT_HOST_ID, NULL,true);

	GetDlgItemText(IDC_COMB_READER_FUNC_TYPE, buf, 50);
    if (strcmp(buf, "SPECIFIC_READER") == 0)
      cmdType = SPECIFIC_READER;
    else if (strcmp(buf, "ALL_READERS") == 0)
      cmdType = ALL_READERS;
    else
    {
	  MessageBox("No Reader Function Type", "API", MB_ICONHAND);
      return;
    }

	tagSelect = new rfTagSelect_t;
	GetDlgItemText(IDC_COMB_SELECT_TYPE, str);
	if (str == "RF_SELECT_FIELD")
	{
		tagSelect->selectType = RF_SELECT_FIELD;
	}
	else if (str == "RF_SELECT_TAG_ID")
	{
        tagSelect->selectType = RF_SELECT_TAG_ID;

		UINT tagID = GetDlgItemInt(IDC_EDIT_TAG_ID_1, NULL, false); 
	    if (tagID == 0)
	    {
           MessageBox("No Tag ID", "API", MB_ICONHAND);
		   delete tagSelect;
	       return;
	    }

		if (((CButton *)GetDlgItem(IDC_RADIO_ACCESS))->GetCheck() == BST_CHECKED)
	       tagSelect->tagType = ACCESS_TAG;
	    else if (((CButton *)GetDlgItem(IDC_RADIO_ASSET))->GetCheck() == BST_CHECKED)
           tagSelect->tagType = ASSET_TAG;
	    else if (((CButton *)GetDlgItem(IDC_RADIO_INVENTORY))->GetCheck() == BST_CHECKED)
           tagSelect->tagType = INVENTORY_TAG;
	    else if (((CButton *)GetDlgItem(IDC_RADIO_FACTORY))->GetCheck() == BST_CHECKED)
           tagSelect->tagType = FACTORY_TAG;
	    else
	    {
           MessageBox("No Tag Type", "API", MB_ICONHAND);
		   delete tagSelect;
	       return;
	    }
	}
	else if (str == "RF_SELECT_TAG_TYPE")
	{
        tagSelect->selectType = RF_SELECT_TAG_TYPE;
		GetDlgItemText(IDC_COMB_TAG_TYPE, str);
	    if (str.IsEmpty())
	    {
           MessageBox("No Tag Type", "API", MB_ICONHAND);
		   delete tagSelect;
	       return;
	    }
	}
	else
	{
       MessageBox("No Tag Function Type", "API", MB_ICONHAND);
	   delete tagSelect;
       return;
	}
  
    if (((CButton *)GetDlgItem(IDC_RADIO_ACCESS))->GetCheck() == BST_CHECKED)
	       tagSelect->tagType = ACCESS_TAG;
	else if (((CButton *)GetDlgItem(IDC_RADIO_ASSET))->GetCheck() == BST_CHECKED)
           tagSelect->tagType = ASSET_TAG;
	else if (((CButton *)GetDlgItem(IDC_RADIO_INVENTORY))->GetCheck() == BST_CHECKED)
           tagSelect->tagType = INVENTORY_TAG;
	else if (((CButton *)GetDlgItem(IDC_RADIO_FACTORY))->GetCheck() == BST_CHECKED)
           tagSelect->tagType = FACTORY_TAG;
	else
	{
        MessageBox("No Tag Type", "API", MB_ICONHAND);
		delete tagSelect;
	    return;
	}

	int size = 0;
	unsigned int numTag = 0;

	UINT tagID = GetDlgItemInt(IDC_EDIT_TAG_ID_1, NULL, false);
	numTag++;

	tagSelect->tagList[0] = tagID;
	tagSelect->numTags = numTag;

	if(((CButton *)GetDlgItem(IDC_RADIO_Long_INTERVAL))->GetCheck() == BST_CHECKED)
		timeInt = true; //long
	else if(((CButton *)GetDlgItem(IDC_RADIO_SHORT_INTERVAL))->GetCheck() == BST_CHECKED)
		timeInt = false; //short
	else
	   timeInt = false; 
	
	pktID++;
	int ret = rfGetTagSpeakerConfig(hostID, rdrID, 0, tagSelect, timeInt, cmdType, pktID);

	apiDlg->DisplayTag("rfGetTagSpeakerConfig() ", pktID, ret, tagID, str);
	delete tagSelect;
}*/

//void CTagPage::OnBnClickedButtonSetTagSpeakerConfig()
//{
//	char buf[64];
//    unsigned short cmdType;
//	bool timeInt;
//
//	CString str;
//	GetDlgItemText(IDC_COMB_READER_FUNC_TYPE, str);
//	if (str.IsEmpty())
//	{
//       MessageBox("No Function Type", "API", MB_ICONHAND);
//	   return;
//	}
//
//	int rdrID = GetDlgItemInt(IDC_EDIT_RDR_ID, NULL,true);
//    int hostID = GetDlgItemInt(IDC_EDIT_HOST_ID, NULL,true);
//
//	GetDlgItemText(IDC_COMB_READER_FUNC_TYPE, buf, 50);
//    if (strcmp(buf, "SPECIFIC_READER") == 0)
//      cmdType = SPECIFIC_READER;
//    else if (strcmp(buf, "ALL_READERS") == 0)
//      cmdType = ALL_READERS;
//    else
//    {
//	  MessageBox("No Reader Function Type", "API", MB_ICONHAND);
//      return;
//    }
//
//	tagSelect = new rfTagSelect_t;
//	GetDlgItemText(IDC_COMB_SELECT_TYPE, str);
//	if (str == "RF_SELECT_FIELD")
//	{
//		tagSelect->selectType = RF_SELECT_FIELD;
//	}
//	else if (str == "RF_SELECT_TAG_ID")
//	{
//        tagSelect->selectType = RF_SELECT_TAG_ID;
//
//		UINT tagID = GetDlgItemInt(IDC_EDIT_TAG_ID_1, NULL, false); 
//	    if (tagID == 0)
//	    {
//           MessageBox("No Tag ID", "API", MB_ICONHAND);
//		   delete tagSelect;
//	       return;
//	    }
//
//		if (((CButton *)GetDlgItem(IDC_RADIO_ACCESS))->GetCheck() == BST_CHECKED)
//	       tagSelect->tagType = ACCESS_TAG;
//	    else if (((CButton *)GetDlgItem(IDC_RADIO_ASSET))->GetCheck() == BST_CHECKED)
//           tagSelect->tagType = ASSET_TAG;
//	    else if (((CButton *)GetDlgItem(IDC_RADIO_INVENTORY))->GetCheck() == BST_CHECKED)
//           tagSelect->tagType = INVENTORY_TAG;
//	    else if (((CButton *)GetDlgItem(IDC_RADIO_FACTORY))->GetCheck() == BST_CHECKED)
//           tagSelect->tagType = FACTORY_TAG;
//	    else
//	    {
//           MessageBox("No Tag Type", "API", MB_ICONHAND);
//		   delete tagSelect;
//	       return;
//	    }
//	}
//	else if (str == "RF_SELECT_TAG_TYPE")
//	{
//        tagSelect->selectType = RF_SELECT_TAG_TYPE;
//		GetDlgItemText(IDC_COMB_TAG_TYPE, str);
//	    if (str.IsEmpty())
//	    {
//           MessageBox("No Tag Type", "API", MB_ICONHAND);
//		   delete tagSelect;
//	       return;
//	    }
//	}
//	else
//	{
//       MessageBox("No Tag Function Type", "API", MB_ICONHAND);
//	   delete tagSelect;
//       return;
//	}
//  
//    if (((CButton *)GetDlgItem(IDC_RADIO_ACCESS))->GetCheck() == BST_CHECKED)
//	       tagSelect->tagType = ACCESS_TAG;
//	else if (((CButton *)GetDlgItem(IDC_RADIO_ASSET))->GetCheck() == BST_CHECKED)
//           tagSelect->tagType = ASSET_TAG;
//	else if (((CButton *)GetDlgItem(IDC_RADIO_INVENTORY))->GetCheck() == BST_CHECKED)
//           tagSelect->tagType = INVENTORY_TAG;
//	else if (((CButton *)GetDlgItem(IDC_RADIO_FACTORY))->GetCheck() == BST_CHECKED)
//           tagSelect->tagType = FACTORY_TAG;
//	else
//	{
//        MessageBox("No Tag Type", "API", MB_ICONHAND);
//		delete tagSelect;
//	    return;
//	}
//
//	int size = 0;
//	unsigned int numTag = 0;
//
//	UINT tagID = GetDlgItemInt(IDC_EDIT_TAG_ID_1, NULL, false);
//	numTag++;
//
//	tagSelect->tagList[0] = tagID;
//	tagSelect->numTags = numTag;
//
//	if(((CButton *)GetDlgItem(IDC_RADIO_Long_INTERVAL))->GetCheck() == BST_CHECKED)
//		timeInt = true; //long
//	else if(((CButton *)GetDlgItem(IDC_RADIO_SHORT_INTERVAL))->GetCheck() == BST_CHECKED)
//		timeInt = false; //short
//	else
//	   timeInt = false;
//
//	UInt16 numBeep = GetDlgItemInt(IDC_EDIT_TAG_SPEAKER, NULL, false);
//	
//	pktID++;
//	int ret = rfSetTagSpeakerConfig(hostID, rdrID, 0, tagSelect, numBeep, timeInt, cmdType, pktID);
//
//	apiDlg->DisplayTag("rfSetTagSpeakerConfig() ", pktID, ret, tagID, str);
//	delete tagSelect;
//}

void CTagPage::OnChildActivate()
{
	CDialog::OnChildActivate();

	((CComboBox *)GetDlgItem(IDC_COMB_READER_FUNC_TYPE))->SetCurSel(0);
	((CComboBox *)GetDlgItem(IDC_COMB_SELECT_TYPE))->SetCurSel(0);
}

void CTagPage::OnBnClickedButtonSetTempConfig()
{
	char buf[64];
    unsigned short cmdType;
	bool setTxTimeInt;
	bool timeInt;

	//CTemperatureDlg tempDlg;
	//if (tempDlg.DoModal() == IDCANCEL)
		//return;

    if(((CButton *)GetDlgItem(IDC_CHECK_REPORT_TEMP_UNDER))->GetCheck())
		tagTemp->rptUnderLowerLimit = true;
	else
		tagTemp->rptUnderLowerLimit = false;

	if(((CButton *)GetDlgItem(IDC_CHECK_REPORT_TEMP_UPPER))->GetCheck())
		tagTemp->rptOverUpperLimit = true;
	else
		tagTemp->rptOverUpperLimit = false;

	if(((CButton *)GetDlgItem(IDC_CHECK_REPORT_TEMP_PERIODIC))->GetCheck())
		tagTemp->rptPeriodicRead = true;
	else
		tagTemp->rptPeriodicRead = false;

	tagTemp->numReadAve = GetDlgItemInt(IDC_COMBO_NUM_READ_AVE, NULL, false);

	tagTemp->periodicRptTime = GetDlgItemInt(IDC_EDIT_PERIODIC_REPORT_TIME, NULL, false);
	   
	if(((CButton *)GetDlgItem(IDC_RADIO_TIME_HOUR))->GetCheck())
		tagTemp->periodicTimeType = RF_TIME_HOUR;
	else
		tagTemp->periodicTimeType = RF_TIME_MINUTE;

	if(((CButton *)GetDlgItem(IDC_CHECK_TAG_TEMP_LOGGING))->GetCheck())
		tagTemp->enableTempLogging = true;
	else
		tagTemp->enableTempLogging = false; 

	if(((CButton *)GetDlgItem(IDC_CHECK_WARP_AROUND))->GetCheck())
		tagTemp->wrapAround = true;
	else
		tagTemp->wrapAround = false; 

	GetDlgItemText(IDC_EDIT_UP_LIMIT_TEMP, buf, 10);
	tagTemp->upperLimitTemp = (float)atof(buf);
	GetDlgItemText(IDC_EDIT_LOW_LIMIT_TEMP, buf, 10);
	tagTemp->lowerLimitTemp = (float)atof(buf);
	/////////////////////////////////////////////////////

	CString str;
	GetDlgItemText(IDC_COMB_SELECT_TYPE, str);
	if (str.IsEmpty())
	{
		MessageBox("No Tag Function Type", "API", MB_ICONHAND);
		return;
	}

	GetDlgItemText(IDC_COMB_READER_FUNC_TYPE, buf, 50);
    if (strcmp(buf, "SPECIFIC_READER") == 0)
		cmdType = SPECIFIC_READER;
    else if (strcmp(buf, "ALL_READERS") == 0)
		cmdType = ALL_READERS;
    else
    {
		MessageBox("No Reader Function Type", "API", MB_ICONHAND);
		return;
    }

		tagSelect = new rfTagSelect_t;
		if (str == "RF_SELECT_FIELD")
		{
				tagSelect->selectType = RF_SELECT_FIELD;
		}
		else if (str == "RF_SELECT_TAG_TYPE")
		{
				tagSelect->selectType = RF_SELECT_TAG_TYPE;
				GetDlgItemText(IDC_COMB_TAG_TYPE, str);
				if (str.IsEmpty())
				{
					MessageBox("No Tag Type", "API", MB_ICONHAND);
					delete tagSelect;
					return;
				}
	  }
	  else if ((str == "RF_SELECT_TAG_ID") || (str == "RF_SELECT_TAG_RANGE"))
	  {
		   if (str == "RF_SELECT_TAG_RANGE")
		   {
				tagSelect->selectType = RF_SELECT_TAG_RANGE;
				tagSelect->rangeIndex = (Byte)GetDlgItemInt(IDC_COMB_ID_RANGE, NULL, true);
		  }
		  else
			  tagSelect->selectType = RF_SELECT_TAG_ID;
	}
	else  
	{
       MessageBox("No Tag Function Type", "API", MB_ICONHAND);
	     delete tagSelect;
       return;
	}
  
	if (((CButton *)GetDlgItem(IDC_RADIO_ACCESS))->GetCheck() == BST_CHECKED)
			tagSelect->tagType = ACCESS_TAG;
	else if (((CButton *)GetDlgItem(IDC_RADIO_ASSET))->GetCheck() == BST_CHECKED)
			tagSelect->tagType = ASSET_TAG;
	else if (((CButton *)GetDlgItem(IDC_RADIO_INVENTORY))->GetCheck() == BST_CHECKED)
			tagSelect->tagType = INVENTORY_TAG;
	else if (((CButton *)GetDlgItem(IDC_RADIO_FACTORY))->GetCheck() == BST_CHECKED)
			tagSelect->tagType = FACTORY_TAG;
	else
	{
			MessageBox("No Tag Type", "API", MB_ICONHAND);
			delete tagSelect;
			return;
	}

	int size = 0;
	unsigned int numTag = 0;

	UINT tagID = GetDlgItemInt(IDC_EDIT_TAG_ID_1, NULL, false); 
	if (tagID == 0)
	{
			MessageBox("No Tag ID", "API", MB_ICONHAND);
			delete tagSelect;
			return;
	}
	else
	{
			tagSelect->tagList[0] = tagID;
			numTag++;
	}
	tagSelect->numTags = numTag;

	int rdrID = GetDlgItemInt(IDC_EDIT_RDR_ID, NULL,true);
    int hostID = GetDlgItemInt(IDC_EDIT_HOST_ID, NULL,true);
	
	if(((CButton *)GetDlgItem(IDC_RADIO_Long_INTERVAL))->GetCheck() == BST_CHECKED)
	{
			setTxTimeInt = true;
			timeInt = true; //long
	}
	else if(((CButton *)GetDlgItem(IDC_RADIO_SHORT_INTERVAL))->GetCheck() == BST_CHECKED)
	{
			setTxTimeInt = true;
			timeInt = false; //short
	}
	else
	{
			setTxTimeInt = false;
			timeInt = false; 
	}

	int ret = rfSetTagTempConfig(hostID, rdrID, 0, tagSelect, tagTemp, setTxTimeInt, timeInt, cmdType, ++pktID);

	apiDlg->Display("rfSetTagTempConfig()", pktID, ret);
	delete tagSelect;
}

void CTagPage::OnBnClickedButtonGetTagTemp()
{
		char buf[64];
		unsigned short cmdType;
		bool setTxTimeInt;
		bool timeInt;

		CString str;
		GetDlgItemText(IDC_COMB_SELECT_TYPE, str);
		if (str.IsEmpty())
		{
				MessageBox("No Tag Function Type", "API", MB_ICONHAND);
			  return;
		}

	  GetDlgItemText(IDC_COMB_READER_FUNC_TYPE, buf, 50);
    if (strcmp(buf, "SPECIFIC_READER") == 0)
				cmdType = SPECIFIC_READER;
    else if (strcmp(buf, "ALL_READERS") == 0)
				cmdType = ALL_READERS;
    else
    {
				MessageBox("No Reader Function Type", "API", MB_ICONHAND);
				return;
    }

		tagSelect = new rfTagSelect_t;
		if (str == "RF_SELECT_FIELD")
		{
				tagSelect->selectType = RF_SELECT_FIELD;
		}
		else if (str == "RF_SELECT_TAG_TYPE")
		{
				tagSelect->selectType = RF_SELECT_TAG_TYPE;
				GetDlgItemText(IDC_COMB_TAG_TYPE, str);
				if (str.IsEmpty())
				{
					MessageBox("No Tag Type", "API", MB_ICONHAND);
					delete tagSelect;
					return;
				}
		}
	  else if ((str == "RF_SELECT_TAG_ID") || (str == "RF_SELECT_TAG_RANGE"))
		{
		   if (str == "RF_SELECT_TAG_RANGE")
       {
					tagSelect->selectType = RF_SELECT_TAG_RANGE;
          tagSelect->rangeIndex = (Byte)GetDlgItemInt(IDC_COMB_ID_RANGE, NULL, true);
       }
			 else
					 tagSelect->selectType = RF_SELECT_TAG_ID;
		}
		else  
		{
       MessageBox("No Tag Function Type", "API", MB_ICONHAND);
	     delete tagSelect;
       return;
		}
  
		if (((CButton *)GetDlgItem(IDC_RADIO_ACCESS))->GetCheck() == BST_CHECKED)
				tagSelect->tagType = ACCESS_TAG;
		else if (((CButton *)GetDlgItem(IDC_RADIO_ASSET))->GetCheck() == BST_CHECKED)
				tagSelect->tagType = ASSET_TAG;
		else if (((CButton *)GetDlgItem(IDC_RADIO_INVENTORY))->GetCheck() == BST_CHECKED)
				tagSelect->tagType = INVENTORY_TAG;
		else if (((CButton *)GetDlgItem(IDC_RADIO_FACTORY))->GetCheck() == BST_CHECKED)
				tagSelect->tagType = FACTORY_TAG;
		else
		{
				MessageBox("No Tag Type", "API", MB_ICONHAND);
				delete tagSelect;
				return;
		}

		int size = 0;
		unsigned int numTag = 0;

		UINT tagID = GetDlgItemInt(IDC_EDIT_TAG_ID_1, NULL, false); 
		if (tagID == 0)
		{
				MessageBox("No Tag ID", "API", MB_ICONHAND);
				delete tagSelect;
				return;
		}
		else
		{
				tagSelect->tagList[0] = tagID;
				numTag++;
		}
		tagSelect->numTags = numTag;

		int rdrID = GetDlgItemInt(IDC_EDIT_RDR_ID, NULL,true);
    int hostID = GetDlgItemInt(IDC_EDIT_HOST_ID, NULL,true);
	

    if(((CButton *)GetDlgItem(IDC_RADIO_Long_INTERVAL))->GetCheck() == BST_CHECKED)
		{
				setTxTimeInt = true;
				timeInt = true; //long
		}
		else if(((CButton *)GetDlgItem(IDC_RADIO_SHORT_INTERVAL))->GetCheck() == BST_CHECKED)
		{
				setTxTimeInt = true;
				timeInt = false; //short
		}
		else
		{
				setTxTimeInt = false;
				timeInt = false; 
		}

		int ret = rfGetTagTemp(hostID, rdrID, 0, tagSelect, setTxTimeInt, timeInt, cmdType, ++pktID);

		apiDlg->Display("rfGetTagTemp()", pktID, ret);
		delete tagSelect;
}

void CTagPage::OnBnClickedButtonReadTag()
{
		char buf[64];
        unsigned short cmdType;
		CString sAddr;
		unsigned int numTag = 0;

		CString str;
		GetDlgItemText(IDC_COMB_SELECT_TYPE, str);
		if (str.IsEmpty())
		{
				MessageBox("No Function Type", "API", MB_ICONHAND);
				return;
		}

		int rdrID = GetDlgItemInt(IDC_EDIT_RDR_ID, NULL,true);
        int hostID = GetDlgItemInt(IDC_EDIT_HOST_ID, NULL,true);
		int dataLen = GetDlgItemInt(IDC_EDIT_MEM_DATA_LEN_READ, NULL,true);
		GetDlgItemText(IDC_EDIT_MEM_START_ADDR_READ, sAddr);
		int address = HexToInt(sAddr.GetBuffer(), sAddr.GetLength());
        sAddr.ReleaseBuffer();

		GetDlgItemText(IDC_COMB_READER_FUNC_TYPE, buf, 50);
        if (strcmp(buf, "SPECIFIC_READER") == 0)
           cmdType = SPECIFIC_READER;
        else if (strcmp(buf, "ALL_READERS") == 0)
          cmdType = ALL_READERS;
    else
    {
	  MessageBox("No Reader Function Type", "API", MB_ICONHAND);
      return;
    }

		tagSelect = new rfTagSelect_t;
		memset(tagSelect, 0, sizeof(rfTagSelect_t));

		if (str == "RF_SELECT_FIELD")
		{
				tagSelect->selectType = RF_SELECT_FIELD;
				int ret = rfReadTags(hostID, rdrID, 0, tagSelect, address, dataLen, false, false, cmdType, ++pktID);
				apiDlg->Display("rfReadTags() ALL ", pktID, ret);
				delete tagSelect;
				return;
		}
		else if (str == "RF_SELECT_TAG_TYPE")
		{
             tagSelect->selectType = RF_SELECT_TAG_TYPE;
			 //GetDlgItemText(IDC_COMB_TAG_TYPE, str);
	         //if (str.IsEmpty())
	         //{
                  //MessageBox("No Tag Type", "API", MB_ICONHAND);
				  //delete tagSelect;
	              //return;
	         //}
		}
	    else if ((str == "RF_SELECT_TAG_ID") || (str == "RF_SELECT_TAG_RANGE"))
	    {
		   if (str == "RF_SELECT_TAG_RANGE")
           {
			  tagSelect->selectType = RF_SELECT_TAG_RANGE;
              tagSelect->rangeIndex = (Byte)GetDlgItemInt(IDC_COMB_ID_RANGE, NULL, true);
           }
		   else
			  tagSelect->selectType = RF_SELECT_TAG_ID;

		   UINT tagID = GetDlgItemInt(IDC_EDIT_TAG_ID_1, NULL, false); 
		   if (tagID == 0)
		   {
			  MessageBox("No Tag ID", "API", MB_ICONHAND);
			  delete tagSelect;
			  return;
		   }
		   else
		   {
			  tagSelect->tagList[0] = tagID;
			  numTag++;
		   }

		   tagSelect->numTags = numTag;

	  }
	  else  
	  {
         MessageBox("No Tag Function Type", "API", MB_ICONHAND);
	     delete tagSelect;
         return;
	  }

		if (((CButton *)GetDlgItem(IDC_RADIO_ACCESS))->GetCheck() == BST_CHECKED)
				tagSelect->tagType = ACCESS_TAG;
		else if (((CButton *)GetDlgItem(IDC_RADIO_ASSET))->GetCheck() == BST_CHECKED)
				tagSelect->tagType = ASSET_TAG;
		else if (((CButton *)GetDlgItem(IDC_RADIO_INVENTORY))->GetCheck() == BST_CHECKED)
				tagSelect->tagType = INVENTORY_TAG;
		else if (((CButton *)GetDlgItem(IDC_RADIO_FACTORY))->GetCheck() == BST_CHECKED)
				tagSelect->tagType = FACTORY_TAG;
		else
		{
			MessageBox("No Tag Type", "API", MB_ICONHAND);
			delete tagSelect;
			return;
		}

		int size = 0;
		/*unsigned int numTag = 0;

		UINT tagID = GetDlgItemInt(IDC_EDIT_TAG_ID_1, NULL, false); 
		if (tagID == 0)
		{
			 MessageBox("No Tag ID", "API", MB_ICONHAND);
			 delete tagSelect;
			 return;
		}
		else
		{
			 tagSelect->tagList[0] = tagID;
			 numTag++;
		}

		tagSelect->numTags = numTag;*/
		
		int ret = rfReadTags(hostID, rdrID, 0, tagSelect, address, dataLen, false, false, cmdType, ++pktID);
		apiDlg->Display("rfReadTags()", pktID, ret);
		delete tagSelect;
}

void CTagPage::OnBnClickedButtonWriteTag()
{
		char buf[64];
		char data[15];
    unsigned short cmdType;
		CString sAddr;

		CString str;
		GetDlgItemText(IDC_COMB_SELECT_TYPE, str);
		if (str.IsEmpty())
		{
				 MessageBox("No Function Type", "API", MB_ICONHAND);
				return;
		}

		memset(data, 0, sizeof(char));
		int rdrID = GetDlgItemInt(IDC_EDIT_RDR_ID, NULL,true);
    int hostID = GetDlgItemInt(IDC_EDIT_HOST_ID, NULL,true);
		int dataLen = GetDlgItemInt(IDC_EDIT_MEM_DATA_LEN_WRITE, NULL,true);
		GetDlgItemText(IDC_EDIT_MEM_START_ADDR_WRITE, sAddr);
		int address = HexToInt(sAddr.GetBuffer(), sAddr.GetLength());
		sAddr.ReleaseBuffer();
		GetDlgItemText(IDC_EDIT_MEM_DATA, data, dataLen+1);
	
		GetDlgItemText(IDC_COMB_READER_FUNC_TYPE, buf, 50);
    if (strcmp(buf, "SPECIFIC_READER") == 0)
				cmdType = SPECIFIC_READER;
    else if (strcmp(buf, "ALL_READERS") == 0)
			 cmdType = ALL_READERS;
    else
    {
				MessageBox("No Reader Function Type", "API", MB_ICONHAND);
				return;
    }

    tagSelect = new rfTagSelect_t;
    memset(tagSelect, 0, sizeof(rfTagSelect_t));

		if (str == "RF_SELECT_FIELD")
		{
				tagSelect->selectType = RF_SELECT_FIELD;
				int ret = rfWriteTags(hostID, rdrID, 0, tagSelect, address, dataLen, (Byte*)data, false, false, cmdType, ++pktID);
				apiDlg->Display("rfWriteTags() ALL ", pktID, ret);
				delete tagSelect;
				return;
		}
	  else if (str == "RF_SELECT_TAG_TYPE")
		{
       tagSelect->selectType = RF_SELECT_TAG_TYPE;
			 GetDlgItemText(IDC_COMB_TAG_TYPE, str);
	     if (str.IsEmpty())
	     {
          MessageBox("No Tag Type", "API", MB_ICONHAND);
					delete tagSelect;
	        return;
	     }
		}
	  else if ((str == "RF_SELECT_TAG_ID") || (str == "RF_SELECT_TAG_RANGE"))
		{
		   if (str == "RF_SELECT_TAG_RANGE")
       {
					tagSelect->selectType = RF_SELECT_TAG_RANGE;
          tagSelect->rangeIndex = (Byte)GetDlgItemInt(IDC_COMB_ID_RANGE, NULL, true);
       }
			 else
					 tagSelect->selectType = RF_SELECT_TAG_ID;
		}
		else  
		{
       MessageBox("No Tag Function Type", "API", MB_ICONHAND);
	     delete tagSelect;
       return;
		}
		
		if (((CButton *)GetDlgItem(IDC_RADIO_ACCESS))->GetCheck() == BST_CHECKED)
	       tagSelect->tagType = ACCESS_TAG;
		else if (((CButton *)GetDlgItem(IDC_RADIO_ASSET))->GetCheck() == BST_CHECKED)
        tagSelect->tagType = ASSET_TAG;
		else if (((CButton *)GetDlgItem(IDC_RADIO_INVENTORY))->GetCheck() == BST_CHECKED)
        tagSelect->tagType = INVENTORY_TAG;
		else if (((CButton *)GetDlgItem(IDC_RADIO_FACTORY))->GetCheck() == BST_CHECKED)
        tagSelect->tagType = FACTORY_TAG;
		else
		{
        MessageBox("No Tag Type", "API", MB_ICONHAND);
				delete tagSelect;
				return;
		}

		int size = 0;
		unsigned int numTag = 0;

		UINT tagID = GetDlgItemInt(IDC_EDIT_TAG_ID_1, NULL, false); 
		if (tagID == 0)
		{
       MessageBox("No Tag ID", "API", MB_ICONHAND);
			 delete tagSelect;
			 return;
		}
		else
		{
			 tagSelect->tagList[0] = tagID;
			 numTag++;
		}

		tagSelect->numTags = numTag;
	
		int ret = rfWriteTags(hostID, rdrID, 0, tagSelect, address, dataLen, (Byte*)data, false, false, cmdType, ++pktID);
		apiDlg->Display("rfWriteTags()", pktID, ret);
		delete tagSelect;
}

//void CTagPage::OnBnClickedButtonConfigTag()
//{
//    /*rfConfigureTags(UInt16 host,
//                    UInt16 reader,
//                    UInt16 repeater,
//                    rfTagSelect_t* tagSelect,
//                    rfTagInfo_t* tagInfo,
//					UInt16 configType,
//			        Boolean setTxTimeInterval,
//					Boolean timeInterval,
//                    UInt16 cmdType,
//                    UInt16 pktID)*/
//}

/*void CTagPage::OnBnClickedCheckAnyId()
{
	if (((CButton *)GetDlgItem(IDC_CHECK_ANY_ID))->GetCheck() == BST_CHECKED)
      ((CWnd *)GetDlgItem(IDC_EDIT_TAG_ID_1))->EnableWindow(false);
	else
	  ((CWnd *)GetDlgItem(IDC_EDIT_TAG_ID_1))->EnableWindow(true);
}*/

//void CTagPage::OnBnClickedCheckModifyTifgc()
//{
//	if (((CButton *)GetDlgItem(IDC_CHECK_MODIFY_TIFGC))->GetCheck() == BST_CHECKED)
//	{
//	   ((CWnd *)GetDlgItem(IDC_GROUP_TIFGC))->EnableWindow(true);
//     //?((CWnd *)GetDlgItem(IDC_EDIT_NEW_FIELD_TIME))->EnableWindow(true);
//     ((CWnd *)GetDlgItem(IDC_STATIC_CFG_03))->EnableWindow(true);
//	   //?((CWnd *)GetDlgItem(IDC_EDIT_NEW_GROUP_CT))->EnableWindow(true);
//     //?((CWnd *)GetDlgItem(IDC_STATIC_CFG_04))->EnableWindow(true);
//
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_ID))->EnableWindow(false);
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_TYPE))->EnableWindow(false); 
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_SEND_TIME))->EnableWindow(false);
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_RPT_TAMPER))->EnableWindow(false);
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_FACTORY))->EnableWindow(false); 
//	}
//	else
//	{
//	   ((CWnd *)GetDlgItem(IDC_GROUP_TIFGC))->EnableWindow(false);
//       //?((CWnd *)GetDlgItem(IDC_EDIT_NEW_FIELD_TIME))->EnableWindow(false);
//       ((CWnd *)GetDlgItem(IDC_STATIC_CFG_03))->EnableWindow(false);
//	     //?((CWnd *)GetDlgItem(IDC_EDIT_NEW_GROUP_CT))->EnableWindow(false);
//       //?((CWnd *)GetDlgItem(IDC_STATIC_CFG_04))->EnableWindow(false);
//
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_ID))->EnableWindow(true);
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_TYPE))->EnableWindow(true); 
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_SEND_TIME))->EnableWindow(true);
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_RPT_TAMPER))->EnableWindow(true);
//	   ((CWnd *)GetDlgItem(IDC_CHECK_MODIFY_FACTORY))->EnableWindow(true); 
//	}
//}

BOOL CTagPage::OnInitDialog()
	{
	CDialog::OnInitDialog();

	// TODO:  Add extra initialization here
	((CButton *)GetDlgItem(IDC_RADIO_ANY_TYPE))->SetCheck(1);
  
	return TRUE;  // return TRUE unless you set the focus to a control
	// EXCEPTION: OCX Property Pages should return FALSE
	}

//	void CTagPage::OnBnHotItemChangeButtonWriteTag(NMHDR *pNMHDR, LRESULT *pResult)
//	{
//		// This feature requires Internet Explorer 6 or greater.
//		// The symbol _WIN32_IE must be >= 0x0600.
//		LPNMBCHOTITEM pHotItem = reinterpret_cast<LPNMBCHOTITEM>(pNMHDR);
//		// TODO: Add your control notification handler code here
//		*pResult = 0;
//	}

	void CTagPage::OnBnClickedButtonSetTempLogTimestamp()
	{
		char buf[64];
        unsigned short cmdType;

		CString str;
		GetDlgItemText(IDC_COMB_SELECT_TYPE, str);
		if (str.IsEmpty())
		{
			MessageBox("No Tag Function Type", "API", MB_ICONHAND);
			return;
		}

		GetDlgItemText(IDC_COMB_READER_FUNC_TYPE, buf, 50);
		if (strcmp(buf, "SPECIFIC_READER") == 0)
		cmdType = SPECIFIC_READER;
		else if (strcmp(buf, "ALL_READERS") == 0)
			cmdType = ALL_READERS;
		else
		{
			MessageBox("No Reader Function Type", "API", MB_ICONHAND);
			return;
		}

		tagSelect = new rfTagSelect_t;

		if (str == "RF_SELECT_FIELD")
		{
			tagSelect->selectType = RF_SELECT_FIELD;
		}
		else if (str == "RF_SELECT_TAG_TYPE")
		{
		tagSelect->selectType = RF_SELECT_TAG_TYPE;
		GetDlgItemText(IDC_COMB_TAG_TYPE, str);
		if (str.IsEmpty())
		{
			MessageBox("No Tag Type", "API", MB_ICONHAND);
			delete tagSelect;
			return;
		}
		}
		else if ((str == "RF_SELECT_TAG_ID") || (str == "RF_SELECT_TAG_RANGE"))
		{
		if (str == "RF_SELECT_TAG_RANGE")
		{
			tagSelect->selectType = RF_SELECT_TAG_RANGE;
			tagSelect->rangeIndex = (Byte)GetDlgItemInt(IDC_COMB_ID_RANGE, NULL, true);
		}
		else
			tagSelect->selectType = RF_SELECT_TAG_ID;
		}
		else  
		{
		MessageBox("No Tag Function Type", "API", MB_ICONHAND);
		delete tagSelect;
		return;
		}
	  
		if (((CButton *)GetDlgItem(IDC_RADIO_ACCESS))->GetCheck() == BST_CHECKED)
				tagSelect->tagType = ACCESS_TAG;
		else if (((CButton *)GetDlgItem(IDC_RADIO_ASSET))->GetCheck() == BST_CHECKED)
				tagSelect->tagType = ASSET_TAG;
		else if (((CButton *)GetDlgItem(IDC_RADIO_INVENTORY))->GetCheck() == BST_CHECKED)
				tagSelect->tagType = INVENTORY_TAG;
		else if (((CButton *)GetDlgItem(IDC_RADIO_FACTORY))->GetCheck() == BST_CHECKED)
				tagSelect->tagType = FACTORY_TAG;
		else
		{
			MessageBox("No Tag Type", "API", MB_ICONHAND);
			delete tagSelect;
			return;
		}

		int size = 0;
		unsigned int numTag = 0;

		UINT tagID = GetDlgItemInt(IDC_EDIT_TAG_ID_1, NULL, false); 
		if (tagID == 0)
		{
			MessageBox("No Tag ID", "API", MB_ICONHAND);
			delete tagSelect;
			return;
		}
		else
		{
			tagSelect->tagList[0] = tagID;
			numTag++;
		}
		tagSelect->numTags = numTag;

		int rdrID = GetDlgItemInt(IDC_EDIT_RDR_ID, NULL,true);
		int hostID = GetDlgItemInt(IDC_EDIT_HOST_ID, NULL,true);
		
		bool interval = false; 
		if(((CButton *)GetDlgItem(IDC_RADIO_Long_INTERVAL))->GetCheck() == BST_CHECKED)
			interval = true;
		int ret = rfSetTagTempLogTimestamp(hostID, rdrID, 0, tagSelect, false, false, cmdType, ++pktID);

		apiDlg->Display("rfSetTempLogTimestamp()", pktID, ret);
		delete tagSelect;
	}

	void CTagPage::OnBnClickedButtonGetTempLogTimestamp2()
	{
		char buf[64];
        unsigned short cmdType;

		CString str;
		GetDlgItemText(IDC_COMB_SELECT_TYPE, str);
		if (str.IsEmpty())
		{
			MessageBox("No Tag Function Type", "API", MB_ICONHAND);
			return;
		}

		GetDlgItemText(IDC_COMB_READER_FUNC_TYPE, buf, 50);
		if (strcmp(buf, "SPECIFIC_READER") == 0)
		cmdType = SPECIFIC_READER;
		else if (strcmp(buf, "ALL_READERS") == 0)
			cmdType = ALL_READERS;
		else
		{
			MessageBox("No Reader Function Type", "API", MB_ICONHAND);
			return;
		}

		tagSelect = new rfTagSelect_t;

		if (str == "RF_SELECT_FIELD")
		{
			tagSelect->selectType = RF_SELECT_FIELD;
		}
		else if (str == "RF_SELECT_TAG_TYPE")
		{
		tagSelect->selectType = RF_SELECT_TAG_TYPE;
		GetDlgItemText(IDC_COMB_TAG_TYPE, str);
		if (str.IsEmpty())
		{
			MessageBox("No Tag Type", "API", MB_ICONHAND);
			delete tagSelect;
			return;
		}
		}
		else if ((str == "RF_SELECT_TAG_ID") || (str == "RF_SELECT_TAG_RANGE"))
		{
		if (str == "RF_SELECT_TAG_RANGE")
		{
			tagSelect->selectType = RF_SELECT_TAG_RANGE;
			tagSelect->rangeIndex = (Byte)GetDlgItemInt(IDC_COMB_ID_RANGE, NULL, true);
		}
		else
			tagSelect->selectType = RF_SELECT_TAG_ID;
		}
		else  
		{
		MessageBox("No Tag Function Type", "API", MB_ICONHAND);
		delete tagSelect;
		return;
		}
	  
		if (((CButton *)GetDlgItem(IDC_RADIO_ACCESS))->GetCheck() == BST_CHECKED)
				tagSelect->tagType = ACCESS_TAG;
		else if (((CButton *)GetDlgItem(IDC_RADIO_ASSET))->GetCheck() == BST_CHECKED)
				tagSelect->tagType = ASSET_TAG;
		else if (((CButton *)GetDlgItem(IDC_RADIO_INVENTORY))->GetCheck() == BST_CHECKED)
				tagSelect->tagType = INVENTORY_TAG;
		else if (((CButton *)GetDlgItem(IDC_RADIO_FACTORY))->GetCheck() == BST_CHECKED)
				tagSelect->tagType = FACTORY_TAG;
		else
		{
			MessageBox("No Tag Type", "API", MB_ICONHAND);
			delete tagSelect;
			return;
		}

		int size = 0;
		unsigned int numTag = 0;

		UINT tagID = GetDlgItemInt(IDC_EDIT_TAG_ID_1, NULL, false); 
		if (tagID == 0)
		{
			MessageBox("No Tag ID", "API", MB_ICONHAND);
			delete tagSelect;
			return;
		}
		else
		{
			tagSelect->tagList[0] = tagID;
			numTag++;
		}
		tagSelect->numTags = numTag;

		int rdrID = GetDlgItemInt(IDC_EDIT_RDR_ID, NULL,true);
		int hostID = GetDlgItemInt(IDC_EDIT_HOST_ID, NULL,true);
		
		bool interval = false; 
		if(((CButton *)GetDlgItem(IDC_RADIO_Long_INTERVAL))->GetCheck() == BST_CHECKED)
			interval = true;
		int ret = rfGetTagTempLogTimestamp(hostID, rdrID, 0, tagSelect, false, false, cmdType, ++pktID);

		apiDlg->Display("rfSetTempLogTimestamp()", pktID, ret);
		delete tagSelect;
	}
