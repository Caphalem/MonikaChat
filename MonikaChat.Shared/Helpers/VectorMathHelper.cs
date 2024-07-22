using MonikaChat.Shared.Models;

namespace MonikaChat.Shared.Helpers
{
	// I'm no math wizard. Credits go to this blog post written by Alexander Bartz.
	// https://crispycode.net/vector-search-with-c-a-practical-approach-for-small-datasets/
	
	public static class VectorMathHelper
	{
		public static double DotProduct(double[] a, double[] b)
		{
			double sum = 0;
			for (int i = 0; i < a.Length; i++)
			{
				sum += a[i] * b[i];
			}

			return sum;
		}

		public static IEnumerable<ConversationMemory> FindNearestConversations(double[] query, List<ConversationMemory> snippets, int amount = 1)
		{
			Dictionary<int, double> indexScoreDictionary = new Dictionary<int, double>();

			for (int i = 0; i < snippets.Count; i++)
			{
				double dotProd = DotProduct(snippets[i].GetVector(), query);
				indexScoreDictionary.Add(i, dotProd);
			}

			IEnumerable<ConversationMemory> orderedSnippets = indexScoreDictionary.OrderByDescending((x) => x.Value).Select(x => snippets[x.Key]);

			return orderedSnippets.Take(amount);
		}
	}
}
