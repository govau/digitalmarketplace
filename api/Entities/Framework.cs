using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Entities
{
    [Table("framework")]
    public partial class Framework
    {
        public Framework()
        {
            ArchivedService = new HashSet<ArchivedService>();
            Brief = new HashSet<Brief>();
            DraftService = new HashSet<DraftService>();
            FrameworkLot = new HashSet<FrameworkLot>();
            Service = new HashSet<Service>();
            SupplierFramework = new HashSet<SupplierFramework>();
            UserFramework = new HashSet<UserFramework>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("slug", TypeName = "character varying")]
        public string Slug { get; set; }
        [Required]
        [Column("name", TypeName = "character varying")]
        public string Name { get; set; }
        [Required]
        [Column("framework", TypeName = "character varying")]
        public string Framework1 { get; set; }
        [Required]
        [Column("status", TypeName = "character varying")]
        public string Status { get; set; }
        [Column("clarification_questions_open")]
        public bool ClarificationQuestionsOpen { get; set; }
        [Column("framework_agreement_details", TypeName = "json")]
        public string FrameworkAgreementDetails { get; set; }

        [InverseProperty("Framework")]
        public virtual ICollection<ArchivedService> ArchivedService { get; set; }
        [InverseProperty("Framework")]
        public virtual ICollection<Brief> Brief { get; set; }
        [InverseProperty("Framework")]
        public virtual ICollection<DraftService> DraftService { get; set; }
        [InverseProperty("Framework")]
        public virtual ICollection<FrameworkLot> FrameworkLot { get; set; }
        [InverseProperty("Framework")]
        public virtual ICollection<Service> Service { get; set; }
        [InverseProperty("Framework")]
        public virtual ICollection<SupplierFramework> SupplierFramework { get; set; }
        [InverseProperty("Framework")]
        public virtual ICollection<UserFramework> UserFramework { get; set; }
    }
}
