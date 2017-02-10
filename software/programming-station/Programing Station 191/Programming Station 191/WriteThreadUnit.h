//---------------------------------------------------------------------------

#ifndef WriteThreadUnitH
#define WriteThreadUnitH
//---------------------------------------------------------------------------
#include <Classes.hpp>
//---------------------------------------------------------------------------
class WriteThread : public TThread
{
private:
protected:
        void __fastcall Execute();
public:
        __fastcall WriteThread(bool CreateSuspended);
        bool __fastcall WriteComm();
};
//---------------------------------------------------------------------------
#endif
