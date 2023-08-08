namespace Catalog.Infrastructure.Identity;

/// <summary>
/// Keycloak client credentials
/// </summary>
public class KeycloakClientInstallationCredentials
{
    /// <summary>
    /// Secret
    /// </summary>
    public string Secret { get; set; } = string.Empty;
}