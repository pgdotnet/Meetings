using System.Collections.Generic;

namespace SimpleWithMssql.Dto
{
	public class Movie : MovieBase
	{
		public decimal Profit { get; set; }

		public decimal OpenningProfit { get; set; }

		public decimal Budget { get; set; }

		public IList<Actor> Actors { get; set; }

		public Director Director { get; set; }
	}
}