using System;
using System.Configuration;
using System.Web.Mvc;
using MVCUmbraco.Models;
using System.Net.Mail;
using System.Net;

namespace MVCUmbraco.Controllers
{
    public class ContactController : Umbraco.Web.Mvc.SurfaceController
    {
        [HttpPost]
        public ActionResult SendMail(Contact form)
        {
            string retValue = "There was an error submitting the form, please try again later.";
            if (!ModelState.IsValid)
            {
                return Content(retValue);
            }

            if (ModelState.IsValid)
            {
                //Update your SMTP server credentials
                using (var client = new SmtpClient
                {
                    //Host = "10.31.10.11",
                    Host = ConfigurationManager.AppSettings["SMTP_Server"],

                    //Port = 587,
                    //EnableSsl = true,
                    //Credentials = new NetworkCredential("account@gmail.com", "password"),
                    DeliveryMethod = SmtpDeliveryMethod.Network
                })
                {
                    var mail = new MailMessage();
                    var receivers = ConfigurationManager.AppSettings["Contact_To_Address"].Split(';');
                    foreach (var receiver in receivers)
                    {
                        mail.To.Add(receiver); // Update your email address
                    }
                   
                    mail.From = new MailAddress(form.Email, form.Name);
                    mail.Subject = String.Format("Request to Contact from");
                    mail.Body = form.Message;
                    mail.IsBodyHtml = false;
                    try
                    {
                        client.Send(mail);
                        retValue = "Your Request for Contact was submitted successfully.<br/>We will contact you shortly.";
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
            return Content(retValue);
        }
    }
}