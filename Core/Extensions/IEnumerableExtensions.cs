namespace Core.Extensions;

public static class IEnumerableExtensions
{
    public static IEnumerable<IEnumerable<T>> Permutations<T>(this IEnumerable<T> list)
    {
        if (list.Count() == 1)
        {
            yield return list;
        }
        else
        {
            foreach (var element in list)
            {
                var rest = list.Except(new List<T> {element});
                foreach (var restPerm in Permutations(rest))
                {
                    yield return new List<T> {element}.Concat(restPerm);
                }
            }
        }
    }
}