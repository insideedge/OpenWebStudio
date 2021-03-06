'<LICENSE>
'   Open Web Studio - http://www.OpenWebStudio.com
'   Copyright (c) 2007-2008
'   by R2Integrated Inc. http://www.R2integrated.com
'      
'   Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
'   documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
'   the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
'   to permit persons to whom the Software is furnished to do so, subject to the following conditions:
'    
'   The above copyright notice and this permission notice shall be included in all copies or substantial portions of 
'   the Software.
'   
'   This Software and associated documentation files are subject to the terms and conditions of the Open Web Studio 
'   End User License Agreement and version 2 of the GNU General Public License.
'    
'   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
'   TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
'   THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
'   CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
'   DEALINGS IN THE SOFTWARE.
'</LICENSE>
Imports System.Xml.Serialization
Imports System.Collections.Generic
Imports System.Net.Mail
Imports System.Text
Imports r2i.OWS.Framework
Imports r2i.OWS.Framework.Utilities
Imports r2i.OWS.Framework.Utilities.Compatibility
Imports r2i.OWS.Framework.Plugins.Actions
Imports r2i.OWS.Framework.Entities
Imports r2i.OWS.Framework.DataAccess
Imports r2i.OWS.Actions.Utilities

Namespace r2i.OWS.Actions
    Public Class DelayAction
        Inherits ActionBase


#Region "Debugging and Identification: Name,Style,Description"
        Public Overrides Function Description(ByRef act As MessageActionItem) As String
            Dim str As String = ""
            If Not act.Parameters Is Nothing AndAlso act.Parameters.Count > 0 Then
                Dim type As String = Nothing
                Dim number As String = Nothing


                If act.Parameters.ContainsKey(MessageActionsConstants.ACTIONDELAY_TYPE_KEY) AndAlso CStr(act.Parameters(MessageActionsConstants.ACTIONDELAY_TYPE_KEY)).Length > 0 Then
                    type = Utility.GetDictionaryValue(act.Parameters, MessageActionsConstants.ACTIONDELAY_TYPE_KEY, 0)
                End If
                If act.Parameters.ContainsKey(MessageActionsConstants.ACTIONDELAY_VALUE_KEY) AndAlso CStr(act.Parameters(MessageActionsConstants.ACTIONDELAY_VALUE_KEY)).Length > 0 Then
                    number = Utility.GetDictionaryValue(act.Parameters, MessageActionsConstants.ACTIONDELAY_VALUE_KEY, 0)
                End If


                If number > 0 Then
                    If number <> 1 Then
                        str &= " " & number & " " & type & "s"
                    Else
                        str &= " " & number & " " & type
                    End If
                Else
                    str &= " for an unspecified length of time"
                End If
            Else
                str &= " for an unknown length of time"
            End If
            Return str
        End Function
        Public Overrides Function Name() As String
            Return "Delay"
        End Function
        Public Overrides Function Style() As String
            Return ""
        End Function
        Public Overrides Function Title(ByRef act As MessageActionItem) As String
            Return Name()
        End Function
#End Region
        Public Overrides Function Handle_Action(ByRef Caller As RuntimeBase, ByRef sharedds As System.Data.DataSet, ByRef act As MessageActionItem, ByRef previous As Runtime.ActionExecutionResult, ByRef Debugger As Framework.Debugger) As Runtime.ExecutableResult
            Dim str As String = ""
            If Not act.Parameters Is Nothing AndAlso act.Parameters.Count > 0 Then
                Dim type As String = Nothing
                Dim number As String = Nothing


                If act.Parameters.ContainsKey(MessageActionsConstants.ACTIONDELAY_TYPE_KEY) AndAlso CStr(act.Parameters(MessageActionsConstants.ACTIONDELAY_TYPE_KEY)).Length > 0 Then
                    type = Utility.GetDictionaryValue(act.Parameters, MessageActionsConstants.ACTIONDELAY_TYPE_KEY, 0)
                End If
                If act.Parameters.ContainsKey(MessageActionsConstants.ACTIONDELAY_VALUE_KEY) AndAlso CStr(act.Parameters(MessageActionsConstants.ACTIONDELAY_VALUE_KEY)).Length > 0 Then
                    number = Utility.GetDictionaryValue(act.Parameters, MessageActionsConstants.ACTIONDELAY_VALUE_KEY, 0)
                End If


                If number > 0 Then
                    Select Case type.ToLower
                        Case "millisecond"
                            Sleep(System.TimeSpan.FromMilliseconds(number))
                        Case "second"
                            Sleep(System.TimeSpan.FromSeconds(number))
                        Case "minute"
                            Sleep(System.TimeSpan.FromMinutes(number))
                    End Select
                End If
            End If
            Return New Runtime.ExecutableResult(Runtime.ExecutableResultEnum.Executed, Nothing)
        End Function
        Private Sub Sleep(ByVal Length As TimeSpan)
            System.Threading.Thread.Sleep(Length)
        End Sub

        Public Overrides Function Key() As String
            Return "Action-Delay"
        End Function
    End Class
End Namespace