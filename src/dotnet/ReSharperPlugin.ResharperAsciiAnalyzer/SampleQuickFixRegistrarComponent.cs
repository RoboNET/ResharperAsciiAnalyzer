using JetBrains.Application;
using JetBrains.Lifetimes;
using JetBrains.ReSharper.Feature.Services.QuickFixes;

namespace ReSharperPlugin.ResharperAsciiAnalyzer
{
    [ShellComponent]
    internal class SampleQuickFixRegistrarComponent
    {
        public SampleQuickFixRegistrarComponent(IQuickFixes table)
        {
            table.RegisterQuickFix<SampleHighlighting>(
                Lifetime.Eternal,
                h => new SampleFix(h.Declaration),
                typeof(SampleFix));
        }
    }
}