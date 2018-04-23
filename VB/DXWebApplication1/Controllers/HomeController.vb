﻿Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports DevExpress.Web.Mvc
Imports Models

Namespace GridViewBatchEdit.Controllers
    Public Class HomeController
        Inherits Controller
        Public Function Index() As ActionResult
            Return View()
        End Function

        <ValidateInput(False)> _
        Public Function GridViewPartial() As ActionResult
            Return PartialView("_GridViewPartial", BatchEditRepository.GridData)
        End Function

        <HttpPost, ValidateInput(False)> _
        Public Function BatchUpdatePartial(ByVal batchValues As MVCxGridViewBatchUpdateValues(Of GridDataItem, Integer)) As ActionResult
            For Each item In batchValues.Insert
                If batchValues.IsValid(item) Then
                    BatchEditRepository.InsertNewItem(item, batchValues)
                Else
                    batchValues.SetErrorText(item, "Correct validation errors")
                End If
            Next item
            For Each item In batchValues.Update
                If batchValues.IsValid(item) Then
                    BatchEditRepository.UpdateItem(item, batchValues)
                Else
                    batchValues.SetErrorText(item, "Correct validation errors")
                End If
            Next item
            For Each itemKey In batchValues.DeleteKeys
                BatchEditRepository.DeleteItem(itemKey, batchValues)
            Next itemKey
            Return PartialView("_GridViewPartial", BatchEditRepository.GridData)
        End Function
        Public Function GridViewCustomActionPartial(ByVal key As String) As ActionResult
            Session("Mode") = key
            Return PartialView("_GridViewPartial", BatchEditRepository.GridData)
        End Function
    End Class
End Namespace