namespace VMTP.Code.Domain.Utilities;

public class CodeUtil
{
    private static readonly Random Random = new Random();
    private const int CODE_LENGTH = 6;

    /// <summary>
    /// Метод для генерации кода
    /// </summary>
    /// <returns>Код</returns>
    public static string Generate()
    {
        var currentCode = string.Empty;
        for (var i = 0; i < CODE_LENGTH; i++)
        {
            currentCode += Random.Next(0, 9);
        }

        return currentCode;
    }

}