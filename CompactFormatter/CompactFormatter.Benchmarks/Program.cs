using System;

using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

namespace CompactFormatter.Benchmarks
{
	class Program
	{
		static void Main()
		{
			var exporter = new CsvExporter(
				CsvSeparator.CurrentCulture,
				new SummaryStyle(
					cultureInfo: System.Globalization.CultureInfo.CurrentCulture,
					printUnitsInHeader: true,
					printUnitsInContent: false,
					timeUnit: Perfolizer.Horology.TimeUnit.Microsecond,
					sizeUnit: SizeUnit.KB
				));

			var config = ManualConfig.CreateMinimumViable().AddExporter(exporter);

			Summary[] summaries = BenchmarkRunner.Run(typeof(Program).Assembly, config);

			Console.ReadKey();
		}
	}
}
