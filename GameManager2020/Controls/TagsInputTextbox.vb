'* Copyright (C) Zettabyte Technology - All Rights Reserved
'* Unauthorized copying of this file, via any medium Is strictly prohibited
'* Proprietary And confidential
'* Written by Bartholomew Ho <qiangqiang101@hotmail.com>, February 2019

Imports System.ComponentModel
Imports System.Drawing.Drawing2D

Public Class TagsInputTextbox
    Inherits FlowLayoutPanel

    Private WithEvents _TB As New TextBox
    Private ReadOnly _itemList As New List(Of TagsInputItem)
    Private _defaultHeight As Integer = 23

    Public Sub New()
        Controls.Add(_TB)
        _TB.Margin = New Padding(0)
        _TB.BorderStyle = BorderStyle.None
        _TB.Size = New Size(Width - 4, Height - 4)
        _TB.TabIndex = 99
        BackColor = SystemColors.Window
        BorderStyle = BorderStyle.FixedSingle
    End Sub

    Public Overrides Property Text() As String
        Get
            Return _TB.Text
        End Get
        Set(value As String)
            _TB.Text = value
        End Set
    End Property

    Public Property TextBoxBackColor() As Color
        Get
            Return _TB.BackColor
        End Get
        Set(value As Color)
            _TB.BackColor = value
        End Set
    End Property

    Public Overrides Property ForeColor() As Color
        Get
            Return _TB.ForeColor
        End Get
        Set(value As Color)
            _TB.ForeColor = value
        End Set
    End Property

    Private _tbfont As Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)

    Public Overrides Property Font() As Font
        Get
            Return _tbfont
        End Get
        Set(value As Font)
            _tbfont = value
        End Set
    End Property

    Private _max As Integer = 5
    <Description("The Maximum Amount of Items to be added into.")>
    Public Property MaximumItem() As Integer
        Get
            Return _max
        End Get
        Set(value As Integer)
            _max = value
        End Set
    End Property

    Private _random As Boolean = False
    Public Property Randomcolor() As Boolean
        Get
            Return _random
        End Get
        Set(value As Boolean)
            _random = value
        End Set
    End Property

    Public Property AutoCompleteCustomSource() As AutoCompleteStringCollection
        Get
            Return _TB.AutoCompleteCustomSource
        End Get
        Set(value As AutoCompleteStringCollection)
            _TB.AutoCompleteCustomSource = value
        End Set
    End Property

    Private _autoCompletesource As AutoCompleteSource = AutoCompleteSource.CustomSource
    Public Property AutoCompleteSource() As AutoCompleteSource
        Get
            Return _autoCompletesource
        End Get
        Set(value As AutoCompleteSource)
            _autoCompletesource = value
            _TB.AutoCompleteSource = value
        End Set
    End Property

    Private _autoCompleteMode As AutoCompleteMode = AutoCompleteMode.SuggestAppend
    Public Property AutoCompleteMode() As AutoCompleteMode
        Get
            Return _TB.AutoCompleteMode
        End Get
        Set(value As AutoCompleteMode)
            _autoCompleteMode = value
            _TB.AutoCompleteMode = value
        End Set
    End Property

    Private _multiline As Boolean = False
    Public Property MultiLine() As Boolean
        Get
            Return _multiline
        End Get
        Set(value As Boolean)
            _multiline = value
        End Set
    End Property

    Public ReadOnly Property Items() As List(Of TagsInputItem)
        Get
            Return _itemList
        End Get
    End Property

    Private _addkey As Char = ","
    Public Property AddItemKey() As Char
        Get
            Return _addkey
        End Get
        Set(value As Char)
            _addkey = value
        End Set
    End Property

    Private _backcolor As Color = SystemColors.InactiveCaption

    Public Property ItemBasecolor() As Color
        Get
            Return _backcolor
        End Get
        Set(value As Color)
            _backcolor = value
        End Set
    End Property

    Public Sub Add(tbpi As TagsInputItem)
        _itemList.Add(tbpi)
    End Sub

    Public Sub Add(text As String)
        Dim newLabel As New TagsInputItem()
        With newLabel
            .Text = text
            .AutoSize = False
            .WidthAutoSize = True
            If _multiline Then
                .Height = 18
            Else
                .Height = Height - 2
            End If
            .TextAlign = ContentAlignment.MiddleLeft
            .Margin = New Padding(0)
            .Basecolor = _backcolor
            If _random Then .Randomcolor = True
            AddHandler newLabel.MouseClick, AddressOf OnLabelItemClicked
        End With
        _itemList.Add(newLabel)
        Controls.Add(newLabel)
        Dim lbIndex As Integer = Controls.Count
        Controls.SetChildIndex(_TB, lbIndex)
        If _itemList.Count >= _max Then
            _TB.Enabled = False
            _TB.Hide()
        End If
        If _multiline Then

        Else
            _TB.Width = _TB.Width - newLabel.Width
        End If
    End Sub

    Public Sub AddRange(items As List(Of String))
        For Each item In items
            Add(item)
        Next
    End Sub

    Public Sub AddRange(items As String())
        For Each item In items
            Add(item)
        Next
    End Sub

    Public Overrides Function ToString() As String
        Dim result As String = Nothing
        For Each item In _itemList
            item.Text = item.Text.Trim()
            result &= $"{item.Text}, "
        Next
        Return result.Remove(result.Length - 2, 2)
    End Function

    Public Function Val() As String()
        Dim result(_itemList.Count) As String
        For i As Integer = 0 To _itemList.Count
            result(i) = _itemList(i).Text
        Next
        Return result
    End Function

    Public Function ToList() As List(Of TagsInputItem)
        Return _itemList
    End Function

    Public Sub Remove(tbpi As TagsInputItem)
        _itemList.Remove(tbpi)
    End Sub

    Public Sub Remove(index As Integer)
        _itemList.RemoveAt(index)
    End Sub

    Public Sub Clear()
        For Each item In _itemList
            Controls.Remove(item)

            If _multiline Then

            Else
                _TB.Width = item.Width + _TB.Width
            End If

            If _itemList.Count <= _max Then
                _TB.Enabled = True
                _TB.Show()
            End If
        Next
        _itemList.Clear()
    End Sub

    Public Sub ClearText()
        _TB.Clear()
    End Sub

    Private Sub _TB_TextChanged(sender As Object, e As EventArgs) Handles _TB.TextChanged
        If _TB.Text.Contains(_addkey) Then
            Dim item As String = _TB.Text.Replace(_addkey, "")
            Dim newLabel As New TagsInputItem()
            With newLabel
                .Text = item
                .AutoSize = False
                .WidthAutoSize = True
                If _multiline Then
                    .Height = 18
                Else
                    .Height = Height - 2
                End If
                .TextAlign = ContentAlignment.MiddleLeft
                .Margin = New Padding(0)
                .Basecolor = _backcolor
                If _random Then .Randomcolor = True
                AddHandler newLabel.MouseClick, AddressOf OnLabelItemClicked
            End With
            _itemList.Add(newLabel)
            Controls.Add(newLabel)
            Dim lbIndex As Integer = Controls.Count
            Controls.SetChildIndex(_TB, lbIndex)
            If _itemList.Count >= _max Then
                _TB.Enabled = False
                _TB.Hide()
            End If
            If _multiline Then

            Else
                _TB.Width = _TB.Width - newLabel.Width
            End If
            _TB.Clear()
        End If
    End Sub

    Private Sub OnLabelItemClicked(sender As TagsInputItem, e As MouseEventArgs)
        _itemList.Remove(sender)
        If _multiline Then

        Else
            _TB.Width = sender.Width + _TB.Width
        End If

        If _itemList.Count <= _max Then
            _TB.Enabled = True
            _TB.Show()
        End If
    End Sub

    Protected Overrides Sub OnSizeChanged(e As EventArgs)
        MyBase.OnSizeChanged(e)

        _TB.Width = Width - 4
        If Not _multiline Then Height = _defaultHeight
    End Sub

    Protected Overrides Sub SetBoundsCore(x As Integer, y As Integer, width As Integer, height As Integer, specified As BoundsSpecified)
        MyBase.SetBoundsCore(x, y, width, height, specified)

        If (specified And BoundsSpecified.Height) = 0 OrElse height = _defaultHeight Then
            If Not _multiline Then MyBase.SetBoundsCore(x, y, width, _defaultHeight, specified)
        Else
            Return
        End If
    End Sub

