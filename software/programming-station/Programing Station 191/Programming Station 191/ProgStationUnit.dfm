object ProgStationForm: TProgStationForm
  Left = 353
  Top = 210
  Anchors = [akLeft, akTop, akRight, akBottom]
  BorderIcons = [biSystemMenu, biMinimize]
  BorderStyle = bsDialog
  Caption = 'Programming  Station          Active Wave Inc.'
  ClientHeight = 692
  ClientWidth = 1028
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  Position = poDesktopCenter
  OnActivate = FormActivate
  OnClick = FormClick
  OnClose = FormClose
  OnDestroy = FormDestroy
  PixelsPerInch = 96
  TextHeight = 13
  object Label44: TLabel
    Left = 50
    Top = 116
    Width = 60
    Height = 16
    Caption = 'Host ID: '
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clNavy
    Font.Height = -13
    Font.Name = 'MS Sans Serif'
    Font.Style = [fsBold]
    ParentFont = False
  end
  object Label52: TLabel
    Left = 16
    Top = 168
    Width = 93
    Height = 16
    Caption = 'Repeater ID: '
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clNavy
    Font.Height = -13
    Font.Name = 'MS Sans Serif'
    Font.Style = [fsBold]
    ParentFont = False
  end
  object Label45: TLabel
    Left = 12
    Top = 220
    Width = 99
    Height = 16
    Caption = 'Field Gen. ID: '
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clNavy
    Font.Height = -13
    Font.Name = 'MS Sans Serif'
    Font.Style = [fsBold]
    ParentFont = False
  end
  object Label41: TLabel
    Left = 30
    Top = 64
    Width = 80
    Height = 16
    Caption = 'Reader ID: '
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clNavy
    Font.Height = -13
    Font.Name = 'MS Sans Serif'
    Font.Style = [fsBold]
    ParentFont = False
  end
  object Panel1: TPanel
    Left = 6
    Top = 50
    Width = 1031
    Height = 613
    BevelInner = bvLowered
    BevelOuter = bvLowered
    TabOrder = 2
    object FGenResetGroupBox: TGroupBox
      Left = 12
      Top = 6
      Width = 551
      Height = 315
      Caption = 'Reset Smart Field Generator '
      Color = clMenu
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clRed
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentColor = False
      ParentFont = False
      TabOrder = 27
      Visible = False
      object FGenResetReaderLabel: TLabel
        Left = 12
        Top = 84
        Width = 80
        Height = 16
        Caption = 'Reader ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label174: TLabel
        Left = 28
        Top = 130
        Width = 60
        Height = 16
        Caption = 'Host ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object FGenResetIDLabel: TLabel
        Left = 22
        Top = 48
        Width = 65
        Height = 16
        Caption = 'FGen ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object FGenResetReaderIDComboBox: TComboBox
        Left = 94
        Top = 84
        Width = 73
        Height = 24
        Cursor = crHandPoint
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ItemHeight = 16
        ParentFont = False
        Sorted = True
        TabOrder = 0
      end
      object FGenResetHostIDEdit: TEdit
        Left = 93
        Top = 123
        Width = 72
        Height = 24
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 1
      end
      object FGenResetListView: TListView
        Left = 360
        Top = 36
        Width = 171
        Height = 247
        Cursor = crHandPoint
        Color = clInfoBk
        Columns = <
          item
            Width = 1
          end
          item
            Alignment = taCenter
            Caption = 'FGen ID'
            Width = 75
          end
          item
            Alignment = taCenter
            Caption = 'Reader ID'
            Width = 90
          end>
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clGreen
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        GridLines = True
        HideSelection = False
        ReadOnly = True
        RowSelect = True
        ParentFont = False
        SortType = stBoth
        TabOrder = 2
        ViewStyle = vsReport
        OnColumnClick = ResetListViewColumnClick
        OnCompare = ResetListViewCompare
      end
      object FGenResetClearBitBtn: TBitBtn
        Left = 420
        Top = 286
        Width = 75
        Height = 21
        Cursor = crHandPoint
        Caption = 'Clear'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 3
        OnClick = FGenResetClearBitBtnClick
      end
      object FGenResetBroadcastRdrCheckBox: TCheckBox
        Left = 12
        Top = 286
        Width = 203
        Height = 17
        Cursor = crHandPoint
        Caption = 'BroadcastTo All Readers'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 4
        OnClick = FGenResetBroadcastRdrCheckBoxClick
      end
      object FGenResetIDComboBox: TComboBox
        Left = 94
        Top = 44
        Width = 73
        Height = 24
        Cursor = crHandPoint
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ItemHeight = 16
        ParentFont = False
        Sorted = True
        TabOrder = 5
      end
      object FGenResetBroadcastSmartFGenCheckBox: TCheckBox
        Left = 12
        Top = 258
        Width = 237
        Height = 17
        Cursor = crHandPoint
        Caption = 'Broadcast To All Smart FGen'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 6
        OnClick = FGenResetBroadcastSmartFGenCheckBoxClick
      end
    end
    object ConfigSFGenGroupBox: TGroupBox
      Left = 12
      Top = 6
      Width = 551
      Height = 315
      Caption = 'Configure Smart Field Generator '
      Color = clMenu
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clRed
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentColor = False
      ParentFont = False
      TabOrder = 36
      Visible = False
      object Label181: TLabel
        Left = 34
        Top = 108
        Width = 67
        Height = 13
        Caption = 'Reader ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label185: TLabel
        Left = 18
        Top = 38
        Width = 84
        Height = 13
        Caption = 'Field Gen  ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label186: TLabel
        Left = 50
        Top = 72
        Width = 52
        Height = 13
        Caption = 'Host ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object GroupBox64: TGroupBox
        Left = 362
        Top = 18
        Width = 179
        Height = 79
        Caption = 'Transmit Time'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 0
        object Label187: TLabel
          Left = 15
          Top = 30
          Width = 36
          Height = 13
          Caption = 'Time: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label188: TLabel
          Left = 132
          Top = 32
          Width = 23
          Height = 13
          Caption = 'Sec'
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentColor = False
          ParentFont = False
        end
        object SFGenConfigTxTimeComboBox: TComboBox
          Left = 56
          Top = 26
          Width = 73
          Height = 21
          Cursor = crHandPoint
          Color = clMenu
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 13
          ParentFont = False
          TabOrder = 0
        end
        object SFGenConfigTxTimeModifyCheckBox: TCheckBox
          Left = 116
          Top = 58
          Width = 61
          Height = 17
          Caption = 'Modify'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clOlive
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          OnClick = SFGenConfigTxTimeModifyCheckBoxClick
        end
      end
      object GroupBox65: TGroupBox
        Left = 362
        Top = 100
        Width = 179
        Height = 87
        Caption = 'Wait Time'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 1
        object Label189: TLabel
          Left = 17
          Top = 20
          Width = 36
          Height = 13
          Caption = 'Time: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object SFGenConfigWaitTimeComboBox: TComboBox
          Left = 60
          Top = 16
          Width = 73
          Height = 21
          Cursor = crHandPoint
          Color = clMenu
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 13
          ParentFont = False
          TabOrder = 0
          Items.Strings = (
            '1'
            '2'
            '3'
            '4'
            '5')
        end
        object SFGenConfigWaitTimeSecRadioButton: TRadioButton
          Left = 10
          Top = 42
          Width = 51
          Height = 19
          Cursor = crHandPoint
          Caption = 'Sec. '
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          TabStop = True
          OnClick = SFGenConfigWaitTimeSecRadioButtonClick
          OnMouseDown = SFGenConfigWaitTimeSecRadioButtonMouseDown
        end
        object SFGenConfigWaitTimeMinRadioButton: TRadioButton
          Left = 68
          Top = 42
          Width = 51
          Height = 19
          Cursor = crHandPoint
          Caption = 'Min.'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 2
          OnClick = SFGenConfigWaitTimeMinRadioButtonClick
          OnMouseDown = SFGenConfigWaitTimeMinRadioButtonMouseDown
        end
        object SFGenConfigWaitTimeHourRadioButton: TRadioButton
          Left = 124
          Top = 42
          Width = 53
          Height = 19
          Cursor = crHandPoint
          Caption = 'Hour'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 3
          OnClick = SFGenConfigWaitTimeHourRadioButtonClick
          OnMouseDown = SFGenConfigWaitTimeHourRadioButtonMouseDown
        end
        object SFGenConfigWaitTimeModifyCheckBox: TCheckBox
          Left = 116
          Top = 66
          Width = 61
          Height = 17
          Caption = 'Modify'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clOlive
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 4
          OnClick = SFGenConfigWaitTimeModifyCheckBoxClick
        end
      end
      object CheckBox3: TCheckBox
        Left = 336
        Top = 286
        Width = 17
        Height = 17
        Caption = 'Default Transmit && Wait Time'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 2
        Visible = False
      end
      object GroupBox66: TGroupBox
        Left = 180
        Top = 18
        Width = 175
        Height = 77
        Caption = 'Tag Type'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 3
        object SFGenConfigTagTypeModifyCheckBox: TCheckBox
          Left = 110
          Top = 56
          Width = 61
          Height = 17
          Caption = 'Modify'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clOlive
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          OnClick = SFGenConfigTagTypeModifyCheckBoxClick
        end
        object SFGenConfigTagTypeComboBox: TComboBox
          Left = 22
          Top = 18
          Width = 133
          Height = 21
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 13
          ParentFont = False
          TabOrder = 1
          Text = 'Any Type'
          Items.Strings = (
            'Factory'
            'Any Type')
        end
      end
      object GroupBox67: TGroupBox
        Left = 180
        Top = 98
        Width = 175
        Height = 83
        Caption = 'Tag ID'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 4
        object SFGenConfigTagIDRadioButton: TRadioButton
          Left = 6
          Top = 22
          Width = 67
          Height = 17
          Cursor = crHandPoint
          Caption = 'Tag ID: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          OnClick = FGenConfigTagIDRadioButtonClick
        end
        object SFGenConfigTagIDEdit: TEdit
          Left = 82
          Top = 20
          Width = 83
          Height = 21
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          ReadOnly = True
          TabOrder = 1
          OnChange = FGenConfigTagIDEditChange
        end
        object SFGenConfigTagIDModifyCheckBox: TCheckBox
          Left = 110
          Top = 62
          Width = 61
          Height = 17
          Caption = 'Modify'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clOlive
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 2
          OnClick = SFGenConfigTagIDModifyCheckBoxClick
        end
        object SFGenConfigAnyTagIDRadioButton: TRadioButton
          Left = 6
          Top = 50
          Width = 87
          Height = 17
          Cursor = crHandPoint
          Caption = 'Any Tag ID'
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 3
          TabStop = True
          OnClick = FGenConfigAnyTypeRadioButtonClick
          OnMouseDown = FGenConfigAnyTypeRadioButtonMouseDown
        end
      end
      object GroupBox68: TGroupBox
        Left = 180
        Top = 246
        Width = 175
        Height = 59
        Caption = 'Assigned Reader ID'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 5
        object Label190: TLabel
          Left = 10
          Top = 26
          Width = 22
          Height = 13
          Caption = 'ID: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object SFGenConfigAssignedReaderIDEdit: TEdit
          Left = 34
          Top = 24
          Width = 47
          Height = 21
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          ReadOnly = True
          TabOrder = 0
          OnChange = FGenConfigAssignedReaderIDEditChange
        end
        object SFGenConfigAssignedReaderIDModifyCheckBox: TCheckBox
          Left = 110
          Top = 36
          Width = 61
          Height = 17
          Caption = 'Modify'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clOlive
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          OnClick = SFGenConfigAssignedReaderIDModifyCheckBoxClick
        end
      end
      object GroupBox69: TGroupBox
        Left = 8
        Top = 132
        Width = 165
        Height = 53
        Caption = 'New Field Gen. ID'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 6
        object Label191: TLabel
          Left = 8
          Top = 26
          Width = 22
          Height = 13
          Caption = 'ID: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object SFGenConfigFGenIDEdit: TEdit
          Left = 32
          Top = 22
          Width = 51
          Height = 21
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          ReadOnly = True
          TabOrder = 0
          OnChange = FGenConfigFGenIDEditChange
        end
        object SFGenConfigFGenIDModifyCheckBox: TCheckBox
          Left = 102
          Top = 32
          Width = 61
          Height = 17
          Caption = 'Modify'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clOlive
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          OnClick = SFGenConfigFGenIDModifyCheckBoxClick
        end
      end
      object GroupBox70: TGroupBox
        Left = 180
        Top = 184
        Width = 175
        Height = 55
        Caption = 'Tag Response Delay '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 7
        object SFGenConfigRaRnModifyCheckBox: TCheckBox
          Left = 110
          Top = 34
          Width = 61
          Height = 17
          Caption = 'Modify'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clOlive
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          OnClick = SFGenConfigRaRnModifyCheckBoxClick
        end
        object SFGenConfigRNShortRadioButton: TRadioButton
          Left = 8
          Top = 16
          Width = 103
          Height = 17
          Cursor = crHandPoint
          Caption = 'Short Random '
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          TabStop = True
        end
        object SFGenConfigRALongRadioButton: TRadioButton
          Left = 8
          Top = 32
          Width = 99
          Height = 17
          Cursor = crHandPoint
          Caption = 'Long Random '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 2
        end
      end
      object ConfigSFGenUpdateBitBtn: TBitBtn
        Left = 378
        Top = 282
        Width = 151
        Height = 27
        Caption = 'Get Configuration'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 8
        OnClick = ConfigSFGenUpdateBitBtnClick
      end
      object SFGenConfigHostIDEdit: TEdit
        Left = 103
        Top = 69
        Width = 68
        Height = 21
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 9
      end
      object GroupBox72: TGroupBox
        Left = 362
        Top = 192
        Width = 179
        Height = 83
        Caption = 'Motion Detector '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 10
        object SFGenConfigMDCheckBox: TCheckBox
          Left = 116
          Top = 62
          Width = 59
          Height = 17
          Caption = 'Modify'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clOlive
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          OnClick = SFGenConfigMDCheckBoxClick
        end
        object SFGenConfigMDActiveHiRadioButton: TRadioButton
          Left = 10
          Top = 40
          Width = 93
          Height = 17
          Cursor = crHandPoint
          Caption = 'Active High  '
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          TabStop = True
        end
        object SFGenConfigMDActiveLoRadioButton: TRadioButton
          Left = 10
          Top = 58
          Width = 89
          Height = 17
          Cursor = crHandPoint
          Caption = 'Active Low '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 2
        end
        object SFGenConfigMDEnableCheckBox: TCheckBox
          Left = 12
          Top = 20
          Width = 77
          Height = 17
          Caption = 'Enable '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 3
          OnClick = FGenConfigTagTypeModifyCheckBoxClick
        end
      end
      object CheckBox12: TCheckBox
        Left = 356
        Top = 292
        Width = 19
        Height = 17
        Caption = 'Monitor PIR Signal'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 11
        Visible = False
        OnClick = FGenConfigActivePIRCheckBoxClick
        OnMouseDown = FGenConfigActivePIRCheckBoxMouseDown
      end
      object CheckBox13: TCheckBox
        Left = 356
        Top = 280
        Width = 21
        Height = 17
        Caption = 'Plus Active PIR Signal'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 12
        Visible = False
        OnClick = FGenConfigActivePIRCheckBoxClick
        OnMouseDown = FGenConfigActivePIRCheckBoxMouseDown
      end
      object FGenConfigSmartFieldGenIDComboBox: TComboBox
        Left = 104
        Top = 34
        Width = 67
        Height = 21
        Cursor = crHandPoint
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ItemHeight = 13
        ParentFont = False
        Sorted = True
        TabOrder = 13
      end
      object FGenConfigSmartFGenReaderIDComboBox: TComboBox
        Left = 102
        Top = 104
        Width = 69
        Height = 21
        Cursor = crHandPoint
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ItemHeight = 13
        ParentFont = False
        Sorted = True
        TabOrder = 14
      end
      object GroupBox111: TGroupBox
        Left = 8
        Top = 188
        Width = 165
        Height = 119
        Caption = 'Field Strength '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 15
        object FGenConfigFSLabel: TLabel
          Left = 6
          Top = 86
          Width = 83
          Height = 13
          Caption = 'Range (0 - 20)'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object SFGenConfigFSRangeModifyCheckBox: TCheckBox
          Left = 102
          Top = 28
          Width = 61
          Height = 17
          Caption = 'Modify'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clOlive
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          OnClick = SFGenConfigFGenIDModifyCheckBoxClick
        end
        object SFGenConfigShortRangeRadioButton: TRadioButton
          Left = 6
          Top = 16
          Width = 95
          Height = 19
          Cursor = crHandPoint
          Caption = 'Short Range'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          OnClick = SFGenConfigWaitTimeSecRadioButtonClick
          OnMouseDown = SFGenConfigWaitTimeSecRadioButtonMouseDown
        end
        object SFGenConfigLongRangeRadioButton: TRadioButton
          Left = 6
          Top = 32
          Width = 91
          Height = 19
          Cursor = crHandPoint
          Caption = 'Long Range'
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 2
          TabStop = True
          OnClick = SFGenConfigWaitTimeSecRadioButtonClick
          OnMouseDown = SFGenConfigWaitTimeSecRadioButtonMouseDown
        end
        object FGenConfigPotentiComboBox: TComboBox
          Left = 8
          Top = 62
          Width = 81
          Height = 21
          Cursor = crHandPoint
          Color = clMenu
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 13
          ParentFont = False
          TabOrder = 3
          Items.Strings = (
            '0'
            '1'
            '2'
            '3'
            '4'
            '5'
            '6'
            '7'
            '8'
            '9'
            '10'
            '11'
            '12'
            '13'
            '14'
            '15'
            '16'
            '17'
            '18'
            '19'
            '20')
        end
        object FGenConfigPotBitBtn: TBitBtn
          Left = 106
          Top = 90
          Width = 55
          Height = 25
          Caption = 'Config'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clRed
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 4
          OnClick = FGenConfigPotBitBtnClick
        end
        object FGenConfigPotentiModifyCheckBox: TCheckBox
          Left = 102
          Top = 60
          Width = 61
          Height = 17
          Caption = 'Modify'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clOlive
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 5
          OnClick = FGenConfigPotentiModifyCheckBoxClick
        end
      end
    end
    object QuerySFGenGroupBox: TGroupBox
      Left = 12
      Top = 6
      Width = 551
      Height = 315
      Caption = 'Query Smart Field Generator '
      Color = clMenu
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clRed
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentColor = False
      ParentFont = False
      TabOrder = 35
      Visible = False
      object Label178: TLabel
        Left = 48
        Top = 76
        Width = 60
        Height = 16
        Caption = 'Host ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label179: TLabel
        Left = 10
        Top = 42
        Width = 99
        Height = 16
        Caption = 'Field Gen  ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label180: TLabel
        Left = 16
        Top = 144
        Width = 93
        Height = 16
        Caption = 'Repeater ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        Visible = False
      end
      object Label182: TLabel
        Left = 184
        Top = 148
        Width = 84
        Height = 11
        Caption = 'NA = Not Assigned  '
        Font.Charset = ANSI_CHARSET
        Font.Color = clBlack
        Font.Height = -9
        Font.Name = 'Small Fonts'
        Font.Style = []
        ParentFont = False
      end
      object Label183: TLabel
        Left = 184
        Top = 160
        Width = 99
        Height = 11
        Caption = 'MD = Motion Detector  '
        Font.Charset = ANSI_CHARSET
        Font.Color = clBlack
        Font.Height = -9
        Font.Name = 'Small Fonts'
        Font.Style = []
        ParentFont = False
      end
      object Label184: TLabel
        Left = 184
        Top = 172
        Width = 80
        Height = 11
        Caption = 'FS = Field Strength '
        Font.Charset = ANSI_CHARSET
        Font.Color = clBlack
        Font.Height = -9
        Font.Name = 'Small Fonts'
        Font.Style = []
        ParentFont = False
      end
      object QueryFGenRdrIDLabel: TLabel
        Left = 30
        Top = 110
        Width = 80
        Height = 16
        Caption = 'Reader ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object QuerySFGenHostIDEdit: TEdit
        Left = 109
        Top = 71
        Width = 64
        Height = 24
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 0
        OnChange = HostIDEditChange
      end
      object Edit7: TEdit
        Left = 109
        Top = 141
        Width = 64
        Height = 24
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 1
        Visible = False
        OnChange = HostIDEditChange
      end
      object QuerySFGenListView: TListView
        Left = 184
        Top = 22
        Width = 359
        Height = 123
        Color = clInfoBk
        Columns = <
          item
            Width = 1
          end
          item
            Alignment = taCenter
            Caption = 'FG ID'
            Width = 40
          end
          item
            Alignment = taCenter
            Caption = 'Rdr ID'
            Width = 45
          end
          item
            Alignment = taCenter
            Caption = 'TX Time'
            Width = 53
          end
          item
            Alignment = taCenter
            Caption = 'Wait Time'
            Width = 60
          end
          item
            Alignment = taCenter
            Caption = 'MD Det.'
            Width = 55
          end
          item
            Alignment = taCenter
            Caption = 'MD Status'
            Width = 68
          end
          item
            Caption = 'FS'
            Width = 30
          end
          item
            Caption = 'FS Range'
            Width = 60
          end
          item
            Alignment = taCenter
            Caption = 'Tag Type'
            Width = 60
          end
          item
            Caption = 'Tag ID'
            Width = 100
          end>
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clGreen
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        GridLines = True
        HideSelection = False
        ReadOnly = True
        RowSelect = True
        ParentFont = False
        SortType = stBoth
        TabOrder = 2
        ViewStyle = vsReport
      end
      object QuerySFGenClearBitBtn: TBitBtn
        Left = 398
        Top = 148
        Width = 75
        Height = 21
        Cursor = crHandPoint
        Caption = 'Clear'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 3
        OnClick = QuerySFGenClearBitBtnClick
      end
      object QuerySFGenCheckBox: TCheckBox
        Left = 488
        Top = 148
        Width = 61
        Height = 17
        Cursor = crHandPoint
        Caption = 'Keep'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 4
      end
      object QuerySFGenProcListView: TListView
        Left = 192
        Top = 194
        Width = 351
        Height = 93
        Color = clInfoBk
        Columns = <
          item
            Width = 1
          end
          item
            Alignment = taCenter
            Caption = 'FG ID'
            Width = 37
          end
          item
            Alignment = taCenter
            Caption = 'Rdr ID'
            Width = 38
          end
          item
            Alignment = taCenter
            Caption = '"J" Code Ver'
            Width = 66
          end
          item
            Alignment = taCenter
            Caption = '"J" Code Date'
            Width = 72
          end
          item
            Alignment = taCenter
            Caption = '"K" Code Ver'
            Width = 67
          end
          item
            Alignment = taCenter
            Caption = '"K" Code Date'
            Width = 72
          end>
        Font.Charset = ANSI_CHARSET
        Font.Color = clGreen
        Font.Height = -9
        Font.Name = 'Small Fonts'
        Font.Style = []
        GridLines = True
        HideSelection = False
        ReadOnly = True
        RowSelect = True
        ParentFont = False
        SortType = stBoth
        TabOrder = 5
        ViewStyle = vsReport
      end
      object QuerySFGenSmartFGenBroadcastCheckBox: TCheckBox
        Left = 8
        Top = 204
        Width = 177
        Height = 17
        Cursor = crHandPoint
        Caption = 'Broadcast To All Smart FGen'
        Font.Charset = ANSI_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 6
        OnClick = QuerySFGenSmartFGenBroadcastCheckBoxClick
      end
      object GetSFGenProcRevDateBitBtn: TBitBtn
        Left = 220
        Top = 288
        Width = 165
        Height = 21
        Cursor = crHandPoint
        Caption = 'Get Processor Rev && Date'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 7
        OnClick = GetProcRevDateBitBtnClick
      end
      object QuerySFGenClearRevListBitBtn: TBitBtn
        Left = 398
        Top = 288
        Width = 75
        Height = 21
        Cursor = crHandPoint
        Caption = 'Clear'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 8
        OnClick = QuerySFGenClearRevListBitBtnClick
      end
      object QuerySFGenKeepRevListCheckBox: TCheckBox
        Left = 488
        Top = 288
        Width = 61
        Height = 17
        Cursor = crHandPoint
        Caption = 'Keep'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 9
      end
      object QuerySFGenSmartFGenBroadcastRdrCheckBox: TCheckBox
        Left = 6
        Top = 286
        Width = 185
        Height = 17
        Cursor = crHandPoint
        Caption = 'Broadcast To All Readers'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 10
        OnClick = QueryFGenSmartFGenBroadcastRdrCheckBoxClick
      end
      object QueryFGenSmartFGenIDComboBox: TComboBox
        Left = 108
        Top = 36
        Width = 65
        Height = 24
        Cursor = crHandPoint
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ItemHeight = 16
        ParentFont = False
        Sorted = True
        TabOrder = 11
        OnChange = ReaderIDComboBoxChange
      end
      object QueryFGenSmartFGenRdrIDComboBox: TComboBox
        Left = 108
        Top = 106
        Width = 65
        Height = 24
        Cursor = crHandPoint
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ItemHeight = 16
        ParentFont = False
        Sorted = True
        TabOrder = 12
        OnChange = ReaderIDComboBoxChange
      end
      object QuerySFGenAnyRdrRadioButton: TRadioButton
        Left = 16
        Top = 246
        Width = 151
        Height = 17
        Caption = 'Respond To Any Reader'
        Enabled = False
        Font.Charset = ANSI_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 13
      end
      object QuerySFGenSpecificRdrRadioButton: TRadioButton
        Left = 16
        Top = 226
        Width = 175
        Height = 17
        Caption = 'Respond To Specific Reader'
        Checked = True
        Enabled = False
        Font.Charset = ANSI_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 14
        TabStop = True
      end
    end
    object TagTempGroupBox: TGroupBox
      Left = 12
      Top = 6
      Width = 551
      Height = 317
      Caption = 'Tag Temperature '
      Color = clMenu
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clRed
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentColor = False
      ParentFont = False
      TabOrder = 25
      Visible = False
      object Label139: TLabel
        Left = 18
        Top = 32
        Width = 67
        Height = 13
        Caption = 'Reader ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label140: TLabel
        Left = 34
        Top = 64
        Width = 52
        Height = 13
        Caption = 'Host ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label141: TLabel
        Left = 8
        Top = 96
        Width = 78
        Height = 13
        Caption = 'Repeater ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        Visible = False
      end
      object TagTempReptIDEdit: TEdit
        Left = 88
        Top = 93
        Width = 65
        Height = 21
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 0
        Visible = False
      end
      object TagTempReaderIDComboBox: TComboBox
        Left = 86
        Top = 30
        Width = 69
        Height = 21
        Cursor = crHandPoint
        Color = clInfoBk
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ItemHeight = 13
        ParentFont = False
        Sorted = True
        TabOrder = 1
      end
      object TagTempHostIDEdit: TEdit
        Left = 86
        Top = 61
        Width = 67
        Height = 21
        Color = clInfoBk
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        TabOrder = 2
      end
      object GroupBox53: TGroupBox
        Left = 160
        Top = 10
        Width = 131
        Height = 109
        Caption = 'Tag ID'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 3
        object TagTempAnyTagIDRadioButton: TRadioButton
          Left = 8
          Top = 88
          Width = 93
          Height = 17
          Cursor = crHandPoint
          Caption = 'Any Tag ID'
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          TabStop = True
          OnClick = TagTempAnyTagIDRadioButtonClick
        end
        object TagTempTagIDRadioButton: TRadioButton
          Left = 8
          Top = 16
          Width = 83
          Height = 17
          Cursor = crHandPoint
          Caption = 'Tag ID: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          OnClick = TagTempTagIDRadioButtonClick
        end
        object TagTempTagIDEdit: TEdit
          Left = 10
          Top = 34
          Width = 111
          Height = 21
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          ReadOnly = True
          TabOrder = 2
        end
        object TagTempTagIDRangeRadioButton: TRadioButton
          Left = 8
          Top = 62
          Width = 59
          Height = 17
          Caption = 'Range: '
          Font.Charset = ANSI_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 3
          OnClick = TagTempTagIDRangeRadioButtonClick
        end
        object TagTempTagIDRangeComboBox: TComboBox
          Left = 72
          Top = 60
          Width = 49
          Height = 21
          Color = clMenu
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 13
          ParentFont = False
          TabOrder = 4
          Text = '2'
          OnExit = TagTempTagIDRangeComboBoxExit
          Items.Strings = (
            '2'
            '3'
            '4'
            '5'
            '6'
            '7'
            '8'
            '10'
            '15'
            '20'
            '40'
            '60'
            '80'
            '100'
            '150'
            '200')
        end
      end
      object GroupBox54: TGroupBox
        Left = 296
        Top = 10
        Width = 101
        Height = 93
        Caption = 'Tag Type'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 4
        object TagTempTagTypeComboBox: TComboBox
          Left = 4
          Top = 24
          Width = 93
          Height = 21
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 13
          ParentFont = False
          TabOrder = 0
          Text = 'Any Type'
          Items.Strings = (
            'Factory'
            'Any Type')
        end
      end
      object TagTempBroadcastRdrCheckBox: TCheckBox
        Left = 8
        Top = 292
        Width = 155
        Height = 17
        Cursor = crHandPoint
        Caption = 'Broadcast All Readers'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 5
        OnClick = TagTempBroadcastRdrCheckBoxClick
      end
      object GroupBox55: TGroupBox
        Left = 8
        Top = 244
        Width = 281
        Height = 43
        Caption = 'Tag Response Delay'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 6
        object TagTempTagRNShortRadioButton: TRadioButton
          Left = 152
          Top = 18
          Width = 111
          Height = 17
          Cursor = crHandPoint
          Caption = 'Short Random '
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          TabStop = True
        end
        object TagTempTagRNLongRadioButton: TRadioButton
          Left = 18
          Top = 18
          Width = 105
          Height = 17
          Cursor = crHandPoint
          Caption = 'Long Random'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
        end
      end
      object GroupBox46: TGroupBox
        Left = 8
        Top = 126
        Width = 281
        Height = 115
        Caption = 'Temp Limits'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 7
        object Label142: TLabel
          Left = 12
          Top = 34
          Width = 43
          Height = 13
          Caption = 'Upper: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label143: TLabel
          Left = 12
          Top = 64
          Width = 43
          Height = 13
          Caption = 'Lower: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label144: TLabel
          Left = 66
          Top = 14
          Width = 46
          Height = 13
          Caption = 'Current '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label145: TLabel
          Left = 154
          Top = 14
          Width = 26
          Height = 13
          Caption = 'New'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label146: TLabel
          Left = 218
          Top = 14
          Width = 42
          Height = 13
          Caption = 'Modify '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object TagTempCalibValueLabel: TLabel
          Left = 84
          Top = 92
          Width = 43
          Height = 13
          AutoSize = False
          Caption = '(0.00)'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label154: TLabel
          Left = 12
          Top = 92
          Width = 71
          Height = 13
          Alignment = taCenter
          AutoSize = False
          Caption = 'Calibration: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object TagTempLimitCdegRadioButton: TRadioButton
          Left = 202
          Top = 90
          Width = 61
          Height = 17
          Cursor = crHandPoint
          Caption = 'C Deg'
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          TabStop = True
          OnClick = TagTempLimitCdegRadioButtonClick
        end
        object TagTempLimitFdegRadioButton: TRadioButton
          Left = 134
          Top = 90
          Width = 59
          Height = 17
          Cursor = crHandPoint
          Caption = 'F Deg'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          OnClick = TagTempLimitFdegRadioButtonClick
        end
        object TagTempNewUpLimitEdit: TEdit
          Left = 136
          Top = 29
          Width = 65
          Height = 21
          Hint = 'Upper Temp Limit, Min value 32F (0C)'
          Color = clBtnFace
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          MaxLength = 7
          ParentFont = False
          ReadOnly = True
          TabOrder = 2
          OnChange = ReadMemoryNumByteEditChange
        end
        object TagTempCurrUpLimitEdit: TEdit
          Left = 54
          Top = 29
          Width = 65
          Height = 21
          Color = clInfoBk
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          MaxLength = 3
          ParentFont = False
          ReadOnly = True
          TabOrder = 3
          OnChange = ReadMemoryNumByteEditChange
        end
        object TagTempCurrLowLimitEdit: TEdit
          Left = 54
          Top = 59
          Width = 65
          Height = 21
          Color = clInfoBk
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          MaxLength = 3
          ParentFont = False
          ReadOnly = True
          TabOrder = 4
          OnChange = ReadMemoryNumByteEditChange
        end
        object TagTempNewLowLimitEdit: TEdit
          Left = 136
          Top = 59
          Width = 65
          Height = 21
          Hint = 'Lower Temp Limit, Min value 32F (0C)'
          Color = clBtnFace
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          MaxLength = 7
          ParentFont = False
          ReadOnly = True
          TabOrder = 5
          OnChange = ReadMemoryNumByteEditChange
        end
        object TagTempChangeUpLimitCheckBox: TCheckBox
          Left = 230
          Top = 30
          Width = 21
          Height = 17
          Color = clBtnFace
          ParentColor = False
          TabOrder = 6
          OnClick = TagTempChangeUpLimitCheckBoxClick
        end
        object TagTempChangeLowLimitCheckBox: TCheckBox
          Left = 230
          Top = 62
          Width = 21
          Height = 17
          Color = clBtnFace
          ParentColor = False
          TabOrder = 7
          Visible = False
          OnClick = TagTempChangeLowLimitCheckBoxClick
        end
        object TagTempCurrUpCalibLimitEdit: TEdit
          Left = 4
          Top = 17
          Width = 13
          Height = 21
          Color = clInfoBk
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          MaxLength = 3
          ParentFont = False
          ReadOnly = True
          TabOrder = 8
          Visible = False
          OnChange = ReadMemoryNumByteEditChange
        end
        object TagTempCurrLowCalibLimitEdit: TEdit
          Left = 20
          Top = 17
          Width = 11
          Height = 21
          Color = clInfoBk
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          MaxLength = 3
          ParentFont = False
          ReadOnly = True
          TabOrder = 9
          Visible = False
          OnChange = ReadMemoryNumByteEditChange
        end
      end
      object GroupBox56: TGroupBox
        Left = 296
        Top = 104
        Width = 249
        Height = 185
        Caption = 'Tag Report Control '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 8
        object Label147: TLabel
          Left = 6
          Top = 130
          Width = 77
          Height = 13
          Caption = '# Reads/Ave'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label148: TLabel
          Left = 4
          Top = 158
          Width = 124
          Height = 13
          Caption = 'Periodic Report Time:'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object TagTempRepLowLimitLabel: TLabel
          Left = 24
          Top = 22
          Width = 149
          Height = 13
          Caption = 'Report Under Lower Limit '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object TagTempRepUpLimitLabel: TLabel
          Left = 24
          Top = 42
          Width = 142
          Height = 13
          Caption = 'Report Over Upper Limit '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object TagTempRepPeriodLabel: TLabel
          Left = 24
          Top = 62
          Width = 157
          Height = 13
          Caption = 'Report With Periodic Read '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object TagTempPeriodRepTimeHourLabel: TLabel
          Left = 204
          Top = 150
          Width = 32
          Height = 13
          Caption = 'Hour '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object TagTempPeriodRepTimeMinLabel: TLabel
          Left = 204
          Top = 166
          Width = 25
          Height = 13
          Caption = 'Min '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label32: TLabel
          Left = 24
          Top = 84
          Width = 93
          Height = 13
          Caption = 'Enable Logging '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label66: TLabel
          Left = 22
          Top = 104
          Width = 75
          Height = 13
          Caption = 'Warp-Around'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object TagTempRepLowLimitCheckBox: TCheckBox
          Left = 6
          Top = 20
          Width = 19
          Height = 17
          Caption = 'Report Under Lower Limit'
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          OnClick = TagTempRepLowLimitCheckBoxClick
          OnMouseDown = TagTempRepLowLimitCheckBoxMouseDown
        end
        object TagTempRepPeriodCheckBox: TCheckBox
          Left = 6
          Top = 60
          Width = 17
          Height = 17
          Caption = 'Report With Periodic Read'
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          OnClick = TagTempRepPeriodCheckBoxClick
          OnMouseDown = TagTempRepPeriodCheckBoxMouseDown
        end
        object TagTempRepUpLimitCheckBox: TCheckBox
          Left = 6
          Top = 40
          Width = 17
          Height = 17
          Caption = 'Report Over Upper Limit'
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 2
          OnClick = TagTempRepUpLimitCheckBoxClick
          OnMouseDown = TagTempRepUpLimitCheckBoxMouseDown
        end
        object TagTempNumReadAveComboBox: TComboBox
          Left = 90
          Top = 126
          Width = 55
          Height = 21
          Color = clMenu
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 13
          ParentFont = False
          TabOrder = 3
          Items.Strings = (
            '1'
            '2'
            '4')
        end
        object TagTempPeriodRepTimeEdit: TEdit
          Left = 132
          Top = 153
          Width = 51
          Height = 21
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          MaxLength = 3
          ParentFont = False
          ReadOnly = True
          TabOrder = 4
          OnChange = ReadMemoryNumByteEditChange
        end
        object TagTempPeriodRepTimeMinRadioButton: TRadioButton
          Left = 188
          Top = 164
          Width = 17
          Height = 17
          Caption = 'Min'
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 5
          OnClick = TagTempPeriodRepTimeMinRadioButtonClick
          OnMouseDown = TagTempPeriodRepTimeMinRadioButtonMouseDown
        end
        object TagTempPeriodRepTimeHourRadioButton: TRadioButton
          Left = 188
          Top = 148
          Width = 15
          Height = 17
          Caption = 'Hour'
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 6
          OnClick = TagTempPeriodRepTimeHourRadioButtonClick
          OnMouseDown = TagTempPeriodRepTimeHourRadioButtonMouseDown
        end
        object TagTempChangeReportCheckBox: TCheckBox
          Left = 186
          Top = 10
          Width = 61
          Height = 17
          Caption = 'Modify '
          Font.Charset = ANSI_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 7
          OnClick = TagTempChangeReportCheckBoxClick
        end
        object TagTempLoggingCheckBox: TCheckBox
          Left = 6
          Top = 82
          Width = 17
          Height = 17
          Caption = 'Report With Periodic Read'
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 8
          OnClick = TagTempLoggingCheckBoxClick
          OnMouseDown = TagTempLoggingCheckBoxMouseDown
        end
        object TagTempWarpAroundCheckBox: TCheckBox
          Left = 6
          Top = 102
          Width = 17
          Height = 17
          Caption = 'Report With Periodic Read'
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 9
          OnClick = TagTempRepPeriodCheckBoxClick
          OnMouseDown = TagTempRepPeriodCheckBoxMouseDown
        end
        object TagTempTimeStampBitBtn: TBitBtn
          Left = 134
          Top = 82
          Width = 77
          Height = 23
          Caption = 'TimeStamp'
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clRed
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 10
          OnClick = TagTempTimeStampBitBtnClick
        end
      end
      object GroupBox57: TGroupBox
        Left = 402
        Top = 10
        Width = 143
        Height = 93
        Caption = 'Tag Temp Value'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 9
        object TagTempCdegRadioButton: TRadioButton
          Left = 6
          Top = 70
          Width = 59
          Height = 17
          Cursor = crHandPoint
          Caption = 'C Deg'
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          TabStop = True
          OnClick = TagTempCdegRadioButtonClick
        end
        object TagTempFdegRadioButton: TRadioButton
          Left = 76
          Top = 70
          Width = 59
          Height = 17
          Cursor = crHandPoint
          Caption = 'F Deg'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          OnClick = TagTempFdegRadioButtonClick
        end
        object TagTempTempValueEdit: TEdit
          Left = 6
          Top = 21
          Width = 71
          Height = 28
          Color = clInfoBk
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGreen
          Font.Height = -16
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          MaxLength = 7
          ParentFont = False
          ReadOnly = True
          TabOrder = 2
          OnChange = ReadMemoryNumByteEditChange
        end
        object TagTempReadTempValueBitBtn: TBitBtn
          Left = 82
          Top = 22
          Width = 51
          Height = 27
          Caption = 'READ'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clRed
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 3
          OnClick = TagTempReadTempValueBitBtnClick
        end
        object TempStatusStaticText: TStaticText
          Left = 6
          Top = 50
          Width = 73
          Height = 17
          Alignment = taCenter
          AutoSize = False
          Caption = 'NORMAL'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGreen
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 4
        end
      end
      object TagTempRefreshBitBtn: TBitBtn
        Left = 452
        Top = 290
        Width = 93
        Height = 23
        Caption = 'Refresh'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 10
        OnClick = TagTempRefreshBitBtnClick
      end
      object TagTempDisplayListCheckBox: TCheckBox
        Left = 280
        Top = 294
        Width = 155
        Height = 17
        Caption = 'Display Tag Temp List'
        Checked = True
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        State = cbChecked
        TabOrder = 11
        OnClick = TagTempDisplayListCheckBoxClick
      end
    end
    object SetFStrengthGroupBox: TGroupBox
      Left = 12
      Top = 6
      Width = 551
      Height = 315
      Caption = 'Set Field Strength '
      Color = clMenu
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clRed
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentColor = False
      ParentFont = False
      TabOrder = 37
      Visible = False
      object Label55: TLabel
        Left = 14
        Top = 38
        Width = 80
        Height = 16
        Caption = 'Reader ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label192: TLabel
        Left = 30
        Top = 84
        Width = 60
        Height = 16
        Caption = 'Host ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object ReaderFStrengthReaderComboBox: TComboBox
        Left = 96
        Top = 38
        Width = 73
        Height = 24
        Cursor = crHandPoint
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ItemHeight = 16
        ParentFont = False
        Sorted = True
        TabOrder = 0
      end
      object ReaderFStrengthHostEdit: TEdit
        Left = 95
        Top = 77
        Width = 72
        Height = 24
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 1
      end
      object ReaderFStrengthListView: TListView
        Left = 374
        Top = 36
        Width = 165
        Height = 247
        Cursor = crHandPoint
        Color = clInfoBk
        Columns = <
          item
            Width = 1
          end
          item
            Alignment = taCenter
            Caption = 'Reader ID'
            Width = 80
          end
          item
            Alignment = taCenter
            Caption = 'Field Strength'
            Width = 80
          end>
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clGreen
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        GridLines = True
        HideSelection = False
        ReadOnly = True
        RowSelect = True
        ParentFont = False
        SortType = stBoth
        TabOrder = 2
        ViewStyle = vsReport
        Visible = False
        OnColumnClick = ResetListViewColumnClick
        OnCompare = ResetListViewCompare
      end
      object ReaderFStrengthClearBitBtn: TBitBtn
        Left = 420
        Top = 286
        Width = 75
        Height = 21
        Cursor = crHandPoint
        Caption = 'Clear'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 3
        Visible = False
        OnClick = FGenResetClearBitBtnClick
      end
      object ReaderFStrengthBroadcastCheckBox: TCheckBox
        Left = 30
        Top = 292
        Width = 203
        Height = 17
        Cursor = crHandPoint
        Caption = 'Broadcast To All Readers'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 4
        OnClick = ResetBroadcastReaderCheckBoxClick
      end
      object GroupBox62: TGroupBox
        Left = 30
        Top = 116
        Width = 311
        Height = 159
        Caption = 'Digital Potentiometer'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 5
        object Label194: TLabel
          Left = 220
          Top = 65
          Width = 86
          Height = 13
          Caption = 'Increase  TX Field'
          Font.Charset = ANSI_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          Visible = False
        end
        object Label195: TLabel
          Left = 218
          Top = 83
          Width = 88
          Height = 13
          Caption = 'Decrease TX Field'
          Font.Charset = ANSI_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          Visible = False
        end
        object Label196: TLabel
          Left = 36
          Top = 61
          Width = 59
          Height = 11
          Caption = 'Range ( 0 - 20)'
          Font.Charset = ANSI_CHARSET
          Font.Color = clNavy
          Font.Height = -9
          Font.Name = 'Small Fonts'
          Font.Style = []
          ParentFont = False
        end
        object Label197: TLabel
          Left = 8
          Top = 134
          Width = 134
          Height = 16
          Caption = 'Current Potentiometer: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object ReaderFStrengthLabel: TLabel
          Left = 144
          Top = 132
          Width = 6
          Height = 20
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -16
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label193: TLabel
          Left = 36
          Top = 115
          Width = 59
          Height = 11
          Caption = 'Range ( 0 - 20)'
          Font.Charset = ANSI_CHARSET
          Font.Color = clNavy
          Font.Height = -9
          Font.Name = 'Small Fonts'
          Font.Style = []
          ParentFont = False
        end
        object Label199: TLabel
          Left = 130
          Top = 97
          Width = 73
          Height = 13
          Caption = 'Modify TX Field'
          Font.Charset = ANSI_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object ReaderFStrengthUpDown: TUpDown
          Left = 287
          Top = 40
          Width = 16
          Height = 24
          Min = 0
          Max = 21
          Position = 0
          TabOrder = 0
          Visible = False
          Wrap = False
        end
        object ReaderFStrengthEdit: TEdit
          Left = 234
          Top = 14
          Width = 69
          Height = 24
          Color = clMenu
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          MaxLength = 2
          ParentFont = False
          TabOrder = 1
          Text = '0'
          Visible = False
        end
        object ReaderGetFStrengthBitBtn: TBitBtn
          Left = 238
          Top = 122
          Width = 65
          Height = 31
          Caption = 'Get'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clRed
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 2
          OnClick = ReaderGetFStrengthBitBtnClick
        end
        object ReaderModifyTXFComboBox: TComboBox
          Left = 36
          Top = 92
          Width = 87
          Height = 21
          Cursor = crHandPoint
          Color = clWhite
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 13
          ParentFont = False
          TabOrder = 3
          Items.Strings = (
            '0'
            '1'
            '2'
            '3'
            '4'
            '5'
            '6'
            '7'
            '8'
            '9'
            '10'
            '11'
            '12'
            '13'
            '14'
            '15'
            '16'
            '17'
            '18'
            '19'
            '20')
        end
        object ReaderINCDECTxFieldCheckBox: TCheckBox
          Left = 12
          Top = 36
          Width = 23
          Height = 17
          TabOrder = 4
          OnClick = ReaderINCDECTxFieldCheckBoxClick
          OnMouseDown = ReaderINCDECTxFieldCheckBoxMouseDown
        end
        object ReaderSetABSTxFieldCheckBox: TCheckBox
          Left = 12
          Top = 94
          Width = 23
          Height = 17
          Checked = True
          State = cbChecked
          TabOrder = 5
          OnClick = ReaderSetABSTxFieldCheckBoxClick
          OnMouseDown = ReaderSetABSTxFieldCheckBoxMouseDown
        end
        object ReaderIncreaseTXBitBtn: TBitBtn
          Left = 36
          Top = 32
          Width = 121
          Height = 25
          Caption = 'Increase TX Field'
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clRed
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 6
          OnClick = ReaderIncreaseTXBitBtnClick
        end
        object ReaderDecreaseTXBitBtn: TBitBtn
          Left = 160
          Top = 32
          Width = 121
          Height = 25
          Caption = 'Decrease TX Field'
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clRed
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 7
          OnClick = ReaderDecreaseTXBitBtnClick
        end
      end
      object ReaderRangeGroupBox: TGroupBox
        Left = 184
        Top = 30
        Width = 157
        Height = 73
        Caption = 'Range '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 6
        object ReaderShortRangeRadioButton: TRadioButton
          Left = 18
          Top = 26
          Width = 125
          Height = 17
          Caption = 'Short Distance '
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          TabStop = True
        end
        object ReaderLongRangeRadioButton: TRadioButton
          Left = 18
          Top = 46
          Width = 133
          Height = 17
          Caption = 'Long Distance'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
        end
      end
    end
    object EnableFGenGroupBox: TGroupBox
      Left = 12
      Top = 6
      Width = 551
      Height = 317
      Caption = 'Enable Reader Field Generator'
      Color = clMenu
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clRed
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentColor = False
      ParentFont = False
      TabOrder = 22
      Visible = False
      object Label162: TLabel
        Left = 36
        Top = 82
        Width = 80
        Height = 16
        Caption = 'Reader ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label163: TLabel
        Left = 56
        Top = 142
        Width = 60
        Height = 16
        Caption = 'Host ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label164: TLabel
        Left = 24
        Top = 200
        Width = 93
        Height = 16
        Caption = 'Repeater ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        Visible = False
      end
      object Edit4: TEdit
        Left = 118
        Top = 197
        Width = 83
        Height = 24
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 0
        Visible = False
      end
      object EnableFGenReaderIDComboBox: TComboBox
        Left = 118
        Top = 78
        Width = 83
        Height = 24
        Cursor = crHandPoint
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ItemHeight = 16
        ParentFont = False
        Sorted = True
        TabOrder = 1
      end
      object EnableFGenHostIDEdit: TEdit
        Left = 118
        Top = 137
        Width = 83
        Height = 24
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 2
      end
      object GroupBox60: TGroupBox
        Left = 284
        Top = 80
        Width = 135
        Height = 137
        Caption = 'Tag Type'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 3
        object EnableFGenANYRadioButton: TRadioButton
          Left = 20
          Top = 98
          Width = 89
          Height = 17
          Cursor = crHandPoint
          Caption = 'Any Type'
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          TabStop = True
        end
        object EnableFGenACCRadioButton: TRadioButton
          Left = 22
          Top = 116
          Width = 77
          Height = 17
          Cursor = crHandPoint
          Caption = 'Access'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
        end
        object EnableFGenASSRadioButton: TRadioButton
          Left = 20
          Top = 52
          Width = 65
          Height = 17
          Cursor = crHandPoint
          Caption = 'Asset'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 2
        end
        object EnableFGenINVRadioButton: TRadioButton
          Left = 20
          Top = 74
          Width = 89
          Height = 17
          Cursor = crHandPoint
          Caption = 'Inventory'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 3
        end
        object EnableFGenTagTypeComboBox: TComboBox
          Left = 14
          Top = 20
          Width = 111
          Height = 21
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 13
          ParentFont = False
          TabOrder = 4
          Text = 'Any Type'
          Items.Strings = (
            'Factory'
            'Any Type')
        end
      end
    end
    object CallTagGroupBox: TGroupBox
      Left = 12
      Top = 4
      Width = 551
      Height = 317
      Caption = 'Call Tag '
      Color = clMenu
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clRed
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentColor = False
      ParentFont = False
      TabOrder = 17
      Visible = False
      object Label124: TLabel
        Left = 36
        Top = 36
        Width = 80
        Height = 16
        Caption = 'Reader ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label125: TLabel
        Left = 56
        Top = 76
        Width = 60
        Height = 16
        Caption = 'Host ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label126: TLabel
        Left = 24
        Top = 114
        Width = 93
        Height = 16
        Caption = 'Repeater ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        Visible = False
      end
      object CallTagReptIDEdit: TEdit
        Left = 118
        Top = 111
        Width = 83
        Height = 24
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 0
        Visible = False
      end
      object CallTagReaderIDComboBox: TComboBox
        Left = 118
        Top = 32
        Width = 85
        Height = 24
        Cursor = crHandPoint
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ItemHeight = 16
        ParentFont = False
        Sorted = True
        TabOrder = 1
      end
      object CallTagHostIDEdit: TEdit
        Left = 118
        Top = 71
        Width = 83
        Height = 24
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 2
      end
      object GroupBox40: TGroupBox
        Left = 224
        Top = 26
        Width = 143
        Height = 199
        Caption = 'Tag ID'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 3
        object CallTagAnyTagIDRadioButton: TRadioButton
          Left = 20
          Top = 164
          Width = 101
          Height = 17
          Cursor = crHandPoint
          Caption = 'Any Tag ID'
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          TabStop = True
          OnClick = CallTagAnyTagIDRadioButtonClick
        end
        object CallTagIDRadioButton: TRadioButton
          Left = 20
          Top = 30
          Width = 83
          Height = 17
          Cursor = crHandPoint
          Caption = 'Tag ID: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          OnClick = CallTagIDRadioButtonClick
        end
        object CallTagIDEdit: TEdit
          Left = 22
          Top = 48
          Width = 103
          Height = 24
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          ReadOnly = True
          TabOrder = 2
        end
        object CallTagIDRangeComboBox: TComboBox
          Left = 22
          Top = 116
          Width = 75
          Height = 24
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 16
          ParentFont = False
          TabOrder = 3
          OnExit = CallTagIDRangeComboBoxExit
          Items.Strings = (
            '2'
            '3'
            '4'
            '5'
            '6'
            '7'
            '8'
            '10'
            '15'
            '20'
            '40'
            '60'
            '80'
            '100'
            '150'
            '200')
        end
        object CallTagIDRangeRadioButton: TRadioButton
          Left = 20
          Top = 98
          Width = 71
          Height = 17
          Caption = 'Range: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 4
          OnClick = CallTagIDRangeRadioButtonClick
        end
      end
      object GroupBox41: TGroupBox
        Left = 384
        Top = 26
        Width = 153
        Height = 199
        Caption = 'Tag Type'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 4
        object CallTagTypeComboBox: TComboBox
          Left = 12
          Top = 32
          Width = 131
          Height = 24
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 16
          ParentFont = False
          TabOrder = 0
          Text = 'Any Type'
          Items.Strings = (
            'Factory'
            'Any Type')
        end
      end
      object CallTagBroadcastRdrCheckBox: TCheckBox
        Left = 24
        Top = 278
        Width = 185
        Height = 17
        Cursor = crHandPoint
        Caption = 'Broadcast All Readers'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 5
        OnClick = CallTagBroadcastRdrCheckBoxClick
      end
      object GroupBox42: TGroupBox
        Left = 22
        Top = 166
        Width = 181
        Height = 81
        Caption = 'Tag Response Delay'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 6
        object CallTagRNShortRadioButton: TRadioButton
          Left = 18
          Top = 46
          Width = 109
          Height = 17
          Cursor = crHandPoint
          Caption = 'Short Random '
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          TabStop = True
        end
        object CallTagRNLongRadioButton: TRadioButton
          Left = 18
          Top = 24
          Width = 109
          Height = 17
          Cursor = crHandPoint
          Caption = 'Long Random '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
        end
      end
      object GroupBox71: TGroupBox
        Left = 384
        Top = 234
        Width = 153
        Height = 71
        Caption = 'Tag LED'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 7
        object CallTagDisableLEDRadioButton: TRadioButton
          Left = 22
          Top = 44
          Width = 87
          Height = 17
          Cursor = crHandPoint
          Caption = 'Disable '
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          TabStop = True
        end
        object CallTagEnableLEDRadioButton: TRadioButton
          Left = 22
          Top = 24
          Width = 79
          Height = 17
          Cursor = crHandPoint
          Caption = 'Enable'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
        end
      end
      object GroupBox100: TGroupBox
        Left = 222
        Top = 234
        Width = 147
        Height = 71
        Caption = 'Tag Speaker'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 8
        object CallTagDisableSpeakerRadioButton: TRadioButton
          Left = 22
          Top = 44
          Width = 87
          Height = 17
          Cursor = crHandPoint
          Caption = 'Disable '
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          TabStop = True
        end
        object CallTagEnableSpeakerRadioButton: TRadioButton
          Left = 22
          Top = 24
          Width = 79
          Height = 17
          Cursor = crHandPoint
          Caption = 'Enable'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
        end
      end
    end
    object RelayGroupBox: TGroupBox
      Left = 12
      Top = 6
      Width = 551
      Height = 315
      Caption = 'Output Relay '
      Color = clMenu
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clRed
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentColor = False
      ParentFont = False
      TabOrder = 13
      Visible = False
      object Label94: TLabel
        Left = 42
        Top = 70
        Width = 80
        Height = 16
        Caption = 'Reader ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label95: TLabel
        Left = 62
        Top = 136
        Width = 60
        Height = 16
        Caption = 'Host ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label96: TLabel
        Left = 28
        Top = 200
        Width = 93
        Height = 16
        Caption = 'Repeater ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        Visible = False
      end
      object Edit2: TEdit
        Left = 123
        Top = 195
        Width = 82
        Height = 24
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 0
        Visible = False
      end
      object RelayReaderIDComboBox: TComboBox
        Left = 122
        Top = 66
        Width = 83
        Height = 24
        Cursor = crHandPoint
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ItemHeight = 16
        ParentFont = False
        Sorted = True
        TabOrder = 1
      end
      object RelayHostIDEdit: TEdit
        Left = 123
        Top = 131
        Width = 82
        Height = 24
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 2
      end
      object RelayListView: TListView
        Left = 240
        Top = 118
        Width = 291
        Height = 165
        Color = clInfoBk
        Columns = <
          item
            Width = 1
          end
          item
            Alignment = taCenter
            Caption = 'Reader ID'
            Width = 89
          end
          item
            Alignment = taCenter
            Caption = 'Output Relay 1'
            Width = 89
          end
          item
            Alignment = taCenter
            Caption = 'Output Relay 2'
            Width = 89
          end>
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        GridLines = True
        ReadOnly = True
        ParentFont = False
        TabOrder = 3
        ViewStyle = vsReport
        OnCustomDrawItem = RelayListViewCustomDrawItem
        OnCustomDrawSubItem = RelayListViewCustomDrawSubItem
      end
      object RelayBroadcastRdrCheckBox: TCheckBox
        Left = 12
        Top = 286
        Width = 195
        Height = 17
        Caption = 'Broadcast All Readers'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 4
        OnClick = RelayBroadcastRdrCheckBoxClick
      end
      object RelayKeepListCheckBox: TCheckBox
        Left = 242
        Top = 288
        Width = 113
        Height = 17
        Caption = 'Keep List Items'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 5
      end
      object RelayClearBitBtn: TBitBtn
        Left = 456
        Top = 286
        Width = 75
        Height = 21
        Caption = 'Clear'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 6
        OnClick = RelayClearBitBtnClick
      end
      object GroupBox97: TGroupBox
        Left = 240
        Top = 36
        Width = 139
        Height = 75
        Caption = 'Output Relay 1'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 7
        object EnableRelay1RadioButton: TRadioButton
          Left = 12
          Top = 28
          Width = 109
          Height = 17
          Caption = 'Enable Relay '
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          TabStop = True
          OnClick = EnableRelay1RadioButtonClick
        end
        object DisableRelay1RadioButton: TRadioButton
          Left = 12
          Top = 50
          Width = 115
          Height = 17
          Caption = 'Disable Relay '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          OnClick = DisableRelay1RadioButtonClick
        end
      end
      object GroupBox98: TGroupBox
        Left = 392
        Top = 36
        Width = 139
        Height = 75
        Caption = 'Output Relay 2'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 8
        object EnableRelay2RadioButton: TRadioButton
          Left = 12
          Top = 28
          Width = 115
          Height = 17
          Caption = 'Enable Relay '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          OnClick = EnableRelay2RadioButtonClick
        end
        object DisableRelay2RadioButton: TRadioButton
          Left = 12
          Top = 50
          Width = 115
          Height = 17
          Caption = 'Disable Relay '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          OnClick = DisableRelay2RadioButtonClick
        end
      end
    end
    object InputGroupBox: TGroupBox
      Left = 12
      Top = 6
      Width = 551
      Height = 315
      Caption = 'Input Status'
      Color = clMenu
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clRed
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentColor = False
      ParentFont = False
      TabOrder = 21
      Visible = False
      object Label159: TLabel
        Left = 42
        Top = 70
        Width = 80
        Height = 16
        Caption = 'Reader ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label160: TLabel
        Left = 62
        Top = 136
        Width = 60
        Height = 16
        Caption = 'Host ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label161: TLabel
        Left = 28
        Top = 200
        Width = 93
        Height = 16
        Caption = 'Repeater ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        Visible = False
      end
      object InputRepeaterIDEdit: TEdit
        Left = 123
        Top = 195
        Width = 82
        Height = 24
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 0
        Visible = False
      end
      object InputReaderIDComboBox: TComboBox
        Left = 120
        Top = 66
        Width = 83
        Height = 24
        Cursor = crHandPoint
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ItemHeight = 16
        ParentFont = False
        Sorted = True
        TabOrder = 1
      end
      object InputHostIDEdit: TEdit
        Left = 123
        Top = 131
        Width = 82
        Height = 24
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 2
      end
      object InputListView: TListView
        Left = 232
        Top = 182
        Width = 305
        Height = 109
        Color = clInfoBk
        Columns = <
          item
            Width = 1
          end
          item
            Alignment = taCenter
            Caption = 'Rdr ID'
            Width = 55
          end
          item
            Alignment = taCenter
            Caption = 'Input 1'
            Width = 113
          end
          item
            Alignment = taCenter
            Caption = 'Input 2'
            Width = 113
          end>
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        GridLines = True
        ParentFont = False
        SortType = stBoth
        TabOrder = 3
        ViewStyle = vsReport
        OnColumnClick = InputListViewColumnClick
        OnCompare = InputListViewCompare
      end
      object InputBroadCastCheckBox: TCheckBox
        Left = 26
        Top = 266
        Width = 195
        Height = 17
        Caption = 'Broadcast All Readers'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 4
        OnClick = InputBroadCastCheckBoxClick
      end
      object InputKeepItemsCheckBox: TCheckBox
        Left = 232
        Top = 292
        Width = 113
        Height = 17
        Caption = 'Keep List Items'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 5
      end
      object InputClearBitBtn: TBitBtn
        Left = 462
        Top = 292
        Width = 75
        Height = 21
        Caption = 'Clear'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 6
        OnClick = InputClearBitBtnClick
      end
      object GroupBox2: TGroupBox
        Left = 232
        Top = 12
        Width = 305
        Height = 81
        Caption = 'Input 1'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 7
        object Label43: TLabel
          Left = 32
          Top = 22
          Width = 170
          Height = 13
          Caption = 'Do Not Report Status Change'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label48: TLabel
          Left = 32
          Top = 42
          Width = 126
          Height = 13
          Caption = 'Report Status Change'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label224: TLabel
          Left = 32
          Top = 62
          Width = 64
          Height = 13
          Caption = 'Supervised'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Input1NoReportRadioButton: TRadioButton
          Left = 16
          Top = 20
          Width = 17
          Height = 17
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
        end
        object Input1ReportRadioButton: TRadioButton
          Left = 16
          Top = 40
          Width = 15
          Height = 17
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
        end
        object Input1NoChangeRadioButton: TRadioButton
          Left = 246
          Top = 18
          Width = 39
          Height = 17
          Caption = 'No Change'
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 2
          TabStop = True
          Visible = False
        end
        object Input1SupervisedCheckBox: TCheckBox
          Left = 16
          Top = 60
          Width = 15
          Height = 17
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 3
        end
        object Input1ModifyCheckBox: TCheckBox
          Left = 232
          Top = 60
          Width = 69
          Height = 17
          Caption = 'Modify'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clOlive
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 4
          OnClick = Input1ModifyCheckBoxClick
        end
      end
      object GroupBox16: TGroupBox
        Left = 232
        Top = 94
        Width = 305
        Height = 81
        Caption = 'Input 2'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 8
        object Label225: TLabel
          Left = 32
          Top = 22
          Width = 170
          Height = 13
          Caption = 'Do Not Report Status Change'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label228: TLabel
          Left = 32
          Top = 42
          Width = 126
          Height = 13
          Caption = 'Report Status Change'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label230: TLabel
          Left = 32
          Top = 62
          Width = 64
          Height = 13
          Caption = 'Supervised'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Input2NoReportRadioButton: TRadioButton
          Left = 16
          Top = 20
          Width = 15
          Height = 17
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
        end
        object Input2ReportRadioButton: TRadioButton
          Left = 16
          Top = 40
          Width = 15
          Height = 17
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
        end
        object Input2NoChangeRadioButton: TRadioButton
          Left = 250
          Top = 20
          Width = 39
          Height = 17
          Caption = 'No Change'
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 2
          TabStop = True
          Visible = False
        end
        object Input2SupervisedCheckBox: TCheckBox
          Left = 16
          Top = 60
          Width = 17
          Height = 17
          Caption = 'Supervised'
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 3
        end
        object Input2ModifyCheckBox: TCheckBox
          Left = 232
          Top = 60
          Width = 71
          Height = 17
          Caption = 'Modify'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clOlive
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 4
          OnClick = Input2ModifyCheckBoxClick
        end
      end
    end
    object GeneralGroupBox: TGroupBox
      Left = 12
      Top = 6
      Width = 549
      Height = 315
      TabOrder = 1
      object GroupBox4: TGroupBox
        Left = 10
        Top = 9
        Width = 87
        Height = 192
        Caption = 'Tag Type'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
        TabOrder = 0
        object AccessCtrlRadioButton: TRadioButton
          Left = 11
          Top = 22
          Width = 62
          Height = 17
          Caption = 'Access '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clWindowText
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          TabOrder = 0
          OnClick = AccessCtrlRadioButtonClick
        end
        object InvetRadioButton: TRadioButton
          Left = 11
          Top = 64
          Width = 68
          Height = 17
          Caption = 'Inventory '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clWindowText
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          TabOrder = 1
          OnClick = InvetRadioButtonClick
        end
        object AssetCtrlRadioButton: TRadioButton
          Left = 11
          Top = 43
          Width = 50
          Height = 17
          Caption = 'Asset '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clWindowText
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          TabOrder = 2
          OnClick = AssetCtrlRadioButtonClick
        end
        object ReservedRadioButton2: TRadioButton
          Left = 11
          Top = 124
          Width = 70
          Height = 17
          Caption = 'Reserved'
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clWindowText
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          TabOrder = 3
        end
        object CarRadioButton: TRadioButton
          Left = 11
          Top = 84
          Width = 40
          Height = 17
          Caption = 'Car '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clWindowText
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          TabOrder = 4
          OnClick = CarRadioButtonClick
        end
        object AnyTagRadioButton: TRadioButton
          Left = 11
          Top = 104
          Width = 70
          Height = 17
          Caption = 'Any Type'
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clWindowText
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          TabOrder = 5
          TabStop = True
          OnClick = AnyTagRadioButtonClick
        end
        object ReservedRadioButton: TRadioButton
          Left = 11
          Top = 144
          Width = 70
          Height = 17
          Caption = 'Reserved'
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clWindowText
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          TabOrder = 6
        end
        object ReservedRadioButton1: TRadioButton
          Left = 11
          Top = 164
          Width = 70
          Height = 17
          Caption = 'Reserved'
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clWindowText
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          TabOrder = 7
        end
      end
      object GroupBox5: TGroupBox
        Left = 108
        Top = 16
        Width = 147
        Height = 75
        Caption = 'Tag ID'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
        TabOrder = 1
        object NewIDEdit: TEdit
          Left = 94
          Top = 16
          Width = 43
          Height = 21
          CharCase = ecUpperCase
          Color = clMenu
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          MaxLength = 10
          ParentFont = False
          TabOrder = 0
        end
        object TagIDEdit: TEdit
          Left = 94
          Top = 42
          Width = 43
          Height = 21
          CharCase = ecUpperCase
          Color = clMenu
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          MaxLength = 10
          ParentFont = False
          TabOrder = 1
          OnChange = TagIDEditChange
          OnExit = TagIDEditExit
        end
        object TagIDRadioButton: TCheckBox
          Left = 12
          Top = 44
          Width = 51
          Height = 17
          Caption = 'Tag ID '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          TabOrder = 2
          OnClick = TagIDRadioButtonClick
          OnMouseDown = TagIDRadioButtonMouseDown
        end
        object NewIDRadioButton: TCheckBox
          Left = 12
          Top = 22
          Width = 77
          Height = 17
          Caption = 'New Tag ID'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          TabOrder = 3
        end
      end
      object DisplayFormatBox: TGroupBox
        Left = 409
        Top = 242
        Width = 126
        Height = 41
        Caption = 'Display Format'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
        TabOrder = 2
        Visible = False
        object HexRadioButton: TRadioButton
          Left = 7
          Top = 16
          Width = 41
          Height = 17
          Caption = 'Hex'
          Checked = True
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          TabOrder = 0
          TabStop = True
          Visible = False
          OnClick = HexRadioButtonClick
          OnMouseUp = HexRadioButtonMouseUp
        end
        object DecRadioButton: TRadioButton
          Left = 60
          Top = 17
          Width = 60
          Height = 17
          Caption = 'Decimal'
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          TabOrder = 1
          Visible = False
          OnClick = DecRadioButtonClick
          OnMouseUp = DecRadioButtonMouseUp
        end
      end
      object GroupBox10: TGroupBox
        Left = 361
        Top = 12
        Width = 142
        Height = 309
        Caption = 'Peripherals ID'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
        TabOrder = 3
        object Label20: TLabel
          Left = 10
          Top = 20
          Width = 49
          Height = 13
          Caption = 'Reader ID'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label21: TLabel
          Left = 10
          Top = 60
          Width = 36
          Height = 13
          Caption = 'Host ID'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label22: TLabel
          Left = 10
          Top = 140
          Width = 59
          Height = 13
          Caption = 'Field Gen ID'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label23: TLabel
          Left = 8
          Top = 100
          Width = 61
          Height = 13
          Caption = 'Repeater ID '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label34: TLabel
          Left = 94
          Top = 20
          Width = 36
          Height = 13
          Caption = 'New ID'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          Visible = False
        end
        object Label35: TLabel
          Left = 94
          Top = 60
          Width = 36
          Height = 13
          Caption = 'New ID'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          Visible = False
        end
        object Label36: TLabel
          Left = 96
          Top = 98
          Width = 36
          Height = 13
          Caption = 'New ID'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          Visible = False
        end
        object Label38: TLabel
          Left = 8
          Top = 194
          Width = 65
          Height = 13
          Caption = 'Reader Type '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          Visible = False
        end
        object HostIDEdit: TEdit
          Left = 9
          Top = 73
          Width = 58
          Height = 24
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          MaxLength = 3
          ParentFont = False
          TabOrder = 0
          OnChange = HostIDEditChange
          OnExit = RdrCodeVerHostIDEdit
        end
        object FieldGenIDEdit: TEdit
          Left = 9
          Top = 153
          Width = 58
          Height = 24
          Color = clWhite
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          MaxLength = 3
          ParentFont = False
          TabOrder = 1
          Text = '0'
          OnChange = FieldGenIDEditChange
          OnExit = FieldGenIDEditExit
        end
        object RepeaterIDEdit: TEdit
          Left = 9
          Top = 113
          Width = 58
          Height = 24
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -15
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          MaxLength = 3
          ParentFont = False
          ReadOnly = True
          TabOrder = 2
          Text = '0'
          OnChange = RepeaterIDEditChange
          OnExit = RepeaterIDEditExit
        end
        object NewReaderIDCheckBox: TCheckBox
          Left = 76
          Top = 36
          Width = 17
          Height = 17
          Caption = 'NewReaderIDCheckBox'
          TabOrder = 3
          Visible = False
          OnClick = NewReaderIDCheckBoxClick
        end
        object NewReaderIDEdit: TEdit
          Left = 94
          Top = 33
          Width = 39
          Height = 24
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          MaxLength = 5
          ParentFont = False
          ReadOnly = True
          TabOrder = 4
          Visible = False
          OnChange = NewReaderIDEditChange
          OnExit = NewReaderIDEditExit
        end
        object NewHostIDCheckBox: TCheckBox
          Left = 76
          Top = 76
          Width = 17
          Height = 17
          Caption = 'CheckBox1'
          TabOrder = 5
          Visible = False
          OnClick = NewHostIDCheckBoxClick
        end
        object NewHostIDEdit: TEdit
          Left = 94
          Top = 73
          Width = 39
          Height = 24
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          MaxLength = 3
          ParentFont = False
          ReadOnly = True
          TabOrder = 6
          Visible = False
          OnChange = NewHostIDEditChange
          OnExit = NewHostIDEditExit
        end
        object NewRepeaterIDCheckBox: TCheckBox
          Left = 76
          Top = 114
          Width = 17
          Height = 17
          Caption = 'CheckBox1'
          Enabled = False
          TabOrder = 7
          Visible = False
          OnClick = NewRepeaterIDCheckBoxClick
        end
        object NewRepeaterIDEdit: TEdit
          Left = 94
          Top = 111
          Width = 39
          Height = 24
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          MaxLength = 3
          ParentFont = False
          ReadOnly = True
          TabOrder = 8
          Visible = False
          OnChange = NewRepeaterIDEditChange
          OnExit = NewRepeaterIDEditExit
        end
        object ReaderIDComboBox: TComboBox
          Left = 10
          Top = 32
          Width = 59
          Height = 24
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 16
          ParentFont = False
          Sorted = True
          TabOrder = 9
          OnChange = ReaderIDComboBoxChange
          OnExit = ReaderIDComboBoxExit
        end
        object ReaderTypeComboBox: TComboBox
          Left = 8
          Top = 208
          Width = 125
          Height = 21
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ItemHeight = 13
          ParentFont = False
          TabOrder = 10
          Text = 'No Change'
          Visible = False
          Items.Strings = (
            'No Change'
            'Programming Station'
            'Standard Reader'
            'Access Control Reader'
            'Small RF Reader'
            'PDA Reader')
        end
        object RedaerNoBroadcastCheckBox: TCheckBox
          Left = 8
          Top = 240
          Width = 125
          Height = 17
          Caption = 'Respond to broadcast'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          TabOrder = 11
          Visible = False
        end
        object ReaderEnablePowerupCheckBox: TCheckBox
          Left = 8
          Top = 262
          Width = 125
          Height = 17
          Caption = 'Enable at power up'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          TabOrder = 12
          Visible = False
        end
        object ReaderNoRSSICheckBox: TCheckBox
          Left = 8
          Top = 284
          Width = 81
          Height = 17
          Caption = 'Send RSSI '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          TabOrder = 13
          Visible = False
        end
        object ReaderDisableCheckBox: TCheckBox
          Left = 8
          Top = 306
          Width = 99
          Height = 17
          Caption = 'Reader enable'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          TabOrder = 14
          Visible = False
        end
      end
      object GroupBox8: TGroupBox
        Left = 140
        Top = 136
        Width = 181
        Height = 95
        Caption = 'Tag Random Number'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clOlive
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 4
        object Panel4: TPanel
          Left = 96
          Top = 26
          Width = 75
          Height = 41
          BevelOuter = bvLowered
          TabOrder = 0
          object RNShortRadioButton: TRadioButton
            Left = 6
            Top = 2
            Width = 63
            Height = 17
            Caption = 'Short'
            Checked = True
            Enabled = False
            Font.Charset = DEFAULT_CHARSET
            Font.Color = clTeal
            Font.Height = -13
            Font.Name = 'MS Sans Serif'
            Font.Style = [fsBold]
            ParentFont = False
            TabOrder = 0
            TabStop = True
          end
          object RNLongRadioButton: TRadioButton
            Left = 6
            Top = 22
            Width = 61
            Height = 17
            Caption = 'Long'
            Enabled = False
            Font.Charset = DEFAULT_CHARSET
            Font.Color = clTeal
            Font.Height = -13
            Font.Name = 'MS Sans Serif'
            Font.Style = [fsBold]
            ParentFont = False
            TabOrder = 1
          end
        end
        object RNChangeRadioButton: TRadioButton
          Left = 10
          Top = 38
          Width = 83
          Height = 17
          Caption = 'Change '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          OnClick = RNChangeRadioButtonClick
        end
        object RNNoChangeRadioButton: TRadioButton
          Left = 10
          Top = 70
          Width = 105
          Height = 17
          Caption = 'No Change '
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 2
          TabStop = True
          OnClick = RNNoChangeRadioButtonClick
        end
      end
    end
    object EnableReaderGroupBox: TGroupBox
      Left = 12
      Top = 6
      Width = 551
      Height = 315
      Caption = 'Enable Reader'
      Color = clMenu
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clRed
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentColor = False
      ParentFont = False
      TabOrder = 9
      Visible = False
      OnClick = EnableReaderGroupBoxClick
      object Label82: TLabel
        Left = 38
        Top = 64
        Width = 80
        Height = 16
        Caption = 'Reader ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label83: TLabel
        Left = 58
        Top = 112
        Width = 60
        Height = 16
        Caption = 'Host ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label84: TLabel
        Left = 24
        Top = 162
        Width = 93
        Height = 16
        Caption = 'Repeater ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        Visible = False
      end
      object EnableReaderRepIDEdit: TEdit
        Left = 119
        Top = 159
        Width = 82
        Height = 24
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 0
        Visible = False
      end
      object EnableReaderIDComboBox: TComboBox
        Left = 118
        Top = 60
        Width = 83
        Height = 24
        Cursor = crHandPoint
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ItemHeight = 16
        ParentFont = False
        Sorted = True
        TabOrder = 1
      end
      object EnableReaderHostIDEdit: TEdit
        Left = 119
        Top = 109
        Width = 82
        Height = 24
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 2
      end
      object EnableReaderListView: TListView
        Left = 392
        Top = 36
        Width = 121
        Height = 247
        Cursor = crHandPoint
        Color = clInfoBk
        Columns = <
          item
            Width = 1
          end
          item
            Alignment = taCenter
            Caption = 'Reader ID'
            Width = 115
          end>
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clGreen
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        GridLines = True
        HideSelection = False
        ReadOnly = True
        RowSelect = True
        ParentFont = False
        SortType = stBoth
        TabOrder = 3
        ViewStyle = vsReport
        OnColumnClick = EnableReaderListViewColumnClick
        OnCompare = EnableReaderListViewCompare
      end
      object EnableReaderClearBitBtn: TBitBtn
        Left = 418
        Top = 284
        Width = 75
        Height = 21
        Cursor = crHandPoint
        Caption = 'Clear'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 4
        OnClick = EnableReaderClearBitBtnClick
      end
      object EnableReaderBroadcastRdrCheckBox: TCheckBox
        Left = 14
        Top = 286
        Width = 187
        Height = 17
        Cursor = crHandPoint
        Caption = 'Broadcast All Readers'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 5
        OnClick = EnableReaderBroadcastRdrCheckBoxClick
      end
      object EnableReaderBroadcastReptCheckBox: TCheckBox
        Left = 22
        Top = 212
        Width = 197
        Height = 17
        Cursor = crHandPoint
        Caption = 'Broadcast All Repeaters'
        Enabled = False
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 6
        Visible = False
      end
    end
    object DisableReaderGroupBox: TGroupBox
      Left = 12
      Top = 6
      Width = 551
      Height = 315
      Caption = 'Disable Reader'
      Color = clMenu
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clRed
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentColor = False
      ParentFont = False
      TabOrder = 10
      Visible = False
      object Label85: TLabel
        Left = 38
        Top = 64
        Width = 80
        Height = 16
        Caption = 'Reader ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label86: TLabel
        Left = 58
        Top = 112
        Width = 60
        Height = 16
        Caption = 'Host ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label87: TLabel
        Left = 24
        Top = 162
        Width = 93
        Height = 16
        Caption = 'Repeater ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        Visible = False
      end
      object DisableReaderRepIDEdit: TEdit
        Left = 119
        Top = 159
        Width = 82
        Height = 24
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 0
        Visible = False
      end
      object DisableReaderIDComboBox: TComboBox
        Left = 118
        Top = 60
        Width = 83
        Height = 24
        Cursor = crHandPoint
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ItemHeight = 16
        ParentFont = False
        Sorted = True
        TabOrder = 1
      end
      object DisableReaderHostIDEdit: TEdit
        Left = 119
        Top = 109
        Width = 82
        Height = 24
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 2
      end
      object DisableReaderListView: TListView
        Left = 392
        Top = 36
        Width = 121
        Height = 247
        Cursor = crHandPoint
        Color = clInfoBk
        Columns = <
          item
            Width = 1
          end
          item
            Alignment = taCenter
            Caption = 'Reader ID'
            Width = 115
          end>
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clGreen
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        GridLines = True
        HideSelection = False
        ReadOnly = True
        RowSelect = True
        ParentFont = False
        SortType = stBoth
        TabOrder = 3
        ViewStyle = vsReport
        OnColumnClick = DisableReaderListViewColumnClick
        OnCompare = DisableReaderListViewCompare
      end
      object DisableReaderClearBitBtn: TBitBtn
        Left = 416
        Top = 284
        Width = 75
        Height = 21
        Cursor = crHandPoint
        Caption = 'Clear'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 4
        OnClick = DisableReaderClearBitBtnClick
      end
      object DisableReaderBroadcastRdrCheckBox: TCheckBox
        Left = 14
        Top = 284
        Width = 187
        Height = 17
        Cursor = crHandPoint
        Caption = 'Broadcast All Readers'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 5
        OnClick = DisableReaderBroadcastRdrCheckBoxClick
      end
      object DisableReaderBroadcastReptCheckBox: TCheckBox
        Left = 22
        Top = 214
        Width = 197
        Height = 17
        Cursor = crHandPoint
        Caption = 'Broadcast All Repeaters'
        Enabled = False
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 6
        Visible = False
      end
    end
    object QueryReaderGroupBox: TGroupBox
      Left = 12
      Top = 6
      Width = 551
      Height = 315
      Caption = 'Query Reader'
      Color = clMenu
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clRed
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentColor = False
      ParentFont = False
      TabOrder = 11
      Visible = False
      object Label25: TLabel
        Left = 14
        Top = 64
        Width = 80
        Height = 16
        Caption = 'Reader ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label26: TLabel
        Left = 32
        Top = 112
        Width = 60
        Height = 16
        Caption = 'Host ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label29: TLabel
        Left = 10
        Top = 162
        Width = 93
        Height = 16
        Caption = 'Repeater ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        Visible = False
      end
      object QueryReaderRepIDEdit: TEdit
        Left = 103
        Top = 159
        Width = 40
        Height = 24
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 0
        Visible = False
      end
      object QueryReaderIDComboBox: TComboBox
        Left = 94
        Top = 60
        Width = 73
        Height = 24
        Cursor = crHandPoint
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ItemHeight = 16
        ParentFont = False
        Sorted = True
        TabOrder = 1
      end
      object QueryReaderHostIDEdit: TEdit
        Left = 93
        Top = 109
        Width = 72
        Height = 24
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 2
      end
      object QueryReaderListView: TListView
        Left = 178
        Top = 36
        Width = 361
        Height = 233
        Cursor = crHandPoint
        Color = clInfoBk
        Columns = <
          item
            Width = 1
          end
          item
            Alignment = taCenter
            Caption = 'ID'
            Width = 40
          end
          item
            Alignment = taCenter
            Caption = 'Type'
            Width = 110
          end
          item
            Alignment = taCenter
            Caption = 'Broadcast'
            Width = 60
          end
          item
            Alignment = taCenter
            Caption = 'Enable Pw Up'
            Width = 80
          end
          item
            Alignment = taCenter
            Caption = 'Send RSSI'
            Width = 65
          end>
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clGreen
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        GridLines = True
        HideSelection = False
        ReadOnly = True
        RowSelect = True
        ParentFont = False
        SortType = stBoth
        TabOrder = 3
        ViewStyle = vsReport
        OnColumnClick = QueryReaderListViewColumnClick
        OnCompare = QueryReaderListViewCompare
      end
      object QueryReaderClearBitBtn: TBitBtn
        Left = 332
        Top = 270
        Width = 75
        Height = 23
        Cursor = crHandPoint
        Caption = 'Clear'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 4
        OnClick = QueryReaderClearBitBtnClick
      end
      object QueryReaderBroadcastRdrCheckBox: TCheckBox
        Left = 12
        Top = 286
        Width = 181
        Height = 17
        Cursor = crHandPoint
        Caption = 'Broadcast All Readers'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 5
        OnClick = QueryReaderBroadcastRdrCheckBoxClick
      end
      object QueryReaderBroadcastReptCheckBox: TCheckBox
        Left = 10
        Top = 224
        Width = 147
        Height = 17
        Cursor = crHandPoint
        Caption = 'Broadcast All Repeaters'
        Enabled = False
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 6
        Visible = False
      end
    end
    object ConfigReaderTxTimeGroupBox: TGroupBox
      Left = 12
      Top = 6
      Width = 551
      Height = 315
      Caption = 'Configure Reader && Field Generator Transmit Time'
      Color = clMenu
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clRed
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentColor = False
      ParentFont = False
      TabOrder = 6
      Visible = False
      object Label49: TLabel
        Left = 36
        Top = 48
        Width = 80
        Height = 16
        Caption = 'Reader ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label50: TLabel
        Left = 58
        Top = 114
        Width = 60
        Height = 16
        Caption = 'Host ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label51: TLabel
        Left = 26
        Top = 176
        Width = 93
        Height = 16
        Caption = 'Repeater ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label53: TLabel
        Left = 20
        Top = 242
        Width = 99
        Height = 16
        Caption = 'Field Gen  ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object ConfigTxTimeReaderRadioButton: TRadioButton
        Left = 142
        Top = 286
        Width = 87
        Height = 17
        Cursor = crHandPoint
        Caption = 'Reader'
        Checked = True
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clOlive
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 0
        TabStop = True
        OnClick = ConfigTxTimeReaderRadioButtonClick
      end
      object GroupBox20: TGroupBox
        Left = 238
        Top = 36
        Width = 297
        Height = 105
        TabOrder = 1
        object Label47: TLabel
          Left = 9
          Top = 38
          Width = 108
          Height = 16
          Caption = 'Transmit Time: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object TxTimeComboBox: TComboBox
          Left = 118
          Top = 34
          Width = 83
          Height = 24
          Cursor = crHandPoint
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 16
          ParentFont = False
          TabOrder = 0
          OnChange = ReaderIDComboBoxChange
        end
        object TxTimeSecRadioButton: TRadioButton
          Left = 8
          Top = 74
          Width = 57
          Height = 19
          Cursor = crHandPoint
          Caption = 'Sec. '
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          TabStop = True
          OnClick = TxTimeSecRadioButtonClick
        end
        object TxTimeAllRadioButton: TRadioButton
          Left = 78
          Top = 74
          Width = 79
          Height = 19
          Cursor = crHandPoint
          Caption = 'All Time'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 2
          OnClick = TxTimeAllRadioButtonClick
        end
      end
      object GroupBox21: TGroupBox
        Left = 238
        Top = 156
        Width = 297
        Height = 105
        TabOrder = 2
        object Label46: TLabel
          Left = 9
          Top = 32
          Width = 79
          Height = 16
          Caption = 'Wait Time: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object WaitTimeComboBox: TComboBox
          Left = 90
          Top = 28
          Width = 83
          Height = 24
          Cursor = crHandPoint
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 16
          ParentFont = False
          TabOrder = 0
          OnChange = ReaderIDComboBoxChange
        end
        object WaitTimeSecRadioButton: TRadioButton
          Left = 8
          Top = 74
          Width = 57
          Height = 19
          Cursor = crHandPoint
          Caption = 'Sec. '
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          TabStop = True
          OnClick = WaitTimeSecRadioButtonClick
        end
        object WaitTimeMinRadioButton: TRadioButton
          Left = 76
          Top = 74
          Width = 51
          Height = 19
          Cursor = crHandPoint
          Caption = 'Min.'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 2
          OnClick = WaitTimeMinRadioButtonClick
        end
        object WaitTimeHourRadioButton: TRadioButton
          Left = 138
          Top = 74
          Width = 59
          Height = 19
          Cursor = crHandPoint
          Caption = 'Hour'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 3
          OnClick = WaitTimeHourRadioButtonClick
        end
        object WaitTimeAllRadioButton: TRadioButton
          Left = 208
          Top = 74
          Width = 79
          Height = 19
          Cursor = crHandPoint
          Caption = 'All Time'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 4
          OnClick = WaitTimeAllRadioButtonClick
        end
      end
      object ConfigTxTimeFieldGenIDEdit: TEdit
        Left = 119
        Top = 237
        Width = 82
        Height = 24
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 3
        Text = '0'
        OnChange = HostIDEditChange
      end
      object ConfigTxTimeRepeaterIDEdit: TEdit
        Left = 119
        Top = 173
        Width = 82
        Height = 24
        Color = clMenu
        Enabled = False
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 4
        OnChange = HostIDEditChange
      end
      object ConfigTxTimeHostIDEdit: TEdit
        Left = 119
        Top = 109
        Width = 82
        Height = 24
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 5
        OnChange = HostIDEditChange
      end
      object ConfigTxTimeReaderIDComboBox: TComboBox
        Left = 118
        Top = 44
        Width = 83
        Height = 24
        Cursor = crHandPoint
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ItemHeight = 16
        ParentFont = False
        Sorted = True
        TabOrder = 6
        OnChange = ReaderIDComboBoxChange
      end
      object ConfigTxTimeFieldGenRadioButton: TRadioButton
        Left = 288
        Top = 286
        Width = 141
        Height = 17
        Cursor = crHandPoint
        Caption = 'Field Generator'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clOlive
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 7
        OnClick = ConfigTxTimeFieldGenRadioButtonClick
      end
    end
    object ResetReaderGroupBox: TGroupBox
      Left = 12
      Top = 6
      Width = 551
      Height = 315
      Caption = 'Reset Reader'
      Color = clMenu
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clRed
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentColor = False
      ParentFont = False
      TabOrder = 23
      Visible = False
      object Label77: TLabel
        Left = 28
        Top = 40
        Width = 80
        Height = 16
        Caption = 'Reader ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label79: TLabel
        Left = 48
        Top = 84
        Width = 60
        Height = 16
        Caption = 'Host ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label80: TLabel
        Left = 16
        Top = 132
        Width = 93
        Height = 16
        Caption = 'Repeater ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        Visible = False
      end
      object ResetNewReaderIDLabel: TLabel
        Left = 204
        Top = 40
        Width = 114
        Height = 16
        Caption = 'New Reader ID: '
        Enabled = False
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        Visible = False
      end
      object ResetNewHostIDLabel: TLabel
        Left = 224
        Top = 76
        Width = 94
        Height = 16
        Caption = 'New Host ID: '
        Enabled = False
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        Visible = False
      end
      object ResetNewRepeaterIDLabel: TLabel
        Left = 190
        Top = 112
        Width = 127
        Height = 16
        Caption = 'New Repeater ID: '
        Enabled = False
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        Visible = False
      end
      object Label81: TLabel
        Left = 56
        Top = 194
        Width = 97
        Height = 16
        Caption = 'Reader Type:'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        Visible = False
      end
      object ResetRepeaterIDEdit: TEdit
        Left = 109
        Top = 127
        Width = 72
        Height = 24
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 0
        Visible = False
      end
      object ResetReaderIDComboBox: TComboBox
        Left = 108
        Top = 36
        Width = 87
        Height = 24
        Cursor = crHandPoint
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ItemHeight = 16
        ParentFont = False
        Sorted = True
        TabOrder = 1
      end
      object ResetHostIDEdit: TEdit
        Left = 109
        Top = 81
        Width = 84
        Height = 24
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 2
      end
      object ResetListView: TListView
        Left = 402
        Top = 34
        Width = 121
        Height = 247
        Cursor = crHandPoint
        Color = clInfoBk
        Columns = <
          item
            Width = 1
          end
          item
            Alignment = taCenter
            Caption = 'Reader ID'
            Width = 115
          end>
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clGreen
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        GridLines = True
        HideSelection = False
        ReadOnly = True
        RowSelect = True
        ParentFont = False
        SortType = stBoth
        TabOrder = 3
        ViewStyle = vsReport
        OnColumnClick = ResetListViewColumnClick
        OnCompare = ResetListViewCompare
      end
      object ResetClearBitBtn: TBitBtn
        Left = 430
        Top = 284
        Width = 75
        Height = 21
        Cursor = crHandPoint
        Caption = 'Clear'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 4
        OnClick = ResetClearBitBtnClick
      end
      object ResetBroadcastReaderCheckBox: TCheckBox
        Left = 12
        Top = 284
        Width = 187
        Height = 17
        Cursor = crHandPoint
        Caption = 'Broadcast All Readers'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 5
        OnClick = ResetBroadcastReaderCheckBoxClick
      end
      object ResetBroadcastRepeaterCheckBox: TCheckBox
        Left = 8
        Top = 244
        Width = 197
        Height = 17
        Cursor = crHandPoint
        Caption = 'Broadcast All Repeaters'
        Enabled = False
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 6
        Visible = False
        OnClick = ResetBroadcastRepeaterCheckBoxClick
      end
      object ResetModifyReaderCheckBox: TCheckBox
        Left = 18
        Top = 154
        Width = 193
        Height = 17
        Cursor = crHandPoint
        Caption = 'Modify Reader Settings'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clOlive
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 7
        Visible = False
      end
      object ResetNewReaderIDEdit: TEdit
        Left = 319
        Top = 35
        Width = 72
        Height = 24
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 8
        Visible = False
      end
      object ResetNewHostIDEdit: TEdit
        Left = 321
        Top = 71
        Width = 72
        Height = 24
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 9
        Visible = False
      end
      object ResetNewRepeaterIDEdit: TEdit
        Left = 319
        Top = 107
        Width = 72
        Height = 24
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 10
        Visible = False
      end
      object GroupBox24: TGroupBox
        Left = 204
        Top = 180
        Width = 197
        Height = 121
        Caption = 'Reader Configuration'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 11
        Visible = False
        object ResetRespCheckBox: TCheckBox
          Left = 10
          Top = 34
          Width = 179
          Height = 17
          Caption = 'Respond to Broadcast'
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
        end
        object ResetEnablePWCheckBox: TCheckBox
          Left = 10
          Top = 64
          Width = 179
          Height = 17
          Caption = 'Disable at Power Up'
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
        end
        object ResetRSSICheckBox: TCheckBox
          Left = 10
          Top = 92
          Width = 123
          Height = 17
          Caption = 'Send RSSI '
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 2
        end
      end
      object ResetReaderTypeComboBox: TComboBox
        Left = 8
        Top = 212
        Width = 193
        Height = 21
        Color = clMenu
        Enabled = False
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ItemHeight = 13
        ParentFont = False
        TabOrder = 12
        Visible = False
        Items.Strings = (
          'No Change'
          'Programming Station Reader'
          'Standard Reader'
          'Access Control Reader'
          'Small RF Reader'
          'PDA Reader')
      end
    end
    object ReaderCodeVerGroupBox: TGroupBox
      Left = 12
      Top = 6
      Width = 551
      Height = 315
      Caption = 'Reader Code Version'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clRed
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentFont = False
      TabOrder = 24
      Visible = False
      object Label39: TLabel
        Left = 26
        Top = 52
        Width = 80
        Height = 16
        Caption = 'Reader ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label40: TLabel
        Left = 46
        Top = 98
        Width = 60
        Height = 16
        Caption = 'Host ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object RdrCodeVerReaderComboBox: TComboBox
        Left = 108
        Top = 48
        Width = 75
        Height = 24
        Cursor = crHandPoint
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ItemHeight = 16
        ParentFont = False
        Sorted = True
        TabOrder = 0
        OnChange = ReaderIDComboBoxChange
      end
      object RdrCodeVerHostEdit: TEdit
        Left = 109
        Top = 93
        Width = 74
        Height = 24
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 1
        OnChange = HostIDEditChange
      end
      object RdrCodeVerBroadcastRdrCheckBox: TCheckBox
        Left = 8
        Top = 282
        Width = 181
        Height = 17
        Caption = 'Broadcast All Readers'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 2
      end
      object RdrCodeVerListView: TListView
        Left = 198
        Top = 26
        Width = 341
        Height = 275
        Cursor = crHandPoint
        Color = clInfoBk
        Columns = <
          item
            Width = 1
          end
          item
            Alignment = taCenter
            Caption = 'Reader ID'
            Width = 65
          end
          item
            Alignment = taCenter
            Caption = 'Data Code "C" '
            Width = 90
          end
          item
            Alignment = taCenter
            Caption = 'Prog. Code "D" '
            Width = 90
          end
          item
            Alignment = taCenter
            Caption = 'Host Code  "E"'
            Width = 90
          end>
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clGreen
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        GridLines = True
        HideSelection = False
        ReadOnly = True
        RowSelect = True
        ParentFont = False
        SortType = stBoth
        TabOrder = 3
        ViewStyle = vsReport
      end
    end
    object DownloadRdrGroupBox: TGroupBox
      Left = 12
      Top = 6
      Width = 551
      Height = 315
      Caption = 'Download Reader Firmware'
      Color = clMenu
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clRed
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentColor = False
      ParentFont = False
      TabOrder = 39
      Visible = False
      object Label236: TLabel
        Left = 30
        Top = 40
        Width = 67
        Height = 13
        Caption = 'Reader ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label237: TLabel
        Left = 44
        Top = 74
        Width = 52
        Height = 13
        Caption = 'Host ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label254: TLabel
        Left = 420
        Top = 206
        Width = 27
        Height = 20
        Alignment = taCenter
        AutoSize = False
        Caption = 'C'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -16
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label255: TLabel
        Left = 466
        Top = 206
        Width = 27
        Height = 20
        Alignment = taCenter
        AutoSize = False
        Caption = 'D'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -16
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label256: TLabel
        Left = 510
        Top = 206
        Width = 27
        Height = 20
        Alignment = taCenter
        AutoSize = False
        Caption = 'E'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -16
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object DownloadRdrReaderComboBox: TComboBox
        Left = 98
        Top = 36
        Width = 73
        Height = 21
        Cursor = crHandPoint
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ItemHeight = 13
        ParentFont = False
        Sorted = True
        TabOrder = 0
      end
      object DownloadRdrHostEdit: TEdit
        Left = 99
        Top = 71
        Width = 72
        Height = 21
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 1
      end
      object GroupBox80: TGroupBox
        Left = 14
        Top = 106
        Width = 161
        Height = 113
        Caption = 'Boot Process'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 2
        object DownloadRdrProcessCRadioButton: TRadioButton
          Left = 20
          Top = 30
          Width = 113
          Height = 17
          Caption = 'Process ( C )'
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          TabStop = True
          OnClick = DownloadRdrProcessCRadioButtonClick
        end
        object DownloadRdrProcessDRadioButton: TRadioButton
          Left = 20
          Top = 56
          Width = 113
          Height = 17
          Caption = 'Process ( D )'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          OnClick = DownloadRdrProcessDRadioButtonClick
        end
        object DownloadRdrProcessERadioButton: TRadioButton
          Left = 20
          Top = 82
          Width = 113
          Height = 17
          Caption = 'Process ( E )'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 2
          OnClick = DownloadRdrProcessERadioButtonClick
        end
      end
      object GroupBox81: TGroupBox
        Left = 12
        Top = 230
        Width = 385
        Height = 75
        Caption = 'File'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 3
        object Label238: TLabel
          Left = 6
          Top = 26
          Width = 65
          Height = 13
          Caption = 'File Name: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object DownloadRdrFileNameLabel: TLabel
          Left = 172
          Top = 44
          Width = 86
          Height = 13
          Caption = 'AWI501_C.BIN'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object TestLabel: TLabel
          Left = 8
          Top = 48
          Width = 11
          Height = 16
          Caption = 'T'
        end
        object test2: TLabel
          Left = 72
          Top = 50
          Width = 13
          Height = 16
          Caption = 't2'
        end
        object test3: TLabel
          Left = 126
          Top = 50
          Width = 13
          Height = 16
          Caption = 't3'
        end
        object DownloadRdrFileNameEdit: TEdit
          Left = 70
          Top = 22
          Width = 305
          Height = 21
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
        end
        object DownloadRdrGetFileBitBtn: TBitBtn
          Left = 290
          Top = 46
          Width = 85
          Height = 25
          Caption = 'Get File'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clRed
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          OnClick = DownloadRdrGetFileBitBtnClick
        end
      end
      object DownloadRdrProgressBarE: TProgressBar
        Left = 510
        Top = 34
        Width = 27
        Height = 171
        Min = 0
        Max = 0
        Orientation = pbVertical
        Smooth = True
        Step = 1
        TabOrder = 4
      end
      object GroupBox82: TGroupBox
        Left = 188
        Top = 28
        Width = 215
        Height = 193
        Caption = 'Process Data Info'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 5
        object Label239: TLabel
          Left = 10
          Top = 28
          Width = 66
          Height = 13
          Caption = 'Process (C)'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label240: TLabel
          Left = 88
          Top = 28
          Width = 51
          Height = 13
          Caption = 'Version: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object DownloadRdrPCVerLabel: TLabel
          Left = 138
          Top = 28
          Width = 19
          Height = 13
          Caption = '00 '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label242: TLabel
          Left = 102
          Top = 42
          Width = 36
          Height = 13
          Caption = 'Date: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object DownloadRdrPCDateLabel: TLabel
          Left = 136
          Top = 42
          Width = 51
          Height = 13
          Caption = '00 00 00'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label244: TLabel
          Left = 10
          Top = 68
          Width = 67
          Height = 13
          Caption = 'Process (D)'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label245: TLabel
          Left = 88
          Top = 68
          Width = 51
          Height = 13
          Caption = 'Version: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object DownloadRdrPDVerLabel: TLabel
          Left = 140
          Top = 68
          Width = 19
          Height = 13
          Caption = '00 '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object DownloadRdrPDDateLabel: TLabel
          Left = 140
          Top = 82
          Width = 51
          Height = 13
          Caption = '00 00 00'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label248: TLabel
          Left = 102
          Top = 82
          Width = 36
          Height = 13
          Caption = 'Date: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label249: TLabel
          Left = 12
          Top = 108
          Width = 66
          Height = 13
          Caption = 'Process (E)'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label250: TLabel
          Left = 90
          Top = 108
          Width = 51
          Height = 13
          Caption = 'Version: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object DownloadRdrPEVerLabel: TLabel
          Left = 140
          Top = 108
          Width = 19
          Height = 13
          Caption = '00 '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object DownloadRdrPEDateLabel: TLabel
          Left = 140
          Top = 122
          Width = 51
          Height = 13
          Caption = '00 00 00'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label253: TLabel
          Left = 104
          Top = 122
          Width = 36
          Height = 13
          Caption = 'Date: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object DownloadRdrBootQueryBitBtn: TBitBtn
          Left = 62
          Top = 156
          Width = 89
          Height = 29
          Caption = 'Boot Query'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clRed
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          OnClick = DownloadRdrBootQueryBitBtnClick
        end
        object DownloadRdrTestCheckBox: TCheckBox
          Left = 6
          Top = 162
          Width = 51
          Height = 17
          Caption = 'Test'
          TabOrder = 1
          OnClick = DownloadRdrTestCheckBoxClick
        end
      end
      object DownloadRdrProgressBarD: TProgressBar
        Left = 466
        Top = 34
        Width = 27
        Height = 171
        Min = 0
        Max = 0
        Orientation = pbVertical
        Smooth = True
        Step = 1
        TabOrder = 6
      end
      object DownloadRdrProgressBarC: TProgressBar
        Left = 420
        Top = 34
        Width = 27
        Height = 171
        Min = 0
        Max = 0
        Orientation = pbVertical
        Smooth = True
        Step = 1
        TabOrder = 7
      end
      object GroupBox83: TGroupBox
        Left = 402
        Top = 230
        Width = 139
        Height = 75
        Caption = 'Download Status'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 8
        object Label257: TLabel
          Left = 12
          Top = 20
          Width = 37
          Height = 13
          Caption = 'P (C): '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label258: TLabel
          Left = 12
          Top = 38
          Width = 38
          Height = 13
          Caption = 'P (D): '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label259: TLabel
          Left = 12
          Top = 56
          Width = 37
          Height = 13
          Caption = 'P (E): '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object DownloadLabelC: TLabel
          Left = 50
          Top = 20
          Width = 55
          Height = 13
          Caption = 'Not Done'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object DownloadLabelD: TLabel
          Left = 50
          Top = 38
          Width = 55
          Height = 13
          Caption = 'Not Done'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object DownloadLabelE: TLabel
          Left = 50
          Top = 56
          Width = 55
          Height = 13
          Caption = 'Not Done'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
      end
    end
    object DownloadSFGenGroupBox: TGroupBox
      Left = 12
      Top = 6
      Width = 551
      Height = 315
      Caption = 'Download Smart Field Generator Firmware'
      Color = clMenu
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clRed
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentColor = False
      ParentFont = False
      TabOrder = 41
      Visible = False
      object Label243: TLabel
        Left = 30
        Top = 40
        Width = 67
        Height = 13
        Caption = 'Reader ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label246: TLabel
        Left = 44
        Top = 70
        Width = 52
        Height = 13
        Caption = 'Host ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label260: TLabel
        Left = 420
        Top = 206
        Width = 27
        Height = 20
        Alignment = taCenter
        AutoSize = False
        Caption = 'J'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -16
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label261: TLabel
        Left = 466
        Top = 206
        Width = 27
        Height = 20
        Alignment = taCenter
        AutoSize = False
        Caption = 'K'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -16
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label293: TLabel
        Left = 40
        Top = 102
        Width = 56
        Height = 13
        Caption = 'FGen ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object DownloadSFGenReaderComboBox: TComboBox
        Left = 98
        Top = 36
        Width = 73
        Height = 21
        Cursor = crHandPoint
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ItemHeight = 13
        ParentFont = False
        Sorted = True
        TabOrder = 0
      end
      object DownloadSFGenHostEdit: TEdit
        Left = 99
        Top = 67
        Width = 72
        Height = 21
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 1
      end
      object GroupBox87: TGroupBox
        Left = 14
        Top = 134
        Width = 161
        Height = 85
        Caption = 'Boot Process'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 2
        object DownloadSFGenProcessJRadioButton: TRadioButton
          Left = 20
          Top = 26
          Width = 113
          Height = 17
          Caption = 'Process ( J )'
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          TabStop = True
          OnClick = DownloadRdrProcessCRadioButtonClick
        end
        object DownloadSFGenProcessKRadioButton: TRadioButton
          Left = 20
          Top = 52
          Width = 113
          Height = 17
          Caption = 'Process ( K )'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          OnClick = DownloadRdrProcessDRadioButtonClick
        end
      end
      object GroupBox89: TGroupBox
        Left = 12
        Top = 230
        Width = 385
        Height = 75
        Caption = 'File'
        TabOrder = 3
        object Label265: TLabel
          Left = 6
          Top = 26
          Width = 65
          Height = 13
          Caption = 'File Name: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object DownloadSFGenFileNameLabel: TLabel
          Left = 172
          Top = 44
          Width = 84
          Height = 13
          Caption = 'AWI501_J.BIN'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object DownloadSFGenFileNameEdit: TEdit
          Left = 70
          Top = 22
          Width = 305
          Height = 21
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
        end
        object DownloadSFGenGetFileBitBtn: TBitBtn
          Left = 290
          Top = 46
          Width = 85
          Height = 25
          Caption = 'Get File'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clRed
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          OnClick = DownloadSFGenGetFileBitBtnClick
        end
      end
      object GroupBox90: TGroupBox
        Left = 188
        Top = 28
        Width = 215
        Height = 193
        Caption = 'Process Data Info'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 4
        object Label269: TLabel
          Left = 10
          Top = 28
          Width = 64
          Height = 13
          Caption = 'Process (J)'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label270: TLabel
          Left = 88
          Top = 28
          Width = 51
          Height = 13
          Caption = 'Version: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object DownloadSFGenPJVerLabel: TLabel
          Left = 138
          Top = 28
          Width = 19
          Height = 13
          Caption = '00 '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label272: TLabel
          Left = 102
          Top = 42
          Width = 36
          Height = 13
          Caption = 'Date: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object DownloadSFGenPJDateLabel: TLabel
          Left = 136
          Top = 42
          Width = 51
          Height = 13
          Caption = '00 00 00'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label274: TLabel
          Left = 10
          Top = 68
          Width = 66
          Height = 13
          Caption = 'Process (K)'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label278: TLabel
          Left = 88
          Top = 68
          Width = 51
          Height = 13
          Caption = 'Version: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object DownloadSFGenPKVerLabel: TLabel
          Left = 140
          Top = 68
          Width = 19
          Height = 13
          Caption = '00 '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object DownloadSFGenPKDateLabel: TLabel
          Left = 140
          Top = 82
          Width = 51
          Height = 13
          Caption = '00 00 00'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label281: TLabel
          Left = 102
          Top = 82
          Width = 36
          Height = 13
          Caption = 'Date: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object DownloadSFGenBootQueryBitBtn: TBitBtn
          Left = 62
          Top = 156
          Width = 89
          Height = 29
          Caption = 'Boot Query'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clRed
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          OnClick = DownloadRdrBootQueryBitBtnClick
        end
      end
      object DownloadSFGenProgressBarK: TProgressBar
        Left = 466
        Top = 34
        Width = 27
        Height = 171
        Min = 0
        Max = 0
        Orientation = pbVertical
        Smooth = True
        Step = 1
        TabOrder = 5
      end
      object DownloadSFGenProgressBarJ: TProgressBar
        Left = 420
        Top = 34
        Width = 27
        Height = 171
        Min = 0
        Max = 0
        Orientation = pbVertical
        Smooth = True
        Step = 1
        TabOrder = 6
      end
      object GroupBox91: TGroupBox
        Left = 402
        Top = 230
        Width = 139
        Height = 75
        Caption = 'Download Status'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 7
        object Label287: TLabel
          Left = 12
          Top = 26
          Width = 35
          Height = 13
          Caption = 'P (J): '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label288: TLabel
          Left = 12
          Top = 48
          Width = 37
          Height = 13
          Caption = 'P (K): '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object DownloadLabelJ: TLabel
          Left = 50
          Top = 26
          Width = 55
          Height = 13
          Caption = 'Not Done'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object DownloadLabelK: TLabel
          Left = 50
          Top = 48
          Width = 55
          Height = 13
          Caption = 'Not Done'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
      end
      object DownloadSFGenIDComboBox: TComboBox
        Left = 98
        Top = 98
        Width = 73
        Height = 21
        Cursor = crHandPoint
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ItemHeight = 13
        ParentFont = False
        Sorted = True
        TabOrder = 8
      end
    end
    object EncryptGroupBox: TGroupBox
      Left = 12
      Top = 6
      Width = 551
      Height = 315
      Caption = 'Encrypt Firmware Hex Files'
      Color = clMenu
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clRed
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentColor = False
      ParentFont = False
      TabOrder = 40
      Visible = False
      DesignSize = (
        551
        315)
      object GroupBox85: TGroupBox
        Left = 14
        Top = 26
        Width = 195
        Height = 181
        Caption = 'Boot Process'
        TabOrder = 0
        object Label234: TLabel
          Left = 120
          Top = 12
          Width = 43
          Height = 13
          Alignment = taCenter
          Caption = 'Version'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clRed
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object EncryptPCRadioButton: TRadioButton
          Left = 8
          Top = 28
          Width = 97
          Height = 17
          Caption = 'Process ( C )'
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          TabStop = True
          OnClick = EncryptPCRadioButtonClick
        end
        object EncryptPDRadioButton: TRadioButton
          Left = 8
          Top = 58
          Width = 97
          Height = 17
          Caption = 'Process ( D )'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          OnClick = EncryptPDRadioButtonClick
        end
        object EncryptPERadioButton: TRadioButton
          Left = 8
          Top = 88
          Width = 97
          Height = 17
          Caption = 'Process ( E )'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 2
          OnClick = EncryptPERadioButtonClick
        end
        object EncryptPJRadioButton: TRadioButton
          Left = 8
          Top = 118
          Width = 95
          Height = 17
          Caption = 'Process ( J )'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 3
          OnClick = EncryptPJRadioButtonClick
        end
        object EncryptPKRadioButton: TRadioButton
          Left = 8
          Top = 148
          Width = 97
          Height = 17
          Caption = 'Process ( K )'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 4
          OnClick = EncryptPKRadioButtonClick
        end
        object EncryptPCVerEdit: TEdit
          Left = 120
          Top = 26
          Width = 59
          Height = 21
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 5
        end
        object EncryptPDVerEdit: TEdit
          Left = 120
          Top = 54
          Width = 59
          Height = 21
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 6
        end
        object EncryptPEVerEdit: TEdit
          Left = 120
          Top = 84
          Width = 59
          Height = 21
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 7
        end
        object EncryptPJVerEdit: TEdit
          Left = 120
          Top = 114
          Width = 59
          Height = 21
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 8
        end
        object EncryptPKVerEdit: TEdit
          Left = 120
          Top = 144
          Width = 59
          Height = 21
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 9
        end
      end
      object GroupBox86: TGroupBox
        Left = 12
        Top = 230
        Width = 527
        Height = 75
        Caption = 'File'
        TabOrder = 1
        object Label252: TLabel
          Left = 8
          Top = 26
          Width = 65
          Height = 13
          Caption = 'File Name: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object EncryptFileNameLabel: TLabel
          Left = 222
          Top = 44
          Width = 91
          Height = 16
          Caption = 'awi501c.Hex '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object EncryptFileNameEdit: TEdit
          Left = 74
          Top = 22
          Width = 445
          Height = 21
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
        end
        object EncryptGetFileBitBtn: TBitBtn
          Left = 418
          Top = 46
          Width = 101
          Height = 25
          Caption = 'Get File'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGreen
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          OnClick = EncryptGetFileBitBtnClick
        end
      end
      object GroupBox88: TGroupBox
        Left = 218
        Top = 24
        Width = 319
        Height = 183
        Caption = 'Encryption Status'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 2
        object Label276: TLabel
          Left = 12
          Top = 28
          Width = 37
          Height = 13
          Caption = 'P (C): '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object EncryptStatusCLabel: TLabel
          Left = 48
          Top = 28
          Width = 55
          Height = 13
          Caption = 'Not Done'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label241: TLabel
          Left = 118
          Top = 28
          Width = 90
          Height = 13
          Caption = 'Encrypted File: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -12
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object EncryptFNameCLabel: TLabel
          Left = 208
          Top = 28
          Width = 82
          Height = 13
          Caption = 'AW501_C.BIN'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGreen
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          Visible = False
        end
        object EncryptStatusDLabel: TLabel
          Left = 48
          Top = 58
          Width = 55
          Height = 13
          Caption = 'Not Done'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label247: TLabel
          Left = 12
          Top = 58
          Width = 38
          Height = 13
          Caption = 'P (D): '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label251: TLabel
          Left = 118
          Top = 58
          Width = 90
          Height = 13
          Caption = 'Encrypted File: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -12
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object EncryptFNameDLabel: TLabel
          Left = 208
          Top = 58
          Width = 83
          Height = 13
          Caption = 'AW501_D.BIN'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGreen
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          Visible = False
        end
        object EncryptStatusELabel: TLabel
          Left = 48
          Top = 88
          Width = 55
          Height = 13
          Caption = 'Not Done'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label263: TLabel
          Left = 12
          Top = 88
          Width = 37
          Height = 13
          Caption = 'P (E): '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label264: TLabel
          Left = 118
          Top = 88
          Width = 90
          Height = 13
          Caption = 'Encrypted File: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -12
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object EncryptFNameELabel: TLabel
          Left = 208
          Top = 88
          Width = 82
          Height = 13
          Caption = 'AW501_E.BIN'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGreen
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          Visible = False
        end
        object EncryptStatusJLabel: TLabel
          Left = 48
          Top = 118
          Width = 55
          Height = 13
          Caption = 'Not Done'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label267: TLabel
          Left = 12
          Top = 118
          Width = 35
          Height = 13
          Caption = 'P (J): '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label268: TLabel
          Left = 120
          Top = 118
          Width = 90
          Height = 13
          Caption = 'Encrypted File: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -12
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object EncryptFNameJLabel: TLabel
          Left = 208
          Top = 118
          Width = 80
          Height = 13
          Caption = 'AW501_J.BIN'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGreen
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          Visible = False
        end
        object EncryptStatusKLabel: TLabel
          Left = 48
          Top = 148
          Width = 55
          Height = 13
          Caption = 'Not Done'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label275: TLabel
          Left = 12
          Top = 148
          Width = 37
          Height = 13
          Caption = 'P (K): '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label277: TLabel
          Left = 118
          Top = 148
          Width = 90
          Height = 13
          Caption = 'Encrypted File: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -12
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object EncryptFNameKLabel: TLabel
          Left = 208
          Top = 148
          Width = 82
          Height = 13
          Caption = 'AW501_K.BIN'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGreen
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          Visible = False
        end
      end
      object EncryptClearBitBtn: TBitBtn
        Left = 470
        Top = 212
        Width = 66
        Height = 24
        Cursor = crHandPoint
        Anchors = [akRight, akBottom]
        Caption = 'Clear'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
        TabOrder = 3
        OnClick = EncryptClearBitBtnClick
      end
      object EncryptBitBtn: TBitBtn
        Left = 240
        Top = 210
        Width = 159
        Height = 27
        Caption = 'ENCRYPT'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 4
        OnClick = EncryptBitBtnClick
      end
    end
    object ConfigTagGroupBox: TGroupBox
      Left = 12
      Top = 6
      Width = 547
      Height = 315
      Caption = 'Configure Tag '
      Color = clMenu
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clRed
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentColor = False
      ParentFont = False
      TabOrder = 26
      Visible = False
      object Label97: TLabel
        Left = 30
        Top = 38
        Width = 67
        Height = 13
        Caption = 'Reader ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label98: TLabel
        Left = 44
        Top = 78
        Width = 52
        Height = 13
        Caption = 'Host ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label105: TLabel
        Left = 128
        Top = 16
        Width = 22
        Height = 13
        Caption = 'GC '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clOlive
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        Visible = False
      end
      object ConfigTagReaderIDComboBox: TComboBox
        Left = 98
        Top = 34
        Width = 79
        Height = 21
        Cursor = crHandPoint
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ItemHeight = 13
        ParentFont = False
        Sorted = True
        TabOrder = 0
      end
      object ConfigTagHostIDEdit: TEdit
        Left = 98
        Top = 73
        Width = 77
        Height = 21
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 1
      end
      object GroupBox28: TGroupBox
        Left = 8
        Top = 104
        Width = 171
        Height = 113
        Caption = 'Tag ID '
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentColor = False
        ParentFont = False
        TabOrder = 2
        object ConfigTagTagIDEdit: TEdit
          Left = 78
          Top = 24
          Width = 79
          Height = 21
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
        end
        object ConfigTagTagIDRadioButton: TRadioButton
          Left = 6
          Top = 26
          Width = 65
          Height = 17
          Cursor = crHandPoint
          Caption = 'Tag ID: '
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          TabStop = True
          OnClick = ConfigTagTagIDRadioButtonClick
        end
        object ConfigTagAnyTagIDRadioButton: TRadioButton
          Left = 6
          Top = 86
          Width = 87
          Height = 17
          Cursor = crHandPoint
          Caption = 'Any Tag ID'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 2
          OnClick = ConfigTagAnyTagIDRadioButtonClick
        end
        object ConfigTagIDRangeRadioButton: TRadioButton
          Left = 6
          Top = 56
          Width = 65
          Height = 17
          Caption = 'Range: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 3
          OnClick = ConfigTagIDRangeRadioButtonClick
        end
        object ConfigTagIDRangeComboBox: TComboBox
          Left = 78
          Top = 54
          Width = 63
          Height = 21
          Color = clMenu
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 13
          ParentFont = False
          TabOrder = 4
          OnExit = ConfigTagIDRangeComboBoxExit
          Items.Strings = (
            '2'
            '3'
            '4'
            '5'
            '6'
            '7'
            '8'
            '10'
            '15'
            '20'
            '40'
            '60'
            '80'
            '100'
            '150'
            '200')
        end
      end
      object GroupBox26: TGroupBox
        Left = 186
        Top = 16
        Width = 197
        Height = 67
        Caption = 'New Tag ID'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 3
        object TagNewIDLabel: TLabel
          Left = 12
          Top = 20
          Width = 77
          Height = 13
          Caption = 'New Tag ID: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object ConfigTagNewIDEdit: TEdit
          Left = 102
          Top = 18
          Width = 83
          Height = 21
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          ReadOnly = True
          TabOrder = 0
        end
        object ConfigTagNewTagIDCheckBox: TCheckBox
          Left = 132
          Top = 46
          Width = 63
          Height = 17
          Cursor = crHandPoint
          Caption = 'Modify'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clOlive
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          OnClick = ConfigTagNewTagIDCheckBoxClick
        end
      end
      object GroupBox31: TGroupBox
        Left = 392
        Top = 142
        Width = 149
        Height = 71
        Hint = 'Time In the Field & Grpoup Count'
        Caption = ' TIF && GC'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 4
        object ConfigTagTIFComboBox: TComboBox
          Left = 10
          Top = 23
          Width = 59
          Height = 21
          Cursor = crHandPoint
          Hint = 'Time In the Field'
          Color = clWhite
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 13
          MaxLength = 2
          ParentFont = False
          TabOrder = 0
          Items.Strings = (
            '1'
            '2'
            '4'
            '6'
            '8'
            '10'
            '12'
            '14'
            '16'
            '18'
            '20'
            '22'
            '24'
            '26'
            '28'
            '30')
        end
        object ConfigTagTIFCheckBox: TCheckBox
          Left = 86
          Top = 53
          Width = 59
          Height = 13
          Caption = 'Modify'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clOlive
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          OnClick = ConfigTagTIFCheckBoxClick
        end
        object ConfigTagGCComboBox: TComboBox
          Left = 80
          Top = 23
          Width = 61
          Height = 21
          Cursor = crHandPoint
          Hint = 'Group Count'
          Color = clWhite
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 13
          MaxLength = 2
          ParentFont = False
          TabOrder = 2
          Items.Strings = (
            '1'
            '2'
            '4'
            '6'
            '8'
            '10'
            '12'
            '14'
            '16'
            '18'
            '20'
            '22'
            '24'
            '26'
            '28'
            '30')
        end
      end
      object GroupBox32: TGroupBox
        Left = 186
        Top = 252
        Width = 197
        Height = 57
        Hint = 'Group Count'
        Caption = 'Tag Response Delay '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 5
        object Label204: TLabel
          Left = 30
          Top = 20
          Width = 85
          Height = 13
          Caption = 'Short Random '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label205: TLabel
          Left = 30
          Top = 38
          Width = 83
          Height = 13
          Caption = 'Long Random '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object ConfigTagRNShortRadioButton: TRadioButton
          Left = 12
          Top = 18
          Width = 15
          Height = 17
          Cursor = crHandPoint
          Caption = 'Short'
          Checked = True
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          TabStop = True
        end
        object ConfigTagRNLongRadioButton: TRadioButton
          Left = 12
          Top = 36
          Width = 17
          Height = 17
          Cursor = crHandPoint
          Caption = 'Long'
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
        end
        object ConfigTagModifyRNCheckBox: TCheckBox
          Left = 134
          Top = 39
          Width = 59
          Height = 13
          Caption = 'Modify'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clOlive
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 2
          OnClick = ConfigTagModifyRNCheckBoxClick
        end
      end
      object GroupBox30: TGroupBox
        Left = 392
        Top = 16
        Width = 149
        Height = 121
        Caption = 'Auto-send Time '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 6
        object Label206: TLabel
          Left = 28
          Top = 52
          Width = 32
          Height = 13
          Caption = 'Hour '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label207: TLabel
          Left = 28
          Top = 70
          Width = 27
          Height = 13
          Caption = 'Sec '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label208: TLabel
          Left = 102
          Top = 52
          Width = 21
          Height = 13
          Caption = 'Min'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object ConfigTagDurationComboBox: TComboBox
          Left = 24
          Top = 20
          Width = 99
          Height = 24
          Color = clWhite
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 16
          MaxLength = 4
          ParentFont = False
          TabOrder = 0
          Items.Strings = (
            'Stop'
            '2'
            '3'
            '4'
            '5'
            '6'
            '7'
            '8'
            '9'
            '10'
            '11'
            '12'
            '13'
            '14'
            '15'
            '16'
            '17'
            '18'
            '19'
            '20'
            '21'
            '22'
            '23'
            '24'
            '25'
            '26'
            '27'
            '28'
            '29'
            '30'
            '31'
            '32'
            '33'
            '34'
            '35'
            '36'
            '37'
            '38'
            '39'
            '40'
            '41'
            '42'
            '43'
            '44'
            '45'
            '46'
            '47'
            '48'
            '49'
            '50'
            '51'
            '52'
            '53'
            '54'
            '55'
            '56'
            '57'
            '58'
            '59')
        end
        object ConfigTagEnableTimeCheckBox: TCheckBox
          Left = 83
          Top = 97
          Width = 61
          Height = 17
          Cursor = crHandPoint
          Caption = 'Modify'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clOlive
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          OnClick = ConfigTagEnableTimeCheckBoxClick
        end
        object ConfigTagMSRadioButton: TRadioButton
          Left = 84
          Top = 71
          Width = 57
          Height = 14
          Cursor = crHandPoint
          Hint = 'milliseconds'
          Caption = 'mSec'
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          ParentShowHint = False
          ShowHint = True
          TabOrder = 2
          Visible = False
          OnClick = ConfigTagMSRadioButtonClick
        end
        object ConfigTagSecRadioButton: TRadioButton
          Left = 10
          Top = 71
          Width = 15
          Height = 14
          Cursor = crHandPoint
          Hint = 'seconds'
          Caption = 'Sec'
          Checked = True
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          ParentShowHint = False
          ShowHint = True
          TabOrder = 3
          TabStop = True
          OnClick = ConfigTagSecRadioButtonClick
        end
        object ConfigTagMinRadioButton: TRadioButton
          Left = 84
          Top = 52
          Width = 15
          Height = 14
          Cursor = crHandPoint
          Caption = 'Min'
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 4
          OnClick = ConfigTagMinRadioButtonClick
        end
        object ConfigTagHourRadioButton: TRadioButton
          Left = 10
          Top = 52
          Width = 17
          Height = 14
          Cursor = crHandPoint
          Caption = 'Hour'
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 5
          OnClick = ConfigTagHourRadioButtonClick
        end
      end
      object GroupBox25: TGroupBox
        Left = 392
        Top = 218
        Width = 149
        Height = 45
        Hint = 'Set Tag Configuration To Factory Setting'
        Caption = 'Default Setting '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 7
        object ConfigTagFactoryIDCheckBox: TCheckBox
          Left = 10
          Top = 20
          Width = 69
          Height = 17
          Cursor = crHandPoint
          Hint = 'Set Tag Configuration To Factory Setting'
          Caption = 'Factory'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          OnClick = ConfigTagFactoryIDCheckBoxClick
        end
      end
      object ConfigTagRNChangeRadioButton: TRadioButton
        Left = 32
        Top = 14
        Width = 15
        Height = 17
        Cursor = crHandPoint
        Caption = 'Change'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 8
        Visible = False
        OnClick = ConfigTagRNChangeRadioButtonClick
      end
      object ConfigTagRNNoChangeRadioButton: TRadioButton
        Left = 4
        Top = 14
        Width = 17
        Height = 17
        Cursor = crHandPoint
        Caption = 'No Change'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 9
        Visible = False
        OnClick = ConfigTagRNNoChangeRadioButtonClick
      end
      object ConfigTagNoChangeRadioButton: TRadioButton
        Left = 55
        Top = 14
        Width = 30
        Height = 17
        Cursor = crHandPoint
        Caption = 'No Change'
        Checked = True
        Font.Charset = ANSI_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'Microsoft Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 10
        TabStop = True
        Visible = False
        OnClick = HexRadioButtonClick
      end
      object ConfigTagGetConfigBitBtn: TBitBtn
        Left = 392
        Top = 274
        Width = 149
        Height = 33
        Caption = 'Get Tag Configuration'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 11
        OnClick = ConfigTagGetConfigBitBtnClick
      end
      object GroupBox27: TGroupBox
        Left = 186
        Top = 86
        Width = 197
        Height = 65
        Caption = 'New Tag Type'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 12
        object ConfigTagNewTagTypeCheckBox: TCheckBox
          Left = 132
          Top = 44
          Width = 61
          Height = 17
          Cursor = crHandPoint
          Caption = 'Modify'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clOlive
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          OnClick = ConfigTagNewTagTypeCheckBoxClick
        end
        object ConfigTagNewTagTypeComboBox: TComboBox
          Left = 18
          Top = 18
          Width = 143
          Height = 21
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 13
          ParentFont = False
          TabOrder = 1
          Text = 'Any Type'
          Items.Strings = (
            'Factory'
            'Any Type')
        end
      end
      object GroupBox33: TGroupBox
        Left = 8
        Top = 228
        Width = 171
        Height = 81
        Caption = 'Tag Type'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 13
        object ConfigTagTypeComboBox: TComboBox
          Left = 16
          Top = 30
          Width = 139
          Height = 21
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 13
          ParentFont = False
          TabOrder = 0
          Text = 'Any Type'
          Items.Strings = (
            'Factory'
            'Any Type')
        end
      end
      object TamperGroupBox: TGroupBox
        Left = 186
        Top = 156
        Width = 197
        Height = 93
        Caption = 'Tamper Switch'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 14
        object Label153: TLabel
          Left = 32
          Top = 22
          Width = 47
          Height = 13
          Caption = 'Disable '
        end
        object Label198: TLabel
          Left = 32
          Top = 40
          Width = 104
          Height = 13
          Caption = 'Report Real Time '
        end
        object Label200: TLabel
          Left = 32
          Top = 58
          Width = 86
          Height = 13
          Caption = 'Report History '
        end
        object ConfigTagReportStatusRadioButton: TRadioButton
          Left = 13
          Top = 38
          Width = 16
          Height = 17
          Cursor = crHandPoint
          Caption = 'Report Real Time'
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          OnClick = HexRadioButtonClick
        end
        object ConfigTagReportHistoryRadioButton: TRadioButton
          Left = 13
          Top = 56
          Width = 14
          Height = 17
          Cursor = crHandPoint
          Caption = 'Report History'
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          OnClick = HexRadioButtonClick
        end
        object ConfigTagModifyTamperCheckBox: TCheckBox
          Left = 134
          Top = 72
          Width = 61
          Height = 17
          Caption = 'Modify'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clOlive
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 2
          OnClick = ConfigTagModifyTamperCheckBoxClick
        end
        object ConfigTagNoReportRadioButton: TRadioButton
          Left = 13
          Top = 20
          Width = 16
          Height = 17
          Cursor = crHandPoint
          Caption = 'Disable'
          Enabled = False
          Font.Charset = ANSI_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'Microsoft Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 3
          OnClick = HexRadioButtonClick
        end
      end
      object ConfigTagGCCheckBox: TCheckBox
        Left = 114
        Top = 15
        Width = 19
        Height = 16
        Cursor = crHandPoint
        Caption = 'Modify'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clOlive
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 15
        Visible = False
        OnClick = ConfigTagGCCheckBoxClick
      end
    end
    object EnableTagGroupBox: TGroupBox
      Left = 12
      Top = 6
      Width = 551
      Height = 317
      Caption = 'Enable Tag '
      Color = clMenu
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clRed
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentColor = False
      ParentFont = False
      TabOrder = 14
      Visible = False
      object Label100: TLabel
        Left = 32
        Top = 30
        Width = 67
        Height = 13
        Caption = 'Reader ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label101: TLabel
        Left = 44
        Top = 56
        Width = 52
        Height = 13
        Caption = 'Host ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label102: TLabel
        Left = 20
        Top = 84
        Width = 78
        Height = 13
        Caption = 'Repeater ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        Visible = False
      end
      object Label103: TLabel
        Left = 408
        Top = 16
        Width = 99
        Height = 16
        Caption = 'Enabled Tags'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object EnableTagReptIDEdit: TEdit
        Left = 98
        Top = 81
        Width = 83
        Height = 21
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 0
        Visible = False
      end
      object EnableTagIDComboBox: TComboBox
        Left = 98
        Top = 26
        Width = 83
        Height = 21
        Cursor = crHandPoint
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ItemHeight = 13
        ParentFont = False
        Sorted = True
        TabOrder = 1
      end
      object EnableTagHostIDEdit: TEdit
        Left = 98
        Top = 53
        Width = 83
        Height = 21
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 2
      end
      object GroupBox35: TGroupBox
        Left = 196
        Top = 18
        Width = 139
        Height = 157
        Caption = 'Tag ID'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 3
        object EnableAnyTagIDRadioButton: TRadioButton
          Left = 14
          Top = 130
          Width = 101
          Height = 17
          Cursor = crHandPoint
          Caption = 'Any Tag ID'
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          TabStop = True
          OnClick = EnableAnyTagIDRadioButtonClick
        end
        object EnableTagIDRadioButton: TRadioButton
          Left = 14
          Top = 24
          Width = 83
          Height = 17
          Cursor = crHandPoint
          Caption = 'Tag ID: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          OnClick = EnableTagIDRadioButtonClick
        end
        object EnableTagIDEdit: TEdit
          Left = 14
          Top = 42
          Width = 113
          Height = 21
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          ReadOnly = True
          TabOrder = 2
        end
        object EnableTagIDRangeRadioButton: TRadioButton
          Left = 14
          Top = 74
          Width = 65
          Height = 17
          Cursor = crHandPoint
          Caption = 'Range'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 3
          OnClick = EnableTagIDRangeRadioButtonClick
        end
        object EnableTagIDRangeComboBox: TComboBox
          Left = 14
          Top = 90
          Width = 77
          Height = 24
          Color = clMenu
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 16
          ParentFont = False
          TabOrder = 4
          OnExit = EnableTagIDRangeComboBoxExit
          Items.Strings = (
            '2'
            '3'
            '4'
            '5'
            '6'
            '7'
            '8'
            '10'
            '15'
            '20'
            '40'
            '60'
            '80'
            '100'
            '150'
            '200')
        end
      end
      object EnableTagListView: TListView
        Left = 352
        Top = 34
        Width = 183
        Height = 137
        Cursor = crHandPoint
        Color = clInfoBk
        Columns = <
          item
            Width = 1
          end
          item
            Alignment = taCenter
            Caption = 'Type'
            Width = 45
          end
          item
            Alignment = taCenter
            Caption = 'ID'
            Width = 110
          end>
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clGreen
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        GridLines = True
        HideSelection = False
        ReadOnly = True
        RowSelect = True
        ParentFont = False
        SortType = stBoth
        TabOrder = 5
        ViewStyle = vsReport
        OnColumnClick = EnableTagListViewColumnClick
        OnCompare = EnableTagListViewCompare
      end
      object EnableTagClearBitBtn: TBitBtn
        Left = 362
        Top = 172
        Width = 55
        Height = 21
        Cursor = crHandPoint
        Caption = 'Clear'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
        TabOrder = 6
        OnClick = EnableTagClearBitBtnClick
      end
      object EnableTagBroadcastRdrCheckBox: TCheckBox
        Left = 194
        Top = 294
        Width = 155
        Height = 17
        Cursor = crHandPoint
        Caption = 'Broadcast All Readers'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 7
        OnClick = EnableTagBroadcastRdrCheckBoxClick
      end
      object GroupBox7: TGroupBox
        Left = 16
        Top = 114
        Width = 167
        Height = 63
        Caption = 'Tag Response Delay'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 8
        object EnableTagRNShortRadioButton: TRadioButton
          Left = 10
          Top = 40
          Width = 125
          Height = 17
          Cursor = crHandPoint
          Caption = 'Short Random '
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          TabStop = True
        end
        object EnableTagRNLongRadioButton: TRadioButton
          Left = 10
          Top = 22
          Width = 127
          Height = 17
          Cursor = crHandPoint
          Caption = 'Long Random '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
        end
      end
      object GroupBox11: TGroupBox
        Left = 352
        Top = 200
        Width = 187
        Height = 105
        Caption = 'Enabled Tag Report'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 9
        object EnableTagType01Label: TLabel
          Left = 68
          Top = 20
          Width = 6
          Height = 13
          Caption = '0'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object EnableTagType03Name: TLabel
          Left = 28
          Top = 40
          Width = 39
          Height = 13
          Alignment = taRightJustify
          Caption = 'Type 3: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object EnableTagType03Label: TLabel
          Left = 68
          Top = 40
          Width = 6
          Height = 13
          Caption = '0'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object EnableTagType01Name: TLabel
          Left = 28
          Top = 20
          Width = 39
          Height = 13
          Alignment = taRightJustify
          Caption = 'Type 1: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object EnableTagType02Name: TLabel
          Left = 124
          Top = 20
          Width = 39
          Height = 13
          Alignment = taRightJustify
          Caption = 'Type 2: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object EnableTagType02Label: TLabel
          Left = 164
          Top = 20
          Width = 6
          Height = 13
          Caption = '0'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label114: TLabel
          Left = 133
          Top = 80
          Width = 30
          Height = 13
          Alignment = taRightJustify
          Caption = 'Total: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGreen
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object EnableTagTotalLabel: TLabel
          Left = 164
          Top = 80
          Width = 6
          Height = 13
          Caption = '0'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clMaroon
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object EnableTagFactoryLabel: TLabel
          Left = 68
          Top = 80
          Width = 6
          Height = 13
          Caption = '0'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clMaroon
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label91: TLabel
          Left = 26
          Top = 80
          Width = 41
          Height = 13
          Alignment = taRightJustify
          Caption = 'Factory: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object EnableTagType04Name: TLabel
          Left = 124
          Top = 40
          Width = 39
          Height = 13
          Alignment = taRightJustify
          Caption = 'Type 4: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object EnableTagType04Label: TLabel
          Left = 164
          Top = 40
          Width = 6
          Height = 13
          Caption = '0'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object EnableTagType05Name: TLabel
          Left = 28
          Top = 60
          Width = 39
          Height = 13
          Alignment = taRightJustify
          Caption = 'Type 5: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object EnableTagType05Label: TLabel
          Left = 68
          Top = 60
          Width = 6
          Height = 13
          Caption = '0'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object EnableTagType06Name: TLabel
          Left = 124
          Top = 60
          Width = 39
          Height = 13
          Alignment = taRightJustify
          Caption = 'Type 6: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object EnableTagType06Label: TLabel
          Left = 164
          Top = 60
          Width = 6
          Height = 13
          Caption = '0'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
      end
      object EnableTagKeepListCheckBox: TCheckBox
        Left = 436
        Top = 172
        Width = 91
        Height = 17
        Cursor = crHandPoint
        Caption = 'Keep List Items'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
        TabOrder = 10
        OnClick = NewListItemCheckBoxClick
      end
      object GroupBox36: TGroupBox
        Left = 196
        Top = 178
        Width = 141
        Height = 105
        Caption = 'Tag Type'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 4
        object EnableTagTypeComboBox: TComboBox
          Left = 14
          Top = 30
          Width = 115
          Height = 21
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 13
          ParentFont = False
          TabOrder = 0
          Text = 'All Types'
          Items.Strings = (
            'Factory'
            'Any Type')
        end
      end
      object GroupBox79: TGroupBox
        Left = 16
        Top = 186
        Width = 169
        Height = 55
        Caption = 'Tag LED '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 11
        object EnableTagDisableLEDRadioButton: TRadioButton
          Left = 92
          Top = 26
          Width = 69
          Height = 17
          Cursor = crHandPoint
          Caption = 'Disable '
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          TabStop = True
        end
        object EnableTagEnableLEDRadioButton: TRadioButton
          Left = 10
          Top = 26
          Width = 67
          Height = 17
          Cursor = crHandPoint
          Caption = 'Enable'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
        end
      end
      object GroupBox103: TGroupBox
        Left = 14
        Top = 250
        Width = 169
        Height = 59
        Caption = 'Tag Speaker'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 12
        object EnableTagDisableSpeakerRadioButton: TRadioButton
          Left = 94
          Top = 28
          Width = 69
          Height = 17
          Cursor = crHandPoint
          Caption = 'Disable '
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          TabStop = True
        end
        object EnableTagEnableSpeakerRadioButton: TRadioButton
          Left = 10
          Top = 28
          Width = 65
          Height = 17
          Cursor = crHandPoint
          Caption = 'Enable'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
        end
      end
    end
    object DisableTagGroupBox: TGroupBox
      Left = 12
      Top = 6
      Width = 551
      Height = 317
      Caption = 'Disable Tag '
      Color = clMenu
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clRed
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentColor = False
      ParentFont = False
      TabOrder = 15
      Visible = False
      object Label106: TLabel
        Left = 36
        Top = 32
        Width = 67
        Height = 13
        Caption = 'Reader ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label108: TLabel
        Left = 50
        Top = 60
        Width = 52
        Height = 13
        Caption = 'Host ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label109: TLabel
        Left = 24
        Top = 90
        Width = 78
        Height = 13
        Caption = 'Repeater ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        Visible = False
      end
      object Label110: TLabel
        Left = 388
        Top = 16
        Width = 104
        Height = 16
        Caption = 'Disabled Tags'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object DisableTagReptIDEdit: TEdit
        Left = 104
        Top = 87
        Width = 83
        Height = 21
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 0
        Visible = False
      end
      object DisableTagIDComboBox: TComboBox
        Left = 104
        Top = 28
        Width = 83
        Height = 21
        Cursor = crHandPoint
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ItemHeight = 13
        ParentFont = False
        Sorted = True
        TabOrder = 1
      end
      object DisableTagHostIDEdit: TEdit
        Left = 104
        Top = 57
        Width = 83
        Height = 21
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 2
      end
      object GroupBox13: TGroupBox
        Left = 200
        Top = 18
        Width = 145
        Height = 151
        Caption = 'Tag ID'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 3
        object DisableAnyTagIDRadioButton: TRadioButton
          Left = 10
          Top = 124
          Width = 101
          Height = 17
          Cursor = crHandPoint
          Caption = 'Any Tag ID'
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          TabStop = True
          OnClick = DisableAnyTagIDRadioButtonClick
        end
        object DisableTagIDRadioButton: TRadioButton
          Left = 12
          Top = 26
          Width = 83
          Height = 17
          Cursor = crHandPoint
          Caption = 'Tag ID: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          OnClick = DisableTagIDRadioButtonClick
        end
        object DisableTagIDEdit: TEdit
          Left = 12
          Top = 44
          Width = 121
          Height = 21
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          ReadOnly = True
          TabOrder = 2
        end
        object DisableTagIDRangeRadioButton: TRadioButton
          Left = 10
          Top = 74
          Width = 83
          Height = 17
          Cursor = crHandPoint
          Caption = 'Range: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 3
          OnClick = DisableTagIDRangeRadioButtonClick
        end
        object DisableTagIDRangeComboBox: TComboBox
          Left = 10
          Top = 90
          Width = 85
          Height = 24
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 16
          ParentFont = False
          TabOrder = 4
          Items.Strings = (
            '2'
            '3'
            '4'
            '5'
            '6'
            '7'
            '8'
            '10'
            '15'
            '20'
            '40'
            '60'
            '80'
            '100'
            '150'
            '200')
        end
      end
      object GroupBox14: TGroupBox
        Left = 200
        Top = 174
        Width = 143
        Height = 109
        Caption = 'Tag Type'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 4
        object DisableTagTypeComboBox: TComboBox
          Left = 12
          Top = 30
          Width = 123
          Height = 21
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 13
          ParentFont = False
          TabOrder = 0
          Text = 'All Types'
          Items.Strings = (
            'Factory'
            'Any Type')
        end
      end
      object DisableTagListView: TListView
        Left = 356
        Top = 32
        Width = 183
        Height = 137
        Cursor = crHandPoint
        Color = clInfoBk
        Columns = <
          item
            Width = 1
          end
          item
            Alignment = taCenter
            Caption = 'Type'
            Width = 45
          end
          item
            Alignment = taCenter
            Caption = 'ID'
            Width = 100
          end>
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clGreen
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        GridLines = True
        HideSelection = False
        ReadOnly = True
        RowSelect = True
        ParentFont = False
        SortType = stBoth
        TabOrder = 5
        ViewStyle = vsReport
        OnColumnClick = DisableTagListViewColumnClick
        OnCompare = DisableTagListViewCompare
      end
      object DisableTagClearBitBtn: TBitBtn
        Left = 368
        Top = 172
        Width = 55
        Height = 21
        Cursor = crHandPoint
        Caption = 'Clear'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
        TabOrder = 6
        OnClick = DisableTagClearBitBtnClick
      end
      object DisableTagBroadcastRdrCheckBox: TCheckBox
        Left = 198
        Top = 292
        Width = 155
        Height = 17
        Cursor = crHandPoint
        Caption = 'Broadcast All Readers'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 7
        OnClick = DisableTagBroadcastRdrCheckBoxClick
      end
      object GroupBox15: TGroupBox
        Left = 10
        Top = 116
        Width = 181
        Height = 63
        Caption = 'Tag Response Delay  '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 8
        object DisableTagRNShortRadioButton: TRadioButton
          Left = 10
          Top = 40
          Width = 123
          Height = 17
          Cursor = crHandPoint
          Caption = 'Short Random '
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          TabStop = True
        end
        object DisableTagRNLongRadioButton: TRadioButton
          Left = 10
          Top = 22
          Width = 125
          Height = 17
          Cursor = crHandPoint
          Caption = 'Long Random '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
        end
      end
      object GroupBox34: TGroupBox
        Left = 354
        Top = 204
        Width = 187
        Height = 105
        Caption = 'Disabled Tag Report'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 9
        object DisableTagType01Label: TLabel
          Left = 70
          Top = 20
          Width = 6
          Height = 13
          Caption = '0'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object DisableTagType03Name: TLabel
          Left = 29
          Top = 40
          Width = 36
          Height = 13
          Alignment = taRightJustify
          Caption = 'Type 3:'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object DisableTagType03Label: TLabel
          Left = 70
          Top = 40
          Width = 6
          Height = 13
          Caption = '0'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object DisableTagType01Name: TLabel
          Left = 29
          Top = 20
          Width = 36
          Height = 13
          Alignment = taRightJustify
          Caption = 'Type 1:'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object DisableTagType02Name: TLabel
          Left = 123
          Top = 20
          Width = 36
          Height = 13
          Alignment = taRightJustify
          Caption = 'Type 2:'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object DisableTagType02Label: TLabel
          Left = 164
          Top = 20
          Width = 6
          Height = 13
          Caption = '0'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label120: TLabel
          Left = 132
          Top = 80
          Width = 27
          Height = 13
          Alignment = taRightJustify
          Caption = 'Total:'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGreen
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object DisableTagTotalLabel: TLabel
          Left = 164
          Top = 80
          Width = 6
          Height = 13
          Caption = '0'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clMaroon
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object DisableTagTypeFactoryName: TLabel
          Left = 28
          Top = 80
          Width = 41
          Height = 13
          Alignment = taRightJustify
          Caption = 'Factory: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object DisableTagFactoryLabel: TLabel
          Left = 70
          Top = 80
          Width = 6
          Height = 13
          Caption = '0'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clMaroon
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object DisableTagType04Name: TLabel
          Left = 123
          Top = 40
          Width = 36
          Height = 13
          Alignment = taRightJustify
          Caption = 'Type 4:'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object DisableTagType04Label: TLabel
          Left = 164
          Top = 40
          Width = 6
          Height = 13
          Caption = '0'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object DisableTagType06Name: TLabel
          Left = 123
          Top = 60
          Width = 36
          Height = 13
          Alignment = taRightJustify
          Caption = 'Type 6:'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object DisableTagType06Label: TLabel
          Left = 164
          Top = 60
          Width = 6
          Height = 13
          Caption = '0'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object DisableTagType05Name: TLabel
          Left = 31
          Top = 60
          Width = 36
          Height = 13
          Alignment = taRightJustify
          Caption = 'Type 5:'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object DisableTagType05Label: TLabel
          Left = 70
          Top = 60
          Width = 6
          Height = 13
          Caption = '0'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
      end
      object DisableTagKeepListCheckBox: TCheckBox
        Left = 442
        Top = 174
        Width = 91
        Height = 17
        Cursor = crHandPoint
        Caption = 'Keep List Items'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
        TabOrder = 10
        OnClick = NewListItemCheckBoxClick
      end
      object GroupBox78: TGroupBox
        Left = 10
        Top = 184
        Width = 181
        Height = 61
        Caption = 'Tag LED '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 11
        object DisableTagDisableLEDRadioButton: TRadioButton
          Left = 96
          Top = 28
          Width = 73
          Height = 17
          Cursor = crHandPoint
          Caption = 'Disable '
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          TabStop = True
        end
        object DisableTagEnableLEDRadioButton: TRadioButton
          Left = 10
          Top = 28
          Width = 69
          Height = 17
          Cursor = crHandPoint
          Caption = 'Enable'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
        end
      end
      object GroupBox102: TGroupBox
        Left = 8
        Top = 248
        Width = 181
        Height = 61
        Caption = 'Tag Speaker '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 12
        object DisableTagDisableSpeakerRadioButton: TRadioButton
          Left = 96
          Top = 28
          Width = 69
          Height = 17
          Cursor = crHandPoint
          Caption = 'Disable '
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          TabStop = True
        end
        object DisableTagEnableSpeakerRadioButton: TRadioButton
          Left = 10
          Top = 28
          Width = 67
          Height = 17
          Cursor = crHandPoint
          Caption = 'Enable'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
        end
      end
    end
    object QueryTagGroupBox: TGroupBox
      Left = 12
      Top = 6
      Width = 551
      Height = 317
      Caption = 'Query Tag '
      Color = clMenu
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clRed
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentColor = False
      ParentFont = False
      TabOrder = 16
      Visible = False
      object Label113: TLabel
        Left = 10
        Top = 22
        Width = 67
        Height = 13
        Caption = 'Reader ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label116: TLabel
        Left = 24
        Top = 56
        Width = 52
        Height = 13
        Caption = 'Host ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label119: TLabel
        Left = 532
        Top = 296
        Width = 93
        Height = 16
        Caption = 'Repeater ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        Visible = False
      end
      object QueryTagReptIDEdit: TEdit
        Left = 534
        Top = 271
        Width = 11
        Height = 24
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 0
        Visible = False
      end
      object QueryTagReaderIDComboBox: TComboBox
        Left = 80
        Top = 20
        Width = 77
        Height = 21
        Cursor = crHandPoint
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ItemHeight = 13
        ParentFont = False
        Sorted = True
        TabOrder = 1
      end
      object QueryTagHostIDEdit: TEdit
        Left = 82
        Top = 53
        Width = 75
        Height = 21
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 2
      end
      object GroupBox37: TGroupBox
        Left = 172
        Top = 16
        Width = 119
        Height = 149
        Caption = 'Tag ID'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 3
        object QueryAnyTagIDRadioButton: TRadioButton
          Left = 10
          Top = 122
          Width = 89
          Height = 17
          Cursor = crHandPoint
          Caption = 'Any Tag ID'
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          TabStop = True
          OnClick = QueryAnyTagIDRadioButtonClick
        end
        object QueryTagIDRadioButton: TRadioButton
          Left = 10
          Top = 24
          Width = 83
          Height = 17
          Cursor = crHandPoint
          Caption = 'Tag ID: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          OnClick = QueryTagIDRadioButtonClick
        end
        object QueryTagIDEdit: TEdit
          Left = 10
          Top = 42
          Width = 97
          Height = 21
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          ReadOnly = True
          TabOrder = 2
        end
        object QueryTagIDRangeComboBox: TComboBox
          Left = 10
          Top = 92
          Width = 75
          Height = 21
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 13
          ParentFont = False
          TabOrder = 3
          OnExit = QueryTagIDRangeComboBoxExit
          Items.Strings = (
            '2'
            '3'
            '4'
            '5'
            '6'
            '7'
            '8'
            '10'
            '15'
            '20'
            '40'
            '60'
            '80'
            '100'
            '150'
            '200')
        end
        object QueryTagIDRangeRadioButton: TRadioButton
          Left = 10
          Top = 74
          Width = 75
          Height = 17
          Caption = 'Range: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 4
          OnClick = QueryTagIDRangeRadioButtonClick
        end
      end
      object GroupBox38: TGroupBox
        Left = 12
        Top = 86
        Width = 147
        Height = 79
        Caption = 'Tag Type'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 4
        object QueryTagTypeComboBox: TComboBox
          Left = 14
          Top = 26
          Width = 117
          Height = 21
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 13
          ParentFont = False
          TabOrder = 0
          Text = 'Any Type'
          Items.Strings = (
            'Factory'
            'Any Type')
        end
      end
      object QueryTagBroadcastRdrCheckBox: TCheckBox
        Left = 412
        Top = 100
        Width = 133
        Height = 17
        Cursor = crHandPoint
        Caption = 'Broadcast Readers'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 5
        OnClick = QueryTagBroadcastRdrCheckBoxClick
      end
      object GroupBox39: TGroupBox
        Left = 410
        Top = 16
        Width = 131
        Height = 69
        Caption = 'Tag Response Delay'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 6
        object QueryTagRNShortRadioButton: TRadioButton
          Left = 10
          Top = 38
          Width = 107
          Height = 17
          Cursor = crHandPoint
          Caption = 'Short Random '
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          TabStop = True
        end
        object QueryTagRNLongRadioButton: TRadioButton
          Left = 10
          Top = 20
          Width = 105
          Height = 17
          Cursor = crHandPoint
          Caption = 'Long Random '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
        end
      end
      object GroupBox73: TGroupBox
        Left = 302
        Top = 16
        Width = 97
        Height = 71
        Caption = 'Tag LED'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 7
        object QueryTagDisableLEDRadioButton: TRadioButton
          Left = 10
          Top = 42
          Width = 71
          Height = 17
          Cursor = crHandPoint
          Caption = 'Disable '
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          TabStop = True
        end
        object QueryTagEnableLEDRadioButton: TRadioButton
          Left = 10
          Top = 22
          Width = 65
          Height = 17
          Cursor = crHandPoint
          Caption = 'Enable'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
        end
      end
      object QueryTagListView: TListView
        Left = 12
        Top = 186
        Width = 529
        Height = 123
        Cursor = crHandPoint
        Color = clInfoBk
        Columns = <
          item
            Width = 1
          end
          item
            Alignment = taCenter
            Caption = 'Tag ID'
            Width = 70
          end
          item
            Alignment = taCenter
            Caption = 'Type'
            Width = 60
          end
          item
            Alignment = taCenter
            Caption = 'Status'
            Width = 60
          end
          item
            Alignment = taCenter
            Caption = 'Battery'
          end
          item
            Alignment = taCenter
            Caption = 'Resend Time'
            Width = 75
          end
          item
            Alignment = taCenter
            Caption = 'TIF'
            Width = 40
          end
          item
            Alignment = taCenter
            Caption = 'GC'
            Width = 40
          end
          item
            Alignment = taCenter
            Caption = 'Tampered'
            Width = 60
          end
          item
            Alignment = taCenter
            Caption = 'Version'
          end>
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clGreen
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        GridLines = True
        HideSelection = False
        ReadOnly = True
        RowSelect = True
        ParentFont = False
        SortType = stBoth
        TabOrder = 8
        ViewStyle = vsReport
        OnColumnClick = QueryTagListViewColumnClick
        OnCompare = QueryTagListViewCompare
        OnCustomDrawItem = QueryTagListViewCustomDrawItem
      end
      object QueryTagClearListButton: TButton
        Left = 464
        Top = 166
        Width = 75
        Height = 19
        Caption = 'Clear List'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
        TabOrder = 9
        OnClick = QueryTagClearListButtonClick
      end
      object QueryTagKeepItemsCheckBox: TCheckBox
        Left = 362
        Top = 168
        Width = 97
        Height = 17
        Caption = 'Keep List Item'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
        TabOrder = 10
      end
      object GroupBox101: TGroupBox
        Left = 304
        Top = 96
        Width = 97
        Height = 69
        Caption = 'Tag Speaker'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 11
        object QueryTagDisableSpeakerRadioButton: TRadioButton
          Left = 10
          Top = 42
          Width = 67
          Height = 17
          Cursor = crHandPoint
          Caption = 'Disable '
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          TabStop = True
        end
        object QueryTagEnableSpeakerRadioButton: TRadioButton
          Left = 10
          Top = 24
          Width = 67
          Height = 17
          Cursor = crHandPoint
          Caption = 'Enable'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
        end
      end
    end
    object SmartFGenGroupBox: TGroupBox
      Left = 12
      Top = 6
      Width = 551
      Height = 315
      Caption = 'Smart Field Generator Call Tag '
      Color = clMenu
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clRed
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentColor = False
      ParentFont = False
      TabOrder = 30
      Visible = False
      object Label166: TLabel
        Left = 30
        Top = 96
        Width = 67
        Height = 13
        Caption = 'Reader ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label167: TLabel
        Left = 12
        Top = 48
        Width = 84
        Height = 13
        Caption = 'Field Gen  ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label168: TLabel
        Left = 50
        Top = 142
        Width = 52
        Height = 13
        Caption = 'Host ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object SmartFGenBroadcastAllRdrCheckBox: TCheckBox
        Left = 12
        Top = 286
        Width = 177
        Height = 17
        Cursor = crHandPoint
        Caption = 'Broadcast To All Readers'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 0
        OnClick = SmartFGenBroadcastAllRdrCheckBoxClick
      end
      object SmartFGenHostIDEdit: TEdit
        Left = 99
        Top = 139
        Width = 70
        Height = 24
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 1
        Text = '1'
        OnChange = HostIDEditChange
      end
      object GroupBox59: TGroupBox
        Left = 340
        Top = 208
        Width = 201
        Height = 93
        Caption = 'Tag ID'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 2
        object SmartFGenAnyTagIDRadioButton: TRadioButton
          Left = 8
          Top = 58
          Width = 101
          Height = 17
          Cursor = crHandPoint
          Caption = 'Any Tag ID'
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          TabStop = True
          OnClick = SmartFGenAnyTagIDRadioButtonClick
        end
        object SmartFGenTagIDRadioButton: TRadioButton
          Left = 10
          Top = 30
          Width = 67
          Height = 17
          Cursor = crHandPoint
          Caption = 'Tag ID: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          OnClick = SmartFGenTagIDRadioButtonClick
        end
        object SmartFGenTagIDEdit: TEdit
          Left = 90
          Top = 28
          Width = 95
          Height = 21
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          ReadOnly = True
          TabOrder = 2
        end
      end
      object GroupBox61: TGroupBox
        Left = 204
        Top = 210
        Width = 125
        Height = 91
        Caption = 'Tag Type'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 3
        object SmartFGenTagTypeComboBox: TComboBox
          Left = 8
          Top = 30
          Width = 109
          Height = 21
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 13
          ParentFont = False
          TabOrder = 0
          Text = 'Any Type'
          Items.Strings = (
            'Factory'
            'Any Type')
        end
      end
      object SmartFGenRNShortRadioButton: TGroupBox
        Left = 12
        Top = 174
        Width = 175
        Height = 73
        Caption = 'Tag Response Delay'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 4
        object SmartFGenShortRNRadioButton: TRadioButton
          Left = 10
          Top = 26
          Width = 133
          Height = 17
          Cursor = crHandPoint
          Caption = 'Short Random '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
        end
        object SmartFGenLongRNRadioButton: TRadioButton
          Left = 10
          Top = 44
          Width = 117
          Height = 21
          Cursor = crHandPoint
          Caption = 'Long Random '
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          TabStop = True
        end
      end
      object GroupBox63: TGroupBox
        Left = 338
        Top = 30
        Width = 201
        Height = 167
        Caption = 'Digital Potentiometer'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 5
        object Label165: TLabel
          Left = 96
          Top = 27
          Width = 89
          Height = 11
          Caption = 'Increase TX Field'
          Font.Charset = ANSI_CHARSET
          Font.Color = clPurple
          Font.Height = -9
          Font.Name = 'Small Fonts'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label172: TLabel
          Left = 96
          Top = 43
          Width = 94
          Height = 11
          Caption = 'Decrease TX Field'
          Font.Charset = ANSI_CHARSET
          Font.Color = clPurple
          Font.Height = -9
          Font.Name = 'Small Fonts'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label171: TLabel
          Left = 14
          Top = 53
          Width = 59
          Height = 11
          Caption = 'Range ( 0 - 20)'
          Font.Charset = ANSI_CHARSET
          Font.Color = clPurple
          Font.Height = -9
          Font.Name = 'Small Fonts'
          Font.Style = []
          ParentFont = False
        end
        object Label56: TLabel
          Left = 12
          Top = 114
          Width = 132
          Height = 13
          Caption = 'Current Potentiometer: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object SmartFGenDPotValueLabel: TLabel
          Left = 142
          Top = 114
          Width = 17
          Height = 16
          Caption = '00'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object SmartFGenPotentioUpDown: TUpDown
          Left = 75
          Top = 28
          Width = 15
          Height = 24
          Associate = SmartFGenNewDPotEdit
          Min = 0
          Max = 20
          Position = 1
          TabOrder = 0
          Wrap = False
          OnChangingEx = SmartFGenPotentioUpDownChangingEx
          OnClick = SmartFGenPotentioUpDownClick
        end
        object SmartFGenNewDPotEdit: TEdit
          Left = 14
          Top = 28
          Width = 61
          Height = 24
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          MaxLength = 2
          ParentFont = False
          TabOrder = 1
          Text = '1'
          OnChange = SmartFGenNewDPotEditChange
        end
        object SmartFGenGetDPotBitBtn: TBitBtn
          Left = 138
          Top = 138
          Width = 57
          Height = 23
          Caption = 'Get'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clRed
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 2
          OnClick = SmartFGenGetDPotBitBtnClick
        end
        object SmartFGenShortRangeRadioButton: TRadioButton
          Left = 12
          Top = 72
          Width = 99
          Height = 17
          Caption = 'Short Range  '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 3
          Visible = False
        end
        object SmartFGenLongRangeRadioButton: TRadioButton
          Left = 12
          Top = 92
          Width = 103
          Height = 17
          Caption = 'Long Range'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 4
          Visible = False
        end
      end
      object SmartFGenReaderIDComboBox: TComboBox
        Left = 98
        Top = 92
        Width = 71
        Height = 24
        Cursor = crHandPoint
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ItemHeight = 16
        ParentFont = False
        Sorted = True
        TabOrder = 6
      end
      object SmartFGenIDComboBox: TComboBox
        Left = 98
        Top = 46
        Width = 71
        Height = 24
        Cursor = crHandPoint
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ItemHeight = 16
        ParentFont = False
        Sorted = True
        TabOrder = 7
        Text = '3'
      end
      object SmartFGenBroadcastAllCheckBox: TCheckBox
        Left = 12
        Top = 264
        Width = 187
        Height = 17
        Cursor = crHandPoint
        Caption = 'Broadcast To All Smart FGen'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 8
        OnClick = SmartFGenBroadcastAllCheckBoxClick
      end
      object GroupBox109: TGroupBox
        Left = 202
        Top = 120
        Width = 127
        Height = 77
        Caption = 'Tag LED'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 9
        object SmartFGenLEDDisableRadioButton: TRadioButton
          Left = 12
          Top = 48
          Width = 73
          Height = 17
          Cursor = crHandPoint
          Caption = 'Disable '
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          TabStop = True
        end
        object SmartFGenLEDEnableRadioButton: TRadioButton
          Left = 12
          Top = 24
          Width = 67
          Height = 17
          Cursor = crHandPoint
          Caption = 'Enable'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
        end
      end
      object GroupBox110: TGroupBox
        Left = 202
        Top = 32
        Width = 127
        Height = 77
        Caption = 'Tag Speaker'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 10
        object SmartFGenSpkDisableRadioButton: TRadioButton
          Left = 12
          Top = 44
          Width = 67
          Height = 17
          Cursor = crHandPoint
          Caption = 'Disable '
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          TabStop = True
        end
        object SmartFGenSpkEnableRadioButton: TRadioButton
          Left = 12
          Top = 22
          Width = 67
          Height = 17
          Cursor = crHandPoint
          Caption = 'Enable'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
        end
      end
    end
    object ConfigTagLEDGroupBox: TGroupBox
      Left = 12
      Top = 6
      Width = 551
      Height = 317
      Caption = 'Configure Tag LED / Speaker '
      Color = clMenu
      Font.Charset = ANSI_CHARSET
      Font.Color = clRed
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentColor = False
      ParentFont = False
      TabOrder = 38
      Visible = False
      object Label226: TLabel
        Left = 20
        Top = 34
        Width = 67
        Height = 13
        Caption = 'Reader ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label227: TLabel
        Left = 34
        Top = 64
        Width = 52
        Height = 13
        Caption = 'Host ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object ConfigTagLEDReptIDEdit: TEdit
        Left = 6
        Top = 31
        Width = 11
        Height = 21
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 0
        Visible = False
      end
      object ConfigTagLEDReaderIDComboBox: TComboBox
        Left = 90
        Top = 30
        Width = 71
        Height = 21
        Cursor = crHandPoint
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ItemHeight = 13
        ParentFont = False
        Sorted = True
        TabOrder = 1
      end
      object ConfigTagLEDHostIDEdit: TEdit
        Left = 90
        Top = 61
        Width = 69
        Height = 21
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 2
      end
      object GroupBox74: TGroupBox
        Left = 6
        Top = 88
        Width = 153
        Height = 87
        Caption = 'Tag ID'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 3
        object ConfigTagLEDAnyTagIDRadioButton: TRadioButton
          Left = 6
          Top = 66
          Width = 101
          Height = 17
          Cursor = crHandPoint
          Caption = 'Any Tag ID'
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          TabStop = True
          OnClick = ConfigTagLEDAnyTagIDRadioButtonClick
        end
        object ConfigTagLEDTagIDRadioButton: TRadioButton
          Left = 6
          Top = 20
          Width = 67
          Height = 17
          Cursor = crHandPoint
          Caption = 'Tag ID: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          OnClick = ConfigTagLEDTagIDRadioButtonClick
        end
        object ConfigTagLEDTagIDEdit: TEdit
          Left = 76
          Top = 16
          Width = 71
          Height = 21
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          ReadOnly = True
          TabOrder = 2
        end
        object ConfigTagLEDTagIDRangeRadioButton: TRadioButton
          Left = 6
          Top = 44
          Width = 65
          Height = 17
          Caption = 'Range: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 3
          OnClick = ConfigTagLEDTagIDRangeRadioButtonClick
        end
        object ConfigTagLEDTagIDRangeComboBox: TComboBox
          Left = 76
          Top = 42
          Width = 55
          Height = 21
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 13
          ParentFont = False
          TabOrder = 4
          Text = '2'
          OnExit = ConfigTagLEDTagIDRangeComboBoxExit
          Items.Strings = (
            '2'
            '3'
            '4'
            '5'
            '6'
            '7'
            '8'
            '10'
            '15'
            '20'
            '40'
            '60'
            '80'
            '100'
            '150'
            '200')
        end
      end
      object GroupBox75: TGroupBox
        Left = 6
        Top = 180
        Width = 153
        Height = 55
        Caption = 'Tag Type'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 4
        object ConfigTagLEDTagTypeComboBox: TComboBox
          Left = 12
          Top = 20
          Width = 129
          Height = 21
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 13
          ParentFont = False
          TabOrder = 0
          Text = 'All Types'
          Items.Strings = (
            'Factory'
            'Any Type')
        end
      end
      object ConfigTagLEDBroadcastRdrCheckBox: TCheckBox
        Left = 6
        Top = 14
        Width = 17
        Height = 17
        Cursor = crHandPoint
        Caption = 'Broadcast All Readers'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 5
        Visible = False
        OnClick = ConfigTagLEDBroadcastRdrCheckBoxClick
      end
      object GroupBox76: TGroupBox
        Left = 8
        Top = 238
        Width = 151
        Height = 53
        Caption = 'Tag Response Delay'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 6
        object ConfigTagLEDRNShortRadioButton: TRadioButton
          Left = 6
          Top = 34
          Width = 107
          Height = 17
          Cursor = crHandPoint
          Caption = 'Short Random '
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          TabStop = True
        end
        object ConfigTagLEDRNLongRadioButton: TRadioButton
          Left = 6
          Top = 16
          Width = 109
          Height = 17
          Cursor = crHandPoint
          Caption = 'Long Random '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
        end
      end
      object GroupBox77: TGroupBox
        Left = 358
        Top = 22
        Width = 187
        Height = 269
        Caption = 'Tag LED '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 7
        object Label229: TLabel
          Left = 14
          Top = 20
          Width = 151
          Height = 13
          Caption = 'Set Num Cycles To Flash: '
          Font.Charset = ANSI_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label223: TLabel
          Left = 104
          Top = 44
          Width = 37
          Height = 13
          Caption = '( 1 - 255 )'
          Font.Charset = ANSI_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Serif'
          Font.Style = []
          ParentFont = False
        end
        object ConfigTagLEDGetBitBtn: TBitBtn
          Left = 6
          Top = 242
          Width = 45
          Height = 21
          Caption = 'Get'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clRed
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          OnClick = ConfigTagLEDGetBitBtnClick
        end
        object ConfigTagLEDNumCyclesEdit: TEdit
          Left = 54
          Top = 40
          Width = 45
          Height = 21
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          ReadOnly = True
          TabOrder = 1
        end
        object ConfigTagLEDListView: TListView
          Left = 6
          Top = 76
          Width = 177
          Height = 165
          Cursor = crHandPoint
          Color = clInfoBk
          Columns = <
            item
              Width = 1
            end
            item
              Alignment = taCenter
              Caption = 'Rdr'
              Width = 29
            end
            item
              Alignment = taCenter
              Caption = 'Tag'
              Width = 55
            end
            item
              Alignment = taCenter
              Caption = 'Type'
              Width = 36
            end
            item
              Alignment = taCenter
              Caption = 'Flash'
              Width = 38
            end>
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGreen
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          GridLines = True
          HideSelection = False
          ReadOnly = True
          RowSelect = True
          ParentFont = False
          SortType = stBoth
          TabOrder = 2
          ViewStyle = vsReport
          OnColumnClick = ConfigTagLEDListViewColumnClick
          OnCompare = ConfigTagLEDListViewCompare
          OnCustomDrawItem = ConfigTagLEDListViewCustomDrawItem
          OnDblClick = ConfigTagLEDListViewDblClick
        end
        object ConfigTagLEDClearListBitBtn: TBitBtn
          Left = 54
          Top = 242
          Width = 55
          Height = 21
          Cursor = crHandPoint
          Caption = 'Clear List'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          TabOrder = 3
          OnClick = ConfigTagLEDClearListBitBtnClick
        end
        object ConfigTagLEDKeepListCheckBox: TCheckBox
          Left = 116
          Top = 244
          Width = 67
          Height = 17
          Caption = 'Keep List'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          TabOrder = 4
        end
      end
      object GroupBox99: TGroupBox
        Left = 164
        Top = 22
        Width = 189
        Height = 269
        Caption = 'Tag Speaker '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 8
        object Label279: TLabel
          Left = 20
          Top = 22
          Width = 146
          Height = 13
          Caption = 'Set Num Times To Beep: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label280: TLabel
          Left = 106
          Top = 46
          Width = 37
          Height = 13
          Caption = '( 1 - 255 )'
          Font.Charset = ANSI_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Serif'
          Font.Style = []
          ParentFont = False
        end
        object ConfigTagLEDSpeakerEdit: TEdit
          Left = 56
          Top = 42
          Width = 47
          Height = 21
          Color = clWhite
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
        end
        object ConfigTagSpeakerListView: TListView
          Left = 6
          Top = 76
          Width = 177
          Height = 165
          Cursor = crHandPoint
          Color = clInfoBk
          Columns = <
            item
              Width = 1
            end
            item
              Alignment = taCenter
              Caption = 'Rdr'
              Width = 29
            end
            item
              Alignment = taCenter
              Caption = 'Tag'
              Width = 55
            end
            item
              Alignment = taCenter
              Caption = 'Type'
              Width = 36
            end
            item
              Alignment = taCenter
              Caption = 'Beep'
              Width = 37
            end>
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGreen
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          GridLines = True
          HideSelection = False
          ReadOnly = True
          RowSelect = True
          ParentFont = False
          SortType = stBoth
          TabOrder = 1
          ViewStyle = vsReport
          OnColumnClick = ConfigTagSpeakerListViewColumnClick
          OnCompare = ConfigTagSpeakerListViewCompare
          OnCustomDrawItem = ConfigTagSpeakerListViewCustomDrawItem
          OnDblClick = ConfigTagSpeakerListViewDblClick
        end
        object ConfigTagSpeakerGetBitBtn: TBitBtn
          Left = 6
          Top = 242
          Width = 45
          Height = 21
          Caption = 'Get'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clRed
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 2
          OnClick = ConfigTagSpeakerGetBitBtnClick
        end
        object ConfigTagSpeakerClearListBitBtn: TBitBtn
          Left = 54
          Top = 242
          Width = 55
          Height = 21
          Cursor = crHandPoint
          Caption = 'Clear List'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          TabOrder = 3
          OnClick = ConfigTagSpeakerClearListBitBtnClick
        end
        object ConfigTagSpeakerKeepListCheckBox: TCheckBox
          Left = 116
          Top = 244
          Width = 67
          Height = 17
          Caption = 'Keep List'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          TabOrder = 4
        end
      end
      object ConfigLEDRadioButton: TRadioButton
        Left = 392
        Top = 294
        Width = 117
        Height = 17
        Caption = 'Config Tag LED '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 9
        OnClick = ConfigLEDRadioButtonClick
      end
      object ConfigLEDSpeakerRadioButton: TRadioButton
        Left = 190
        Top = 294
        Width = 139
        Height = 17
        Caption = 'Config Tag Speaker '
        Checked = True
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 10
        TabStop = True
        OnClick = ConfigLEDSpeakerRadioButtonClick
      end
      object ConfigTagLEDBroadcastReaderCheckBox: TCheckBox
        Left = 8
        Top = 294
        Width = 151
        Height = 17
        Caption = 'Broadcast All Readers'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -12
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 11
        OnClick = ConfigTagLEDBroadcastReaderCheckBoxClick
      end
    end
    object AssignTagReaderGroupBox: TGroupBox
      Left = 12
      Top = 6
      Width = 551
      Height = 315
      Caption = 'Assign Reader to Tag'
      Color = clMenu
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clRed
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentColor = False
      ParentFont = False
      TabOrder = 7
      Visible = False
      object Label63: TLabel
        Left = 20
        Top = 48
        Width = 67
        Height = 13
        Caption = 'Reader ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label64: TLabel
        Left = 34
        Top = 84
        Width = 52
        Height = 13
        Caption = 'Host ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label67: TLabel
        Left = 18
        Top = 142
        Width = 72
        Height = 13
        Caption = 'Tag Rdr ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object AssignTagRdrRdrIDComboBox: TComboBox
        Left = 90
        Top = 44
        Width = 73
        Height = 21
        Cursor = crHandPoint
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ItemHeight = 13
        ParentFont = False
        Sorted = True
        TabOrder = 0
        OnChange = ReaderIDComboBoxChange
      end
      object AssignTagRdrHostIDEdit: TEdit
        Left = 89
        Top = 81
        Width = 74
        Height = 21
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 1
        OnChange = HostIDEditChange
      end
      object AssignTagRdrTagRdrIDComboBox: TComboBox
        Left = 92
        Top = 140
        Width = 97
        Height = 21
        Cursor = crHandPoint
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ItemHeight = 13
        ParentFont = False
        Sorted = True
        TabOrder = 2
        Text = 'Any Reader'
        Items.Strings = (
          'Any Reader')
      end
      object AssignTagRdrListView: TListView
        Left = 384
        Top = 30
        Width = 153
        Height = 253
        Color = clInfoBk
        Columns = <
          item
            Width = 1
          end
          item
            Caption = 'Type'
            Width = 37
          end
          item
            Alignment = taCenter
            Caption = 'ID'
            Width = 110
          end>
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clGreen
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        GridLines = True
        HideSelection = False
        ReadOnly = True
        RowSelect = True
        ParentFont = False
        SortType = stBoth
        TabOrder = 3
        ViewStyle = vsReport
        OnColumnClick = AssignTagRdrListViewColumnClick
        OnCompare = AssignTagRdrListViewCompare
      end
      object AssignTagRdrClearBitBtn: TBitBtn
        Left = 424
        Top = 286
        Width = 75
        Height = 23
        Caption = 'Clear'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 4
        OnClick = AssignTagRdrClearBitBtnClick
      end
      object AssignTagRdrBroadcastRdrCheckBox: TCheckBox
        Left = 16
        Top = 284
        Width = 189
        Height = 17
        Cursor = crHandPoint
        Caption = 'Broadcast All Readers'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 5
        OnClick = AssignTagRdrBroadcastRdrCheckBoxClick
      end
      object GroupBox95: TGroupBox
        Left = 210
        Top = 188
        Width = 157
        Height = 79
        Caption = 'Tag Type'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 6
        object AssignTagRdrTagTypeComboBox: TComboBox
          Left = 20
          Top = 26
          Width = 123
          Height = 21
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 13
          ParentFont = False
          TabOrder = 0
          Text = 'All Types'
          Items.Strings = (
            'Factory'
            'Any Type')
        end
      end
      object GroupBox96: TGroupBox
        Left = 210
        Top = 26
        Width = 157
        Height = 153
        Caption = 'Tag ID'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 7
        object AssignTagRdrTagIDEdit: TEdit
          Left = 19
          Top = 37
          Width = 122
          Height = 21
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          MaxLength = 12
          ParentFont = False
          TabOrder = 0
        end
        object AssignTagRdrTagIDRadioButton: TRadioButton
          Left = 18
          Top = 20
          Width = 93
          Height = 17
          Caption = 'Tag ID: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          OnClick = AssignTagRdrTagIDRadioButtonClick
        end
        object AssignTagRdrTagIDRangeRadioButton: TRadioButton
          Left = 16
          Top = 70
          Width = 69
          Height = 17
          Caption = 'Range: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 2
          OnClick = AssignTagRdrTagIDRangeRadioButtonClick
        end
        object AssignTagRdrTagIDRangeComboBox: TComboBox
          Left = 18
          Top = 88
          Width = 67
          Height = 21
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 13
          ParentFont = False
          TabOrder = 3
          Text = '2'
          OnExit = AssignTagRdrTagIDRangeComboBoxExit
          Items.Strings = (
            '2'
            '3'
            '4'
            '5'
            '6'
            '7'
            '8'
            '10'
            '15'
            '20'
            '40'
            '60'
            '80'
            '100'
            '150'
            '200')
        end
        object AssignTagRdrAnyTagIDRadioButton: TRadioButton
          Left = 18
          Top = 126
          Width = 75
          Height = 17
          Caption = 'Any ID'
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 4
          TabStop = True
          OnClick = AssignTagRdrAnyTagIDRadioButtonClick
        end
      end
      object GroupBox104: TGroupBox
        Left = 18
        Top = 188
        Width = 169
        Height = 77
        Caption = 'Tag Response Delay'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 8
        object AssignTagRdrLongRNDRadioButton: TRadioButton
          Left = 16
          Top = 24
          Width = 127
          Height = 17
          Caption = 'Long  Random'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
        end
        object AssignTagRdrShortRNDRadioButton: TRadioButton
          Left = 16
          Top = 44
          Width = 113
          Height = 17
          Caption = 'Short Random '
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          TabStop = True
        end
      end
    end
    object ConfigTagRandGroupBox: TGroupBox
      Left = 12
      Top = 6
      Width = 551
      Height = 315
      Caption = 'Configure Tag Random Number Generator'
      Color = clMenu
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clRed
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentColor = False
      ParentFont = False
      TabOrder = 8
      Visible = False
      object Label68: TLabel
        Left = 20
        Top = 38
        Width = 67
        Height = 13
        Caption = 'Reader ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label69: TLabel
        Left = 36
        Top = 72
        Width = 52
        Height = 13
        Caption = 'Host ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object ConfigTagRNDRdrIDComboBox: TComboBox
        Left = 88
        Top = 34
        Width = 73
        Height = 21
        Cursor = crHandPoint
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ItemHeight = 13
        ParentFont = False
        Sorted = True
        TabOrder = 0
      end
      object ConfigTagRNDHostIDEdit: TEdit
        Left = 87
        Top = 69
        Width = 74
        Height = 21
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 1
        OnChange = HostIDEditChange
      end
      object ConfigTagRNDListView: TListView
        Left = 384
        Top = 26
        Width = 153
        Height = 259
        Color = clInfoBk
        Columns = <
          item
            Width = 1
          end
          item
            Caption = 'Type'
            Width = 37
          end
          item
            Alignment = taCenter
            Caption = 'ID'
            Width = 110
          end>
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clGreen
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        GridLines = True
        HideSelection = False
        ReadOnly = True
        RowSelect = True
        ParentFont = False
        SortType = stBoth
        TabOrder = 2
        ViewStyle = vsReport
        OnColumnClick = ConfigTagRNDListViewColumnClick
        OnCompare = ConfigTagRNDListViewCompare
      end
      object ConfigTagRNDClearBitBtn: TBitBtn
        Left = 424
        Top = 288
        Width = 75
        Height = 21
        Cursor = crHandPoint
        Caption = 'Clear'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 3
        OnClick = ConfigTagRNDClearBitBtnClick
      end
      object ConfigTagRNDBroadcastCheckBox: TCheckBox
        Left = 232
        Top = 290
        Width = 157
        Height = 17
        Cursor = crHandPoint
        Caption = 'Broadcast All Readers '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 4
        OnClick = ConfigTagRNDBroadcastCheckBoxClick
      end
      object GroupBox105: TGroupBox
        Left = 232
        Top = 186
        Width = 139
        Height = 95
        Caption = 'Tag Type '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 5
        object ConfigTagRNDTagTypeComboBox: TComboBox
          Left = 14
          Top = 28
          Width = 111
          Height = 21
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 13
          ParentFont = False
          TabOrder = 0
          Text = 'Any Type'
          Items.Strings = (
            'Factory'
            'Any Type')
        end
      end
      object GroupBox106: TGroupBox
        Left = 14
        Top = 98
        Width = 205
        Height = 141
        Caption = 'Tag Random Number '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 6
        object Label74: TLabel
          Left = 19
          Top = 28
          Width = 154
          Height = 13
          Caption = 'Short Random Num Period:'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label72: TLabel
          Left = 18
          Top = 110
          Width = 35
          Height = 13
          Caption = 'Upper: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label78: TLabel
          Left = 16
          Top = 52
          Width = 35
          Height = 13
          Caption = 'Upper: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label75: TLabel
          Left = 113
          Top = 110
          Width = 32
          Height = 13
          Caption = 'Lower:'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label65: TLabel
          Left = 113
          Top = 52
          Width = 32
          Height = 13
          Caption = 'Lower:'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label73: TLabel
          Left = 21
          Top = 86
          Width = 152
          Height = 13
          Caption = 'Long Random Num Period:'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object ConfigTagRNDShortUpEdit: TEdit
          Left = 51
          Top = 49
          Width = 38
          Height = 24
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          MaxLength = 12
          ParentFont = False
          TabOrder = 0
        end
        object ConfigTagRNDShortLowEdit: TEdit
          Left = 147
          Top = 47
          Width = 40
          Height = 24
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          MaxLength = 12
          ParentFont = False
          TabOrder = 1
        end
        object ConfigTagRNDLongUpEdit: TEdit
          Left = 53
          Top = 105
          Width = 38
          Height = 24
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          MaxLength = 12
          ParentFont = False
          TabOrder = 2
        end
        object ConfigTagRNDLongLowEdit: TEdit
          Left = 146
          Top = 105
          Width = 43
          Height = 24
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          MaxLength = 12
          ParentFont = False
          TabOrder = 3
        end
      end
      object GroupBox107: TGroupBox
        Left = 234
        Top = 22
        Width = 139
        Height = 155
        Caption = 'Tag ID '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 7
        object ConfigTagRNDTagIDEdit: TEdit
          Left = 13
          Top = 39
          Width = 110
          Height = 21
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          MaxLength = 12
          ParentFont = False
          TabOrder = 0
        end
        object ConfigTagRNDTagIDRadioButton: TRadioButton
          Left = 14
          Top = 22
          Width = 73
          Height = 17
          Caption = 'Tag ID: '
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          TabStop = True
          OnClick = ConfigTagRNDTagIDRadioButtonClick
        end
        object ConfigTagRNDTagIDRangeRadioButton: TRadioButton
          Left = 12
          Top = 76
          Width = 73
          Height = 17
          Caption = 'Range: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 2
          OnClick = ConfigTagRNDTagIDRangeRadioButtonClick
        end
        object ConfigTagRNDTagIDRangeComboBox: TComboBox
          Left = 14
          Top = 94
          Width = 75
          Height = 21
          Color = clMenu
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 13
          ParentFont = False
          TabOrder = 3
          Text = '2'
          OnExit = ConfigTagRNDTagIDRangeComboBoxExit
          Items.Strings = (
            '2'
            '3'
            '4'
            '5'
            '6'
            '7'
            '8'
            '10'
            '15'
            '20'
            '40'
            '60'
            '80'
            '100'
            '150'
            '200')
        end
        object ConfigTagRNDAnyTagIDRadioButton: TRadioButton
          Left = 12
          Top = 132
          Width = 73
          Height = 17
          Caption = 'Any ID'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 4
          OnClick = ConfigTagRNDAnyTagIDRadioButtonClick
        end
      end
      object GroupBox108: TGroupBox
        Left = 14
        Top = 248
        Width = 205
        Height = 59
        Caption = 'Tag Response Delay'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 8
        object ConfigTagRNDLongRespRadioButton: TRadioButton
          Left = 12
          Top = 18
          Width = 105
          Height = 17
          Caption = 'Long Random'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
        end
        object ConfigTagRNDShortRespRadioButton: TRadioButton
          Left = 12
          Top = 38
          Width = 113
          Height = 17
          Caption = 'Short Random'
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          TabStop = True
        end
      end
    end
    object ReadMemoryGroupBox: TGroupBox
      Left = 12
      Top = 6
      Width = 551
      Height = 319
      Caption = 'Read Tag Memory'
      Color = clMenu
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clRed
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentColor = False
      ParentFont = False
      TabOrder = 19
      Visible = False
      object Label134: TLabel
        Left = 34
        Top = 36
        Width = 80
        Height = 16
        Caption = 'Reader ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label135: TLabel
        Left = 52
        Top = 76
        Width = 60
        Height = 16
        Caption = 'Host ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label136: TLabel
        Left = 20
        Top = 114
        Width = 93
        Height = 16
        Caption = 'Repeater ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        Visible = False
      end
      object Label137: TLabel
        Left = 12
        Top = 216
        Width = 79
        Height = 13
        Caption = 'Starting Addr:'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label138: TLabel
        Left = 24
        Top = 254
        Width = 65
        Height = 13
        Caption = 'Num Bytes:'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -9
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label151: TLabel
        Left = 40
        Top = 230
        Width = 27
        Height = 13
        Caption = 'Hex '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label152: TLabel
        Left = 32
        Top = 268
        Width = 45
        Height = 11
        Caption = 'Decimal '
        Font.Charset = ANSI_CHARSET
        Font.Color = clTeal
        Font.Height = -9
        Font.Name = 'Small Fonts'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object MaxReadLabel: TLabel
        Left = 158
        Top = 264
        Width = 66
        Height = 13
        Alignment = taCenter
        Caption = '( Max 12 )  '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label157: TLabel
        Left = 90
        Top = 240
        Width = 64
        Height = 13
        Caption = '(E0 - 3F00)'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object ReadTagReadLabel: TLabel
        Left = 244
        Top = 292
        Width = 92
        Height = 13
        Caption = 'Bytes Read : 00'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        Visible = False
      end
      object ReadTimeLabel: TLabel
        Left = 372
        Top = 292
        Width = 39
        Height = 13
        Caption = '00 sec'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        Visible = False
      end
      object ReadMemReptIDEdit: TEdit
        Left = 116
        Top = 109
        Width = 83
        Height = 24
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 0
        Visible = False
      end
      object ReadMemoryReaderIDComboBox: TComboBox
        Left = 116
        Top = 32
        Width = 83
        Height = 24
        Cursor = crHandPoint
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ItemHeight = 16
        ParentFont = False
        Sorted = True
        TabOrder = 1
      end
      object ReadMemoryHostIDEdit: TEdit
        Left = 116
        Top = 71
        Width = 83
        Height = 24
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        TabOrder = 2
      end
      object GroupBox50: TGroupBox
        Left = 218
        Top = 10
        Width = 153
        Height = 195
        Caption = 'Tag ID'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 3
        object ReadMemoryAnyTagIDRadioButton: TRadioButton
          Left = 20
          Top = 162
          Width = 101
          Height = 17
          Cursor = crHandPoint
          Caption = 'Any Tag ID'
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          TabStop = True
          OnClick = ReadMemoryAnyTagIDRadioButtonClick
        end
        object ReadMemoryTagIDRadioButton: TRadioButton
          Left = 20
          Top = 36
          Width = 83
          Height = 17
          Cursor = crHandPoint
          Caption = 'Tag ID: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          OnClick = ReadMemoryTagIDRadioButtonClick
        end
        object ReadMemoryTagIDEdit: TEdit
          Left = 22
          Top = 54
          Width = 93
          Height = 24
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          ReadOnly = True
          TabOrder = 2
        end
        object ReadMemoryTagIDRangeRadioButton: TRadioButton
          Left = 20
          Top = 100
          Width = 77
          Height = 17
          Caption = 'Range: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 3
          OnClick = ReadMemoryTagIDRangeRadioButtonClick
        end
        object ReadMemoryTagIDRangeComboBox: TComboBox
          Left = 20
          Top = 118
          Width = 73
          Height = 24
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 16
          ParentFont = False
          TabOrder = 4
          OnExit = ReadMemoryTagIDRangeComboBoxExit
          Items.Strings = (
            '2'
            '3'
            '4'
            '5'
            '6'
            '7'
            '8'
            '10'
            '15'
            '20'
            '40'
            '60'
            '80'
            '100'
            '150'
            '200')
        end
      end
      object GroupBox51: TGroupBox
        Left = 386
        Top = 10
        Width = 153
        Height = 119
        Caption = 'Tag Type'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 4
        object ReadMemoryTagTypeComboBox: TComboBox
          Left = 14
          Top = 28
          Width = 127
          Height = 24
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 16
          ParentFont = False
          TabOrder = 0
          Text = 'All Types'
          Items.Strings = (
            'Factory'
            'Any Type')
        end
      end
      object ReadMemoryBroadcastRdrCheckBox: TCheckBox
        Left = 16
        Top = 174
        Width = 185
        Height = 17
        Cursor = crHandPoint
        Caption = 'Broadcast All Readers'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 5
        OnClick = ReadMemoryBroadcastRdrCheckBoxClick
      end
      object GroupBox52: TGroupBox
        Left = 386
        Top = 134
        Width = 153
        Height = 73
        Caption = 'Tag Response Delay'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 6
        object ReadMemoryTagRNShortRadioButton: TRadioButton
          Left = 20
          Top = 48
          Width = 105
          Height = 17
          Cursor = crHandPoint
          Caption = 'Short Random '
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          TabStop = True
        end
        object ReadMemoryTagRNLongRadioButton: TRadioButton
          Left = 20
          Top = 24
          Width = 101
          Height = 17
          Cursor = crHandPoint
          Caption = 'Long Random '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
        end
      end
      object ReadMemoryStartAddrEdit: TEdit
        Left = 92
        Top = 215
        Width = 61
        Height = 24
        Color = clInfoBk
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 4
        ParentFont = False
        TabOrder = 7
        Text = '0000'
      end
      object ReadMemoryNumByteEdit: TEdit
        Left = 92
        Top = 257
        Width = 63
        Height = 24
        Color = clInfoBk
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 2
        ParentFont = False
        TabOrder = 8
        OnChange = ReadMemoryNumByteEditChange
      end
      object ReadMemoryStringGrid: TStringGrid
        Left = 170
        Top = 216
        Width = 369
        Height = 47
        Color = clInfoBk
        ColCount = 13
        DefaultColWidth = 27
        DefaultRowHeight = 20
        FixedColor = clMoneyGreen
        RowCount = 2
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clOlive
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        Options = [goFixedVertLine, goFixedHorzLine, goVertLine, goHorzLine, goRangeSelect, goTabs]
        ParentFont = False
        ScrollBars = ssVertical
        TabOrder = 9
      end
      object Panel6: TPanel
        Left = 252
        Top = 264
        Width = 203
        Height = 25
        BevelOuter = bvNone
        TabOrder = 10
        object ReadMemoryHexRadioButton: TRadioButton
          Left = 10
          Top = 4
          Width = 51
          Height = 17
          Caption = 'Hex'
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          TabStop = True
          OnClick = ReadMemoryHexRadioButtonClick
        end
        object ReadMemDecRadioButton: TRadioButton
          Left = 80
          Top = 4
          Width = 47
          Height = 17
          Caption = 'Dec'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          OnClick = ReadMemDecRadioButtonClick
        end
        object ReadMemCharRadioButton: TRadioButton
          Left = 148
          Top = 4
          Width = 49
          Height = 17
          Caption = 'Char'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 2
          OnClick = ReadMemCharRadioButtonClick
        end
      end
      object ReadTagLargeDataCheckBox: TCheckBox
        Left = 16
        Top = 292
        Width = 97
        Height = 17
        Caption = 'Large Data'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 11
        OnClick = ReadTagLargeDataCheckBoxClick
      end
      object DisplayDataCheckBox: TCheckBox
        Left = 122
        Top = 292
        Width = 97
        Height = 17
        Caption = 'Display Data'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 12
        Visible = False
      end
      object ReadMemLargeDataStopBitBtn: TBitBtn
        Left = 448
        Top = 288
        Width = 51
        Height = 19
        Caption = 'Stop'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
        TabOrder = 13
        Visible = False
        OnClick = ReadMemLargeDataStopBitBtnClick
      end
    end
    object WriteMemoryGroupBox: TGroupBox
      Left = 12
      Top = 6
      Width = 551
      Height = 317
      Caption = 'Write Tag Memory'
      Color = clMenu
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clRed
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentColor = False
      ParentFont = False
      TabOrder = 18
      Visible = False
      object Label129: TLabel
        Left = 34
        Top = 36
        Width = 80
        Height = 16
        Caption = 'Reader ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label130: TLabel
        Left = 52
        Top = 76
        Width = 60
        Height = 16
        Caption = 'Host ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label131: TLabel
        Left = 20
        Top = 114
        Width = 93
        Height = 16
        Caption = 'Repeater ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        Visible = False
      end
      object Label132: TLabel
        Left = 12
        Top = 204
        Width = 79
        Height = 13
        Caption = 'Starting Addr:'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label133: TLabel
        Left = 26
        Top = 254
        Width = 65
        Height = 13
        Caption = 'Num Bytes:'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label149: TLabel
        Left = 38
        Top = 218
        Width = 27
        Height = 13
        Caption = 'Hex '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label150: TLabel
        Left = 34
        Top = 268
        Width = 45
        Height = 11
        Caption = 'Decimal '
        Font.Charset = ANSI_CHARSET
        Font.Color = clTeal
        Font.Height = -9
        Font.Name = 'Small Fonts'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object MaxWDataLabel: TLabel
        Left = 92
        Top = 274
        Width = 67
        Height = 13
        Alignment = taCenter
        AutoSize = False
        Caption = '( Max 12 )'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        Layout = tlCenter
      end
      object Label158: TLabel
        Left = 92
        Top = 228
        Width = 64
        Height = 13
        Caption = '(E0 - 3F00)'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object BytesWrittenLabel: TLabel
        Left = 112
        Top = 294
        Width = 107
        Height = 13
        Caption = 'Bytes Written : 00 '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        Visible = False
      end
      object WriteTimeLabel: TLabel
        Left = 250
        Top = 294
        Width = 43
        Height = 13
        Caption = '00 sec '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        Visible = False
      end
      object WriteNumPKtLabel: TLabel
        Left = 322
        Top = 294
        Width = 60
        Height = 13
        Caption = '#Pkts: 00 '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        Visible = False
      end
      object WriteNumRetryLabel: TLabel
        Left = 406
        Top = 294
        Width = 47
        Height = 13
        Caption = '#W: 00 '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        Visible = False
      end
      object ReadNumRetryLabel: TLabel
        Left = 476
        Top = 294
        Width = 44
        Height = 13
        Caption = '#R: 00 '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        Visible = False
      end
      object WriteMemReptIDEdit: TEdit
        Left = 116
        Top = 109
        Width = 83
        Height = 24
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 0
        Visible = False
      end
      object WriteMemoryReaderIDComboBox: TComboBox
        Left = 116
        Top = 32
        Width = 83
        Height = 24
        Cursor = crHandPoint
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ItemHeight = 16
        ParentFont = False
        Sorted = True
        TabOrder = 1
      end
      object WriteMemoryHostIDEdit: TEdit
        Left = 116
        Top = 71
        Width = 83
        Height = 24
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        TabOrder = 2
      end
      object GroupBox47: TGroupBox
        Left = 216
        Top = 12
        Width = 149
        Height = 195
        Caption = 'Tag ID'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 3
        object WriteMemoryAnyTagIDRadioButton: TRadioButton
          Left = 20
          Top = 148
          Width = 101
          Height = 17
          Cursor = crHandPoint
          Caption = 'Any Tag ID'
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          TabStop = True
          OnClick = WriteMemoryAnyTagIDRadioButtonClick
        end
        object WriteMemoryTagIDRadioButton: TRadioButton
          Left = 20
          Top = 26
          Width = 83
          Height = 17
          Cursor = crHandPoint
          Caption = 'Tag ID: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          OnClick = WriteMemoryTagIDRadioButtonClick
        end
        object WriteMemoryTagIDEdit: TEdit
          Left = 22
          Top = 44
          Width = 109
          Height = 24
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          ReadOnly = True
          TabOrder = 2
        end
        object WriteMemoryTagIDRangeRadioButton: TRadioButton
          Left = 20
          Top = 82
          Width = 79
          Height = 17
          Caption = 'Range: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 3
          OnClick = WriteMemoryTagIDRangeRadioButtonClick
        end
        object WriteMemoryTagIDRangeComboBox: TComboBox
          Left = 20
          Top = 100
          Width = 69
          Height = 24
          Color = clMenu
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 16
          ParentFont = False
          TabOrder = 4
          Text = '2'
          OnExit = WriteMemoryTagIDRangeComboBoxExit
          Items.Strings = (
            '2'
            '3'
            '4'
            '5'
            '6'
            '7'
            '8'
            '10'
            '15'
            '20'
            '40'
            '60'
            '80'
            '100'
            '150'
            '200')
        end
      end
      object GroupBox48: TGroupBox
        Left = 380
        Top = 12
        Width = 159
        Height = 123
        Caption = 'Tag Type'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 4
        object WriteMemoryTagTypeComboBox: TComboBox
          Left = 14
          Top = 30
          Width = 133
          Height = 24
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 16
          ParentFont = False
          TabOrder = 0
          Text = 'Any Type'
          Items.Strings = (
            'Factory'
            'Any Type')
        end
      end
      object WriteMemoryBroadcastRdrCheckBox: TCheckBox
        Left = 16
        Top = 160
        Width = 185
        Height = 17
        Cursor = crHandPoint
        Caption = 'Broadcast All Readers'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 5
        OnClick = WriteMemoryBroadcastRdrCheckBoxClick
      end
      object GroupBox49: TGroupBox
        Left = 380
        Top = 140
        Width = 159
        Height = 67
        Caption = 'Tag Response Delay '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 6
        object WriteMemoryTagRNShortRadioButton: TRadioButton
          Left = 14
          Top = 40
          Width = 105
          Height = 17
          Cursor = crHandPoint
          Caption = 'Short Random '
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          TabStop = True
        end
        object WriteMemoryTagRNLongRadioButton: TRadioButton
          Left = 14
          Top = 20
          Width = 113
          Height = 17
          Cursor = crHandPoint
          Caption = 'Long Random'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
        end
      end
      object WriteMemoryStartAddrEdit: TEdit
        Left = 94
        Top = 203
        Width = 61
        Height = 24
        Color = clInfoBk
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 4
        ParentFont = False
        TabOrder = 7
        Text = '0000'
      end
      object WriteMemoryNumByteEdit: TEdit
        Left = 94
        Top = 249
        Width = 63
        Height = 24
        Color = clInfoBk
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 2
        ParentFont = False
        TabOrder = 8
      end
      object WriteMemoryStringGrid: TStringGrid
        Left = 170
        Top = 216
        Width = 369
        Height = 47
        Color = clInfoBk
        ColCount = 13
        DefaultColWidth = 27
        DefaultRowHeight = 20
        FixedColor = clMoneyGreen
        RowCount = 2
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clOlive
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        Options = [goFixedVertLine, goFixedHorzLine, goVertLine, goHorzLine, goRangeSelect, goEditing, goTabs]
        ParentFont = False
        ScrollBars = ssVertical
        TabOrder = 9
      end
      object Panel3: TPanel
        Left = 228
        Top = 270
        Width = 195
        Height = 25
        BevelOuter = bvNone
        TabOrder = 10
        object WriteMemoryHexRadioButton: TRadioButton
          Left = 8
          Top = 0
          Width = 51
          Height = 17
          Caption = 'Hex'
          Checked = True
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          TabStop = True
          OnClick = WriteMemoryHexRadioButtonClick
        end
        object WriteMemDecRadioButton: TRadioButton
          Left = 74
          Top = 0
          Width = 47
          Height = 17
          Caption = 'Dec'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          OnClick = WriteMemDecRadioButtonClick
        end
        object WriteMemCharRadioButton: TRadioButton
          Left = 136
          Top = 0
          Width = 47
          Height = 17
          Caption = 'Char'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clTeal
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 2
          OnClick = WriteMemCharRadioButtonClick
        end
      end
      object WriteMemoryClearBitBtn: TBitBtn
        Left = 482
        Top = 264
        Width = 57
        Height = 25
        Caption = 'Clear'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 11
        OnClick = WriteMemoryClearBitBtnClick
      end
      object LargeDataCheckBox: TCheckBox
        Left = 12
        Top = 292
        Width = 97
        Height = 17
        Caption = 'Large Data'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 12
        OnClick = LargeDataCheckBoxClick
      end
    end
    object QueryFGenGroupBox: TGroupBox
      Left = 12
      Top = 6
      Width = 551
      Height = 315
      Caption = 'Query Standard Field Generator '
      Color = clMenu
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clRed
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentColor = False
      ParentFont = False
      TabOrder = 28
      Visible = False
      object Label60: TLabel
        Left = 34
        Top = 74
        Width = 52
        Height = 13
        Caption = 'Host ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label61: TLabel
        Left = 6
        Top = 40
        Width = 80
        Height = 13
        Caption = 'Field Gen ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label62: TLabel
        Left = 6
        Top = 140
        Width = 93
        Height = 16
        Caption = 'Repeater ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        Visible = False
      end
      object Label173: TLabel
        Left = 184
        Top = 300
        Width = 99
        Height = 11
        Caption = 'MD = Motion Detector  '
        Font.Charset = ANSI_CHARSET
        Font.Color = clBlack
        Font.Height = -9
        Font.Name = 'Small Fonts'
        Font.Style = []
        ParentFont = False
      end
      object Label175: TLabel
        Left = 184
        Top = 172
        Width = 80
        Height = 11
        Caption = 'FS = Field Strength '
        Font.Charset = ANSI_CHARSET
        Font.Color = clBlack
        Font.Height = -9
        Font.Name = 'Small Fonts'
        Font.Style = []
        ParentFont = False
        Visible = False
      end
      object Label104: TLabel
        Left = 184
        Top = 288
        Width = 84
        Height = 11
        Caption = 'NA = Not Assigned  '
        Font.Charset = ANSI_CHARSET
        Font.Color = clBlack
        Font.Height = -9
        Font.Name = 'Small Fonts'
        Font.Style = []
        ParentFont = False
      end
      object QueryFGenIDEdit: TEdit
        Left = 85
        Top = 35
        Width = 36
        Height = 24
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        TabOrder = 0
        OnChange = HostIDEditChange
      end
      object QueryFGenHostIDEdit: TEdit
        Left = 85
        Top = 69
        Width = 36
        Height = 24
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 1
        OnChange = HostIDEditChange
      end
      object QueryFGenRepeaterIDEdit: TEdit
        Left = 7
        Top = 155
        Width = 36
        Height = 24
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 2
        Visible = False
        OnChange = HostIDEditChange
      end
      object QueryFGenClearBitBtn: TBitBtn
        Left = 368
        Top = 288
        Width = 75
        Height = 21
        Cursor = crHandPoint
        Caption = 'Clear'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 4
        OnClick = QueryFGenClearBitBtnClick
      end
      object QueryFGenRdrRadioButton: TRadioButton
        Left = 12
        Top = 232
        Width = 187
        Height = 17
        Cursor = crHandPoint
        Caption = 'Reader Field Generator'
        Enabled = False
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 5
        Visible = False
      end
      object QueryFGenFgRadioButton: TRadioButton
        Left = 12
        Top = 184
        Width = 201
        Height = 17
        Cursor = crHandPoint
        Caption = 'Standard Field Generator'
        Checked = True
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 6
        TabStop = True
        Visible = False
        OnClick = QueryFGenFgRadioButtonClick
      end
      object QueryFGenCheckBox: TCheckBox
        Left = 482
        Top = 290
        Width = 61
        Height = 17
        Cursor = crHandPoint
        Caption = 'Keep'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 7
      end
      object QueryFGenProcListView: TListView
        Left = 216
        Top = 194
        Width = 327
        Height = 93
        Color = clInfoBk
        Columns = <
          item
            Width = 1
          end
          item
            Caption = 'FG ID'
            Width = 37
          end
          item
            Alignment = taCenter
            Caption = '"J" Code Ver'
            Width = 68
          end
          item
            Alignment = taCenter
            Caption = '"J" Code Date'
            Width = 72
          end
          item
            Alignment = taCenter
            Caption = '"K" Code Ver'
            Width = 68
          end
          item
            Caption = '"J" Code Date'
            Width = 72
          end>
        Font.Charset = ANSI_CHARSET
        Font.Color = clGreen
        Font.Height = -9
        Font.Name = 'Small Fonts'
        Font.Style = []
        GridLines = True
        HideSelection = False
        ReadOnly = True
        RowSelect = True
        ParentFont = False
        SortType = stBoth
        TabOrder = 8
        ViewStyle = vsReport
        Visible = False
      end
      object QueryFGenSmartFGenBroadcastCheckBox: TCheckBox
        Left = 12
        Top = 266
        Width = 191
        Height = 17
        Cursor = crHandPoint
        Caption = 'Broadcast To All Smart FGen'
        Enabled = False
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 9
        Visible = False
        OnClick = QueryFGenSmartFGenBroadcastCheckBoxClick
      end
      object GetProcRevDateBitBtn: TBitBtn
        Left = 12
        Top = 280
        Width = 101
        Height = 21
        Cursor = crHandPoint
        Caption = 'Get Processor Rev && Date'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 10
        Visible = False
        OnClick = GetProcRevDateBitBtnClick
      end
      object QueryFGenClearRevListBitBtn: TBitBtn
        Left = 12
        Top = 256
        Width = 75
        Height = 21
        Cursor = crHandPoint
        Caption = 'Clear'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 11
        Visible = False
        OnClick = QueryFGenClearRevListBitBtnClick
      end
      object QueryFGenKeepRevListCheckBox: TCheckBox
        Left = 12
        Top = 234
        Width = 61
        Height = 17
        Cursor = crHandPoint
        Caption = 'Keep'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 12
        Visible = False
      end
      object QueryFGenSmartFGRadioButton: TRadioButton
        Left = 12
        Top = 208
        Width = 183
        Height = 17
        Cursor = crHandPoint
        Caption = 'Smart Field Generator'
        Enabled = False
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 13
        Visible = False
        OnClick = QueryFGenSmartFGRadioButtonClick
      end
      object QueryFGenSmartFGenBroadcastRdrCheckBox: TCheckBox
        Left = 12
        Top = 286
        Width = 169
        Height = 17
        Cursor = crHandPoint
        Caption = 'Broadcast To All Readers'
        Enabled = False
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 14
        Visible = False
        OnClick = QueryFGenSmartFGenBroadcastRdrCheckBoxClick
      end
      object QueryFGenListView: TListView
        Left = 128
        Top = 34
        Width = 415
        Height = 249
        Color = clInfoBk
        Columns = <
          item
            Width = 1
          end
          item
            Alignment = taCenter
            Caption = 'FG ID'
            Width = 40
          end
          item
            Alignment = taCenter
            Caption = 'Rdr ID'
            Width = 45
          end
          item
            Alignment = taCenter
            Caption = 'TX Time'
            Width = 53
          end
          item
            Alignment = taCenter
            Caption = 'Wait Time'
            Width = 60
          end
          item
            Alignment = taCenter
            Caption = 'Hold Time'
            Width = 60
          end
          item
            Alignment = taCenter
            Caption = 'MD Det.'
            Width = 55
          end
          item
            Alignment = taCenter
            Caption = 'MD Status'
            Width = 68
          end
          item
            Alignment = taCenter
            Caption = 'Motion Sen.'
            Width = 75
          end
          item
            Alignment = taCenter
            Caption = 'Tag Type'
            Width = 60
          end
          item
            Caption = 'Tag ID'
            Width = 100
          end>
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clGreen
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        GridLines = True
        HideSelection = False
        ReadOnly = True
        RowSelect = True
        ParentFont = False
        SortType = stBoth
        TabOrder = 3
        ViewStyle = vsReport
      end
    end
    object ConfigFGenGroupBox: TGroupBox
      Left = 12
      Top = 6
      Width = 551
      Height = 315
      Caption = 'Configure Standard Field Generator '
      Color = clMenu
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clRed
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentColor = False
      ParentFont = False
      TabOrder = 29
      Visible = False
      object Label57: TLabel
        Left = 16
        Top = 40
        Width = 84
        Height = 13
        Caption = 'Field Gen  ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label169: TLabel
        Left = 46
        Top = 74
        Width = 52
        Height = 13
        Caption = 'Host ID: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object GroupBox22: TGroupBox
        Left = 362
        Top = 18
        Width = 179
        Height = 63
        Caption = 'Transmit Time'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 0
        object Label58: TLabel
          Left = 7
          Top = 22
          Width = 36
          Height = 13
          Caption = 'Time: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label54: TLabel
          Left = 114
          Top = 22
          Width = 23
          Height = 13
          Caption = 'Sec'
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentColor = False
          ParentFont = False
        end
        object FGenConfigTxTimeComboBox: TComboBox
          Left = 40
          Top = 18
          Width = 73
          Height = 21
          Cursor = crHandPoint
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 13
          ParentFont = False
          TabOrder = 0
        end
        object FGenConfigTxTimeModifyCheckBox: TCheckBox
          Left = 114
          Top = 42
          Width = 61
          Height = 17
          Caption = 'Modify'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clOlive
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          OnClick = FGenConfigTxTimeModifyCheckBoxClick
        end
      end
      object GroupBox23: TGroupBox
        Left = 362
        Top = 80
        Width = 179
        Height = 117
        Caption = 'Wait / Hold Time'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 1
        object Label59: TLabel
          Left = 21
          Top = 22
          Width = 66
          Height = 13
          Caption = 'Wait Time: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label209: TLabel
          Left = 21
          Top = 42
          Width = 27
          Height = 13
          Caption = 'Sec '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label210: TLabel
          Left = 81
          Top = 42
          Width = 25
          Height = 13
          Caption = 'Min '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label211: TLabel
          Left = 137
          Top = 42
          Width = 32
          Height = 13
          Caption = 'Hour '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object FGenConfigHTimeLabel: TLabel
          Left = 21
          Top = 76
          Width = 66
          Height = 13
          Caption = 'Hold Time: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object FGenConfigHoldTimeLabel: TLabel
          Left = 146
          Top = 76
          Width = 21
          Height = 13
          Caption = 'sec'
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentColor = False
          ParentFont = False
        end
        object FGenConfigWaitTimeLabel: TLabel
          Left = 144
          Top = 22
          Width = 21
          Height = 13
          Caption = 'sec'
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentColor = False
          ParentFont = False
        end
        object FGenConfigWaitTimeComboBox: TComboBox
          Left = 88
          Top = 18
          Width = 55
          Height = 21
          Cursor = crHandPoint
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 13
          ParentFont = False
          TabOrder = 0
          OnClick = FGenConfigWaitTimeComboBoxClick
        end
        object FGenConfigWaitTimeSecRadioButton: TRadioButton
          Left = 4
          Top = 40
          Width = 15
          Height = 19
          Cursor = crHandPoint
          Caption = 'Sec. '
          Checked = True
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          TabStop = True
          OnClick = FGenConfigWaitTimeSecRadioButtonClick
          OnMouseDown = FGenConfigWaitTimeSecRadioButtonMouseDown
        end
        object FGenConfigWaitTimeMinRadioButton: TRadioButton
          Left = 64
          Top = 40
          Width = 15
          Height = 19
          Cursor = crHandPoint
          Caption = 'Min.'
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 2
          OnClick = FGenConfigWaitTimeMinRadioButtonClick
          OnMouseDown = FGenConfigWaitTimeMinRadioButtonMouseDown
        end
        object FGenConfigWaitTimeHourRadioButton: TRadioButton
          Left = 122
          Top = 40
          Width = 15
          Height = 19
          Cursor = crHandPoint
          Caption = 'Hour'
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 3
          OnClick = FGenConfigWaitTimeHourRadioButtonClick
          OnMouseDown = FGenConfigWaitTimeHourRadioButtonMouseDown
        end
        object FGenConfigWaitTimeModifyCheckBox: TCheckBox
          Left = 114
          Top = 98
          Width = 61
          Height = 17
          Caption = 'Modify'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clOlive
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 4
          OnClick = FGenConfigWaitTimeModifyCheckBoxClick
        end
        object FGenConfigHoldTimeComboBox: TComboBox
          Left = 88
          Top = 72
          Width = 55
          Height = 21
          Cursor = crHandPoint
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 13
          ParentFont = False
          TabOrder = 5
          OnClick = FGenConfigWaitTimeComboBoxClick
          Items.Strings = (
            '0'
            '1'
            '2'
            '3'
            '4'
            '5'
            '6'
            '7'
            '8'
            '9'
            '10'
            '11'
            '12'
            '13'
            '14'
            '15'
            '16'
            '17'
            '18'
            '19'
            '20'
            '21'
            '22'
            '23'
            '24'
            '25'
            '26'
            '27'
            '28'
            '29'
            '30'
            '31'
            '32'
            '33'
            '34'
            '35'
            '36'
            '37'
            '38'
            '39'
            '40'
            '41'
            '42'
            '43'
            '44'
            '45'
            '46'
            '47'
            '48'
            '49'
            '50'
            '51'
            '52'
            '53'
            '54'
            '55'
            '56'
            '57'
            '58'
            '59'
            '60'
            '')
        end
        object FGenConfigWaitTimeCheckBox: TCheckBox
          Left = 162
          Top = 8
          Width = 15
          Height = 17
          Enabled = False
          TabOrder = 6
          Visible = False
          OnClick = FGenConfigWaitTimeCheckBoxClick
        end
        object FGenConfigHoldTimeCheckBox: TCheckBox
          Left = 160
          Top = 56
          Width = 15
          Height = 17
          Enabled = False
          TabOrder = 7
          Visible = False
          OnClick = FGenConfigHoldTimeCheckBoxClick
        end
      end
      object FGenConfigFieldGenIDEdit: TEdit
        Left = 103
        Top = 36
        Width = 62
        Height = 21
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        TabOrder = 2
      end
      object FGenConfigReaderIDEdit: TEdit
        Left = 5
        Top = 57
        Width = 16
        Height = 21
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 3
        Visible = False
      end
      object FGenConfigDefaultTimeCheckBox: TCheckBox
        Left = 336
        Top = 286
        Width = 17
        Height = 17
        Caption = 'Default Transmit && Wait Time'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 4
        Visible = False
      end
      object GroupBox12: TGroupBox
        Left = 180
        Top = 18
        Width = 175
        Height = 99
        Caption = 'Tag Type / LED / SPK'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 5
        object Label27: TLabel
          Left = 31
          Top = 56
          Width = 25
          Height = 13
          Caption = 'LED'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label28: TLabel
          Left = 31
          Top = 78
          Width = 52
          Height = 13
          Caption = 'Speaker '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object FGenConfigTagTypeModifyCheckBox: TCheckBox
          Left = 108
          Top = 76
          Width = 61
          Height = 17
          Caption = 'Modify'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clOlive
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          OnClick = FGenConfigTagTypeModifyCheckBoxClick
        end
        object FGenConfigTagTypeComboBox: TComboBox
          Left = 14
          Top = 28
          Width = 149
          Height = 21
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 13
          ParentFont = False
          TabOrder = 1
          Text = 'Any Type'
          Items.Strings = (
            'Factory'
            'Any Type')
        end
        object FGenConfigLEDCheckBox: TCheckBox
          Left = 14
          Top = 54
          Width = 15
          Height = 17
          Enabled = False
          TabOrder = 2
        end
        object FGenConfigSPKCheckBox: TCheckBox
          Left = 14
          Top = 76
          Width = 15
          Height = 17
          Enabled = False
          TabOrder = 3
        end
      end
      object GroupBox43: TGroupBox
        Left = 180
        Top = 122
        Width = 175
        Height = 71
        Caption = 'Tag ID'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 6
        object Label217: TLabel
          Left = 27
          Top = 22
          Width = 48
          Height = 13
          Caption = 'Tag ID: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label218: TLabel
          Left = 25
          Top = 46
          Width = 69
          Height = 13
          Caption = 'Any Tag ID '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object FGenConfigTagIDRadioButton: TRadioButton
          Left = 8
          Top = 20
          Width = 17
          Height = 17
          Cursor = crHandPoint
          Caption = 'Tag ID: '
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          OnClick = FGenConfigTagIDRadioButtonClick
        end
        object FGenConfigTagIDEdit: TEdit
          Left = 84
          Top = 18
          Width = 83
          Height = 21
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          ReadOnly = True
          TabOrder = 1
          OnChange = FGenConfigTagIDEditChange
        end
        object FGenConfigTagIDModifyCheckBox: TCheckBox
          Left = 106
          Top = 44
          Width = 61
          Height = 17
          Caption = 'Modify'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clOlive
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 2
          OnClick = FGenConfigTagIDModifyCheckBoxClick
        end
        object FGenConfigAnyTagIDRadioButton: TRadioButton
          Left = 6
          Top = 44
          Width = 15
          Height = 17
          Cursor = crHandPoint
          Caption = 'Any Tag ID'
          Checked = True
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 3
          TabStop = True
          OnClick = FGenConfigAnyTypeRadioButtonClick
          OnMouseDown = FGenConfigAnyTypeRadioButtonMouseDown
        end
      end
      object GroupBox44: TGroupBox
        Left = 12
        Top = 106
        Width = 159
        Height = 69
        Caption = 'Assigned Reader ID'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 7
        object Label127: TLabel
          Left = 10
          Top = 30
          Width = 22
          Height = 13
          Caption = 'ID: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object FGenConfigAssignedReaderIDEdit: TEdit
          Left = 34
          Top = 28
          Width = 47
          Height = 21
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          ReadOnly = True
          TabOrder = 0
          OnChange = FGenConfigAssignedReaderIDEditChange
        end
        object FGenConfigAssignedReaderIDModifyCheckBox: TCheckBox
          Left = 94
          Top = 48
          Width = 61
          Height = 17
          Caption = 'Modify'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clOlive
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          OnClick = FGenConfigAssignedReaderIDModifyCheckBoxClick
        end
      end
      object GroupBox45: TGroupBox
        Left = 12
        Top = 182
        Width = 159
        Height = 67
        Caption = 'New Field Gen. ID'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 8
        object Label128: TLabel
          Left = 8
          Top = 30
          Width = 22
          Height = 13
          Caption = 'ID: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object FGenConfigFGenIDEdit: TEdit
          Left = 32
          Top = 26
          Width = 51
          Height = 21
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          ReadOnly = True
          TabOrder = 0
          OnChange = FGenConfigFGenIDEditChange
        end
        object FGenConfigFGenIDModifyCheckBox: TCheckBox
          Left = 96
          Top = 46
          Width = 61
          Height = 17
          Caption = 'Modify'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clOlive
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          OnClick = FGenConfigFGenIDModifyCheckBoxClick
        end
      end
      object Tag: TGroupBox
        Left = 180
        Top = 196
        Width = 175
        Height = 77
        Caption = 'Tag Response Delay'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 9
        object Label215: TLabel
          Left = 27
          Top = 22
          Width = 85
          Height = 13
          Caption = 'Short Random '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label216: TLabel
          Left = 27
          Top = 44
          Width = 83
          Height = 13
          Caption = 'Long Random '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object FGenConfigRaRnModifyCheckBox: TCheckBox
          Left = 110
          Top = 56
          Width = 61
          Height = 17
          Caption = 'Modify'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clOlive
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          OnClick = FGenConfigRaRnModifyCheckBoxClick
        end
        object FGenConfigRNShortRadioButton: TRadioButton
          Left = 8
          Top = 20
          Width = 17
          Height = 17
          Cursor = crHandPoint
          Caption = 'Short'
          Checked = True
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          TabStop = True
        end
        object FGenConfigRALongRadioButton: TRadioButton
          Left = 8
          Top = 42
          Width = 15
          Height = 17
          Cursor = crHandPoint
          Caption = 'Long'
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 2
        end
      end
      object FGenConfigHostIDEdit: TEdit
        Left = 103
        Top = 69
        Width = 62
        Height = 21
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 11
      end
      object FGenConfigSmartFgenRadioButton: TRadioButton
        Left = 4
        Top = 22
        Width = 25
        Height = 17
        Cursor = crHandPoint
        Caption = 'Smart Field Gen'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 12
        Visible = False
        OnClick = FGenConfigSmartFgenRadioButtonClick
      end
      object FGenConfigStandFgenRadioButton: TRadioButton
        Left = 36
        Top = 20
        Width = 25
        Height = 17
        Cursor = crHandPoint
        Caption = 'Standard Field Gen'
        Checked = True
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 13
        TabStop = True
        Visible = False
        OnClick = FGenConfigStandFgenRadioButtonClick
      end
      object GroupBox29: TGroupBox
        Left = 362
        Top = 202
        Width = 179
        Height = 105
        Caption = 'Motion Detector '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 14
        object Label212: TLabel
          Left = 25
          Top = 38
          Width = 63
          Height = 13
          Caption = 'Enable MD'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label213: TLabel
          Left = 25
          Top = 58
          Width = 71
          Height = 13
          Caption = 'Active High '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label214: TLabel
          Left = 25
          Top = 78
          Width = 68
          Height = 13
          Caption = 'Active Low '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object FGenConfigEnableISLabel: TLabel
          Left = 25
          Top = 18
          Width = 134
          Height = 13
          Caption = 'Enable Internal Sensor '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object FGenConfigMDCheckBox: TCheckBox
          Left = 114
          Top = 84
          Width = 59
          Height = 17
          Caption = 'Modify'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clOlive
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          OnClick = FGenConfigMDCheckBoxClick
        end
        object FGenConfigMDActiveHiRadioButton: TRadioButton
          Left = 8
          Top = 58
          Width = 17
          Height = 17
          Cursor = crHandPoint
          Caption = 'Active High  '
          Checked = True
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
          TabStop = True
        end
        object FGenConfigMDActiveLoRadioButton: TRadioButton
          Left = 8
          Top = 78
          Width = 17
          Height = 17
          Cursor = crHandPoint
          Caption = 'Active Low '
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 2
        end
        object FGenConfigMDEnableCheckBox: TCheckBox
          Left = 8
          Top = 36
          Width = 17
          Height = 17
          Caption = 'Enable '
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 3
          OnClick = FGenConfigTagTypeModifyCheckBoxClick
        end
        object FGenConfigEnableISCheckBox: TCheckBox
          Left = 8
          Top = 16
          Width = 17
          Height = 17
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 4
          OnClick = FGenConfigTagTypeModifyCheckBoxClick
        end
      end
      object FGenConfigMonitorPIRCheckBox: TCheckBox
        Left = 334
        Top = 292
        Width = 19
        Height = 17
        Caption = 'Monitor PIR Signal'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 15
        Visible = False
        OnClick = FGenConfigActivePIRCheckBoxClick
        OnMouseDown = FGenConfigActivePIRCheckBoxMouseDown
      end
      object FGenConfigActivePIRCheckBox: TCheckBox
        Left = 336
        Top = 280
        Width = 21
        Height = 17
        Caption = 'Plus Active PIR Signal'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 16
        Visible = False
        OnClick = FGenConfigActivePIRCheckBoxClick
        OnMouseDown = FGenConfigActivePIRCheckBoxMouseDown
      end
      object GroupBox92: TGroupBox
        Left = 12
        Top = 254
        Width = 159
        Height = 49
        Caption = 'Tag Reader ID'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 17
        object Label42: TLabel
          Left = 29
          Top = 24
          Width = 42
          Height = 13
          Caption = 'Assign '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object FGenConfigTagRdrIDModifyCheckBox: TCheckBox
          Left = 96
          Top = 28
          Width = 61
          Height = 17
          Caption = 'Modify'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clOlive
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          OnClick = FGenConfigTagRdrIDModifyCheckBoxClick
        end
        object FGenConfigTagRdrIDCheckBox: TCheckBox
          Left = 12
          Top = 22
          Width = 17
          Height = 17
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
        end
      end
      object ConfigFGenUpdateBitBtn: TBitBtn
        Left = 180
        Top = 278
        Width = 175
        Height = 29
        Caption = 'Get Configuration'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 10
        OnClick = ConfigFGenUpdateBitBtnClick
      end
    end
    object AssignReaderGroupBox: TGroupBox
      Left = 12
      Top = 6
      Width = 551
      Height = 315
      Caption = 'Configure Reader'
      Color = clMenu
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clRed
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentColor = False
      ParentFont = False
      TabOrder = 12
      Visible = False
      object AssignReaderReptIDEdit: TEdit
        Left = 479
        Top = 69
        Width = 10
        Height = 24
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 0
        Visible = False
      end
      object AssignReaderListView: TListView
        Left = 10
        Top = 226
        Width = 531
        Height = 83
        Cursor = crHandPoint
        Color = clInfoBk
        Columns = <
          item
            Width = 1
          end
          item
            Alignment = taCenter
            Caption = 'Rdr ID'
            Width = 45
          end
          item
            Alignment = taCenter
            Caption = 'Host ID'
          end
          item
            Alignment = taCenter
            Caption = 'TX Time'
            Width = 65
          end
          item
            Alignment = taCenter
            Caption = 'WT Time'
            Width = 60
          end
          item
            Alignment = taCenter
            Caption = 'FS'
            Width = 40
          end
          item
            Alignment = taCenter
            Caption = 'MD Enable'
            Width = 70
          end
          item
            Alignment = taCenter
            Caption = 'MD Active'
            Width = 65
          end
          item
            Caption = 'Rdr Type'
            Width = 155
          end>
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clGreen
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        GridLines = True
        HideSelection = False
        ReadOnly = True
        RowSelect = True
        ParentFont = False
        SortType = stBoth
        TabOrder = 1
        ViewStyle = vsReport
        OnColumnClick = AssignReaderListViewColumnClick
        OnCompare = AssignReaderListViewCompare
        OnDblClick = AssignReaderListViewDblClick
      end
      object AssignReaderClearBitBtn: TBitBtn
        Left = 464
        Top = 204
        Width = 75
        Height = 21
        Cursor = crHandPoint
        Caption = 'Clear List'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 2
        OnClick = AssignReaderClearBitBtnClick
      end
      object AssignReaderNewReptIDEdit: TEdit
        Left = 493
        Top = 69
        Width = 10
        Height = 24
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        MaxLength = 3
        ParentFont = False
        ReadOnly = True
        TabOrder = 3
        Visible = False
      end
      object GroupBox18: TGroupBox
        Left = 274
        Top = 22
        Width = 267
        Height = 107
        Caption = 'Reader Configuration'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 4
        object AssinReaderTypeLabel: TLabel
          Left = 10
          Top = 25
          Width = 78
          Height = 13
          Caption = 'Reader Type:'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label266: TLabel
          Left = 28
          Top = 48
          Width = 127
          Height = 13
          Caption = 'Respond to Broadcast'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label271: TLabel
          Left = 28
          Top = 66
          Width = 114
          Height = 13
          Caption = 'Enable at Power Up'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label273: TLabel
          Left = 28
          Top = 84
          Width = 67
          Height = 13
          Caption = 'Send RSSI '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object AssignReaderBroadcastCheckBox: TCheckBox
          Left = 10
          Top = 46
          Width = 17
          Height = 17
          Cursor = crHandPoint
          Caption = 'Respond to Broadcast'
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
        end
        object AssignReaderEnableCheckBox: TCheckBox
          Left = 10
          Top = 64
          Width = 17
          Height = 17
          Cursor = crHandPoint
          Caption = 'Disable at Power Up'
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 1
        end
        object AssignReaderRSSICheckBox: TCheckBox
          Left = 10
          Top = 82
          Width = 17
          Height = 17
          Cursor = crHandPoint
          Caption = 'Send RSSI '
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 2
        end
        object AssignReaderNoChangeCheckBox: TCheckBox
          Left = 202
          Top = 82
          Width = 61
          Height = 17
          Cursor = crHandPoint
          Caption = 'Modify'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clOlive
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 3
          OnClick = AssignReaderNoChangeCheckBoxClick
        end
        object AssignReaderTypeComboBox: TComboBox
          Left = 94
          Top = 22
          Width = 163
          Height = 21
          Cursor = crHandPoint
          Color = clWhite
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 13
          ParentFont = False
          TabOrder = 4
          Text = 'Standard Reader'
          Items.Strings = (
            'Standard Reader'
            'Access Control'
            'Small RF Reader'
            'PDA Reader'
            'FGen Reader'
            'SaniFaucet Door Unit'
            'SaniFaucet Faucet Unit'
            'SaniFaucet Sanitization Unit'
            'SaniFaucet Contamination Unit'
            'SaniFaucet Bed Unit'
            'Query FGen Reader')
        end
      end
      object AssignReaderBroadcastRdrCheckBox: TCheckBox
        Left = 638
        Top = 16
        Width = 109
        Height = 17
        Cursor = crHandPoint
        Caption = 'Broadcast'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 5
        OnClick = AssignReaderBroadcastRdrCheckBoxClick
      end
      object AssignReaderBroadcastReptCheckBox: TCheckBox
        Left = 514
        Top = 72
        Width = 17
        Height = 17
        Cursor = crHandPoint
        Caption = 'Broadcast All Repeaters'
        Enabled = False
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 6
        Visible = False
      end
      object GroupBox84: TGroupBox
        Left = 10
        Top = 134
        Width = 119
        Height = 87
        Caption = 'Transmit Time '
        Color = clMenu
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentColor = False
        ParentFont = False
        TabOrder = 7
        object Label89: TLabel
          Left = 4
          Top = 22
          Width = 36
          Height = 13
          Caption = 'Time: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object Label92: TLabel
          Left = 60
          Top = 40
          Width = 23
          Height = 13
          Caption = 'Sec'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object AssignReaderModifyTXCheckBox: TCheckBox
          Left = 52
          Top = 66
          Width = 61
          Height = 17
          Caption = 'Modify'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clOlive
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 0
          OnClick = AssignReaderModifyTXCheckBoxClick
        end
        object AssignReaderTXComboBox: TComboBox
          Left = 38
          Top = 18
          Width = 73
          Height = 21
          Enabled = False
          ItemHeight = 13
          TabOrder = 1
          Items.Strings = (
            '1'
            '2'
            '3'
            '4'
            '5'
            '6'
            '7'
            '8'
            '9'
            '10'
            '11'
            '12'
            '13'
            '14'
            '15'
            '16'
            '17'
            '18'
            '19'
            '20'
            '21'
            '22'
            '23'
            '24'
            '25'
            '26'
            '27'
            '28'
            '29'
            '30'
            '31'
            '32'
            '33'
            '34'
            '35'
            '36'
            '37'
            '38'
            '39'
            '40'
            '41'
            '42'
            '43'
            '44'
            '45'
            '46'
            '47'
            '48'
            '49'
            '50'
            '51'
            '52'
            '53'
            '54'
            '55'
            '56'
            '57'
            '58'
            '59'
            '60'
            '61'
            '62'
            '63')
        end
      end
      object AssignReaderWTGroupBox: TGroupBox
        Left = 136
        Top = 134
        Width = 123
        Height = 87
        Caption = 'Wait Time '
        Color = clMenu
        Enabled = False
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentColor = False
        ParentFont = False
        TabOrder = 8
        object AssignReaderTimeLabel: TLabel
          Left = 8
          Top = 24
          Width = 36
          Height = 13
          Caption = 'Time: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object AssignReaderSecLabel: TLabel
          Left = 22
          Top = 40
          Width = 31
          Height = 13
          Caption = 'Sec. '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object AssignReaderMinLabel: TLabel
          Left = 82
          Top = 42
          Width = 29
          Height = 13
          Caption = 'Min. '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object AssignReaderHourLabel: TLabel
          Left = 22
          Top = 62
          Width = 32
          Height = 13
          Caption = 'Hour '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object AssignReaderWTComboBox: TComboBox
          Left = 44
          Top = 20
          Width = 69
          Height = 21
          Color = clWhite
          Enabled = False
          ItemHeight = 13
          TabOrder = 0
          Items.Strings = (
            '0'
            '1'
            '2'
            '3'
            '4'
            '5'
            '6'
            '7'
            '8'
            '9'
            '10'
            '11'
            '12'
            '13'
            '14'
            '15'
            '16'
            '17'
            '18'
            '19'
            '20'
            '21'
            '22'
            '23'
            '24'
            '25'
            '26'
            '27'
            '28'
            '29'
            '30'
            '31'
            '32'
            '33'
            '34'
            '35'
            '36'
            '37'
            '38'
            '39'
            '40'
            '41'
            '42'
            '43'
            '44'
            '45'
            '46'
            '47'
            '48'
            '49'
            '50'
            '51'
            '52'
            '53'
            '54'
            '55'
            '56'
            '57'
            '58'
            '59'
            '60')
        end
        object AssignReaderWTSecRadioButton: TRadioButton
          Left = 6
          Top = 40
          Width = 17
          Height = 17
          Caption = 'AssignReaderWTSecRadioButton'
          Checked = True
          Enabled = False
          TabOrder = 1
          TabStop = True
        end
        object AssignReaderWTMinRadioButton: TRadioButton
          Left = 66
          Top = 42
          Width = 17
          Height = 17
          Caption = 'AssignReaderWTMinRadioButton'
          Enabled = False
          TabOrder = 2
        end
        object AssignReaderWTHourRadioButton: TRadioButton
          Left = 6
          Top = 60
          Width = 17
          Height = 17
          Caption = 'AssignReaderWTHourRadioButton'
          Enabled = False
          TabOrder = 3
        end
        object AssignReaderModifyWTCheckBox: TCheckBox
          Left = 60
          Top = 66
          Width = 61
          Height = 17
          Caption = 'Modify'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clOlive
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 4
          OnClick = AssignReaderModifyWTCheckBoxClick
        end
      end
      object GroupBox93: TGroupBox
        Left = 10
        Top = 24
        Width = 123
        Height = 105
        Caption = 'Reader ID '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 9
        object Label88: TLabel
          Left = 8
          Top = 56
          Width = 34
          Height = 13
          Caption = 'New: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object AssignReaderIDComboBox: TComboBox
          Left = 42
          Top = 22
          Width = 65
          Height = 21
          Cursor = crHandPoint
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ItemHeight = 13
          ParentFont = False
          Sorted = True
          TabOrder = 0
          OnChange = AssignReaderIDComboBoxChange
        end
        object AssignReaderNewIDEdit: TEdit
          Left = 42
          Top = 53
          Width = 63
          Height = 21
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          MaxLength = 3
          ParentFont = False
          ReadOnly = True
          TabOrder = 1
        end
        object AssignReaderNewRdrCheckBox: TCheckBox
          Left = 56
          Top = 82
          Width = 59
          Height = 17
          Caption = 'Modify'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clOlive
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 2
          OnClick = AssignReaderNewRdrCheckBoxClick
        end
      end
      object GroupBox94: TGroupBox
        Left = 140
        Top = 24
        Width = 127
        Height = 105
        Caption = 'Host ID '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 10
        object Label30: TLabel
          Left = 18
          Top = 54
          Width = 34
          Height = 13
          Caption = 'New: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object AssignReaderHostIDEdit: TEdit
          Left = 52
          Top = 23
          Width = 61
          Height = 21
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          MaxLength = 3
          ParentFont = False
          ReadOnly = True
          TabOrder = 0
        end
        object AssignReaderNewHostIDEdit: TEdit
          Left = 52
          Top = 51
          Width = 61
          Height = 21
          Color = clMenu
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          MaxLength = 3
          ParentFont = False
          ReadOnly = True
          TabOrder = 1
        end
        object AssignReaderNewHostCheckBox: TCheckBox
          Left = 62
          Top = 82
          Width = 61
          Height = 17
          Caption = 'Modify'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clOlive
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 2
          OnClick = AssignReaderNewHostCheckBoxClick
        end
      end
      object AssignReaderGetConfigBitBtn: TBitBtn
        Left = 394
        Top = 140
        Width = 147
        Height = 27
        Cursor = crHandPoint
        Caption = 'Get Reader Config'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 11
        OnClick = AssignReaderGetConfigBitBtnClick
      end
      object AssignReaderMDGroupBox: TGroupBox
        Left = 266
        Top = 134
        Width = 121
        Height = 87
        Caption = 'Motion Detector'
        Color = clMenu
        Enabled = False
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentColor = False
        ParentFont = False
        TabOrder = 12
        object AssignReaderEnableLabel: TLabel
          Left = 26
          Top = 18
          Width = 44
          Height = 13
          Caption = 'Enable '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object AssignReaderActiveHiLabel: TLabel
          Left = 26
          Top = 36
          Width = 67
          Height = 13
          Caption = 'Active High'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object AssignReaderActiveLoLabel: TLabel
          Left = 26
          Top = 52
          Width = 68
          Height = 13
          Caption = 'Active Low '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clNavy
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object AssignReaderEnableMDCheckBox: TCheckBox
          Left = 8
          Top = 16
          Width = 17
          Height = 17
          Caption = 'AssignReaderEnableMDCheckBox'
          Enabled = False
          TabOrder = 0
        end
        object AssignReaderMDActiveHiRadioButton: TRadioButton
          Left = 8
          Top = 34
          Width = 17
          Height = 17
          Caption = 'AssignReaderMDActiveHiRadioButton'
          Checked = True
          Enabled = False
          TabOrder = 1
          TabStop = True
        end
        object AssignReaderMDActiveLoRadioButton: TRadioButton
          Left = 8
          Top = 50
          Width = 17
          Height = 17
          Caption = 'RadioButton1'
          Enabled = False
          TabOrder = 2
        end
        object AssignReaderModifyMDCheckBox: TCheckBox
          Left = 58
          Top = 66
          Width = 61
          Height = 17
          Caption = 'Modify'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clOlive
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 3
          OnClick = AssignReaderModifyMDCheckBoxClick
        end
      end
      object AssignReaderBroadcastReaderCheckBox: TCheckBox
        Left = 394
        Top = 178
        Width = 147
        Height = 17
        Caption = 'Broadcast To Readers'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 13
        Visible = False
        OnClick = AssignReaderBroadcastReaderCheckBoxClick
      end
      object AssignReaderSetConfigBitBtn: TBitBtn
        Left = 490
        Top = 72
        Width = 19
        Height = 21
        Cursor = crHandPoint
        Caption = 'Set FGen Config'
        Enabled = False
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 14
        Visible = False
        OnClick = AssignReaderSetConfigBitBtnClick
      end
    end
    object GroupBox9: TGroupBox
      Left = 868
      Top = 3
      Width = 153
      Height = 596
      Caption = 'Commands '
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clRed
      Font.Height = -16
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentFont = False
      TabOrder = 3
      object TagCMDRadioButton: TRadioButton
        Left = 8
        Top = 62
        Width = 113
        Height = 17
        Caption = 'Tag'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 3
        OnClick = TagCMDRadioButtonClick
      end
      object ReaderCMDRadioButton: TRadioButton
        Left = 8
        Top = 34
        Width = 95
        Height = 17
        Caption = 'Reader'
        Checked = True
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 0
        TabStop = True
        OnClick = ReaderCMDRadioButtonClick
      end
      object FGenCMDRadioButton: TRadioButton
        Left = 8
        Top = 120
        Width = 133
        Height = 17
        Caption = 'Field Generator'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 1
        OnClick = FGenCMDRadioButtonClick
      end
      object SFGenCMDRadioButton: TRadioButton
        Left = 8
        Top = 92
        Width = 133
        Height = 17
        Caption = 'Smart FGen'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 2
        OnClick = SFGenCMDRadioButtonClick
      end
    end
    object FGenCMDPanel: TPanel
      Left = 870
      Top = 145
      Width = 147
      Height = 448
      BevelOuter = bvNone
      TabOrder = 34
      object Label170: TLabel
        Left = 4
        Top = 4
        Width = 23
        Height = 13
        Caption = 'fGen'
        Visible = False
      end
      object ConfigFGenBitBtn: TBitBtn
        Left = 4
        Top = 56
        Width = 137
        Height = 35
        Cursor = crHandPoint
        Hint = 'Transmit Configure Field Generator Command'
        Caption = 'Configure'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 0
        Visible = False
        OnClick = ConfigFGenBitBtnClick
      end
      object ConfigFGenStaticText: TStaticText
        Left = 8
        Top = 66
        Width = 131
        Height = 17
        Cursor = crHandPoint
        Hint = 'Activate Configure Field Generator  Parameter Box'
        Alignment = taCenter
        AutoSize = False
        Caption = 'Configure'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 1
        OnClick = ConfigFGenStaticTextClick
      end
      object QueryFGenBitBtn: TBitBtn
        Left = 4
        Top = 19
        Width = 137
        Height = 34
        Cursor = crHandPoint
        Hint = 'Transmit Query Field Generator Command'
        Caption = 'Query'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 2
        Visible = False
        OnClick = QueryFGenBitBtnClick
      end
      object QueryFGenStaticText: TStaticText
        Left = 8
        Top = 28
        Width = 131
        Height = 17
        Cursor = crHandPoint
        Hint = 'Activate Query Field Generator Parameter Box'
        Alignment = taCenter
        AutoSize = False
        Caption = 'Query'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 3
        OnClick = QueryFGenStaticTextClick
      end
      object ResetCMDFGenBitBtn: TBitBtn
        Left = 22
        Top = 420
        Width = 105
        Height = 23
        Caption = 'Reset Commands'
        Font.Charset = ANSI_CHARSET
        Font.Color = clBlue
        Font.Height = -9
        Font.Name = 'Small Fonts'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 4
        Visible = False
        OnClick = ResetCMDFGenBitBtnClick
      end
    end
    object SmartFGenCMDPanel: TPanel
      Left = 870
      Top = 145
      Width = 147
      Height = 448
      BevelOuter = bvNone
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clPurple
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentFont = False
      TabOrder = 31
      Visible = False
      object Label176: TLabel
        Left = 102
        Top = 2
        Width = 39
        Height = 13
        Caption = 'SFGen'
        Visible = False
      end
      object FGenResetBitBtn: TBitBtn
        Left = 6
        Top = 19
        Width = 137
        Height = 34
        Cursor = crHandPoint
        Hint = 'Transmit Query Field Generator Command'
        Caption = 'Reset'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 0
        Visible = False
        OnClick = FGenResetBitBtnClick
      end
      object FGenResetStaticText: TStaticText
        Left = 8
        Top = 28
        Width = 131
        Height = 17
        Cursor = crHandPoint
        Hint = 'Activate Query Field Generator Parameter Box'
        Alignment = taCenter
        AutoSize = False
        Caption = 'Reset'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 1
        OnClick = FGenResetStaticTextClick
      end
      object SmartFGenBitBtn: TBitBtn
        Left = 6
        Top = 127
        Width = 137
        Height = 34
        Cursor = crHandPoint
        Hint = 'Transmit Query Field Generator Command'
        Caption = 'Call Tag'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 2
        Visible = False
        OnClick = SmartFGenBitBtnClick
      end
      object SmartFGenStaticText: TStaticText
        Left = 8
        Top = 136
        Width = 131
        Height = 17
        Cursor = crHandPoint
        Hint = 'Activate Query Field Generator Parameter Box'
        Alignment = taCenter
        AutoSize = False
        Caption = 'Call Tag'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 3
        OnClick = SmartFGenStaticTextClick
      end
      object ResetCMDSFGenBitBtn: TBitBtn
        Left = 22
        Top = 420
        Width = 105
        Height = 23
        Caption = 'Reset Commands'
        Font.Charset = ANSI_CHARSET
        Font.Color = clBlue
        Font.Height = -9
        Font.Name = 'Small Fonts'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 4
        Visible = False
        OnClick = ResetCMDSFGenBitBtnClick
      end
      object ConfigSFGenBitBtn: TBitBtn
        Left = 6
        Top = 90
        Width = 137
        Height = 35
        Cursor = crHandPoint
        Hint = 'Transmit Configure Field Generator Command'
        Caption = 'Configure'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 5
        Visible = False
        OnClick = ConfigSFGenBitBtnClick
      end
      object QuerySFGenBitBtn: TBitBtn
        Left = 6
        Top = 55
        Width = 137
        Height = 34
        Cursor = crHandPoint
        Hint = 'Transmit Query Field Generator Command'
        Caption = 'Query'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 6
        Visible = False
        OnClick = QuerySFGenBitBtnClick
      end
      object ConfigSFGenStaticText: TStaticText
        Left = 8
        Top = 100
        Width = 131
        Height = 17
        Cursor = crHandPoint
        Hint = 'Activate Configure Field Generator  Parameter Box'
        Alignment = taCenter
        AutoSize = False
        Caption = 'Configure'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 7
        OnClick = ConfigSFGenStaticTextClick
      end
      object QuerySFGenStaticText: TStaticText
        Left = 10
        Top = 64
        Width = 131
        Height = 17
        Cursor = crHandPoint
        Hint = 'Activate Query Field Generator Parameter Box'
        Alignment = taCenter
        AutoSize = False
        Caption = 'Query'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 8
        OnClick = QuerySFGenStaticTextClick
      end
      object DownloadSFGenBitBtn: TBitBtn
        Left = 6
        Top = 163
        Width = 137
        Height = 34
        Cursor = crHandPoint
        Hint = 'Transmit Query Field Generator Command'
        Caption = 'Download'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 9
        Visible = False
        OnClick = DownloadSFGenBitBtnClick
      end
      object DownloadSmartFGenStaticText: TStaticText
        Left = 8
        Top = 172
        Width = 131
        Height = 17
        Cursor = crHandPoint
        Hint = 'Activate Query Field Generator Parameter Box'
        Alignment = taCenter
        AutoSize = False
        Caption = 'Download'
        Enabled = False
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 10
        OnClick = DownloadSmartFGenStaticTextClick
      end
    end
    object TagCMDPanel: TPanel
      Left = 870
      Top = 148
      Width = 147
      Height = 447
      BevelOuter = bvNone
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clPurple
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentFont = False
      TabOrder = 33
      Visible = False
      object Label177: TLabel
        Left = 116
        Top = 2
        Width = 23
        Height = 13
        Caption = 'Tag'
        Visible = False
      end
      object EnableTagBitBtn: TBitBtn
        Left = 6
        Top = 20
        Width = 137
        Height = 35
        Cursor = crHandPoint
        Hint = 'Transmit Enable Tag Command'
        Caption = 'Enable'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 1
        Visible = False
        OnClick = EnableTagBitBtnClick
      end
      object DisableTagBitBtn: TBitBtn
        Left = 8
        Top = 57
        Width = 135
        Height = 36
        Cursor = crHandPoint
        Hint = 'Transmit Disable Tag Command'
        Caption = 'Disable'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 2
        Visible = False
        OnClick = DisableTagBitBtnClick
      end
      object QueryTagBitBtn: TBitBtn
        Left = 6
        Top = 134
        Width = 137
        Height = 35
        Cursor = crHandPoint
        Hint = 'Transmit Query Tag Command'
        Caption = 'Query'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 3
        Visible = False
        OnClick = QueryTagBitBtnClick
      end
      object CallTagBitBtn: TBitBtn
        Left = 6
        Top = 172
        Width = 137
        Height = 35
        Cursor = crHandPoint
        Hint = 'Transmit Call Tag Command'
        Caption = 'Call'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 4
        Visible = False
        OnClick = CallTagBitBtnClick
      end
      object AssignTagRdrBitBtn: TBitBtn
        Left = 6
        Top = 209
        Width = 137
        Height = 34
        Cursor = crHandPoint
        Hint = 'Transmit  Assign Tag  Reader Command'
        Caption = 'Assign Reader'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 5
        Visible = False
        OnClick = AssignTagRdrBitBtnClick
      end
      object ConfigTagRNDBitBtn: TBitBtn
        Left = 6
        Top = 245
        Width = 137
        Height = 34
        Cursor = crHandPoint
        Hint = 'Transmit Config Tag  Random Command'
        Caption = 'Configure RND'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 6
        Visible = False
        OnClick = ConfigTagRNDBitBtnClick
      end
      object WriteMemoryBitBtn: TBitBtn
        Left = 6
        Top = 316
        Width = 137
        Height = 35
        Cursor = crHandPoint
        Hint = 'Transmit Config Tag  Random Command'
        Caption = 'Write Memory'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 7
        Visible = False
        OnClick = WriteMemoryBitBtnClick
      end
      object ReadMemoryBitBtn: TBitBtn
        Left = 6
        Top = 281
        Width = 137
        Height = 34
        Cursor = crHandPoint
        Hint = 'Transmit Config Tag  Random Command'
        Caption = 'Read Memory'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 8
        Visible = False
        OnClick = ReadMemoryBitBtnClick
      end
      object TagTempBitBtn: TBitBtn
        Left = 6
        Top = 355
        Width = 137
        Height = 34
        Cursor = crHandPoint
        Hint = 'Transmit Config Tag  Random Command'
        Caption = 'Config Temperature'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 9
        Visible = False
        OnClick = TagTempBitBtnClick
      end
      object EnableTagStaticText: TStaticText
        Left = 10
        Top = 30
        Width = 131
        Height = 17
        Cursor = crHandPoint
        Hint = 'Activate Enable Tag Parameter Box'
        Alignment = taCenter
        AutoSize = False
        Caption = 'Enable'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 10
        OnClick = EnableTagStaticTextClick
      end
      object DisableTagStaticText: TStaticText
        Left = 10
        Top = 68
        Width = 131
        Height = 17
        Cursor = crHandPoint
        Hint = 'Activate Disable Tag Parameter Box'
        Alignment = taCenter
        AutoSize = False
        Caption = 'Disable'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 11
        OnClick = DisableTagStaticTextClick
      end
      object QueryTagStaticText: TStaticText
        Left = 8
        Top = 144
        Width = 131
        Height = 17
        Cursor = crHandPoint
        Hint = 'Activate Query Tag Parameter Box'
        Alignment = taCenter
        AutoSize = False
        Caption = 'Query'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 12
        OnClick = QueryTagStaticTextClick
      end
      object CallTagStaticText: TStaticText
        Left = 8
        Top = 182
        Width = 131
        Height = 17
        Cursor = crHandPoint
        Hint = 'Activate Call Tag Parameter Box'
        Alignment = taCenter
        AutoSize = False
        Caption = 'Call'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 13
        OnClick = CallTagStaticTextClick
      end
      object AssignTagRdrStaticText: TStaticText
        Left = 8
        Top = 218
        Width = 131
        Height = 17
        Cursor = crHandPoint
        Hint = 'Activate Enable Assign Tag Reader Parameter Box'
        Alignment = taCenter
        AutoSize = False
        Caption = 'Assign Reader'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 14
        OnClick = AssignTagRdrStaticTextClick
      end
      object ConfigTagRNDStaticText: TStaticText
        Left = 8
        Top = 254
        Width = 131
        Height = 17
        Hint = 'Activate Configure Tag Random Number Parameter Box'
        Alignment = taCenter
        AutoSize = False
        Caption = 'Configure RND'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 15
        OnClick = ConfigTagRNDStaticTextClick
      end
      object ReadMemoryStaticText: TStaticText
        Left = 10
        Top = 290
        Width = 131
        Height = 17
        Alignment = taCenter
        AutoSize = False
        Caption = 'Read Memory'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 16
        OnClick = ReadMemoryStaticTextClick
      end
      object WriteMemoryStaticText: TStaticText
        Left = 10
        Top = 328
        Width = 131
        Height = 15
        Alignment = taCenter
        AutoSize = False
        Caption = 'Write Memory'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 17
        OnClick = WriteMemoryStaticTextClick
      end
      object TagTempStaticText: TStaticText
        Left = 10
        Top = 364
        Width = 131
        Height = 17
        Alignment = taCenter
        AutoSize = False
        Caption = 'Config Temperature'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 18
        OnClick = TagTempStaticTextClick
      end
      object ConfigTagBitBtn: TBitBtn
        Left = 6
        Top = 96
        Width = 137
        Height = 35
        Cursor = crHandPoint
        Hint = 'Transmit Configure Tag Command'
        Caption = 'Configure'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 0
        Visible = False
        OnClick = ConfigTagBitBtnClick
      end
      object ConfigTagStaticText: TStaticText
        Left = 8
        Top = 106
        Width = 131
        Height = 17
        Cursor = crHandPoint
        Hint = 'Activate Configure Tag Parameter Box'
        Alignment = taCenter
        AutoSize = False
        Caption = 'Configure'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 19
        OnClick = ConfigTagStaticTextClick
      end
      object ResetCMDTagBitBtn: TBitBtn
        Left = 22
        Top = 420
        Width = 105
        Height = 23
        Caption = 'Reset Commands'
        Font.Charset = ANSI_CHARSET
        Font.Color = clBlue
        Font.Height = -9
        Font.Name = 'Small Fonts'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 20
        Visible = False
        OnClick = ResetCMDTagBitBtnClick
      end
      object ConfigTagLEDBitBtn: TBitBtn
        Left = 6
        Top = 392
        Width = 137
        Height = 31
        Cursor = crHandPoint
        Hint = 'Transmit Config Tag  Random Command'
        Caption = 'Config LED/Speaker'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 21
        Visible = False
        OnClick = ConfigTagLEDBitBtnClick
      end
      object ConfigTagLEDStaticText: TStaticText
        Left = 8
        Top = 400
        Width = 131
        Height = 17
        Alignment = taCenter
        AutoSize = False
        Caption = 'Config LED/Speaker'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -9
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 22
        Visible = False
        OnClick = ConfigTagLEDStaticTextClick
      end
    end
    object ReaderCMDPanel: TPanel
      Left = 872
      Top = 144
      Width = 147
      Height = 449
      BevelOuter = bvNone
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clTeal
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentFont = False
      TabOrder = 32
      object Label93: TLabel
        Left = 2
        Top = 2
        Width = 37
        Height = 13
        Caption = 'reader'
        Visible = False
      end
      object ResetDeviceBitBtn: TBitBtn
        Left = 6
        Top = 20
        Width = 137
        Height = 35
        Cursor = crHandPoint
        Hint = 'Transmit Reset Reader Command'
        Caption = 'Reset'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 0
        OnClick = ResetDeviceBitBtnClick
      end
      object ResetReaderStaticText: TStaticText
        Left = 10
        Top = 30
        Width = 131
        Height = 17
        Cursor = crHandPoint
        Hint = 'Activate Reset Reader Parameter Box'
        Alignment = taCenter
        AutoSize = False
        Caption = 'Reset '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 1
        Visible = False
        OnClick = ResetReaderStaticTextClick
      end
      object EnableReaderBitBtn: TBitBtn
        Left = 6
        Top = 57
        Width = 137
        Height = 34
        Cursor = crHandPoint
        Hint = 'Transmit Enable Reader Command'
        Caption = 'Enable'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 2
        Visible = False
        OnClick = EnableReaderBitBtnClick
      end
      object EnableReaderStaticText: TStaticText
        Left = 8
        Top = 66
        Width = 131
        Height = 17
        Cursor = crHandPoint
        Hint = 'Activate Enable Reader Parameter Box'
        Alignment = taCenter
        AutoSize = False
        Caption = 'Enable'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 3
        OnClick = EnableReaderStaticTextClick
      end
      object DisableReaderBitBtn: TBitBtn
        Left = 6
        Top = 93
        Width = 137
        Height = 34
        Cursor = crHandPoint
        Hint = 'Transmit Disable Reader Command'
        Caption = 'Disable'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 4
        Visible = False
        OnClick = DisableReaderBitBtnClick
      end
      object DisableReaderStaticText: TStaticText
        Left = 10
        Top = 104
        Width = 131
        Height = 13
        Cursor = crHandPoint
        Hint = 'Activate Disable Reader Parameter Box'
        Alignment = taCenter
        AutoSize = False
        Caption = 'Disable'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 5
        OnClick = DisableReaderStaticTextClick
      end
      object QueryReaderBitBtn: TBitBtn
        Left = 6
        Top = 130
        Width = 137
        Height = 31
        Cursor = crHandPoint
        Hint = 'Transmit Query Reader Command'
        Caption = 'Query'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 6
        Visible = False
        OnClick = QueryReaderBitBtnClick
      end
      object QueryReaderStaticText: TStaticText
        Left = 8
        Top = 138
        Width = 131
        Height = 17
        Cursor = crHandPoint
        Hint = 'Activate Query Reader Parameter Box'
        Alignment = taCenter
        AutoSize = False
        Caption = 'Query'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 7
        OnClick = QueryReaderStaticTextClick
      end
      object AssignReaderBitBtn: TBitBtn
        Left = 6
        Top = 165
        Width = 137
        Height = 34
        Cursor = crHandPoint
        Hint = 'Transmit Assign Reader Command'
        Caption = 'Configure'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 8
        Visible = False
        OnClick = AssignReaderBitBtnClick
      end
      object AssignReaderStaticText: TStaticText
        Left = 8
        Top = 174
        Width = 131
        Height = 17
        Cursor = crHandPoint
        Hint = 'Activate Assign Reader Parameter Box'
        Alignment = taCenter
        AutoSize = False
        Caption = 'Configure'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 9
        OnClick = AssignReaderStaticTextClick
      end
      object ReaderVersion: TBitBtn
        Left = 6
        Top = 202
        Width = 137
        Height = 35
        Cursor = crHandPoint
        Hint = 'Transmit Reader Version Command'
        Caption = 'Get Version'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 10
        Visible = False
        OnClick = ReaderVersionClick
      end
      object ReaderVersionStaticText: TStaticText
        Left = 8
        Top = 212
        Width = 131
        Height = 17
        Cursor = crHandPoint
        Hint = 'Activate Reader Version Parameter Box'
        Alignment = taCenter
        AutoSize = False
        Caption = 'Get Version'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 11
        OnClick = ReaderVersionStaticTextClick
      end
      object EnableFGenBitBtn: TBitBtn
        Left = 6
        Top = 416
        Width = 137
        Height = 35
        Cursor = crHandPoint
        Hint = 'Transmit Reader Version Command'
        Caption = 'Enable Transmitter'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 12
        Visible = False
        OnClick = EnableFGenBitBtnClick
      end
      object EnableRdrFGenStaticText: TStaticText
        Left = 10
        Top = 430
        Width = 131
        Height = 13
        Cursor = crHandPoint
        Hint = 'Write To Tag Memory'
        Alignment = taCenter
        AutoSize = False
        Caption = 'Enable Transmitter'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 13
        Visible = False
        OnClick = EnableRdrFGenStaticTextClick
      end
      object RelayBitBtn: TBitBtn
        Left = 6
        Top = 239
        Width = 137
        Height = 34
        Cursor = crHandPoint
        Hint = 'Enable / Disable Relay Command'
        Caption = 'Set Output Relay'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 14
        Visible = False
        OnClick = RelayBitBtnClick
      end
      object RelayStaticText: TStaticText
        Left = 8
        Top = 248
        Width = 131
        Height = 17
        Cursor = crHandPoint
        Hint = 'Enable Disable Relay Parameter Box'
        Alignment = taCenter
        AutoSize = False
        Caption = 'Set Output Relay'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 15
        OnClick = RelayStaticTextClick
      end
      object InputsBitBtn: TBitBtn
        Left = 6
        Top = 276
        Width = 137
        Height = 35
        Cursor = crHandPoint
        Hint = 'Read Inputs Command'
        Caption = 'Config Input Status'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 16
        Visible = False
        OnClick = InputsBitBtnClick
      end
      object InputsStaticText: TStaticText
        Left = 8
        Top = 286
        Width = 131
        Height = 17
        Cursor = crHandPoint
        Alignment = taCenter
        AutoSize = False
        Caption = 'Config Input Status'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 17
        OnClick = InputsStaticTextClick
      end
      object ResetCMDReaderBitBtn: TBitBtn
        Left = 24
        Top = 420
        Width = 105
        Height = 23
        Caption = 'Reset Commands'
        Font.Charset = ANSI_CHARSET
        Font.Color = clBlue
        Font.Height = -9
        Font.Name = 'Small Fonts'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 18
        Visible = False
        OnClick = ResetCMDReaderBitBtnClick
      end
      object SetFStrengthBitBtn: TBitBtn
        Left = 6
        Top = 314
        Width = 137
        Height = 35
        Cursor = crHandPoint
        Hint = 'Read Inputs Command'
        Caption = 'Set Field Strength'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 19
        Visible = False
        OnClick = SetFStrengthBitBtnClick
      end
      object SetFStrengthStaticText: TStaticText
        Left = 8
        Top = 324
        Width = 133
        Height = 17
        Cursor = crHandPoint
        Alignment = taCenter
        AutoSize = False
        Caption = 'Set Field Strength'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 20
        OnClick = SetFStrengthStaticTextClick
      end
      object DownloadRdrBitBtn: TBitBtn
        Left = 6
        Top = 352
        Width = 137
        Height = 31
        Cursor = crHandPoint
        Hint = 'Read Inputs Command'
        Caption = 'Download'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 21
        Visible = False
        OnClick = DownloadRdrBitBtnClick
      end
      object DownloadRdrStaticText: TStaticText
        Left = 8
        Top = 360
        Width = 131
        Height = 17
        Cursor = crHandPoint
        Alignment = taCenter
        AutoSize = False
        Caption = 'Download'
        Enabled = False
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 22
        OnClick = DownloadRdrStaticTextClick
      end
    end
    object TagTempListGroupBox: TGroupBox
      Left = 570
      Top = 6
      Width = 291
      Height = 593
      Caption = 'Tags Temp List '
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clRed
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentFont = False
      TabOrder = 20
      Visible = False
      object TagTempListView: TListView
        Left = 12
        Top = 40
        Width = 273
        Height = 519
        Cursor = crHandPoint
        Color = clInfoBk
        Columns = <
          item
            Width = 1
          end
          item
            Alignment = taCenter
            Caption = 'Tag ID'
            Width = 75
          end
          item
            Alignment = taCenter
            Caption = 'Temp'
            Width = 55
          end
          item
            Alignment = taCenter
            Caption = 'Time'
            Width = 80
          end
          item
            Caption = 'Status'
            Width = 42
          end
          item
            Alignment = taCenter
            Caption = 'Date'
            Width = 80
          end
          item
            Caption = 'Type'
            Width = 40
          end
          item
            Alignment = taCenter
            Caption = 'Rdr'
            Width = 40
          end>
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clGreen
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        GridLines = True
        HideSelection = False
        ReadOnly = True
        RowSelect = True
        ParentFont = False
        SortType = stBoth
        TabOrder = 0
        ViewStyle = vsReport
        OnColumnClick = TagTempListViewColumnClick
        OnCompare = TagTempListViewCompare
        OnCustomDrawItem = TagTempListViewCustomDrawItem
        OnDblClick = TagTempListViewDblClick
      end
      object ClearTagTempListBitBtn: TBitBtn
        Left = 8
        Top = 556
        Width = 81
        Height = 23
        Cursor = crHandPoint
        Caption = 'Clear List'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
        TabOrder = 1
        OnClick = ClearTagTempListBitBtnClick
      end
      object TagTempListCdegRadioButton: TRadioButton
        Left = 150
        Top = 560
        Width = 59
        Height = 17
        Cursor = crHandPoint
        Caption = 'C Deg'
        Checked = True
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 2
        TabStop = True
        OnClick = TagTempListCdegRadioButtonClick
      end
      object TagTempListFdegRadioButton: TRadioButton
        Left = 224
        Top = 560
        Width = 59
        Height = 17
        Cursor = crHandPoint
        Caption = 'F Deg'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clTeal
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 3
        OnClick = TagTempListFdegRadioButtonClick
      end
    end
    object TagDetectedGroupBox: TGroupBox
      Left = 570
      Top = 6
      Width = 291
      Height = 593
      Caption = 'Detected Tags'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clRed
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentFont = False
      TabOrder = 4
      object FGLabel: TLabel
        Left = 38
        Top = 382
        Width = 24
        Height = 13
        Caption = 'OFF'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object GroupLabel: TLabel
        Left = 166
        Top = 382
        Width = 28
        Height = 13
        Caption = 'OFF '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object MTLabel: TLabel
        Left = 258
        Top = 382
        Width = 28
        Height = 13
        Caption = 'OFF '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label121: TLabel
        Left = 14
        Top = 382
        Width = 25
        Height = 13
        Caption = 'FG: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label122: TLabel
        Left = 112
        Top = 382
        Width = 52
        Height = 13
        Caption = 'GroupID:'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label123: TLabel
        Left = 232
        Top = 382
        Width = 27
        Height = 13
        Caption = 'MT: '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clPurple
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object DetectedTagListView: TListView
        Left = 14
        Top = 16
        Width = 275
        Height = 363
        Cursor = crHandPoint
        Color = clInfoBk
        Columns = <
          item
            Width = 1
          end
          item
            Caption = 'FG'
            Width = 29
          end
          item
            Alignment = taCenter
            Caption = 'Rdr'
            Width = 32
          end
          item
            Alignment = taCenter
            Caption = 'Type'
            Width = 43
          end
          item
            Alignment = taCenter
            Caption = 'ID'
            Width = 75
          end
          item
            Alignment = taCenter
            Caption = 'RSSI'
            Width = 37
          end
          item
            Caption = 'Cmd'
            Width = 35
          end
          item
            Caption = 'Dup.'
          end>
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clGreen
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        GridLines = True
        HideSelection = False
        ReadOnly = True
        RowSelect = True
        ParentFont = False
        SortType = stBoth
        TabOrder = 0
        ViewStyle = vsReport
        OnColumnClick = DetectedTagListViewColumnClick
        OnCompare = DetectedTagListViewCompare
        OnDblClick = DetectedTagListViewDblClick
        OnDrawItem = DetectedTagListViewDrawItem
      end
      object ClearTagListBitBtn: TBitBtn
        Left = 12
        Top = 566
        Width = 81
        Height = 21
        Cursor = crHandPoint
        Caption = 'Clear List'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
        TabOrder = 1
        OnClick = ClearTagListBitBtnClick
      end
      object NewListItemCheckBox: TCheckBox
        Left = 190
        Top = 568
        Width = 91
        Height = 17
        Cursor = crHandPoint
        Caption = 'Keep List Items'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
        TabOrder = 2
        OnClick = NewListItemCheckBoxClick
      end
      object GroupBox17: TGroupBox
        Left = 12
        Top = 460
        Width = 269
        Height = 101
        Caption = 'Detected Tag Report'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
        TabOrder = 3
        object ReportType1Label: TLabel
          Left = 78
          Top = 22
          Width = 6
          Height = 13
          Caption = '0'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object ReportType03Label: TLabel
          Left = 36
          Top = 40
          Width = 36
          Height = 13
          Alignment = taRightJustify
          Caption = 'Type 3:'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object ReportType3Label: TLabel
          Left = 78
          Top = 40
          Width = 6
          Height = 13
          Caption = '0'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object ReportType04Label: TLabel
          Left = 198
          Top = 40
          Width = 36
          Height = 13
          Alignment = taRightJustify
          Caption = 'Type 4:'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object ReportType4Label: TLabel
          Left = 238
          Top = 40
          Width = 6
          Height = 13
          Caption = '0'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object ReportType01Label: TLabel
          Left = 38
          Top = 22
          Width = 36
          Height = 13
          Alignment = taRightJustify
          Caption = 'Type 1:'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object ReportType02Label: TLabel
          Left = 197
          Top = 22
          Width = 36
          Height = 13
          Alignment = taRightJustify
          Caption = 'Type 2:'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object ReportType2Label: TLabel
          Left = 238
          Top = 22
          Width = 6
          Height = 13
          Caption = '0'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object TotalLabel: TLabel
          Left = 208
          Top = 76
          Width = 27
          Height = 13
          Alignment = taRightJustify
          Caption = 'Total:'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGreen
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object ReportTotalLabel: TLabel
          Left = 238
          Top = 76
          Width = 6
          Height = 13
          Caption = '0'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clMaroon
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label37: TLabel
          Left = 118
          Top = 76
          Width = 56
          Height = 13
          Alignment = taRightJustify
          Caption = 'Duplicates: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clPurple
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object ReportNDupLabel: TLabel
          Left = 174
          Top = 76
          Width = 6
          Height = 13
          Caption = '0'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clMaroon
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object ReportType05Label: TLabel
          Left = 36
          Top = 58
          Width = 36
          Height = 13
          Alignment = taRightJustify
          Caption = 'Type 5:'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object ReportType5Label: TLabel
          Left = 78
          Top = 58
          Width = 6
          Height = 13
          Caption = '0'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object ReportType06Label: TLabel
          Left = 198
          Top = 58
          Width = 36
          Height = 13
          Alignment = taRightJustify
          Caption = 'Type 6:'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object ReportType6Label: TLabel
          Left = 238
          Top = 58
          Width = 6
          Height = 13
          Caption = '0'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGray
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label115: TLabel
          Left = 35
          Top = 76
          Width = 41
          Height = 13
          Alignment = taRightJustify
          Caption = 'Factory: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object ReportFACTLabel: TLabel
          Left = 78
          Top = 76
          Width = 6
          Height = 13
          Caption = '0'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clMaroon
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
      end
      object GroupBox19: TGroupBox
        Left = 12
        Top = 400
        Width = 269
        Height = 57
        Hint = 'Detected Message Box'
        Caption = 'Detected Tag Message'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 4
        object DetectedMsg: TLabel
          Left = 14
          Top = 18
          Width = 93
          Height = 16
          Caption = 'DetectedMsg'
          Font.Charset = ANSI_CHARSET
          Font.Color = clBlue
          Font.Height = -13
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
        end
        object TamperSWMsg: TLabel
          Left = 78
          Top = 38
          Width = 109
          Height = 13
          Caption = 'Tamper  Switch  Enabled !'
          Font.Charset = ANSI_CHARSET
          Font.Color = clRed
          Font.Height = -11
          Font.Name = 'MS Serif'
          Font.Style = []
          ParentFont = False
        end
        object ClearMsg: TBitBtn
          Left = 250
          Top = 38
          Width = 15
          Height = 17
          Cursor = crHandPoint
          Hint = 'Clear Message'
          Caption = 'C'
          TabOrder = 0
          OnClick = ClearMsgClick
        end
      end
    end
    object GroupBox1: TGroupBox
      Left = 11
      Top = 390
      Width = 270
      Height = 209
      Caption = 'Transmit Window'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clRed
      Font.Height = -15
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentFont = False
      TabOrder = 0
      object TxListBox: TListBox
        Left = 10
        Top = 24
        Width = 251
        Height = 147
        Color = clInfoBk
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        IntegralHeight = True
        ItemHeight = 13
        ParentFont = False
        TabOrder = 0
      end
      object TXNormalDisplayGroup: TPanel
        Left = 16
        Top = 24
        Width = 241
        Height = 175
        BevelOuter = bvLowered
        TabOrder = 1
        Visible = False
        DesignSize = (
          241
          175)
        object TxTypeLabel: TLabel
          Left = 8
          Top = 7
          Width = 52
          Height = 13
          Caption = 'Tag Type: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object TxIDLabel: TLabel
          Left = 8
          Top = 26
          Width = 39
          Height = 13
          Caption = 'Tag ID: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label5: TLabel
          Left = 8
          Top = 46
          Width = 66
          Height = 13
          Caption = 'ResendTime: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label6: TLabel
          Left = 8
          Top = 104
          Width = 53
          Height = 13
          Caption = 'Command: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object TxTagTypeLabel: TLabel
          Left = 65
          Top = 7
          Width = 41
          Height = 13
          Caption = 'Label 00'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object TxTagIDLabel: TLabel
          Left = 49
          Top = 26
          Width = 38
          Height = 13
          Caption = 'Label01'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object TxCommandLabel: TLabel
          Left = 65
          Top = 104
          Width = 38
          Height = 13
          Caption = 'Label03'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object TxTagTimeLabel: TLabel
          Left = 78
          Top = 46
          Width = 30
          Height = 13
          Caption = 'Lab02'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label7: TLabel
          Left = 8
          Top = 85
          Width = 41
          Height = 13
          Caption = 'Tag Dir: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object TxTagDirection: TLabel
          Left = 50
          Top = 85
          Width = 38
          Height = 13
          Caption = 'Label08'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object NewTagIDLabel: TLabel
          Left = 188
          Top = 26
          Width = 38
          Height = 13
          Caption = 'Label10'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label2: TLabel
          Left = 8
          Top = 65
          Width = 55
          Height = 13
          Caption = 'Reader ID: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object TxReaderIDLabel: TLabel
          Left = 65
          Top = 65
          Width = 38
          Height = 13
          Caption = 'Label13'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label1: TLabel
          Left = 141
          Top = 46
          Width = 48
          Height = 13
          Caption = 'TIF Time: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object TxTIFLabel: TLabel
          Left = 189
          Top = 46
          Width = 30
          Height = 13
          Caption = 'Lab16'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label10: TLabel
          Left = 169
          Top = 65
          Width = 21
          Height = 13
          Caption = 'GC: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object TxGCLabel: TLabel
          Left = 189
          Top = 65
          Width = 30
          Height = 13
          Caption = 'Lab17'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label18: TLabel
          Left = 112
          Top = 85
          Width = 77
          Height = 13
          Caption = 'Tamper Switch: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object TxTamperLabel: TLabel
          Left = 189
          Top = 85
          Width = 18
          Height = 13
          Caption = 'L18'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object TxNewTagIDLabel: TLabel
          Left = 124
          Top = 26
          Width = 64
          Height = 13
          Caption = 'New Tag ID: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object FieldGenIDLabel01: TLabel
          Left = 124
          Top = 8
          Width = 65
          Height = 13
          Caption = 'Field Gen ID: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          Visible = False
        end
        object FieldGenIDLabel: TLabel
          Left = 188
          Top = 8
          Width = 38
          Height = 13
          Caption = 'Label19'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          Visible = False
        end
        object TxClearBitBtn: TBitBtn
          Left = 189
          Top = 153
          Width = 51
          Height = 20
          Cursor = crHandPoint
          Anchors = [akRight, akBottom]
          Caption = 'Clear'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          TabOrder = 0
          OnClick = TxClearBitBtnClick
        end
      end
      object StopGoTXBitBtn: TBitBtn
        Left = 54
        Top = 174
        Width = 62
        Height = 21
        Caption = 'Stop'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
        TabOrder = 2
        Visible = False
        OnClick = StopGoTXBitBtnClick
      end
      object TxClear: TBitBtn
        Left = 154
        Top = 174
        Width = 63
        Height = 21
        Caption = 'Clear'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
        TabOrder = 3
        Visible = False
        OnClick = TxClearClick
      end
    end
    object GroupBox3: TGroupBox
      Left = 290
      Top = 390
      Width = 273
      Height = 209
      Caption = 'Receive Window'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clRed
      Font.Height = -15
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentFont = False
      TabOrder = 2
      object RecListBox: TListBox
        Left = 10
        Top = 24
        Width = 253
        Height = 147
        Color = clInfoBk
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clGreen
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        IntegralHeight = True
        ItemHeight = 13
        ParentFont = False
        TabOrder = 0
      end
      object RecClear: TBitBtn
        Left = 154
        Top = 175
        Width = 61
        Height = 20
        Caption = 'Clear'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clGreen
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
        TabOrder = 1
        Visible = False
        OnClick = RecClearClick
      end
      object StopGoRXBitBtn: TBitBtn
        Left = 54
        Top = 175
        Width = 62
        Height = 20
        Caption = 'Stop'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clGreen
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
        TabOrder = 2
        Visible = False
        OnClick = StopGoRXBitBtnClick
      end
      object RXNormalDisplayGroup: TPanel
        Left = 12
        Top = 24
        Width = 249
        Height = 175
        BevelOuter = bvLowered
        TabOrder = 3
        DesignSize = (
          249
          175)
        object Label11: TLabel
          Left = 7
          Top = 7
          Width = 52
          Height = 13
          Caption = 'Tag Type: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label12: TLabel
          Left = 7
          Top = 24
          Width = 39
          Height = 13
          Caption = 'Tag ID: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label13: TLabel
          Left = 7
          Top = 42
          Width = 69
          Height = 13
          Caption = 'Resend Time: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label14: TLabel
          Left = 7
          Top = 96
          Width = 41
          Height = 13
          Caption = 'Tag Dir: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object RxTagTypeLabel: TLabel
          Left = 63
          Top = 7
          Width = 38
          Height = 13
          Caption = 'Label04'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGreen
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object RxTagIDLabel: TLabel
          Left = 49
          Top = 24
          Width = 38
          Height = 13
          Caption = 'Label05'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGreen
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object RxTagDirection: TLabel
          Left = 47
          Top = 96
          Width = 38
          Height = 13
          Caption = 'Label07'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGreen
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object RxTagTimeLabel: TLabel
          Left = 80
          Top = 42
          Width = 38
          Height = 13
          Caption = 'Label06'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGreen
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label8: TLabel
          Left = 7
          Top = 132
          Width = 53
          Height = 13
          Caption = 'Command: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object RxCommandLabel: TLabel
          Left = 62
          Top = 132
          Width = 38
          Height = 13
          Caption = 'Label09'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGreen
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label3: TLabel
          Left = 7
          Top = 78
          Width = 49
          Height = 13
          Caption = 'Group ID: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object GroupID: TLabel
          Left = 56
          Top = 78
          Width = 38
          Height = 13
          Caption = 'Label11'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGreen
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label4: TLabel
          Left = 7
          Top = 60
          Width = 55
          Height = 13
          Caption = 'Reader ID: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object RxReaderIDLabel: TLabel
          Left = 62
          Top = 60
          Width = 39
          Height = 13
          AutoSize = False
          Caption = 'Label14'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGreen
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label9: TLabel
          Left = 158
          Top = 42
          Width = 48
          Height = 13
          Caption = 'TIF Time: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object RxTIFLabel: TLabel
          Left = 207
          Top = 42
          Width = 18
          Height = 13
          Caption = 'L16'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGreen
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label16: TLabel
          Left = 185
          Top = 60
          Width = 21
          Height = 13
          Caption = 'GC: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object RxGCLabel: TLabel
          Left = 207
          Top = 60
          Width = 18
          Height = 13
          Caption = 'L17'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGreen
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label17: TLabel
          Left = 152
          Top = 96
          Width = 54
          Height = 13
          Caption = 'Tampered: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object RxTamperLabel: TLabel
          Left = 207
          Top = 96
          Width = 18
          Height = 13
          Caption = 'L12'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGreen
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label19: TLabel
          Left = 150
          Top = 26
          Width = 55
          Height = 13
          Caption = 'Cont. 4.33: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object ContinuousLabel: TLabel
          Left = 207
          Top = 26
          Width = 18
          Height = 13
          Caption = 'L12'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGreen
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label15: TLabel
          Left = 149
          Top = 78
          Width = 61
          Height = 13
          Caption = 'Tag Status:  '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object RxTagStatusLabel: TLabel
          Left = 208
          Top = 78
          Width = 18
          Height = 13
          Caption = 'L30'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGreen
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label24: TLabel
          Left = 143
          Top = 7
          Width = 66
          Height = 13
          Caption = 'Tag Version:  '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object RxTagVersionLabel: TLabel
          Left = 208
          Top = 8
          Width = 18
          Height = 13
          Caption = 'L31'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGreen
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label33: TLabel
          Left = 7
          Top = 114
          Width = 65
          Height = 13
          Caption = 'Field Gen ID: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object FGenIDLabel: TLabel
          Left = 72
          Top = 114
          Width = 18
          Height = 13
          Caption = 'L34'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGreen
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label235: TLabel
          Left = 168
          Top = 114
          Width = 39
          Height = 13
          Caption = 'Battery: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object RxBatteryLabel: TLabel
          Left = 207
          Top = 118
          Width = 18
          Height = 13
          Caption = 'L12'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGreen
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object Label262: TLabel
          Left = 176
          Top = 132
          Width = 31
          Height = 13
          Caption = 'RSSI: '
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlack
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object RxRSSILabel: TLabel
          Left = 207
          Top = 132
          Width = 18
          Height = 13
          Caption = 'L12'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGreen
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
        end
        object RxClearBitBtn: TBitBtn
          Left = 198
          Top = 154
          Width = 50
          Height = 20
          Cursor = crHandPoint
          Anchors = [akRight, akBottom]
          Caption = 'Clear'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clGreen
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          TabOrder = 0
          OnClick = RxClearBitBtnClick
        end
      end
    end
    object GroupBox6: TGroupBox
      Left = 11
      Top = 338
      Width = 550
      Height = 42
      Caption = 'System Message'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clRed
      Font.Height = -15
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentFont = False
      TabOrder = 5
      object StaticText: TStaticText
        Left = 9
        Top = 20
        Width = 19
        Height = 20
        Caption = 'ID'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlue
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 0
      end
      object ClearMsgBitBtn: TBitBtn
        Left = 496
        Top = 12
        Width = 50
        Height = 25
        Cursor = crHandPoint
        Caption = 'Clear All'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
        TabOrder = 1
        OnClick = ClearMsgBitBtnClick
      end
      object ConfigTxTimeBitBtn: TBitBtn
        Left = 464
        Top = 12
        Width = 25
        Height = 26
        Cursor = crHandPoint
        Hint = 'Transmit Configure Tx Time Command'
        Caption = 'Config TX Time'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 2
        Visible = False
        OnClick = ConfigTxTimeBitBtnClick
      end
      object ConfigTxTimeStaticText: TStaticText
        Left = 440
        Top = 17
        Width = 17
        Height = 17
        Cursor = crHandPoint
        Hint = 'Activate Configure Reader Transmit Time Parameter Box'
        Alignment = taCenter
        AutoSize = False
        Caption = 'Config TX Time'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clNavy
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = [fsBold]
        ParentFont = False
        ParentShowHint = False
        ShowHint = True
        TabOrder = 3
        Visible = False
        OnClick = ConfigTxTimeStaticTextClick
      end
    end
  end
  object MainStatusBar: TStatusBar
    Left = 0
    Top = 666
    Width = 1028
    Height = 26
    Anchors = []
    Panels = <
      item
        Alignment = taCenter
        Style = psOwnerDraw
        Width = 8
      end
      item
        Alignment = taCenter
        Style = psOwnerDraw
        Width = 65
      end
      item
        Alignment = taCenter
        Style = psOwnerDraw
        Width = 65
      end
      item
        Alignment = taCenter
        Style = psOwnerDraw
        Text = 'No Connection'
        Width = 65
      end
      item
        Alignment = taCenter
        Style = psOwnerDraw
        Width = 45
      end
      item
        Alignment = taCenter
        Style = psOwnerDraw
        Width = 50
      end
      item
        Alignment = taCenter
        Style = psOwnerDraw
        Width = 40
      end>
    SimplePanel = False
    OnDrawPanel = MainStatusBarDrawPanel
  end
  object ToolBar1: TToolBar
    Left = 0
    Top = 0
    Width = 1028
    Height = 45
    AutoSize = True
    ButtonHeight = 41
    ButtonWidth = 52
    Caption = 'ToolBar1'
    EdgeOuter = esRaised
    Images = ImageList1
    TabOrder = 0
    Wrapable = False
    object CommToolButton: TToolButton
      Left = 0
      Top = 2
      Cursor = crHandPoint
      Hint = 'Configure Communication'
      AutoSize = True
      Caption = 'CommToolButton'
      ImageIndex = 0
      ParentShowHint = False
      ShowHint = True
      OnClick = CommToolButtonClick
    end
    object DebugDisplayToolButton: TToolButton
      Left = 52
      Top = 2
      Cursor = crHandPoint
      Hint = 'Display Debug Information'
      AutoSize = True
      Caption = 'DebugDisplayToolButton'
      ImageIndex = 1
      ParentShowHint = False
      ShowHint = True
      OnClick = DebugDisplayToolButtonClick
    end
    object TextDisplayToolButton: TToolButton
      Left = 104
      Top = 2
      Cursor = crHandPoint
      Hint = 'Display Text Information'
      AutoSize = True
      Caption = 'TextDisplayToolButton'
      ImageIndex = 2
      ParentShowHint = False
      ShowHint = True
      Visible = False
      OnClick = TextDisplayToolButtonClick
    end
    object RecordToolButton: TToolButton
      Left = 156
      Top = 2
      Cursor = crHandPoint
      Hint = 'Configure File Recording'
      AutoSize = True
      Caption = 'RecordToolButton'
      ImageIndex = 3
      ParentShowHint = False
      ShowHint = True
      Visible = False
      OnClick = RecordToolButtonClick
    end
    object StartRecToolButton: TToolButton
      Left = 208
      Top = 2
      Cursor = crHandPoint
      Hint = 'Start Recording'
      AutoSize = True
      Caption = 'StartRecToolButton'
      ImageIndex = 4
      ParentShowHint = False
      ShowHint = True
      Visible = False
      OnClick = StartRecToolButtonClick
    end
    object StopRecToolButton: TToolButton
      Left = 260
      Top = 2
      Hint = 'Stop Recording'
      AutoSize = True
      Caption = 'StopRecToolButton'
      ImageIndex = 5
      ParentShowHint = False
      ShowHint = True
      Visible = False
      OnClick = StopRecToolButtonClick
    end
    object ConfigToolButton: TToolButton
      Left = 312
      Top = 2
      Cursor = crHandPoint
      Hint = 'Configure Programming Station'
      Caption = 'ConfigToolButton'
      ImageIndex = 8
      ParentShowHint = False
      ShowHint = True
      OnClick = ConfigToolButtonClick
    end
    object ReaderFgenToolButton: TToolButton
      Left = 364
      Top = 2
      Hint = 'Set Programming Station To Field Generator Mode'
      Caption = 'ReaderFgenToolButton'
      ImageIndex = 12
      ParentShowHint = False
      ShowHint = True
      OnClick = ReaderFgenToolButtonClick
    end
    object CloseToolButton: TToolButton
      Left = 416
      Top = 2
      Cursor = crHandPoint
      Hint = 'Close Programming Station Application'
      Caption = 'CloseToolButton'
      ImageIndex = 7
      ParentShowHint = False
      ShowHint = True
      OnClick = CloseToolButtonClick
    end
    object EncryptToolButton: TToolButton
      Left = 468
      Top = 2
      Caption = 'EncryptToolButton'
      ImageIndex = 14
      Visible = False
      OnClick = EncryptToolButtonClick
    end
    object HelpToolButton: TToolButton
      Left = 520
      Top = 2
      Cursor = crHandPoint
      Hint = 'About Programming Station'
      AutoSize = True
      Caption = 'HelpToolButton'
      ImageIndex = 6
      ParentShowHint = False
      ShowHint = True
      OnClick = HelpToolButtonClick
    end
  end
  object Timer2: TTimer
    OnTimer = Timer2Timer
    Left = 534
    Top = 50
  end
  object ImageList1: TImageList
    Height = 35
    Width = 45
    Left = 686
    Top = 52
    Bitmap = {
      494C01010F00130004002D002300FFFFFFFFFF10FFFFFFFFFFFFFFFF424D3600
      0000000000003600000028000000B4000000AF000000010020000000000030EC
      0100000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000848484000000000000000000000000000000000000000000000000000000
      0000848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000FF000000FF000000FF000000FF000000FF000000FF008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400000000000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000084848400FFFF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF00848484008484840084848400000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000FF000000FF000000
      FF000000FF000000FF000000FF0084848400FFFFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0084848400848484008484
      840084848400000000000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400FFFFFF000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000FFFF0000FFFF008484840084848400848484008484
      8400848484000000000000000000000000000000000000000000000000000000
      0000000000000000FF000000FF000000FF000000FF000000FF000000FF008484
      8400FFFFFF00C6C6C600C6C6C600C6C6C600FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600FFFFFF00C6C6C600C6C6C60000FF
      FF0000FFFF008484840084848400848484008484840084848400000000000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000084848400FFFF
      FF00848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      840084848400848484008484840084848400848484008484840000FFFF0000FF
      FF00848484008484840084848400848484008484840000000000000000000000
      000000000000000000000000000000000000000000000000FF000000FF000000
      FF000000FF000000FF000000FF0084848400FFFFFF0084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      840084848400848484008484840000FFFF0000FFFF0084848400848484008484
      84008484840084848400000000000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF0000000000000000000000000000000000000000000000
      00000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF0000000000000000000000FF000000FF000000000000000000000000000000
      000000000000C0C0C0000000FF000000FF000000000000000000000000000000
      000000000000C0C0C0000000FF000000FF00C0C0C00000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400FFFFFF000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000FFFF0000FFFF008484840084848400848484008484
      8400848484000000000000000000000000000000000000000000000000000000
      0000000000000000FF000000FF000000FF000000FF000000FF000000FF008484
      8400FFFFFF00C6C6C600C6C6C600C6C6C600FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600FFFFFF00C6C6C600C6C6C60000FF
      FF0000FFFF008484840084848400848484008484840084848400000000000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF00000000000000
      0000000000000000000000000000000000000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF0000000000000000000000FF000000
      FF0000000000000000000000000000000000000000000000FF000000FF000000
      FF00000000000000000000000000C0C0C0000000FF000000FF000000FF000000
      FF000000FF000000FF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000084848400FFFF
      FF00848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      840084848400848484008484840084848400848484008484840000FFFF0000FF
      FF00848484008484840084848400848484008484840000000000000000000000
      000000000000000000000000000000000000000000000000FF000000FF000000
      FF000000FF000000FF000000FF0084848400FFFFFF0084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      840084848400848484008484840000FFFF0000FFFF0084848400848484008484
      84008484840084848400000000000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF0000000000000000000000000000000000000000000000
      00000000FF000000FF0000000000000000000000000000000000000000000000
      000000000000000000000000FF000000FF000000000000000000000000000000
      00000000FF00C0C0C0000000FF000000FF000000000000000000C0C0C0000000
      FF00C0C0C00000000000000000000000000000000000C0C0C000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400FFFFFF008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840000FFFF0000FFFF008484840084848400848484008484
      8400848484000000000000000000000000000000000000000000000000000000
      0000000000000000FF000000FF000000FF000000FF000000FF000000FF008484
      8400FFFFFF008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      84008484840084848400848484008484840084848400848484008484840000FF
      FF0000FFFF008484840084848400848484008484840084848400000000000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF00000000000000
      0000000000000000000000000000000000000000FF000000FF00000000000000
      00000000000000000000000000000000000000000000000000000000FF000000
      FF000000000000000000000000000000FF0080808000000000000000FF000000
      FF0000000000000000000000FF000000FF000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000084848400FFFF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF00848484008484840084848400848484008484840000000000000000000000
      000000000000000000000000000000000000000000000000FF000000FF000000
      FF000000FF000000FF000000FF0084848400FFFFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0084848400848484008484
      84008484840084848400000000000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF0000000000000000000000000000000000000000000000
      00000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF0000000000000000000000FF000000FF000000000000000000C0C0C0000000
      FF0000000000000000000000FF000000FF0000000000000000000000FF000000
      FF00000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400FFFFFF0000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000840000008400000084
      0000008400000084000000FFFF0000FFFF008484840084848400848484008484
      8400848484000000000000000000000000000000000000000000000000000000
      0000000000000000FF000000FF000000FF000000FF000000FF000000FF008484
      8400FFFFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF00008400000084000000FF0000008400000084000000FF
      FF0000FFFF008484840084848400848484008484840084848400000000000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF00000000000000
      0000000000000000000000000000000000000000FF000000FF00000000000000
      00000000000000000000000000000000000000000000000000000000FF000000
      FF00000000000000FF000000FF000000000000000000000000000000FF000000
      FF000000000000000000808080000000FF000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000084848400FFFF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FF000000FF000000FF000000FF000000FF000000FFFF0000FF
      FF00848484008484840084848400848484008484840000000000000000000000
      000000000000000000000000000000000000000000000000FF000000FF000000
      FF000000FF000000FF000000FF0084848400FFFFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF000000FF
      000000FF000000FF000000FF000000FFFF0000FFFF0084848400848484008484
      84008484840084848400000000000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF0000000000000000000000000000000000000000000000
      00000000FF000000FF0000000000000000000000000000000000000000000000
      000000000000000000000000FF000000FF00C0C0C0000000FF00000000000000
      000000000000000000000000FF000000FF000000000000000000C0C0C0000000
      FF00C0C0C00000000000000000000000000000000000C0C0C000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400FFFFFF000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840084848400848484008484
      8400848484000000000000000000000000000000000000000000000000000000
      0000000000000000FF000000FF000000FF000000FF000000FF000000FF008484
      8400FFFFFF00C6C6C600C6C6C600C6C6C600FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C6008484840084848400848484008484840084848400000000000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF00000000000000
      0000000000000000000000000000000000000000FF000000FF00000000000000
      00000000000000000000000000000000000000000000000000000000FF000000
      FF000000FF00C0C0C000000000000000000000000000000000000000FF000000
      FF000000000000000000000000000000FF000000FF00C0C0C000000000000000
      0000C0C0C0000000FF00C0C0C000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000008484
      840000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0084848400848484008484840000000000000000000000
      000000000000000000000000000000000000000000000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF008484840000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF008484
      84008484840084848400000000000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF0000000000000000000000000000000000000000000000
      00000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF0000000000000000000000FF000000FF000000FF0000000000000000000000
      000000000000000000000000FF000000FF00000000000000000000000000C0C0
      C0000000FF000000FF000000FF000000FF000000FF000000FF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000848484008484840000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF008484
      8400848484000000000000000000000000000000000000000000000000000000
      0000000000000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF00848484008484840000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF008484840084848400000000000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840000000000000000000000
      000000000000000000000000000000000000000000000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      840084848400848484000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000FF000000FF0000000000000000000000
      0000000000000000FF000000000000000000000000000000FF00000000000000
      00000000000000000000000000000000000000000000000000000000FF000000
      FF00000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF0000FFFF0000FFFF000000FF000000FF000000FF000000FF0000FFFF000000
      FF000000FF000000FF0000FFFF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF0000FFFF0000FFFF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000FF000000FF000000FF000000FF000000
      00000000000000000000000000000000FF000000FF0000000000000000000000
      000000000000000000000000FF000000FF000000FF0000000000000000000000
      0000000000000000000000000000000000000000FF000000FF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF0000FFFF0000FFFF0000FFFF0000FFFF000000FF000000FF000000FF000000
      FF0000FFFF0000FFFF000000FF000000FF000000FF000000FF000000FF0000FF
      FF0000FFFF0000FFFF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF0000FFFF0000FFFF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000000000000000FF000000
      00000000000000000000000000000000000000000000000000000000FF000000
      000000000000000000000000FF000000FF000000000000000000000000000000
      0000000000000000FF000000FF000000FF000000FF000000FF00000000000000
      000000000000000000000000FF000000FF000000FF000000FF00000000000000
      000000000000000000000000000000000000000000000000FF000000FF000000
      FF000000FF000000FF000000FF0000FFFF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF0000FFFF000000FF000000FF000000FF0000FF
      FF0000FFFF000000FF000000FF000000FF000000FF000000FF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF000000FF000000FF000000FF000000FF0000FF
      FF0000FFFF0000FFFF0000FFFF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000FF00000000000000000000000000000000000000
      000000000000000000000000FF000000000000000000000000000000FF000000
      FF0000000000000000000000000000000000000000000000FF000000FF000000
      FF000000FF000000FF00000000000000000000000000000000000000FF000000
      FF000000FF000000FF0000000000000000000000000000000000000000000000
      0000000000000000FF000000FF000000FF000000FF000000FF000000FF0000FF
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF0000FF
      FF000000FF000000FF000000FF0000FFFF0000FFFF000000FF000000FF000000
      FF000000FF000000FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF000000
      FF000000FF000000FF000000FF0000FFFF0000FFFF0000FFFF0000FFFF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000000000000000FF000000
      00000000000000000000000000000000000000000000000000000000FF000000
      0000000000000000000000000000000000000000FF0000000000000000000000
      00000000000000000000000000000000000000000000000000000000FF000000
      00000000000000000000000000000000000000000000000000000000FF000000
      000000000000000000000000000000000000000000000000FF000000FF000000
      FF000000FF000000FF000000FF0000FFFF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF0000FFFF000000FF000000FF000000FF000000
      FF000000FF0000FFFF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF0000FFFF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF0000FFFF000000FF000000FF000000FF000000
      FF000000FF000000FF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000FF000000FF0000000000000000000000000000000000000000000000
      000000000000000000000000FF00000000000000000000000000000000000000
      0000000000000000FF0000000000000000000000000000000000000000000000
      000000000000000000000000FF00000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000FF000000FF000000FF000000FF0000FFFF0000FFFF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF0000FF
      FF000000FF000000FF000000FF000000FF000000FF000000FF0000FFFF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF0000FF
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000FF000000000000000000000000000000
      00000000000000000000000000000000FF000000FF000000FF00000000000000
      000000000000000000000000000000000000000000000000FF00000000000000
      00000000000000000000000000000000000000000000000000000000FF000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000FF000000FF000000
      FF0000FFFF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF0000FFFF0000FFFF0000FFFF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF0000FFFF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF0000FFFF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000FF000000FF000000
      00000000000000000000000000000000000000000000000000000000FF000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000FF0000000000000000000000000000000000000000000000
      00000000000000000000000000000000FF000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000FF0000FFFF0000FFFF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF0000FFFF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF0000FFFF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF0000FFFF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000FF000000FF000000FF00
      0000FF000000FF000000FF000000000000000000000000000000000000008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF0000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000000000FF00
      0000FF000000FF000000FF000000FF0000000000000000000000000000000000
      0000000000000000000000000000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF0000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000FF000000FF000000FF000000FF000000000000000000
      000000000000000000000000000000000000000000000000000000000000FFFF
      FF00848484000000000000000000FFFFFF0000000000FFFFFF0000000000FFFF
      FF0084848400FFFFFF0000000000FFFFFF008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000FF000000FF00
      0000FF0000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000FFFFFF0084848400FFFFFF0000000000FFFF
      FF0000000000FFFFFF0000000000FFFFFF0084848400FFFFFF0000000000FFFF
      FF00848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000084848400FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000FF000000FF000000FF000000FF00000000000000000000000000
      000000000000000000000000000000000000000000000000000000000000FFFF
      FF0084848400FFFFFF0000000000FFFFFF0000000000FFFFFF0000000000FFFF
      FF0084848400FFFFFF0000000000FFFFFF008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF0000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF00000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000FF000000FF000000FF00
      0000FF0000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000FFFFFF00000000000000000000000000FFFF
      FF0000000000FFFFFF0000000000FFFFFF0084848400FFFFFF0000000000FFFF
      FF00848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF00000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000FF000000FF000000FF000000FF000000FF000000000000000000
      000000000000FF0000000000000000000000000000000000000000000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000FF0000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000FF000000FF000000FF00
      0000FF000000FF000000FF00000000000000FF000000FF000000000000000000
      0000000000000000000000000000FFFFFF0084848400FFFFFF00000000000000
      000000000000FFFFFF0000000000FFFFFF00848484000000000000000000FFFF
      FF00848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000FF000000FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF000000FF000000FF0000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840000000000000000000000
      0000000000000000000000000000000000008484840000000000000000000000
      0000000000000000000000000000000000008484840000000000000000000000
      0000000000000000000000000000000000008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF0000000000000000000000000000000000000000000000FFFF
      FF0084848400FFFFFF0000000000F7FFFF0000000000FFFFFF0000000000FFFF
      FF0084848400FFFFFF0000000000FFFFFF008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF0000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000000000000000
      0000000000000000000000000000FFFFFF0084848400FFFFFF0000000000F7FF
      FF0000000000FFFFFF0000000000FFFFFF0084848400FFFFFF0000000000FFFF
      FF00848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000FFFFFF00FFFFFF000000000000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF000000000000000000FFFFFF00FFFFFF0000000000000000000000
      00000000000000000000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000000
      000000000000FFFFFF00FFFFFF00FFFFFF008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840084848400FFFFFF000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000008484840084848400848484008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF0000000000000000000000000000000000000000000000FFFF
      FF0084848400FFFFFF0000000000F7FFFF0000000000FFFFFF0000000000FFFF
      FF0084848400FFFFFF0000000000FFFFFF008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000FFFFFF00FFFFFF000000
      000000000000FFFFFF00FFFFFF00FFFFFF00FFFFFF000000000000000000FFFF
      FF000000000000000000FFFFFF00FFFFFF00FFFFFF000000000000000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF000000000000000000FFFFFF00FFFFFF00FFFF
      FF00848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF000000FF000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00008484840084848400FFFFFF000000000000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0000000000000000000000000084848400848484008484
      8400848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FF000000FF000000FF000000FF000000FF000000FF000000000000000000
      0000000000000000000000000000FFFFFF0084848400FFFFFF00000000000000
      000000000000FFFFFF0000000000FFFFFF00848484000000000000000000FFFF
      FF00848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000FFFFFF00FFFFFF00FFFFFF000000000000000000000000000000
      00000000000000000000FFFFFF00FFFFFF000000000000000000FFFFFF00FFFF
      FF00FFFFFF000000000000000000FFFFFF00FFFFFF00FFFFFF00FFFFFF000000
      000000000000FFFFFF00FFFFFF00FFFFFF008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000FF0000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840084848400FFFFFF000000
      0000000000008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400000000000000
      0000000000008484840084848400848484008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF0000000000000000000000000000000000000000000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF000000000000000000FFFFFF00FFFFFF000000000000000000FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000000000000000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF000000000000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF000000FF000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00008484840084848400FFFFFF000000000000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0000000000000000000000000084848400848484008484
      8400848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000000000000000
      00000000000000000000F7FFFF00000000008484840000000000000000000000
      0000000000000000000000000000000000008484840000000000000000000000
      0000848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF000000000000000000FFFFFF00FFFF
      FF000000000000000000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000000
      00000000000000000000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000000
      000000000000FFFFFF00FFFFFF00FFFFFF00FFFFFF0000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000FF0000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840084848400FFFFFF000000
      0000000000008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400000000000000
      0000000000008484840084848400848484008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF0000000000FFFFFF00FFFFFF0000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00000000000000000000000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF000000000000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF000000FF000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00008484840084848400FFFFFF00000000000000000084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840000000000000000000000000084848400848484008484
      8400848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840000000000000000000000
      0000000000000000000000000000000000008484840000000000000000000000
      0000000000000000000000000000000000008484840000000000000000000000
      0000000000000000000000000000000000008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF0000000000000000000000
      000000000000FFFFFF00FFFFFF00FFFFFF000000000000000000FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000000
      000000000000FFFFFF00FFFFFF00FFFFFF00FFFFFF0000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000FF0000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840084848400FFFFFF000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000008484840084848400848484008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF0000000000000000000000000000000000FFFFFF00FFFFFF00FFFF
      FF000000000000000000FFFFFF00FFFFFF00FFFFFF000000000000000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF000000000000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF000000FF000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00008484840084848400FFFFFF00000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000008400000084
      000000FF00000084000000000000000000000000000084848400848484008484
      8400848484000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400FFFFFF000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000848484008484
      8400000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00000000000000
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF000000000000000000FFFFFF00FFFF
      FF00FFFFFF000000000000000000FFFFFF00FFFFFF00FFFFFF00FFFFFF000000
      000000000000FFFFFF00FFFFFF00FFFFFF00FFFFFF0000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000FF0000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840084848400FFFFFF000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000FF000000FF000000FF000000FF0000000000000000
      0000000000008484840084848400848484008484840000000000000000000000
      000000000000000000000000000000000000000000000000000084848400FFFF
      FF00000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400848484008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF000000000000000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF000000000000000000000000000000000000000000FFFFFF00FFFF
      FF00000000000000000000000000000000000000000000000000000000000000
      0000848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF000000FF000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00008484840084848400FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF0084848400848484008484
      8400848484000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400FFFFFF000000000000000000000000000000
      00000000000000000000000000000000000000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF0000000000000000000000
      0000000000000000000000000000000000000000000000000000848484008484
      8400848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000FF0000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400848484008484840000000000000000000000
      000000000000000000000000000000000000000000000000000084848400FFFF
      FF00000000000000000000000000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF000000000000000000000000000000000000000000000000000000
      000084848400FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000000
      0000000000000000000084848400848484008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF00000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000848484008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000848484000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400FFFFFF000000000000000000000000008484
      8400848484008484840084848400848484008484840000000000000000000000
      0000000000000000000000000000000000000000000084848400848484008484
      8400848484008484840084848400000000000000000000000000848484008484
      8400848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF00000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840000000000000000000000
      000000000000000000000000000000000000000000000000000084848400FFFF
      FF00000000000000000000000000000000000000000000000000000000000000
      0000000000008484840084848400848484008484840084848400848484008484
      8400848484000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400848484008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400FFFFFF000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000FF000000FF0000000000000000000000000000000000848484008484
      8400848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF000000FF000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000084848400FFFF
      FF00000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000FF000000FF00000000000000
      0000000000000000000084848400848484008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400FFFFFF000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000848484008484
      8400848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF0000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000084848400FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0084848400848484008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000084848400FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000008484
      8400848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00008484840000FFFF0000FFFF0000FFFF0000FFFF0000FFFF00008484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000008484840000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF00008484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000084000000840000008400000084000000840000008400000084000000
      8400000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000008484
      840000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF000084
      8400008484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000840000008400000084000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000840000008400000084000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000008484840000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF00008484000084840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000084000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF00000084000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000840000008400000084
      0000008400000084000000840000008400000084000000840000008400000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00008484840000FFFF0000FFFF0000FFFF0000FFFF0000FFFF00008484000084
      8400008484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000084000000
      84000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF00000084000000840000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000008400000084000000840000008400000084000000840000008400000084
      0000008400000084000000840000008400000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000848400008484000084
      8400008484000084840000000000008484000084840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000084000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF00000084000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000008400000084000000840000008400000084
      0000008400000084000000840000008400000084000000840000008400000084
      0000008400000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      8400000000000000000000000000000000008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000084000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF00000084000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000008400000084
      0000008400000084000000840000008400000084000000840000008400000084
      0000008400000084000000840000008400000084000000840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF00000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000848400FFFFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF00008484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000084000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      8400000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000084000000840000008400000084000000840000008400000084
      0000008400000084000000840000008400000084000000840000008400000084
      0000008400000084000000840000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000000000000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF00000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      8400FFFFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF000084
      8400008484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000084000000FF000000FF000000FF000000
      FF000000FF000000FF00FFFFFF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF00FFFFFF000000FF000000
      FF000000FF000000FF000000FF000000FF000000840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000084000000840000008400000084
      0000008400000084000000840000008400000084000000840000008400000084
      0000008400000084000000840000008400000084000000840000008400000084
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF0000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000848400FFFFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF00008484000084840000848400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000084000000
      FF000000FF000000FF000000FF000000FF000000FF00FFFFFF00FFFFFF00FFFF
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF00FFFFFF00FFFFFF00FFFFFF000000FF000000FF000000FF000000FF000000
      FF000000FF000000840000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      0000008400000084000000840000008400000084000000840000008400000084
      0000008400000084000000840000008400000084000000840000008400000084
      0000008400000084000000840000008400000084000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      8400FFFFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF000084
      8400008484000084840000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000084000000FF000000FF000000FF000000FF000000
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000000FF000000FF000000
      FF000000FF000000FF000000FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF000000FF000000FF000000FF000000FF000000FF0000008400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000008400000084000000840000008400000084
      0000008400000084000000840000008400000084000000840000008400000084
      0000008400000084000000840000008400000084000000840000008400000084
      0000008400000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000848400FFFFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF00008484000084840000848400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000084000000
      FF000000FF000000FF000000FF000000FF000000FF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF000000FF000000FF000000FF000000FF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF000000FF000000FF000000FF000000FF000000
      FF000000FF000000840000000000840084000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      0000008400000084000000840000008400000084000000840000008400000084
      0000008400000084000000840000008400000084000000840000008400000084
      0000008400000084000000840000008400000084000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000084840000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF00008484000084840000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000084000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000000
      FF000000FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF00000084000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000008400000084000000840000008400000084
      0000008400000084000000840000008400000084000000840000008400000084
      0000008400000084000000840000008400000084000000840000008400000084
      0000008400000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000848400FFFFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF000084840000848400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000000084000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF0000008400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      0000008400000084000000840000008400000084000000840000008400000084
      0000008400000084000000840000008400000084000000840000008400000084
      0000008400000084000000840000008400000084000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000848400FFFFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF000084840000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000084000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF00000084000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000008400000084000000840000008400000084
      0000008400000084000000840000008400000084000000840000008400000084
      0000008400000084000000840000008400000084000000840000008400000084
      0000008400000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000848400FFFF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF00008484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000000084000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF0000008400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      0000008400000084000000840000008400000084000000840000008400000084
      0000008400000084000000840000008400000084000000840000008400000084
      0000008400000084000000840000008400000084000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000008484000000000000000000000000008484840000000000000000000000
      000000000000000000000000000000848400FFFFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF00008484008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000084000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF00000084000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000008400000084000000840000008400000084
      0000008400000084000000840000008400000084000000840000008400000084
      0000008400000084000000840000008400000084000000840000008400000084
      0000008400000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000848400FFFFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF00000000000000000000000000000000000000
      00008484840000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000000084000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF0000008400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      0000008400000084000000840000008400000084000000840000008400000084
      0000008400000084000000840000008400000084000000840000008400000084
      0000008400000084000000840000008400000084000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000848400FFFFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF000084
      8400000000000000000000000000000000008484840000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000848400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000084000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF00000084000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000008400000084000000840000008400000084
      0000008400000084000000840000008400000084000000840000008400000084
      0000008400000084000000840000008400000084000000840000008400000084
      0000008400000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000848400FFFFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF00008484000084840000000000000000000000
      00008484840000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF000084840000848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000000084000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF000000FF000000FF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF0000008400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000008400000084000000840000008400000084000000840000008400000084
      0000008400000084000000840000008400000084000000840000008400000084
      0000008400000084000000840000008400000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000848400FFFFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF000084
      8400008484000084840000000000000000008484840000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000848400008484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000084000000FF000000FF000000FF000000FF000000
      FF000000FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000000FF000000
      FF000000FF000000FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000000
      FF000000FF000000FF000000FF000000FF000000FF0000008400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000840000008400000084
      0000008400000084000000840000008400000084000000840000008400000084
      0000008400000084000000840000008400000084000000840000008400000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000848400FFFFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF000084840000848400000000000000
      000000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF000084840000848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000084000000
      FF000000FF000000FF000000FF000000FF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF000000FF000000FF000000FF000000FF000000FF000000FF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000000FF000000FF000000FF000000
      FF000000FF000000840000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000840000008400000084000000840000008400000084
      0000008400000084000000840000008400000084000000840000008400000084
      0000008400000084000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000848400FFFFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000848400008484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000084000000FF000000FF000000FF000000FF000000
      FF000000FF00FFFFFF00FFFFFF00FFFFFF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF00FFFFFF00FFFFFF00FFFFFF000000
      FF000000FF000000FF000000FF000000FF000000FF0000008400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      0000008400000084000000840000008400000084000000840000008400000084
      0000008400000084000000840000008400000084000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000848400FFFFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF00000000000084840000848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      84000000FF000000FF000000FF000000FF000000FF000000FF00FFFFFF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF00FFFFFF000000FF000000FF000000FF000000FF000000FF000000
      FF00000084000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000084000000840000008400000084
      0000008400000084000000840000008400000084000000840000008400000084
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF0000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000848400FFFFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF00000000000000000000000000008484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000084000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF00000084000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000084000000840000008400000084000000840000008400000084
      0000008400000084000000840000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000000000000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF00000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000848400FFFF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF00000000000000
      0000000000000000000000848400008484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000084000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      8400000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000848400FFFFFF0000FFFF0000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF00FFFFFF00FFFFFF0000000000000000000000000000000000008484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000000084000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF0000008400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000FFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000000000000000000000000000000
      0000000000000084840000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000008400000084000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000840000008400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000084840000848400FFFF
      FF0000000000FFFFFF0000000000FFFFFF0000000000FFFFFF00FFFFFF000000
      0000FFFFFF0000000000000000000000000000FFFF0000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000084000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      8400000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000008484000084840000000000FFFFFF000000
      0000FFFFFF00FFFFFF0000000000FFFFFF000000000000000000008484000084
      8400000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000008400000084000000
      84000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF00000084000000840000008400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000848400008484000084840000FFFF0000848400008484000084
      8400008484000084840000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000840000008400000084000000
      8400000084000000840000008400000084000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000840000008400000084000000
      8400000084000000840000008400000084000000840000008400000084000000
      8400000084000000840000008400000084000000840000008400000084000000
      8400000084000000840000008400000084000000840000008400000084000000
      8400000084000000840084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000840000008400000084000000840000008400000084000000
      8400000084000000840000008400000084000000840000008400000084000000
      8400000084000000840000008400000084000000840000008400000084000000
      8400000084000000840000008400000084000000840000008400000084008484
      8400000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000008400848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000008400848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000840084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000084008484
      8400000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000084848400C6C6C600C6C6C600000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000C6C6C600C6C6C600C6C6C600000000000000
      00000000000000000000C6C6C600000000000000840084848400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000084848400C6C6C600C6C6
      C600000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000C6C6C600C6C6C600C6C6C60000000000000000000000000000000000C6C6
      C600000000000000840084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000084848400C6C6C600C6C6
      C600000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000C6C6C60000000000000000000000000000000000C6C6C600000000000000
      8400848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400C6C6C600C6C6C60000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000C6C6C600000000000000
      00000000000000000000C6C6C600000000000000840084848400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000848484008484840084848400848484008484
      8400848484008484840084848400848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C6000000000000008400848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000008484
      8400C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600000000000000
      8400848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000008484
      8400000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000008400848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000008400848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000848484008484
      8400848484008484840084848400848484008484840084848400848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000C6C6C600C6C6C600C6C6C600C6C6
      C600FFFFFF00C6C6C600C6C6C600848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000848484008484840084848400848484008484840084848400848484008484
      8400848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000008484840000000000C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600000000000000840084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00008484840000000000C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C60000000000000084008484
      8400000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840000000000000000000000
      0000000000000000000084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840000000000000000008484
      8400000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840084848400848484008484
      8400848484008484840084848400848484008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000008484
      840000000000C6C6C600C6C6C6000000FF0000000000C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C6000000000000000000000000000000
      0000000000000000000000000000C6C6C6000000000000008400848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840000000000C6C6C600C6C6
      C6000000FF0000000000C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C6000000000000000000000000000000000000000000000000000000
      0000C6C6C6000000000000008400848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000000000C6C6
      C600FFFFFF00C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000C6C6C600C6C6C600C6C6C600C6C6
      C600FFFFFF00C6C6C600C6C6C600848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000FFFFFF000000000000000000000000000000
      0000000000000000000000000000848484008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF000000000000000000000000000000000000000000000000000000
      0000848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000008484840000000000C6C6C600C6C6C6000000
      FF0000000000C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C60084848400848484008484840084848400848484008484840084848400C6C6
      C600000000000000840084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00008484840000000000C6C6C600C6C6C6000000FF0000000000C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C60084848400848484008484
      840084848400848484008484840084848400C6C6C60000000000000084008484
      8400000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000C6C6C600FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C60084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000C6C6C600C6C6C600C6C6C600C6C6C600FFFFFF00C6C6C600C6C6C6008484
      8400000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000000000FFFF
      FF00848484000000000000000000000000000000000000000000000000008484
      8400848484000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000FFFFFF0000000000000000000000
      0000000000000000000000000000000000008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000008484
      840000000000C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C6000000000000008400848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840000000000C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C6000000000000008400848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000848484000000000000000000000000000000000000000000000000000000
      0000848484000000000000000000848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000FFFFFF000000000000000000000000000000
      0000000000000000000000000000848484008484840000000000000000000000
      0000000000000000000000000000000000008484840000000000000000000000
      0000FFFFFF000000000000000000000000000000000000000000000000000000
      0000848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000840084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000084008484
      8400000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000C6C6C600FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C60084848400000000000000000000000000000000000000
      0000000000000000000000000000000000008484840000000000000000000000
      0000C6C6C600C6C6C600C6C6C600C6C6C600FFFFFF00C6C6C600C6C6C6008484
      8400000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000000000FFFF
      FF00000000000000000000000000000000000000000000000000000000008484
      8400848484000000000000000000000000000000000000000000000000000000
      000084848400000000000000000000000000FFFFFF0000000000000000000000
      0000000000000000000000000000000000008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000840084848400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000840084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000848484000000000000000000000000000000000000000000848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000FFFFFF008484840000000000000000000000
      0000000000000000000000000000848484008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF000000000000000000000000000000000000000000000000000000
      0000848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000084848400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      8400848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000840084848400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400000000000000000000000000000000000000
      0000000000000000000000000000000000008484840000000000000000000000
      0000000000000000000000000000000000008484840000000000000000008484
      8400000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000000000FFFF
      FF00000000000000000000000000000000000000000000000000000000008484
      8400848484000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000FFFFFF0000000000000000000000
      0000000000000000000000000000000000008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000008484840000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000084008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000084008484840000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000848484000000000000000000000000000000000000000000000000000000
      0000848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000FFFFFF008484840000000000000000000000
      0000000000000000000000000000848484008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000008484840000000000C6C6
      C600000084000000840000008400000084000000840000008400000084000000
      840000008400000084000000840000008400C6C6C60000000000000000000000
      8400848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000008484840000000000C6C6C600FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000C6C6C60000000000000000000000840084848400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840000000000000000000000
      0000000000000000000000000000000000008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000000000FFFF
      FF00848484000000000000000000000000000000000000000000000000008484
      8400848484000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000008484840000000000C6C6C6000000840000000000000000000000
      0000000084000000000000008400000000000000840000000000000084000000
      8400C6C6C6000000000000000000000084008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000848484000000
      0000C6C6C600FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000C6C6C600000000000000
      0000000084008484840000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840000000000000000000000
      0000000000000000000000000000000000008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000008484840000000000C6C6
      C600000084000000000000008400000000000000840000000000000084000000
      000000008400000000000000840000008400C6C6C60000000000000000000000
      8400848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000008484840000000000C6C6C600FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000C6C6C60000000000000000000000840084848400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000084848400FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000008484840000000000C6C6C6000000840000000000000000000000
      0000000084000000000000008400000000000000840000000000000084000000
      8400C6C6C6000000000000000000000084008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000848484000000
      0000C6C6C600FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000C6C6C600000000000000
      0000000084008484840000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000008484840000000000C6C6
      C600000084000000840000008400000084000000840000008400000084000000
      840000008400000084000000840000008400C6C6C60000000000000000000000
      8400848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000008484840000000000C6C6C600FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000C6C6C60000000000000000000000840084848400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000008484840000000000C6C6C6000000840000000000000084000000
      0000000000000000000000008400000000000000000000000000000084000000
      8400C6C6C6000000000000000000000084008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000848484000000
      0000C6C6C600FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000C6C6C600000000000000
      0000000084008484840000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840000000000000000000000
      0000000000000000000000000000000000008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000008484840000000000C6C6
      C600000084000000000000008400000000000000840000000000000084000000
      000000008400000000000000840000008400C6C6C60000000000000000000000
      8400848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000008484840000000000C6C6C600FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000C6C6C60000000000000000000000840084848400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000008484840000000000C6C6C6000000840000000000000084000000
      0000000000000000000000008400000000000000000000000000000084000000
      8400C6C6C6000000000000000000000084008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000848484000000
      0000C6C6C600FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000C6C6C600000000000000
      0000000084008484840000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      FF0000000000000000000000FF00000000000000FF000000FF000000FF000000
      0000000000000000FF000000FF000000000000000000000000000000FF000000
      FF0000000000000000000000FF0000000000000000000000FF00000000000000
      FF000000FF000000FF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000008484840000000000C6C6
      C600000084000000840000008400000084000000840000008400000084000000
      840000008400000084000000840000008400C6C6C60000000000000000000000
      8400848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000008484840000000000C6C6C600FF000000FF000000FF00
      0000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF00
      0000FF000000C6C6C60000000000000000000000840084848400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000FF0000000000000000000000FF000000
      00000000FF000000000000000000000000000000FF0000000000000000000000
      FF00000000000000FF0000000000000000000000FF00000000000000FF000000
      0000000000000000FF00000000000000FF0000000000000000000000FF000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000008484840000000000C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C6000000000000000000000084008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000848484000000
      0000C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600000000000000
      0000000084008484840000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      FF000000FF000000FF0000000000000000000000FF000000FF00000000000000
      00000000FF00000000000000000000000000000000000000FF00000000000000
      00000000FF00000000000000FF000000FF000000FF0000000000000000000000
      FF0000000000000000000000FF00000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000084848400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      8400000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000FF0000000000000000000000FF000000
      00000000FF000000000000000000000000000000FF0000000000000000000000
      FF00000000000000FF0000000000000000000000FF00000000000000FF000000
      0000000000000000FF00000000000000FF0000000000000000000000FF000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      FF000000FF000000FF0000000000000000000000FF000000FF000000FF000000
      0000000000000000FF000000FF000000000000000000000000000000FF000000
      FF0000000000000000000000FF000000FF000000FF0000000000000000000000
      FF000000FF000000FF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000424D3E000000000000003E00000028000000B4000000AF00000001000100
      00000000681000000000000000000000000000000000000000000000FFFFFF00
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000FFFFFFFFFFF800000000003FFFFFFFFFFE00000000000000
      FFFFFFFFFFF800000000003FFFFFFFFFFE00000000000000FFFFFFFFFFF80000
      0000003FFFFFFFFFFE00000000000000FFFFFFFFFFF800000000003FFFFFFFFF
      FE00000000000000FFFFFFFFFFF800000000003FFFFFFFFFFE00000000000000
      FFFFFFFFFFF800000000003FFFFFFFFFFE00000000000000FFFFFFFFFFF80000
      0000003FFFFFFFFFFE00000000000000FFFFFFFFFFF800000000003FFFFFFFFF
      FE00000000000000FFFFFFFFFFF800000000003FFFFFFFFFFE00000000000000
      FFFFFFFFFFF800000000003FFFFFFFFFFE00000000000000FE0000003FF80000
      0000003FFFFFFFFFFE00000000000000FC0000001FF800000000003FFFFFFFFF
      FE00000000000000FC00000007F800000000003FFFFFFFFFFE00000000000000
      FCFFFFFC03F800000000003FFFFFFFFFFE00000000000000FC00000003F80000
      0000003F00CF8F87FE00000000000000FCFFFFFC03F800000000003F00CF8E03
      FE00000000000000FC00000003F800000000003F3FCF0C7BFE00000000000000
      FC00000003F800000000003F3FCE4CFFFE00000000000000FC00000003F80000
      0000003F00CCCCFFFE00000000000000FC00000003F800000000003F3FC9CCFF
      FE00000000000000FC00000003F800000000003F3FC3CC7BFE00000000000000
      FCFFFFFF03F800000000003F3FC3CE31FE00000000000000FE00000003F80000
      0000003F00C7CE03FE00000000000000FF00000003F800000000003FFFFFFFFF
      FE00000000000000FFC0000007F800000000003FFFFFFFFFFE00000000000000
      FFFFFFFFFFF800000000003FFFFFFFFFFE00000000000000FFE7BBFCFFF80000
      0000003FFFFFFFFFFE00000000000000FE1E7C7F3FF800000000003FFFFFFFFF
      FE00000000000000FDFDCF83C3F800000000003FFFFFFFFFFE00000000000000
      FDFDCF83C3F800000000003FFFFFFFFFFE00000000000000FDFDF7FDFDF80000
      0000003FFFFFFFFFFE00000000000000F3FDFBFDFFF800000000003FFFFFFFFF
      FE00000000000000EFE3FBFDFFF800000000003FFFFFFFFFFE00000000000000
      9FDFFBFEFFF800000000003FFFFFFFFFFE00000000000000FFFFFFFFFFF80000
      0000003FFFFFFFFFFE00000000000000FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF
      FFFFFFFFFFFFF000FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF000
      FFFFFE0007FFFFFF07FFFFFFFFFFFFFFFFFFFFFFFFFFF000FFF81C0003FFFFF0
      007FFFFFFFFFFFFFFFFFFFFFFFFFF000FFE0FC0003FFFFE0003FFFFFFFFFFFFF
      FFFFFFFFFFFFF000FFC3FC0003FFFF80000FFFFFFFFFFFFFFFFFFFFFFFFFF000
      FFC7FC0003FFFF000007FFFFFFFFFFFFFFFFFFFFFFFFF000FF87FC0003FFFE00
      0003FFFFFFE007FFFFFFFFFFFFFFF000FF87FC0003FFFC000001FFFFFFC003FF
      FFFFFFFFFFFFF000FF83BC0003FFFC000001FFFFFF8001FFFFFFFFFFFFFFF000
      FF813C0003FFF8000000FFFFFF0000FFFFC000000003F000FF803C0003FFF800
      0000FFFFFE00007FFF0000000000F000FFC03C0003FFF1867C187FFFFC00003F
      FF1FFFFFFFF87000FFE03C0003FFF186C6187FFFF800001FFF18000000381000
      FFF03C0003FFF0FCC6187FFFF800001FFF18000000381000FFE03C0003FFE0CC
      06183FFFF800001FFF18000000381000FFC03C0007FFE0CC1C183FFFF800001F
      FF18000000381000FFFFFFFFFFFFE04870183FFFF800001FFF18000000381000
      FE0000001FFFE078C0183FFFF800001FFF1FFFFFFFF81000FC0000000FFFE078
      C6183FFFF800001FFF1FFFFFFC381000FCFFFFFFC7FFE030C6183FFFF800001F
      FF1FFFFFFC381000FCFFFFFFC3FFF0307CFF7FFFF800001FFF00000000001000
      FCFF807FC3FFF00000007FFFF800001FFFDFFFFFFFFC1000FCE00001C3FFF000
      00007FFFFC00003FFFE7FFFFFFFF1000FCE00001C3FFF8000000FFFFFE00007F
      FFF8000000007000FCFF807FC3FFF8000000FFFFFF0000FFFFFFFFFFFFFFF000
      FCFFFFF3C3FFFC000001FFFFFF8001FFFFFFFFFFFFFFF000FCFFFFF3C3FFFC00
      0001FFFFFFC003FFFFFFFFFFFFFFF000FCFFFFFFC3FFFE000003FFFFFFE007FF
      FFFFFFFFFFFFF000FC00000003FFFF000007FFFFFFFFFFFFFFFFFFFFFFFFF000
      FEFFFFFFE3FFFF80000FFFFFFFFFFFFFFFFFFFFFFFFFF000FF7FFFFFF3FFFFE0
      003FFFFFFFFFFFFFFFFFFFFFFFFFF000FF80000007FFFFF0007FFFFFFFFFFFFF
      FFFFFFFFFFFFF000FFFFFFFFFFFFFFFF07FFFFFFFFFFFFFFFFFFFFFFFFFFF000
      FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF000FFFFFFFFFFFFFFFF
      FFFFFFFFFFFFFFFFFFFFFFFFFFFFF000FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF
      FFFFFFFFFFFFF000FFFFFFFFFFFFFFFFFFFFFFFFFFF81FFFFFFFFFFFFFFFF000
      FFFFFFFFFFFFFFFFFFFFFFFFFFF00FFFFFFFFF00FFFFF000FFFFFFFFFFFFFFFF
      FFFFFFFFFFE007FFFFFFF8001FFFF000FFFFFFFFFFFFFFFFFFFFFFFFFFE003FF
      FFFFF0000FFFF000FFFFFFFFFFFFFFFFFFFFFFFFFFE003FFFFFFE00003FFF000
      FFFF801FFFFFFFFC00FFFFFFFFF003FFFFFFC00001FFF000FFFF000FFFFFFFF8
      007FFFFFFFF823FFFFFF800000FFF000FFFE0007FFFFFFF0003FFFFFFFE007FF
      FFFF0000007FF000FFFC0003FFFFFFE0001FFFFFFFE007FFFFFF0000007FF000
      FFF80001FFFFFFC0000FFFFFFFE003FFFFFE0000003FF000FFF00000FFFFFF80
      0007FFFFFFE001FFFFFC0000001FF000FFE000007FFFFF000003FFFFFFE001FF
      FFFC0000001FF000FFE000007FFFFF000003FFFFFFE001FFFFFC0000000FF000
      FFE000007FFFFF000003FFFFFFF001FFFFF80000000FF000FFE000007FFFFF00
      0003FFFFFFF001FFFFF80000000FF000FFE000007FFFFF000003FFFFFFF801FF
      FFF80000000FF000FFE000007FFFFF000003FFFFFFFC00FFFFF80000000FF000
      FFE000007FFFFF000003FFFFF01E007FFFF80000000FF000FFE000007FFFFF00
      0003FFFFF00F003FFFF80000000FF000FFE000007FFFFF000003FFFFF007001F
      FFF80000000FF000FFE000007FFFFF000003FFFFF003000FFFF80000000FF000
      FFF00000FFFFFF800007FFFFF001000FFFFC0000001FF000FFF80001FFFFFFC0
      000FFFFFF000000FFFFC0000001FF000FFFC0003FFFFFFE0001FFFFFF000000F
      FFFC0000001FF000FFFE0007FFFFFFF0003FFFFFF800008FFFFE0000003FF000
      FFFF000FFFFFFFF8007FFFFFF80001CFFFFF0000007FF000FFFF801FFFFFFFFC
      00FFFFFFFC0003CFFFFF000000FFF000FFFFFFFFFFFFFFFFFFFFFFFFFE0003DF
      FFFF800001FFF000FFFFFFFFFFFFFFFFFFFFFFFFFF000FBFFFFFC00003FFF000
      FFFFFFFFFFFFFFFFFFFFFFFFFF8A977FFFFFF0000FFFF000FFFFFFFFFFFFFFFF
      FFFFFFFFFFE52CFFFFFFF8001FFFF000FFFFFFFFFFFFFFFFFFFFFFFFFFF803FF
      FFFFFF00FFFFF000FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF000
      FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF000FFFFFFFFFFFFFFFF
      FFFFFFFFFFFFFFFFFFFFFFFFFFFFF000FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF
      FFFFFFFFFFFFF000FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFE03FFFFFF000
      FFFFFFFFFFFFF80000001FFFC0000000FFFFDFDFFFFFF000FFFFFFFFFFFFF000
      00001FFF80000000FFFFB06FF01FF000FFF01FFFFFFFE00000001FFF00000000
      FFFFB06FF01FF000FFEFEFFFFFFFEFFFFFFF1FFF7FFFFFF8FFFFAFAFF7DFF000
      FFD837F80FFFF0AAAA143FFF855550A1FFFFAFAFC007F000FFD837F80FFFF855
      55287FFFC2AAA943FFFFAFAFC007F000FFD7D7F80FFFFC000000FFFFE0000007
      FFFFAFAFC007F000FFD7D7E003FFFEFFFFF1FFFFF7FFFF8FFFF800AFD007F000
      FFD7D7E003FFFE800001FFFFF400000FFFFA00B1D007F000FFD7D7E003FFFE88
      0FE1FFFFF4407F0FFFFA00DED007F000FCFE57E7F3FFFE880001FFFFF440000F
      FFFA00DED007F000FC0058E003FFFE800001FFFFF400000FFFFA00E35007F000
      FCFE6F67F3FFFEFFFFF1FFFFF7FFFF8FFFFA00FD5007F000FCFE6F67F3FFFF00
      0003FFFFF800001FFFFA00FD5FE7F000FC0071A003FFFFBFFFC7FFFFFDFFFE3F
      FFFA00FD4007F000FCFE7EA7F3FFFFBFFFC7FFFFFDFFFE3FFFF800FD7D7FF000
      FC007EA003FFFFA00047FFFFFD00023FFFFEFBFD7D7FF000FC007EA003FFFFA7
      5447FFFFFD00023FFFFE03FD837FF000FC007EBEBFFFFFA55447FFFFFD00023F
      FFFE03FD837FF000FF01FEBEBFFFFFA75447FFFFFD00023FFFFFFFFEFEFFF000
      FF01FEC1BFFFFFA00047FFFFFD00023FFFFFFFFF01FFF000FF01FEC1BFFFFFA5
      DC47FFFFFD00023FFFFFFFFFFFFFF000FFFFFF7F7FFFFFA55447FFFFFD00023F
      FFFFFFFFFFFFF000FFFFFF80FFFFFFA5DC47FFFFFD00023FFFFED19CDA3FF000
      FFFFFFFFFFFFFFA00047FFFFFD00023FFFFED76B5ADFF000FFFFFFFFFFFFFFA0
      0047FFFFFD00023FFFFE337B46DFF000FFFFFFFFFFFFFFBFFFCFFFFFFDFFFE7F
      FFFED76B5ADFF000FFFFFFFFFFFFFFC0001FFFFFFE0000FFFFFE319CC63FF000
      FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF000FFFFFFFFFFFFFFFF
      FFFFFFFFFFFFFFFFFFFFFFFFFFFFF000FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF
      FFFFFFFFFFFFF00000000000000000000000000000000000000000000000}
  end
  object Timer3: TTimer
    Enabled = False
    Interval = 1800
    OnTimer = Timer3Timer
    Left = 596
    Top = 50
  end
  object SaveDialog: TSaveDialog
    DefaultExt = 'txt'
    FileName = 'PrgStationCom'
    Filter = '|txt'
    InitialDir = 'c:'
    Left = 716
    Top = 52
  end
  object CMDEnableTimer: TTimer
    Enabled = False
    Interval = 3000
    OnTimer = CMDEnableTimerTimer
    Left = 746
    Top = 52
  end
  object BootloadTimer: TTimer
    Enabled = False
    Interval = 5000
    OnTimer = BootloadTimerTimer
    Left = 776
    Top = 52
  end
  object PollTimer: TTimer
    Enabled = False
    Interval = 250
    OnTimer = PollTimerTimer
    Left = 502
    Top = 52
  end
  object ClientSocket: TClientSocket
    Active = False
    ClientType = ctNonBlocking
    Port = 10001
    OnConnect = ClientSocketConnect
    OnRead = ClientSocketRead
    OnError = ClientSocketError
    Left = 306
    Top = 48
  end
  object DownloadRdrOpenDialog: TOpenDialog
    Filter = '"awi501c.Hex|*.HEX"'
    InitialDir = 'C:'
    Left = 278
    Top = 48
  end
  object TimeTimer1: TTimer
    OnTimer = TimeTimer1Timer
    Left = 562
    Top = 50
  end
  object GeneralTimer: TTimer
    Enabled = False
    OnTimer = GeneralTimerTimer
    Left = 818
    Top = 52
  end
  object WriteTimer: TTimer
    Enabled = False
    Interval = 850
    OnTimer = WriteTimerTimer
    Left = 2
    Top = 68
  end
  object ReadTimer: TTimer
    Enabled = False
    Interval = 850
    OnTimer = ReadTimerTimer
    Left = 2
    Top = 102
  end
  object RetryTimer: TTimer
    Enabled = False
    Interval = 850
    OnTimer = RetryTimerTimer
    Left = 2
    Top = 132
  end
  object ReadLargDataTimer: TTimer
    Enabled = False
    Interval = 400
    OnTimer = ReadLargDataTimerTimer
    Left = 2
    Top = 166
  end
end
