//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GreatPizza.Server.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PizzaTopings
    {
        public int Id { get; set; }
    
        public virtual Pizza Pizza { get; set; }
        public virtual Toping Toping { get; set; }
    }
}
