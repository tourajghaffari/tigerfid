//---------------------------------------------------------------------------

#ifndef MsgThreadUnitH
#define MsgThreadUnitH
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include "netConnectMsgUnit.h"
//---------------------------------------------------------------------------
class TMsgThread : public TThread
{
private:
protected:
        void __fastcall Execute();
public:
        __fastcall TMsgThread(bool CreateSuspended);
        TNetConnectStatusForm* netCoonectMsgForm;
};
//---------------------------------------------------------------------------
#endif
