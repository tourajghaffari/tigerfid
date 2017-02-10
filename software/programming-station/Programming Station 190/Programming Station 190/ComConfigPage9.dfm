object CommConfigDlg: TCommConfigDlg
  Left = 308
  Top = 100
  Width = 356
  Height = 472
  Caption = 'Communication  Configuration Dialog'
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
  object cancelBtn: TBitBtn
    Left = 182
    Top = 404
    Width = 91
    Height = 31
    Cancel = True
    Caption = 'Cancel'
    ModalResult = 2
    TabOrder = 0
    OnClick = cancelBtnClick
    Glyph.Data = {
      DE010000424DDE01000000000000760000002800000024000000120000000100
      0400000000006801000000000000000000001000000000000000000000000000
      80000080000000808000800000008000800080800000C0C0C000808080000000
      FF0000FF000000FFFF00FF000000FF00FF00FFFF0000FFFFFF00333333333333
      333333333333333333333333000033338833333333333333333F333333333333
      0000333911833333983333333388F333333F3333000033391118333911833333
      38F38F333F88F33300003339111183911118333338F338F3F8338F3300003333
      911118111118333338F3338F833338F3000033333911111111833333338F3338
      3333F8330000333333911111183333333338F333333F83330000333333311111
      8333333333338F3333383333000033333339111183333333333338F333833333
      00003333339111118333333333333833338F3333000033333911181118333333
      33338333338F333300003333911183911183333333383338F338F33300003333
      9118333911183333338F33838F338F33000033333913333391113333338FF833
      38F338F300003333333333333919333333388333338FFF830000333333333333
      3333333333333333333888330000333333333333333333333333333333333333
      0000}
    NumGlyphs = 2
  end
  object OKBitBtn: TBitBtn
    Left = 66
    Top = 404
    Width = 91
    Height = 31
    Caption = 'OK'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clGreen
    Font.Height = -13
    Font.Name = 'MS Sans Serif'
    Font.Style = [fsBold]
    ParentFont = False
    TabOrder = 1
    OnClick = OKBitBtnClick
  end
  object CommPageControl: TPageControl
    Left = 8
    Top = 6
    Width = 333
    Height = 327
    ActivePage = RS232TabSheet
    TabIndex = 0
    TabOrder = 2
    OnChange = CommPageControlChange
    object RS232TabSheet: TTabSheet
      Caption = 'RS-232'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
      object GroupBox1: TGroupBox
        Left = 12
        Top = 10
        Width = 297
        Height = 273
        Caption = 'Port Settings'
        TabOrder = 0
        object Label1: TLabel
          Left = 39
          Top = 32
          Width = 82
          Height = 13
          Alignment = taRightJustify
          Caption = 'Bits per second : '
        end
        object Label2: TLabel
          Left = 70
          Top = 72
          Width = 51
          Height = 13
          Alignment = taRightJustify
          Caption = 'Data bits : '
        end
        object Label3: TLabel
          Left = 86
          Top = 112
          Width = 35
          Height = 13
          Alignment = taRightJustify
          Caption = 'Parity : '
        end
        object Label4: TLabel
          Left = 71
          Top = 152
          Width = 50
          Height = 13
          Alignment = taRightJustify
          Caption = 'Stop bits : '
        end
        object Label5: TLabel
          Left = 55
          Top = 192
          Width = 66
          Height = 13
          Alignment = taRightJustify
          Caption = 'Flow control : '
        end
        object Label6: TLabel
          Left = 60
          Top = 234
          Width = 60
          Height = 13
          Caption = 'Comm Port : '
        end
        object BaudRateComboBox: TComboBox
          Left = 128
          Top = 32
          Width = 145
          Height = 21
          ItemHeight = 13
          TabOrder = 0
          Text = '2400'
          OnChange = BaudRateComboBoxChange
          Items.Strings = (
            '2400'
            '4800'
            '9600'
            '19200'
            '38400'
            '57600'
            '115200'
            '230400')
        end
        object DataBitsComboBox: TComboBox
          Left = 128
          Top = 72
          Width = 145
          Height = 21
          ItemHeight = 13
          TabOrder = 1
          Text = '8'
          OnChange = DataBitsComboBoxChange
          Items.Strings = (
            '4'
            '5'
            '6'
            '7'
            '8')
        end
        object ParityComboBox: TComboBox
          Left = 128
          Top = 112
          Width = 145
          Height = 21
          ItemHeight = 13
          TabOrder = 2
          Text = 'None'
          OnChange = ParityComboBoxChange
          Items.Strings = (
            'Even'
            'Odd'
            'None'
            'Mark'
            'Space')
        end
        object StopBitsComboBox: TComboBox
          Left = 128
          Top = 154
          Width = 145
          Height = 21
          ItemHeight = 13
          TabOrder = 3
          Text = '1'
          OnChange = StopBitsComboBoxChange
          Items.Strings = (
            '1'
            '1.5'
            '2')
        end
        object FlowCtrlComboBox: TComboBox
          Left = 128
          Top = 192
          Width = 145
          Height = 21
          ItemHeight = 13
          TabOrder = 4
          Text = 'None'
          Items.Strings = (
            'Xon / Xoff'
            'Hardware'
            'None')
        end
        object CommPortComboBox: TComboBox
          Left = 128
          Top = 230
          Width = 145
          Height = 21
          ItemHeight = 13
          TabOrder = 5
          Text = 'COM1'
          Items.Strings = (
            'COM1'
            'COM2'
            'COM3'
            'COM4'
            'COM5'
            'COM6'
            'COM7'
            'COM8'
            'COM9'
            'COM10'
            'COM11'
            'COM12'
            'COM13'
            'COM14'
            'COM15'
            'COM16'
            'COM17'
            'COM18'
            'COM19'
            'COM20')
        end
      end
    end
    object TCPIPTabSheet: TTabSheet
      Caption = 'TCP/IP'
      ImageIndex = 1
      object Label7: TLabel
        Left = 70
        Top = 116
        Width = 39
        Height = 13
        Caption = 'Port ID: '
      end
      object IPEdit1: TEdit
        Left = 112
        Top = 28
        Width = 33
        Height = 21
        MaxLength = 3
        TabOrder = 0
      end
      object IPEdit2: TEdit
        Left = 146
        Top = 28
        Width = 33
        Height = 21
        MaxLength = 3
        TabOrder = 1
      end
      object IPEdit3: TEdit
        Left = 180
        Top = 28
        Width = 33
        Height = 21
        MaxLength = 3
        TabOrder = 2
      end
      object IPEdit4: TEdit
        Left = 214
        Top = 28
        Width = 33
        Height = 21
        MaxLength = 3
        TabOrder = 3
      end
      object IPAddrRadioButton: TRadioButton
        Left = 34
        Top = 30
        Width = 75
        Height = 17
        Caption = 'IP Address'
        Checked = True
        TabOrder = 4
        TabStop = True
        OnClick = IPAddrRadioButtonClick
      end
      object HostNameEdit: TEdit
        Left = 112
        Top = 70
        Width = 137
        Height = 21
        Color = clMenu
        ReadOnly = True
        TabOrder = 5
      end
      object HostNameRadioButton: TRadioButton
        Left = 34
        Top = 72
        Width = 75
        Height = 17
        Caption = 'Host Name'
        TabOrder = 6
        OnClick = HostNameRadioButtonClick
      end
      object ComPortIDEdit: TEdit
        Left = 112
        Top = 112
        Width = 69
        Height = 21
        Color = clWhite
        TabOrder = 7
      end
    end
  end
  object GroupBox2: TGroupBox
    Left = 8
    Top = 342
    Width = 331
    Height = 51
    Caption = 'Communication Protocol '
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
    TabOrder = 3
    object RS232RadioButton: TRadioButton
      Left = 58
      Top = 24
      Width = 87
      Height = 17
      Caption = 'RS-232'
      Checked = True
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
      TabOrder = 0
      TabStop = True
      OnClick = RS232RadioButtonClick
    end
    object TCPIPRadioButton: TRadioButton
      Left = 204
      Top = 24
      Width = 81
      Height = 17
      Caption = 'TCP/IP'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
      TabOrder = 1
      OnClick = TCPIPRadioButtonClick
    end
  end
end
