//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cooking_App.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Receipe
    {
        public int RId { get; set; }
        public string RName { get; set; }
        public string Photo { get; set; }
        public string Youtube { get; set; }
        public string Ingredient { get; set; }
        public string HTM { get; set; }
        public string VNB { get; set; }
        public Nullable<int> RoleId { get; set; }
        public string State { get; set; }
        public Nullable<int> UserId { get; set; }
    
        public virtual Login Login { get; set; }
        public virtual State State1 { get; set; }
    }
}
