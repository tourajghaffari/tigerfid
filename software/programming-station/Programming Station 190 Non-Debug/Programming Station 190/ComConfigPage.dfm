object CommConfigDlg: TCommConfigDlg
  Left = 335
  Top = 39
  Width = 533
  Height = 694
  AutoSize = True
  BorderIcons = [biSystemMenu, biHelp]
  Caption = 'Communication  Configuration Dialog'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clPurple
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  Position = poOwnerFormCenter
  OnActivate = FormActivate
  OnClose = FormClose
  PixelsPerInch = 96
  TextHeight = 13
  object Label12: TLabel
    Left = 14
    Top = 178
    Width = 57
    Height = 13
    Caption = 'IP Address: '
  end
  object ConnectLabel: TLabel
    Left = 2
    Top = 618
    Width = 5
    Height = 16
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clPurple
    Font.Height = -13
    Font.Name = 'MS Sans Serif'
    Font.Style = [fsBold]
    ParentFont = False
  end
  object Msg: TLabel
    Left = 4
    Top = 642
    Width = 21
    Height = 16
    Caption = '     '
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clPurple
    Font.Height = -13
    Font.Name = 'MS Sans Serif'
    Font.Style = [fsBold]
    ParentFont = False
  end
  object cancelBtn: TBitBtn
    Left = 434
    Top = 638
    Width = 91
    Height = 29
    TabOrder = 0
    OnClick = cancelBtnClick
    Kind = bkClose
  end
  object CommPageControl: TPageControl
    Left = 0
    Top = 0
    Width = 523
    Height = 575
    ActivePage = TCPIPTabSheet
    TabIndex = 1
    TabOrder = 1
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
        Left = 76
        Top = 94
        Width = 323
        Height = 323
        Caption = 'Port Settings'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
        TabOrder = 0
        object Label1: TLabel
          Left = 33
          Top = 58
          Width = 102
          Height = 16
          Alignment = taRightJustify
          Caption = 'Bits per second : '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label2: TLabel
          Left = 73
          Top = 98
          Width = 62
          Height = 16
          Alignment = taRightJustify
          Caption = 'Data bits : '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label3: TLabel
          Left = 92
          Top = 138
          Width = 43
          Height = 16
          Alignment = taRightJustify
          Caption = 'Parity : '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label4: TLabel
          Left = 74
          Top = 178
          Width = 61
          Height = 16
          Alignment = taRightJustify
          Caption = 'Stop bits : '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label5: TLabel
          Left = 55
          Top = 218
          Width = 80
          Height = 16
          Alignment = taRightJustify
          Caption = 'Flow control : '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label6: TLabel
          Left = 60
          Top = 260
          Width = 75
          Height = 16
          Caption = 'Comm Port : '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object BaudRateComboBox: TComboBox
          Left = 142
          Top = 58
          Width = 145
          Height = 24
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ItemHeight = 16
          ParentFont = False
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
          Left = 142
          Top = 98
          Width = 145
          Height = 24
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ItemHeight = 16
          ParentFont = False
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
          Left = 142
          Top = 138
          Width = 145
          Height = 24
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ItemHeight = 16
          ParentFont = False
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
          Left = 142
          Top = 180
          Width = 145
          Height = 24
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ItemHeight = 16
          ParentFont = False
          TabOrder = 3
          Text = '1'
          OnChange = StopBitsComboBoxChange
          Items.Strings = (
            '1'
            '1.5'
            '2')
        end
        object FlowCtrlComboBox: TComboBox
          Left = 142
          Top = 218
          Width = 145
          Height = 24
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ItemHeight = 16
          ParentFont = False
          TabOrder = 4
          Text = 'None'
          Items.Strings = (
            'Xon / Xoff'
            'Hardware'
            'None')
        end
        object CommPortComboBox: TComboBox
          Left = 142
          Top = 256
          Width = 145
          Height = 24
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ItemHeight = 16
          ParentFont = False
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
        Left = 8
        Top = 522
        Width = 46
        Height = 16
        Caption = 'Port ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
      end
      object Label8: TLabel
        Left = 8
        Top = 121
        Width = 499
        Height = 19
        Alignment = taCenter
        AutoSize = False
        Caption = 'List of Active IP Addresses '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -16
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
      end
      object ComPortIDEdit: TEdit
        Left = 56
        Top = 516
        Width = 59
        Height = 24
        Color = clInfoBk
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
        ReadOnly = True
        TabOrder = 0
        Text = '10001'
      end
      object IPListView: TListView
        Left = 8
        Top = 140
        Width = 499
        Height = 229
        Checkboxes = True
        Color = clInfoBk
        Columns = <
          item
            Caption = 'Selected'
            Width = 55
          end
          item
            Alignment = taCenter
            Caption = 'IP Address'
            Width = 105
          end
          item
            Alignment = taCenter
            Caption = 'Reader ID'
            Width = 70
          end
          item
            Alignment = taCenter
            Caption = 'Host ID'
            Width = 60
          end
          item
            Alignment = taCenter
            Caption = 'Network Status'
            Width = 85
          end
          item
            Alignment = taCenter
            Caption = 'Rdr Status'
            Width = 65
          end
          item
            Caption = 'Connect Time'
            Width = 150
          end>
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clHotLight
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        GridLines = True
        ReadOnly = True
        RowSelect = True
        ParentFont = False
        SortType = stText
        TabOrder = 1
        ViewStyle = vsReport
        OnClick = IPListViewClick
        OnColumnClick = IPListViewColumnClick
        OnCompare = IPListViewCompare
        OnCustomDrawItem = IPListViewCustomDrawItem
        OnDblClick = IPListViewDblClick
      end
      object ClearListBitBtn: TBitBtn
        Left = 138
        Top = 512
        Width = 16
        Height = 29
        Caption = 'Reset List'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 2
        Visible = False
        OnClick = ClearListBitBtnClick
      end
      object SearchIPBitBtn: TBitBtn
        Left = 378
        Top = 510
        Width = 123
        Height = 29
        Caption = 'Search'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 3
        OnClick = SearchIPBitBtnClick
      end
      object GroupBox3: TGroupBox
        Left = 9
        Top = 440
        Width = 498
        Height = 57
        Caption = 'Assign IP Address '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
        TabOrder = 4
        object Label9: TLabel
          Left = 8
          Top = 28
          Width = 82
          Height = 13
          Caption = 'New IP Address: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object AssignIPBitBtn: TBitBtn
          Left = 368
          Top = 18
          Width = 122
          Height = 29
          Caption = 'Assign IP'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          OnClick = AssignIPBitBtnClick
        end
        object IPEdit1: TEdit
          Left = 94
          Top = 22
          Width = 43
          Height = 24
          Color = clInfoBk
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          TabOrder = 1
        end
        object IPEdit2: TEdit
          Left = 140
          Top = 22
          Width = 43
          Height = 24
          Color = clInfoBk
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          TabOrder = 2
        end
        object IPEdit3: TEdit
          Left = 186
          Top = 22
          Width = 43
          Height = 24
          Color = clInfoBk
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          TabOrder = 3
        end
        object IPEdit4: TEdit
          Left = 232
          Top = 22
          Width = 43
          Height = 24
          Color = clInfoBk
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          TabOrder = 4
        end
      end
      object ResetReadersBitBtn: TBitBtn
        Left = 248
        Top = 508
        Width = 123
        Height = 29
        Caption = 'Reset Reader'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clGreen
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 5
        OnClick = ResetReadersBitBtnClick
      end
      object SelectAllCheckBox: TCheckBox
        Left = 8
        Top = 386
        Width = 85
        Height = 17
        Caption = 'Select All'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
        TabOrder = 6
        OnClick = SelectAllCheckBoxClick
      end
      object GroupBox4: TGroupBox
        Left = 136
        Top = 384
        Width = 217
        Height = 43
        Caption = 'Encryption'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
        TabOrder = 7
        object NoEncryptRadioButton: TRadioButton
          Left = 30
          Top = 18
          Width = 55
          Height = 17
          Caption = 'None'
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          TabOrder = 0
          TabStop = True
          OnClick = NoEncryptRadioButtonClick
        end
        object RijndaelEncryptRadioButton: TRadioButton
          Left = 121
          Top = 18
          Width = 73
          Height = 17
          Caption = 'Encrypted '
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          TabOrder = 1
          OnClick = RijndaelEncryptRadioButtonClick
        end
      end
      object KeepListItemCheckBox: TCheckBox
        Left = 8
        Top = 412
        Width = 111
        Height = 17
        Caption = 'Keep List Item'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
        TabOrder = 8
        OnClick = SelectAllCheckBoxClick
      end
      object RemoveIPBitBtn: TBitBtn
        Left = 400
        Top = 392
        Width = 99
        Height = 29
        Caption = 'Remove IP'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
        TabOrder = 9
        OnClick = RemoveIPBitBtnClick
      end
      object GroupBox5: TGroupBox
        Left = 10
        Top = 14
        Width = 495
        Height = 93
        Caption = 'Network Connection '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
        TabOrder = 10
        object UseSearchIPRadioButton: TRadioButton
          Left = 34
          Top = 30
          Width = 201
          Height = 17
          Caption = 'Use Search For Active IP'#39's.'
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          TabOrder = 0
          TabStop = True
          OnClick = UseSearchIPRadioButtonClick
        end
        object UseSpecificIPRadioButton: TRadioButton
          Left = 34
          Top = 64
          Width = 121
          Height = 17
          Caption = 'Use Specific IP'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          TabOrder = 1
          OnClick = UseSpecificIPRadioButtonClick
        end
        object IPSpecEdit1: TEdit
          Left = 191
          Top = 57
          Width = 43
          Height = 28
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -16
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          ReadOnly = True
          TabOrder = 2
        end
        object IPSpecEdit2: TEdit
          Left = 237
          Top = 57
          Width = 43
          Height = 28
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -16
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          ReadOnly = True
          TabOrder = 3
        end
        object IPSpecEdit3: TEdit
          Left = 283
          Top = 57
          Width = 43
          Height = 28
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -16
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          ReadOnly = True
          TabOrder = 4
        end
        object IPSpecEdit4: TEdit
          Left = 329
          Top = 57
          Width = 43
          Height = 28
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -16
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          ReadOnly = True
          TabOrder = 5
        end
        object AddIPBitBtn: TBitBtn
          Left = 394
          Top = 58
          Width = 75
          Height = 25
          Caption = 'Add'
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          TabOrder = 6
          OnClick = AddIPBitBtnClick
        end
      end
    end
  end
  object GroupBox2: TGroupBox
    Left = 2
    Top = 582
    Width = 521
    Height = 53
    Caption = 'Communication Protocol '
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clRed
    Font.Height = -13
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
    TabOrder = 2
    object RS232RadioButton: TRadioButton
      Left = 18
      Top = 24
      Width = 77
      Height = 17
      Caption = 'RS-232'
      Checked = True
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentFont = False
      TabOrder = 0
      TabStop = True
      OnClick = RS232RadioButtonClick
    end
    object TCPIPRadioButton: TRadioButton
      Left = 144
      Top = 24
      Width = 69
      Height = 17
      Caption = 'TCP/IP'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentFont = False
      TabOrder = 1
      OnClick = TCPIPRadioButtonClick
    end
    object ConnectBitBtn: TBitBtn
      Left = 388
      Top = 16
      Width = 123
      Height = 31
      Caption = 'Connect'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlue
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentFont = False
      TabOrder = 2
      OnClick = ConnectBitBtnClick
    end
    object DisconnectBitBtn: TBitBtn
      Left = 256
      Top = 16
      Width = 123
      Height = 31
      Caption = 'Disconnect'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clRed
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentFont = False
      TabOrder = 3
      OnClick = DisconnectBitBtnClick
    end
  end
  object ConnectTimer: TTimer
    Enabled = False
    Interval = 250
    OnTimer = ConnectTimerTimer
    Left = 478
    Top = 50
  end
end
