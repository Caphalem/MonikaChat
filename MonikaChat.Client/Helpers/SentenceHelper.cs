using MonikaChat.Client.Models;
using System.Text.RegularExpressions;

namespace MonikaChat.Client.Helpers
{
	public static class SentenceHelper
	{
		public static List<Sentence> SplitParagraph(string paragraph)
		{
			List<Sentence> sentences = new List<Sentence>();
			string codeBlockPattern = @"(```[\s\S]*?```)";
			var splits = Regex.Split(paragraph, codeBlockPattern);

			string sentencePattern = @"(?<sentence>[^.!?~\s].*?(?:[.!?~](?:~)?(?:\s|$)|$))\s*(\{\{(?<sprite>[^\}]+)\}\})?\s*";
			string spritePattern = @"\s*\{\{(?<sprite>[^\}]+)\}\}\s*";

			foreach (string split in splits)
			{
				if (Regex.IsMatch(split, "^```"))
				{
					// Directly add code blocks as a single "sentence" with no sprite
					sentences.Add(new Sentence
					{
						Content = split,
						Sprite = ""
					});
				}
				else
				{
					foreach (Match match in Regex.Matches(split, sentencePattern))
					{
						string sentenceContent = match.Groups["sentence"].Value.Trim();
						string sprite = match.Groups["sprite"].Success ? match.Groups["sprite"].Value : "";
						string midSentenceSprite = string.Empty;

						// Now look for sprites within the sentence
						MatchCollection spriteMatches = Regex.Matches(sentenceContent, spritePattern);
						if (spriteMatches.Count > 0)
						{
							midSentenceSprite = spriteMatches[0].Groups["sprite"].Value;

							// Remove all sprite patterns from the sentence and clean up surrounding spaces
							sentenceContent = Regex.Replace(sentenceContent, spritePattern, " ").Trim();
						}

						sentences.Add(new Sentence
						{
							Content = sentenceContent,
							Sprite = string.IsNullOrWhiteSpace(sprite) ? midSentenceSprite : sprite // Prioritize the end-sentence sprite over the mid-sentence sprite
						});
					}
				}
			}

			return sentences;
		}
	}
}
