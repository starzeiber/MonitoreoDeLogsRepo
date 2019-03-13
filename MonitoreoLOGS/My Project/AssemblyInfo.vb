﻿Imports System
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

'@UMB   120613  version 1.0.0.0 se crea la aplicación que lee los registros de un log especificamente indicado evaluando su respuesta como transacción exitosa o no y envia correo de reporte
'@UMB   200613  version 2.0.0.0 se comienza la restructuración del programa para que sea monitoreo automático y secuencial con liberación de recursos
'@UMB   120613  version 3.0.0.0 se agrupan las funciones para encapsular métodos y realizar una monitoreo con TIMER además de ser configurable por .config
'@UMB   180214  Versión 3.1.0.0 se agrega un monitoreo especial a Isend en la madrugada y se agrega el log de errores cuando haya excepciones
'@      091118  Versión 4.0.0.0 Se re estructuró para manejar la mensajería TPV además de la del PX

<Assembly: AssemblyVersion("4.0.0.0")>
<Assembly: AssemblyFileVersion("4.0.0.0")>
