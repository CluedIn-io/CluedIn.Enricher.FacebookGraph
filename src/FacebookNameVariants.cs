using System.Collections.Generic;

namespace CluedIn.ExternalSearch.Providers.FacebookGraph
{
	public static class FacebookNameVariants
	{
		public static IEnumerable<string> GetFacebookNameVariants(this IEnumerable<string> names)
		{
			foreach (var name in names)
			{
				yield return name;

				var n = name.Replace("/", string.Empty);

				yield return n.Replace(" ", string.Empty);
				yield return n.Replace(" ", string.Empty).Replace(".", string.Empty);
				yield return n.Replace(" ", ".");
			}
		}
	}
}