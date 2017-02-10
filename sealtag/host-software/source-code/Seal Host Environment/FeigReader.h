#pragma once

class FeigReader
{
public:
	FeigReader(void);
	~FeigReader(void);
	int connect(void);
	int disconnect(void);
	int write(unsigned char[], unsigned char, unsigned char, unsigned char[]);
	int read(unsigned char[]);
	int readUid(void);
	unsigned char *getRsp(void);
	int getRspLen(void);
private:
	unsigned char sRspData[32];
	int iRspLen, iDeviceHandle, iFeiscHandle;
};
