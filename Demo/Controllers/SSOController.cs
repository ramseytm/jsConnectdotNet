﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;

namespace jsConnectdotNet.Controllers
{
    public class SSOController : Controller
    {
        public ActionResult Index()
        {
            string responsePayload = string.Empty;
            try
            {
                // 1. Get your client ID and secret here. These must match those in your jsConnect settings.
                string clientID = "123";
                string secret = "123";

                // 2. Grab the current user from your session management system or database here.

                bool signedIn = true; // this is just a placeholder

                // YOUR CODE HERE.

                // 3. Fill in the user information in a way that Vanilla can understand.
                SortedList user = new SortedList();

                if (signedIn)
                {
                    // CHANGE THESE FOUR LINES.
                    user["uniqueid"] = "123";
                    user["name"] = "John ASP.NET";
                    user["email"] = "john.asp.net@example.com";
                    user["photourl"] = "";
                }

                // 4. Generate the jsConnect string.
                bool secure = false; // this should be true unless you are testing
                string hash = "sha256"; // values supported are md5, sha1, sha256
                // Vanilla.jsConnect.Debug = true; // turn on debug to help troubleshoot some issues.
                responsePayload = Vanilla.jsConnect.GetJsConnectString(user, Request.QueryString, clientID, secret, secure, hash);
            }
            catch (System.Threading.ThreadAbortException)
            {
                responsePayload = "{\"error\": \"Unknown error.\"}";
            }
            catch (Exception ex)
            {
                SortedList exCollection = new SortedList();
                exCollection["error"] = "exception";
                exCollection["message"] = ex.Message;

                responsePayload = Vanilla.jsConnect.JsonEncode(exCollection);
            }

            return Content(responsePayload + ";", "application/javascript");
        }
    }
}
