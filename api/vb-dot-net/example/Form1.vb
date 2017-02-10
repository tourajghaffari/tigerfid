Imports System
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic
Imports AW_API_NET

Public Class Form1
    Inherits System.Windows.Forms.Form

    Dim Hconn As IntPtr
    Dim readerIP(20) As Byte
    Dim readerPort As UInt16
    Dim commPort As UInt32
    Dim commBaud As UInt32
    Dim myPKTID As Integer
    Dim registered As Boolean
    Dim ActiveWaveAPI As AW_API_NET.APINetClass = new AW_API_NET.APINetClass
    Dim ReaderEventHandler As AW_API_NET.fReaderEvent
    Dim TagEventHandler As AW_API_NET.fTagEvent

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents MsgArea As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents TagBox As System.Windows.Forms.TextBox
    Friend WithEvents TagButton As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ReaderIDTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ACCRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents ASTRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents INVRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents QueryButton As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents SocketFlg As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents rfScanButton As System.Windows.Forms.Button
    Friend WithEvents rfOpenButton As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents RdrCmdTypeComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents TagCmdTypeComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents rfCloseButton As System.Windows.Forms.Button
    Friend WithEvents rfResetRdrButton As System.Windows.Forms.Button
    Friend WithEvents rfQueryRdrButton As System.Windows.Forms.Button
    Friend WithEvents BroadcastFGenCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents rfResetSmartFGenButton As System.Windows.Forms.Button
    Friend WithEvents rfQuerySTDFGenButton As System.Windows.Forms.Button
    Friend WithEvents FGenIDTextBox As System.Windows.Forms.TextBox
    Friend WithEvents IPListBox As System.Windows.Forms.ListBox
    Friend WithEvents LengthTextBox As System.Windows.Forms.TextBox
    Friend WithEvents AddressTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ReadTagButton As System.Windows.Forms.Button
    Friend WithEvents EnableTagButton As System.Windows.Forms.Button
    Friend WithEvents ClearButton As System.Windows.Forms.Button
    Friend WithEvents LongIntervalCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents STDFGenCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents rfSetReaderFSButton As System.Windows.Forms.Button
    Friend WithEvents rfGetReaderFSButton As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents FSTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents rfCloseSocketButton As System.Windows.Forms.Button
    Friend WithEvents rfScanIPButton As System.Windows.Forms.Button
    Friend WithEvents SpecificIPRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents AllIPRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents rfOpenSocketButton As System.Windows.Forms.Button
    Friend WithEvents IPTextBox As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.rfOpenButton = New System.Windows.Forms.Button
        Me.rfCloseButton = New System.Windows.Forms.Button
        Me.MsgArea = New System.Windows.Forms.TextBox
        Me.rfResetRdrButton = New System.Windows.Forms.Button
        Me.TagBox = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.TagButton = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.ReaderIDTextBox = New System.Windows.Forms.TextBox
        Me.ACCRadioButton = New System.Windows.Forms.RadioButton
        Me.ASTRadioButton = New System.Windows.Forms.RadioButton
        Me.INVRadioButton = New System.Windows.Forms.RadioButton
        Me.QueryButton = New System.Windows.Forms.Button
        Me.rfQueryRdrButton = New System.Windows.Forms.Button
        Me.TagCmdTypeComboBox = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.rfOpenSocketButton = New System.Windows.Forms.Button
        Me.IPTextBox = New System.Windows.Forms.TextBox
        Me.AllIPRadioButton = New System.Windows.Forms.RadioButton
        Me.SpecificIPRadioButton = New System.Windows.Forms.RadioButton
        Me.rfCloseSocketButton = New System.Windows.Forms.Button
        Me.rfScanIPButton = New System.Windows.Forms.Button
        Me.rfScanButton = New System.Windows.Forms.Button
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.FSTextBox = New System.Windows.Forms.TextBox
        Me.rfGetReaderFSButton = New System.Windows.Forms.Button
        Me.rfSetReaderFSButton = New System.Windows.Forms.Button
        Me.SocketFlg = New System.Windows.Forms.CheckBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.LengthTextBox = New System.Windows.Forms.TextBox
        Me.AddressTextBox = New System.Windows.Forms.TextBox
        Me.ReadTagButton = New System.Windows.Forms.Button
        Me.EnableTagButton = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.STDFGenCheckBox = New System.Windows.Forms.CheckBox
        Me.rfQuerySTDFGenButton = New System.Windows.Forms.Button
        Me.rfResetSmartFGenButton = New System.Windows.Forms.Button
        Me.BroadcastFGenCheckBox = New System.Windows.Forms.CheckBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.RdrCmdTypeComboBox = New System.Windows.Forms.ComboBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.FGenIDTextBox = New System.Windows.Forms.TextBox
        Me.IPListBox = New System.Windows.Forms.ListBox
        Me.ClearButton = New System.Windows.Forms.Button
        Me.LongIntervalCheckBox = New System.Windows.Forms.CheckBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'rfOpenButton
        '
        Me.rfOpenButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rfOpenButton.ForeColor = System.Drawing.Color.Blue
        Me.rfOpenButton.Location = New System.Drawing.Point(16, 26)
        Me.rfOpenButton.Name = "rfOpenButton"
        Me.rfOpenButton.Size = New System.Drawing.Size(120, 26)
        Me.rfOpenButton.TabIndex = 0
        Me.rfOpenButton.Text = "rfOpen"
        '
        'rfCloseButton
        '
        Me.rfCloseButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rfCloseButton.ForeColor = System.Drawing.Color.Blue
        Me.rfCloseButton.Location = New System.Drawing.Point(16, 58)
        Me.rfCloseButton.Name = "rfCloseButton"
        Me.rfCloseButton.Size = New System.Drawing.Size(120, 26)
        Me.rfCloseButton.TabIndex = 1
        Me.rfCloseButton.Text = "rfClose"
        '
        'MsgArea
        '
        Me.MsgArea.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MsgArea.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MsgArea.Location = New System.Drawing.Point(8, 482)
        Me.MsgArea.Multiline = True
        Me.MsgArea.Name = "MsgArea"
        Me.MsgArea.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.MsgArea.Size = New System.Drawing.Size(438, 202)
        Me.MsgArea.TabIndex = 3
        Me.MsgArea.Text = ""
        '
        'rfResetRdrButton
        '
        Me.rfResetRdrButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rfResetRdrButton.ForeColor = System.Drawing.Color.Blue
        Me.rfResetRdrButton.Location = New System.Drawing.Point(12, 38)
        Me.rfResetRdrButton.Name = "rfResetRdrButton"
        Me.rfResetRdrButton.Size = New System.Drawing.Size(96, 26)
        Me.rfResetRdrButton.TabIndex = 5
        Me.rfResetRdrButton.Text = "rfResetReader"
        '
        'TagBox
        '
        Me.TagBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TagBox.Location = New System.Drawing.Point(252, 16)
        Me.TagBox.Name = "TagBox"
        Me.TagBox.Size = New System.Drawing.Size(90, 21)
        Me.TagBox.TabIndex = 6
        Me.TagBox.Text = ""
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(202, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 18)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Tag ID: "
        '
        'TagButton
        '
        Me.TagButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TagButton.ForeColor = System.Drawing.Color.Blue
        Me.TagButton.Location = New System.Drawing.Point(50, 28)
        Me.TagButton.Name = "TagButton"
        Me.TagButton.Size = New System.Drawing.Size(144, 23)
        Me.TagButton.TabIndex = 8
        Me.TagButton.Text = "rfCallTag"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(24, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 18)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Reader ID:"
        '
        'ReaderIDTextBox
        '
        Me.ReaderIDTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ReaderIDTextBox.Location = New System.Drawing.Point(90, 14)
        Me.ReaderIDTextBox.Name = "ReaderIDTextBox"
        Me.ReaderIDTextBox.Size = New System.Drawing.Size(54, 21)
        Me.ReaderIDTextBox.TabIndex = 10
        Me.ReaderIDTextBox.Text = ""
        '
        'ACCRadioButton
        '
        Me.ACCRadioButton.Checked = True
        Me.ACCRadioButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ACCRadioButton.Location = New System.Drawing.Point(364, 18)
        Me.ACCRadioButton.Name = "ACCRadioButton"
        Me.ACCRadioButton.Size = New System.Drawing.Size(64, 24)
        Me.ACCRadioButton.TabIndex = 11
        Me.ACCRadioButton.TabStop = True
        Me.ACCRadioButton.Text = "Access"
        '
        'ASTRadioButton
        '
        Me.ASTRadioButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ASTRadioButton.Location = New System.Drawing.Point(364, 44)
        Me.ASTRadioButton.Name = "ASTRadioButton"
        Me.ASTRadioButton.Size = New System.Drawing.Size(64, 24)
        Me.ASTRadioButton.TabIndex = 12
        Me.ASTRadioButton.Text = "Asset"
        '
        'INVRadioButton
        '
        Me.INVRadioButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INVRadioButton.Location = New System.Drawing.Point(364, 70)
        Me.INVRadioButton.Name = "INVRadioButton"
        Me.INVRadioButton.Size = New System.Drawing.Size(76, 24)
        Me.INVRadioButton.TabIndex = 13
        Me.INVRadioButton.Text = "Inventory"
        '
        'QueryButton
        '
        Me.QueryButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.QueryButton.ForeColor = System.Drawing.Color.Blue
        Me.QueryButton.Location = New System.Drawing.Point(50, 56)
        Me.QueryButton.Name = "QueryButton"
        Me.QueryButton.Size = New System.Drawing.Size(144, 23)
        Me.QueryButton.TabIndex = 14
        Me.QueryButton.Text = "rfQueryTag"
        '
        'rfQueryRdrButton
        '
        Me.rfQueryRdrButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rfQueryRdrButton.ForeColor = System.Drawing.Color.Blue
        Me.rfQueryRdrButton.Location = New System.Drawing.Point(12, 68)
        Me.rfQueryRdrButton.Name = "rfQueryRdrButton"
        Me.rfQueryRdrButton.Size = New System.Drawing.Size(96, 26)
        Me.rfQueryRdrButton.TabIndex = 16
        Me.rfQueryRdrButton.Text = "rfQuryReader"
        '
        'TagCmdTypeComboBox
        '
        Me.TagCmdTypeComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TagCmdTypeComboBox.Items.AddRange(New Object() {"RF_SELECT_TAG_ID", "RF_SELECT_FIELD", "RF_SELECT_TAG_TYPE", "RF_SELECT_TAG_RANGE"})
        Me.TagCmdTypeComboBox.Location = New System.Drawing.Point(200, 68)
        Me.TagCmdTypeComboBox.Name = "TagCmdTypeComboBox"
        Me.TagCmdTypeComboBox.Size = New System.Drawing.Size(160, 23)
        Me.TagCmdTypeComboBox.TabIndex = 17
        Me.TagCmdTypeComboBox.Text = "RF_SELECT_TAG_ID"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(24, 46)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(120, 16)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "Reader Cmd Type : "
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GroupBox6)
        Me.GroupBox1.Controls.Add(Me.GroupBox5)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(8, 104)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(176, 356)
        Me.GroupBox1.TabIndex = 19
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Communication"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.rfOpenButton)
        Me.GroupBox6.Controls.Add(Me.rfCloseButton)
        Me.GroupBox6.Location = New System.Drawing.Point(16, 248)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(148, 98)
        Me.GroupBox6.TabIndex = 22
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "RS-232"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.rfOpenSocketButton)
        Me.GroupBox5.Controls.Add(Me.IPTextBox)
        Me.GroupBox5.Controls.Add(Me.AllIPRadioButton)
        Me.GroupBox5.Controls.Add(Me.SpecificIPRadioButton)
        Me.GroupBox5.Controls.Add(Me.rfCloseSocketButton)
        Me.GroupBox5.Controls.Add(Me.rfScanIPButton)
        Me.GroupBox5.Controls.Add(Me.rfScanButton)
        Me.GroupBox5.Location = New System.Drawing.Point(16, 28)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(150, 208)
        Me.GroupBox5.TabIndex = 21
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Network"
        '
        'rfOpenSocketButton
        '
        Me.rfOpenSocketButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rfOpenSocketButton.ForeColor = System.Drawing.Color.Blue
        Me.rfOpenSocketButton.Location = New System.Drawing.Point(18, 146)
        Me.rfOpenSocketButton.Name = "rfOpenSocketButton"
        Me.rfOpenSocketButton.Size = New System.Drawing.Size(120, 26)
        Me.rfOpenSocketButton.TabIndex = 24
        Me.rfOpenSocketButton.Text = "rfOpenSocket"
        '
        'IPTextBox
        '
        Me.IPTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IPTextBox.Location = New System.Drawing.Point(20, 110)
        Me.IPTextBox.Name = "IPTextBox"
        Me.IPTextBox.ReadOnly = True
        Me.IPTextBox.Size = New System.Drawing.Size(118, 21)
        Me.IPTextBox.TabIndex = 23
        Me.IPTextBox.Text = ""
        '
        'AllIPRadioButton
        '
        Me.AllIPRadioButton.Checked = True
        Me.AllIPRadioButton.Location = New System.Drawing.Point(96, 22)
        Me.AllIPRadioButton.Name = "AllIPRadioButton"
        Me.AllIPRadioButton.Size = New System.Drawing.Size(52, 24)
        Me.AllIPRadioButton.TabIndex = 22
        Me.AllIPRadioButton.TabStop = True
        Me.AllIPRadioButton.Text = "All IP"
        '
        'SpecificIPRadioButton
        '
        Me.SpecificIPRadioButton.Location = New System.Drawing.Point(8, 22)
        Me.SpecificIPRadioButton.Name = "SpecificIPRadioButton"
        Me.SpecificIPRadioButton.Size = New System.Drawing.Size(84, 24)
        Me.SpecificIPRadioButton.TabIndex = 21
        Me.SpecificIPRadioButton.Text = "Specific IP"
        '
        'rfCloseSocketButton
        '
        Me.rfCloseSocketButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rfCloseSocketButton.ForeColor = System.Drawing.Color.Blue
        Me.rfCloseSocketButton.Location = New System.Drawing.Point(18, 174)
        Me.rfCloseSocketButton.Name = "rfCloseSocketButton"
        Me.rfCloseSocketButton.Size = New System.Drawing.Size(120, 26)
        Me.rfCloseSocketButton.TabIndex = 20
        Me.rfCloseSocketButton.Text = "rfCloseSocket"
        '
        'rfScanIPButton
        '
        Me.rfScanIPButton.Enabled = False
        Me.rfScanIPButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rfScanIPButton.ForeColor = System.Drawing.Color.Blue
        Me.rfScanIPButton.Location = New System.Drawing.Point(20, 82)
        Me.rfScanIPButton.Name = "rfScanIPButton"
        Me.rfScanIPButton.Size = New System.Drawing.Size(120, 26)
        Me.rfScanIPButton.TabIndex = 19
        Me.rfScanIPButton.Text = "rfScanIP"
        '
        'rfScanButton
        '
        Me.rfScanButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rfScanButton.ForeColor = System.Drawing.Color.Blue
        Me.rfScanButton.Location = New System.Drawing.Point(20, 52)
        Me.rfScanButton.Name = "rfScanButton"
        Me.rfScanButton.Size = New System.Drawing.Size(120, 26)
        Me.rfScanButton.TabIndex = 18
        Me.rfScanButton.Text = "rfScanNetwork"
        '
        'Label12
        '
        Me.Label12.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(172, 76)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(76, 12)
        Me.Label12.TabIndex = 23
        Me.Label12.Text = "FS Limit (0 - 20)"
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(136, 54)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(32, 16)
        Me.Label10.TabIndex = 22
        Me.Label10.Text = "FS : "
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FSTextBox
        '
        Me.FSTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FSTextBox.Location = New System.Drawing.Point(172, 52)
        Me.FSTextBox.Name = "FSTextBox"
        Me.FSTextBox.Size = New System.Drawing.Size(68, 21)
        Me.FSTextBox.TabIndex = 21
        Me.FSTextBox.Text = ""
        '
        'rfGetReaderFSButton
        '
        Me.rfGetReaderFSButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rfGetReaderFSButton.ForeColor = System.Drawing.Color.Blue
        Me.rfGetReaderFSButton.Location = New System.Drawing.Point(136, 92)
        Me.rfGetReaderFSButton.Name = "rfGetReaderFSButton"
        Me.rfGetReaderFSButton.Size = New System.Drawing.Size(106, 23)
        Me.rfGetReaderFSButton.TabIndex = 20
        Me.rfGetReaderFSButton.Text = "rfGetReaderFS"
        '
        'rfSetReaderFSButton
        '
        Me.rfSetReaderFSButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rfSetReaderFSButton.ForeColor = System.Drawing.Color.Blue
        Me.rfSetReaderFSButton.Location = New System.Drawing.Point(142, 26)
        Me.rfSetReaderFSButton.Name = "rfSetReaderFSButton"
        Me.rfSetReaderFSButton.Size = New System.Drawing.Size(98, 23)
        Me.rfSetReaderFSButton.TabIndex = 19
        Me.rfSetReaderFSButton.Text = "rfSetReaderFS"
        '
        'SocketFlg
        '
        Me.SocketFlg.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SocketFlg.Location = New System.Drawing.Point(-2, 58)
        Me.SocketFlg.Name = "SocketFlg"
        Me.SocketFlg.Size = New System.Drawing.Size(16, 14)
        Me.SocketFlg.TabIndex = 17
        Me.SocketFlg.Text = "Open Using Socket"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.LengthTextBox)
        Me.GroupBox2.Controls.Add(Me.AddressTextBox)
        Me.GroupBox2.Controls.Add(Me.ReadTagButton)
        Me.GroupBox2.Controls.Add(Me.EnableTagButton)
        Me.GroupBox2.Controls.Add(Me.TagButton)
        Me.GroupBox2.Controls.Add(Me.QueryButton)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(192, 104)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(260, 212)
        Me.GroupBox2.TabIndex = 20
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Tag"
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(162, 152)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(38, 16)
        Me.Label9.TabIndex = 21
        Me.Label9.Text = "(Hex)"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(58, 178)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(52, 16)
        Me.Label8.TabIndex = 20
        Me.Label8.Text = "Length : "
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(50, 152)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(60, 16)
        Me.Label7.TabIndex = 19
        Me.Label7.Text = "Address : "
        '
        'LengthTextBox
        '
        Me.LengthTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LengthTextBox.Location = New System.Drawing.Point(110, 174)
        Me.LengthTextBox.Name = "LengthTextBox"
        Me.LengthTextBox.Size = New System.Drawing.Size(50, 21)
        Me.LengthTextBox.TabIndex = 18
        Me.LengthTextBox.Text = ""
        '
        'AddressTextBox
        '
        Me.AddressTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AddressTextBox.Location = New System.Drawing.Point(110, 150)
        Me.AddressTextBox.Name = "AddressTextBox"
        Me.AddressTextBox.Size = New System.Drawing.Size(50, 21)
        Me.AddressTextBox.TabIndex = 17
        Me.AddressTextBox.Text = "E0"
        '
        'ReadTagButton
        '
        Me.ReadTagButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ReadTagButton.ForeColor = System.Drawing.Color.Blue
        Me.ReadTagButton.Location = New System.Drawing.Point(50, 124)
        Me.ReadTagButton.Name = "ReadTagButton"
        Me.ReadTagButton.Size = New System.Drawing.Size(144, 23)
        Me.ReadTagButton.TabIndex = 16
        Me.ReadTagButton.Text = "rfReadTag"
        '
        'EnableTagButton
        '
        Me.EnableTagButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EnableTagButton.ForeColor = System.Drawing.Color.Blue
        Me.EnableTagButton.Location = New System.Drawing.Point(50, 84)
        Me.EnableTagButton.Name = "EnableTagButton"
        Me.EnableTagButton.Size = New System.Drawing.Size(144, 23)
        Me.EnableTagButton.TabIndex = 15
        Me.EnableTagButton.Text = "rfEnableTag"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.STDFGenCheckBox)
        Me.GroupBox3.Controls.Add(Me.rfQuerySTDFGenButton)
        Me.GroupBox3.Controls.Add(Me.rfResetSmartFGenButton)
        Me.GroupBox3.Controls.Add(Me.BroadcastFGenCheckBox)
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(466, 104)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(200, 356)
        Me.GroupBox3.TabIndex = 21
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Field Generator"
        '
        'STDFGenCheckBox
        '
        Me.STDFGenCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.STDFGenCheckBox.Location = New System.Drawing.Point(16, 54)
        Me.STDFGenCheckBox.Name = "STDFGenCheckBox"
        Me.STDFGenCheckBox.Size = New System.Drawing.Size(176, 24)
        Me.STDFGenCheckBox.TabIndex = 26
        Me.STDFGenCheckBox.Text = "STD Field Gen (9600 baud)"
        '
        'rfQuerySTDFGenButton
        '
        Me.rfQuerySTDFGenButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rfQuerySTDFGenButton.ForeColor = System.Drawing.Color.Blue
        Me.rfQuerySTDFGenButton.Location = New System.Drawing.Point(32, 30)
        Me.rfQuerySTDFGenButton.Name = "rfQuerySTDFGenButton"
        Me.rfQuerySTDFGenButton.Size = New System.Drawing.Size(144, 23)
        Me.rfQuerySTDFGenButton.TabIndex = 20
        Me.rfQuerySTDFGenButton.Text = "rfQuerySTDFGen"
        '
        'rfResetSmartFGenButton
        '
        Me.rfResetSmartFGenButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rfResetSmartFGenButton.ForeColor = System.Drawing.Color.Blue
        Me.rfResetSmartFGenButton.Location = New System.Drawing.Point(34, 124)
        Me.rfResetSmartFGenButton.Name = "rfResetSmartFGenButton"
        Me.rfResetSmartFGenButton.Size = New System.Drawing.Size(144, 23)
        Me.rfResetSmartFGenButton.TabIndex = 22
        Me.rfResetSmartFGenButton.Text = "rfResetSmartFGen"
        '
        'BroadcastFGenCheckBox
        '
        Me.BroadcastFGenCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BroadcastFGenCheckBox.Location = New System.Drawing.Point(20, 144)
        Me.BroadcastFGenCheckBox.Name = "BroadcastFGenCheckBox"
        Me.BroadcastFGenCheckBox.Size = New System.Drawing.Size(84, 24)
        Me.BroadcastFGenCheckBox.TabIndex = 25
        Me.BroadcastFGenCheckBox.Text = "Broadcast"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 464)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 16)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Message List :"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(468, 464)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 16)
        Me.Label5.TabIndex = 24
        Me.Label5.Text = "IP Address List :"
        '
        'RdrCmdTypeComboBox
        '
        Me.RdrCmdTypeComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RdrCmdTypeComboBox.Items.AddRange(New Object() {"SPECIFIC_READER", "ALL_READERS"})
        Me.RdrCmdTypeComboBox.Location = New System.Drawing.Point(22, 64)
        Me.RdrCmdTypeComboBox.Name = "RdrCmdTypeComboBox"
        Me.RdrCmdTypeComboBox.Size = New System.Drawing.Size(156, 23)
        Me.RdrCmdTypeComboBox.TabIndex = 25
        Me.RdrCmdTypeComboBox.Text = "ALL_READERS"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(202, 50)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(120, 16)
        Me.Label6.TabIndex = 26
        Me.Label6.Text = "Tag Cmd Type : "
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(470, 20)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(56, 18)
        Me.Label11.TabIndex = 28
        Me.Label11.Text = "FGen ID: "
        '
        'FGenIDTextBox
        '
        Me.FGenIDTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FGenIDTextBox.Location = New System.Drawing.Point(528, 18)
        Me.FGenIDTextBox.Name = "FGenIDTextBox"
        Me.FGenIDTextBox.Size = New System.Drawing.Size(90, 21)
        Me.FGenIDTextBox.TabIndex = 27
        Me.FGenIDTextBox.Text = ""
        '
        'IPListBox
        '
        Me.IPListBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IPListBox.ItemHeight = 15
        Me.IPListBox.Location = New System.Drawing.Point(466, 482)
        Me.IPListBox.Name = "IPListBox"
        Me.IPListBox.Size = New System.Drawing.Size(200, 199)
        Me.IPListBox.TabIndex = 29
        '
        'ClearButton
        '
        Me.ClearButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ClearButton.ForeColor = System.Drawing.Color.Blue
        Me.ClearButton.Location = New System.Drawing.Point(396, 462)
        Me.ClearButton.Name = "ClearButton"
        Me.ClearButton.Size = New System.Drawing.Size(50, 18)
        Me.ClearButton.TabIndex = 30
        Me.ClearButton.Text = "Clear"
        '
        'LongIntervalCheckBox
        '
        Me.LongIntervalCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LongIntervalCheckBox.Location = New System.Drawing.Point(470, 44)
        Me.LongIntervalCheckBox.Name = "LongIntervalCheckBox"
        Me.LongIntervalCheckBox.Size = New System.Drawing.Size(100, 24)
        Me.LongIntervalCheckBox.TabIndex = 31
        Me.LongIntervalCheckBox.Text = "Long Interval"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.rfResetRdrButton)
        Me.GroupBox4.Controls.Add(Me.rfQueryRdrButton)
        Me.GroupBox4.Controls.Add(Me.Label12)
        Me.GroupBox4.Controls.Add(Me.Label10)
        Me.GroupBox4.Controls.Add(Me.FSTextBox)
        Me.GroupBox4.Controls.Add(Me.rfGetReaderFSButton)
        Me.GroupBox4.Controls.Add(Me.rfSetReaderFSButton)
        Me.GroupBox4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(190, 326)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(260, 134)
        Me.GroupBox4.TabIndex = 32
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Reader"
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(678, 691)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.LongIntervalCheckBox)
        Me.Controls.Add(Me.ClearButton)
        Me.Controls.Add(Me.IPListBox)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.FGenIDTextBox)
        Me.Controls.Add(Me.ReaderIDTextBox)
        Me.Controls.Add(Me.TagBox)
        Me.Controls.Add(Me.MsgArea)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.RdrCmdTypeComboBox)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TagCmdTypeComboBox)
        Me.Controls.Add(Me.INVRadioButton)
        Me.Controls.Add(Me.ASTRadioButton)
        Me.Controls.Add(Me.ACCRadioButton)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.SocketFlg)
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = ".NET VB  RFID API Examples  V8.0"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        readerPort = Convert.ToUInt16(10001)
        commPort = Convert.ToUInt32(1)
        commBaud = Convert.ToUInt32(115200)

        ' Initialize callback functions
        'new AW_API_NET.fReaderEvent(ReaderEvent);
        ReaderEventHandler = new AW_API_NET.fReaderEvent(AddressOf Me.OnReaderEvent)  'AddressOf Me.OnReaderEvent
        TagEventHandler = new AW_API_NET.fTagEvent(AddressOf Me.OnTagEvent) 

        registered = False

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rfOpenButton.Click
        Dim iRet As Integer

        myPKTID = myPKTID + 1
        
        iRet = ActiveWaveAPI.rfOpen(commBaud, commPort)
        If (iRet = 0) Then
            AddMsg("Open Com Port:" + commPort.ToString() + " baud:" + commBaud.ToString() + " Successful")
        Else
            AddMsg("Open Com Port Failed")
        End If

        If registered = False Then
            ' Register reader callback handler
            ActiveWaveAPI.rfRegisterReaderEvent(ReaderEventHandler)
            AddMsg("ReaderEvent registered")

            ' Register tag callback handler
            ActiveWaveAPI.rfRegisterTagEvent(TagEventHandler)
            AddMsg("TagEvent registered")

            registered = True
        End If

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rfCloseButton.Click
        Dim iRet As Integer
        iRet = ActiveWaveAPI.rfClose()
        AddMsg("Close: " + iRet.ToString())

    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub AddMsg(ByVal msg As String)
        If msg.Length > 0 Then
            MsgArea.AppendText(vbCrLf & Now.ToString("hh:mm:ss") + ": " & msg & vbCrLf)
        End If
        MsgArea.Invalidate()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rfResetRdrButton.Click
        Dim iRet As Integer

        If (STDFGenCheckBox.Checked = True) Then
            MsgBox("Change baudrate to 115200")
            Return
        End If

        myPKTID = myPKTID + 1
        If (RdrCmdTypeComboBox.Text.Equals("ALL_READERS")) Then
            iRet = ActiveWaveAPI.rfResetReader(UInt16.Parse(1), UInt16.Parse(0), UInt16.Parse(0), Convert.ToUInt16(AW_API_NET.APIConsts.ALL_READERS), Convert.ToUInt16(myPKTID))
        Else
            If (ReaderIDTextBox.Text.Equals("")) Then
                MsgBox("No Reader ID", MsgBoxStyle.OKOnly, "Error Msg")
                Return
            End If
            iRet = ActiveWaveAPI.rfResetReader(UInt16.Parse(1), Convert.ToUInt16(ReaderIDTextBox.Text), UInt16.Parse(0), Convert.ToUInt16(AW_API_NET.APIConsts.SPECIFIC_READER), Convert.ToUInt16(myPKTID))
        End If
        AddMsg("ResetReader: " + iRet.ToString())
    End Sub

    Private Sub TagButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TagButton.Click
        Dim iRet As Integer
        Dim limit As UInt32
        Dim tagSelect As AW_API_NET.rfTagSelect_t
        Dim tagList(50) As UInt32
        Dim rdrID As UInt16
        Dim longInterval As Boolean
        Dim RdrCmdType As Integer

        If (STDFGenCheckBox.Checked = True) Then
            MsgBox("Change baudrate to 115200")
            Return
        End If

        If (RdrCmdTypeComboBox.SelectedIndex = 0) Then
            If (ReaderIDTextBox.Text.Equals("")) Then
                MsgBox("No Reader ID", MsgBoxStyle.OKOnly, "Error Msg")
                Return
            Else
                rdrID = Convert.ToUInt16(ReaderIDTextBox.Text)
            End If
        Else
            rdrID = UInt16.Parse(0)
        End If

        If (RdrCmdTypeComboBox.SelectedIndex = 0) Then
            RdrCmdType = APIConsts.SPECIFIC_READER
        Else
            RdrCmdType = APIConsts.ALL_READERS
        End If

        tagSelect.tagList = tagList

        If (TagBox.Text.Equals("")) Then
            If ((TagCmdTypeComboBox.SelectedIndex = 0) Or (TagCmdTypeComboBox.SelectedIndex = 3)) Then
                MsgBox("No Tag ID", MsgBoxStyle.OKOnly, "Error Msg")
                Return
            Else
                tagSelect.tagList(0) = UInt32.Parse(0)
                tagSelect.numTags = Convert.ToUInt32(1)
            End If
        Else
            tagSelect.tagList(0) = Convert.ToUInt32(TagBox.Text)
            tagSelect.numTags = Convert.ToUInt32(1)
        End If

        If (TagCmdTypeComboBox.SelectedIndex = 0) Then
            tagSelect.selectType = Convert.ToUInt32(AW_API_NET.APIConsts.RF_SELECT_TAG_ID)
        ElseIf (TagCmdTypeComboBox.SelectedIndex = 1) Then
            tagSelect.selectType = Convert.ToUInt32(AW_API_NET.APIConsts.RF_SELECT_FIELD)
        ElseIf (TagCmdTypeComboBox.SelectedIndex = 2) Then
            tagSelect.selectType = Convert.ToUInt32(AW_API_NET.APIConsts.RF_SELECT_TAG_TYPE)
        Else
            tagSelect.selectType = Convert.ToUInt32(AW_API_NET.APIConsts.RF_SELECT_TAG_RANGE)
        End If

        If (ACCRadioButton.Checked) Then
            tagSelect.tagType = APIConsts.ACCESS_TAG
        ElseIf (ASTRadioButton.Checked) Then
            tagSelect.tagType = APIConsts.ASSET_TAG
        Else
            tagSelect.tagType = APIConsts.INVENTORY_TAG
        End If

        If (myPKTID >= 223) Then
            myPKTID = 1
        Else
            myPKTID = myPKTID + 1
        End If

        If (LongIntervalCheckBox.Checked) Then
            longInterval = True
        Else
            longInterval = False
        End If

        iRet = ActiveWaveAPI.rfCallTags(UInt16.Parse(1), rdrID, UInt16.Parse(0), UInt16.Parse(0), tagSelect, True, longInterval, Convert.ToUInt16(RdrCmdType), Convert.ToUInt16(myPKTID))
        AddMsg("CallTags: " + iRet.ToString())
    End Sub

    Private Function OnReaderEvent(ByVal readerEvent As AW_API_NET.rfReaderEvent_t) As Integer

        Dim ipStr As String

        ipStr = ""

        Beep()
        If readerEvent.eventType.Equals(Convert.ToUInt16(AW_API_NET.APIConsts.RF_SCAN_NETWORK)) Then
            For i As Integer = 0 To readerEvent.ip.Length - 1
                ipStr += Convert.ToChar(readerEvent.ip(i))
            Next I
            IPListBox.Items.Add(ipStr)
        ElseIf readerEvent.eventType.Equals(Convert.ToUInt16(AW_API_NET.APIConsts.RF_OPEN_SOCKET)) Then
            For i As Integer = 0 To readerEvent.ip.Length - 1
                ipStr += Convert.ToChar(readerEvent.ip(i))
            Next I
            AddMsg("Socket Opened IP = " + ipStr)
        ElseIf readerEvent.eventType.Equals(Convert.ToUInt16(AW_API_NET.APIConsts.RF_CLOSE_SOCKET)) Then
            For i As Integer = 0 To readerEvent.ip.Length - 1
                ipStr += Convert.ToChar(readerEvent.ip(i))
            Next I
            AddMsg("Socket Closed IP = " + ipStr)

            Dim index As Integer
            If ipStr.Length > 0 Then
                index = IPListBox.FindStringExact(ipStr)
                If index >= 0 Then
                    IPListBox.Items.RemoveAt(index)
                End If
            End If
        ElseIf readerEvent.eventType.Equals(Convert.ToUInt16(AW_API_NET.APIConsts.RF_STD_FGEN_POWERUP)) Then
            FGenIDTextBox.Text = readerEvent.fGenerator.ToString()
            AddMsg("STD FGen Powered UP")
        ElseIf readerEvent.eventType.Equals(Convert.ToUInt16(AW_API_NET.APIConsts.RF_READER_POWERUP)) Then
            ReaderIDTextBox.Text = readerEvent.reader.ToString()
            AddMsg("Reader Powered UP")
        ElseIf readerEvent.eventType.Equals(Convert.ToUInt16(AW_API_NET.APIConsts.RF_QUERY_STD_FGEN)) Then
            Dim str As String

            AddMsg(readerEvent.eventType.ToString)
            AddMsg(AW_API_NET.APIConsts.RF_READER_POWERUP.ToString)

            AddMsg("STD FGEN Query __________")
            str = readerEvent.smartFgen.fsValue
            AddMsg("FS Value = " + str)
            str = readerEvent.smartFgen.txTime
            AddMsg("TX Time = " + str)
            str = readerEvent.smartFgen.waitTime
            AddMsg("Wait Time = " + str)
            str = readerEvent.smartFgen.assignRdr
            AddMsg("Assigned Rdr = " + str)
        ElseIf readerEvent.eventType.Equals(Convert.ToUInt16(AW_API_NET.APIConsts.RF_GET_RDR_FS)) Then
            FSTextBox.Text = readerEvent.smartFgen.fsValue.ToString()
            AddMsg("Reader ID:" + readerEvent.reader.ToString() + "  FS:" + readerEvent.smartFgen.fsValue.ToString())
        ElseIf readerEvent.eventType.Equals(Convert.ToUInt16(AW_API_NET.APIConsts.RF_SET_RDR_FS)) Then
            AddMsg("Reader FS was set successfully")
        ElseIf readerEvent.eventType.Equals(Convert.ToUInt16(AW_API_NET.APIConsts.RF_SCAN_IP)) Then
            ipStr = GetStringIP(readerEvent.ip)
            If ipStr.Length > 0 Then
                If IPListBox.FindStringExact(ipStr) = -1 Then
                    IPListBox.Items.Add(ipStr)
                End If
                AddMsg("ScanIP IP=" + ipStr)
            End If
        End If

        ReportReaderEvent(readerEvent)

        Return 0
    End Function

    Private Function OnTagEvent(ByVal tagEvent As AW_API_NET.rfTagEvent_t) As Integer

        Dim ipStr As String

        ipStr = ""

        Beep()

        If tagEvent.eventType.Equals(Convert.ToUInt16(AW_API_NET.APIConsts.RF_TAG_READ)) Then
            Dim str As String
            Dim n As Integer
            n = CInt(Convert.ToInt16(tagEvent.tag.dataLen))
            For i As Integer = 0 To n - 1
                str = tagEvent.tag.data(i)
                ipStr += str + " "
            Next
            AddMsg("Data :" + ipStr + vbCrLf)
        End If

        ReportTagEvent(tagEvent)

        Return 0
    End Function

    Private Sub ReportReaderEvent(ByRef readerEvent As AW_API_NET.rfReaderEvent_t)
        Dim msg As String

        msg = "eventType " + readerEvent.eventType.ToString() + vbCrLf _
            + vbTab + "errorStatus = " + readerEvent.errorStatus.ToString() + vbCrLf _
            + vbTab + "host = " + readerEvent.host.ToString() + vbCrLf _
            + vbTab + "reader = " + readerEvent.reader.ToString() + vbCrLf _
            + vbTab + "fGenerator = " + readerEvent.fGenerator.ToString() + vbCrLf _
            + vbTab + "cmdType = " + readerEvent.cmdType.ToString() + vbCrLf _
            + vbTab + "eventStatus = " + readerEvent.eventStatus.ToString() + vbCrLf _
            + vbTab + "pktID = " + readerEvent.pktID.ToString() + vbCrLf

        AddMsg(msg)
    End Sub

    Private Sub ReportTagEvent(ByRef tagEvent As AW_API_NET.rfTagEvent_t)
        Dim msg As String

        msg = "eventType " + tagEvent.eventType.ToString() + vbCrLf _
            + vbTab + "errorStatus = " + tagEvent.errorStatus.ToString() + vbCrLf _
            + vbTab + "host = " + tagEvent.host.ToString() + vbCrLf _
            + vbTab + "reader = " + tagEvent.reader.ToString() + vbCrLf _
            + vbTab + "fGenerator = " + tagEvent.fGenerator.ToString() + vbCrLf _
            + vbTab + "eventStatus = " + tagEvent.eventStatus.ToString() + vbCrLf _
            + vbTab + "cmdType = " + tagEvent.cmdType.ToString() + vbCrLf _
            + vbTab + "RSSI = " + tagEvent.RSSI.ToString() + vbCrLf _
            + vbTab + "tagID = " + tagEvent.tag.id.ToString() + vbCrLf _
            + vbTab + "tagType = " + tagEvent.tag.tagType.ToString() + vbCrLf _
            + vbTab + "pktID = " + tagEvent.pktID.ToString() + vbCrLf

        AddMsg(msg)
    End Sub

    Private Sub QueryButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QueryButton.Click
        Dim iRet As Integer
        Dim tagSelect As AW_API_NET.rfTagSelect_t
        Dim tagList(50) As UInt32
        Dim rdrID As UInt16
        Dim longInterval As Boolean
        Dim RdrCmdType As Integer

        If (STDFGenCheckBox.Checked = True) Then
            MsgBox("Change baudrate to 115200")
            Return
        End If

        If (RdrCmdTypeComboBox.SelectedIndex = 0) Then
            If (ReaderIDTextBox.Text.Equals("")) Then
                MsgBox("No Reader ID", MsgBoxStyle.OKOnly, "Error Msg")
                Return
            Else
                rdrID = Convert.ToUInt16(ReaderIDTextBox.Text)
            End If
        Else
            rdrID = UInt16.Parse(0)
        End If

        tagSelect.tagList = tagList

        If (TagBox.Text.Equals("")) Then
            If ((TagCmdTypeComboBox.SelectedIndex = 0) Or (TagCmdTypeComboBox.SelectedIndex = 3)) Then
                MsgBox("No Tag ID", MsgBoxStyle.OKOnly, "Error Msg")
                Return
            Else
                tagSelect.tagList(0) = UInt32.Parse(0)
                tagSelect.numTags = Convert.ToUInt32(1)
            End If
        Else
            tagSelect.tagList(0) = Convert.ToUInt32(TagBox.Text)
            tagSelect.numTags = Convert.ToUInt32(1)
        End If

        If (RdrCmdTypeComboBox.SelectedIndex = 0) Then
            RdrCmdType = APIConsts.SPECIFIC_READER
        Else
            RdrCmdType = APIConsts.ALL_READERS
        End If

        If (TagCmdTypeComboBox.SelectedIndex = 0) Then
            tagSelect.selectType = Convert.ToUInt32(AW_API_NET.APIConsts.RF_SELECT_TAG_ID)
        ElseIf (TagCmdTypeComboBox.SelectedIndex = 1) Then
            tagSelect.selectType = Convert.ToUInt32(AW_API_NET.APIConsts.RF_SELECT_FIELD)
        ElseIf (TagCmdTypeComboBox.SelectedIndex = 2) Then
            tagSelect.selectType = Convert.ToUInt32(AW_API_NET.APIConsts.RF_SELECT_TAG_TYPE)
        Else
            tagSelect.selectType = Convert.ToUInt32(AW_API_NET.APIConsts.RF_SELECT_TAG_RANGE)
        End If

        If (ACCRadioButton.Checked) Then
            tagSelect.tagType = APIConsts.ACCESS_TAG
        ElseIf (ASTRadioButton.Checked) Then
            tagSelect.tagType = APIConsts.ASSET_TAG
        Else
            tagSelect.tagType = APIConsts.INVENTORY_TAG
        End If

        If (myPKTID >= 223) Then
            myPKTID = 1
        Else
            myPKTID = myPKTID + 1
        End If

        If (LongIntervalCheckBox.Checked) Then
            longInterval = True
        Else
            longInterval = False
        End If

        iRet = ActiveWaveAPI.rfQueryTags(UInt16.Parse(1), rdrID, UInt16.Parse(0), tagSelect, True, longInterval, Convert.ToUInt16(RdrCmdType), Convert.ToUInt16(myPKTID))
        AddMsg("QueryTags: " + iRet.ToString())
    End Sub

    Private Sub SocketFlg_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SocketFlg.CheckedChanged

        If (SocketFlg.Checked) Then
            rfScanButton.Enabled = True
            rfOpenButton.Text = "rfOpenSocket"
            rfCloseButton.Text = "rfCloseSocket"
        Else
            rfScanButton.Enabled = False
            rfOpenButton.Text = "rfOpen"
            rfCloseButton.Text = "rfClose"
        End If

    End Sub

    Private Sub rfScanButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rfScanButton.Click

        Dim iRet As Integer

        If (myPKTID >= 223) Then
            myPKTID = 1
        Else
            myPKTID = myPKTID + 1
        End If

        If registered = False Then

            ' Register reader callback handler
            ActiveWaveAPI.rfRegisterReaderEvent(ReaderEventHandler)
            AddMsg("ReaderEvent registered")

            ' Register tag callback handler
            ActiveWaveAPI.rfRegisterTagEvent(TagEventHandler)
            AddMsg("TagEvent registered")

            registered = True
        End If

        IPListBox.Items.Clear()
        iRet = ActiveWaveAPI.rfScanNetwork(Convert.ToUInt16(myPKTID))
        AddMsg("rfScanNetwork: " + iRet.ToString())

    End Sub

    Private Sub ClearButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearButton.Click
        MsgArea.Clear()
    End Sub

    Private Sub rfQueryRdrButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rfQueryRdrButton.Click

        Dim iRet As Integer
        Dim rdrID As UInt16

        If (STDFGenCheckBox.Checked = True) Then
            MsgBox("Change baudrate to 115200")
            Return
        End If

        If (RdrCmdTypeComboBox.SelectedIndex = 0) Then
            If (ReaderIDTextBox.Text.Equals("")) Then
                MsgBox("No Reader ID", MsgBoxStyle.OKOnly, "Error Msg")
                Return
            Else
                rdrID = Convert.ToUInt16(ReaderIDTextBox.Text)
            End If
        Else
            rdrID = UInt16.Parse(0)
        End If



        If (myPKTID >= 223) Then
            myPKTID = 1
        Else
            myPKTID = myPKTID + 1
        End If

        If (RdrCmdTypeComboBox.SelectedIndex = 0) Then
            iRet = ActiveWaveAPI.rfQueryReader(UInt16.Parse(1), rdrID, UInt16.Parse(0), Convert.ToUInt16(APIConsts.SPECIFIC_READER), Convert.ToUInt16(myPKTID))
        Else
            iRet = ActiveWaveAPI.rfQueryReader(UInt16.Parse(1), rdrID, UInt16.Parse(0), Convert.ToUInt16(APIConsts.ALL_READERS), Convert.ToUInt16(myPKTID))
        End If
        AddMsg("rfQueryReader: " + iRet.ToString())

    End Sub

    Private Sub EnableTagButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnableTagButton.Click

        Dim iRet As Integer
        Dim tagSelect As AW_API_NET.rfTagSelect_t
        Dim tagList(50) As UInt32
        Dim rdrID As UInt16
        Dim RdrCmdType As Integer
        Dim longInterval As Boolean

        If (STDFGenCheckBox.Checked = True) Then
            MsgBox("Change baudrate to 115200")
            Return
        End If

        If (RdrCmdTypeComboBox.SelectedIndex = 0) Then
            If (ReaderIDTextBox.Text.Equals("")) Then
                MsgBox("No Reader ID", MsgBoxStyle.OKOnly, "Error Msg")
                Return
            Else
                rdrID = Convert.ToUInt16(ReaderIDTextBox.Text)
            End If
        Else
            rdrID = UInt16.Parse(0)
        End If

        tagSelect.tagList = tagList

        If (TagBox.Text.Equals("")) Then
            If ((TagCmdTypeComboBox.SelectedIndex = 0) Or (TagCmdTypeComboBox.SelectedIndex = 3)) Then
                MsgBox("No Tag ID", MsgBoxStyle.OKOnly, "Error Msg")
                Return
            Else
                tagSelect.tagList(0) = UInt32.Parse(0)
                tagSelect.numTags = Convert.ToUInt32(1)
            End If
        Else
            tagSelect.tagList(0) = Convert.ToUInt32(TagBox.Text)
            tagSelect.numTags = Convert.ToUInt32(1)
        End If

        If (RdrCmdTypeComboBox.SelectedIndex = 0) Then
            RdrCmdType = AW_API_NET.APIConsts.SPECIFIC_READER
        Else
            RdrCmdType = AW_API_NET.APIConsts.ALL_READERS
        End If

        If (TagCmdTypeComboBox.SelectedIndex = 0) Then
            tagSelect.selectType = Convert.ToUInt32(AW_API_NET.APIConsts.RF_SELECT_TAG_ID)
        ElseIf (TagCmdTypeComboBox.SelectedIndex = 1) Then
            tagSelect.selectType = Convert.ToUInt32(AW_API_NET.APIConsts.RF_SELECT_FIELD)
        ElseIf (TagCmdTypeComboBox.SelectedIndex = 2) Then
            tagSelect.selectType = Convert.ToUInt32(AW_API_NET.APIConsts.RF_SELECT_TAG_TYPE)
        Else
            tagSelect.selectType = Convert.ToUInt32(AW_API_NET.APIConsts.RF_SELECT_TAG_RANGE)
        End If

        If (ACCRadioButton.Checked) Then
            tagSelect.tagType = AW_API_NET.APIConsts.ACCESS_TAG
        ElseIf (ASTRadioButton.Checked) Then
            tagSelect.tagType = AW_API_NET.APIConsts.ASSET_TAG
        Else
            tagSelect.tagType = AW_API_NET.APIConsts.INVENTORY_TAG
        End If

        If (myPKTID >= 223) Then
            myPKTID = 1
        Else
            myPKTID = myPKTID + 1
        End If

        If (LongIntervalCheckBox.Checked) Then
            longInterval = True
        Else
            longInterval = False
        End If

        iRet = ActiveWaveAPI.rfEnableTags(UInt16.Parse(1), rdrID, UInt16.Parse(0), tagSelect, True, True, longInterval, Convert.ToUInt16(RdrCmdType), Convert.ToUInt16(myPKTID))
        AddMsg("EnableTags: " + iRet.ToString())

    End Sub

    Private Sub ReadTagButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReadTagButton.Click

        Dim iRet As Integer
        Dim tagSelect As AW_API_NET.rfTagSelect_t
        Dim tagList(50) As UInt32
        Dim rdrID As UInt16
        Dim RdrCmdType As Integer
        Dim longInterval As Boolean
        Dim address As Integer
        Dim len As UInt16

        If (STDFGenCheckBox.Checked = True) Then
            MsgBox("Change baudrate to 115200")
            Return
        End If

        If (RdrCmdTypeComboBox.SelectedIndex = 0) Then
            If (ReaderIDTextBox.Text.Equals("")) Then
                MsgBox("No Reader ID", MsgBoxStyle.OKOnly, "Error Msg")
                Return
            Else
                rdrID = Convert.ToUInt16(ReaderIDTextBox.Text)
            End If
        Else
            rdrID = UInt16.Parse(0)
        End If

        tagSelect.tagList = tagList

        If (TagBox.Text.Equals("")) Then
            If ((TagCmdTypeComboBox.SelectedIndex = 0) Or (TagCmdTypeComboBox.SelectedIndex = 3)) Then
                MsgBox("No Tag ID", MsgBoxStyle.OKOnly, "Error Msg")
                Return
            Else
                tagSelect.tagList(0) = UInt32.Parse(0)
                tagSelect.numTags = Convert.ToUInt32(1)
            End If
        Else
            tagSelect.tagList(0) = Convert.ToUInt32(TagBox.Text)
            tagSelect.numTags = Convert.ToUInt32(1)
        End If

        address = CInt("&H" + AddressTextBox.Text)
        len = Convert.ToUInt16(LengthTextBox.Text)

        If (RdrCmdTypeComboBox.SelectedIndex = 0) Then
            RdrCmdType = APIConsts.SPECIFIC_READER
        Else
            RdrCmdType = APIConsts.ALL_READERS
        End If

        If (TagCmdTypeComboBox.SelectedIndex = 0) Then
            tagSelect.selectType = Convert.ToUInt32(AW_API_NET.APIConsts.RF_SELECT_TAG_ID)
        ElseIf (TagCmdTypeComboBox.SelectedIndex = 1) Then
            tagSelect.selectType = Convert.ToUInt32(AW_API_NET.APIConsts.RF_SELECT_FIELD)
        ElseIf (TagCmdTypeComboBox.SelectedIndex = 2) Then
            tagSelect.selectType = Convert.ToUInt32(AW_API_NET.APIConsts.RF_SELECT_TAG_TYPE)
        Else
            tagSelect.selectType = Convert.ToUInt32(AW_API_NET.APIConsts.RF_SELECT_TAG_RANGE)
        End If

        If (ACCRadioButton.Checked) Then
            tagSelect.tagType = AW_API_NET.APIConsts.ACCESS_TAG
        ElseIf (ASTRadioButton.Checked) Then
            tagSelect.tagType = AW_API_NET.APIConsts.ASSET_TAG
        Else
            tagSelect.tagType = AW_API_NET.APIConsts.INVENTORY_TAG
        End If

        If (myPKTID >= 223) Then
            myPKTID = 1
        Else
            myPKTID = myPKTID + 1
        End If

        If (LongIntervalCheckBox.Checked) Then
            longInterval = True
        Else
            longInterval = False
        End If

        iRet = ActiveWaveAPI.rfReadTags(UInt16.Parse(1), rdrID, UInt16.Parse(0), tagSelect, Convert.ToUInt32(address), len, True, longInterval, Convert.ToUInt16(RdrCmdType), UInt16.Parse(myPKTID))
        AddMsg("rfReadTags: " + iRet.ToString())
    End Sub

    Private Sub STDFGenCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles STDFGenCheckBox.CheckedChanged

        Dim iRet As Integer

        If (STDFGenCheckBox.Checked) Then

            If SocketFlg.Checked Then
                ActiveWaveAPI.rfCloseSocket(readerIP, Convert.ToUInt16(AW_API_NET.APIConsts.ALL_IPS))
                SocketFlg.Checked = False
                IPListBox.Items.Clear()
            Else
                ActiveWaveAPI.rfClose()
            End If

            rfScanButton.Enabled = False
            rfOpenButton.Enabled = False
            rfCloseButton.Enabled = False
            rfResetRdrButton.Enabled = False
            rfQueryRdrButton.Enabled = False
            TagButton.Enabled = False
            QueryButton.Enabled = False
            EnableTagButton.Enabled = False
            ReadTagButton.Enabled = False
            rfResetSmartFGenButton.Enabled = False
            rfQuerySTDFGenButton.Enabled = True

            iRet = ActiveWaveAPI.rfOpen(UInt32.Parse(9600), commPort)

            If (iRet = 0) Then
                AddMsg("Open Com Port:" + commPort.ToString() + " baud:9600 Successful")
            Else
                AddMsg("Open Com Port Failed")
            End If

            If registered = False Then
                ' Register reader callback handler
                ActiveWaveAPI.rfRegisterReaderEvent(ReaderEventHandler)
                AddMsg("ReaderEvent registered")

                ' Register tag callback handler
                ActiveWaveAPI.rfRegisterTagEvent(TagEventHandler)
                AddMsg("TagEvent registered")

                registered = True
            End If

        Else
            rfScanButton.Enabled = True
            rfOpenButton.Enabled = True
            rfCloseButton.Enabled = True
            rfResetRdrButton.Enabled = True
            rfQueryRdrButton.Enabled = True
            TagButton.Enabled = True
            QueryButton.Enabled = True
            EnableTagButton.Enabled = True
            ReadTagButton.Enabled = True
            rfResetSmartFGenButton.Enabled = True
            rfQuerySTDFGenButton.Enabled = False

            ActiveWaveAPI.rfClose()
            iRet = ActiveWaveAPI.rfOpen(UInt32.Parse(115200), commPort)

            If (iRet = 0) Then
                AddMsg("Open Com Port:" + commPort.ToString() + " baud:115200 Successful")
            Else
                AddMsg("Open Com Port Failed")
            End If

            If registered = False Then
                ' Register reader callback handler
                ActiveWaveAPI.rfRegisterReaderEvent(ReaderEventHandler)
                AddMsg("ReaderEvent registered")

                ' Register tag callback handler
                ActiveWaveAPI.rfRegisterTagEvent(TagEventHandler)
                AddMsg("TagEvent registered")

                registered = True
            End If

        End If

    End Sub

    Private Sub rfQuerySTDFGenButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rfQuerySTDFGenButton.Click

        Dim iRet As Integer
        Dim fGenID As UInt16

        If (STDFGenCheckBox.Checked = False) Then
            MsgBox("Change baudrate to 9600")
            Return
        End If

        If (FGenIDTextBox.Text.Equals("")) Then
            MsgBox("No FGen ID", MsgBoxStyle.OKOnly, "Error Msg")
            Return
        End If

        fGenID = Convert.ToUInt16(FGenIDTextBox.Text)

        If (myPKTID >= 223) Then
            myPKTID = 1
        Else
            myPKTID = myPKTID + 1
        End If

        iRet = ActiveWaveAPI.rfQuerySTDFGen(UInt16.Parse(1), fGenID, Convert.ToUInt16(myPKTID))
        AddMsg("rfQuerySTDFGen: " + iRet.ToString())

    End Sub

    Private Sub BroadcastAllFGenCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub rfResetSmartFGenButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rfResetSmartFGenButton.Click
        Dim iRet As Integer
        Dim fGenID As UInt16
        Dim rdrID As UInt16

        If (RdrCmdTypeComboBox.SelectedIndex = 0) Then
            If (ReaderIDTextBox.Text.Equals("")) Then
                MsgBox("No Reader ID", MsgBoxStyle.OKOnly, "Error Msg")
                Return
            Else
                rdrID = Convert.ToUInt16(ReaderIDTextBox.Text)
            End If
        Else
            rdrID = UInt16.Parse(0)
        End If

        If (FGenIDTextBox.Text.Equals("")) Then
            MsgBox("No FGen ID", MsgBoxStyle.OKOnly, "Error Msg")
            Return
        End If

        If (STDFGenCheckBox.Checked = True) Then
            MsgBox("Change baudrate to 115200")
            Return
        End If

        fGenID = Convert.ToUInt16(FGenIDTextBox.Text)

        If (myPKTID >= 223) Then
            myPKTID = 1
        Else
            myPKTID = myPKTID + 1
        End If

        Dim broadcast As Boolean = False
        If (BroadcastFGenCheckBox.Checked) Then
            broadcast = True
        End If

        If (RdrCmdTypeComboBox.SelectedIndex = 0) Then
            iRet = ActiveWaveAPI.rfResetSmartFGen(UInt16.Parse(1), rdrID, UInt16.Parse(0), fGenID, Convert.ToUInt16(AW_API_NET.APIConsts.SPECIFIC_READER), broadcast, Convert.ToUInt16(myPKTID))
        Else
            iRet = ActiveWaveAPI.rfResetSmartFGen(UInt16.Parse(1), rdrID, UInt16.Parse(0), fGenID, Convert.ToUInt16(AW_API_NET.APIConsts.ALL_READERS), broadcast, Convert.ToUInt16(myPKTID))
        End If

        AddMsg("rfResetSmartFGen: " + iRet.ToString())
    End Sub

    Private Sub rfSetReaderFSButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rfSetReaderFSButton.Click
        Dim iRet As Integer
        Dim rdrID As UInt16

        If (STDFGenCheckBox.Checked = True) Then
            MsgBox("Change baudrate to 115200")
            Return
        End If

        If (FSTextBox.Text.Equals("")) Then
            MsgBox("No FS value", MsgBoxStyle.OKOnly, "Error Msg")
            Return
        End If

        If ((Convert.ToInt16(FSTextBox.Text) < 0) Or (Convert.ToInt16(FSTextBox.Text) > 20)) Then
            MsgBox("FS value outside the limit(0-20)", MsgBoxStyle.OKOnly, "Error Msg")
            Return
        End If

        If (RdrCmdTypeComboBox.SelectedIndex = 0) Then
            If (ReaderIDTextBox.Text.Equals("")) Then
                MsgBox("No Reader ID", MsgBoxStyle.OKOnly, "Error Msg")
                Return
            Else
                rdrID = Convert.ToUInt16(ReaderIDTextBox.Text)
            End If
        Else
            rdrID = UInt16.Parse(0)
        End If

        If (myPKTID >= 223) Then
            myPKTID = 1
        Else
            myPKTID = myPKTID + 1
        End If

        If (RdrCmdTypeComboBox.SelectedIndex = 0) Then
            iRet = ActiveWaveAPI.rfSetReaderFS(UInt16.Parse(1), rdrID, UInt16.Parse(0), Convert.ToUInt16(AW_API_NET.APIConsts.RF_ABSOLUTE), Convert.ToByte(FSTextBox.Text), False, Convert.ToUInt16(APIConsts.SPECIFIC_READER), Convert.ToUInt16(myPKTID))
        Else
            iRet = ActiveWaveAPI.rfSetReaderFS(UInt16.Parse(1), rdrID, UInt16.Parse(0), Convert.ToUInt16(AW_API_NET.APIConsts.RF_ABSOLUTE), Convert.ToByte(FSTextBox.Text), False, Convert.ToUInt16(APIConsts.ALL_READERS), Convert.ToUInt16(myPKTID))
        End If
        AddMsg("rfSetReaderFS: " + iRet.ToString())
    End Sub

    Private Sub rfGetReaderFSButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rfGetReaderFSButton.Click
        Dim iRet As Integer
        Dim rdrID As UInt16

        If (STDFGenCheckBox.Checked = True) Then
            MsgBox("Change baudrate to 115200")
            Return
        End If

        If (RdrCmdTypeComboBox.SelectedIndex = 0) Then
            If (ReaderIDTextBox.Text.Equals("")) Then
                MsgBox("No Reader ID", MsgBoxStyle.OKOnly, "Error Msg")
                Return
            Else
                rdrID = Convert.ToUInt16(ReaderIDTextBox.Text)
            End If
        Else
            rdrID = UInt16.Parse(0)
        End If

        If (myPKTID >= 223) Then
            myPKTID = 1
        Else
            myPKTID = myPKTID + 1
        End If

        FSTextBox.Text = ""

        If (RdrCmdTypeComboBox.SelectedIndex = 0) Then
            iRet = ActiveWaveAPI.rfGetReaderFS(UInt16.Parse(1), rdrID, UInt16.Parse(0), Convert.ToUInt16(APIConsts.SPECIFIC_READER), Convert.ToUInt16(myPKTID))
        Else
            iRet = ActiveWaveAPI.rfGetReaderFS(UInt16.Parse(1), rdrID, UInt16.Parse(0), Convert.ToUInt16(APIConsts.ALL_READERS), Convert.ToUInt16(myPKTID))
        End If
        AddMsg("rfGetReaderFS: " + iRet.ToString())

    End Sub

    Private Sub AllIPRadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AllIPRadioButton.CheckedChanged
        If SpecificIPRadioButton.Checked Then
            rfScanButton.Enabled = False
            rfScanIPButton.Enabled = True
            IPTextBox.ReadOnly = False
        Else
            rfScanButton.Enabled = True
            rfScanIPButton.Enabled = False
            IPTextBox.ReadOnly = True
        End If
    End Sub

    Private Sub rfScanIPButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rfScanIPButton.Click
        Dim iRet As Integer
        Dim ip(20) As Byte

        If (myPKTID >= 223) Then
            myPKTID = 1
        Else
            myPKTID = myPKTID + 1
        End If

        If registered = False Then

            ' Register reader callback handler
            ActiveWaveAPI.rfRegisterReaderEvent(ReaderEventHandler)
            AddMsg("ReaderEvent registered")

            ' Register tag callback handler
            ActiveWaveAPI.rfRegisterTagEvent(TagEventHandler)
            AddMsg("TagEvent registered")

            registered = True
        End If

        If IPTextBox.TextLength = 0 Then
            AddMsg("No IP Address")
            Return
        End If

        For i As Integer = 0 To IPTextBox.Text.Length - 1
            ip(i) = Convert.ToByte(IPTextBox.Text.Chars(i))
        Next i


        iRet = ActiveWaveAPI.rfScanIP(ip, Convert.ToUInt16(myPKTID))
        AddMsg("rfScanIP: " + iRet.ToString())
    End Sub

    Private Sub rfOpenSocketButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rfOpenSocketButton.Click
        Dim iRet As Integer
        Dim ip(20) As Byte
        Dim cIP(20) As Char

        myPKTID = myPKTID + 1
        If AllIPRadioButton.Checked Then
            iRet = ActiveWaveAPI.rfOpenSocket(readerIP, readerPort, False, Convert.ToUInt16(AW_API_NET.APIConsts.ALL_IPS), Convert.ToUInt16(myPKTID))
        Else
            Dim ipStr As String = IPTextBox.Text
            cIP = ipStr.ToCharArray(0, ipStr.Length)

            For i As Integer = 0 To IPTextBox.Text.Length - 1
                ip(i) = Convert.ToByte(IPTextBox.Text.Chars(i))
            Next i

            iRet = ActiveWaveAPI.rfOpenSocket(ip, readerPort, False, Convert.ToUInt16(AW_API_NET.APIConsts.SPECIFIC_IP), Convert.ToUInt16(myPKTID))
        End If

        If (iRet = 0) Then
            AddMsg("rfOpenSocket Successful. Return Code = " + iRet.ToString())
        Else
            AddMsg("rfOpenSocket Failed Return Code = " + iRet.ToString())
        End If

    End Sub

    Private Sub rfCloseSocketButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rfCloseSocketButton.Click
        Dim iRet As Integer
        Dim ip(20) As Byte
        Dim cIP(20) As Char

        myPKTID = myPKTID + 1
        If AllIPRadioButton.Checked Then
            iRet = ActiveWaveAPI.rfCloseSocket(readerIP, Convert.ToUInt16(AW_API_NET.APIConsts.ALL_IPS))
            IPListBox.Items.Clear()
        Else
            Dim ipStr As String = IPTextBox.Text
            cIP = ipStr.ToCharArray(0, ipStr.Length)

            For i As Integer = 0 To IPTextBox.Text.Length - 1
                ip(i) = Convert.ToByte(IPTextBox.Text.Chars(i))
            Next i

            iRet = ActiveWaveAPI.rfCloseSocket(ip, Convert.ToUInt16(AW_API_NET.APIConsts.SPECIFIC_IP))
        End If

        If (iRet = 0) Then
            AddMsg("rfCloseSocket Successful. Return Code = " + iRet.ToString())
        Else
            AddMsg("rfCoseSocket Failed Return Code = " + iRet.ToString())
        End If

    End Sub

    Public Function GetStringIP(ByVal ip As Byte()) As String

        Dim p As Integer
        Dim s As String
        Dim ct As Integer

        ct = 0
        p = 0
        s = ""
        While (Convert.ToBoolean((ct <= 3)) AndAlso Convert.ToBoolean((p < 20)) AndAlso Convert.ToBoolean((ip(p) <> 0)))
            If ip(p) <> 46 Then
                s += Convert.ToString(ip(p) - 48) '- 48
                p += 1
            Else
                ct += 1
                p += 1
                s += "."
            End If

        End While
        Return s
    End Function
End Class