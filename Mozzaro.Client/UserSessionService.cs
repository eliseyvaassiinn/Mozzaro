namespace Mozzaro.Client.Services;

public class UserSessionService
{
    public int? UserId { get; private set; }

    public string? UserName { get; private set; }

    public string? UserEmail { get; private set; }

    public bool IsAuthenticated => UserId.HasValue;

    public event Action? OnChange;

    public void Login(int userId, string userName, string userEmail)
    {
        UserId = userId;
        UserName = userName;
        UserEmail = userEmail;

        NotifyStateChanged();
    }

    public void Logout()
    {
        UserId = null;
        UserName = null;
        UserEmail = null;

        NotifyStateChanged();
    }

    private void NotifyStateChanged()
    {
        OnChange?.Invoke();
    }
}