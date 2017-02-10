#include "stdafx.h"
#include <afx.h>
#include "Translate.h"
#include "Commands.h"
#include "FeigReader.h"


#ifndef _DELAY
#define _DELAY 1000
#endif

#ifndef _TIMEOUT
#define _TIMEOUT 5
#endif

FeigReader rfRdr;

Commands::Commands()
{
	int retV = rfRdr.connect();
}

CString Commands::ReadHFID()
{
	CString m_statusText;
	unsigned char *resp;
	int len;
	rfRdr.readUid();
	resp = rfRdr.getRsp();
	len = rfRdr.getRspLen();
	m_statusText.Format(_T("\r\n"));
	for (int i = 6; i < len; i++){ 
		m_statusText.AppendFormat(_T("%c"),resp[i]);
	}
	m_statusText.Append(_T("\r\n"));
	return m_statusText;
}

CString Commands::ReadHistory()
{
	CString m_statusText;

	m_statusText.Format(_T("Iniciando leitura do histórico\r\n"));
	unsigned char membk1, membk2;
	membk1 = 0x00;
	membk2 = 0x01;

	unsigned char memoryBank[2] = {membk1,membk2};
	unsigned char *resp;
	rfRdr.read(memoryBank);
	resp = rfRdr.getRsp();
	int qtd = rfRdr.getRspLen() > 0 ? Translate::getNumber(resp[6],resp[7]) : 0;

	for(int i = 0; i < qtd; i++){
		int qtd2 = i*3+6;

		unsigned char membk1, membk2;
		membk1 = qtd2 / 256;
		membk2 = qtd2 - membk1*256;

		unsigned char memoryBank[2] = {membk1,membk2};
		unsigned char *resp;
		rfRdr.read(memoryBank);
		resp = rfRdr.getRsp();
		int year = Translate::getNumber(resp[2],resp[3]);
		int day = Translate::getNumber(resp[4],resp[5]);
		int month = Translate::getNumber(resp[6],resp[7]);
		int nDay = Translate::getNumber(resp[8],resp[9]);
		if(memoryBank[1] == 0xFF){
			memoryBank[0]++;
			memoryBank[1] = 0x00;
		} else {
			memoryBank[1]++;
		}
		rfRdr.read(memoryBank);
		resp = rfRdr.getRsp();
		int sec = Translate::getNumber(resp[2],resp[3]);
		int min = Translate::getNumber(resp[4],resp[5]);
		int hr = Translate::getNumber(resp[6],resp[7]);
		int res = Translate::getNumber(resp[8],resp[9]);
		if(memoryBank[1] == 0xFF){
			memoryBank[0]++;
			memoryBank[1] = 0x00;
		} else {
			memoryBank[1]++;
		}
		rfRdr.read(memoryBank);
		resp = rfRdr.getRsp();
		int empid1 = Translate::getNumber(resp[2],resp[3]);
		int empid2 = Translate::getNumber(resp[4],resp[5]);
		int tamper = Translate::getNumber(resp[6],resp[7]);
		int cmdS = Translate::getNumber(resp[8],resp[9]);

		CString wDay = Translate::getWDay(nDay);
		CString cmd = Translate::getCmd(res);
		CString tamperStatus = Translate::getTamperStatus(tamper);
	
		m_statusText.AppendFormat(_T("%s executado %s, "),cmd,wDay);
		m_statusText.AppendFormat(_T("%.2d/%.2d/20%.2d às %.2d:%.2d:%.2d.\r\n"),day,month,year,hr,min,sec);
		m_statusText.AppendFormat(_T("%s\r\n"),tamperStatus);
	}
	
	if(qtd == 0){
		m_statusText.Format(_T("Histórico vazio\r\n"));
	}
	return m_statusText;
}

