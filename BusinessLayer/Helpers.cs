using System.Diagnostics;
using System.Text.RegularExpressions;

namespace BusinessLayer
{
    /// <summary>
    /// Clase que contiene varias funciones y variables que se pueden utilizar en todo el proyecto
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Encabezado de un mensaje 13
        /// </summary>
        public static string HEADER_REQUEST_TAE_PX = "13";

        /// <summary>
        /// Encabezado de un mensaje 14
        /// </summary>
        public static String HEADER_RESPONSE_TAE_PX = "14";

        /// <summary>
        /// Encabezado de un mensaje 17
        /// </summary>
        public static String HEADER_QUERY_TAE_PX = "17";

        /// <summary>
        /// Encabezado de un mensaje 18
        /// </summary>
        public static String HEADER_RESPONSE_QUERY_TAE_PX = "18";

        /// <summary>
        /// Encabezado de un mensaje 25
        /// </summary>
        public static String HEADER_REQUEST_DATOS_PX = "25";

        /// <summary>
        /// Encabezado de un mensaje 26
        /// </summary>
        public static String HEADER_RESPONSE_DATOS_PX = "26";

        /// <summary>
        /// Encabezado de un mensaje 27
        /// </summary>
        public static String HEADER_QUERY_DATOS_PX = "27";

        /// <summary>
        /// Encabezado de un mensaje 28
        /// </summary>
        public static String HEADER_RESPONSE_QUERY_DATOS_PX = "28";

        /// <summary>
        /// Encabezado de un mensaje XML de compra TAE
        /// </summary>
        public static String HEADER_REQUEST_TAE_XML = "ReloadRequest";

        /// <summary>
        /// Encabezado de un mensaje XML de respuesta a compra TAE
        /// </summary>
        public static String HEADER_RESPONSE_REQUEST_TAE_XML = "ReloadResponse";

        /// <summary>
        /// Encabezado de un mensaje XML de consulta TAE
        /// </summary>
        public static String HEADER_QUERY_TAE_XML = "QueryRequest";

        /// <summary>
        /// Encabezado de un mensaje XML de respuesa de compra TAE
        /// </summary>
        public static String HEADER_RESPONSE_QUERY_TAE_XML = "QueryResponse";

        /// <summary>
        /// Encabezado de un mensaje XML de compra Datos
        /// </summary>
        public static String HEADER_REQUEST_DATOS_XML = "DataRequest";

        /// <summary>
        /// Encabezado de un mensaje XML de respuesta de compra Datos
        /// </summary>
        public static String HEADER_RESPONSE_REQUEST_DATOS_XML = "DataResponse";

        /// <summary>
        /// Encabezado de un mensaje XML de consulta Datos
        /// </summary>
        public static String HEADER_QUERY_DATOS_XML = "DataQueryRequest";

        /// <summary>
        /// Encabezado de un mensaje XML de respuesa consulta Datos
        /// </summary>
        public static String HEADER_RESPONSE_QUERY_DATOS_XML = "DataQueryResponse";

        /// <summary>
        /// Valor del timeout sobre una compra
        /// </summary>
        public static int timeOut;

        /// <summary>
        /// Nombre del log en el sistema
        /// </summary>
        public static string logName = string.Empty;

        /// <summary>
        /// cadena de conexión al BO
        /// </summary>
        public static string cadenaConexionBO = string.Empty;

        /// <summary>
        /// Cadena de conexión a la base administrativa del ws
        /// </summary>
        public static string cadenaConexionTrx = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public static string cadenaConexionAdmin = string.Empty;

        /// <summary>
        /// Instancia del performance counter de peticiones entrantes
        /// </summary>
        public static PerformanceCounter performancePeticionesEntrantesClientesUserver;

        /// <summary>
        /// Instancia del performance counter de peticiones respondidas al cliente
        /// </summary>
        public static PerformanceCounter performancePeticionesRespondidasClientesUserver;

        /// <summary>
        /// Instancia del performance counter de peticiones salientes a proveedor
        /// </summary>
        public static PerformanceCounter performancePeticionesSalientesProveedorUserver;

        /// <summary>
        /// Instancia del performance counter de peticiones respondidas del proveedor
        /// </summary>
        public static PerformanceCounter performancePeticionesRespondidasProveedorUserver;

        public static int monitorInterval;

