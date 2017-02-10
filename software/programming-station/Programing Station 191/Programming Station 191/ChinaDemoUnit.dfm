object ChinaDemoForm: TChinaDemoForm
  Left = 379
  Top = 124
  Width = 422
  Height = 545
  BorderIcons = [biSystemMenu]
  Caption = 'Demo'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  FormStyle = fsStayOnTop
  OldCreateOrder = False
  Position = poOwnerFormCenter
  OnClose = FormClose
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 54
    Top = 42
    Width = 39
    Height = 13
    Caption = 'Tag ID: '
  end
  object Label2: TLabel
    Left = 54
    Top = 82
    Width = 39
    Height = 13
    Caption = 'Tag ID: '
  end
  object Label3: TLabel
    Left = 54
    Top = 122
    Width = 39
    Height = 13
    Caption = 'Tag ID: '
  end
  object Label4: TLabel
    Left = 54
    Top = 162
    Width = 39
    Height = 13
    Caption = 'Tag ID: '
  end
  object Label5: TLabel
    Left = 54
    Top = 202
    Width = 39
    Height = 13
    Caption = 'Tag ID: '
  end
  object Label6: TLabel
    Left = 216
    Top = 12
    Width = 143
    Height = 20
    Alignment = taCenter
    Caption = 'Tags Last Status '
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clNavy
    Font.Height = -16
    Font.Name = 'MS Sans Serif'
    Font.Style = [fsBold]
    ParentFont = False
  end
  object SysMsgLabel_1: TLabel
    Left = 256
    Top = 222
    Width = 114
    Height = 13
    Caption = 'Message Relay #1: '
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clGreen
    Font.Height = -11
    Font.Name = 'MS Sans Serif'
    Font.Style = [fsBold]
    ParentFont = False
    Visible = False
  end
  object Label7: TLabel
    Left = 22
    Top = 314
    Width = 292
    Height = 13
    Caption = 'Time Interval to Check. Enable Relay #2 for Abnormal  (sec) : '
  end
  object Label8: TLabel
    Left = 22
    Top = 286
    Width = 250
    Height = 13
    Caption = 'Time Interval to Report by  Enabling Relay #1 (sec) : '
  end
  object DemoStatusLabel: TLabel
    Left = 24
    Top = 482
    Width = 3
    Height = 13
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clPurple
    Font.Height = -11
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object Label9: TLabel
    Left = 22
    Top = 258
    Width = 112
    Height = 13
    Caption = 'Close Relay after (sec): '
  end
  object SysMsgLabel_2: TLabel
    Left = 256
    Top = 238
    Width = 114
    Height = 13
    Caption = 'Message Relay #2: '
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clRed
    Font.Height = -11
    Font.Name = 'MS Sans Serif'
    Font.Style = [fsBold]
    ParentFont = False
    Visible = False
  end
  object NormalRelay1Label: TLabel
    Left = 22
    Top = 354
    Width = 333
    Height = 16
    AutoSize = False
    Caption = 'NORMAL Relay  (Relay #1)   '
    Color = clInactiveBorder
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clGray
    Font.Height = -13
    Font.Name = 'MS Sans Serif'
    Font.Style = [fsBold]
    ParentColor = False
    ParentFont = False
  end
  object AbnormalRelay2Label: TLabel
    Left = 20
    Top = 388
    Width = 335
    Height = 16
    AutoSize = False
    Caption = 'ABNORMAL Relay  (Relay #2)   '
    Color = clInactiveBorder
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clGray
    Font.Height = -13
    Font.Name = 'MS Sans Serif'
    Font.Style = [fsBold]
    ParentColor = False
    ParentFont = False
  end
  object Tag1Edit: TEdit
    Left = 96
    Top = 38
    Width = 65
    Height = 21
    TabOrder = 0
  end
  object Tag2Edit: TEdit
    Left = 96
    Top = 78
    Width = 65
    Height = 21
    TabOrder = 1
  end
  object Tag3Edit: TEdit
    Left = 96
    Top = 118
    Width = 65
    Height = 21
    TabOrder = 2
  end
  object Tag4Edit: TEdit
    Left = 96
    Top = 158
    Width = 65
    Height = 21
    TabOrder = 3
  end
  object Tag5Edit: TEdit
    Left = 96
    Top = 198
    Width = 65
    Height = 21
    TabOrder = 4
  end
  object StartDemoBitBtn: TBitBtn
    Left = 34
    Top = 444
    Width = 79
    Height = 25
    Hint = 'Will show only Enabled Tags'
    Caption = 'Start Demo'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clGreen
    Font.Height = -11
    Font.Name = 'MS Sans Serif'
    Font.Style = [fsBold]
    ParentFont = False
    ParentShowHint = False
    ShowHint = True
    TabOrder = 5
    OnClick = StartDemoBitBtnClick
  end
  object StopDemoBitBtn: TBitBtn
    Left = 168
    Top = 444
    Width = 79
    Height = 25
    Caption = 'Stop Demo'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clRed
    Font.Height = -11
    Font.Name = 'MS Sans Serif'
    Font.Style = [fsBold]
    ParentFont = False
    TabOrder = 6
    OnClick = StopDemoBitBtnClick
  end
  object BitBtn3: TBitBtn
    Left = 168
    Top = 476
    Width = 79
    Height = 25
    TabOrder = 7
    OnClick = BitBtn3Click
    Kind = bkClose
  end
  object ClearBitBtn: TBitBtn
    Left = 296
    Top = 444
    Width = 75
    Height = 25
    Caption = 'Clear '
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clBlue
    Font.Height = -13
    Font.Name = 'MS Sans Serif'
    Font.Style = [fsBold]
    ParentFont = False
    TabOrder = 8
    OnClick = ClearBitBtnClick
  end
  object NormalRelayTimeEdit: TEdit
    Left = 318
    Top = 282
    Width = 65
    Height = 21
    TabOrder = 9
    Text = '80'
  end
  object AbnormalRelayTimeEdit: TEdit
    Left = 318
    Top = 310
    Width = 65
    Height = 21
    TabOrder = 10
    Text = '40'
  end
  object CloseRelayEdit: TEdit
    Left = 138
    Top = 254
    Width = 47
    Height = 21
    Color = clWhite
    TabOrder = 11
    Text = '10'
  end
  object TagStatus01Edit: TEdit
    Left = 208
    Top = 36
    Width = 161
    Height = 24
    Color = clInactiveCaptionText
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWhite
    Font.Height = -13
    Font.Name = 'MS Sans Serif'
    Font.Style = [fsBold]
    ParentFont = False
    ReadOnly = True
    TabOrder = 12
  end
  object TagStatus02Edit: TEdit
    Left = 208
    Top = 76
    Width = 161
    Height = 24
    Color = clInactiveCaptionText
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWhite
    Font.Height = -13
    Font.Name = 'MS Sans Serif'
    Font.Style = [fsBold]
    ParentFont = False
    ReadOnly = True
    TabOrder = 13
  end
  object TagStatus03Edit: TEdit
    Left = 208
    Top = 116
    Width = 161
    Height = 24
    Color = clInactiveCaptionText
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWhite
    Font.Height = -13
    Font.Name = 'MS Sans Serif'
    Font.Style = [fsBold]
    ParentFont = False
    ReadOnly = True
    TabOrder = 14
  end
  object TagStatus04Edit: TEdit
    Left = 208
    Top = 154
    Width = 161
    Height = 24
    Color = clInactiveCaptionText
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWhite
    Font.Height = -13
    Font.Name = 'MS Sans Serif'
    Font.Style = [fsBold]
    ParentFont = False
    ReadOnly = True
    TabOrder = 15
  end
  object TagStatus05Edit: TEdit
    Left = 208
    Top = 196
    Width = 161
    Height = 24
    Color = clInactiveCaptionText
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWhite
    Font.Height = -13
    Font.Name = 'MS Sans Serif'
    Font.Style = [fsBold]
    ParentFont = False
    ReadOnly = True
    TabOrder = 16
  end
  object TitleBitBtn: TBitBtn
    Left = 296
    Top = 476
    Width = 75
    Height = 25
    Caption = 'Title'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clPurple
    Font.Height = -13
    Font.Name = 'MS Sans Serif'
    Font.Style = [fsBold]
    ParentFont = False
    TabOrder = 17
    OnClick = TitleBitBtnClick
  end
  object NormalRelayTimer: TTimer
    Enabled = False
    Interval = 120000
    OnTimer = NormalRelayTimerTimer
    Left = 328
    Top = 376
  end
  object NormalRelayCloseTimer: TTimer
    Enabled = False
    Interval = 5000
    OnTimer = NormalRelayCloseTimerTimer
    Left = 358
    Top = 406
  end
  object CallTagTimer: TTimer
    Enabled = False
    Interval = 7000
    OnTimer = CallTagTimerTimer
    Left = 328
    Top = 346
  end
  object NoRespTimer: TTimer
    Enabled = False
    OnTimer = NoRespTimerTimer
    Left = 358
    Top = 346
  end
  object AbnormalRelayTimer: TTimer
    Enabled = False
    Interval = 60000
    OnTimer = AbnormalRelayTimerTimer
    Left = 328
    Top = 406
  end
  object AbnormalRelayCloseTimer: TTimer
    Enabled = False
    Interval = 5000
    OnTimer = AbnormalRelayCloseTimerTimer
    Left = 358
    Top = 376
  end
  object StartingTimer: TTimer
    Enabled = False
    Interval = 2500
    OnTimer = StartingTimerTimer
    Left = 298
    Top = 376
  end
  object StartSearchForMissingTagTimer: TTimer
    Enabled = False
    OnTimer = StartSearchForMissingTagTimerTimer
    Left = 298
    Top = 346
  end
end
