using System;
using System.Collections.Generic;
using System.Text;

namespace cgmv.Contracts
{
    public class ScanningReport
    {
        public int NumberFilesScanned { get; set; }

        public IList<FileReport> FilesReport { get; set; } = new List<FileReport>();
    }

    public class FileReport
    {
        public string FilePath { get; set; } = string.Empty;

        public IList<ComponentReport> ComponentReport { get; set; } = new List<ComponentReport>();

        public string? UnexpectedExceptionMessage;
    }

    public class ComponentReport
    {
        public bool ValidationPassed { get; set; }

        public string? ComponentJsonRepresentation { get; set; }

        public IList<string> Messages { get; set; } = new List<string>();
    }
}
