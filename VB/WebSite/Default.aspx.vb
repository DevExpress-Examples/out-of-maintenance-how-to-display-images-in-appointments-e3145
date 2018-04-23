Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports DevExpress.XtraScheduler
Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.Web
Imports DevExpress.Web.ASPxScheduler.Drawing

Partial Public Class [Default]
    Inherits System.Web.UI.Page

    Private ReadOnly Property Storage() As ASPxSchedulerStorage
        Get
            Return ASPxScheduler1.Storage
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        SetupMappings()
        ResourceFiller.FillResources(Me.ASPxScheduler1.Storage, 3)

        ASPxScheduler1.AppointmentDataSource = appointmentDataSource
        ASPxScheduler1.DataBind()

        ASPxScheduler1.GroupType = SchedulerGroupType.Resource
    End Sub

    Protected Sub ASPxScheduler1_InitAppointmentImages(ByVal sender As Object, ByVal e As AppointmentImagesEventArgs)
        Dim imageProperties As New ImageProperties("~/ImageHandler.ashx?imgId=" & e.Appointment.LabelKey.ToString())

        imageProperties.Width = 24
        imageProperties.Height = 24

        Dim info As New AppointmentImageInfo()
        info.ImageProperties = imageProperties

        e.ImageInfoList.Add(info)
    End Sub

    Private Sub SetupMappings()
        Dim mappings As ASPxAppointmentMappingInfo = Storage.Appointments.Mappings
        Storage.BeginUpdate()
        Try
            mappings.AppointmentId = "Id"
            mappings.Start = "StartTime"
            mappings.End = "EndTime"
            mappings.Subject = "Subject"
            mappings.AllDay = "AllDay"
            mappings.Description = "Description"
            mappings.Label = "Label"
            mappings.Location = "Location"
            mappings.RecurrenceInfo = "RecurrenceInfo"
            mappings.ReminderInfo = "ReminderInfo"
            mappings.ResourceId = "OwnerId"
            mappings.Status = "Status"
            mappings.Type = "EventType"
        Finally
            Storage.EndUpdate()
        End Try
    End Sub
    'Initilazing ObjectDataSource
    Protected Sub appointmentsDataSource_ObjectCreated(ByVal sender As Object, ByVal e As ObjectDataSourceEventArgs)
        e.ObjectInstance = New CustomEventDataSource(GetCustomEvents())
    End Sub
    Private Function GetCustomEvents() As CustomEventList

        Dim events_Renamed As CustomEventList = TryCast(Session("ListBoundModeObjects"), CustomEventList)
        If events_Renamed Is Nothing Then
            events_Renamed = New CustomEventList()

            Dim customEvent1 As New CustomEvent()

            customEvent1.Id = customEvent1.GetHashCode()
            customEvent1.StartTime = Date.Today.AddHours(1)
            customEvent1.EndTime = Date.Today.AddHours(2)
            customEvent1.Subject = "Test event"
            customEvent1.OwnerId = "sbrighton"
            customEvent1.Label = 0

            events_Renamed.Add(customEvent1)

            Dim customEvent2 As New CustomEvent()

            customEvent2.Id = customEvent2.GetHashCode()
            customEvent2.StartTime = Date.Today.AddHours(4)
            customEvent2.EndTime = Date.Today.AddHours(5)
            customEvent2.Subject = "Test event 2"
            customEvent2.OwnerId = "rfischer"
            customEvent2.Label = 1

            events_Renamed.Add(customEvent2)

            Session("ListBoundModeObjects") = events_Renamed
        End If
        Return events_Renamed
    End Function
    ' User generated appointment id    
    Protected Sub ASPxScheduler1_AppointmentInserting(ByVal sender As Object, ByVal e As PersistentObjectCancelEventArgs)
        SetAppointmentId(sender, e)
    End Sub
    Private Sub SetAppointmentId(ByVal sender As Object, ByVal e As PersistentObjectCancelEventArgs)

        Dim storage_Renamed As ASPxSchedulerStorage = DirectCast(sender, ASPxSchedulerStorage)
        Dim apt As Appointment = CType(e.Object, Appointment)
        storage_Renamed.SetAppointmentId(apt, apt.GetHashCode())
    End Sub
End Class
