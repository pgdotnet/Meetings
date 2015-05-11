using System.Collections.Generic;

namespace SimpleWithMssql.Dto
{
	public class ManyGenresSingleActorMovie : MovieBase
	{
		public decimal Profit { get; set; }

		public decimal OpenningProfit { get; set; }

		public decimal Budget { get; set; }

		public Actor Actor { get; set; }

		public IList<Genre> Genres { get; set; }

		public Director Director { get; set; }
	}
}