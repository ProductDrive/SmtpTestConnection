using Microsoft.AspNetCore.Mvc;
using SmtpTestConnection.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PD.EmailSender.Helpers;
using Abp.Web.Mvc.Models;

namespace SmtpTestConnection.Web.Controllers
{
    public class ConnectionController : SmtpTestConnectionControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(ConnectionModel model)
        {
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(ConnectionModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password))
            {
                ViewBag.FailedValidation = "true";
                return View();
            }

            try
            {
                if (string.IsNullOrWhiteSpace(model.Domain))
                {
                    var res = await SendMail.AuthenticateSenderDomain(model.Email, model.Password);
                    if (res.IsAuthenticated)
                    {
                        return RedirectToAction(
                            "Details",
                            new ConnectionModel
                            {
                                IsAuth = res.IsAuthenticated,
                                Domain = res.Settings.Domain,
                                Email = res.Settings.Email,
                                Password = res.Settings.Password,
                                Port = res.Settings.Port.ToString()
                            });
                    }
                    else
                    {
                        return RedirectToAction("Details", model);
                    }
                }
                else
                {
                    var isAuth = await SendMail.AuthenticateSenderDomain(model.Email, model.Password, model.Domain, Convert.ToInt32(model.Port));
                    if (isAuth.IsAuthenticated)
                    {
                        return RedirectToAction(
                        "Details",
                        new ConnectionModel
                        {
                            IsAuth = isAuth.IsAuthenticated,
                            Domain = isAuth.Settings?.Domain ?? model.Domain,
                            Email = isAuth.Settings?.Email ?? model.Email,
                            Password = isAuth.Settings?.Password ?? model.Password,
                            Port = isAuth.Settings?.Port.ToString() ?? model.Port.ToString()
                        });
                    }
                    else
                    {
                        return RedirectToAction("Details", model);
                    }
                }
                
            }
            catch
            {
                return View(model);
            }
        }

    }
}
