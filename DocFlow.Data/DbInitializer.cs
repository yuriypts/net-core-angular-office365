using DocFlow.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocFlow.Data.Enums;


namespace DocFlow.Data
{
    public static class DbInitializer
    {

        public static void Initialize(DocFlowCotext context)
        {

            if (!context.Users.Any())
            {
                var users = new User[] {
                new User{UserName = "admin@DocFlowUSA.onmicrosoft.com", FirstName="Admin", LastName="Admin"},
                new User{UserName = "oksana.Paramzin@DocFlowUSA.onmicrosoft.com", FirstName="Oksana", LastName="Paramzin"},
                new User{UserName = "yuriy@DocFlowUSA.onmicrosoft.com", FirstName="Yuriy", LastName="Parashynets"},
                 };
                foreach (User item in users)
                {
                    context.Users.Add(item);
                }
                context.SaveChanges();
            }

            if (!context.ReportTypes.Any())
            {
                var reportType = new ReportType { Name = "Flow Cytometry Report", Template = "FlowWordFile.docx" };
                context.ReportTypes.Add(reportType);

                var reportLabels = new ReportLabel[] {
                new ReportLabel{Name="ClinicalHistory", ReportTypeId=reportType.Id, Type = (int)LabelType.String},
                new ReportLabel{Name="SymptomsReason", ReportTypeId=reportType.Id, Type = (int)LabelType.String},
                new ReportLabel{Name="PatientName", ReportTypeId=reportType.Id, Type = (int)LabelType.String},
                new ReportLabel{Name="PatientAge", ReportTypeId=reportType.Id, Type = (int)LabelType.Date},
                new ReportLabel{Name="Gender", ReportTypeId=reportType.Id, Type = (int)LabelType.String},
                new ReportLabel{Name="MRN", ReportTypeId=reportType.Id, Type = (int)LabelType.String},
                new ReportLabel{Name="ExternalSpecimenId", ReportTypeId=reportType.Id, Type = (int)LabelType.String},
                new ReportLabel{Name="ReferringPhysician", ReportTypeId=reportType.Id, Type = (int)LabelType.String},
                new ReportLabel{Name="Institution", ReportTypeId=reportType.Id, Type = (int)LabelType.String},
                new ReportLabel{Name="FlowCytometryId", ReportTypeId=reportType.Id, Type = (int)LabelType.String},
                new ReportLabel{Name="RequestingPathologist", ReportTypeId=reportType.Id, Type = (int)LabelType.String},
                new ReportLabel{Name="SpecimenSource", ReportTypeId=reportType.Id, Type = (int)LabelType.String},
                new ReportLabel{Name="SpecimenType", ReportTypeId=reportType.Id, Type = (int)LabelType.String},
                new ReportLabel{Name="CollectionDate", ReportTypeId=reportType.Id, Type = (int)LabelType.Date},
                new ReportLabel{Name="ReceivedDate", ReportTypeId=reportType.Id, Type = (int)LabelType.Date},
                new ReportLabel{Name="ReportDate", ReportTypeId=reportType.Id, Type = (int)LabelType.Date},
                };
                foreach (ReportLabel item in reportLabels)
                {
                    context.ReportLabels.Add(item);
                }
                context.SaveChanges();
            }

        }
    }
}
