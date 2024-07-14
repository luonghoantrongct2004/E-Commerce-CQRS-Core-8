﻿using E.Domain.Entities.Users;

namespace E.Domain.Entities.Token;

public class RefreshToken
{
    public int Id { get; set; }
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }

    public Guid UserId { get; set; }
    public DomainUser User { get; set; }
}