Imports Microsoft.VisualBasic
Imports System
Imports System.Web
Imports System.Drawing
Imports System.Drawing.Imaging

Public Class ImageHandler
	Implements IHttpHandler
	Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
		Dim imgId As String = context.Request("imgId")
		Dim path As String = context.Server.MapPath(String.Format("~/App_Data/{0}.png", imgId))

		context.Response.ContentType = "image/png"

		If System.IO.File.Exists(path) Then
			context.Response.WriteFile(path)
		End If

		context.Response.End()
	End Sub

	Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
		Get
			Return False
		End Get
	End Property
End Class