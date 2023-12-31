//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Asp.NETMVCCRUD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class RaviEmp
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int State_Id { get; set; }
        public int City_ID { get; set; }
        public int Country_Id { get; set; }
        public string Gender { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-dd-MM}", ApplyFormatInEditMode = true)]
        
        public Nullable<System.DateTime> DOB { get; set; }
    
        public virtual RaviCity RaviCity { get; set; }
        public virtual RaviCountry RaviCountry { get; set; }
        public virtual RaviState RaviState { get; set; }
    }
}