        public static int maxTimeNotSales;

        public static int hourInitialMonitor;

        public static int hourFinalMonitor;

        public static string clientName = String.Empty;

        public static int numTrxError;

        public static bool withCertificate;

        public static bool validateNotSales;

        private static int contadorDeReferencia = 1;

        public static string lastDateTimeRecord = string.Empty;

        //private static readonly CheaderTPV.CheaderTPV cheaderTPV = new CheaderTPV.CheaderTPV();

        /// <summary>
        /// Log del sistema
        /// </summary>
        private static EventLogTraceListener logListener;

        /// <summary>
        /// Ip donde se coloca la aplicación
        /// </summary>
        public static string ipLocal = string.Empty;
        /// <summary>
        /// Puerto local asignado al servidor
        /// </summary>
        public static int puertoLocal;

        /// <summary>
        /// ip del switch del proveedor
        /// </summary>
        public static string ipProveedor = string.Empty;
        ///// <summary>
        ///// puerto del switch del proveedor
        ///// </summary>
        //public static int puertoProveedor;
        //public static List<int> listaPuertosProveedor = new List<int>();

        /// <summary>
        /// Número aleatorio para seleccionar un puerto disponible
        /// </summary>
        private static Random random;


        /// <summary>
        /// listado de códigos de respuesta del sistema
        /// </summary>
        public enum ResponseCodes
        {
            /// <summary>
            /// 
            /// </summary>
            TransaccionExitosa = 0,
            /// <summary>
            /// 
            /// </summary>
            TerminalInvalida = 2,
            /// <summary>
            /// 
            /// </summary>
            Denegada = 4,
            /// <summary>
            /// 
            /// </summary>
            ErrorEnRed = 5,
            /// <summary>
            /// 
            /// </summary>
            TimeOutInterno = 6,
            /// <summary>
            /// 
            /// </summary>
            ErrorGuardandoDB = 7,
            /// <summary>
            /// 
            /// </summary>
            SinRespuestaCarrier = 8,
            /// <summary>
            /// 
            /// </summary>
            NoExisteOriginal = 9,
            /// <summary>
            /// 
            /// </summary>
            ErrorTELCELTablaLLena = 15,
            /// <summary>
            /// 
            /// </summary>
            ErrorAccesoDB = 16,
            /// <summary>
            /// 
            /// </summary>
            ErrorFormato = 30,
            /// <summary>
            /// 
            /// </summary>
            NumeroTelefono = 35,
            /// <summary>
            /// 
            /// </summary>
            ErrorProceso = 50,
            /// <summary>
            /// 
            /// </summary>
            ClienteBloqueado = 65,
            /// <summary>
            /// 
            /// </summary>
            SinCreditoDisponible = 66,
            /// <summary>
            /// 
            /// </summary>
            ErrorObteniendoCredito = 67,
            /// <summary>
            /// 
            /// </summary>
            ErrorConexionServer = 70,
            /// <summary>
            /// 
            /// </summary>
            SinRespuestaSwitch = 71,
            /// <summary>
            /// 
            /// </summary>
            TimeOutCarrier = 72,
            /// <summary>
            /// 
            /// </summary>
            CarrierAbajo = 73,
            /// <summary>
            /// TelefonoNoSuceptibleARecargas
            /// </summary>
            TelefonoNoSuceptibleARecargas = 87,
            /// <summary>
            /// 
            /// </summary>
            MontoInvalido = 88
        }

        /// <summary>
        /// Enumerado con los tipos de log
        /// </summary>
        public enum LogType
        {
            /// <summary>
            /// Informativo
            /// </summary>
            info = 0,
            /// <summary>
            /// Alerta
            /// </summary>
            warnning = 1,
            /// <summary>
            /// Error
            /// </summary>
            error
        }

        /// <summary>
        /// Enumerado para indicar el tipo de formato a otorgar a una cadena de caracteres
        /// </summary>
        public enum FormatType
        {
            /// <summary>
            /// Se rellena con cero a la izquiera
            /// </summary>
            N = 0,
            /// <summary>
            /// Se rellena con cero a la derecha
            /// </summary>
            AN,
            /// <summary>
            /// Se rellena con espacio a la derecha
            /// </summary>
            ANS
        };

