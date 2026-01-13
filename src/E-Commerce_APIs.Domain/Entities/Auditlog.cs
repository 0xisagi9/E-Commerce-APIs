using System;
using System.Collections.Generic;

namespace E_Commerce_APIs.Domain.Entities;

public partial class Auditlog
{
    public int Id { get; set; }

    public string EntityName { get; set; } = null!;

    public string EntityId { get; set; } = null!;

    public string Action { get; set; } = null!;

    public Guid? UserId { get; set; }

    public DateTime? CreatedAt { get; set; }

}
