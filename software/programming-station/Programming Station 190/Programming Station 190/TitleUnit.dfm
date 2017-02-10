object TitleForm: TTitleForm
  Left = 226
  Top = 164
  Width = 318
  Height = 148
  BorderIcons = []
  Caption = 'Demo Title'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  Position = poOwnerFormCenter
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 16
    Top = 26
    Width = 128
    Height = 13
    Caption = 'Enter New Title For Demo: '
  end
  object TitleEdit: TEdit
    Left = 16
    Top = 42
    Width = 283
    Height = 24
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clBlue
    Font.Height = -13
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
    TabOrder = 0
  end
  object BitBtn1: TBitBtn
    Left = 112
    Top = 78
    Width = 75
    Height = 25
    TabOrder = 1
    OnClick = BitBtn1Click
    Kind = bkOK
  end
end
