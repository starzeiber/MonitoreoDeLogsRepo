Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices

' La información general sobre un ensamblado se controla mediante el siguiente 
' conjunto de atributos. Cambie estos atributos para modificar la información
' asociada con un ensamblado.

' Revisar los valores de los atributos del ensamblado

<Assembly: AssemblyTitle("MonitoreoLOGS")> 
<Assembly: AssemblyDescription("monitoreo de logs")> 
<Assembly: AssemblyCompany("TN")> 
<Assembly: AssemblyProduct("MonitoreoLOGS")> 
<Assembly: AssemblyCopyright("Copyright ©  2013")> 
<Assembly: AssemblyTrademark("UMB")> 

<Assembly: ComVisible(True)> 

'El siguiente GUID sirve como identificador de typelib si este proyecto se expone a COM
<Assembly: Guid("8050ef48-d1a3-4df1-b2b0-0f3aa03f44f7")>

' La información de versión de un ensamblado consta de los cuatro valores siguientes:
'
'      Versión principal
'      Versión secundaria 
'      Número de compilación
'      Revisión
'
' Puede especificar todos los valores o usar los valores predeterminados de número de compilación y de revisión 
' mediante el asterisco ('*'), como se muestra a continuación:
' <Assembly: AssemblyVersion("1.0.*")> 

'Siglas     Autor       Fecha       Versión     Descripción
'@          UMB         120613      1.0.0.0     se crea la aplicación que lee los registros de un log especificamente indicado evaluando su respuesta como transacción exitosa o no y envia correo de reporte
'@          UMB         200613      2.0.0.0     se comienza la restructuración del programa para que sea monitoreo automático y secuencial con liberación de recursos
'@          UMB         120613      3.0.0.0     se agrupan las funciones para encapsular métodos y realizar una monitoreo con TIMER además de ser configurable por .config
'@          UMB         180214      3.1.0.0     se agrega un monitoreo especial a Isend en la madrugada y se agrega el log de errores cuando haya excepciones
'@          UMB         091118      4.0.0.0     Se re estructuró para manejar la mensajería TPV además de la del PX
'@          UMB         130319      4.1.0.0     Se actualiza la versión de la DLL de correo
'@          UMB         190319      4.2.0.0     Se cambia el envío de correo de ser desde la aplicación a consumir un ws, por temas de salida a internet
'@          UMB         10/12/2020  5.0.0.0     Versión que revisa los logs de un WS pero solo para código 6



<Assembly: AssemblyVersion("5.0.0.0")>
<Assembly: AssemblyFileVersion("5.0.0.0")>
