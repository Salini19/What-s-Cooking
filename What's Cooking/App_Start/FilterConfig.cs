﻿using System.Web;
using System.Web.Mvc;

namespace What_s_Cooking
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
