using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Web.Entities
{
    [Table("supplier")]
    public partial class Supplier
    {
        public Supplier()
        {
            Address = new HashSet<Address>();
            Application = new HashSet<Application>();
            ArchivedService = new HashSet<ArchivedService>();
            BriefQuestion = new HashSet<BriefQuestion>();
            BriefResponse = new HashSet<BriefResponse>();
            BriefResponseContact = new HashSet<BriefResponseContact>();
            CaseStudy = new HashSet<CaseStudy>();
            DraftService = new HashSet<DraftService>();
            Evidence = new HashSet<Evidence>();
            PriceSchedule = new HashSet<PriceSchedule>();
            Product = new HashSet<Product>();
            Service = new HashSet<Service>();
            SignedAgreement = new HashSet<SignedAgreement>();
            SupplierContact = new HashSet<SupplierContact>();
            SupplierDomain = new HashSet<SupplierDomain>();
            SupplierExtraLinks = new HashSet<SupplierExtraLinks>();
            SupplierFramework = new HashSet<SupplierFramework>();
            SupplierReference = new HashSet<SupplierReference>();
            SupplierUserInviteLog = new HashSet<SupplierUserInviteLog>();
            User = new HashSet<User>();
            WorkOrder = new HashSet<WorkOrder>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("code")]
        public long Code { get; set; }
        [Required]
        [Column("name", TypeName = "character varying")]
        public string Name { get; set; }
        [Column("summary", TypeName = "character varying")]
        public string Summary { get; set; }
        [Column("description", TypeName = "character varying")]
        public string Description { get; set; }
        [Column("website", TypeName = "character varying")]
        public string Website { get; set; }
        [Column("abn", TypeName = "character varying")]
        public string Abn { get; set; }
        [Column("acn", TypeName = "character varying")]
        public string Acn { get; set; }
        [Column("long_name", TypeName = "character varying")]
        public string LongName { get; set; }
        [Column("creation_time", TypeName = "timestamp with time zone")]
        public DateTime CreationTime { get; set; }
        [Column("last_update_time", TypeName = "timestamp with time zone")]
        public DateTime LastUpdateTime { get; set; }
        [Column("linkedin", TypeName = "character varying")]
        public string Linkedin { get; set; }
        [Column("data", TypeName = "json")]
        public string Data { get; set; }
        [Required]
        [Column("is_recruiter", TypeName = "character varying")]
        public string IsRecruiter { get; set; }

        public virtual ICollection<Address> Address { get; set; }
        public virtual ICollection<Application> Application { get; set; }
        public virtual ICollection<ArchivedService> ArchivedService { get; set; }
        public virtual ICollection<BriefQuestion> BriefQuestion { get; set; }
        public virtual ICollection<BriefResponse> BriefResponse { get; set; }
        public virtual ICollection<BriefResponseContact> BriefResponseContact { get; set; }
        public virtual ICollection<CaseStudy> CaseStudy { get; set; }
        public virtual ICollection<DraftService> DraftService { get; set; }
        public virtual ICollection<Evidence> Evidence { get; set; }
        [InverseProperty("Supplier")]
        public virtual ICollection<PriceSchedule> PriceSchedule { get; set; }
        public virtual ICollection<Product> Product { get; set; }
        public virtual ICollection<Service> Service { get; set; }
        public virtual ICollection<SignedAgreement> SignedAgreement { get; set; }
        [InverseProperty("Supplier")]
        public virtual ICollection<SupplierContact> SupplierContact { get; set; }
        [InverseProperty("Supplier")]
        public virtual ICollection<SupplierDomain> SupplierDomain { get; set; }
        [InverseProperty("Supplier")]
        public virtual ICollection<SupplierExtraLinks> SupplierExtraLinks { get; set; }
        public virtual ICollection<SupplierFramework> SupplierFramework { get; set; }
        [InverseProperty("Supplier")]
        public virtual ICollection<SupplierReference> SupplierReference { get; set; }
        [InverseProperty("Supplier")]
        public virtual ICollection<SupplierUserInviteLog> SupplierUserInviteLog { get; set; }
        public virtual ICollection<User> User { get; set; }
        public virtual ICollection<WorkOrder> WorkOrder { get; set; }
    }
}
