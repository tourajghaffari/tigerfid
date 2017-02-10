object TagReadLargDataForm: TTagReadLargDataForm
  Left = 574
  Top = 32
  Width = 426
  Height = 383
  BorderIcons = [biSystemMenu]
  Caption = 'Read Large Data'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clRed
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  Position = poDesktopCenter
  PixelsPerInch = 96
  TextHeight = 13
  object NumPktsLabel: TLabel
    Left = 8
    Top = 310
    Width = 86
    Height = 13
    Caption = '# Byes To Send:  '
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clGreen
    Font.Height = -11
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object Label1: TLabel
    Left = 8
    Top = 330
    Width = 85
    Height = 13
    Caption = '# PKts To Send : '
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clGreen
    Font.Height = -12
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object BytesToSendLabel: TLabel
    Left = 94
    Top = 310
    Width = 21
    Height = 13
    Caption = '000 '
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clGreen
    Font.Height = -12
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object PktsToSendLabel: TLabel
    Left = 94
    Top = 330
    Width = 21
    Height = 13
    Caption = '000 '
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clGreen
    Font.Height = -12
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object Label2: TLabel
    Left = 202
    Top = 310
    Width = 67
    Height = 13
    Caption = '# Byes Sent:  '
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clBlue
    Font.Height = -12
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object ByesSentLabel: TLabel
    Left = 266
    Top = 310
    Width = 21
    Height = 13
    Caption = '000 '
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clBlue
    Font.Height = -12
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object PktsSentLabel: TLabel
    Left = 268
    Top = 330
    Width = 21
    Height = 13
    Caption = '000 '
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clBlue
    Font.Height = -12
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object Label7: TLabel
    Left = 202
    Top = 330
    Width = 65
    Height = 13
    Caption = '# Pkts Sent : '
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clBlue
    Font.Height = -12
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object SecLabel: TLabel
    Left = 368
    Top = 310
    Width = 41
    Height = 13
    Caption = '000 sec '
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clBlue
    Font.Height = -12
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object ListBox: TListBox
    Left = 8
    Top = 6
    Width = 401
    Height = 289
    Color = clInfoBk
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ItemHeight = 16
    ParentFont = False
    TabOrder = 0
  end
  object StopBitBtn: TBitBtn
    Left = 368
    Top = 328
    Width = 43
    Height = 21
    Caption = 'Stop'
    TabOrder = 1
    OnClick = StopBitBtnClick
  end
end
