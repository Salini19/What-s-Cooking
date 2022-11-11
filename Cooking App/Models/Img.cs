using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace Cooking_App.Models
{
    public class Img
    {
        public string RName { get; set; }
        public WebImage Photo { get; set; }
        public string Youtube { get; set; }
        public string Ingredient { get; set; }
        public string HTM { get; set; }
    }
}