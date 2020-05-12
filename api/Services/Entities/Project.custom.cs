﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Services.Entities {
    public enum ProjectStatus {
        draft,
        published
    }
    public partial class Project {
        [Required]
        [Column("status", TypeName = "project_status_enum")]
        public ProjectStatus Status { get; set; }
    }
}