using Nute.Application.Songs.Vms;
using Nute.Domain.Songs;
using Nute.Domain.Songs.Exceptions;

namespace Nute.Application.Songs;

public static class SongsManagementService
{
    private static void ValidateSourcePath(string sourcePath)
    {
        if (!Directory.Exists(sourcePath))
        {
            throw new DirectoryNotFoundException($"{sourcePath} doesn't exists.");
        }
    }

    public static CompareSongsResultVm CompareAllSongs(string directoryPath, string otherDirectoryPath)
    {
        var directoryUniqueSongs = GetAllUniqueSongsInDirectoryComparedTo(
            directoryPath: directoryPath,
            otherDirectoryPath: otherDirectoryPath
        );
        var otherDirectoryUniqueSongs = GetAllUniqueSongsInDirectoryComparedTo(
            directoryPath: otherDirectoryPath,
            otherDirectoryPath: directoryPath
        );
        var editedSongs = GetAllEditedSongsInDirectoryComparedTo(
            directoryPath: otherDirectoryPath,
            otherDirectoryPath: directoryPath
        );

        return new CompareSongsResultVm(
            AddedSongs: directoryUniqueSongs,
            EditedSongs: editedSongs,
            DeletedSongs: otherDirectoryUniqueSongs
        );
    }

    private static List<string> GetAllUniqueSongsInDirectoryComparedTo(string directoryPath, string otherDirectoryPath)
    {
        var uniqueSongs = new List<string>();

        var directories = Directory.EnumerateDirectories(directoryPath);
        foreach (var directory in directories)
        {
            var newDirectoryPath = directory + "";
            var newOtherDirectoryPath = directory + "";
            var result = GetAllUniqueSongsInDirectoryComparedTo(
                directoryPath: newDirectoryPath,
                otherDirectoryPath: newOtherDirectoryPath
            );
            uniqueSongs.AddRange(result);
        }

        var files = Directory.EnumerateFiles(directoryPath);
        foreach (var file in files)
        {
            var equivalentPath = "";

            if (!File.Exists(equivalentPath))
            {
                uniqueSongs.Add(file);
            }
        }

        return uniqueSongs;
    }

    private static IEnumerable<string> GetAllEditedSongsInDirectoryComparedTo(string directoryPath, string otherDirectoryPath)
    {
        var editedSongs = new List<string>();

        var directories = Directory.EnumerateDirectories(directoryPath);
        foreach (var directory in directories)
        {
            var newDirectoryPath = directory + "";
            var newOtherDirectoryPath = directory + "";
            var result = GetAllUniqueSongsInDirectoryComparedTo(
                directoryPath: newDirectoryPath,
                otherDirectoryPath: newOtherDirectoryPath
            );
            editedSongs.AddRange(result);
        }

        var files = Directory.EnumerateFiles(directoryPath);
        foreach (var file in files)
        {
            var equivalentFilePath = "";

            if (!File.Exists(equivalentFilePath))
            {
                continue;
            }

            var result = AreSongsEqual(
                song1Path: file,
                song2Path: equivalentFilePath
            );

            if (result is false)
            {
                editedSongs.Add(file);
            }
        }

        return editedSongs;
    }

    public static bool AreSongsEqual(string song1Path, string song2Path)
    {
        try
        {
            var song1 = Song.Parse(path: song1Path);
            var song2 = Song.Parse(path: song2Path);
            return song1.Equals(song2);
        }
        catch (SongFilePathIsInvalidDomainException exception)
        {
            throw new Exception(exception.Message);
        }
        catch (SongFileNotExistDomainException exception)
        {
            throw new Exception(exception.Message);
        }
    }

    public static void SyncSongs()
    {
    }

    public static void OrganizeSongsByAlbum(string path)
    {
        ValidateSourcePath(path);

        var files = Directory.EnumerateFiles(path);
        foreach (var file in files)
        {
            try
            {
                var song = Song.Parse(path: file);
                var album = song.Album;

                var parentDirectory = Path.Combine(path, album);
                if (!Directory.Exists(parentDirectory))
                {
                    Directory.CreateDirectory(parentDirectory);
                }

                var newFilePath = Path.Combine(parentDirectory, file);
                File.Move(file, newFilePath);

            }
            catch (Exception)
            {
            }
        }
    }
}
