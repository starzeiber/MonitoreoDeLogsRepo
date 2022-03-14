using BusinessLayer;
using System.ComponentModel;
using System.Text;

namespace LogMonitor
{
    public partial class FrmMain : Form
    {
        TelegramBotAPI.TelegramBot bot;
        List<string> listResponseCodes= new List<string>();

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            if (!FrontOperations.Initialize())
            {
                MessageBox.Show("Error en la carga de configuración inicial, errores en log Application");
                Environment.Exit(0);
            }
            try
            {
                bot = new("1992248379:AAF2HeCDzRVR2R_4tXdLWMmXwYzhaGUgF5I", "-562147827");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Environment.Exit(0);
            }
            buttonLifeTest.Enabled = false;
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = false;
            textBoxLogName.Enabled = false;
            timerEach.Interval = Helpers.monitorInterval * 60000;
            bkGrdWkReadLog.RunWorkerAsync();
            timerEach.Start();
            buttonLifeTest.Enabled = true;
        }

        private void TimerEach_Tick(object sender, EventArgs e)
        {
            bkGrdWkReadLog.RunWorkerAsync();
        }

        private void BkGrdWkReadLog_DoWork(object sender, DoWorkEventArgs e)
        {
            int numTrxSuccess = 0;
            int numTrxFailure = 0;
            try
            {
                if ((24 - DateTime.Now.Hour) > (24 - Helpers.hourFinalMonitor) && (24 - DateTime.Now.Hour) < (24 - Helpers.hourInitialMonitor))
                {
                    DateTime dateTimeReference = DateTime.Now;
                    TimeSpan timeSpan;
                    if (radioButtonPx.Checked)
                    {
                        timeSpan = dateTimeReference.Subtract(DateTime.Parse(Operations.ReadLogEntries(textBoxLogName.Text, Operations.LogTypeEntry.PX, ref numTrxSuccess, ref numTrxFailure, ref listResponseCodes)));
                    }
                    else if (radioButtonTpv.Checked)
                    {
                        timeSpan = dateTimeReference.Subtract(DateTime.Parse(Operations.ReadLogEntries(textBoxLogName.Text, Operations.LogTypeEntry.Tpv, ref numTrxSuccess, ref numTrxFailure, ref listResponseCodes)));
                    }
                    else if (radioButtonWsTrx.Checked)
                    {
                        timeSpan = dateTimeReference.Subtract(DateTime.Parse(Operations.ReadLogEntries(textBoxLogName.Text, Operations.LogTypeEntry.wsTrx, ref numTrxSuccess, ref numTrxFailure, ref listResponseCodes)));
                    }
                    else
                    {
                        timeSpan = dateTimeReference.Subtract(DateTime.Parse(Operations.ReadLogEntries(textBoxLogName.Text, Operations.LogTypeEntry.wsServicePayment, ref numTrxSuccess, ref numTrxFailure, ref listResponseCodes)));
                    }

                    if (Helpers.validateNotSales)
                    {
                        if (timeSpan.Minutes > Helpers.maxTimeNotSales)
                        {
                            Task.Factory.StartNew(() => bot.SendMessageAsync("No se han detectado ventas para el cliente: " + Helpers.clientName + " en " + Helpers.maxTimeNotSales.ToString() + " minutos"));
                        }
                    }

                    e.Result = numTrxSuccess.ToString() + "|" + numTrxFailure.ToString();
                }
            }
            catch (Exception ex)
            {
                Helpers.Log(Helpers.GetPathCall(ex.Message + " valor: " + Helpers.lastDateTimeRecord), Helpers.LogType.error);
                Task.Factory.StartNew(() => bot.SendMessageAsync("Error en el monitor de logs para el cliente: " + Helpers.clientName + ". " + Helpers.GetPathCall(ex.Message + " valor: " + Helpers.lastDateTimeRecord)));
            }

        }

        private void BkGrdWkReadLog_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                string[] split = e.Result.ToString().Split("|");
                if (int.Parse(split[1]) > Helpers.numTrxError)
                {
                    StringBuilder codes=new StringBuilder();
                    listResponseCodes.ForEach(x => codes.Append(x+ ", "));
                    Helpers.Log("Se detectaron " + int.Parse(split[1]).ToString() + " errores: " + codes.ToString(), Helpers.LogType.warnning);
                    Task.Factory.StartNew(() => bot.SendMessageAsync("Se detectaron " + int.Parse(split[1]).ToString() + " errores: " + codes.ToString() +
                        " con el monitor de logs para el cliente " + Helpers.clientName));
                }
                Helpers.Log("Fecha y hora del último registro: " + Helpers.lastDateTimeRecord, Helpers.LogType.info);
            }
        }

        private void buttonLifeTest_Click(object sender, EventArgs e)
        {
            try
            {
                Helpers.Log("Es una prueba del monitor  de Q1C", Helpers.LogType.info);
                Task.Factory.StartNew(() => bot.SendMessageAsync("Es una prueba del monitor  de Q1C"));
            }
            catch (Exception ex)
            {
                Helpers.Log(ex.Message, Helpers.LogType.error);
            }            
        }
    }
}
