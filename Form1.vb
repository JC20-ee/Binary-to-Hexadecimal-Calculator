Public Class Form1
    Private Sub FormReset(sender As Object, e As EventArgs) Handles Me.Load, btnReset.Click, ResetToolStripMenuItem.Click
        txtInput.Clear()
        txtInput.Focus()
        txtOutput.Clear()
    End Sub
    Private Sub txtInput_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtInput.KeyPress
        If Char.IsControl(e.KeyChar) Then Return

        If Not (Asc(e.KeyChar) = 48 OrElse Asc(e.KeyChar) = 49 OrElse e.KeyChar = "."c) Then e.Handled = True

        If e.KeyChar = "."c AndAlso (txtInput.SelectionStart = 0 OrElse txtInput.Text.Contains(".")) Then e.Handled = True
    End Sub
    Private Sub btnConvert_Click(sender As Object, e As EventArgs) Handles btnConvert.Click
        Try
            Dim binInput As String = txtInput.Text
            Dim hexresult As String

            If binInput = "" Then
                MessageBox.Show("Please enter a number!", "Input Error")
                FormReset(Nothing, Nothing)
                Exit Sub
            End If

            hexresult = BintoHex(binInput)
            txtOutput.Text = "The hexadecimal of the binary " & binInput & " is " & hexresult & "."

            txtInput.Focus()
            txtInput.SelectAll()
        Catch Overflow As System.OverflowException
            MessageBox.Show("The value is too large to process.", "Error")
            FormReset(Nothing, Nothing)
        Catch Other As System.Exception
            MessageBox.Show("Invalid!", "Error")
            FormReset(Nothing, Nothing)
        End Try
    End Sub
    Function BintoHex(ByVal bin As String) As String
        Dim parts() As String = bin.Split(".")
        Dim intPart As String = parts(0)
        Dim remInt As Integer = intPart.Length Mod 4
        Dim fracPart, fourbits As String
        Dim hexInt As String = ""
        Dim hexFrac As String = ""

        If parts.Length > 1 Then
            fracPart = parts(1)
        Else
            fracPart = ""
        End If

        If remInt = 1 Then
            intPart = "000" & intPart
        ElseIf remInt = 2 Then
            intPart = "00" & intPart
        ElseIf remInt = 3 Then
            intPart = "0" & intPart
        End If

        For i As Integer = 0 To intPart.Length Step 4
            fourBits = Mid(intPart, i + 1, 4)
            hexInt &= BinToHexDigit(fourBits)
        Next

        If fracPart <> "" Then
            Dim remFrac As Integer = fracPart.Length Mod 4
            If remFrac = 1 Then
                fracPart = fracPart & "000"
            ElseIf remFrac = 2 Then
                fracPart = fracPart & "00"
            ElseIf remFrac = 3 Then
                fracPart = fracPart & "0"
            End If
        End If

        If fracPart <> "" Then
            For i = 0 To fracPart.Length - 1 Step 4
                fourbits = Mid(fracPart, i + 1, 4)
                hexFrac &= BinToHexDigit(fourBits)
            Next
        End If

        If hexFrac <> "" Then
            Return hexInt & "." & hexFrac
        Else
            Return hexInt
        End If
    End Function
    Function BinToHexDigit(ByVal fourBits As String) As String
        Dim hexDigit As String = ""

        Select Case fourBits
            Case "0000"
                hexDigit = "0"
            Case "0001"
                hexDigit = "1"
            Case "0010"
                hexDigit = "2"
            Case "0011"
                hexDigit = "3"
            Case "0100"
                hexDigit = "4"
            Case "0101"
                hexDigit = "5"
            Case "0110"
                hexDigit = "6"
            Case "0111"
                hexDigit = "7"
            Case "1000"
                hexDigit = "8"
            Case "1001"
                hexDigit = "9"
            Case "1010"
                hexDigit = "A"
            Case "1011"
                hexDigit = "B"
            Case "1100"
                hexDigit = "C"
            Case "1101"
                hexDigit = "D"
            Case "1110"
                hexDigit = "E"
            Case "1111"
                hexDigit = "F"
        End Select

        Return hexDigit
    End Function
    Private Sub Form1_Closing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Dim ExitYN = MsgBox("Are you sure?", MsgBoxStyle.YesNo, "Close?")

        If ExitYN = MsgBoxResult.No Then e.Cancel = True
    End Sub
    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        Dim frmAbout As New Form2

        frmAbout.ShowDialog()
    End Sub
End Class