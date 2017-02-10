/*-------------------------------------------------------
|                                                       |
|                       FedmIscCore.h                   |
|                                                       |
---------------------------------------------------------

Copyright © 2008-2010	FEIG ELECTRONIC GmbH, All Rights Reserved.
						Lange Strasse 4
						D-35781 Weilburg
						Federal Republic of Germany
						phone    : +49 6471 31090
						fax      : +49 6471 310999
						e-mail   : obid-support@feig.de
						Internet : http://www.feig.de
					
Author         		:	Markus Hultsch
Begin        		:	21.04.2008
Version       		:	03.02.00 / 14.06.2009 / M. Hultsch

Operation Systems	:	independent


This file includes all dependencies for the component FEDM

OBID® and OBID i-scan® are registered Trademarks of FEIG ELECTRONIC GmbH
Windows is a registered trademark of Microsoft Corporation in the United States and other countries.
Linux® is a registered trademark of Linus Torvalds.
Electronic Product Code (TM) is a Trademark of EPCglobal Inc.
*/


#if !defined(_FEDM_ISC_CORE_H_INCLUDED_)
#define _FEDM_ISC_CORE_H_INCLUDED_

#if !defined(_FEDM_NO_XML_SUPPORT)
	#define _FEDM_XML_SUPPORT
#endif

#if !defined(_FEDM_NO_DLL)
	#define _FEDM_DLL
#endif

#if !defined(_FEDM_NO_COM_SUPPORT)
	#define _FEDM_COM_SUPPORT
#endif

#if !defined(_FEDM_NO_USB_SUPPORT)
	#define _FEDM_USB_SUPPORT
#endif

#if !defined(_FEDM_NO_TCP_SUPPORT)
	#define _FEDM_TCP_SUPPORT
#endif

#if !defined(_FEDM_NO_TAG_HANDLER)
	#define _FEDM_TAG_HANDLER
#endif

#if !defined(_FEDM_NO_FU_SUPPORT)
	#define _FEDM_FU_SUPPORT
#endif

#ifdef _MSC_VER
	#define _FEDM_WINDOWS
	#if !defined(_FEDM_NO_MFC_SUPPORT)
		#define _FEDM_MFC_SUPPORT
	#endif
#else
	#define _FEDM_LINUX
#endif


#include "core/FEDM.h"
#include "core/FEDM_Base.h"
#include "core/FEDM_DataBase.h"
#include "core/FEDM_Functions.h"
#include "core/FEDM_Xml.h"
#include "core/FEDM_XmlBase.h"
#include "core/FEDM_Xml.h"
#include "core/FEDM_XmlReaderCfgDataModul.h"
#include "core/FEDM_XmlReaderCfgProfileModul.h"
#include "core/i_scan/FEDM_ISC.h"
#include "core/i_scan/FEDM_ISCReaderInfo.h"
#include "core/i_scan/FEDM_ISCReaderDiagnostic.h"
#include "core/i_scan/FEDM_ISCReaderID.h"
#include "core/i_scan/FEDM_ISOTabItem.h"
#include "core/i_scan/FEDM_BRMTabItem.h"
#include "core/i_scan/FEDM_ISCReader.h"
#include "core/i_scan/FEDM_ISCReaderModule.h"
#include "core/i_scan/FEDM_ISCReaderConfig.h"
#include "core/i_scan/FEDM_ISO_IEC_7816_6_ICManufacturerRegistration.h"
#include "core/i_scan/utility/FedmIscReport_ReaderInfo.h"
#include "core/i_scan/utility/FedmIscReport_ReaderDiagnostic.h"
#if !defined(_FEDM_NO_PD_SUPPORT)
	#include "core/i_scan/peripheral_devices/FedmIscPeripheralDevice.h"
	#include "core/i_scan/peripheral_devices/FedmIscPeopleCounter.h"
	#include "core/i_scan/peripheral_devices/FedmIscExternalIO.h"
#endif
#if !defined(_FEDM_NO_FU_SUPPORT)
	#include "core/i_scan/function_unit/FEDM_ISCFunctionUnit.h"
	#include "core/i_scan/function_unit/FEDM_ISCFunctionUnitID.h"
#endif
#if !defined(_FEDM_NO_TAG_HANDLER)
	#include "core/i_scan/tag_handler/FedmIscTagHandler_Includes.h"
#endif // #if !defined(_FEDM_NO_TAG_HANDLER)

#endif // _FEDM_ISC_CORE_H_INCLUDED_

