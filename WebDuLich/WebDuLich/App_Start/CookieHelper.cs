using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace WebDuLich.App_Start
{
    public static class CookieHelper
    {
        public static void Create(String name, string value, DateTime expire)
        {
            // Create a cookie object
            HttpCookie cookie = new HttpCookie(name);

            // Set the cookie's value
            cookie.Value = value;

            // Set the cookie's expiration date
            cookie.Expires = expire;

            // Add the cookie to the cookie collection
            HttpContext.Current.Response.Cookies.Add(cookie);

        }
        public static string Get(string name)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[name];

            if (cookie != null)
            {
                return cookie.Value;
            }
            return null;
        }

        public static void Delete(string name)
        {
            // Assuming "myCookie" is the name of the cookie you want to delete
            HttpCookie myCookie = new HttpCookie(name);

            // Set the expiration date to a date in the past
            myCookie.Expires = DateTime.Now.AddYears(-1);

            // Add the cookie to the response to ensure it gets sent to the client with the updated expiration date
            HttpContext.Current.Response.Cookies.Add(myCookie);
        }
    }
}