object TAWSocket: TTAWSocket
  Left = 188
  Top = 108
  Width = 206
  Height = 147
  Caption = 'AW Socket'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object AWClientSocket: TClientSocket
    Active = False
    ClientType = ctNonBlocking
    Port = 10001
    OnConnect = AWClientSocketConnect
    OnDisconnect = AWClientSocketDisconnect
    OnRead = AWClientSocketRead
    OnError = AWClientSocketError
    Left = 10
    Top = 8
  end
end
