using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace NxPDFGenerator
{
    public interface ICustomReport : IDocument
    {
        void ComposeHeader(IContainer container);
        void ComposeContent(IContainer container);
        void ComposeFooter(IContainer container);
    }
}
