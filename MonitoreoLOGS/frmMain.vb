Imports System.Globalization
Imports System.IO
Imports System.Configuration

''' <summary>
''' Clase que realiza un monitoreo de los logs en los visores de sucesos
''' </summary>
''' <remarks>UMB
''' Fecha de creación: 06/06/13</remarks>
Public Class frmMain

#Region "variables globales"

    '@070613 se instancia a un eventlog para poder escribir cualquier error
    Dim oMyLog As EventLog = New EventLog()
    Dim sUltimoReg As String = ""

    ''' <summary>
    ''' Intervalo entre cada monitoreo en milisegundos
    ''' </summary>
    ''' <remarks>UMB
    ''' Fecha de creación: 130613</remarks>
    Private iIntervaloMonitoreo As Integer

    ''' <summary>
    ''' Intervalo entre la ultima venta y la hora actual en minutos
    ''' </summary>
    ''' <remarks>UMB
    ''' Fecha de creación: 130613</remarks>
    Private iTiempoMaxNoVentas As Integer

    ''' <summary>
    ''' Hora inicial del monitoreo en formato de 24 horas
    ''' </summary>
    ''' <remarks></remarks>
    Private iHoraInicioMonitoreo As Integer

    ''' <summary>
    ''' Hora de fin del monitoreo en formato de 24 horas
    ''' </summary>
    ''' <remarks></remarks>
    Private iHoraFinMonitoreo As Integer

    ' ''' <summary>
    ' ''' Nombre de la cadena a monitorear
    ' ''' </summary>
    ' ''' <remarks></remarks>
    Private sNombreCadena As String

    Private correo As New EnvioCorreo.EnviarCorreo

    Private configuracionCorreo As New EnvioCorreo.ConfiguracionCorreo

#End Region

