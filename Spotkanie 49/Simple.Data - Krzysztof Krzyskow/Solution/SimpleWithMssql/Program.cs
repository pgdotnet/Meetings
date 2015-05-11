using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using Simple.Data;
using SimpleWithMssql.Examples;

namespace SimpleWithMssql
{
	class Program
	{
		static void Main(string[] args)
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");

			var source = SimpleDataTraceSources.TraceSource;
			source.Listeners.Add(new TextWriterTraceListener(Console.Out));

			var connectionString = "Server=localhost\\SQLEXPRESS;Database=DemoDB;Integrated Security=SSPI;";
			//var connectionString = "Server=(localdb)\\v11.0;Database=DemoDB;Integrated Security=SSPI;";

			RunExamples(connectionString);

			Console.ReadKey();
		}

		public static void RunExamples(string connectionString)
		{
			ConnectionExample.UseSharedConnection(connectionString);

			NameConventionResolutionExample.CheckDifferentNamesForSameTable(connectionString);
			//NameConventionResolutionExample.ThrowOnPotentialTableNamesConflict(connectionString);

			SimpleRecordExamples.GetByPrimaryKey(connectionString);
			SimpleRecordExamples.FindByGenreId(connectionString);
			SimpleRecordExamples.FindByGenreIdAndYear(connectionString);
			SimpleRecordExamples.FindByGenreIdOrYear(connectionString);


			SimpleQueryExamples.GetAllOfThem(connectionString);

			SimpleQueryExamples.FindAllOfThem(connectionString);
			SimpleQueryExamples.FindAllOfThemWithAnd(connectionString);
			SimpleQueryExamples.FindAllOfThemWithOr(connectionString);

			SimpleQueryExamples.FindAllOfThemBy(connectionString);
			SimpleQueryExamples.FindAllOfThemByNamedParameterVersion(connectionString);
			SimpleQueryExamples.FindAllOfThemByWithAnd(connectionString);
			SimpleQueryExamples.FindAllOfThemByWithAndNamedParameterVersion(connectionString);


			QueryModifiersExamples.SelectSomething(connectionString);
			QueryModifiersExamples.SelectSomethingWithAlias(connectionString);
			QueryModifiersExamples.SelectSomethingWithDistinct(connectionString);
			QueryModifiersExamples.SelectSomethingWithWhere(connectionString);
			QueryModifiersExamples.SelectSomethingWithCombinedWhere(connectionString);

			QueryModifiersExamples.WhereWithIn(connectionString);
			QueryModifiersExamples.WhereWithBetween(connectionString);
			QueryModifiersExamples.WhereWithLike(connectionString);


			ScalarQueriesExample.GetCountByColumn(connectionString);
			ScalarQueriesExample.GetCountByColumnFluent(connectionString);
			ScalarQueriesExample.CheckExistsByColumn(connectionString);
			ScalarQueriesExample.CheckExistsByColumnFluent(connectionString);


			OrderingExamples.OrderBy(connectionString);
			OrderingExamples.OrderByDescending(connectionString);
			OrderingExamples.OrderByInJoinedTablesByPrimaryTable(connectionString);
			OrderingExamples.OrderByInJoinedTablesByJoinedTable(connectionString);
			OrderingExamples.ThenBy(connectionString);
			OrderingExamples.ThenByDescending(connectionString);


			ExplicitJoinExamples.SimpleJoin(connectionString);
			ExplicitJoinExamples.SimpleJoinWithOn(connectionString);
			ExplicitJoinExamples.SimpleJoinWithAlias(connectionString);
			ExplicitJoinExamples.MoreComplexJoin(connectionString);
			ExplicitJoinExamples.MoreComplexJoinWithFilter(connectionString);

			NaturalJoinExamples.SimpleJoin(connectionString);
			NaturalJoinExamples.MoreComplexJoin(connectionString);
			NaturalJoinExamples.MoreComplexJoinWithFilter(connectionString);


			AggragateFunctionsExample.AggregateWithCount(connectionString);
			AggragateFunctionsExample.AggregateWithCount2(connectionString);
			AggragateFunctionsExample.SimpleHaving(connectionString);
			AggragateFunctionsExample.AggregateWithCountAndFilter(connectionString);
			AggragateFunctionsExample.AggregateWithCountAndOrFilter(connectionString);
			AggragateFunctionsExample.AggregateWithCountAndSumFilter(connectionString);
			AggragateFunctionsExample.AggregateWithCountAndMinFilter(connectionString);

			LazyAndEagerLoadingExample.SimpleLazyLoading(connectionString);
			LazyAndEagerLoadingExample.SimpleEagerLoading(connectionString);
			LazyAndEagerLoadingExample.NaiveEagerLoading(connectionString);
			LazyAndEagerLoadingExample.SimpleEagerLoading(connectionString);
			LazyAndEagerLoadingExample.ComplexEagerLoading(connectionString);
			LazyAndEagerLoadingExample.ComplexEagerLoadingShorterWith(connectionString);
			LazyAndEagerLoadingExample.ComplexEagerLoadingShorterWithAndFilter(connectionString);
			LazyAndEagerLoadingExample.ComplexEagerLoadingShorterWithAndFilterAndSingleActor(connectionString);
			LazyAndEagerLoadingExample.ComplexEagerLoadingNonForeignKeyRelationship(connectionString);

			CastingToObjectsExample.SimpleRecordImplicitCast(connectionString);
			CastingToObjectsExample.SimpleRecordImplicitCastWhenNoRecords(connectionString);
			CastingToObjectsExample.SimpleRecordImplicitCastWhenCastingToWrongType(connectionString);
			CastingToObjectsExample.SimpleRecordImplicitCastWhenCastingToAlmostWrongType(connectionString);

			CastingToObjectsExample.SimpleQueryImplicitCast(connectionString);
			CastingToObjectsExample.SimpleQueryImplicitCastWhenNoRecords(connectionString);
			CastingToObjectsExample.SimpleQueryImplicitCastWhenCastingToWrongType(connectionString);
			CastingToObjectsExample.SimpleQueryImplicitCastWhenCastingToAlmostWrongType(connectionString);
			//CastingToObjectsExample.ThrowOnSimpleQueryAndRecordImplicitCastMismatch(connectionString);

			CastingToScalarExample.SimpleRecordImplicitCastToScalar(connectionString);
			CastingToScalarExample.SimpleQueryImplicitCastToScalar(connectionString);
			CastingToScalarExample.SimpleQueryImplicitCastToScalarList(connectionString);

			PagingFunctionExample.SkipFirstTwoAndTakeTwoNext(connectionString);
			PagingFunctionExample.SinglePageWithTotalCount(connectionString);
			PagingFunctionExample.MultiPageWithTotalCount(connectionString);

			InsertExamples.InsertByNamedParameters(connectionString);
			InsertExamples.InsertByObject(connectionString);
			InsertExamples.MultiInsert(connectionString);

			UpdateExamples.UpdateByObject(connectionString);
			UpdateExamples.UpdateByNamedParametersWhenNoPrimaryKey(connectionString);
			UpdateExamples.UpdateByWhenNoPrimaryKey(connectionString);
			UpdateExamples.UpdateAllWithCriteria(connectionString);

			DeleteExamples.DeleteByNamedParameter(connectionString);
			DeleteExamples.DeleteBy(connectionString);
			DeleteExamples.DeleteAllWithCriteria(connectionString);

			TransactionExamples.RunTransactionWithRolback(connectionString);
			TransactionExamples.RunTransactionWithCommit(connectionString);
		}
	}
}