        /// <summary>
        /// Configura e inicializa el log del sistema
        /// </summary>
        public static void InitializeLog(string nombre)
        {
            logName = nombre;
            logListener = new EventLogTraceListener(nombre);
            if (Trace.Listeners.Count < 2)
            {
                Trace.Listeners.Add(logListener);
            }
        }

        /// <summary>
        /// Función que graba en un log interno desde afuera de la capa de negocio
        /// </summary>
        /// <param name="mensaje"></param>
        /// <param name="tipoLog"></param>
        public static void Log(string mensaje, LogType tipoLog)
        {
            switch (tipoLog)
            {
                case LogType.info:
                    Trace.TraceInformation(mensaje);
                    break;
                case LogType.warnning:
                    Trace.TraceWarning(mensaje);
                    break;
                case LogType.error:
                    Trace.TraceError(mensaje);
                    break;
                default:
                    Trace.WriteLine(mensaje);
                    break;
            }
        }

        /// <summary>
        /// Obtiene la descripción sobre un código del sistema
        /// </summary>
        /// <param name="responseCode">código del sistema</param>
        /// <returns></returns>
        public static string GetResponseCodeDescription(ResponseCodes responseCode)
        {
            string descripcion = responseCode switch
            {
                ResponseCodes.TransaccionExitosa => "TransaccionAprobada",
                ResponseCodes.TerminalInvalida => "TerminalInvalida",
                ResponseCodes.Denegada => "Denegada",
                ResponseCodes.ErrorEnRed => "ErrorEnRed",
                ResponseCodes.TimeOutInterno => "TimeOut",
                ResponseCodes.ErrorGuardandoDB => "ErrorGuardandoDB",
                ResponseCodes.NoExisteOriginal => "NoExisteOriginal",
                ResponseCodes.ErrorTELCELTablaLLena => "ErrorTELCELTablaLLena",
                ResponseCodes.ErrorAccesoDB => "ErrorAccesoDB",
                ResponseCodes.ErrorFormato => "ErrorDeFormato",
                ResponseCodes.NumeroTelefono => "NumeroInvalido",
                ResponseCodes.ErrorProceso => "ErrorProceso",
                ResponseCodes.ClienteBloqueado => "ClienteBloqueado",
                ResponseCodes.SinCreditoDisponible => "SinCreditoDisponible",
                ResponseCodes.ErrorObteniendoCredito => "ErrorObteniendoCredito",
                ResponseCodes.ErrorConexionServer => "ErrorConexionServer",
                ResponseCodes.SinRespuestaCarrier => "SinRespuestaCarrier",
                ResponseCodes.CarrierAbajo => "CarrierAbajo",
                ResponseCodes.TelefonoNoSuceptibleARecargas => "TelefonoNoSuceptibleARecargas",
                ResponseCodes.MontoInvalido => "MontoInvalido",
                ResponseCodes.SinRespuestaSwitch => "SinRespuestaSwitch",
                ResponseCodes.TimeOutCarrier => "TimeOutCarrier",
                _ => "Respuesta del carrier",
            };
            return descripcion;

        }

        /// <summary>
        /// Obtiene la descripción sobre un código del sistema
        /// </summary>
        /// <param name="codigo">código del sistema</param>
        /// <returns></returns>
        public static String GetResponseCodeDescription(int codigo)
        {
            string descripcion = codigo switch
            {
                0 => "TransaccionAprobada",
                2 => "TerminalInvalida",
                4 => "Denegada",
                5 => "ErrorEnRed",
                6 => "TimeOut",
                7 => "ErrorGuardandoDB",
                9 => "NoExisteOriginal",
                15 => "ErrorTELCELTablaLLena",
                16 => "ErrorAccesoDB",
                30 => "ErrorDeFormato",
                35 => "NumeroInvalido",
                50 => "ErrorProceso",
                65 => "ClienteBloqueado",
                66 => "SinCreditoDisponible",
                67 => "ErrorObteniendoCredito",
                70 => "ErrorConexionServer",
                71 => "SinRespuestaCarrier",
                73 => "CarrierAbajo",
                87 => "TelefonoNoSuceptibleARecargas",
                88 => "MontoInvalido",
                _ => "Respuesta del carrier",
            };
            return descripcion;

        }

