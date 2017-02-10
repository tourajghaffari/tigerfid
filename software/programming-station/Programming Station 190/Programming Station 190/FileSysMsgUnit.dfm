object FileSysMsgForm: TFileSysMsgForm
  Left = 680
  Top = 188
  BorderIcons = [biSystemMenu]
  BorderStyle = bsDialog
  Caption = 'System Message'
  ClientHeight = 87
  ClientWidth = 325
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  FormStyle = fsStayOnTop
  OldCreateOrder = False
  Position = poOwnerFormCenter
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 14
    Top = 18
    Width = 114
    Height = 16
    Caption = 'File already exists! '
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object OverWriteBitBtn: TBitBtn
    Left = 12
    Top = 52
    Width = 91
    Height = 25
    Caption = 'Overwrite'
    TabOrder = 0
    OnClick = OverWriteBitBtnClick
  end
  object AppendBitBtn: TBitBtn
    Left = 118
    Top = 52
    Width = 91
    Height = 25
    Caption = 'Append'
    TabOrder = 1
    OnClick = AppendBitBtnClick
  end
  object BitBtn3: TBitBtn
    Left = 222
    Top = 52
    Width = 91
    Height = 25
    TabOrder = 2
    Kind = bkCancel
  end
end
