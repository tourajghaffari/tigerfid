object NetworkForm: TNetworkForm
  Left = 533
  Top = 111
  Width = 337
  Height = 480
  Caption = 'Network Form'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  OnClose = FormClose
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 32
    Top = 22
    Width = 32
    Height = 13
    Caption = 'Label1'
  end
  object Label2: TLabel
    Left = 36
    Top = 142
    Width = 32
    Height = 13
    Caption = 'Label2'
  end
  object Label3: TLabel
    Left = 38
    Top = 176
    Width = 32
    Height = 13
    Caption = 'Label3'
  end
  object Label4: TLabel
    Left = 178
    Top = 142
    Width = 32
    Height = 13
    Caption = 'Label4'
  end
  object Button1: TButton
    Left = 74
    Top = 222
    Width = 75
    Height = 25
    Caption = 'connect 1'
    TabOrder = 0
    OnClick = Button1Click
  end
  object Button2: TButton
    Left = 182
    Top = 224
    Width = 75
    Height = 25
    Caption = 'connect 2'
    TabOrder = 1
    OnClick = Button2Click
  end
  object Button3: TButton
    Left = 78
    Top = 282
    Width = 75
    Height = 25
    Caption = 'Button3'
    TabOrder = 2
    OnClick = Button3Click
  end
  object Button4: TButton
    Left = 182
    Top = 282
    Width = 75
    Height = 25
    Caption = 'Button4'
    TabOrder = 3
    OnClick = Button4Click
  end
  object ClientSocket1: TClientSocket
    Active = False
    Address = '192.168.1.107'
    ClientType = ctNonBlocking
    Port = 10001
    OnConnect = ClientSocket1Connect
    OnDisconnect = ClientSocket1Disconnect
    OnRead = ClientSocket1Read
    OnError = ClientSocket1Error
    Left = 44
    Top = 52
  end
  object Timer1: TTimer
    Enabled = False
    Interval = 100
    OnTimer = Timer1Timer
    Left = 132
    Top = 50
  end
end
