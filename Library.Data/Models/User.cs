﻿using Microsoft.AspNetCore.Identity;

namespace Library.Data.Models;

public class User : IdentityUser<long>, IDateTimeEntity
{
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}