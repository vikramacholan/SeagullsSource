//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Seagulls
{
    using System;
    using System.Collections.Generic;
    
    public partial class Venu
    {
        public Venu()
        {
            this.Matches = new HashSet<Match>();
        }
    
        public int Id { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
    
        public virtual ICollection<Match> Matches { get; set; }
    }
}
