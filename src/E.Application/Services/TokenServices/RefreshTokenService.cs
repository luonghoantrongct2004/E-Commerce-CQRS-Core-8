using E.DAL.UoW;
using E.Domain.Entities.Token;
using E.Domain.Entities.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace E.Application.Services.TokenServices;

public class RefreshTokenService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<DomainUser> _userManager;
    private readonly AppDbContext _appDbContext;
    private readonly IUnitOfWork _unitOfWork;

    public RefreshTokenService(IHttpContextAccessor httpContextAccessor,
        UserManager<DomainUser> userManager, AppDbContext appDbContext, IUnitOfWork unitOfWork)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _appDbContext = appDbContext;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> CreateRefreshTokenAsync(DomainUser user)
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            var token = Convert.ToBase64String(randomNumber);
            var refeshToken = new RefreshToken
            {
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddHours(24),
                UserId = user.Id
            };

            _appDbContext.Tokens.Add(refeshToken);
            await _unitOfWork.CompleteAsync();
            return token;
        }
    }

    public async Task<string> GetRefreshTokenAsync(DomainUser user)
    {
        var refreshToken = await _appDbContext.Tokens
            .Where(r => r.UserId == user.Id && r.ExpiresAt > DateTime.Now)
            .Select(r => r.Token).FirstOrDefaultAsync();
        return refreshToken;
    }

    public async Task RevokeRefreshTokenAsync(string token)
    {
        var refreshToken = await _appDbContext.Tokens
            .SingleOrDefaultAsync(rt => rt.Token == token);
        if (refreshToken != null)
        {
            _appDbContext.Tokens.Remove(refreshToken);
            await _unitOfWork.CompleteAsync();
        }
    }
}