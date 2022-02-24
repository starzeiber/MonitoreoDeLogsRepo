using BusinessLayer;
using System.ComponentModel;
using System.Reflection;

namespace LogMonitor
{
    public partial class FrmMain : Form
    {
        TelegramBotAPI.TelegramBot bot;

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
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = false;
            textBoxLogName.Enabled = false;
            timerEach.Interval = Helpers.monitorInterval * 60000;
            bkGrdWkReadLog.RunWorkerAsync();
            timerEach.Start();
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
                        timeSpan = dateTimeReference.Subtract(DateTime.Parse(Operations.ReadLogEntries(textBoxLogName.Text, Operations.LogTypeEntry.PX, ref numTrxSuccess, ref numTrxFailure)));
                    }
                    else if (radioButtonTpv.Checked)
                    {
                        timeSpan = dateTimeReference.Subtract(DateTime.Parse(Operations.ReadLogEntries(textBoxLogName.Text, Operations.LogTypeEntry.Tpv, ref numTrxSuccess, ref numTrxFailure)));
                    }
                    else if (radioButtonWsTrx.Checked)
                    {
                        timeSpan = dateTimeReference.Subtract(DateTime.Parse(Operations.ReadLogEntries(textBoxLogName.Text, Operations.LogTypeEntry.wsTrx, ref numTrxSuccess, ref numTrxFailure)));
                    }
                    else
                    {
                        timeSpan = dateTimeReference.Subtract(DateTime.Parse(Operations.ReadLogEntries(textBoxLogName.Text, Operations.LogTypeEntry.wsServicePayment, ref numTrxSuccess, ref numTrxFailure)));
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
                Task.Factory.StartNew(() => bot.SendMessageAsync("Error en el monitor de logs para el cliente: " + Helpers.clientName + ". " + Helpers.GetPathCall(ex.Message)));
            }

        }

        private void BkGrdWkReadLog_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                string[] split = e.Result.ToString().Split("|");
                if (int.Parse(split[1]) > Helpers.numTrxError)
                {
                    Helpers.Log("Se detectaron " + int.Parse(split[1]).ToString() + " errores", Helpers.LogType.warnning);
                    Task.Factory.StartNew(() => bot.SendMessageAsync("Se detectaron " + int.Parse(split[1]).ToString() + " codigos de error " +
                        "con el monitor de logs para el cliente " + Helpers.clientName));
                }
            }
        }

    }
}
