using NxPDFGenerator.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace NxPDFGenerator.Reports
{
    public class rpt_Invoice_SavaInsurance : ICustomReport
    {
        private static string ImagesFolder { get; } = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"Assets\Images"));
        private static string PrimaryColor { get; } = "#3ca082";


        public TravelInsurancePolicy Policy { get; set; }
        public List<Insurer> Insurers { get; }

        public rpt_Invoice_SavaInsurance(TravelInsurancePolicy policy, List<Insurer> insurers)
        {
            Policy = policy;
            Insurers = insurers;
        }

        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.MarginHorizontal(5, Unit.Millimetre);
                    page.MarginVertical(10, Unit.Millimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(8));

                    page.Header().Element(ComposeHeader);
                    page.Content().Element(ComposeContent);
                    page.Footer().Element(ComposeFooter);
                });
        }

        public void ComposeContent(IContainer container)
        {
            var contractor = Insurers.Where(x => x.IsContractor).First();

            container
                .PaddingTop(30)
                         .Column(mainCol =>
                         {
                             mainCol.Item()
                                 .Row(row =>
                                 {

                                     row.Spacing(20);

                                     row.RelativeItem().AlignCenter().AlignMiddle().Column(col =>
                                     {
                                         col.Item().Text("ПОЛИСА ЗА ПАТНИЧКО ОСИГУРУВАЊЕ").Bold().AlignCenter();
                                         col.Item().Text("TRAVELLERS INSURANCE").Bold().AlignCenter();
                                         col.Item().Text("SAVA INSURANCE").Bold().AlignCenter();
                                     });
                                     row.RelativeItem().AlignCenter().AlignMiddle().Column(col =>
                                     {
                                         col.Item().Text("Број на полиса / Policy No.").AlignCenter();
                                         col.Item().Text(Policy.PolicyNo).Bold().AlignCenter();
                                     });
                                 });

                             mainCol.Item().PaddingVertical(5).LineHorizontal(1).LineColor(PrimaryColor);

                             mainCol.Item()
                                 .Row(row =>
                                 {
                                     row.Spacing(20);

                                     row.RelativeItem().AlignCenter().AlignMiddle().Column(col =>
                                     {
                                         col.Item().Text("Референтен број / Reference No.").AlignCenter();
                                         col.Item().Text(Policy.ReferenceNo).Bold().AlignCenter();
                                     });
                                     row.RelativeItem().AlignCenter().AlignMiddle().Column(col =>
                                     {
                                         col.Item().Text("Ознака на трансакцијата / Transaction ID").AlignCenter();
                                         col.Item().Text(Policy.TransactionID).Bold().AlignCenter();
                                     });
                                 });

                             mainCol.Item().PaddingVertical(5).LineHorizontal(1).LineColor(PrimaryColor);

                             mainCol.Item().Table(table =>
                             {
                                 table.ColumnsDefinition(columns =>
                                 {
                                     columns.RelativeColumn(1);
                                     columns.RelativeColumn(1);
                                 });

                                 table.Cell().Column(1).Row(1).Element(CellStyle)
                                 .Text($"Договорувач/Contractor: {contractor.Name} {contractor.EMBG}");

                                 table.Cell().Column(1).Row(2).Element(CellStyle)
                                 .Text($"Тип/Type: {Policy.TypeOfPolicy}");

                                 table.Cell().Column(1).Row(3).Element(CellStyle)
                                 .Text($"Осигуреник/Insured: На списокот / On the list");

                                 table.Cell().Column(1).Row(4).Element(CellStyle)
                                 .Text($"Лица/Persons: {Policy.NoOfPeopleInsured}");

                                 table.Cell().Column(1).Row(5).Element(CellStyle)
                                .Text($"Адреса/Address: JohnSmith Address 1");

                                 table.Cell().Column(1).Row(6).Element(CellStyle)
                                 .Text($"Вид на покритие/Type of coverage: {Policy.TypeOfCoverage}");

                                 table.Cell().Column(1).Row(7).Element(CellStyle)
                                 .Text($"Започнува на/Valid from: {Policy.StartDate}");

                                 table.Cell().Column(1).Row(8).Element(CellStyle)
                                 .Text($"Важи за сите земји / Valid for all countries");

                                 table.Cell().Column(2).Row(1).Element(CellStyle)
                                .Text($"Завршува на/Valid until: {Policy.EndDate}");
                                 table.Cell().Column(2).Row(2).Element(CellStyle)
                                .Text($"Период на покритие/Insured period: {Policy.NumberOfDaysValid}");
                                 table.Cell().Column(2).Row(3).Element(CellStyle)
                                .Text($"Франшиза/Deductible: {Policy.Deductible}");

                             mainCol.Item().PaddingVertical(5).LineHorizontal(1).LineColor(PrimaryColor);

                             mainCol.Item().Row(row =>
                             {
                                 row.AutoItem().Text("Список на лица осигурани по полиса за патничко осигурување ");
                                 row.AutoItem().Text(" PZO 080224").Bold();
                             });
                             mainCol.Item().Row(row =>
                             {
                                 row.AutoItem().Text("List of insured persons by the travel insurance policy");
                                 row.AutoItem().Text(" PZO 080224").Bold();
                             });

                             mainCol.Item().PaddingVertical(5);

                             var tableHeaders = new List<TableHeader>()
                             {
                                new TableHeader() { NameMK = "Р.Бр", NameEn = "No." },
                                new TableHeader() { NameMK = "Презиме и име", NameEn = "Name of the insurer" },
                                new TableHeader() { NameMK = "ЕМБГ", NameEn = "EMBG" },
                                new TableHeader() { NameMK = "Пасош број", NameEn = "Passport No." },
                                new TableHeader() { NameMK = "Спортски ризик", NameEn = "Sports Risks" },
                                new TableHeader() { NameMK = "Covid 19", NameEn = "Covid 19" },
                                new TableHeader() { NameMK = "Професионален возач", NameEn = "Professional driver" },
                                new TableHeader() { NameMK = "Студент во странство", NameEn = "Students abroad" },
                             };

                             mainCol.Item().Table(table =>
                             {
                                 table.ColumnsDefinition(columns =>
                                 {
                                     columns.ConstantColumn(30);
                                     columns.RelativeColumn(1);
                                     columns.RelativeColumn(1);
                                     columns.RelativeColumn(1);
                                     columns.RelativeColumn(1);
                                     columns.ConstantColumn(40);
                                     columns.RelativeColumn(1);
                                     columns.RelativeColumn(1);
                                 });

                                 table.Header(tableHeader =>
                                 {
                                     foreach (var th in tableHeaders)
                                     {
                                         tableHeader.Cell().Element(Block).Column(col =>
                                         {
                                             col.Item().Text(th.NameMK);
                                             col.Item().Text(th.NameEn);
                                         });
                                     }
                                 });

                                 foreach (var ins in Insurers)
                                 {
                                     foreach (var prop in ins.GetType().GetProperties())
                                     {
                                         if(prop.Name != nameof(Insurer.IsContractor))
                                            table.Cell().Element(Entry).Text(prop?.GetValue(ins)?.ToString());
                                     }
                                 }
                             });

                             mainCol.Item().PaddingVertical(10).LineHorizontal(1).LineColor(PrimaryColor);

                             mainCol.Item().Text("Незгода").Bold();
                             mainCol.Item().Row(row =>
                             {
                                 row.AutoItem().Text("Осигуреник/Insured:");
                                 row.AutoItem().Text(" Сите лица").Bold();
                             });

                             mainCol.Item().PaddingVertical(10);

                             mainCol.Item().Row(row =>
                             {
                                 row.RelativeItem().AlignMiddle().Column(col =>
                                 {
                                     col.Item().PaddingBottom(5).Text("Датум и место на изготвување / Date and place of issue").AlignCenter();
                                     col.Item().Text("Скопје, 22.07.2022").Bold().AlignCenter();
                                 });
                                 row.RelativeItem().AlignMiddle().Column(col =>
                                 {
                                     col.Item().PaddingBottom(5).Text("Вкупна премија / Total premium").AlignCenter();
                                     col.Item().Text(Policy.PremiumAmount).Bold().AlignCenter();
                                 });
                             });

                             mainCol.Item().PaddingVertical(10);

                             mainCol.Item()
                                 .Text(
                                     "Осигурувањето е склучено согласно Условите за патничко осигурување У-ПАТН 01/2021 бр. 02-20225/19 од 01.07.2021 година со датум на примена од 01.08.2021 кои му се предадени на договорувачот на осигурувањето по електронски пат.")
                                 .Italic();
                             mainCol.Item()
                                 .Text(
                                     "Општите услови за осигурување на лица од последици на несреќен случај (незгода) НОУ-01/2021 бр. 02-20225/3 од 01.07.2021 година со датум на примена од 01.08.2021 кои му се предадени на договорувачот на осигурувањето по електронски пат.")
                                 .Italic();

                             mainCol.Item().PaddingVertical(10);

                             mainCol.Item()
                                 .Text("Телефонски број за контакт со Асистентската компанија: +389 2 5101-525")
                                 .Italic();
                             mainCol.Item()
                                 .Text("Сите права по оваа полиса му припаѓаат на осигуреникот")
                                 .Italic();
                             mainCol.Item()
                                 .Text("Согласно законот за ДДВ, член 23, точка 6, дејноста осигурување е ослободена од плаќање на данок без право на одбиток на претходен данок")
                                 .Italic();
                             mainCol.Item()
                                 .Text("Осигурувачот го задржува правото на пресметковна и друга грешка")
                                 .Italic();

                             mainCol.Item().PaddingVertical(10);

                             mainCol.Item()
                                 .Text("Го овластувам Осигурувачот во случај на поднесено побарување да бара податоци за мојата здравствена состојба и лекување. Во случај на поднесено оштетно побарување ги ослободува здравствените институции и мојот матичен лекар да ги дадат бараните податоци на Осигурувачот.")
                                 .Italic();
                         });
        }

        public void ComposeFooter(IContainer container)
        {
            container.Column(footerCol =>
            {
                footerCol.Item().Row(row =>
                {
                    row.Spacing(20);
                    row.RelativeItem().AlignMiddle().AlignCenter().Column(col =>
                    {
                        col.Item().Text("• Ова осигурување е договорено преку ИНТЕРНЕТ;").FontSize(6);
                        col.Item().Text("• Договорувачот на осигурувањето мора своерачно да ја потпише оваа полиса;").FontSize(6);
                        col.Item().Text("• За сите барања поврзани со покритието користете го бројот на ПОЛИСА;").FontSize(6);
                        col.Item().Text("• Оваа полиса е издадена по електронски пат и има важност без печат и потпис од осигурувачот;").FontSize(6);
                    });

                    row.RelativeItem().AlignCenter().Column(col =>
                    {
                        col.Spacing(30);

                        col.Item().AlignMiddle().AlignCenter().Text("Договорувач");
                        col.Item().Width(140).LineHorizontal(1);
                    });
                });
            });
        }

        public void ComposeHeader(IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                table.Cell()
                     .AlignCenter()
                     .Height(15, Unit.Millimetre)
                     .Image(Path.Combine(ImagesFolder, "sava-logo.png")).FitArea();

                table.Cell()
                     .AlignCenter()
                     .PaddingTop(3, Unit.Millimetre)
                     .Column(col =>
                     {
                         col.Item().Text("Ул. Загребска бр.28А");
                         col.Item().Text("1000 Скопје");
                         col.Item().Text("Р. Македонија");
                     });

                table.Cell()
                     .AlignCenter()
                     .Height(15, Unit.Millimetre)
                     .Image(Path.Combine(ImagesFolder, "24-7.png")).FitArea();

                table.Cell()
                     .AlignCenter()
                     .PaddingTop(3, Unit.Millimetre)
                     .Column(col =>
                     {
                         col.Item().Text("тел./tel.: +389 2 5101 525");
                         col.Item().Text("24 часа сервис / 24 hours service");
                     });
            });
        }

        static IContainer Block(IContainer container) =>
        container
                .BorderBottom(1)
                .Background(Colors.Grey.Lighten5)
                .ShowOnce()
                .Padding(3)
                .MinWidth(10)
                .MinHeight(10)
                .AlignMiddle();
        

        static IContainer Entry(IContainer container) =>
        container
            .BorderBottom(1)
            .PaddingVertical(2)
            .PaddingHorizontal(3)
            .ShowOnce()
            .AlignMiddle();
        

        static IContainer CellStyle(IContainer container) =>
        container.PaddingVertical(1).PaddingRight(5);
    }
}
