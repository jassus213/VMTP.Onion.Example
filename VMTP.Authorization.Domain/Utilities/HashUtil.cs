using System.Security.Cryptography;
using System.Text;

namespace VMTP.Authorization.Domain.Utilities;

/// <summary>
/// Утилита для хеша пароля с солью
/// </summary>
public static class HashUtil
{
    private const string CONST_SALT = "aqm2105dovb952jgzyas"; //b1901c1bbd1777ad8ec8

    /// <summary>
    /// Вычисление хеша
    /// </summary>
    /// <param name="password">Пароль</param>
    /// <returns></returns>
    public static string ComputeHash(string password)
    {
        var bytes = Encoding.Unicode.GetBytes(password + CONST_SALT);
        var hash = SHA256.HashData(bytes);

        return string.Join("", hash.Select(b => b.ToString("x2")));
    }
}