﻿using System.Web;
using System.Web.Mvc;

namespace MVCApp_Area
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
