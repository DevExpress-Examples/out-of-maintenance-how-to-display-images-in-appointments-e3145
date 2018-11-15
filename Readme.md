<!-- default file list -->
*Files to look at*:

* [CustomEvents.cs](./CS/WebSite/App_Code/CustomEvents.cs) (VB: [CustomEvents.vb](./VB/WebSite/App_Code/CustomEvents.vb))
* [Helpers.cs](./CS/WebSite/App_Code/Helpers.cs) (VB: [Helpers.vb](./VB/WebSite/App_Code/Helpers.vb))
* [ImageHandler.ashx.cs](./CS/WebSite/App_Code/ImageHandler.ashx.cs) (VB: [ImageHandler.ashx.vb](./VB/WebSite/App_Code/ImageHandler.ashx.vb))
* [Default.aspx](./CS/WebSite/Default.aspx) (VB: [Default.aspx](./VB/WebSite/Default.aspx))
* [Default.aspx.cs](./CS/WebSite/Default.aspx.cs) (VB: [Default.aspx](./VB/WebSite/Default.aspx))
<!-- default file list end -->
# How to display images in appointments


<p>This example illustrates how to display images in appointments. This approach is based on handling the <a href="http://documentation.devexpress.com/#AspNet/DevExpressWebASPxSchedulerASPxScheduler_InitAppointmentImagestopic"><u>ASPxScheduler.InitAppointmentImages Event</u></a>. Note that in this event handler, the URL specified in the <strong>ImageProperties</strong> constructor is not static. Instead, it points to the HTTP Handler, which makes it possible to provide custom image retrieving logic (e. g. you can obtain images from a database). Also, the images in this example can be changed at runtime by changing an appointment label (see <a href="http://documentation.devexpress.com/#AspNet/CustomDocument3811"><u>Appointment Labels and Statuses</u></a>) via the appointment context menu (to activate this menu, click the right mouse button when the appointment is located under the mouse cursor).</p><p>Note that a more powerful <a href="http://demos.devexpress.com/ASPxSchedulerDemos/Templates/AppointmentTemplate.aspx"><u>Templates - Appointment Template</u></a> customization technique can be used if you want to completely customize the appointment appearance and content.</p><p><strong>See also:</strong><br />
<a href="http://demos.devexpress.com/ASPxSchedulerDemos/Customization/CustomAppointments.aspx"><u>Customization - Custom Appointments</u></a></p>

<br/>


