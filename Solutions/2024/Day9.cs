using Core.Interfaces;

namespace Solutions._2024;

public class Day9 : IBaseDay
{
    public long Part1(string input)
    {
        var diskMap = CreateDiskMap(input);

        for (var i = diskMap.Count - 1; i >= 0; i--)
        {
            var firstAvailableIndex = diskMap.IndexOf(-1);
            // If the first available index is greater than the current index, we can break
            if (firstAvailableIndex > i) break;
            diskMap[firstAvailableIndex] = diskMap[i];
            diskMap[i] = -1;
        }

        return CalculateChecksum(diskMap);
    }

    public long Part2(string input)
    {
        var diskMap = CreateDiskMap(input);

        var idGroup = diskMap
            .Select((val, i) => new { val, i })
            .Where(val => val.val != -1)
            .GroupBy(val => val.val)
            .OrderByDescending(val => val.Key)
            .ToDictionary(val => val.Key, val => new Tuple<int, int>(val.First().i, val.Count()));

        var startIndexFreeSpace = -1;
        var spaceBlobs = new Dictionary<int, int>();
        for (var i = 0; i < diskMap.Count; i++)
        {
            if (diskMap[i] == -1 && startIndexFreeSpace == -1) startIndexFreeSpace = i;

            if (diskMap[i] == -1 || startIndexFreeSpace == -1) continue;
            spaceBlobs.Add(startIndexFreeSpace, i - startIndexFreeSpace);
            startIndexFreeSpace = -1;
        }

        foreach (var (spaceId, amountAtIndex) in idGroup)
        foreach (var (startIndex, freeSpace) in spaceBlobs)
        {
            if (freeSpace < amountAtIndex.Item2 || startIndex >= amountAtIndex.Item1) continue;
            for (var i = 0; i < amountAtIndex.Item2; i++)
            {
                diskMap[startIndex + i] = spaceId;
                diskMap[amountAtIndex.Item1 + i] = -1;
            }

            spaceBlobs.Remove(startIndex);
            spaceBlobs.Add(startIndex + amountAtIndex.Item2, freeSpace - amountAtIndex.Item2);
            break;
        }

        return CalculateChecksum(diskMap);
    }

    private static List<int> CreateDiskMap(string orginalInput)
    {
        var input = orginalInput.ToCharArray().Select(c => c - '0').ToList();
        var diskMap = new List<int>();
        var currentId = 0;
        for (var i = 0; i < input.Count; i++)
        {
            for (var j = 0; j < input.ElementAt(i); j++)
            {
                if (i % 2 != 0)
                {
                    diskMap.Add(-1);
                    continue;
                }

                diskMap.Add(currentId);
            }

            if (i % 2 == 0) currentId++;
        }

        return diskMap;
    }

    private static long CalculateChecksum(List<int> diskMap)
    {
        var checksum = 0L;

        for (var i = 0; i < diskMap.Count; i++)
        {
            if (diskMap[i] == -1) continue;
            checksum += i * diskMap[i];
        }

        return checksum;
    }
}