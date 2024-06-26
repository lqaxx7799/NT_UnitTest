﻿using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Nutrition.Library;

public class BaseEntity
{
    [Key]
    [JsonProperty(PropertyName = "id")]
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? ModifiedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? ModifiedBy { get; set; }
    public Guid? DeletedBy { get; set; }
    public bool IsDeleted { get; set; }
}

