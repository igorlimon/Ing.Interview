using System;

namespace Ing.Interview.Domain.Common
{
    public abstract class AuditableEntity
    {
        [System.Text.Json.Serialization.JsonIgnore]
        public DateTime Created { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public string CreatedBy { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public DateTime? LastModified { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public string LastModifiedBy { get; set; }
    }
}
