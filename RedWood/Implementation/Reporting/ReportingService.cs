using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Features.Indexed;
using RedWood.Interface.FileService;
using RedWood.Interface.Reporting;
using RedWood.Interface.SessionLogger;

namespace RedWood.Implementation.Reporting
{
    public class ReportingService : IReportingService
    {
        private readonly ISessionLogger _sessionLogger;

        private readonly IFileService _fileService;

        private string _currentSessionLogKey = null;

        public ReportingService(ISessionLogger sessionLogger,
            IIndex<FileServiceType, IFileService> fileServices )
        {
            _sessionLogger = sessionLogger;

            _fileService = fileServices[FileServiceType.Local];

        }

        public void SetCurrentContext()
        {
            _currentSessionLogKey = _sessionLogger.GenerateGuidDateStampKeyString();
        }

        public void WriteLogMessage(string message)
        {
            if (string.IsNullOrEmpty(_currentSessionLogKey))
            {
                throw new ReportingException("No Key set for reporting service!");
            }
           _sessionLogger.LogMessage(_currentSessionLogKey, message);
        }

        public void WriteReport(string path)
        {
            string str = _fileService.ReadFile(path);

            if (string.IsNullOrEmpty(str))
            {
                throw new ReportingException("No report file found for upload!");
            }

            _sessionLogger.LogMessage(_currentSessionLogKey,str);
        }

    }
}
