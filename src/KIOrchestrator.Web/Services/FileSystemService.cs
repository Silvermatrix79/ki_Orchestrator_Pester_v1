namespace KIOrchestrator.Web.Services;

public class FileSystemService
{
    public List<DriveEntry> GetDrives()
    {
        return DriveInfo.GetDrives()
            .Where(d => d.IsReady)
            .Select(d => new DriveEntry
            {
                Name = d.Name,
                Label = string.IsNullOrEmpty(d.VolumeLabel) ? d.Name : $"{d.VolumeLabel} ({d.Name.TrimEnd('\\')})",
                TotalSize = d.TotalSize,
                FreeSpace = d.AvailableFreeSpace
            })
            .ToList();
    }

    public List<DirectoryEntry> GetDirectories(string path)
    {
        var dir = new DirectoryInfo(path);
        if (!dir.Exists)
            return [];

        return dir.GetDirectories()
            .Where(d => !d.Attributes.HasFlag(FileAttributes.Hidden)
                     && !d.Attributes.HasFlag(FileAttributes.System))
            .OrderBy(d => d.Name)
            .Select(d =>
            {
                bool hasChildren;
                try
                {
                    hasChildren = d.GetDirectories().Any(sub =>
                        !sub.Attributes.HasFlag(FileAttributes.Hidden)
                        && !sub.Attributes.HasFlag(FileAttributes.System));
                }
                catch
                {
                    hasChildren = false;
                }

                return new DirectoryEntry
                {
                    Name = d.Name,
                    FullPath = d.FullName,
                    HasChildren = hasChildren
                };
            })
            .ToList();
    }

    public string? GetParentPath(string path)
    {
        var parent = Directory.GetParent(path);
        return parent?.FullName;
    }

    public bool DirectoryExists(string path)
    {
        return Directory.Exists(path);
    }
}

public class DriveEntry
{
    public required string Name { get; set; }
    public required string Label { get; set; }
    public long TotalSize { get; set; }
    public long FreeSpace { get; set; }
}

public class DirectoryEntry
{
    public required string Name { get; set; }
    public required string FullPath { get; set; }
    public bool HasChildren { get; set; }
}
