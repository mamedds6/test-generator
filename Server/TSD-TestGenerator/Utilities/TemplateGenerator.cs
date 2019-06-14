using System.Collections.Generic;
using System.Text;
using TSDTestGenerator.Model;

namespace TSDTestGenerator.Utilities
{
    public static class TemplateGenerator
    {
        public static string GetHTMLString(List<Question> questions)
        {
            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                            <ol type=''1''>");

            foreach (var question in questions)
            {
                sb.AppendFormat(@"<li>{0}</li>", question.Content);
                sb.Append(@"<ol type=''A''>");
                foreach (QuestionAnswer questionAnswer in question.QuestionAnswer)
                {
                    sb.AppendFormat(@"<li>{0}</li>", questionAnswer.Answer.Content);
                }

                sb.Append("</ol>");
            }

            sb.Append(@"
                                </ol>
                            </body>
                        </html>");

            return sb.ToString();
        }
    }
}