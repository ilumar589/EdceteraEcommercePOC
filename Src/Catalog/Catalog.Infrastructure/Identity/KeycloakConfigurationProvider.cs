using System.Globalization;
using System.Text;
using Microsoft.Extensions.Configuration.Json;

namespace Catalog.Infrastructure.Identity;

public class KeycloakConfigurationProvider : JsonConfigurationProvider
{
    private const char KeycloakPropertyDelimiter = '-';
    private const char NestedConfigurationDelimiter = ':';
    private const int Utf8LowerCaseDistant = 32;
    private readonly StringBuilder _stringBuilder;

    /// <summary>
    /// Initializes a new instance with the specified source.
    /// </summary>
    /// <param name="source">The source settings.</param>
    public KeycloakConfigurationProvider(KeycloakConfigurationSource source) : base(source) =>
        this._stringBuilder = new StringBuilder();

    /// <summary>
    /// Loads the JSON data from a stream.
    /// </summary>
    /// <param name="stream">The stream to read.</param>
    public override void Load(Stream stream)
    {
        base.Load(stream);
        this.Data = this.Data.ToDictionary(
            x => this.NormalizeKey(x.Key),
            x => x.Value,
            StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    private string NormalizeKey(string key)
    {
        var sections = key
            .ToUpper(CultureInfo.InvariantCulture)
            .Split(NestedConfigurationDelimiter);

        foreach (var section in sections)
        {
            if (this._stringBuilder.Length != 0)
            {
                this._stringBuilder.Append(NestedConfigurationDelimiter);
            }

            foreach (var x in section.Split(KeycloakPropertyDelimiter))
            {
                for (var i = 0; i < x.Length; i++)
                {
                    var @char = x[i];
                    if (i == 0)
                    {
                        this._stringBuilder.Append(@char);
                    }
                    else
                    {
                        this._stringBuilder.Append((char)(@char + Utf8LowerCaseDistant));
                    }
                }
            }
        }

        var result = ConfigurationConstants.ConfigurationPrefix + NestedConfigurationDelimiter +
                     this._stringBuilder.ToString();
        this._stringBuilder.Clear();
        return result;
    }
}