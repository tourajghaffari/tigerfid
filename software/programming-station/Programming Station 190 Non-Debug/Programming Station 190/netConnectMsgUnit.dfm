object NetConnectStatusForm: TNetConnectStatusForm
  Left = 378
  Top = 147
  BorderIcons = []
  BorderStyle = bsDialog
  ClientHeight = 39
  ClientWidth = 305
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  Position = poMainFormCenter
  PixelsPerInch = 96
  TextHeight = 13
  object MsgLabel: TLabel
    Left = 6
    Top = 6
    Width = 295
    Height = 20
    Alignment = taCenter
    Caption = 'Please Wait! Trying to connect to network '
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clBlue
    Font.Height = -16
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
    Layout = tlCenter
  end
  object Timer1: TTimer
    Enabled = False
    Interval = 350
    OnTimer = Timer1Timer
    Left = 196
  end
end
