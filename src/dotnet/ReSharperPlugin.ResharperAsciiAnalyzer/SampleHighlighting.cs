using JetBrains.DocumentModel;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using ReSharperPlugin.ResharperAsciiAnalyzer;

[assembly: RegisterConfigurableSeverity(
    SampleHighlighting.SeverityId,
    CompoundItemName: null,
    Group: HighlightingGroupIds.CodeSmell,
    Title: SampleHighlighting.Message,
    Description: SampleHighlighting.Description,
    DefaultSeverity: Severity.WARNING)]

namespace ReSharperPlugin.ResharperAsciiAnalyzer
{
    [ConfigurableSeverityHighlighting(
        SeverityId,
        CSharpLanguage.Name,
        OverlapResolve = OverlapResolveKind.ERROR,
        OverloadResolvePriority = 0,
        ToolTipFormatString = Message)]
    public class SampleHighlighting : IHighlighting
    {
        public const string SeverityId = nameof(SampleHighlighting);
        public const string Message = "Name identifier must contains only ASCII symbols";
        public const string Description = "Name identifier must contains only ASCII symbols";

        public SampleHighlighting(ICSharpDeclaration declaration)
        {
            Declaration = declaration;
        }

        public ICSharpDeclaration Declaration { get; }

        public bool IsValid()
        {
            return Declaration.IsValid();
        }

        public DocumentRange CalculateRange()
        {
            return Declaration.NameIdentifier?.GetHighlightingRange() ?? DocumentRange.InvalidRange;
        }

        public string ToolTip => Message;

        public string ErrorStripeToolTip
            => $"Declaration '{Declaration.DeclaredName}' contains non-ASCII symbols";
    }
}