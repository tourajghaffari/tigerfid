object SocketForm: TSocketForm
  Left = 409
  Top = 287
  Width = 328
  Height = 300
  Caption = 'Configuration'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object ClientSocket1: TClientSocket
    Active = False
    ClientType = ctNonBlocking
    Port = 0
    Left = 60
    Top = 42
  end
end