End Class

Public Class TagsInputItem
    Inherits Label

    Private _colorSet As Boolean = False

    Private _autosize As Boolean = True
    Public Property WidthAutoSize() As Boolean
        Get
            Return _autosize
        End Get
        Set(value As Boolean)
            _autosize = value
        End Set
    End Property

    Private _backcolor As Color = SystemColors.InactiveCaption

    Public Property Basecolor() As Color
        Get
            Return _backcolor
        End Get
        Set(value As Color)
            _backcolor = value
        End Set
    End Property

    Private _random As Boolean = False
    Public Property Randomcolor() As Boolean
        Get
            Return _random
        End Get
        Set(value As Boolean)
            _random = value
        End Set
    End Property

    Public Sub New()
        AutoSize = False
        BackColor = Color.Transparent
    End Sub

    Private _mouse As Boolean = False
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        If _random AndAlso Not _colorSet Then
            Dim rd As New Random()
            Dim R = rd.Next(0, 255)
            Dim G = rd.Next(0, 255)
            Dim B = rd.Next(0, 255)
            _backcolor = Color.FromArgb(R, G, B)
            _colorSet = True
        End If
        DrawRoundedRectangle(e.Graphics, ClientRectangle, 10, New Pen(_backcolor))
        FillRoundedRectangle(e.Graphics, ClientRectangle, 10, New SolidBrush(_backcolor))

        Dim format As StringFormat = StringFormat.GenericDefault
        format.Alignment = StringAlignment.Center
        format.Trimming = StringTrimming.EllipsisWord
        e.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
        If _random Then
            If IsDark(_backcolor) Then
                e.Graphics.DrawString(Text, Font, Brushes.White, New Rectangle(-3, 4, Width - 3, Height - 3), format)
            Else
                e.Graphics.DrawString(Text, Font, New SolidBrush(ForeColor), New Rectangle(-3, 4, Width - 3, Height - 3), format)
            End If
        Else
            e.Graphics.DrawString(Text, Font, New SolidBrush(ForeColor), New Rectangle(-3, 4, Width - 3, Height - 3), format)
        End If
        If _mouse Then
            If _random Then
                If IsDark(_backcolor) Then
                    e.Graphics.FillRectangle(Brushes.White, New Rectangle(Width - 13, 4, 10, 10))
                    e.Graphics.DrawString("r", New Font("Marlett", 8.0F, FontStyle.Regular), Brushes.Red, New Rectangle(Width - 13, 4, 10, 10), format)
                Else
                    e.Graphics.FillRectangle(Brushes.Red, New Rectangle(Width - 13, 4, 10, 10))
                    e.Graphics.DrawString("r", New Font("Marlett", 8.0F, FontStyle.Regular), Brushes.White, New Rectangle(Width - 13, 4, 10, 10), format)
                End If
            Else
                e.Graphics.FillRectangle(Brushes.Red, New Rectangle(Width - 13, 4, 10, 10))
                e.Graphics.DrawString("r", New Font("Marlett", 8.0F, FontStyle.Regular), Brushes.White, New Rectangle(Width - 13, 4, 10, 10), format)
            End If
        Else
            If _random Then
                If IsDark(_backcolor) Then
                    e.Graphics.DrawString("r", New Font("Marlett", 8.0F, FontStyle.Regular), Brushes.White, New Rectangle(Width - 13, 4, 10, 10), format)
                Else
                    e.Graphics.DrawString("r", New Font("Marlett", 8.0F, FontStyle.Regular), Brushes.Red, New Rectangle(Width - 13, 4, 10, 10), format)
                End If
            Else
                e.Graphics.DrawString("r", New Font("Marlett", 8.0F, FontStyle.Regular), Brushes.Red, New Rectangle(Width - 13, 4, 10, 10), format)
            End If
        End If
    End Sub

    Protected Overrides Sub OnTextChanged(e As EventArgs)
        MyBase.OnTextChanged(e)

        If _autosize Then
            Dim size As Size = TextRenderer.MeasureText(Text, Font)
            Width = size.Width + 30
        End If
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)

        If _autosize Then
            Dim size As Size = TextRenderer.MeasureText(Text, Font)
            Width = size.Width + 30
        End If
    End Sub

    Protected Overrides Sub OnMouseClick(e As MouseEventArgs)
        MyBase.OnMouseClick(e)

        Parent.Controls.Remove(Me)
    End Sub

    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        MyBase.OnMouseEnter(e)

        _mouse = True
        Cursor = Cursors.Hand
        Refresh()
    End Sub

    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)

        _mouse = False
        Cursor = Cursor.Current
        Refresh()
    End Sub

    Private Sub DrawRoundedRectangle(ByVal g As Graphics, ByVal r As Rectangle, ByVal d As Integer, ByVal p As Pen)
        g.DrawArc(p, r.X, r.Y, d, d, 180, 90)
        g.DrawLine(p, CInt(r.X + d / 2), r.Y, CInt(r.X + r.Width - d / 2), r.Y)
        g.DrawArc(p, r.X + r.Width - d, r.Y, d, d, 270, 90)
        g.DrawLine(p, r.X, CInt(r.Y + d / 2), r.X, CInt(r.Y + r.Height - d / 2))
        g.DrawLine(p, CInt(r.X + r.Width), CInt(r.Y + d / 2), CInt(r.X + r.Width), CInt(r.Y + r.Height - d / 2))
        g.DrawLine(p, CInt(r.X + d / 2), CInt(r.Y + r.Height), CInt(r.X + r.Width - d / 2), CInt(r.Y + r.Height))
        g.DrawArc(p, r.X, r.Y + r.Height - d, d, d, 90, 90)
        g.DrawArc(p, r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90)
    End Sub

    Private Sub FillRoundedRectangle(ByVal g As Graphics, ByVal r As Rectangle, ByVal d As Integer, ByVal b As Brush)
        Dim mode As SmoothingMode = g.SmoothingMode
        g.SmoothingMode = SmoothingMode.HighSpeed
        g.FillPie(b, r.X, r.Y, d, d, 180, 90)
        g.FillPie(b, r.X + r.Width - d, r.Y, d, d, 270, 90)
        g.FillPie(b, r.X, r.Y + r.Height - d, d, d, 90, 90)
        g.FillPie(b, r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90)
        g.FillRectangle(b, CInt(r.X + d / 2), r.Y, r.Width - d, CInt(d / 2))
        g.FillRectangle(b, r.X, CInt(r.Y + d / 2), r.Width, CInt(r.Height - d))
        g.FillRectangle(b, CInt(r.X + d / 2), CInt(r.Y + r.Height - d / 2), CInt(r.Width - d), CInt(d / 2))
        g.SmoothingMode = mode
    End Sub

    Private Function IsDark(color As Color) As Boolean
        Dim result As Boolean = (color.R <= 128) Or (color.G <= 128) Or (color.B <= 128)
        If result Then
            Return True
        Else
            Return False
        End If
    End Function

End Class
