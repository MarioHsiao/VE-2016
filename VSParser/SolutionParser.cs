using System.IO;

namespace VSProvider
{
    public class SolutionParser : ISolutionParser
    {
        private readonly string _solutionContents;

        public SolutionParser(string path)
        {
			File.AppendAllText ("logger.txt", "\nparser start file exists ... " + path);
            if (!File.Exists(path))
                throw new FileNotFoundException(string.Format("Solution file {0} does not exist", path));
			File.AppendAllText ("logger.txt", "\nSoluti parser start ... ");
			using (var reader = new StreamReader(path))
            {
				File.AppendAllText ("logger.txt", "\nparser start reader... ");
                _solutionContents = reader.ReadToEnd();
				File.AppendAllText ("logger.txt", "\nparser start read to end ... ");

                reader.Close();
				File.AppendAllText ("logger.txt", "\nparser start close ... ");
            }
        }

        public ISolution Parse()
        {
            return new Solution
            {
                preSection = (new PreSectionParser(_solutionContents)).Parse(),
                Global = (new GlobalSectionParser(_solutionContents)).Parse(),
                Projects = (new ProjectParser(_solutionContents)).Parse()
            };
        }

        public static ISolution Parse(string path)
        {
			File.AppendAllText ("logger.txt", "\nparser start ... ");
            var parser = new SolutionParser(path);
			File.AppendAllText ("logger.txt", "\nparse start ... ");
            return parser.Parse();
        }
    }
}