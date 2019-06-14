using System.Collections.Generic;
using System.Linq;
using System;
using TSDTestGenerator.Model;
using Microsoft.AspNetCore.Mvc;
using DinkToPdf;
using DinkToPdf.Contracts;
using System.IO;
using SelectPdf;
using TSDTestGenerator.Utilities;

namespace TSDTestGenerator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfCreatorController : ControllerBase
    {

        private IConverter _converter;

        public PdfCreatorController(IConverter converter)
        {
            _converter = converter;
        }

        [HttpGet]
        public IActionResult CreatePdf()
        {
            List<Question> questions;
            using (QuizDBContext quizDbContext = new QuizDBContext())
            {
                questions = new Randomizer().getRandomQuestions(quizDbContext.Question.ToList(), 10);
            }

//            var globalSettings = new GlobalSettings
//            {
//                ColorMode = ColorMode.Color,
//                Orientation = Orientation.Portrait,
//                PaperSize = PaperKind.A4,
//                Margins = new MarginSettings { Top = 10 },
//                DocumentTitle = "Random Quiz",
//            };
//
//            var objectSettings = new ObjectSettings
//            {
//                PagesCount = true,
//                HtmlContent = TemplateGenerator.GetHTMLString(questions),
//                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
//                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
//                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
//            };
//
//            var pdf = new HtmlToPdfDocument()
//            {
//                GlobalSettings = globalSettings,
//                Objects = { objectSettings }
//            };

            HtmlToPdf converter = new HtmlToPdf();

            // create a new pdf document converting an url 
            PdfDocument doc = converter.ConvertHtmlString(TemplateGenerator.GetHTMLString(questions));

            // save pdf document 
            byte[] pdfFile = doc.Save();

            // close pdf document 
            doc.Close();

            // return resulted pdf document 
            FileResult fileResult = new FileContentResult(pdfFile, "application/pdf");
            fileResult.FileDownloadName = "Document.pdf";
            return fileResult;

//            var file = _converter.Convert(pdfFile);
//            return File(file, "application/pdf","RandomQuiz.pdf");
        }
    }
}
