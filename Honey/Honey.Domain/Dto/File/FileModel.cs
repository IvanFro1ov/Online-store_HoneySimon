namespace Honey.Domain.Dto.File;

/// <summary>
/// Модель файла
/// </summary>
public class FileModel
{
    /// <summary>
    /// Файл в виде масива байтов
    /// </summary>
    public Stream File { get; set; } = default!;

    /// <summary>
    /// Расшиерение файла
    /// </summary>
    public string ContentType { get; set; } = default!;
}