CString Commands::StartTimer(){
	CString m_statusText;

	struct tm local;
	char am_pm[] = "AM";
	time_t long_time;
	errno_t err;

	// Get time as 64-bit integer.
	time( &long_time ); 
	// Convert to local time.
	err = localtime_s( &local, &long_time ); 
	if (err)
	{
		printf("Invalid argument to _localtime64_s.");
		exit(1);
	}
	/*struct tm* localtime_s (const time_t *pt);
	time_t curr;
	tm local;
	time(&curr); // get current time_t value
	local=*(localtime(&curr)); // dereference and assign*/
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
		m_statusText.Format(_T("Erro: Sem resposta do Micro-Controlador"));
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
			m_statusText.Format(_T("Tempo de espera excedido, reposicione o lacre!"));
		} else if(rfRdr.getRspLen() > 0 && status == '0'){
			m_statusText.Format(_T("Contador de tempo iniciado!!\r\n"));
		} else if(rfRdr.getRspLen() > 0 || status != '0') {
			m_statusText.Format(_T("Erro: %s"),Translate::getCmdStatus(Translate::getNumber(resp[4],resp[5])));
			OutputDebugStringA((LPCSTR)resp);
		} else {
			m_statusText.Format(_T("Erro, reposicione o lacre!\r\n"));
		}	
	}
	return m_statusText;
}

CString Commands::StopTimer()
{
	CString m_statusText;
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
		m_statusText.Format(_T("Tempo de espera excedido, reposicione o lacre!"));
	} else if(rfRdr.getRspLen() > 0  && status == '0'){
		m_statusText.Format(_T("Contador de tempo terminado!\r\n"));
	} else if(rfRdr.getRspLen() > 0 || status != '0') {
		m_statusText.AppendFormat(_T("Erro: %s\r\n"),Translate::getCmdStatus(Translate::getNumber(resp[4],resp[5])));
	} else {
		m_statusText.Format(_T("Erro, reposicione o lacre!\r\n"));
	}
	return m_statusText;
}

CString Commands::ReadStatus()
{
	CString m_statusText;
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
		m_statusText.Format(_T("Tempo de espera excedido, reposicione o lacre!"));
	} else if(rfRdr.getRspLen() > 0  && status == '0'){
		memoryBank[0] = 0x00;
		memoryBank[1] = 0x05;
		retV = rfRdr.read(memoryBank);
		resp = rfRdr.getRsp();
		Sleep(_DELAY);
		tamper = Translate::getNumber(resp[6],resp[7]);
		CString statusTamper = Translate::getTamperStatus(tamper);
		m_statusText.Format(_T("Leitura efetuada: %s\r\n"),statusTamper);
	} else if(rfRdr.getRspLen() > 0 && status != '0') {
		m_statusText.AppendFormat(_T("Erro: %s\r\n"),Translate::getCmdStatus(Translate::getNumber(resp[4],resp[5])));
		OutputDebugStringA((char *)resp);
	} else {
		m_statusText.Format(_T("Erro reposicione o lacre!\r\n"));
	}
	return m_statusText;
}

CString Commands::ReadMemoryBank(CString memoryBankS)
{
	//wprintf(_T("Data received: %.4s\r\n"), memoryBankS);
	CString m_statusText;
	unsigned char memoryBank[2];
	unsigned char *resp;

	/*wchar_t * wideChr=memoryBankS.GetBuffer();  //(TCHAR is the same as wchar_t)
	unsigned char * multChr= new unsigned char[wcslen(wideChr)];
	wcstombs((char*)multChr,wideChr,blockSize);
	*/
	unsigned char memory[4];
	for(int i = 0 ; i < 4 ; i++){
		memory[i] = (memoryBankS[i] >= 'A' && memoryBankS[i] <= 'Z') ? memoryBankS[i] - 'A' + 0xA : memoryBankS[i] - '0';
	}

	memoryBank[0] = (unsigned char)memory[0]*16 + memory[1];
	memoryBank[1] = (unsigned char)memory[2]*16 + memory[3];

	rfRdr.read(memoryBank);

	if(rfRdr.getRspLen() > 0){
		resp = rfRdr.getRsp();
		m_statusText.Format(_T(""));
		for (int i=2; i < rfRdr.getRspLen() - 4; i+=2){
				m_statusText.AppendFormat(_T("%c%c "),resp[i],resp[i+1]);
			}
			m_statusText.AppendFormat(_T("\r\n"));
	}
	//delete [] multChr;
	return m_statusText;
}

