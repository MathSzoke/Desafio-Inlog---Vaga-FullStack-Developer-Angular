namespace SharedKernel.CurrentUser;

public class CurrentUserContext : ICurrentUserContext
{
    public string UserId => Guid.NewGuid().ToString();
}