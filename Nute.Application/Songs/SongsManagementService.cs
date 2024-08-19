using Nute.Application.Songs.Vms;
using Nute.Domain.Songs;
using Nute.Domain.Songs.Exceptions;

namespace Nute.Application.Songs;

public static class SongsManagementService
{
    public static CompareSongsResultVm CompareAllSongs(string sourcePath, string destinationPath)
    {
        // loop trough source path
        // if it's a directory
        // call SpecialMethod with new sourcePath and destinationPath
        // if it's a file
        // generate equivalent path
        // compare them
        // get the result

        var ayCarambaResult = AyCaramba(
            sourcePath: sourcePath,
            destinationPath: destinationPath
        );

        var deletedSongs = XCE(
            sourcePath: sourcePath,
            destinationPath: destinationPath
        );

        return new CompareSongsResultVm(
            AddedSongs: ayCarambaResult.AddedSongs,
            UpdatedSongs: ayCarambaResult.UpdatedSongs,
            DeletedSongs: deletedSongs
        );
    }

    private static AyCarambaResult AyCaramba(string sourcePath, string destinationPath)
    {
        var addedSongs = new List<string>();
        var updatedSongs = new List<string>();

        var directories = Directory.EnumerateDirectories(sourcePath);
        foreach (var directory in directories)
        {
            var newSourcePath = sourcePath + "";
            var newDestinationPath = destinationPath + "";
            var result = AyCaramba(sourcePath: newSourcePath, destinationPath: newDestinationPath);
            addedSongs.AddRange(result.AddedSongs);
            updatedSongs.AddRange(result.UpdatedSongs);
        }

        var files = Directory.EnumerateFiles(sourcePath);
        foreach (var filePath in files)
        {
            var fidPath = "";

            if (!File.Exists(fidPath))
            {
                addedSongs.Add(filePath);
                continue;
            }

            var result = AreSongsEqual(
                song1Path: filePath,
                song2Path: fidPath
            );

            if (result is false)
            {
                updatedSongs.Add(filePath);
            }
        }

        return new AyCarambaResult(
            AddedSongs: addedSongs,
            UpdatedSongs: updatedSongs
        );
    }

    private static IEnumerable<string> XCE(string sourcePath, string destinationPath)
    {
        return [];
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
}
