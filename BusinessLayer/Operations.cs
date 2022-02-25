using System.Diagnostics;
using System.Xml;

namespace BusinessLayer
{
    public static class Operations
    {
        public enum LogTypeEntry
        {
            PX,
            Tpv,
            wsTrx,
            wsServicePayment
        }
        public static string ReadLogEntries(string logName, LogTypeEntry logTypeEntry, ref int numTrxSuccess, ref int numTrxFailure)
        {
            EventLog eventLog = new()
            {
                Log = logName
            };

            string lastDateTimeRecordTemp = "";
            try
            {
                string logText;
                switch (logTypeEntry)
                {
                    case LogTypeEntry.PX:
                        if (eventLog.Entries.Count == 0)
                        {
                            Helpers.lastDateTimeRecord = DateTime.Now.ToString();
                            return Helpers.lastDateTimeRecord;
                        }
                        else
                        {
                            foreach (EventLogEntry entry in eventLog.Entries)
                            {
                                logText = entry.Message;
                                if (logText.Length > 29)
                                {
                                    if (logText.Substring(0, 26) == Helpers.lastDateTimeRecord)
                                    {
                                        numTrxSuccess = 0;
                                        numTrxFailure = 0;

                                        if (logText.Substring(24, 5) == ", 140" || logText.Substring(24, 5) == ", 260")
                                        {
                                            if (logText.Substring(307, 2) == "00")
                                            {
                                                numTrxSuccess++;
                                            }
                                            else
                                            {
                                                numTrxFailure++;
                                            }
                                        }
                                        lastDateTimeRecordTemp = logText.Substring(0, 26);
                                    }
                                    else
                                    {
                                        if (logText.Substring(24, 5) == ", 140" || logText.Substring(24, 5) == ", 260")
                                        {
                                            if (logText.Substring(307, 2) == "00")
                                            {
                                                numTrxSuccess++;
                                            }
                                            else
                                            {
                                                numTrxFailure++;
                                            }
                                        }
                                        lastDateTimeRecordTemp = logText.Substring(0, 26);
                                    }
                                }
                            }
                        }
                        Helpers.lastDateTimeRecord = lastDateTimeRecordTemp;
                        return Helpers.lastDateTimeRecord;
                    case LogTypeEntry.Tpv:
                        if (eventLog.Entries.Count == 0)
                        {
                            Helpers.lastDateTimeRecord = DateTime.Now.ToString();
                            return Helpers.lastDateTimeRecord;
                        }
                        else
                        {
                            foreach (EventLogEntry entry in eventLog.Entries)
                            {
                                logText = entry.Message;
                                if (logText.Length > 29)
                                {
                                    if (logText.Substring(0, 26) == Helpers.lastDateTimeRecord)
                                    {
                                        numTrxSuccess = 0;
                                        numTrxFailure = 0;

                                        if (logText.Substring(35, 4) == ":210")
                                        {
                                            if (logText.Substring(133, 2) == "00")
                                            {
                                                numTrxSuccess++;
                                            }
                                            else
                                            {
                                                numTrxFailure++;
                                            }
                                        }
                                        lastDateTimeRecordTemp = logText.Substring(0, 26);
                                    }
                                    else
                                    {
                                        if (logText.Substring(35, 4) == ":210")
                                        {
                                            if (logText.Substring(133, 2) == "00")
                                            {
                                                numTrxSuccess++;
                                            }
                                            else
                                            {
                                                numTrxFailure++;
                                            }
                                        }
                                        lastDateTimeRecordTemp = logText.Substring(0, 26);
                                    }
                                }
                            }
                        }
                        Helpers.lastDateTimeRecord = lastDateTimeRecordTemp;
                        return Helpers.lastDateTimeRecord;
                    case LogTypeEntry.wsTrx:
                        if (eventLog.Entries.Count == 0)
                        {
                            Helpers.lastDateTimeRecord = DateTime.Now.ToString();
                            return Helpers.lastDateTimeRecord;
                        }
                        else
                        {
                            foreach (EventLogEntry entry in eventLog.Entries)
                            {
                                logText = entry.Message;
                                if (logText.Length > 29)
                                {
                                    if (logText.Substring(0, 26) == Helpers.lastDateTimeRecord)
                                    {
                                        numTrxSuccess = 0;
                                        numTrxFailure = 0;

                                        int responseCode = Operations.ReadXML(logText);

                                        if (responseCode > 0)
                                        {
                                            numTrxSuccess++;
                                        }
                                        else if (responseCode == 6)
                                        {
                                            numTrxFailure++;
                                        }

                                        lastDateTimeRecordTemp = logText.Substring(0, 24);
                                    }
                                    else
                                    {
                                        numTrxSuccess = 0;
                                        numTrxFailure = 0;

                                        int responseCode = Operations.ReadXML(logText);

                                        if (responseCode > 0)
                                        {
                                            numTrxSuccess++;
                                        }
                                        else if (responseCode == 6)
                                        {
                                            numTrxFailure++;
                                        }

                                        lastDateTimeRecordTemp = logText.Substring(0, 26);
                                    }
                                }
                            }
                        }
                        Helpers.lastDateTimeRecord = lastDateTimeRecordTemp;
                        return Helpers.lastDateTimeRecord;
                    case LogTypeEntry.wsServicePayment:
                        if (eventLog.Entries.Count == 0)
                        {
                            Helpers.lastDateTimeRecord = DateTime.Now.ToString();
                            return Helpers.lastDateTimeRecord;
                        }
                        else
                        {
                            foreach (EventLogEntry entry in eventLog.Entries)
                            {
                                logText = entry.Message;
                                if (logText.Length > 29)
                                {
                                    if (logText.Substring(0, 26) == Helpers.lastDateTimeRecord)
                                    {
                                        //Helpers.Log("SI, se reinician contadores", Helpers.LogType.info);
                                        numTrxSuccess = 0;
                                        numTrxFailure = 0;

                                        string ResponseCodeXml = "00";
                                        if (logText.IndexOf("CODIGORESPUESTA: ") > 0)
                                        {
                                            ResponseCodeXml = logText.Substring(logText.IndexOf("CODIGORESPUESTA: ") + 17, 2);
                                        }

                                        if (int.Parse(ResponseCodeXml) > 0)
                                        {
                                            numTrxFailure++;
                                        }
                                        lastDateTimeRecordTemp = logText.Substring(0, 26);
                                    }
                                    else
                                    {
                                        string ResponseCodeXml = "00";
                                        if (logText.IndexOf("CODIGORESPUESTA: ") > 0)
                                        {
                                            ResponseCodeXml = logText.Substring(logText.IndexOf("CODIGORESPUESTA: ") + 17, 2);
                                        }

                                        if (int.Parse(ResponseCodeXml) > 0)
                                        {
                                            numTrxFailure++;
                                        }
                                        lastDateTimeRecordTemp = logText.Substring(0, 26);
                                    }
                                }
                            }
                        }
                        Helpers.lastDateTimeRecord = lastDateTimeRecordTemp;
                        return Helpers.lastDateTimeRecord;
                    default:
                        Helpers.lastDateTimeRecord = DateTime.Now.ToString();
                        return Helpers.lastDateTimeRecord;
                }
            }
            catch (Exception ex)
            {
                Helpers.Log(Helpers.GetPathCall(ex.Message), Helpers.LogType.error);
                return DateTime.Now.ToString();
            }
        }

        private static int ReadXML(string xml)
        {
            XmlDocument xmlDocument = new();
            XmlNode xmlNode;

            try
            {
                xml = xml.Substring(xml.IndexOf("<"));

                xmlDocument.LoadXml(xml);
                xmlNode = xmlDocument.DocumentElement;

                string ReloadResponse = "ReloadResponse";
                string DataResponse = "DataResponse";
                string QueryResponse = "QueryResponse";
                string DataQueryResponse = "DataQueryResponse";

                if (string.Equals(xmlNode.ParentNode.LastChild.Name.ToUpper(), ReloadResponse.ToUpper()) ||
                    string.Equals(xmlNode.ParentNode.LastChild.Name.ToUpper(), DataResponse.ToUpper()) ||
                    string.Equals(xmlNode.ParentNode.LastChild.Name.ToUpper(), QueryResponse.ToUpper()) ||
                    string.Equals(xmlNode.ParentNode.LastChild.Name.ToUpper(), DataQueryResponse.ToUpper()))
                {
                    return int.Parse(xmlNode["ResponseCode"].InnerText);
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception ex)
            {
                Helpers.Log(Helpers.GetPathCall(ex.Message), Helpers.LogType.error);
                return 1;
            }
        }
    }
}