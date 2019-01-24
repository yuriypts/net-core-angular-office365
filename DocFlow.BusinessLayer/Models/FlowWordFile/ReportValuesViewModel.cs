using DocFlow.BusinessLayer.Models.FlowWordFile;
using System.Collections.Generic;

namespace DocFlow.Models.FlowWordFile
{
    public class ReportValuesViewModel : File
    {
        public int ReportTypeId { get; set; }
        public List<ReportLabelModel> Values { get; set; }
    }
}