#Region "Funciones locales"

    Private Function CargaInicial(ByRef respuesta As String) As Boolean
        Try
            iIntervaloMonitoreo = Integer.Parse(ConfigurationManager.AppSettings("TiempoIntervalo")) * 60000
            iTiempoMaxNoVentas = Integer.Parse(ConfigurationManager.AppSettings("TiempoIntervalo"))
            iHoraInicioMonitoreo = Integer.Parse(ConfigurationManager.AppSettings("Hora_Inicio_Monitoreo"))
            iHoraFinMonitoreo = Integer.Parse(ConfigurationManager.AppSettings("Hora_Fin_Monitoreo"))
            sNombreCadena = ConfigurationManager.AppSettings("NombreCadena")

            If String.Compare(ConfigurationManager.AppSettings("conCertificado"), "Si") = 0 Then
                configuracionCorreo.conCertificado = True
            Else
                configuracionCorreo.conCertificado = False
            End If

            configuracionCorreo.smtp = ConfigurationManager.AppSettings("smtp")
            configuracionCorreo.usuario = ConfigurationManager.AppSettings("usuario")
            configuracionCorreo.pass = ConfigurationManager.AppSettings("pass")
            configuracionCorreo.puerto = Integer.Parse(ConfigurationManager.AppSettings("puerto"))
            Dim contadorAux As Integer = Integer.Parse(ConfigurationManager.AppSettings("numeroDestinatarios"))
            For index = 1 To contadorAux
                configuracionCorreo.listaDestinatarios.Add(ConfigurationManager.AppSettings("destinatario" & index.ToString()))
            Next
            contadorAux = Integer.Parse(ConfigurationManager.AppSettings("numeroDestinatariosError"))
            For index = 1 To contadorAux
                configuracionCorreo.listaDestinatarios.Add(ConfigurationManager.AppSettings("destinatarioError" & index.ToString()))
            Next
            configuracionCorreo.remitente = configuracionCorreo.usuario
            Return True
        Catch ex As Exception
            respuesta = ex.Message
            Return False
        End Try
    End Function

    Private Function CrearLog(ByRef respuesta As String) As Boolean
        Try
            '@070613 se indica el log a donde se va a escribir en caso de error
            oMyLog.Source = "MonLog"

            '@120613 se revisa que exista el visor de sucesos para la aplicación de lo contrario se crea
            If Not EventLog.SourceExists("MonLog") Then
                If Not EventLog.Exists("MonLog") Then
                    EventLog.CreateEventSource("MonLog", "MonLog")
                End If
            End If
            Return True
        Catch ex As Exception
            respuesta = ex.Message
            Return False
        End Try
    End Function

    Private Function ValidarLogMonitoreo(ByVal sNombreLOG As String) As Boolean
        '@070613 se detecta que el log a monitorear exista
        If Not EventLog.Exists(sNombreLOG, ".") Then
            '@070613 de no existir se termina el programa y se escribe en nuestro log
            oMyLog.WriteEntry("El log en el visor de sucesos del archivo: " & sNombreLOG & " no existe ", System.Diagnostics.EventLogEntryType.Error)
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' Función que lee los logs y evalua sus respuestas
    ''' </summary>
    ''' <param name="sNombreLOG">nombre del log a leer</param>
    ''' <param name="lContadorTrxExitosa">Variable que almacena el conteo de transacciones exitosas</param>
    ''' <param name="lContadorTrxNoExitosa">Variable que almacena el conteo de transacciones no exitosas</param>
    ''' <returns>la fecha y hora del ultimo registro del visor de sucesos leido</returns>
    ''' <remarks>UMB
    ''' Fecha de creación 06/06/13</remarks>
    Function FLeerLogs(ByVal sNombreLOG As String, ByRef lContadorTrxExitosa As String, ByRef lContadorTrxNoExitosa As String) As String
        Dim entradaLog As String = ""
        '@070613 se instancia otra variable que lea cada registro
        Dim oEntry As EventLogEntry = Nothing

        If RadioButton_Px.Checked = True Then
            Try
                '@070613 si existe se instancia otra variable que lea cualquier evento dentro de un log
                Dim oLog As EventLog = New EventLog
                '@070613 el log a leer
                oLog.Log = sNombreLOG

                '@070613 variable que guarda el último log leido
                Dim sUltimoRegaux As String = ""
                '@070613 se instancia otra variable que lea cada registro
                'Dim oEntry As EventLogEntry = Nothing

                '@070613 dado que el evento tendrá muchas entradas al momento de ejecutarse el programa, se traerá todas para no afectar el rendimiento del server
                For Each oEntry In oLog.Entries
                    entradaLog = oEntry.Message
                    If oEntry.Message.Length > 29 Then
                        '@070613 dado que cada vez que se ejecute leería duplicadamente los registros se compara el ultimo registro leido en el recorrido previo tomando como referencia su fecha
                        '@070613 ejemplo: 20130607 16:57:43.312500
                        If oEntry.Message.Substring(0, 24) = sUltimoReg Then
                            lContadorTrxExitosa = 0
                            lContadorTrxNoExitosa = 0

                            '@070613 una vez que se tienen todas las entradas revisamos su contenido una por una para buscar si el mensaje 14 es exitoso

                            If oEntry.Message.Substring(24, 5) = ", 140" Then

                                If oEntry.Message.Substring(307, 2) = "00" Then
                                    lContadorTrxExitosa = lContadorTrxExitosa + 1
                                Else
                                    lContadorTrxNoExitosa = lContadorTrxNoExitosa + 1
                                End If
                            End If
                            '@070613 se graba el ultimo registro leido
                            sUltimoRegaux = oEntry.Message.Substring(0, 24)
                        Else '@070613 lo unico que se hace es descartar lo que ya se pudiera haber leido
                            '@070613 una vez que se tienen todas las entradas revisamos su contenido una por una para buscar si el mensaje 14 es exitoso
                            If oEntry.Message.Substring(24, 5) = ", 140" Then

                                If oEntry.Message.Substring(307, 2) = "00" Then
                                    lContadorTrxExitosa = lContadorTrxExitosa + 1
                                Else
                                    lContadorTrxNoExitosa = lContadorTrxNoExitosa + 1
                                End If
                            End If
                            '@070613 se graba el ultimo registro leido
                            sUltimoRegaux = oEntry.Message.Substring(0, 26)
                        End If
                    End If
                Next

                '@070613 si el log no contiene entradas
                If oEntry Is Nothing Then
                    sUltimoRegaux = Date.Now.ToString()
                    oMyLog.WriteEntry("ultimo registro " & sUltimoReg & " registros tomados " & oLog.Entries.Count)
                    Return sUltimoRegaux
                Else
                    oMyLog.WriteEntry("ultimo registro " & sUltimoReg & " registros tomados " & oLog.Entries.Count)
                    '@070613 con el ultimo registro leido se empezará el conteo posterior
                    sUltimoReg = sUltimoRegaux
                    Return sUltimoReg.Substring(0, 4) & "/" & sUltimoReg.Substring(4, 2) & "/" & sUltimoReg.Substring(6)
                End If

            Catch ex As Exception
                oMyLog.WriteEntry("Error en FleerLogs: " & ex.Message & ", " & oEntry.Message & ", " & entradaLog, System.Diagnostics.EventLogEntryType.Error)
                correo.EnvioCorreo(configuracionCorreo, "Monitoreo " & sNombreCadena, "Error en la aplicación: " & ex.Message & ". Se cerrará por seguridad", True)
                Application.Exit()
                Return Date.Now.ToString()
            Finally
                GC.Collect()
            End Try
        Else
            Try
                '@070613 si existe se instancia otra variable que lea cualquier evento dentro de un log
                Dim oLog As EventLog = New EventLog
                '@070613 el log a leer
                oLog.Log = sNombreLOG

                '@070613 variable que guarda el último log leido
                Dim sUltimoRegaux As String = ""
                '@070613 se instancia otra variable que lea cada registro
                'Dim oEntry As EventLogEntry = Nothing

                '@070613 dado que el evento tendrá muchas entradas al momento de ejecutarse el programa, se traerá todas para no afectar el rendimiento del server
                For Each oEntry In oLog.Entries
                    entradaLog = oEntry.Message
                    If oEntry.Message.Length > 29 Then
                        '@070613 dado que cada vez que se ejecute leería duplicadamente los registros se compara el ultimo registro leido en el recorrido previo tomando como referencia su fecha
                        '@091118 ejemplo: 2018/11/07 09:01:56.509625
                        If oEntry.Message.Substring(0, 26) = sUltimoReg Then
                            lContadorTrxExitosa = 0
                            lContadorTrxNoExitosa = 0

                            '@070613 una vez que se tienen todas las entradas revisamos su contenido una por una para buscar si el mensaje 14 es exitoso

                            If oEntry.Message.Substring(35, 4) = ":210" Then

                                If oEntry.Message.Substring(133, 2) = "00" Then
                                    lContadorTrxExitosa = lContadorTrxExitosa + 1
                                Else
                                    lContadorTrxNoExitosa = lContadorTrxNoExitosa + 1
                                End If
                            End If
                            '@070613 se graba el ultimo registro leido
                            sUltimoRegaux = oEntry.Message.Substring(0, 26)
                        Else '@070613 lo unico que se hace es descartar lo que ya se pudiera haber leido
                            '@070613 una vez que se tienen todas las entradas revisamos su contenido una por una para buscar si el mensaje 14 es exitoso
                            If oEntry.Message.Substring(35, 4) = ":210" Then

                                If oEntry.Message.Substring(133, 2) = "00" Then
                                    lContadorTrxExitosa = lContadorTrxExitosa + 1
                                Else
                                    lContadorTrxNoExitosa = lContadorTrxNoExitosa + 1
                                End If
                            End If
                            '@070613 se graba el ultimo registro leido
                            sUltimoRegaux = oEntry.Message.Substring(0, 26)
                        End If
                    End If
                Next

                '@070613 si el log no contiene entradas
                If oEntry Is Nothing Then
                    sUltimoRegaux = Date.Now.ToString()
                    oMyLog.WriteEntry("ultimo registro " & sUltimoReg & " registros tomados " & oLog.Entries.Count)
                    Return sUltimoRegaux
                Else
                    oMyLog.WriteEntry("ultimo registro " & sUltimoReg & " registros tomados " & oLog.Entries.Count)
                    '@070613 con el ultimo registro leido se empezará el conteo posterior
                    sUltimoReg = sUltimoRegaux
                    'Return sUltimoReg.Substring(0, 4) & "/" & sUltimoReg.Substring(4, 2) & "/" & sUltimoReg.Substring(6)
                    Return sUltimoReg
                End If

            Catch ex As Exception
                oMyLog.WriteEntry("Error en FleerLogs: " & ex.Message & ", " & oEntry.Message & ", " & entradaLog, System.Diagnostics.EventLogEntryType.Error)
                correo.EnvioCorreo(configuracionCorreo, "Monitoreo " & sNombreCadena, "Error en la aplicación: " & ex.Message & ". Se cerrará por seguridad", True)
                Application.Exit()
                Return Date.Now.ToString()
            Finally
                GC.Collect()
            End Try
        End If
    End Function

#End Region

#Region "Manejadores de eventos"
    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim respuesta As String = String.Empty
        If CargaInicial(respuesta) = False Then
            MessageBox.Show("Hubo un problema en la carga inicial: " & respuesta)
            Application.Exit()
        End If
        If CrearLog(respuesta) = False Then
            MessageBox.Show("Hubo un problema en crear el log: " & respuesta)
            Application.Exit()
        End If
    End Sub

    Private Sub Button_Iniciar_Click(sender As Object, e As EventArgs) Handles Button_Iniciar.Click
        If ValidarLogMonitoreo(TextBox_NombreLog.Text) = False Then
            MessageBox.Show("No existe el log que se ingresó para monitoreo")
        Else
            Button_Iniciar.Enabled = False
            TextBox_NombreLog.Enabled = False
            myTimer.Interval = iIntervaloMonitoreo
            BackgroundWorker_Validacion.RunWorkerAsync()
            myTimer.Start()
        End If
    End Sub

    Private Sub myTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles myTimer.Tick
        BackgroundWorker_Validacion.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorker_Validacion_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker_Validacion.DoWork
        Try
            oMyLog.WriteEntry("resta: 24 - Date.Now.Hour=" & (24 - Date.Now.Hour).ToString())
            '@06112013 para realizar el monitoreo en un horario conveniente realizo una resta de las 24 hrs del día menos la hora de inicio también de la hora de fin del monitoreo
            '@06112013 y luego realizo la resta de las 24 horas menos la hora actual para compararlas y tener un rango de acción que solo entre ese horario se realice
            If (24 - Date.Now.Hour) > (24 - iHoraFinMonitoreo) And (24 - Date.Now.Hour) < (24 - iHoraInicioMonitoreo) Then

                myTimer.Interval = iIntervaloMonitoreo
                Dim sNombreLOG As String = TextBox_NombreLog.Text

                oMyLog.WriteEntry("inicio de conteo en el log " & TextBox_NombreLog.Text & " , " & Date.Now)
                '@070613 variables que cuentan los registros del visor de sucesos
                Dim lContadorTrxExitosa As Long = 0
                Dim lContadorTrxNoExitosa As Long = 0

                '@120613 se revisa que la diferencia entre la ultima transacción de un log y la hora actual no supera la media hora
                If DateDiff(DateInterval.Minute, CDate(FLeerLogs(sNombreLOG, lContadorTrxExitosa, lContadorTrxNoExitosa)), Date.Now) > iTiempoMaxNoVentas Then
                    oMyLog.WriteEntry("No se han detectado ventas en el log " & sNombreLOG & " en mas de " & iTiempoMaxNoVentas & " minutos", System.Diagnostics.EventLogEntryType.Warning)
                    e.Result = "0|0"
                Else
                    e.Result = lContadorTrxExitosa.ToString() & "|" & lContadorTrxNoExitosa.ToString()
                End If
                oMyLog.WriteEntry("Fin de conteo " & Date.Now)
            End If
        Catch ex As Exception
            '@070613 cualquier error se escribe a nuestro log
            oMyLog.WriteEntry("Error en mytime.tick" & ex.Message, System.Diagnostics.EventLogEntryType.Error)
            correo.EnvioCorreo(configuracionCorreo, "Monitoreo " & sNombreCadena, "Error en la aplicación: " & ex.Message & ". Se cerrará por seguridad", True)
            Application.Exit()
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub BackgroundWorker_Validacion_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker_Validacion.RunWorkerCompleted
        Dim numeroTrxErrorValidacion As Integer = Integer.Parse(ConfigurationManager.AppSettings("numeroTrxError"))
        If e.Result = Nothing Then

        Else
            Dim splitResultado() As String
            splitResultado = e.Result.ToString.Split(New Char() {"|"})
            If splitResultado(0) = "0" And splitResultado(1) = "0" Then
                correo.EnvioCorreo(configuracionCorreo, "Monitoreo " & sNombreCadena, "No se han detectado ventas en el log " & TextBox_NombreLog.Text & " en mas de " & iTiempoMaxNoVentas & " minutos", True)
            ElseIf Integer.Parse(splitResultado(1)) > numeroTrxErrorValidacion Then
                correo.EnvioCorreo(configuracionCorreo, "Monitoreo " & sNombreCadena, "Se han detectado más de 10 transacciones con error en el log " & TextBox_NombreLog.Text & " en el último periodo de " & iTiempoMaxNoVentas & " minutos", True)
            End If
        End If
    End Sub

#End Region

End Class