CString Commands::WriteMemoryBank(CString dataS)
{
	//wprintf(_T("Data received: %.12s\r\n"), dataS);
	CString m_statusText;
	unsigned char memoryBank[2];
	unsigned char command;
	unsigned char commandStatus;
	unsigned char dados[2];
	unsigned char data[12];
	unsigned char *resp;

	for(int i = 0 ; i < 12 ; i++){
		data[i] = (dataS[i] >= 'A' && dataS[i] <= 'Z') ? dataS[i] - 'A' + 0xA : dataS[i] - '0';
	}

	memoryBank[0] = (unsigned char)data[0]*16 + data[1];
	memoryBank[1] = (unsigned char)data[2]*16 + data[3];

	command = (unsigned char)data[4]*16 + data[5];

	commandStatus = (unsigned char)data[6]*16 + data[7];

	dados[0] = (unsigned char)data[8]*16 + data[9];
	dados[1] = (unsigned char)data[10]*16 + data[11];

	rfRdr.write(memoryBank,commandStatus,command,dados);

	if(rfRdr.getRspLen() > 0){
		resp = rfRdr.getRsp();
		m_statusText.Format(_T(""));
		for (int i=0; i < rfRdr.getRspLen(); i+=2){
				m_statusText.AppendFormat(_T("%c%c "),resp[i],resp[i+1]);
			}
			m_statusText.AppendFormat(_T("\r\n"));
	}

	return m_statusText;
}

CString Commands::ResetToFactory()
{
	CString m_statusText;
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
		status = rfRdr.getRsp()[4]*16 + rfRdr.getRsp()[5];
		resp = rfRdr.getRsp();
		Sleep(_DELAY);
		i++;
	}while((status != '0') && i < _TIMEOUT * 5);
	resp = rfRdr.getRsp();
	if(i == _TIMEOUT * 5) {
		m_statusText.Format(_T("Tempo de espera excedido, reposicione o lacre!"));
	} else if(rfRdr.getRspLen() > 0  && status == '0'){
		m_statusText.Format(_T("Command Successful!!\r\n"));
		m_statusText.Format(this->Flush());
	} else if(rfRdr.getRspLen() > 0 || status != '0') {
		m_statusText.Format(_T("Error (Status) !!\r\n"));
	} else {
		m_statusText.Format(_T("Erro, reposicione o lacre!\r\n"));
	}
	return m_statusText;
}

CString Commands::Flush()
{
	CString m_statusText;
	CString m_edReset = _T("FF");
	
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
	m_statusText.Format(_T("Configurações de fábrica restauradas!\r\n"));
	return m_statusText;
}

CString Commands::StoreUHFID(CString tagId)
{
	CString m_statusText;
	CString address = _T("01FD");
	wchar_t uid[12];
	uid[0] = address[0];
	uid[1] = address[1];
	uid[2] = address[2];
	uid[3] = address[3];
	for(int i = 0 ; i < 8 ; i++)
	{
		uid[i + 4] = tagId[i];
	}
	//wprintf(_T("Writing Data: %.12s\r\n"), (CString)uid);
	this->WriteMemoryBank((CString)uid);

	address = _T("01FE");
	uid[0] = address[0];
	uid[1] = address[1];
	uid[2] = address[2];
	uid[3] = address[3];
	for(int i = 8 ; i < 16 ; i++)
	{
		uid[i - 4] = tagId[i];
	}
	//wprintf(_T("Writing Data: %.12s\r\n"), (CString)uid);
	this->WriteMemoryBank((CString)uid);

	address = _T("01FF");
	uid[0] = address[0];
	uid[1] = address[1];
	uid[2] = address[2];
	uid[3] = address[3];
	for(int i = 16 ; i < 24 ; i++)
	{
		uid[i - 12] = tagId[i];
	}
	//wprintf(_T("Writing Data: %.12s\r\n"), (CString)uid);
	this->WriteMemoryBank((CString)uid);

	m_statusText.Format(_T("WriteOK\r\n"));
	return m_statusText;
}

CString Commands::ReadUHFID()
{
	CString m_statusText;
	CString address = _T("01FD");
	m_statusText.Format(_T("%s"),this->ReadMemoryBank(address));
	address = _T("01FE");
	m_statusText.AppendFormat(_T("%s"),this->ReadMemoryBank(address));
	address = _T("01FF");
	m_statusText.AppendFormat(_T("%s"),this->ReadMemoryBank(address));
	m_statusText.Replace(_T(" "),_T(""));
	m_statusText.Replace(_T("\r\n"),_T(""));
	m_statusText.AppendFormat(_T("\r\n"));
	return m_statusText;
}

CString Commands::GetTagVersion()
{
	CString m_statusText;
	CString address = _T("0004");
	CString memBank = this->ReadMemoryBank(address);
	m_statusText.Format(_T("%c%c\r\n"),memBank[9],memBank[10]);
	return m_statusText;
}