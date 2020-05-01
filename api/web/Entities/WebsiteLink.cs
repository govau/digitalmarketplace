using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Web.Entities
{
    [Table("website_link")]
    public partial class WebsiteLink
    {
        public WebsiteLink()
        {
            SupplierExtraLinks = new HashSet<SupplierExtraLinks>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("url", TypeName = "character varying")]
        public string Url { get; set; }
        [Required]
        [Column("label", TypeName = "character varying")]
        public string Label { get; set; }

        [InverseProperty("WebsiteLink")]
        public virtual ICollection<SupplierExtraLinks> SupplierExtraLinks { get; set; }
    }
}
