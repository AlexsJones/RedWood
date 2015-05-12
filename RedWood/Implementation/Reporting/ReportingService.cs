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

        private Guid _sessionContext;

        private string _currentContextName = null;

        public ReportingService(ISessionLogger sessionLogger,
            IIndex<FileServiceType, IFileService> fileServices)
        {
            _sessionLogger = sessionLogger;

            _fileService = fileServices[FileServiceType.Local];

        }

        public void SetCurrentContext(string contextIdentifier)
        {
            _sessionContext = Guid.NewGuid();

            _currentContextName = contextIdentifier;
        }

        public ISessionDto GetReportingSessionDto()
        {
            return new ReportingSessionDto();
        }

        public void WriteLogMessage(string message)
        {
            ISessionDto dto = GetReportingSessionDto();

            dto.Key = _sessionContext;
            dto.Value = message;
            dto.LogSubmissionTime = DateTime.Now; ;
            dto.Name = string.IsNullOrEmpty(_currentContextName) ?
                string.Empty : _currentContextName; 

            _sessionLogger.LogObject(dto);
        }

        public void WriteObject(ISessionDto dto)
        {
            _sessionLogger.LogObject(dto);
        }

        public void WriteReport(string path)
        {
            string str = _fileService.ReadFile(path);

            if (_sessionContext == null)
            {
                throw new ReportingException("No session context set!");
            }

            ISessionDto dto = GetReportingSessionDto();

            dto.Key = _sessionContext;
            dto.Value = str;
            dto.LogSubmissionTime = DateTime.Now;
            dto.Name = string.IsNullOrEmpty(_currentContextName) ?
                string.Empty : _currentContextName; 

            _sessionLogger.LogObject(dto);
        }

    }
}
