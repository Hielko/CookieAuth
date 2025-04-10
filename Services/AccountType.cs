using Microsoft.AspNetCore.Authorization;

namespace CookieAuth.Services;

public class AccountTypeRequirement : IAuthorizationRequirement
{
    public AccountTypeRequirement(string accountType) =>
        AccountType = accountType;

    public string AccountType { get; }
}


public class AccountTypeHandler : AuthorizationHandler<AccountTypeRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, AccountTypeRequirement requirement)
    {
        var claim = context.User.Claims.FirstOrDefault(c => c.Type == "AccountType");

        if (claim?.Value == requirement.AccountType)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}

