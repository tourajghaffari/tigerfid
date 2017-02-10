#include <afx.h>

#pragma once

class CSCLTModule : public CAtlExeModuleT< CSCLTModule >
{
public :
	DECLARE_LIBID(LIBID_SCLTLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_SCLT, "{00926068-A94F-457D-917E-BB2075DD56CA}")
};

class SCLT
{
private:
	int getNumber(unsigned char, unsigned char);
	CString getWDay(int);
	CString getCmd(int);
	CString getCmdStatus(int);
	CString getTamperStatus(int);
}