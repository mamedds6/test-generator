using System.Collections.Generic;
using System.Linq;
using System;
using TSDTestGenerator.Model;
using Microsoft.AspNetCore.Mvc;
using DinkToPdf;
using DinkToPdf.Contracts;
using System.IO;
using TSDTestGenerator.Utilities;

namespace TSDTestGenerator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfCreatorController : ControllerBase
    {

        private IConverter _converter;
        public List<Question> questions;

        public PdfCreatorController(IConverter converter, List<Question> questions)
        {
            _converter = converter;
            this.questions = questions;
        }

        [HttpGet]
        public IActionResult CreatePDF()
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "Random Quiz",
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = TemplateGenerator.GetHTMLString(questions),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            var file = _converter.Convert(pdf);
            return File(file, "application/pdf","RandomQuiz.pdf");
        }
    }
}
