// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
Solution s = new Solution();
var stones = new int[] { 0, 1, 3, 5, 6, 8, 12, 17 };
var answer = s.CanCross(stones);
Console.WriteLine(answer);

public class Solution
{
  public bool CanCross(int[] stones)
  {
    // [0,1,3,5,6,8,12,17]
    // The key of the map is stone. The value is, if the frog stand on this stone, how many steps this frog can jump.
    // (0, {1}), from 0 stone we can take 0 + 1 steps only 
    // (1, {1, 2}), from 1 stone we can take 1 or 1 + 1 steps as our previous step was 1
    // stone 3 can be reach from 1 by taking 2 steps, 
    // (3, {1, 2, 3}), from 3 stone we can take 2 - 1, 2 or 2 + 1 steps as our previous step was 2.
    // stone 5 can be reach from 3 by taking 2 steps, 
    // (5, {1, 2, 3}), from 5 stone we can take 2 - 1, 2 or 2 + 1 steps as our previous step was 2.
    // stone 6 can be reach from 3 and 5 , by taking 3 steps from stone 3 and 1 steps from stone 5, 
    // (6, {1, 2, 3, 4}) from 6 stone we can take 1, 1 + 1 AND 3 - 1, 3 or 3 + 1 steps as our previous step either 3 or 1.
    // ....... and goes on

    if (stones == null || stones.Length == 0) return false;
    Dictionary<int, HashSet<int>> map = new Dictionary<int, HashSet<int>>();
    map.Add(0, new HashSet<int>());
    map[0].Add(1);

    for (int i = 1; i < stones.Length; i++)
    {
      map.Add(stones[i], new HashSet<int>());
    }

    for (int i = 0; i < stones.Length - 1; i++)
    {
      int stone = stones[i];
      foreach (int step in map[stone])
      {
        var reach = stone + step;

        if (reach == stones[^1]) return true;

        if (map.ContainsKey(reach))
        {
          var set = map[reach];
          if (set != null)
          {
            set.Add(step);
            if (step - 1 > 0) set.Add(step - 1);
            set.Add(step + 1);
          }
        }
      }
    }

    return false;
  }
}
