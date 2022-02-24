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

            string logText;
            switch (logTypeEntry)
            {
                case LogTypeEntry.PX:
                    if (eventLog.Entries.Count == 0)
                    {
                        Helpers.lastDateTimeRecord = DateTime.Now.ToString();
                    }
                    else
                    {
                        foreach (EventLogEntry entry in eventLog.Entries)
                        {
                            logText = entry.Message;
                            //logText = "20220222 18:12:04.218422, 26000300030001220222180954096583760100309       000000000054744000000000                    00000000000300000000000009932091412AMIGO SL                                                                                                                                                  35PA30 ->TRS:1";
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
                    }
                    else
                    {
                        foreach (EventLogEntry entry in eventLog.Entries)
                        {
                            logText = entry.Message;
                            //logText = "";
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
                    }
                    else
                    {
                        foreach (EventLogEntry entry in eventLog.Entries)
                        {
                            logText = entry.Message;
                            //logText = @"22/02/2022 19:01:58.59243, XML respuesta Recarga (TRS->0):<?xml version='1.0' encoding='utf - 8'?><ReloadResponse><ID_GRP>255</ID_GRP><ID_CHAIN>3</ID_CHAIN><ID_MERCHANT>3</ID_MERCHANT><ID_POS>1</ID_POS><DateTime>22/02/2022 19:00:08</DateTime><PhoneNumber>6481004471</PhoneNumber><TransNumber>62666</TransNumber><Brand></Brand><Instr1></Instr1><Instr2></Instr2><AutoNo>0</AutoNo><ResponseCode>35</ResponseCode><DescripcionCode>Numero de telefono invalido</DescripcionCode></ReloadResponse>";
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
                    }
                    else
                    {
                        foreach (EventLogEntry entry in eventLog.Entries)
                        {
                            logText = entry.Message;
                            //Helpers.Log(logText.Substring(0, 26) + " es igual a " + Helpers.lastDateTimeRecord + " ?", Helpers.LogType.info);
                            //logText = "2022/02/18 11:42:26.116066, WS Pago_Servicios | Respuesta del método Ejecuta_Info: CODIGORESPUESTA: 93CODIGORESPUESTADESCR: Mantenimiento en progreso ";
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
                                    //Helpers.Log("NO", Helpers.LogType.info);
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
                    return Helpers.lastDateTimeRecord;
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