        /// <summary>
        /// Obtiene el nombre de la función en conjunto con la clase a la que pertenece y todas sus propiedades con su valor en una cadena de texto
        /// </summary>
        /// <param name="evento"></param>
        /// <param name="memberName"></param>
        /// <param name="sourceFilePath"></param>
        /// <param name="sourceLineNumber"></param>
        /// <returns></returns>
        public static string GetPathCall(string evento, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
        [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
        [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            try
            {
                StackTrace st = new(new StackFrame(1));
                //string infoMetodo = st.GetFrame(0).GetMethod().DeclaringType.FullName + "." +
                //    memberName + ". " + sourceFilePath + ". " + sourceLineNumber + ". " + evento;
                string infoMetodo = sourceFilePath + ". " + sourceLineNumber + ". " +
                    st.GetFrame(0).GetMethod().DeclaringType.FullName +
                    "." + memberName + ". " + evento;
                return infoMetodo;
            }
            catch (Exception ex)
            {
                return "Error al obtener el nombre de la funcion: " + ex.Message;
            }

        }

        /// <summary>
        /// Función para obtener un número aleatorio confiable
        /// </summary>
        /// <returns></returns>
        public static int ObtenerNumeroResultadoAleatorio(int countDigits)
        {
            try
            {
                Thread.Sleep(70);
                int seed = Environment.TickCount;
                random = new Random(seed);
                int aleatorio = 1;
                bool seBloquea = Monitor.TryEnter(random);
                string numDigit = "9";
                while (numDigit.Length < countDigits)
                {
                    numDigit += "9";
                }
                if (seBloquea)
                {
                    aleatorio = random.Next(1, int.Parse(numDigit));
                    Monitor.Exit(random);
                }
                return aleatorio;
            }
            catch (Exception ex)
            {
                Log(GetPathCall(ex.Message), LogType.error);
                return 0;
            }
        }

        /// <summary>
        /// Obtiene un número de referencia para la trama TPV al proveedor
        /// </summary>
        /// <returns></returns>
        public static int ObtenerNumeroReferencia()
        {
            if (contadorDeReferencia > 999999)
            {
                Interlocked.Exchange(ref contadorDeReferencia, 1);
            }
            else
            {
                Interlocked.Increment(ref contadorDeReferencia);
            }
            return contadorDeReferencia;
        }

        /// <summary>
        /// Función que realiza la adecuación de una cadena de caracteres dependiendo el tipo de formato establecido
        /// </summary>
        /// <param name="caracters">Cadena de caracteres a realizar un formato</param>
        /// <param name="formatType">Tipo de formato a otorgar</param>
        /// <param name="length">Longitud final de la cadena</param>
        /// <returns></returns>
        public static object ValueFormatting(string caracters, FormatType formatType, int length)
        {
            try
            {
                switch (formatType)
                {
                    case FormatType.N:
                        while (caracters.Length < length)
                        {
                            caracters = "0" + caracters;
                        }
                        break;
                    case FormatType.AN:
                        while (caracters.Length < length)
                        {
                            caracters += " ";
                        }
                        break;
                    default:
                        while (caracters.Length < length)
                        {
                            caracters += " ";
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Log(GetPathCall(ex.Message), LogType.error);
                return String.Empty;
            }
            return caracters;
        }

        /// <summary>
        /// Funciona para convertir los bytes a kb, gb etc en formato string
        /// </summary>
        /// <param name="bytes">cantidad de bytes a formatear</param>
        /// <returns></returns>
        public static string ConvertirBytesFormato(int bytes)
        {
            string[] Suffix = { "B", "KB", "MB", "GB", "TB" };
            int i;
            double dblSByte = bytes;
            for (i = 0; i < Suffix.Length && bytes >= 1024; i++, bytes /= 1024)
            {
                dblSByte = bytes / 1024.0;
            }

            return String.Format("{0:0.##} {1}", dblSByte, Suffix[i]);
        }

        ///// <summary>
        ///// Obtiene el encabezado hex de una trama
        ///// </summary>
        ///// <param name="trama"></param>
        ///// <returns></returns>
        //public static string GetHexHeader(string trama)
        //{
        //    try
        //    {
        //        return cheaderTPV.CreaHeader(trama.Length);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log(GetPathCall(ex.Message), LogType.error);
        //        return "";
        //    }

        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static Boolean HasSpecialCaracteres(String valor)
        {
            var regex = new Regex(@"[^a-zA-Z0-9:/ ]");
            if (regex.IsMatch(valor))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
