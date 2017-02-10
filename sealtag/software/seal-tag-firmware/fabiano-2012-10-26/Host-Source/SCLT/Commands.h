#pragma once

class Commands
{
private:
	
public:
	Commands(void);
	CString ReadHistory(void);
	CString StartTimer(void);
	CString StopTimer(void);
	CString ReadStatus(void);
	CString ReadHFID(void);
	CString ResetToFactory(void);
	CString Flush(void);
	CString ReadMemoryBank(CString);
	CString WriteMemoryBank(CString);
	CString StoreUHFID(CString);
	CString ReadUHFID(void);
	CString GetTagVersion(void);
};
