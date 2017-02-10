object ConfigProgStationForm: TConfigProgStationForm
  Left = 306
  Top = 158
  AutoSize = True
  BorderIcons = []
  BorderStyle = bsDialog
  Caption = 'Programming Station Configuration'
  ClientHeight = 491
  ClientWidth = 605
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  Position = poOwnerFormCenter
  OnActivate = FormActivate
  PixelsPerInch = 96
  TextHeight = 13
  object GroupBox1: TGroupBox
    Left = 292
    Top = 0
    Width = 313
    Height = 131
    Caption = 'Detected Tag Display Configuration'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clRed
    Font.Height = -11
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
    TabOrder = 0
    object MultiDisplayTagCheckBox: TCheckBox
      Left = 12
      Top = 34
      Width = 229
      Height = 17
      Caption = 'Multi Display Tag Detect in List View'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
      TabOrder = 0
    end
    object DuplicateTagFGenCheckBox: TCheckBox
      Left = 12
      Top = 64
      Width = 295
      Height = 17
      Caption = 'Display Duplicate Tag ID with different Field Generator ID  '
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
      TabOrder = 1
    end
    object DuplicateTagGIDCheckBox: TCheckBox
      Left = 12
      Top = 94
      Width = 257
      Height = 17
      Caption = 'Display Duplicate Tag ID with different Group ID'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
      TabOrder = 2
    end
  end
  object GroupBox2: TGroupBox
    Left = 0
    Top = 70
    Width = 289
    Height = 105
    Caption = 'System Host ID Configuration'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clRed
    Font.Height = -11
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
    TabOrder = 1
    object Label1: TLabel
      Left = 12
      Top = 36
      Width = 61
      Height = 13
      Caption = 'Old Host ID: '
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
    end
    object Label2: TLabel
      Left = 154
      Top = 36
      Width = 67
      Height = 13
      Caption = 'New Host ID: '
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
    end
    object OldHostIDEdit: TEdit
      Left = 74
      Top = 34
      Width = 49
      Height = 21
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
      TabOrder = 0
    end
    object NewHostIDEdit: TEdit
      Left = 220
      Top = 34
      Width = 49
      Height = 21
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
      TabOrder = 1
    end
    object AllHostCheckBox: TCheckBox
      Left = 12
      Top = 72
      Width = 113
      Height = 17
      Caption = 'Accept All Host ID '
      Checked = True
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
      State = cbChecked
      TabOrder = 2
    end
  end
  object BitBtn1: TBitBtn
    Left = 222
    Top = 466
    Width = 75
    Height = 25
    TabOrder = 2
    Kind = bkClose
  end
  object SaveBitBtn: TBitBtn
    Left = 330
    Top = 466
    Width = 75
    Height = 25
    Caption = 'SAVE'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clRed
    Font.Height = -11
    Font.Name = 'MS Sans Serif'
    Font.Style = [fsBold]
    ParentFont = False
    TabOrder = 3
    OnClick = SaveBitBtnClick
  end
  object GroupBox3: TGroupBox
    Left = 0
    Top = 0
    Width = 287
    Height = 65
    Caption = 'Tag Temperature Calibration'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clRed
    Font.Height = -11
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
    TabOrder = 4
    object Label3: TLabel
      Left = 10
      Top = 30
      Width = 118
      Height = 13
      Caption = 'Temperature Calibration: '
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
    end
    object TempCalibEdit: TEdit
      Left = 130
      Top = 26
      Width = 65
      Height = 21
      Color = clWhite
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
      TabOrder = 0
    end
    object TempCalibCRadioButton: TRadioButton
      Left = 208
      Top = 20
      Width = 71
      Height = 17
      Caption = 'Degree C'
      Checked = True
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
      TabOrder = 1
      TabStop = True
      OnClick = TempCalibCRadioButtonClick
    end
    object TempCalibFRadioButton: TRadioButton
      Left = 208
      Top = 40
      Width = 69
      Height = 17
      Caption = 'Degree F '
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
      TabOrder = 2
      OnClick = TempCalibFRadioButtonClick
    end
  end
  object GroupBox4: TGroupBox
    Left = 0
    Top = 180
    Width = 291
    Height = 273
    Caption = 'Tag Type Name Definition'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clRed
    Font.Height = -11
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
    TabOrder = 5
    object Label4: TLabel
      Left = 76
      Top = 36
      Width = 59
      Height = 13
      Caption = 'Abbreviation'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
    end
    object Label5: TLabel
      Left = 150
      Top = 36
      Width = 31
      Height = 13
      Caption = 'Name '
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
    end
    object Label6: TLabel
      Left = 80
      Top = 224
      Width = 46
      Height = 10
      Caption = '(Max 3 char)'
      Font.Charset = ANSI_CHARSET
      Font.Color = clBlack
      Font.Height = -8
      Font.Name = 'Small Fonts'
      Font.Style = []
      ParentFont = False
    end
    object Label7: TLabel
      Left = 174
      Top = 224
      Width = 49
      Height = 10
      Caption = '(Max 12 char)'
      Font.Charset = ANSI_CHARSET
      Font.Color = clBlack
      Font.Height = -8
      Font.Name = 'Small Fonts'
      Font.Style = []
      ParentFont = False
    end
    object Type01CheckBox: TCheckBox
      Left = 14
      Top = 52
      Width = 65
      Height = 17
      Caption = 'Type 1'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
      TabOrder = 0
      OnClick = Type01CheckBoxClick
    end
    object Type02CheckBox: TCheckBox
      Left = 14
      Top = 82
      Width = 65
      Height = 17
      Caption = 'Type 2'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
      TabOrder = 1
      OnClick = Type02CheckBoxClick
    end
    object Type03CheckBox: TCheckBox
      Left = 14
      Top = 112
      Width = 65
      Height = 17
      Caption = 'Type 3'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
      TabOrder = 2
      OnClick = Type03CheckBoxClick
    end
    object Type04CheckBox: TCheckBox
      Left = 14
      Top = 142
      Width = 65
      Height = 17
      Caption = 'Type 4'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
      TabOrder = 3
      OnClick = Type04CheckBoxClick
    end
    object Type05CheckBox: TCheckBox
      Left = 14
      Top = 172
      Width = 65
      Height = 17
      Caption = 'Type 5'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
      TabOrder = 4
      OnClick = Type05CheckBoxClick
    end
    object Type06CheckBox: TCheckBox
      Left = 14
      Top = 202
      Width = 65
      Height = 17
      Caption = 'Type 6'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
      TabOrder = 5
      OnClick = Type06CheckBoxClick
    end
    object Type01Edit: TEdit
      Left = 150
      Top = 52
      Width = 99
      Height = 21
      Color = clInactiveCaptionText
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      MaxLength = 12
      ParentFont = False
      ReadOnly = True
      TabOrder = 6
    end
    object Type02Edit: TEdit
      Left = 150
      Top = 82
      Width = 99
      Height = 21
      Color = clInactiveBorder
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      MaxLength = 12
      ParentFont = False
      ReadOnly = True
      TabOrder = 7
    end
    object Type03Edit: TEdit
      Left = 150
      Top = 112
      Width = 99
      Height = 21
      Color = clInactiveBorder
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      MaxLength = 12
      ParentFont = False
      ReadOnly = True
      TabOrder = 8
    end
    object Type04Edit: TEdit
      Left = 150
      Top = 142
      Width = 99
      Height = 21
      Color = clInactiveBorder
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      MaxLength = 12
      ParentFont = False
      ReadOnly = True
      TabOrder = 9
    end
    object Type05Edit: TEdit
      Left = 150
      Top = 172
      Width = 99
      Height = 21
      Color = clInactiveBorder
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      MaxLength = 12
      ParentFont = False
      ReadOnly = True
      TabOrder = 10
    end
    object Type06Edit: TEdit
      Left = 150
      Top = 202
      Width = 101
      Height = 21
      Color = clInactiveBorder
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      MaxLength = 12
      ParentFont = False
      ReadOnly = True
      TabOrder = 11
    end
    object Type01AbrEdit: TEdit
      Left = 82
      Top = 52
      Width = 43
      Height = 21
      Color = clInactiveCaptionText
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      MaxLength = 3
      ParentFont = False
      ReadOnly = True
      TabOrder = 12
    end
    object Type02AbrEdit: TEdit
      Left = 82
      Top = 82
      Width = 43
      Height = 21
      Color = clInactiveCaptionText
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      MaxLength = 3
      ParentFont = False
      ReadOnly = True
      TabOrder = 13
    end
    object Type03AbrEdit: TEdit
      Left = 82
      Top = 112
      Width = 43
      Height = 21
      Color = clInactiveCaptionText
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      MaxLength = 3
      ParentFont = False
      ReadOnly = True
      TabOrder = 14
    end
    object Type04AbrEdit: TEdit
      Left = 82
      Top = 142
      Width = 43
      Height = 21
      Color = clInactiveCaptionText
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      MaxLength = 3
      ParentFont = False
      ReadOnly = True
      TabOrder = 15
    end
    object Type05AbrEdit: TEdit
      Left = 82
      Top = 172
      Width = 43
      Height = 21
      Color = clInactiveCaptionText
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      MaxLength = 3
      ParentFont = False
      ReadOnly = True
      TabOrder = 16
    end
    object Type06AbrEdit: TEdit
      Left = 82
      Top = 202
      Width = 43
      Height = 21
      Color = clInactiveCaptionText
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      MaxLength = 3
      ParentFont = False
      ReadOnly = True
      TabOrder = 17
    end
    object Reset: TBitBtn
      Left = 120
      Top = 246
      Width = 51
      Height = 19
      Caption = 'Reset'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlue
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
      TabOrder = 18
      OnClick = ResetClick
    end
  end
end
