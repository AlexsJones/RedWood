using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Features.Indexed;
using RedWood.Implementation.SessionLogger;
using RedWood.Interface.FileService;
using RedWood.Interface.Reporting;
using RedWood.Interface.SessionLogger;

namespace RedWood.Implementation.Reporting
{
    public class ReportingService : IReportingService
    {
        private readonly ISessionLogger _sessionLogger;

        private readonly IFileService _fileService;

        private ISessionInfo _sessionContext = null;

        public ReportingService(ISessionLogger sessionLogger,
            IIndex<FileServiceType, IFileService> fileServices)
        {
            _sessionLogger = sessionLogger;

            _fileService = fileServices[FileServiceType.Local];

        }

        public void SetCurrentContext()
        {
            _sessionContext = _sessionLogger.GenerateSessionInfo();
        }

        public void WriteLogMessage(string message)
        {
            _sessionLogger.LogMessage(_sessionContext.GetCurrentGuidTimeString(), message);
        }

        public void WriteReport(string path)
        {
            string str = _fileService.ReadFile(path);

            if (_sessionContext == null)
            {
                throw new ReportingException("No session context set!");
            }

            _sessionLogger.LogMessage(_sessionContext.GetCurrentGuidTimeString(), str);
        }

    }
}
