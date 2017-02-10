#pragma once

class Translate
{
private:
	Translate(void);
public:
	static int getNumber(unsigned char, unsigned char);
	static CString getWDay(int);
	static CString getCmd(int);
	static CString getCmdStatus(int);
	static CString getTamperStatus(int);
};
