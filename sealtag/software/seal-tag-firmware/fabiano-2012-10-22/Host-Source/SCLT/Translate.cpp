#include "stdafx.h"
#include <atlstr.h>
#include "Translate.h"

	Translate::Translate()
	{
	}

	int Translate::getNumber(unsigned char l, unsigned char r)
	{
		int ret = ((l >= 'A' && l <= 'Z') ? l - 'A' + 0xA : l - '0') * 16 + ((r >= 'A' && r <= 'Z') ? r - 'A' + 0xA : r - '0');
		return ret;
	}

	CString Translate::getWDay(int iwDay)
	{
		CString wDay;

		switch(iwDay){
			case 1:
				wDay.Format(_T("Domingo"));
				break;
			case 2:
				wDay.Format(_T("Segunda-Feira"));
				break;
			case 3:
				wDay.Format(_T("Terça-Feira"));
				break;
			case 4:
				wDay.Format(_T("Quarta-Feira"));
				break;
			case 5:
				wDay.Format(_T("Quinta-Feira"));
				break;
			case 6:
				wDay.Format(_T("Sexta-Feira"));
				break;
			case 7:
				wDay.Format(_T("Sábado"));
				break;
		}

		return wDay;
	}

	CString Translate::getCmd(int iCmd)
	{
		CString cmd;

		switch(iCmd){
			case 1:
				cmd.Format(_T("Ler relógio"));
				break;
			case 2:
				cmd.Format(_T("Iniciar cronômetro"));
				break;
			case 3:
				cmd.Format(_T("Interromper cronômetro"));
				break;
			case 4:
				cmd.Format(_T("Ler status"));
				break;
			case 5:
				cmd.Format(_T("Eliminar histórico"));
				break;
			case 6:
				cmd.Format(_T("Ler nível de bateria"));
				break;
			case 170:
				cmd.Format(_T("Abrir lacre"));
				break;
		}

		return cmd;
	}

	CString Translate::getCmdStatus(int iCmd)
	{
		CString cmd;

		switch(iCmd){
			case 0:
				cmd.Format(_T("Completo"));
				break;
			case 1:
				cmd.Format(_T("Ocupado"));
				break;
			case 2:
				cmd.Format(_T("Recebido"));
				break;
			case 3:
				cmd.Format(_T("Enviado"));
				break;
			case 4:
				cmd.Format(_T("Em Execução"));
				break;
			case 245:
				cmd.Format(_T("I2C Master Receiver Mode Selected Event Failure"));
				break;
			case 246:
				cmd.Format(_T("I2C Master Byte Transmitted Event Failure"));
				break;
			case 247:
				cmd.Format(_T("I2C Master Byte Transmitting Event Failure"));
				break;
			case 248:
				cmd.Format(_T("I2C Master Transmitter Mode Event Failure"));
				break;
			case 249:
				cmd.Format(_T("I2C Master Mode Select Event Failure"));
				break;
			case 250:
				cmd.Format(_T("I2C Bus Busy"));
				break;
			case 251:
				cmd.Format(_T("Invalid Command Status"));
				break;
			case 252:
				cmd.Format(_T("Invalid Command"));
				break;
			case 253:
				cmd.Format(_T("Inicie o cronômetro!"));
				break;
			case 254:
				cmd.Format(_T("Feche o lacre antes de iniciar o cronômetro"));
				break;
			case 255:
				cmd.Format(_T("Leia o histórico antes de tentar restaurar configurações"));
				break;
			default:
				cmd.Format(_T("%d!"),iCmd);
				break;
		}

		return cmd;
	}

	CString Translate::getTamperStatus(int iTamper)
	{
		CString tamperStatus;

		switch(iTamper){
			case 0:
				tamperStatus.Format(_T("Lacre Inviolado."));
				break;
			case 1:
				tamperStatus.Format(_T("Detectada abertura do lacre, contadores parados."));
				break;
			case 2:
				tamperStatus.Format(_T("Detectada abertura do lacre."));
				break;
			case 4:
				tamperStatus.Format(_T("Detectada tentativa de fraude no lacre (ByPass)."));
				break;
		}

		return tamperStatus;
	}