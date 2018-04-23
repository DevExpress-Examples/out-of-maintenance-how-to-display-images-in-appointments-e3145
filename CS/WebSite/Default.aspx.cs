using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.XtraScheduler;
using DevExpress.Web.ASPxScheduler;
using DevExpress.Web;
using DevExpress.Web.ASPxScheduler.Drawing;

public partial class Default : System.Web.UI.Page
{
    ASPxSchedulerStorage Storage { get { return ASPxScheduler1.Storage; } }

    protected void Page_Load(object sender, EventArgs e) {
        SetupMappings();
        ResourceFiller.FillResources(this.ASPxScheduler1.Storage, 3);

        ASPxScheduler1.AppointmentDataSource = appointmentDataSource;
        ASPxScheduler1.DataBind();

        ASPxScheduler1.GroupType = SchedulerGroupType.Resource;
    }

    protected void ASPxScheduler1_InitAppointmentImages(object sender, AppointmentImagesEventArgs e) {
        ImageProperties imageProperties = new ImageProperties("~/ImageHandler.ashx?imgId=" + e.Appointment.LabelKey.ToString());

        imageProperties.Width = 24;
        imageProperties.Height = 24;

        AppointmentImageInfo info = new AppointmentImageInfo();
        info.ImageProperties = imageProperties;

        e.ImageInfoList.Add(info);
    }

    void SetupMappings() {
        ASPxAppointmentMappingInfo mappings = Storage.Appointments.Mappings;
        Storage.BeginUpdate();
        try {
            mappings.AppointmentId = "Id";
            mappings.Start = "StartTime";
            mappings.End = "EndTime";
            mappings.Subject = "Subject";
            mappings.AllDay = "AllDay";
            mappings.Description = "Description";
            mappings.Label = "Label";
            mappings.Location = "Location";
            mappings.RecurrenceInfo = "RecurrenceInfo";
            mappings.ReminderInfo = "ReminderInfo";
            mappings.ResourceId = "OwnerId";
            mappings.Status = "Status";
            mappings.Type = "EventType";
        }
        finally {
            Storage.EndUpdate();
        }
    }
    //Initilazing ObjectDataSource
    protected void appointmentsDataSource_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
        e.ObjectInstance = new CustomEventDataSource(GetCustomEvents());
    }
    CustomEventList GetCustomEvents() {
        CustomEventList events = Session["ListBoundModeObjects"] as CustomEventList;
        if (events == null) {
            events = new CustomEventList();
            
            CustomEvent  customEvent1 = new CustomEvent();

            customEvent1.Id = customEvent1.GetHashCode();
            customEvent1.StartTime = DateTime.Today.AddHours(1);
            customEvent1.EndTime = DateTime.Today.AddHours(2);
            customEvent1.Subject = "Test event";
            customEvent1.OwnerId = "sbrighton";
            customEvent1.Label = 0;

            events.Add(customEvent1);

            CustomEvent customEvent2 = new CustomEvent();

            customEvent2.Id = customEvent2.GetHashCode();
            customEvent2.StartTime = DateTime.Today.AddHours(4);
            customEvent2.EndTime = DateTime.Today.AddHours(5);
            customEvent2.Subject = "Test event 2";
            customEvent2.OwnerId = "rfischer";
            customEvent2.Label = 1;

            events.Add(customEvent2);

            Session["ListBoundModeObjects"] = events;
        }
        return events;
    }
    // User generated appointment id    
    protected void ASPxScheduler1_AppointmentInserting(object sender, PersistentObjectCancelEventArgs e) {
        SetAppointmentId(sender, e);
    }
    void SetAppointmentId(object sender, PersistentObjectCancelEventArgs e) {
        ASPxSchedulerStorage storage = (ASPxSchedulerStorage)sender;
        Appointment apt = (Appointment)e.Object;
        storage.SetAppointmentId(apt, apt.GetHashCode());
    }
}
