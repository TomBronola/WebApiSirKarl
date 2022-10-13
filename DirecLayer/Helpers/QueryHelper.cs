using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirecLayer.Helpers
{
    public class QueryHelper
    {
        public static string dbname = ConfigurationManager.AppSettings["SAPDatabase"];
        public static string GetCompanies()
        {
            return "SELECT * FROM SRGC";
        }
        public string GetSLSettings()
        {
            return $@"Select DISTINCT T1.Value AS 'URL',T2.Value AS 'Port',T3.Value AS 'ServerName',T4.Value AS 'ServerIPAddress',T5.Value AS 'SAPDatabase',
                      T6.Value AS 'SAPDBUserID',T7.Value AS 'SAPDBPassword',T8.Value AS 'SAPUserId',T9.Value AS 'SAPPassword',T10.Value AS 'Servertype', T11.Value AS 'SAPLicense' FROM Settings T0 
                      outer apply (Select Value FROM Settings WHERE ClusterCode = 'SL_SETTINGS' AND Title = 'URL') as T1
                      outer apply (Select Value FROM Settings WHERE ClusterCode = 'SL_SETTINGS' AND Title = 'Port') as T2
                      outer apply (Select Value FROM Settings WHERE ClusterCode = 'SL_SETTINGS' AND Title = 'ServerName') as T3
                      outer apply (Select Value FROM Settings WHERE ClusterCode = 'SL_SETTINGS' AND Title = 'ServerIPAddress') as T4
                      outer apply (Select Value FROM Settings WHERE ClusterCode = 'SL_SETTINGS' AND Title = 'SAPDatabase') as T5
                      outer apply (Select Value FROM Settings WHERE ClusterCode = 'SL_SETTINGS' AND Title = 'SAPDBUserID') as T6
                      outer apply (Select Value FROM Settings WHERE ClusterCode = 'SL_SETTINGS' AND Title = 'SAPDBPassword') as T7
                      outer apply (Select Value FROM Settings WHERE ClusterCode = 'SL_SETTINGS' AND Title = 'SAPUserId') as T8
                      outer apply (Select Value FROM Settings WHERE ClusterCode = 'SL_SETTINGS' AND Title = 'SAPPassword') as T9
                      outer apply (Select Value FROM Settings WHERE ClusterCode = 'SL_SETTINGS' AND Title = 'ServerType') as T10
                      outer apply (Select Value FROM Settings WHERE ClusterCode = 'SL_SETTINGS' AND Title = 'SAPLicense') as T11
                      WHERE ClusterCode = 'SL_SETTINGS'";
        }

        public string FindUser(string username)
        {
            //return $@"SELECT EmpId,(firstName + ' ' + lastName) as CompleteName, firstName as Fname, middleName as Mname, 
            //    lastName as Lname, Email as Email, 
            //    U_Username,
            //     U_Password, 
            //    Active as isActive,
            //     U_UserType,
            //    U_UserActive
            //     FROM OHEM
            //     Where U_Username = '{username}' and Active = 'Y'";

            return $@"SELECT ""Code"",(""firstName"" || ' ' || ""lastName"") as ""CompleteName"", ""firstName"" as ""Fname"", ""middleName"" as ""Mname"", 
                ""lastName"" as ""Lname"", ""email"" as ""Email"", 
                ""U_Username"",
                 ""U_Password"", 
                ""Active"" as ""isActive"",
                 ""U_UserType""
                 FROM OHEM
                 Where ""U_Username"" = '{username}' and ""Active"" = 'Y'";
        }

        public string GetUserAccounts()
        {
            return $@"Select T0.empID as EmpID, T0.lastName as LastName, T0.firstName as FirstName, T0.jobTitle, T0.email as Email, T0.position as posID, isnull(CONVERT(VARCHAR(10), T0.CreateDate, 101), '') as CreateDate, T0.Active as isActive, T2.name as Position
                    from OHEM T0
                    left join OHPS T2 on T2.posID = T0.position";
        }
        public string GetUserTypes()
        {
            return $@"select posID as Code, name as Name from OHPS";
        }

        public string GetUserAccountDetails(int userID)
        {
            return $@"select T0.empID as empID, T0.firstName as FirstName, T0.lastName as LastName, T0.middleName as MiddleName, T0.jobTitle, T2.posID as Position, T0.dept, T0.mobile as MobileNo, T0.email as Email, T0.userID as UserID, T0.Active as isActive,
                    T0.U_iCAPTUREUser as iCAP, T0.U_iCAPUser as iCAPUser, T0.U_iCAPPassword as iCAPPass, T1.USER_CODE as UserName, T1.U_NAME as Position
                    from OHEM T0
                    left join OUSR T1 on T1.USERID = T0.userID
                    left join OHPS T2 on T2.posID = T0.position
                    where T0.empID = {userID}";
        }
        public string GetMeterReadings()
        {
            return $@"select T0.DocEntry as DocEntry, T1.Branch as Branch, T1.ScannedDate as ScannedDate, T1.CardCode as CardCode, T1.CardName as CardName, T0.ItemCode as ItemCode, T0.ItemName as ItemName, T0.LineNum as LineNum, 
                        T0.Meter as Meter, T0.Pressure as Pressure, T0.Temperature as Temperature 
                        from MeterReadingLines T0
                        Inner join MeterReadings T1 on T1.Id = T0.DocEntry";
        }
        public string GetLogs()
        {
            return $@"select T0.DocEntry as DocEntry, T1.Branch as Branch, T1.ScannedDate as ScannedDate, T1.CardCode as CardCode, T1.CardName as CardName, T1.Personnel as Personnel, T0.ItemCode as ItemCode, T0.ItemName as ItemName, T0.LineNum as LineNum, 
                        T0.Meter as Meter, T0.Pressure as Pressure, T0.Temperature as Temperature 
                        from MeterReadingLines T0
                        Inner join MeterReadings T1 on T1.Id = T0.DocEntry";
        }
        public string GetLogsx(string CardCode)
        {
            return $@"select T0.DocEntry as DocEntry, T1.Branch as Branch, T1.ScannedDate as ScannedDate, T1.CardCode as CardCode, T1.CardName as CardName, T1.Personnel as Personnel, T0.ItemCode as ItemCode, T0.ItemName as ItemName, T0.LineNum as LineNum, 
                        T0.Meter as Meter, T0.Pressure as Pressure, T0.Temperature as Temperature 
                        from MeterReadingLines T0
                        Inner join MeterReadings T1 on T1.Id = T0.DocEntry where T1.CardCode = '{CardCode}'";
        }
        public string GetMeterReadingDetails(int docEntry)
        {
            return $@"select T0.DocEntry as DocEntry, T1.ScannedDate as ScannedDate, T1.CardCode as CardCode, T1.CardName as CardName, T0.ItemCode as ItemCode, T0.ItemName as ItemName, T0.LineNum as LineNum, 
                        T0.Meter as Meter, T0.Pressure as Pressure, T0.Temperature as Temperature, T0.Method as Method
                        from MeterReadingLines T0
                        Inner join MeterReadings T1 on T1.Id = T0.DocEntry";
        }

        public string GetMeterReadingLines(int docEntry)
        {
            return $@"select T0.AttachmentPath_GasMeter as Gas Meter, T0.AttachmentPath_Temperature as Temperature, T0.AttachmentPath_Pressure as Pressure, T0.AttachmentPath_Acknowledgement as Acknowledgement
                        from MeterReadingLines T0
                        where T0.DocEntry = '{docEntry}'";
        }
        public string GetSchedVisits()
        {
            return $@"select Id as Id, Branch as Branch, CardCode as CardCode, CardName as CardName, Status as Status, Personnel as Personnel, ScheduledDate as ScheduledDate, Method as Method, Meter as Meter, UoM as UoM from ScheduledVisits";
        }



        public string GetMeterReadingSAP(string date1, string date2)
        {
            //return $@"SELECT A.DocEntry [Id], '' Status ,A.CardCode, A.CardName, A.DocDate [ScheduledDate] ,B.U_Branch [Branch] ,(SELECT CONCAT(z.FirstName, ' ', z.lastName) FROM OHEM z WHERE z.empID = A.OwnerCode) [Personnel] ,(SELECT TOP 1 z.UomCode FROM RDR1 Z WHERE Z.DocEntry = A.DocEntry ) [UoM], (SELECT TOP 1 z.U_Consumption FROM RDR1 Z WHERE Z.DocEntry = A.DocEntry ) [Meter], (CASE ISNULL(CAST(A.U_Remarks AS NVARCHAR(250)),'') WHEN '' THEN 'Manual' ELSE 'OCR' END)   [Method] FROM ORDR A LEFT JOIN OCRD B ON A.CardCode = B.CardCode  ";
            //return $@"SELECT TOP 100 A.DocEntry [Id], A.CardCode, A.CardName, convert(varchar, A.DocDate, 107) [ScheduledDate] ,B.U_Branch [Branch] ,(SELECT CONCAT(z.FirstName, ' ', z.lastName) FROM OHEM z WHERE z.empID = A.OwnerCode) [Personnel] ,(SELECT TOP 1 z.UomCode FROM RDR1 Z WHERE Z.DocEntry = A.DocEntry ) [UoM], (SELECT TOP 1 z.U_Meter FROM RDR1 Z WHERE Z.DocEntry = A.DocEntry ) [Meter], (CASE ISNULL(CAST(A.U_Remarks AS NVARCHAR(250)),'') WHEN '' THEN 'Manual' ELSE 'OCR' END)   [Method],(SELECT TOP 1 z.ItemCode FROM RDR1 z WHERE z.DocEntry = A.DocEntry) ItemCode FROM ORDR A LEFT JOIN OCRD B ON A.CardCode = B.CardCode  WHERE A.DocDate BETWEEN '{date1}' AND '{date2}'";
            return $@"SELECT A.U_GasMeterSerial [Serial],A.U_TenantName [Tenant],A.DocEntry [Id], A.CardCode, A.CardName, convert(varchar, A.DocDate, 107) [ScheduledDate] ,B.U_Branch [Branch] ,A.U_AssignedTechnician [Personnel],(SELECT TOP 1 z.UomCode FROM RDR1 Z WHERE Z.DocEntry = A.DocEntry ) [UoM], (SELECT TOP 1 z.U_Meter FROM RDR1 Z WHERE Z.DocEntry = A.DocEntry ) [Meter], (CASE ISNULL(CAST(A.U_Remarks AS NVARCHAR(250)),'') WHEN '' THEN 'Manual' ELSE 'OCR' END) [Method],(SELECT TOP 1 z.ItemCode FROM RDR1 z WHERE z.DocEntry = A.DocEntry) ItemCode FROM ORDR A LEFT JOIN OCRD B ON A.CardCode = B.CardCode WHERE A.DocDate BETWEEN '{date1}' AND '{date2}'";
        }

        public string GetMeterReadingAddon(string date1, string date2)
        {
            //return $@"SELECT A.DocEntry [Id], '' Status ,A.CardCode, A.CardName, A.DocDate [ScheduledDate] ,B.U_Branch [Branch] ,(SELECT CONCAT(z.FirstName, ' ', z.lastName) FROM OHEM z WHERE z.empID = A.OwnerCode) [Personnel] ,(SELECT TOP 1 z.UomCode FROM RDR1 Z WHERE Z.DocEntry = A.DocEntry ) [UoM], (SELECT TOP 1 z.U_Consumption FROM RDR1 Z WHERE Z.DocEntry = A.DocEntry ) [Meter], (CASE ISNULL(CAST(A.U_Remarks AS NVARCHAR(250)),'') WHEN '' THEN 'Manual' ELSE 'OCR' END)   [Method] FROM ORDR A LEFT JOIN OCRD B ON A.CardCode = B.CardCode  ";
            //return $@"SELECT TOP 100 A.DocEntry [Id], A.CardCode, A.CardName, convert(varchar, A.DocDate, 107) [ScheduledDate] ,B.U_Branch [Branch] ,(SELECT CONCAT(z.FirstName, ' ', z.lastName) FROM OHEM z WHERE z.empID = A.OwnerCode) [Personnel] ,(SELECT TOP 1 z.UomCode FROM RDR1 Z WHERE Z.DocEntry = A.DocEntry ) [UoM], (SELECT TOP 1 z.U_Meter FROM RDR1 Z WHERE Z.DocEntry = A.DocEntry ) [Meter], (CASE ISNULL(CAST(A.U_Remarks AS NVARCHAR(250)),'') WHEN '' THEN 'Manual' ELSE 'OCR' END)   [Method],(SELECT TOP 1 z.ItemCode FROM RDR1 z WHERE z.DocEntry = A.DocEntry) ItemCode FROM ORDR A LEFT JOIN OCRD B ON A.CardCode = B.CardCode  WHERE A.DocDate BETWEEN '{date1}' AND '{date2}'";
            return $@"SELECT A.CardCode [Serial],
                    A.Id [Id], A.DocEntry,A.CardCode, A.CardName, 
                    convert(varchar, A.ReadDate, 107) [ScheduledDate] ,
                    A.EmpId [PersonnelId], A.Meter [Meter],
                     A.ItemCode [ItemCode], A.ReadingType
                     FROM TransactionLogModels A
                        WHERE A.ReadDate
                        between '{date1}' AND
                                 '{date2}' order by A.Id  desc";
            //A.ReadDate BETWEEN '{date1}' AND '{date2}'";
        }

        public string GetMeterReadingAddonNew()
        {
            //return $@"SELECT A.DocEntry [Id], '' Status ,A.CardCode, A.CardName, A.DocDate [ScheduledDate] ,B.U_Branch [Branch] ,(SELECT CONCAT(z.FirstName, ' ', z.lastName) FROM OHEM z WHERE z.empID = A.OwnerCode) [Personnel] ,(SELECT TOP 1 z.UomCode FROM RDR1 Z WHERE Z.DocEntry = A.DocEntry ) [UoM], (SELECT TOP 1 z.U_Consumption FROM RDR1 Z WHERE Z.DocEntry = A.DocEntry ) [Meter], (CASE ISNULL(CAST(A.U_Remarks AS NVARCHAR(250)),'') WHEN '' THEN 'Manual' ELSE 'OCR' END)   [Method] FROM ORDR A LEFT JOIN OCRD B ON A.CardCode = B.CardCode  ";
            //return $@"SELECT TOP 100 A.DocEntry [Id], A.CardCode, A.CardName, convert(varchar, A.DocDate, 107) [ScheduledDate] ,B.U_Branch [Branch] ,(SELECT CONCAT(z.FirstName, ' ', z.lastName) FROM OHEM z WHERE z.empID = A.OwnerCode) [Personnel] ,(SELECT TOP 1 z.UomCode FROM RDR1 Z WHERE Z.DocEntry = A.DocEntry ) [UoM], (SELECT TOP 1 z.U_Meter FROM RDR1 Z WHERE Z.DocEntry = A.DocEntry ) [Meter], (CASE ISNULL(CAST(A.U_Remarks AS NVARCHAR(250)),'') WHEN '' THEN 'Manual' ELSE 'OCR' END)   [Method],(SELECT TOP 1 z.ItemCode FROM RDR1 z WHERE z.DocEntry = A.DocEntry) ItemCode FROM ORDR A LEFT JOIN OCRD B ON A.CardCode = B.CardCode  WHERE A.DocDate BETWEEN '{date1}' AND '{date2}'";
            return $@"SELECT A.CardCode [Serial],
                 A.Id [Id], A.DocEntry, A.CardCode, A.CardName, 
                 A.ReadDate [ScheduledDate] ,
                A.EmpId [PersonnelId], A.Meter [Meter], A.Method,
                 A.ItemCode [ItemCode], A.ReadingType
                 FROM TransactionLogModels A where DocEntry = '' and A.ReadingType = 'Consumption' ";
        }

        public string GetMeterReadingAddonNewWithDocEntry()
        {
            //return $@"SELECT A.DocEntry [Id], '' Status ,A.CardCode, A.CardName, A.DocDate [ScheduledDate] ,B.U_Branch [Branch] ,(SELECT CONCAT(z.FirstName, ' ', z.lastName) FROM OHEM z WHERE z.empID = A.OwnerCode) [Personnel] ,(SELECT TOP 1 z.UomCode FROM RDR1 Z WHERE Z.DocEntry = A.DocEntry ) [UoM], (SELECT TOP 1 z.U_Consumption FROM RDR1 Z WHERE Z.DocEntry = A.DocEntry ) [Meter], (CASE ISNULL(CAST(A.U_Remarks AS NVARCHAR(250)),'') WHEN '' THEN 'Manual' ELSE 'OCR' END)   [Method] FROM ORDR A LEFT JOIN OCRD B ON A.CardCode = B.CardCode  ";
            //return $@"SELECT TOP 100 A.DocEntry [Id], A.CardCode, A.CardName, convert(varchar, A.DocDate, 107) [ScheduledDate] ,B.U_Branch [Branch] ,(SELECT CONCAT(z.FirstName, ' ', z.lastName) FROM OHEM z WHERE z.empID = A.OwnerCode) [Personnel] ,(SELECT TOP 1 z.UomCode FROM RDR1 Z WHERE Z.DocEntry = A.DocEntry ) [UoM], (SELECT TOP 1 z.U_Meter FROM RDR1 Z WHERE Z.DocEntry = A.DocEntry ) [Meter], (CASE ISNULL(CAST(A.U_Remarks AS NVARCHAR(250)),'') WHEN '' THEN 'Manual' ELSE 'OCR' END)   [Method],(SELECT TOP 1 z.ItemCode FROM RDR1 z WHERE z.DocEntry = A.DocEntry) ItemCode FROM ORDR A LEFT JOIN OCRD B ON A.CardCode = B.CardCode  WHERE A.DocDate BETWEEN '{date1}' AND '{date2}'";
            return $@"SELECT A.CardCode [Serial],
                 A.Id [Id], A.DocEntry, A.CardCode, A.CardName, 
                 A.ReadDate [ScheduledDate] ,
                A.EmpId [PersonnelId], A.Meter [Meter], A.Method,
                 A.ItemCode [ItemCode], A.ReadingType
                 FROM TransactionLogModels A where DocEntry != '' order by A.ReadDate desc";
        }
        public string GetMeterReadingAddonNewWithOthers()
        {
            //return $@"SELECT A.DocEntry [Id], '' Status ,A.CardCode, A.CardName, A.DocDate [ScheduledDate] ,B.U_Branch [Branch] ,(SELECT CONCAT(z.FirstName, ' ', z.lastName) FROM OHEM z WHERE z.empID = A.OwnerCode) [Personnel] ,(SELECT TOP 1 z.UomCode FROM RDR1 Z WHERE Z.DocEntry = A.DocEntry ) [UoM], (SELECT TOP 1 z.U_Consumption FROM RDR1 Z WHERE Z.DocEntry = A.DocEntry ) [Meter], (CASE ISNULL(CAST(A.U_Remarks AS NVARCHAR(250)),'') WHEN '' THEN 'Manual' ELSE 'OCR' END)   [Method] FROM ORDR A LEFT JOIN OCRD B ON A.CardCode = B.CardCode  ";
            //return $@"SELECT TOP 100 A.DocEntry [Id], A.CardCode, A.CardName, convert(varchar, A.DocDate, 107) [ScheduledDate] ,B.U_Branch [Branch] ,(SELECT CONCAT(z.FirstName, ' ', z.lastName) FROM OHEM z WHERE z.empID = A.OwnerCode) [Personnel] ,(SELECT TOP 1 z.UomCode FROM RDR1 Z WHERE Z.DocEntry = A.DocEntry ) [UoM], (SELECT TOP 1 z.U_Meter FROM RDR1 Z WHERE Z.DocEntry = A.DocEntry ) [Meter], (CASE ISNULL(CAST(A.U_Remarks AS NVARCHAR(250)),'') WHEN '' THEN 'Manual' ELSE 'OCR' END)   [Method],(SELECT TOP 1 z.ItemCode FROM RDR1 z WHERE z.DocEntry = A.DocEntry) ItemCode FROM ORDR A LEFT JOIN OCRD B ON A.CardCode = B.CardCode  WHERE A.DocDate BETWEEN '{date1}' AND '{date2}'";
            return $@"SELECT A.CardCode [Serial],
                 A.Id [Id], A.DocEntry, A.CardCode, A.CardName, 
                 A.ReadDate [ScheduledDate] ,
                A.EmpId [PersonnelId], A.Meter [Meter], A.Method,
                 A.ItemCode [ItemCode], A.ReadingType
                 FROM TransactionLogModels A where DocEntry = '' and A.ReadingType = 'Others' ";
        }

        public string GetMeterReadingAddonNewId(string Id)
        {
            //return $@"SELECT A.DocEntry [Id], '' Status ,A.CardCode, A.CardName, A.DocDate [ScheduledDate] ,B.U_Branch [Branch] ,(SELECT CONCAT(z.FirstName, ' ', z.lastName) FROM OHEM z WHERE z.empID = A.OwnerCode) [Personnel] ,(SELECT TOP 1 z.UomCode FROM RDR1 Z WHERE Z.DocEntry = A.DocEntry ) [UoM], (SELECT TOP 1 z.U_Consumption FROM RDR1 Z WHERE Z.DocEntry = A.DocEntry ) [Meter], (CASE ISNULL(CAST(A.U_Remarks AS NVARCHAR(250)),'') WHEN '' THEN 'Manual' ELSE 'OCR' END)   [Method] FROM ORDR A LEFT JOIN OCRD B ON A.CardCode = B.CardCode  ";
            //return $@"SELECT TOP 100 A.DocEntry [Id], A.CardCode, A.CardName, convert(varchar, A.DocDate, 107) [ScheduledDate] ,B.U_Branch [Branch] ,(SELECT CONCAT(z.FirstName, ' ', z.lastName) FROM OHEM z WHERE z.empID = A.OwnerCode) [Personnel] ,(SELECT TOP 1 z.UomCode FROM RDR1 Z WHERE Z.DocEntry = A.DocEntry ) [UoM], (SELECT TOP 1 z.U_Meter FROM RDR1 Z WHERE Z.DocEntry = A.DocEntry ) [Meter], (CASE ISNULL(CAST(A.U_Remarks AS NVARCHAR(250)),'') WHEN '' THEN 'Manual' ELSE 'OCR' END)   [Method],(SELECT TOP 1 z.ItemCode FROM RDR1 z WHERE z.DocEntry = A.DocEntry) ItemCode FROM ORDR A LEFT JOIN OCRD B ON A.CardCode = B.CardCode  WHERE A.DocDate BETWEEN '{date1}' AND '{date2}'";
            return $@"SELECT A.CardCode [Serial],
                 A.Id [Id], A.DocEntry, A.CardCode, A.CardName, 
                 A.ReadDate [ScheduledDate] , A.Method,
                A.EmpId [PersonnelId], A.Meter [Meter],
                 A.ItemCode [ItemCode], A.ReadingType, A.Signature, A.Image, A.Remarks, A.ItemCode
                 FROM TransactionLogModels A where A.Id = '{Id}' order by A.Id  desc";
        }

        public string GetMeterReadingAddonByDocEntry(string Id)
        {
            //return $@"SELECT A.DocEntry [Id], '' Status ,A.CardCode, A.CardName, A.DocDate [ScheduledDate] ,B.U_Branch [Branch] ,(SELECT CONCAT(z.FirstName, ' ', z.lastName) FROM OHEM z WHERE z.empID = A.OwnerCode) [Personnel] ,(SELECT TOP 1 z.UomCode FROM RDR1 Z WHERE Z.DocEntry = A.DocEntry ) [UoM], (SELECT TOP 1 z.U_Consumption FROM RDR1 Z WHERE Z.DocEntry = A.DocEntry ) [Meter], (CASE ISNULL(CAST(A.U_Remarks AS NVARCHAR(250)),'') WHEN '' THEN 'Manual' ELSE 'OCR' END)   [Method] FROM ORDR A LEFT JOIN OCRD B ON A.CardCode = B.CardCode  ";
            //return $@"SELECT TOP 100 A.DocEntry [Id], A.CardCode, A.CardName, convert(varchar, A.DocDate, 107) [ScheduledDate] ,B.U_Branch [Branch] ,(SELECT CONCAT(z.FirstName, ' ', z.lastName) FROM OHEM z WHERE z.empID = A.OwnerCode) [Personnel] ,(SELECT TOP 1 z.UomCode FROM RDR1 Z WHERE Z.DocEntry = A.DocEntry ) [UoM], (SELECT TOP 1 z.U_Meter FROM RDR1 Z WHERE Z.DocEntry = A.DocEntry ) [Meter], (CASE ISNULL(CAST(A.U_Remarks AS NVARCHAR(250)),'') WHEN '' THEN 'Manual' ELSE 'OCR' END)   [Method],(SELECT TOP 1 z.ItemCode FROM RDR1 z WHERE z.DocEntry = A.DocEntry) ItemCode FROM ORDR A LEFT JOIN OCRD B ON A.CardCode = B.CardCode  WHERE A.DocDate BETWEEN '{date1}' AND '{date2}'";
            return $@"SELECT A.CardCode [Serial],
                     A.Id [Id], A.DocEntry, A.CardCode, A.CardName, 
                    A.ReadDate [ScheduledDate] ,
                    A.EmpId [PersonnelId], A.Meter [Meter],
                     A.ItemCode [ItemCode], A.ReadingType
                     FROM TransactionLogModels A WHERE A.Id = '{Id}'";
        }


        public string GetSchedVisitsSAP()
        {
            //return $@"select isnull(a.DocEntry,0) [Id], A.U_StoreCode [Branch] ,B.CardCode [CardCode], B.CardName [CardName]	,(SELECT CONCAT(z.FirstName, ' ', z.lastName) FROM OHEM z WHERE z.empID = A.AttendEmpl) [Personnel]	,(CASE ISNULL(A.DocNum,'') WHEN '' THEN 'Pending' ELSE 'Uploaded' END) [Status]	,(SELECT c.U_Consumption FROM RDR1 C WHERE C.DocEntry = A.DocEntry) [Meter] ,'CBM' [UoM] from	oclg a left join ocrd b on a.CardCode = b.CardCode ";
            //return $@"select P.Personnel [Personnel], 
            //            A.U_StoreCode [Branch],
            //            B.CardCode [CardCode], 
            //            B.CardName [CardName],
            //            isnull(C.DocEntry,0) [Id] ,
            //            (CASE ISNULL(C.DocEntry,'') WHEN '' THEN 'Pending' ELSE 'Uploaded' END) [Status],
            //            (SELECT D.U_Meter FROM RDR1 D WHERE C.DocEntry = D.DocEntry) [Meter],
            //            a.Recontact [ReadDate] ,'CBM' [UoM] 
            //            from OCLG a
            //            left join ocrd b on a.CardCode = b.CardCode
            //            outer apply(SELECT CONCAT(z.FirstName,' ' , isnull(z.middleName,''),' ' , z.lastName) [Personnel] FROM OHEM z WHERE z.empID = A.AttendEmpl) AS P
            //            left join ordr c on c.U_AssignedTechnician = P.Personnel and c.CardCode = a.CardCode and a.Recontact >= c.DocDate";

            return $@"select P.Personnel [Personnel], 
                        A.U_StoreCode [Branch],
                        B.CardCode [CardCode], 
                        B.CardName [CardName],
                        isnull(C.DocEntry,0) [Id] ,
                        (CASE ISNULL(C.DocEntry,'') WHEN '' THEN 'Pending' ELSE 'Uploaded' END) [Status],
                        (SELECT D.U_Meter FROM RDR1 D WHERE C.DocEntry = D.DocEntry) [Meter],
                        a.Recontact [ReadDate] ,'CBM' [UoM] 
                        from OCLG a
                        left join ocrd b on a.CardCode = b.CardCode
                        outer apply(SELECT CONCAT(z.FirstName,' ' , isnull(z.middleName,''),' ' , z.lastName) [Personnel] FROM OHEM z WHERE z.empID = A.AttendEmpl) AS P
                        left join ordr c on c.U_AssignedTechnician = P.Personnel and c.CardCode = a.CardCode and a.Recontact = c.DocDate";
        }

        public string GetActivityWithSO(int ClgCode)
        {
            return $@"
                        select P.Personnel [Personnel], 
                                                A.U_StoreCode [Branch],
                                                B.CardCode [CardCode], 
                                                B.CardName [CardName],
                                                isnull(C.DocEntry,0) [Id] ,
                                                (CASE ISNULL(C.DocEntry,'') WHEN '' THEN 'Pending' ELSE 'Uploaded' END) [Status],
                                                (SELECT D.U_Meter FROM RDR1 D WHERE C.DocEntry = D.DocEntry) [Meter],
                                                a.Recontact [StartDate],a.endDate [EndDate],c.DocDate [ReadDate],a.EndType,
						                        'CBM' [UoM],
						                        a.ClgCode 
                        from  OCLG a
	                        left join ocrd b on a.CardCode = b.CardCode
	                        outer apply(SELECT CONCAT(z.FirstName,' ' , isnull(z.middleName,''),' ' , z.lastName) [Personnel] FROM OHEM z WHERE z.empID = A.AttendEmpl) AS P
	                        left join ordr c on c.U_AssignedTechnician = P.Personnel and c.CardCode = a.CardCode 	
                        where a.ClgCode={ClgCode} 
	                        and c.DocDate between a.Recontact and CASE WHEN a.EndType='N' THEN (select Max(DocDate) from ORDR) else a.endDate END
                        order by c.DocDate desc	";
        }

        public string GetActivityWithoutSO(int ClgCode)
        {
            return $@"
                        select 
                        P.Personnel [Personnel], 
                        A.U_StoreCode [Branch],
                        B.CardCode [CardCode], 
                        B.CardName [CardName],
						a.Recontact [StartDate],a.endDate [EndDate],a.EndType,
						'CBM' [UoM],
						a.ClgCode 

                        from OCLG a 
                        left join OCRD b on a.CardCode = b.CardCode 
                        outer apply(SELECT CONCAT(z.FirstName,' ' , isnull(z.middleName,''),' ' , z.lastName) [Personnel] FROM OHEM z WHERE z.empID = A.AttendEmpl) AS P
                        where a.ClgCode={ClgCode} 
                    ";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RecurenceType">'D' for daily or 'M' for monthly</param>
        /// <returns></returns>
        public string GetActivities(char RecurenceType)
        {
            return $@"select ClgCode from OCLG a
                        where a.Recurpat = '{RecurenceType}'";
        }

        public string GetUnsyncedCount()
        {
            return $@"select count (Id) as count from ScheduledVisits where Status = 'Unsynced'";
        }


        //GET CRITICAL TANKS
        public string GetCriticalTank()
        {
            return $@"select count (A.Id) as count from GasTankModels A inner join TankLevelMaintenanceModels B ON A.Serial = B.CardCode WHERE B.Minimum >= A.Reading  ";
        }

        //GET NO. OF PENDING READINGSS
        public string GetUnsyncedCountSAP()
        {
            return $@"select count (ClgCode) as count from OCLG where status = '-2'";
        }


        //GET BPs from SAP
        public string GetBPs()
        {
            return $@"select top 500 T1.DocEntry as Id, T0.CardCode as CardCode, T0.CardName as CardName, T0.U_Branch as Store, cast(T2.U_T1ROTO as decimal(10,2)) as Minimum, cast(T2.U_T1Liters as decimal(10,2)) as Maximum 
                    from OCRD T0
                    left join [@CALIBHEAD] T1 on T0.CardCode = T1.Code
                    left join [@CALIBROW]  T2 on T1.Code = T2.Code
                    where T0.CardName is not null AND T0.CardType = 'C' 
                    order by T0.CardName asc";
        }


        public string GetImageFIleName(string id)
        {
            return $@"select isnull(concat((select top 1 value from settings where title = 'GasMeter'),ImagePath),'') img From TransactionLogModels where Id = '{id}'";
        }

        public string GetAcknowledgementFIleName2(string id)
        {
            return $@"select isnull(concat((select top 1 value from settings where title = 'GasMeter'),AcknowledgementImagePath),'') img From TransactionLogModels where Id = '{id}'";
        }

        public string GetBP()
        {
            return $@"select top 100 CardCode, CardName from OCRD where CardName is not null";
        }



        public string GetSerials(string serial)
        {
            return $@"SELECT DISTINCT A.DistNumber,B.CardName fROM OSRN A LEFT JOIN OCRD B ON A.DistNumber = B.CardCode 
                        WHERE B.CardName is not null and A.DistNumber = '" + serial + "'";

        }

        public string GetUploadedForms()
        {
            return $@"select Id, Name, Description, FileName from TenantFormModels";
        }



        public string GetSerialConversion(string itemCode, string distNumber, string meterReading)
        {
            return $@"exec [sp_usr_GetSerialConversion] '{itemCode}', '{distNumber}', '{meterReading}'";
        }


        public string GetGasTanks(string date2)
        {
            //return $@"select Id,Serial [Tank], Reading [CurrentLevel], ReadDate [LastCheckedDate], AssignedTechnician [Personnel], '' [Store] from GasTankModels where ReadDate between '{date1}' and '{date2}'";
            return $@"select Id,Serial [Tank], Reading [CurrentLevel], ReadDate [LastCheckedDate], AssignedTechnician [Personnel], '' [Store] from GasTankModels where cast(ReadDate as date) = '{date2}' AND ToSync = 0";
        }

        public string GetCardName(string code)
        {
            return $@"SELECT B.CardName FROM OWHS A INNER JOIN OCRD B ON A.U_Store = B.CardCode  WHERE A.WhsCode = '{code}'";
        }


        public string GetTankLiters(string serial, string CurrentLevel)
        {
            return $@"SELECT TOP 1 U_T1Liters [Liters] FROM [@CALIBROW] WHERE Code = '{serial}' AND U_T1ROTO = '{CurrentLevel}'";
        }


        public string GetMonths(string year)
        {
            return $@"SELECT A.ID, A.Months, B.FileName, B.Remarks FROM MonthsModels A LEFT JOIN PaymentAttachmentModels B On A.Months = B.Month AND B.Year = '{year}'";
        }

        public string GetSerialByWarehouse(string Serial)
        {
            //       return $@"SELECT T1.[ItemCode], T1.[SysNumber] as SysSerial, T1.[MnfSerial], T1.[DistNumber] as IntrSerial, T2.[CardName],T0.[WhsCode]
            //               FROM[dbo].[OSRQ] T0
            //               inner join OSRN T1 ON T0.[MdAbsEntry] = T1.AbsEntry
            //inner join OCRD T2 ON T1.[DistNumber] = T2.[CardCode]
            //               WHERE 
            //T0.[WhsCode] = ('{Serial}')  AND T0.[Quantity] <> (0)";


            return $@"SELECT T1.[ItemCode], T1.[SysNumber] as SysSerial, T1.[MnfSerial], T1.[DistNumber] as IntrSerial, T2.[WhsName],T2.[WhsCode]
                    FROM[dbo].[OSRQ] T0
                    inner join OSRN T1 ON T0.[MdAbsEntry] = T1.AbsEntry
					inner join OWHS T2 ON cast(T1.Notes as nvarchar(max)) = T2.U_Store
                    WHERE 
					T2.[U_SerialNo] = ('{Serial}')  AND T0.[Quantity] <> (0)";
        }

        public string GetSerialByBp(string Bp)
        {
            return $@"SELECT T1.[ItemCode], T1.[SysNumber] as SysSerial, T1.[MnfSerial], T1.[DistNumber] as IntrSerial, T2.[CardName],T0.[WhsCode]
                    FROM[dbo].[OSRQ] T0
                    inner join OSRN T1 ON T0.[MdAbsEntry] = T1.AbsEntry
					inner join OCRD T2 ON T1.[DistNumber] = T2.[CardCode]
                    WHERE   
					 T1.[MnfSerial] = ('{Bp}')
					AND T0.[Quantity] <> (0)";
        }

        public string GetWarehouse()
        {
            return $@"select WhsCode,WhsName,U_SerialNo AS Serial from OWHS";
        }


        public string GetBpReports(string Codes)
        {
            //return $@"SELECT DISTINCT T1.WhsCode, T1.WhsName, T0.CardCode , T0.CardName
            //          FROM OCRD T0 INNER JOIN OWHS T1 ON T0.CardCode=T1.U_Store
            //          WHERE T1.WhsCode IN ({Codes})";

            return $@"SELECT DISTINCT T0.CardCode , T0.CardName,T0.CardFName 
                          FROM OCRD T0 INNER JOIN OWHS T1 ON T0.CardCode=T1.U_Store
                WHERE T1.U_SerialNo IN ({Codes})";
        }


        public string GetBpReports2(string date1, string date2)
        {
            //return $@"SELECT DISTINCT T1.WhsCode, T1.WhsName, T0.CardCode , T0.CardName
            //          FROM OCRD T0 INNER JOIN OWHS T1 ON T0.CardCode=T1.U_Store
            //          WHERE T1.WhsCode IN ({Codes})";

            return $@"SELECT DISTINCT T0.CardCode , T0.CardName,T0.CardFName 
                      FROM OCRD T0 INNER JOIN OWHS T1 ON T0.CardCode=T1.U_Store
                        WHERE T0.CreateDate BETWEEN '{date1}' AND '{date2}'";
            //WHERE T1.U_SerialNo IN ({Codes})";
        }

        public string GetReportWhs(string Code)
        {
            return $@"SELECT T0.WhsCode,T0.U_Store,T0.U_TankNum,U_SerialNo FROM OWHS T0 INNER JOIN OCRD T1 ON T0.U_Store = T1.CardCode WHERE T1.CardCode = '{Code}'";
        }
        public string GetBpName(string Code)
        {
            return $@"SELECT CardFName FROM OCRD WHERE CardCode = '{Code}'";
        }

        public string GetBPNameByCardCode(string dateRange)
        {
            var date = dateRange.Split('-');
            var startDate = Convert.ToDateTime(date[0].Trim().ToString());
            var endDate = Convert.ToDateTime(date[1].Trim().ToString());
            return $@" 
                        DECLARE   @SQLQuery AS NVARCHAR(MAX)
                        DECLARE   @PivotColumns AS NVARCHAR(MAX)
                        DECLARE	  @PivotSelColumns AS NVARCHAR(MAX)
                        /**Get unique values of pivot column **/ 
                        SELECT   @PivotColumns= COALESCE(@PivotColumns + ',','') + QUOTENAME(CardFName)
                        FROM (SELECT DISTINCT T2.CardFName from TruckMeterModels T0
													                        INNER JOIN MLPG_TESTDB.dbo.OWHS T1 ON T0.Serial Collate SQL_Latin1_General_CP850_CI_AS = T1.U_SerialNo
													                        INNER JOIN MLPG_TESTDB.dbo.OCRD T2 ON  T1.U_Store =T2.CardCode 
												                        Where (MONTH(T0.ReadDate) = {startDate.Month} and YEAR(T0.ReadDate) = {endDate.Year}) ) AS PivotExample

                        /**Get unique values of pivot column with isnull**/ 
                        SELECT   @PivotSelColumns= ISNULL(@PivotSelColumns + ',','') 
		                        + 'ISNULL(' + QUOTENAME(CardFName) + ', 0) AS ' 
		                        + QUOTENAME(CardFName)
                        FROM (SELECT DISTINCT T2.CardFName from TruckMeterModels T0
													                        INNER JOIN MLPG_TESTDB.dbo.OWHS T1 ON T0.Serial Collate SQL_Latin1_General_CP850_CI_AS = T1.U_SerialNo
													                        INNER JOIN MLPG_TESTDB.dbo.OCRD T2 ON  T1.U_Store =T2.CardCode 
												                        Where (MONTH(T0.ReadDate) = {startDate.Month}  and YEAR(T0.ReadDate) = {endDate.Year}) ) AS PivotExample

                        SET   @SQLQuery = N' 
	                        /**Create the temporary table**/
	                        declare @days TABLE (Dates int)
	                        Declare @i int = 1
	                        WHILE @i <= 31
	                           begin
	                           INSERT INTO @days values(@i)    
	                           SET @i = @i + 1
	                           end
	                        IF OBJECT_ID(''tempdb..#TEMPDATA'') IS NOT NULL
	                        DROP TABLE #TEMPDATA

	                        SELECT * INTO #TEMPDATA FROM 
								                        (
								                        select D.Dates, isnull(X.Total,0) as Total, X.CardCode, X.CardFName
									                        from  @days D 
									                        OUTER APPLY(SELECT Sum(cast(Reading as numeric(18,2))) as Total, DAY(T0.ReadDate) as Dates, cast(ReadDate as date) as ReadDate, T2.CardCode, T2.CardFName  
												                        FROM TruckMeterModels T0
													                        INNER JOIN MLPG_TESTDB.dbo.OWHS T1 ON T0.Serial Collate SQL_Latin1_General_CP850_CI_AS = T1.U_SerialNo
													                        INNER JOIN MLPG_TESTDB.dbo.OCRD T2 ON  T1.U_Store =T2.CardCode 
												                        Where (MONTH(T0.ReadDate) = {startDate.Month} and YEAR(T0.ReadDate) = {endDate.Year}) AND  DAY(T0.ReadDate) = D.Dates
												                        Group by DAY(T0.ReadDate), cast(ReadDate as date), T2.CardCode,  T2.CardFName 
												                        ) AS X
								                        ) AS TBL ;

	                        SELECT  Dates, ' +   @PivotSelColumns + '
                            FROM #TEMPDATA    
                            PIVOT(
		                        SUM(Total) 
                                  FOR CardFName IN (' + @PivotColumns + ')) AS P  
	                        Order by Dates ;
	                        DROP TABLE #TEMPDATA ; '
                        EXEC sp_executesql @SQLQuery";
        }
        public string GetStoreBySerial(string Code)
        {
            return $@"SELECT DISTINCT T0.CardCode , T0.CardName,T0.CardFName, T1.U_SerialNo
                      FROM OCRD T0 INNER JOIN OWHS T1 ON T0.CardCode=T1.U_Store where T0.CardCode = '{Code}'";
        }

        public string GetTotalReading(DateTime start, DateTime end)
        {
            return $@"select Sum(cast(Reading as numeric(18,2))) as Total, cast(ReadDate as date) as ReadDate from 
                    GasTankModels where ReadDate between '{start}' and '{end}' group by  cast(ReadDate as date)";
        }


        public string getSerialReading(string code)
        {
            return $@"select Sum(cast(Reading as numeric(18,2))) as Total, cast(ReadDate as date) as ReadDate, T2.CardCode from GasTankModels T0
	        inner join {dbname}.dbo.OWHS T1 ON T0.Serial Collate SQL_Latin1_General_CP850_CI_AS = T1.U_SerialNo
	        INNER JOIN {dbname}.dbo.OCRD T2 ON  T1.U_Store =T2.CardCode 
	        where T2.CardCode = '{code}' 
		group by  cast(ReadDate as date), T2.CardCode";
        }

        public string getBPSerialNew(string code)
        {
            return $@"select Sum(cast(Reading as numeric(18,2))) as Total, cast(ReadDate as date) as ReadDate, T2.CardCode, T2.CardFName from GasTankModels T0
	        inner join {dbname}.dbo.OWHS T1 ON T0.Serial Collate SQL_Latin1_General_CP850_CI_AS = T1.U_SerialNo
	        INNER JOIN {dbname}.dbo.OCRD T2 ON  T1.U_Store =T2.CardCode 
	        where T2.CardCode = '{code}' 
		group by  cast(ReadDate as date), T2.CardCode,  T2.CardFName Order by cast(ReadDate as date), T2.CardCode, T2.CardFName";
        }


        public string CheckifClose(string DocEntry)
        {
            return $@"SELECT DocStatus FROM ORDR WHERE DOcEntry = '{DocEntry}' AND DocStatus != 'C'";
        }

        public string GetMeterReadingDocEntry(string DocEntry)
        {
            //return $@"SELECT A.DocEntry [Id], '' Status ,A.CardCode, A.CardName, A.DocDate [ScheduledDate] ,B.U_Branch [Branch] ,(SELECT CONCAT(z.FirstName, ' ', z.lastName) FROM OHEM z WHERE z.empID = A.OwnerCode) [Personnel] ,(SELECT TOP 1 z.UomCode FROM RDR1 Z WHERE Z.DocEntry = A.DocEntry ) [UoM], (SELECT TOP 1 z.U_Consumption FROM RDR1 Z WHERE Z.DocEntry = A.DocEntry ) [Meter], (CASE ISNULL(CAST(A.U_Remarks AS NVARCHAR(250)),'') WHEN '' THEN 'Manual' ELSE 'OCR' END)   [Method] FROM ORDR A LEFT JOIN OCRD B ON A.CardCode = B.CardCode  ";
            //return $@"SELECT TOP 100 A.DocEntry [Id], A.CardCode, A.CardName, convert(varchar, A.DocDate, 107) [ScheduledDate] ,B.U_Branch [Branch] ,(SELECT CONCAT(z.FirstName, ' ', z.lastName) FROM OHEM z WHERE z.empID = A.OwnerCode) [Personnel] ,(SELECT TOP 1 z.UomCode FROM RDR1 Z WHERE Z.DocEntry = A.DocEntry ) [UoM], (SELECT TOP 1 z.U_Meter FROM RDR1 Z WHERE Z.DocEntry = A.DocEntry ) [Meter], (CASE ISNULL(CAST(A.U_Remarks AS NVARCHAR(250)),'') WHEN '' THEN 'Manual' ELSE 'OCR' END)   [Method],(SELECT TOP 1 z.ItemCode FROM RDR1 z WHERE z.DocEntry = A.DocEntry) ItemCode FROM ORDR A LEFT JOIN OCRD B ON A.CardCode = B.CardCode  WHERE A.DocDate BETWEEN '{date1}' AND '{date2}'";
            return $@"SELECT TOP 100 A.DocEntry [Id], A.CardCode, A.CardName, convert(varchar, A.DocDate, 107) [ScheduledDate] ,B.U_Branch [Branch] ,A.U_AssignedTechnician [Personnel],(SELECT TOP 1 z.UomCode FROM RDR1 Z WHERE Z.DocEntry = A.DocEntry ) [UoM], (SELECT TOP 1 z.U_Meter FROM RDR1 Z WHERE Z.DocEntry = A.DocEntry ) [Meter], (CASE ISNULL(CAST(A.U_Remarks AS NVARCHAR(250)),'') WHEN '' THEN 'Manual' ELSE 'OCR' END)   [Method],(SELECT TOP 1 z.ItemCode FROM RDR1 z WHERE z.DocEntry = A.DocEntry) ItemCode FROM ORDR A LEFT JOIN OCRD B ON A.CardCode = B.CardCode  WHERE A.DocEntry = '{DocEntry}' ";
        }

        public string GetSchedule()
        {
            return $@"SELECT DISTINCT 
                        T0.Code,
                        T0.Name AS 'Technician',
                        CASE WHEN T0.U_SchedType = 'Gas Meter Reading' THEN 'Gas Meter Reading' ELSE 'Gas Tank Reading' END SchedType,
                        T0.U_StoreCode,
                        T3.CardName AS 'Description',
                        T3.U_Branch AS 'ForeignName',
                        T2.U_TenantName AS 'Tenant',
                        T2.U_CustGrp AS 'CustomerGroup',
                        T2.ItemCode,
                        T2.ItemName,
                        T2.MnfSerial AS 'Serial',
                        T0.CreateDate 
                        FROM [@TECHSKEDHEAD] T0 
                        INNER JOIN [@TECHSKEDROW] T1 
                        ON T0.Code = T1.Code 
                        INNER JOIN OSRN T2
						ON T0.Code = T2.U_Schedule
						INNER JOIN OCRD T3 
                        ON convert(nvarchar(500),T2.Notes) = T3.CardCode
                        WHERE T0.U_SchedType = 'Gas Meter Reading' AND (T1.U_Day = '{DateTime.Now.Day}' OR T0.U_WeekDate = '{DateTime.Now.ToString("dddd")}')

                        UNION 

                        SELECT DISTINCT 
                        T0.Code,
                        T0.Name AS 'Technician',
                        CASE WHEN T0.U_SchedType = 'Gas Meter Reading' THEN 'Gas Meter Reading' ELSE 'Gas Tank Reading' END SchedType,
                        T0.U_StoreCode,
                        T2.WhsName AS 'Description',
                        T4.U_Branch AS 'ForeignName',
                        T3.U_TenantName AS 'Tenant',
                        T3.U_CustGrp AS 'CustomerGroup',
                        T3.ItemCode,
                        T3.ItemName,
                        T2.WhsCode AS 'Serial',
                        T0.CreateDate 
                        FROM [@TECHSKEDHEAD] T0 
                        INNER JOIN [@TECHSKEDROW] T1 
                        ON T0.Code = T1.Code 
                        INNER JOIN OWHS T2 
                        ON T0.Code = T2.U_TechSched 
                        LEFT JOIN OSRN T3 ON T0.Code = T3.U_Schedule
                        INNER JOIN OCRD T4 ON T2.U_Store = T4.CardCode 
                        WHERE T0.U_SchedType = 'Gas Tank Reading' AND (T1.U_Day = '{DateTime.Now.Day}' OR T0.U_WeekDate = '{DateTime.Now.ToString("dddd")}')";
        }

        public string GetGasMeter()
        {
            return $@"SELECT 
                        T1.CardCode,
                        T1.CardName,
                        T1.ChannlBP AS 'BranchCode',
                        T1.U_Branch AS 'Branch',
                        T0.U_TenantName AS 'Tenant',
                        T0.U_CustGrp AS 'CustomerGroup', 
                        T0.MnfSerial AS 'Serial',
                        T0.ItemCode,
                        T0.itemName
                        FROM OSRN T0 INNER JOIN OCRD T1 ON convert(nvarchar(500),T0.Notes) = T1.ChannlBP";
        }

        public string GetAllSites()
        {
            return $@"SELECT 
            CAST(a.U_CustGrp AS NVARCHAR(MAX)) as Branch,
                COUNT(CONVERT(NVARCHAR(MAX), A.U_CustGrp)) as TotalNoOfTenants,
            ISNULL((SELECT COUNT(c.U_CustomerGroup) as Transacted
            FROM
            ORDR AS x
            inner join
            RDR1 as C
            on x.DocEntry = C.DocEntry
            WHERE
            x.U_Remarks LIKE '%ICAP%' and
            C.U_CustomerGroup = A.U_CustGrp and
             (select convert(varchar, x.CreateDate, 23)) >= (select convert(varchar, GETDATE(), 23))
              GROUP BY c.U_CustomerGroup,  C.U_CustomerGroup) ,0)  Transacted,
            (SELECT ISNULL(SUM(C.U_Consumption), 0) as Consumption
            FROM
             ORDR AS x
             inner join
             RDR1 as C on x.DocEntry = C.DocEntry
            WHERE x.U_Remarks LIKE '%ICAP%' and
            C.U_CustomerGroup = A.U_CustGrp) Consumption
            FROM OSRN A
            inner join OCRD as B
            on CONVERT(NVARCHAR(MAX), A.Notes) = B.CardCode
            GROUP BY A.U_CustGrp, B.CardFName,B.DocEntry";
            //return $@"SELECT COUNT(CONVERT(NVARCHAR(MAX), B.CardCode)) as TotalNoOfTenants,REPLACE(B.CardFName,'''','') as Branch, 
            //ISNULL((SELECT COUNT(c.U_CustomerGroup) as Transacted 
            // FROM ORDR AS x  inner join RDR1 as C on x.DocEntry = C.DocEntry 
            // WHERE x.U_Remarks LIKE '%ICAP%' and x.CardCode = B.CardCode AND (select convert(varchar,x.CreateDate, 23)) >= (select convert(varchar,GETDATE(), 23)) GROUP BY c.U_CustomerGroup,  C.U_CustomerGroup),0)  Transacted,
            // (SELECT ISNULL(SUM(C.U_Consumption),0) as Consumption FROM  
            // ORDR AS x inner join RDR1 as C on x.DocEntry = C.DocEntry 
            //  WHERE x.U_Remarks LIKE '%ICAP%' and x.CardCode = B.CardCode) Consumption
            //FROM OSRN A inner join OCRD as B on CONVERT(NVARCHAR(MAX), A.Notes) = B.CardCode
            //GROUP BY B.CardCode, B.CardFName,B.DocEntry";
        }

        public string GetWarehouseBySerial(string Serial)
        {
            return $@"SELECT WhsCode FROM OWHS WHERE U_SerialNo = '{Serial}'";
        }

        public string CheckIfBinActivated(string tank)
        {
            return $@"SELECT T0.BinActivat FROM OWHS T0 WHERE T0.WhsCode = '{tank}' AND T0.BinActivat = 'Y'";
        }

        public string GetBinEntry(string tank)
        {
            return $@"SELECT DftBinAbs FROM OWHS T0 WHERE T0.WhsCode = '{tank}'";
        }


        //DELEX
        public string GetAllBP()
        {
            return $@"select CardCode, CardName from OCRD";
        }

        public string GetEmailByUserType(string UserType, string Id)
        {
            return $@"select EmailAddress from UserAccountEmails where UserType = '{UserType}'  and EmailType = 'to'";
        }
        public string GetEmailByUserTypeSales(string UserType, string Id, string Email)
        {
            return $@"select EmailAddress from UserAccountEmails where UserType in ('Admin', 'Admin Asst','CEO') and EmailType = 'to' 
				and EmailId = 4";
        }

        public string GetEmailByUserTypeJMN(string UserType, string Id, string Email)
        {
            return $@"select EmailAddress from UserAccountEmails where UserType in ('JMN', 'Admin Asst','CEO') and EmailType = 'to' 
				and EmailId = 4";
        }
        public string GetEmailByUserTypeMarketing(string UserType, string Id, string Email)
        {
            return $@"select EmailAddress from UserAccountEmails where UserType in ('Marketing', 'Admin Asst','CEO') and EmailType = 'to' 
				and EmailId = 4";

    //        return $@"select EmailAddress from UserAccountEmails where UserType in ('Marketing', 'Admin Asst','CEO') and EmailType = 'to' 
				//and EmailAddress = '{Email}' and EmailId = 7";
        }


        public string GetEmailByUserTypeEmail(string UserType, string Id, string email)
        {
            return $@"select EmailAddress from UserAccountEmails where UserType = '{UserType}' and EmailType = 'to' and EmailAddress = '{email}'";
        }

        public string GetAllUser()
        {
            return $@"SELECT empID, (firstName + ' ' + lastName) as CompleteName
                 FROM OHEM where U_UserType != '8' order by empID asc";
        }

        public string GetAllSLSettings()
        {
            return $@"select * from Settings where ClusterCode = 'SL_SETTINGS'";
        }

        public string GetAlleMAILcREDS()
        {
            return $@"select * from Settings where ClusterCode = 'CRED_MAILSENDER'";
        }
        public string GetAllEmailUser()
        {
            return $@"SELECT empID,email FROM OHEM order by empID asc";
        }

        public string GetAllUserType()
        {
            return $@"SELECT * from UserTypes";
        }

        public string GetBatchPerIdAddon(int Id)
        {
            return $@"select * from Batches where PDRId = '{Id}'";
        }

        public string GetBatchPerPDRIdAddon(int Id, string itemcode)
        {
            return $@"select * from Batches where PDRId = '{Id}' and ItemCode = '{itemcode}'";
        }

        public string GetAttachments(int Id)
        {
            return $@"select Id, AttachFile,AttachFileName,CreatedDate from PDRs where Id = '{Id}'";
        }


        public string GetAllOpenPDR()
        {
            return $@"select a.*, b.CustomerCode,b.CustomerName from PDRs as a inner join Customers as b
            on a.Id = b.PDRId where a.Status != 'Closed' order by a.CreatedDate desc";
        }

        public string GetAllOpenPDRSteps(int userId)
        {
            return $@"select a.*, b.CustomerCode,b.CustomerName from PDRs as a inner join Customers as b
            on a.Id = b.PDRId where a.Status != 'Closed' and a.UserId = '{userId}' order by a.CreatedDate desc";
        }

        public string GetStatusCount()
        {
            return $@"select 'For Approval of DSM' as Status, Count(Status) as Count from PDRs where Status = 'For Approval of DSM'
                UNION
                select 'For Approval of Sr. Trade Sales Manager' as Status, Count(Status) as Count from PDRs where Status = 'For Approval of Sr. Trade Sales Manager'
                UNION
                select 'For Approval of AVP-Sales' as Status, Count(Status) as Count from PDRs where Status = 'For Approval of AVP- Sales'
                UNION
                select 'For Approval of VP-Marketing' as Status, Count(Status) as Count from PDRs where Status = 'For Approval of VP- Marketing'
                UNION
                select 'For Approval of CEO' as Status, Count(Status) as Count from PDRs where Status = 'For Approval of CEO'
                UNION
                select 'For Approval of Head of Sales' as Status, Count(Status) as Count from PDRs where Status = 'For Approval of Head of Sales'
                UNION
                select 'For Approval of President' as Status, Count(Status) as Count from PDRs where Status = 'For Approval of President'
                UNION
                select 'For Approval of Finance Manager' as Status, Count(Status) as Count from PDRs where Status IN ('For Approval of Finance', 'For Approval of Finance Manager')
                UNION
                select 'For Approval of Vice President' as Status, Count(Status) as Count from PDRs where Status = 'For Approval of Vice President'
                UNION
                select 'For Approval of Accounting Assistant' as Status, Count(Status) as Count from PDRs where Status = 'For Approval of Accounting Assistant'
                UNION
                select 'Rejected' as Status, Count(Status) as Count from PDRs where Status = 'Rejected'
                UNION
                select 'Closed' as Status, Count(Status) as Count from PDRs where Status = 'Closed'
                UNION
                select 'Total PDR' as Status, Count(Status) as Count from PDRs ";
        }

        public string GetAllOpenPerUserType(string steps)
        {
            return $@"select a.*, b.CustomerCode,b.CustomerName from PDRs as a inner join Customers as b
            on a.Id = b.PDRId where a.Status != 'Closed' and a.Steps = '{steps}' order by a.CreatedDate desc";
        }

        public string GetAllOpenPerUserTypeforVP(string steps)
        {
            return $@"select a.*, b.CustomerCode,b.CustomerName from PDRs as a inner join Customers as b
            on a.Id = b.PDRId where a.Status != 'Closed' and a.Steps in ('Vice President', 'VP- Sales and Marketing') 
			order by a.CreatedDate desc";
        }

        public string GetAllOpenPerUserTypeByCardCode(string cardcode, string steps)
        {
            return $@"select a.*, b.CustomerCode,b.CustomerName from PDRs as a inner join Customers as b
            on a.Id = b.PDRId where a.Status NOT IN ('Rejected','Closed') and b.CustomerCode = '{cardcode}' and  a.Steps = '{steps}' order by a.CreatedDate desc";
        }

        public string GetAllOpenPerUserTypeByCardCode2(string UserId, string steps)
        {
            return $@"select a.*, b.CustomerCode,b.CustomerName from PDRs as a inner join Customers as b
            on a.Id = b.PDRId where a.Status NOT IN ('Rejected','Closed') and a.UserId = '{UserId}' and  a.Steps = '{steps}' order by a.CreatedDate desc";
        }

        public string GetAllOpenPerUserTypeByCardCodeandID(string cardcode, string steps, int Id)
        {
            return $@"select a.*, b.CustomerCode,b.CustomerName from PDRs as a inner join Customers as b
            on a.Id = b.PDRId where a.Status NOT IN ('Rejected','Closed') and b.CustomerCode = '{cardcode}' and a.UserId = '{Id}' and  a.Steps = '{steps}' order by a.CreatedDate desc";
        }

        public string GetAllOpenPerUserTypeByCardCodePerUSER(string Id, string steps)
        {
            return $@"select a.*, b.CustomerCode,b.CustomerName from PDRs as a inner join Customers as b
            on a.Id = b.PDRId where a.Status NOT IN ('Rejected','Closed') and a.UserId = '{Id}' and  a.Steps = '{steps}' order by a.CreatedDate desc";
        }

        public string GetAllClosedPDR()
        {
            return $@"select a.*, b.CustomerCode,b.CustomerName from PDRs as a inner join Customers as b
            on a.Id = b.PDRId where a.Status = 'Closed' order by a.CreatedDate desc";
        }

        public string GetAllClosedPDRSteps(string steps, int userId)
        {
            return $@"select a.*, b.CustomerCode,b.CustomerName from PDRs as a inner join Customers as b
            on a.Id = b.PDRId where a.Status = 'Closed'  and a.Steps = '{steps}' and a.UserId = '{userId}' order by a.CreatedDate desc";
        }

        public string GetAllCustomer()
        {
            return $@"select CardCode, CardName, Address from OCRD";
        }

        public string GetAllAttachmentFilesPerId(string PDRId)
        {
            return $@"select * from AttachmentFiles where PDRId = '{PDRId}'";
        }

        public string GetAllCustomerPerSales(string Id, string name)
        {
            //return $@"select a.CardCode, a.CardName, a.Address from OCRD as a inner join [@TSR] as b on a.U_SalesRep3 = b.Code where  b.U_Active ='1'
            //            UNION
            //            select a.CardCode, a.CardName, a.Address from OCRD as a inner join OSLP as b on a.SlpCode = b.SlpCode where b.U_SalesCode is not null and b.Active ='Y'
            //            ";

            //return $@"select a.CardCode, a.CardName, a.Address from OCRD as a inner join [@TSR] as b on a.U_SalesRep3 = b.Code
            //            where  b.U_Active ='Y' and a.U_SalesRep3 = '{Id}'";

            return $@"select a.CardCode, a.CardName, a.Address, b.Name as TSR, b.U_TSRDistrictManager as DSM from OCRD as a inner join [@TSR] as b on a.U_SalesRep3 = b.Code 
            inner join [qryWFRCustomerTagging] as c on a.CardCode = c.CustomerNumber 
            where  b.U_Active ='Y' and c.CompanyCode = 'Delex' and b.Name like '%{name}%'
            UNION
            select a.CardCode, a.CardName, a.Address, b.SlpName as TSR, b.U_DistrictMngr as DSM from OCRD as a inner join OSLP as b on a.U_SalesRep2 = b.SlpName
			inner join [qryWFRCustomerTagging] as c on a.CardCode = c.CustomerNumber 
            where c.CompanyCode = 'Delex' and a.U_SalesRep2 LIKE '%{name}%'
            UNION
            select a.CardCode, a.CardName, a.Address, b.SlpName as TSR, b.U_DistrictMngr as DSM from OCRD as a inner join OSLP as b on a.U_SalesRep1 = b.SlpName 
			inner join [qryWFRCustomerTagging] as c on a.CardCode = c.CustomerNumber 
            where c.CompanyCode = 'Delex' and a.U_SalesRep2 LIKE '%{name}%'
            UNION
			select a.CardCode, a.CardName, a.Address, b.SlpName as TSR, b.U_DistrictMngr as DSM from OCRD as a inner join OSLP as b on a.U_SalesRep4 = b.SlpName 
			inner join [qryWFRCustomerTagging] as c on a.CardCode = c.CustomerNumber 
            where c.CompanyCode = 'Delex' and a.U_SalesRep4 LIKE '%{name}%'";
        }



        public string GetAllCustomerPerJMN(string Id, string name)
        {
            //return $@"select a.CardCode, a.CardName, a.Address from OCRD as a inner join [@TSR] as b on a.U_SalesRep3 = b.Code where  b.U_Active ='1'
            //            UNION
            //            select a.CardCode, a.CardName, a.Address from OCRD as a inner join OSLP as b on a.SlpCode = b.SlpCode where b.U_SalesCode is not null and b.Active ='Y'
            //            ";

            //return $@"select a.CardCode, a.CardName, a.Address from OCRD as a inner join [@JMN_EMPLOYEE] as b on a.U_SalesRep5 = b.Name
            //        where b.U_SalesCode is not null and b.Code = '{Id}' ";

            return $@"select a.CardCode, a.CardName, a.Address from OCRD as a inner join [@JMN_EMPLOYEE] as b on a.U_SalesRep5 = b.Name
                    inner join [qryWFRCustomerTagging] as c on a.CardCode = c.CustomerNumber
                    where c.CompanyCode = 'Delex'  and b.Code = '{Id}'
					union
					select a.CardCode, a.CardName, a.Address from OCRD as a inner join [@JMN_EMPLOYEE] as b on a.U_SalesRep6 = b.Name
					inner join [qryWFRCustomerTagging] as c on a.CardCode = c.CustomerNumber
                    where c.CompanyCode = 'Delex' and b.Code = '{Id}'";
        }



        public string GetAllCustomerPerTSR()
        {
            return $@"select a.CardCode, a.CardName, a.Address from OCRD as a inner join [@TSR] as b on a.U_SalesRep3 = b.Code where  b.U_Active ='1'";
        }

        public string GetCustomerPerIdAddon(int Id)
        {
            return $@"select * from Customers where PDRId = '{Id}'";
        }

        public string GetBatchPerItemcode(string itemcode)
        {
            return $@"SELECT 
                    T0.SysNumber  
                    ,	T0.ItemCode  
                    ,	T0.WhsCode  
                    ,	(SELECT TOP 1 DistNumber FROM OBTN WHERE ItemCode = T0.ItemCode AND SysNumber = T0.SysNumber) as BatchNumber  
                    ,	T0.Quantity   
                    ,	CAST(CAST((SELECT TOP 1 CAST(ExpDate as DATE) AS ExpDate FROM OBTN WHERE ItemCode = T0.ItemCode AND SysNumber = T0.SysNumber) AS DATE) as nvarchar(max)) as ExpDate1  
                    FROM obtq T0 WHERE T0.Quantity <> 0 AND T0.ItemCode = '{itemcode}'";
        }

        public string GetAllItems()
        {
            return $@"SELECT A.ItemCode, A.ItemName, CAST(A.U_WFR_BrandName AS NVARCHAR(100)) as GenericName, B.ListName, A.ItmsGrpCod 
                FROM OITM A
	                INNER JOIN OITB G ON A.ItmsGrpCod = G.ItmsGrpCod
                    INNER JOIN OPLN B ON A.ItemClass = B.ListNum
                    INNER JOIN ITM1 C ON B.BASE_NUM = C.PriceList
                WHERE G.ItmsGrpNam in ('DRUG CRITICAL CARE','DRUG CRITICAL CARE 2','JMN PRODUCTS','MEDICAL DEVICE')
                      /**G.ItmsGrpCod in (102,103,116,101) **/
                GROUP BY A.ItemCode,  A.ItemName,CAST(A.U_WFR_BrandName AS NVARCHAR(100)), B.ListName, A.ItmsGrpCod";
        }


        public string GetAllItemsPerId(int Id)
        {
            return $@" select * from Items where PDRId = '{Id}'";
        }

        public string GetTransactionCount()
        {
            return $@"SELECT DISTINCT
                     (SELECT COUNT(DISTINCT A.DocEntry) as Count FROM ORDR A WHERE A.U_Remarks LIKE '%ICAP%' AND (select convert(varchar,A.CreateDate, 23)) = (select convert(varchar,DATEADD(day, -1,GETDATE()), 23))) AS Previous_Day ,
                     (SELECT COUNT(DISTINCT A.DocEntry) as Count FROM ORDR A WHERE A.U_Remarks LIKE '%ICAP%' AND (select convert(varchar,A.CreateDate, 23)) >= (select convert(varchar,GETDATE(), 23))) AS Today ,
                     (SELECT COUNT(DISTINCT A.DocEntry) as Count FROM ORDR A WHERE A.U_Remarks LIKE '%ICAP%' AND (select convert(varchar,A.CreateDate, 23)) <= (select convert(varchar,DATEADD(week, 1,GETDATE()), 23))) AS Week,
                     (SELECT COUNT(DISTINCT A.DocEntry) as Count FROM ORDR A WHERE A.U_Remarks LIKE '%ICAP%' AND (select convert(varchar,A.CreateDate, 23)) <= (select convert(varchar,DATEADD(month, 1,GETDATE()), 23))) AS Month
                      FROM ORDR";
        }

    }
}
