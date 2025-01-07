using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIVCombo.Interface
{
	public class Changelog
	{
		public static Dictionary<string, string[]> GetChangelog()
		{
			return new Dictionary<string, string[]>()
				{					
					{
						"v2.0.0.3",
						[
							"Fixed PCT's Substractive applying to both Fire in Red and Blizzard in Cyan, only applying to Fire in Red now as it used to."
						]
					},							
					{
						"v2.0.0.2",
						[
							"Fixed MCH's Hypercharge Blaster not replacing the correct action like it used to."
						]
					},					
					{
						"v2.0.0.1",
						[
							"Added back BLM's Flare/Freeze in the Enochian combo line."
						]
					},
					{
						"v2.0.0.0",
						[
							"Initial re-release!",
							"Every job is Dawntrail updated.",
							"XIVCombo is a tool designed for accessibility.",
							"Some new features may be added in the future.",
							"You may run into potential bugs or issue ; if you do, please use the Dalamud Feedback feature.",
						]
					},
				};
		}
	}
}
