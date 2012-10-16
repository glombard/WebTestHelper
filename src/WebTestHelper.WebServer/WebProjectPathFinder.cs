using System;
using System.IO;
using System.Text.RegularExpressions;

namespace WebTestHelper.WebServer
{
    public class WebProjectPathFinder : IWebProjectPathFinder
    {
        public string FindWebProjectPath()
        {
            Uri codeBaseUri = new Uri(this.GetType().Assembly.CodeBase);
            string testAssemblyLocation = codeBaseUri.LocalPath;
            
            // Remove bin and Debug from the path:
            string projectFolder = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(testAssemblyLocation)));
            projectFolder = Regex.Replace(projectFolder, @"\.?Tests?$", "", RegexOptions.IgnoreCase);

            if (!Directory.Exists(projectFolder))
            {
                throw new WebServerException(string.Format("Web project path '{0}' not found", projectFolder));
            }

            return projectFolder;
        }
    }
}
