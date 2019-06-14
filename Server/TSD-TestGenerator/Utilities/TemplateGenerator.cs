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
                                <div class='header'><h1>This is the generated PDF report!!!</h1></div>
                                <table align='center'>
                                    <tr>
                                        <th>Name</th>
                                    </tr>");

            foreach (var question in questions)
            {
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                  </tr>", question.Content);
            }

            sb.Append(@"
                                </table>
                            </body>
                        </html>");

            return sb.ToString();
        }
    }
}