object DiagnosticForm: TDiagnosticForm
  Left = 347
  Top = 294
  Width = 250
  Height = 305
  Caption = 'Diagnostics'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -10
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  Position = poMainFormCenter
  OnActivate = FormActivate
  PixelsPerInch = 96
  TextHeight = 13
  object SetCommandBitBtn: TBitBtn
    Left = 35
    Top = 238
    Width = 82
    Height = 27
    Caption = 'SET'
    TabOrder = 0
    OnClick = SetCommandBitBtnClick
  end
  object BitBtn1: TBitBtn
    Left = 138
    Top = 238
    Width = 74
    Height = 28
    TabOrder = 1
    Kind = bkClose
  end
  object GroupBox1: TGroupBox
    Left = 8
    Top = 8
    Width = 227
    Height = 224
    Caption = 'Diagnostic Byte Setting'
    TabOrder = 2
    object Label1: TLabel
      Left = 28
      Top = 26
      Width = 18
      Height = 13
      Caption = '0 -  '
    end
    object Label2: TLabel
      Left = 29
      Top = 48
      Width = 18
      Height = 13
      Caption = '1 -  '
    end
    object Label3: TLabel
      Left = 28
      Top = 72
      Width = 18
      Height = 13
      Caption = '2 -  '
    end
    object Label4: TLabel
      Left = 28
      Top = 95
      Width = 18
      Height = 13
      Caption = '3 -  '
    end
    object Label5: TLabel
      Left = 28
      Top = 116
      Width = 18
      Height = 13
      Caption = '4 -  '
    end
    object Label6: TLabel
      Left = 28
      Top = 139
      Width = 18
      Height = 13
      Caption = '5 -  '
    end
    object Label7: TLabel
      Left = 28
      Top = 163
      Width = 18
      Height = 13
      Caption = '6 -  '
    end
    object Label8: TLabel
      Left = 28
      Top = 184
      Width = 18
      Height = 13
      Caption = '7 -  '
    end
    object Label06: TLabel
      Left = 151
      Top = 163
      Width = 38
      Height = 13
      Caption = 'All Pkts '
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clGray
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
    end
    object CheckBoxBit00: TCheckBox
      Left = 49
      Top = 26
      Width = 166
      Height = 14
      Caption = 'Reserved'
      Enabled = False
      TabOrder = 0
    end
    object CheckBoxBit01: TCheckBox
      Left = 50
      Top = 49
      Width = 167
      Height = 14
      Caption = 'Reserved'
      Enabled = False
      TabOrder = 1
    end
    object CheckBoxBit02: TCheckBox
      Left = 49
      Top = 72
      Width = 166
      Height = 13
      Caption = 'Reserved'
      Enabled = False
      TabOrder = 2
    end
    object CheckBoxBit03: TCheckBox
      Left = 49
      Top = 94
      Width = 166
      Height = 14
      Caption = 'Reserved'
      Enabled = False
      TabOrder = 3
    end
    object CheckBoxBit04: TCheckBox
      Left = 49
      Top = 117
      Width = 166
      Height = 14
      Caption = 'Reserved'
      Enabled = False
      TabOrder = 4
    end
    object CheckBoxBit05: TCheckBox
      Left = 49
      Top = 140
      Width = 79
      Height = 14
      Caption = 'Reserved'
      Enabled = False
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
      TabOrder = 5
    end
    object CheckBoxBit06: TCheckBox
      Left = 49
      Top = 163
      Width = 98
      Height = 13
      Caption = 'Good Pkts Only'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlue
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
      TabOrder = 6
      OnClick = CheckBoxBit06Click
    end
    object CheckBoxBit07: TCheckBox
      Left = 49
      Top = 185
      Width = 166
      Height = 14
      Caption = 'Reserved'
      Enabled = False
      TabOrder = 7
    end
  end
end
