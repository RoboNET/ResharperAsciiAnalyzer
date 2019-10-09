using System.Text;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace ReSharperPlugin.ResharperAsciiAnalyzer
{
    [ElementProblemAnalyzer(
        typeof(ICSharpDeclaration),
        HighlightingTypes = new[] {typeof(SampleHighlighting)})]
    public class SampleProblemAnalyzer : ElementProblemAnalyzer<ICSharpDeclaration>
    {
        protected override void Run(ICSharpDeclaration element, ElementProblemAnalyzerData data,
            IHighlightingConsumer consumer)
        {
            // Hint: Avoid LINQ methods to increase performance
            // if (element.Name.Any(char.IsUpper))
            //     consumer.AddHighlighting(new IdentifierHasUpperCaseLetterHighlighting(element));

            // Hint: Also foreach creates additional enumerator
            // ReSharper disable once ForCanBeConvertedToForeach
            // ReSharper disable once LoopCanBeConvertedToQuery

            if (IsContainsNonAsciiSymbol(element.NameIdentifier.Name, out var _, out var _))
                consumer.AddHighlighting(new SampleHighlighting(element));
        }

        private static bool IsContainsNonAsciiSymbol(string input, out char symbol, out int index)
        {
            byte[] array = Encoding.Unicode.GetBytes(input);

            for (int i = 0; i < array.Length; i += 2)
            {
                if ((array[i] | (array[i + 1] << 8)) > 128)
                {
                    symbol = (char) (array[i] | (array[i + 1] << 8));
                    index = i / 2;
                    return true;
                }
            }

            symbol = (char) 0;
            index = 0;

            return false;
        }
    }
}