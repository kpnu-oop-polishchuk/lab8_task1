internal class Program
{
    private static void CopyDir(string src, string dest)
    {
        var src_dir = new DirectoryInfo(src);

        if (!Directory.Exists(dest))
            Directory.CreateDirectory(dest);
        foreach (FileInfo inner in src_dir.GetFiles())
            inner.CopyTo(Path.Combine(dest, inner.Name));
        foreach (DirectoryInfo inner in src_dir.GetDirectories())
            CopyDir(inner.FullName, Path.Combine(dest, inner.Name));
    }

    private static void Main(string[] args)
    {
        string drive = "D:";
        string oop_dir = Path.Combine(drive, "OOP_lab08");
        string[] dirnames = [
            "KN1-B23",
            "Polishchuk",
            "Sources",
            "Reports",
            "Texts",
        ];

        string[] dirs = dirnames
            .Select(name => Path.Combine(oop_dir, name))
            .ToArray();

        if (!Directory.Exists(oop_dir))
            Directory.CreateDirectory(oop_dir);

        foreach (string path in dirs)
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

        foreach (string path in dirs[2..])
            CopyDir(path, dirs[1]);

        Directory.Move(dirs[1], dirs[0]);

        DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(dirs[0], dirnames[1], dirnames[4]));
        StreamWriter file = new(Path.Combine(dirs[0], dirnames[1], dirnames[4], "dirinfo.txt"));

        file.WriteLine($"Назва каталогу: {dirInfo.Name}");
        file.WriteLine($"Повна назва каталогу: {dirInfo.FullName}");
        file.WriteLine($"Час створення каталогу: {dirInfo.CreationTime}");
        file.WriteLine($"Кореневий каталог: {dirInfo.Root}");

        file.Close();
    }
}
