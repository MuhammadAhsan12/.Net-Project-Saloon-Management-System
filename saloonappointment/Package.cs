//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace saloonappointment
{
    using System;
    using System.Collections.Generic;
    
    public partial class Package
    {
        public int Package_Id { get; set; }
        public string Package_Name { get; set; }
        public string Package_desc { get; set; }
        public Nullable<int> Service_Id { get; set; }
        public Nullable<int> Product_Id { get; set; }
        public Nullable<System.DateTime> Avalaible_From_Date { get; set; }
        public Nullable<System.DateTime> Avalaible_To_Date { get; set; }
    
        public virtual Service Service { get; set; }
        public virtual Package Package1 { get; set; }
        public virtual Package Package2 { get; set; }
    }
}